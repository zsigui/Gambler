using Gambler.Config;
using Gambler.Module.X469;
using Gambler.Module.X469.Model;
using Gambler.Module.XPJ.Model;
using Gambler.Utils;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler.UI
{
    public partial class FormYL5Info : Form
    {

        private static FormYL5Info sInstance;

        public static FormYL5Info NewInstance()
        {
            if (sInstance == null)
            {
                sInstance = new FormYL5Info();
            } else
            {
                sInstance.Focus();
            }
            return sInstance;
        }

        public FormYL5Info()
        {
            InitializeComponent();
        }


        private bool _refreshing = false;
        private HashSet<string> _leagueSet;
        private System.Threading.Timer _refreshTimer;
        private int _refreshCountdown;
        private Dictionary<string, List<X469OddItem>> _oddDataDict;
        private List<X469OddItem> _allSourceData;
        private List<X469OddItem> _showDataList;
        private YL5Client _client;

        /// <summary>
        /// 1 按时间排序 2 按联盟排序
        /// </summary>
        private int _sortType = 1;
        private string _selectLeagueVal = null;
        private string _searchText = "";

        private bool _isInAutoRequest = false;
        private string[] _autoSearcwhKey;
        private bool _isHomePen = false;

        public void Show(YL5Client client)
        {
            _client = client;
            Show();
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        private void InitOnStart()
        {
            if (_client == null)
            {
                DialogResult r = MessageBox.Show("请先添加登录至少一个新葡京网账号！有账号请按‘是’添加，无账号请按‘否’先注册", "提示",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                if (r == DialogResult.Yes)
                {
                    Hide();
                    FormAddUser.newInstance().Show();
                    Close();
                } else if (r == DialogResult.No)
                {
                    Close();
                    System.Diagnostics.Process.Start("https://www.469355.com/");
                } else
                {
                    Close();
                }
            }
            else
            {
                CB_OrderBy.SelectedIndex = _sortType - 1;
                SetBetBtnEnabled(false);
                RefreshOrInitTimer(true);
            }
        }

        private void RefreshOrInitTimer(bool forceReset)
        {

            if (forceReset)
                _refreshCountdown = 0;

            if (_refreshTimer != null)
            {
                _refreshTimer.Change(1000, 1000);
            }
            else
            {
                _refreshTimer = ThreadUtil.RunOnTimer((state) =>
                {
                    if (_refreshCountdown == 0)
                    {
                        RefreshOddData();
                    }
                    else
                    {
                        _refreshCountdown--;
                        RefreshBtn(2);
                    }
                },
                null,
                0,
                1000);
            }
        }

        private void JudgeAutoBetRequest()
        {
           
        }

        /// <summary>
        /// 更新整个界面的显示内容
        /// </summary>
        /// <param name="odd"></param>
        private void UpdateUIData(Dictionary<string, List<X469OddItem>> odd)
        {
            _oddDataDict = odd;
            ThreadUtil.WorkOnUI<object>(this,
                new Action(() =>{
                    try
                    {
                        UpdateLeague(_oddDataDict.Keys);
                        _showDataList = GetShowDataDefault();
                        UpdateGDVData(_showDataList);

                        JudgeAutoBetRequest();
                    } catch (Exception e)
                    {
                        LogUtil.Write(e);
                    }
                }));
        }

        /// <summary>
        /// 判断新分值是否超过盘口值
        /// </summary>
        private bool JudgeBig(string scoreC, string scoreH, string project)
        {
            try
            {
                // 无有效的数值
                if (String.IsNullOrEmpty(project))
                    return false;
                float p;
                if (project.Contains("/"))
                {
                    p = float.Parse(project.Split('/')[1].Trim());
                }
                else
                {
                    p = float.Parse(project);
                }
                int total = 1;
                total += (String.IsNullOrEmpty(scoreC) ? 0 : Int32.Parse(scoreC));
                total += (String.IsNullOrEmpty(scoreH) ? 0 : Int32.Parse(scoreH));
                return total >= p;
            }
            catch (Exception e)
            {
                LogUtil.Write(e);
                return false;
            }
        }

        /// <summary>
        /// 判断让球分值, 1,2 : 主让客，主-客 3,4 ：客让主，主-客
        /// </summary>
        private int JudgeConcedePoints(string scoreC, string scoreH, string project)
        {
            try
            {
                // 无有效的数值
                if (String.IsNullOrEmpty(project))
                    return 0;
                float[] p = new float[2];
                bool isHomeWin = true;
                if (project[0] == '-')
                {
                    project = project.Substring(1);
                    isHomeWin = false;
                }
                if (project.Contains("/"))
                {
                    string[] sp = project.Split('/');
                    p[0] = float.Parse(sp[0].Trim());
                    p[1] = float.Parse(sp[1].Trim());
                }
                else
                {
                    p[0] = p[1] = float.Parse(project);
                }

                int sc = (String.IsNullOrEmpty(scoreC) ? 0 : Int32.Parse(scoreC)) + (_isHomePen ? 0 : 1);
                int sh = (String.IsNullOrEmpty(scoreH) ? 0 : Int32.Parse(scoreH)) + (_isHomePen ? 1 : 0);
                if (isHomeWin)
                {
                    if (sh - sc >= p[1])
                        return 1;
                    else if (sh - sc < p[0])
                        return 2;
                }
                else
                {
                    if (sc - sh >= p[1])
                        return 4;
                    else if (sc - sh < p[0])
                        return 3;
                }
                return 0;
            }
            catch (Exception e)
            {
                LogUtil.Write(e);
                return 0;
            }
        }

        /// <summary>
        /// 根据现有分值判断独赢, 0平局，1主赢，2客赢
        /// </summary>
        private int JudgeCapot(string scoreC, string scoreH)
        {
            int result = -1;
            int sc = (String.IsNullOrEmpty(scoreC) ? 0 : Int32.Parse(scoreC));
            int sh = (String.IsNullOrEmpty(scoreH) ? 0 : Int32.Parse(scoreH));
            if (_isHomePen)
            {
                if (sh + 1 > sc)
                    result = 1;
                else if (sh + 1 < sc)
                    result = 2;
                else
                    result = 0;
            }
            else
            {
                if (sc + 1 > sh)
                    result = 2;
                else if (sc + 1 < sh)
                    result = 1;
                else
                    result = 0;
            }
            return result;
        }

        private void UpdateGDVData(List<X469OddItem> _showDataList)
        {
            DGV_Info.Rows.Clear();
            if (_showDataList != null)
            {
                DataGridViewRow dr;
                string tmpS;
                foreach (X469OddItem d in _showDataList)
                {
                    dr = new DataGridViewRow();
                    dr.CreateCells(DGV_Info);
                    dr.Cells[0].Value = String.Format("{0}分钟", d.a19);
                    dr.Cells[1].Value = String.Format("{0} : {1}", d.a16, d.a17);

                    dr.Cells[2].Value = d.a26;
                    dr.Cells[3].Value = d.a2;
                    dr.Cells[4].Value = d.a3;
                    if (RB_Whole.Checked)
                    {

                        if (!String.IsNullOrEmpty(d.a11) && !String.IsNullOrEmpty(d.a12))
                        {
                            if (String.IsNullOrEmpty(d.a20))
                            {
                                dr.Cells[5].Value = String.Format("【客让{0}】\n【主{1:N2}】\n【客{2:N2}】", d.a10, d.a11, d.a12);
                            }
                            else
                            {
                                dr.Cells[5].Value = String.Format("【主让{0}】\n【主{1:N2}】\n【客{2:N2}】", d.a10, d.a11, d.a12);
                            }
                        }

                        if (!String.IsNullOrEmpty(d.a14) && !String.IsNullOrEmpty(d.a15))
                            dr.Cells[6].Value = String.Format("【{0}】\n【大{1:N2}】\n【小{2:N2}】", d.a13, d.a14, d.a15);
                      
                        if (!String.IsNullOrEmpty(d.a7) && !String.IsNullOrEmpty(d.a8) && !String.IsNullOrEmpty(d.a9))
                            dr.Cells[7].Value = String.Format("【主{0}】\n【客{1:N2}】\n【和{2:N2}】", d.a7, d.a8, d.a9);

                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(d.a31) && !String.IsNullOrEmpty(d.a32))
                        {
                            if (String.IsNullOrEmpty(d.a36))
                            {
                                dr.Cells[5].Value = String.Format("【客让{0}】\n【主{1:N2}】\n【客{2:N2}】", d.a30, d.a31, d.a32);
                            }
                            else
                            {
                                dr.Cells[5].Value = String.Format("【主让{0}】\n【主{1:N2}】\n【客{2:N2}】", d.a30, d.a31, d.a32);
                            }
                        }

                        if (!String.IsNullOrEmpty(d.a34) && !String.IsNullOrEmpty(d.a35))
                            dr.Cells[6].Value = String.Format("【{0}】\n【大{1:N2}】\n【小{2:N2}】", d.a33, d.a34, d.a35);

                        if (!String.IsNullOrEmpty(d.a27) && !String.IsNullOrEmpty(d.a28) && !String.IsNullOrEmpty(d.a29))
                            dr.Cells[7].Value = String.Format("【主{0}】\n【客{1:N2}】\n【和{2:N2}】", d.a27, d.a28, d.a29);

                    }
                    DGV_Info.Rows.Add(dr);
                }
            }
        }

        private void UpdateLeague(IEnumerable<string> items)
        {
            _selectLeagueVal = (CB_Leagues.SelectedIndex == 0 ? CB_Leagues.Items[CB_Leagues.SelectedIndex].ToString() : null);
            if (items != null)
            {
                CB_Leagues.Items.Clear();
                CB_Leagues.Items.Add("联盟赛列表");
                foreach (string name in items)
                {
                    CB_Leagues.Items.Add(name);
                }
                if (_selectLeagueVal != null)
                {
                    CB_Leagues.SelectedIndex = CB_Leagues.Items.IndexOf(_selectLeagueVal);
                }
            }

        }

        private List<X469OddItem> GetShowDataDefault()
        {
            return GetShowDataBySource(_searchText, _selectLeagueVal, _oddDataDict, _allSourceData);
        }

        private List<X469OddItem> GetShowDataBySource(string searchText, string leagueVal, Dictionary<string, List<X469OddItem>> odd, List<X469OddItem> source)
        {
            return SearchUtil.FilterBySearch(searchText, SearchUtil.FilterByLeague(leagueVal, odd, source));
        }
        
        private void RefreshOddData()
        {
            if (_refreshTimer == null)
            {
                RefreshOrInitTimer(true);
                return;
            }

            // 请求网络，此时先暂停定时器
            RefreshState(0);
            _client.GetAllRBOddData(
                (ret) => {
                    Dictionary<string, List<X469OddItem>> dict = X469DataParser.TransformRespDataToXPJOddDataDict(ret, out _allSourceData);
                    if (dict != null)
                        UpdateUIData(dict);
                    else
                        LogUtil.Write("解析dict数据获取失败!");
                    RefreshState(1);
                },
                (statusCode, errCode, errMsg) =>
                {
                    RefreshState(1);
                },
                (err) =>
                {
                    RefreshState(1);
                });
        }
        
        private void RefreshState(int state)
        {
            switch(state)
            {
                case 0:
                    _refreshing = false;
                    _refreshTimer.Change(Timeout.Infinite, 1000);
                    RefreshBtn(0);
                    break;
                case 1:
                    _refreshing = true;
                    _refreshCountdown = GlobalSetting.GetInstance().AutoRefreshTime;
                    RefreshOrInitTimer(false);
                    RefreshBtn(1);
                    break;
            }
        }

        private void RefreshBtn(int state)
        {
            ThreadUtil.WorkOnUI(this, new Action<int>((s) =>
            {
                switch (s)
                {
                    case 0:
                        BTN_Refresh.Text = "刷新中...";
                        BTN_Refresh.Enabled = false;
                        break;
                    case 1:
                        BTN_Refresh.Text = String.Format("刷新：{0}s", _refreshCountdown);
                        BTN_Refresh.Enabled = true;
                        break;
                    case 2:
                        BTN_Refresh.Text = String.Format("刷新：{0}s", _refreshCountdown);
                        break;
                }
            }),
            state);
        }

        private void FormInfo_Load(object sender, EventArgs e)
        {
            InitOnStart();
            if (GlobalSetting.GetInstance().IsShowHalfOddFirst)
                RB_Half.Checked = true;
            else
                RB_Whole.Checked = true;
            CB_AcceptOpt.Checked = GlobalSetting.GetInstance().IsAutoAcceptBestOdd;
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            RefreshOrInitTimer(true);
        }
        
        private void BTN_BigOrSmall_Host_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_BigOrSmall_Host.Enabled = false;
            ShowBigOrSmallBig(_showDataList[DGV_Info.SelectedRows[0].Index], false);
            BTN_BigOrSmall_Host.Enabled = true;
        }

        private void ShowBigOrSmallBig(X469OddItem oddData, bool isAuto)
        {
            YL5DialogConfirm dialog = YL5DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_BigOrSmall_Host.Text, "10", "C", CB_AcceptOpt.Checked, oddData.a14);
            }
            else
            {
                dialog.Update(oddData, BTN_BigOrSmall_Host.Text, "30", "C", CB_AcceptOpt.Checked, oddData.a14);
            }
        }

        private void ShowBigOrSmallSmall(X469OddItem oddData)
        {
            YL5DialogConfirm dialog = YL5DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_BigOrSmall_Away.Text, "10", "H", CB_AcceptOpt.Checked, oddData.a15);
            }
            else
            {
                dialog.Update(oddData, BTN_BigOrSmall_Away.Text, "30", "H", CB_AcceptOpt.Checked, oddData.a15);
            }
        }

        private void BTN_BigOrSmall_Away_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_BigOrSmall_Away.Enabled = false;
            ShowBigOrSmallSmall(_showDataList[DGV_Info.SelectedRows[0].Index]);
            BTN_BigOrSmall_Away.Enabled = true;
        }

        private void ShowCapotHost(XPJOddData oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_Capot_Host.Text, "ior_MH", oddData.ior_MH, "");
            }
            else
            {
                dialog.Update(oddData, BTN_Capot_Host.Text, "ior_HMH", oddData.ior_HMH, "");
            }
        }

        private void BTN_Capot_Host_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Capot_Host.Enabled = false;
            //ShowCapotHost(_showDataList[DGV_Info.SelectedRows[0].Index]);

            BTN_Capot_Host.Enabled = true;
        }

        private void ShowCapotNone(X469OddItem oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
//             if (RB_Whole.Checked)
//             {
//                 dialog.Update(oddData, BTN_Capot_None.Text, "ior_MN", oddData.ior_MN, "");
//             }
//             else
//             {
//                 dialog.Update(oddData, BTN_Capot_None.Text, "ior_HMN", oddData.ior_HMN, "");
//             }
        }

        private void BTN_Capot_None_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Capot_None.Enabled = false;
            ShowCapotNone(_showDataList[DGV_Info.SelectedRows[0].Index]);

            BTN_Capot_None.Enabled = true;
        }

        private void ShowCapotAway(X469OddItem oddData)
        {
//             YL5DialogConfirm dialog = YL5DialogConfirm.newInstance();
//             dialog.Show();
//             if (RB_Whole.Checked)
//             {
//                 dialog.Update(oddData, BTN_Capot_Away.Text, "ior_MC", oddData.ior_MC, "");
//             }
//             else
//             {
//                 dialog.Update(oddData, BTN_Capot_Away.Text, "ior_HMC", oddData.ior_HMC, "");
//             }
        }

        private void BTN_Capot_Away_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Capot_Away.Enabled = false;
            ShowCapotAway(_showDataList[DGV_Info.SelectedRows[0].Index]);

            BTN_Capot_Away.Enabled = true;
        }

        private void ShowConcedePointsHost(X469OddItem oddData)
        {
            YL5DialogConfirm dialog = YL5DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_ConcedePoints_Host.Text, "9", "H", CB_AcceptOpt.Checked, oddData.a11);
            }
            else
            {
                dialog.Update(oddData, BTN_ConcedePoints_Host.Text, "19", "H", CB_AcceptOpt.Checked, oddData.a31);
            }
        }

        private void BTN_ConcedePoints_Host_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_ConcedePoints_Host.Enabled = false;
            ShowConcedePointsHost(_showDataList[DGV_Info.SelectedRows[0].Index]);

            BTN_ConcedePoints_Host.Enabled = true;
        }

        private void ShowConcedePointsAway(X469OddItem oddData)
        {
            YL5DialogConfirm dialog = YL5DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_ConcedePoints_Away.Text, "9", "C", CB_AcceptOpt.Checked, oddData.a12);
            }
            else
            {
                dialog.Update(oddData, BTN_ConcedePoints_Away.Text, "19", "C", CB_AcceptOpt.Checked, oddData.a32);
            }
        }

        private void BTN_ConcedePoints_Away_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_ConcedePoints_Away.Enabled = false;
            ShowConcedePointsAway(_showDataList[DGV_Info.SelectedRows[0].Index]);
            BTN_ConcedePoints_Away.Enabled = true;
        }

        private void FormInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            sInstance = null;
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGDVData(_showDataList);
            GlobalSetting.GetInstance().IsShowHalfOddFirst = RB_Half.Checked;
        }

        private void CB_Leagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectLeagueVal = (CB_Leagues.SelectedIndex <= 0 ? null : CB_Leagues.SelectedItem.ToString());
            _showDataList = GetShowDataDefault();
            UpdateGDVData(_showDataList);
        }

        private void FormInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_refreshTimer != null)
            {
                _refreshTimer.Change(Timeout.Infinite, 1000);
            }
        }

        private void CB_OrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sortType = CB_OrderBy.SelectedIndex + 1;
            BTN_Refresh_Click(sender, e);
        }

        private void TB_Search_TextChanged(object sender, EventArgs e)
        {
            _searchText = TB_Search.Text.Trim();
        }

        private void TB_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _showDataList = GetShowDataDefault();
                UpdateGDVData(_showDataList);
            }
        }

        private void DGV_Info_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
        }

        private void DGV_Info_SelectionChanged(object sender, EventArgs e)
        {
            SetBetBtnEnabled(false);
            if (DGV_Info.SelectedRows != null && DGV_Info.SelectedRows.Count > 0)
            {
                X469OddItem d = _showDataList[DGV_Info.SelectedRows[0].Index];
                if (RB_Whole.Checked)
                {
                    if (!String.IsNullOrEmpty(d.a7) && !String.IsNullOrEmpty(d.a8) && !String.IsNullOrEmpty(d.a9))
                    {
                        BTN_Capot_Away.Enabled = true;
                        BTN_Capot_Host.Enabled = true;
                        BTN_Capot_None.Enabled = true;
                        BTN_Capot_Host.Text = String.Format("【全场-独赢】\n【主赢】\n【赔率：{0:N2}】", d.a7);
                        BTN_Capot_Away.Text = String.Format("【全场-独赢】\n【客赢】\n【赔率：{0:N2}】", d.a8);
                        BTN_Capot_None.Text = String.Format("【全场-独赢】\n【和局】\n【赔率：{0:N2}】", d.a9);
                    }
                    if (!String.IsNullOrEmpty(d.a14) && !String.IsNullOrEmpty(d.a15))
                    {
                        BTN_BigOrSmall_Away.Enabled = true;
                        BTN_BigOrSmall_Host.Enabled = true;
                        BTN_BigOrSmall_Host.Text = String.Format("【全场-大小】\n【大{0}】\n【赔率：{1:N2}】", d.a13, d.a14);
                        BTN_BigOrSmall_Away.Text = String.Format("【全场-大小】\n【小{0}】\n【赔率：{1:N2}】", d.a13, d.a15);
                    }
                    if (!String.IsNullOrEmpty(d.a11) && !String.IsNullOrEmpty(d.a12))
                    {
                        BTN_ConcedePoints_Host.Enabled = true;
                        BTN_ConcedePoints_Away.Enabled = true;
                        if (String.IsNullOrEmpty(d.a20))
                        {
                            BTN_ConcedePoints_Host.Text = String.Format("【全场-让球】\n【客让{0}】\n【主】【赔率：{1:N2}】", d.a10, d.a11);
                            BTN_ConcedePoints_Away.Text = String.Format("【全场-让球】\n【客让{0}】\n【客】【赔率：{1:N2}】", d.a10, d.a12);
                        }
                        else
                        {
                            BTN_ConcedePoints_Host.Text = String.Format("【全场-让球】\n【主让{0}】\n【主】【赔率：{1:N2}】", d.a10, d.a11);
                            BTN_ConcedePoints_Away.Text = String.Format("【全场-让球】\n【主让{0}】\n【客】【赔率：{1:N2}】", d.a10, d.a12);
                        }
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(d.a27) && !String.IsNullOrEmpty(d.a28) && !String.IsNullOrEmpty(d.a29))
                    {
                        BTN_Capot_Away.Enabled = true;
                        BTN_Capot_Host.Enabled = true;
                        BTN_Capot_None.Enabled = true;
                        BTN_Capot_Host.Text = String.Format("【半场-独赢】\n【主赢】\n【赔率：{0:N2}】", d.a27);
                        BTN_Capot_Away.Text = String.Format("【半场-独赢】\n【客赢】\n【赔率：{0:N2}】", d.a28);
                        BTN_Capot_None.Text = String.Format("【半场-独赢】\n【和局】\n【赔率：{0:N2}】", d.a29);
                    }
                    if (!String.IsNullOrEmpty(d.a34) && !String.IsNullOrEmpty(d.a35))
                    {
                        BTN_BigOrSmall_Away.Enabled = true;
                        BTN_BigOrSmall_Host.Enabled = true;
                        BTN_BigOrSmall_Host.Text = String.Format("【半场-大小】\n【大{0}】\n【赔率：{1:N2}】", d.a33, d.a34);
                        BTN_BigOrSmall_Away.Text = String.Format("【半场-大小】\n【小{0}】\n【赔率：{1:N2}】", d.a33, d.a35);
                    }
                    if (!String.IsNullOrEmpty(d.a31) && !String.IsNullOrEmpty(d.a32))
                    {
                        BTN_ConcedePoints_Host.Enabled = true;
                        BTN_ConcedePoints_Away.Enabled = true;
                        if (String.IsNullOrEmpty(d.a36))
                        {
                            BTN_ConcedePoints_Host.Text = String.Format("【半场-让球】\n【客让{0}】\n【主】【赔率：{1:N2}】", d.a30, d.a11);
                            BTN_ConcedePoints_Away.Text = String.Format("【半场-让球】\n【客让{0}】\n【客】【赔率：{1:N2}】", d.a30, d.a12);
                        }
                        else
                        {
                            BTN_ConcedePoints_Host.Text = String.Format("【半场-让球】\n【主让{0}】\n【主】【赔率：{1:N2}】", d.a30, d.a11);
                            BTN_ConcedePoints_Away.Text = String.Format("【半场-让球】\n【主让{0}】\n【客】【赔率：{1:N2}】", d.a30, d.a12);
                        }
                    }
                }
            }
        }

        private void SetBetBtnEnabled(bool enabled)
        {
            BTN_BigOrSmall_Away.Text = "无";
            BTN_BigOrSmall_Host.Text = "无";
            BTN_Capot_Away.Text = "无";
            BTN_Capot_Host.Text = "无";
            BTN_Capot_None.Text = "无";
            BTN_ConcedePoints_Away.Text = "无";
            BTN_ConcedePoints_Host.Text = "无";
            BTN_ConcedePoints_Away.Text = "无";
            BTN_ConcedePoints_Host.Text = "无";

            BTN_BigOrSmall_Away.Enabled = enabled;
            BTN_BigOrSmall_Host.Enabled = enabled;
            BTN_Capot_Away.Enabled = enabled;
            BTN_Capot_Host.Enabled = enabled;
            BTN_Capot_None.Enabled = enabled;
            BTN_ConcedePoints_Away.Enabled = enabled;
            BTN_ConcedePoints_Host.Enabled = enabled;
            BTN_ConcedePoints_Away.Enabled = enabled;
            BTN_ConcedePoints_Host.Enabled = enabled;
        }

        public void AutoRequest(string[] searchKeys, bool isHome)
        {
            this.TopMost = true;
            _isInAutoRequest = true;
            _isHomePen = isHome;
            _autoSearcwhKey = searchKeys;
            _searchText = String.Format("l:{0} h:{1} a:{2}", _autoSearcwhKey[0], _autoSearcwhKey[1], _autoSearcwhKey[2]);
            TB_Search.Text = _searchText;
            _selectLeagueVal = null;
            CB_Leagues.SelectedIndex = 0;

            BTN_Refresh_Click(null, null);
            this.TopMost = false;
        }

        private void CB_AcceptOpt_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSetting.GetInstance().IsAutoAcceptBestOdd = CB_AcceptOpt.Checked;
        }
        
        private void DGV_Info_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewCell cell = DGV_Info.Rows[e.RowIndex].Cells[e.ColumnIndex];
                CommonUtil.Copy(cell.Value.ToString());
            }
            
        }

        private void TB_Search_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CommonUtil.SelectText(TB_Search);
        }
    }
}
