using Gambler.Config;
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
    public partial class FormInfo : Form
    {

        private static FormInfo sInstance;

        public static FormInfo NewInstance()
        {
            if (sInstance == null)
            {
                sInstance = new FormInfo();
            } else
            {
                sInstance.Focus();
            }
            return sInstance;
        }

        public FormInfo()
        {
            InitializeComponent();
        }


        private bool _refreshing = false;
        private HashSet<string> _leagueSet;
        private System.Threading.Timer _refreshTimer;
        private int _refreshCountdown;
        private Dictionary<string, List<XPJOddData>> _oddDataDict;
        private List<XPJOddData> _allSourceData;
        private List<XPJOddData> _showDataList;
        private XPJClient _client;

        /// <summary>
        /// 1 按时间排序 2 按联盟排序
        /// </summary>
        private int _sortType = 1;
        private string _selectLeagueVal = null;
        private string _searchText = "";
        private string _gameType = XPJClient.GameType.FT_RB_MN;

        private bool _isInAutoRequest = false;
        private string[] _autoSearcwhKey;
        private bool _isHomePen = false;

        public void Show(XPJClient client)
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
                    System.Diagnostics.Process.Start("http://www.1559501.com/");
                } else
                {
                    Close();
                }
            }
            else
            {
                CB_OrderBy.SelectedIndex = _sortType - 1;
                CB_Leagues.SelectedIndex = 0;
                CB_GameType.SelectedIndex = 0;
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
            // 界面更新完成之后，判断是否进行下注
            if (_isInAutoRequest)
            {
                _isInAutoRequest = false;
                if (GlobalSetting.GetInstance().IsAutoBet
                    && _showDataList != null && _showDataList.Count > 0)
                {
                    // 随机下注方式，Random是否下注
                    if (GlobalSetting.GetInstance().GameBetMethod == BetMethod.Random
                        && (int)(new Random().NextDouble() * 2) == 0)
                        return;

                    BetType betType = GlobalSetting.GetInstance().GameBetType;
                    float mostOdd = 0;
                    XPJOddData data;
                    int mostIndex = -1;
                    int whoWin = 0; // 0 平 1 主赢 2 客赢
                    switch (betType)
                    {
                        case BetType.BigOrSmall:
                            // 可能存在多个下注选项
                            for (int i = 0; i < _showDataList.Count; i++)
                            {
                                data = _showDataList[i];
                                if (JudgeBig(data.scoreC, data.scoreH, data.CON_OUH))
                                {
                                    if (mostOdd < data.ior_OUH)
                                    {
                                        mostOdd = data.ior_OUH;
                                        mostIndex = i;
                                    }
                                }
                            }
                            Console.WriteLine("查找到的 mostIndex = " + mostIndex);
                            if (mostIndex != -1 && mostOdd >= GlobalSetting.GetInstance().AutoBetRate)
                            {
                                RB_Whole.Checked = true;
                                ShowBigOrSmallBig(_showDataList[mostIndex], true);
                            }
                            break;
                        case BetType.Capot:
                            for (int i = 0; i < _showDataList.Count; i++)
                            {
                                data = _showDataList[i];
                                switch ((whoWin = JudgeCapot(data.scoreC, data.scoreH)))
                                {
                                    case 0:
                                        if (mostOdd < data.ior_HOUH)
                                        {
                                            mostOdd = data.ior_HOUH;
                                            mostIndex = i;
                                        }
                                        break;
                                }
                            }
                            if (mostIndex != -1 && mostOdd >= GlobalSetting.GetInstance().AutoBetRate)
                            {
                                RB_Whole.Checked = true;
                                RB_Half.Checked = false;
                                switch (whoWin)
                                {
                                    case 0:
                                        ShowCapotNone(_showDataList[mostIndex]);
                                        break;
                                    case 1:
                                        ShowCapotHost(_showDataList[mostIndex]);
                                        break;
                                    case 2:
                                        ShowCapotAway(_showDataList[mostIndex]);
                                        break;
                                }
                            }
                            break;
                        case BetType.ConcedePoints:
                            int state;
                            for (int i = 0; i < _showDataList.Count; i++)
                            {
                                data = _showDataList[i];
                                if ((state = JudgeConcedePoints(data.scoreC, data.scoreH, data.CON_RH)) != 0)
                                {
                                    if (mostOdd < data.ior_HOUH)
                                    {
                                        mostOdd = data.ior_HOUH;
                                        mostIndex = i;
                                    }
                                }
                            }
                            break;
                        case BetType.HalfBigOrSmall:
                            for (int i = 0; i < _showDataList.Count; i++)
                            {
                                data = _showDataList[i];
                                if (JudgeBig(data.scoreC, data.scoreH, data.CON_HOUH))
                                {
                                    if (mostOdd < data.ior_HOUH)
                                    {
                                        mostOdd = data.ior_HOUH;
                                        mostIndex = i;
                                    }
                                }
                            }
                            if (mostIndex != -1 && mostOdd >= GlobalSetting.GetInstance().AutoBetRate)
                            {
                                RB_Whole.Checked = false;
                                RB_Half.Checked = true;
                                ShowBigOrSmallBig(_showDataList[mostIndex], true);
                            }
                            break;
                        case BetType.HalfCapot:
                            for (int i = 0; i < _showDataList.Count; i++)
                            {
                                data = _showDataList[i];
                                switch ((whoWin = JudgeCapot(data.scoreC, data.scoreH)))
                                {
                                    case 0:
                                        if (mostOdd < data.ior_HOUH)
                                        {
                                            mostOdd = data.ior_HOUH;
                                            mostIndex = i;
                                        }
                                        break;
                                }
                            }
                            if (mostIndex != -1 && mostOdd >= GlobalSetting.GetInstance().AutoBetRate)
                            {
                                RB_Whole.Checked = false;
                                RB_Half.Checked = true;
                                switch (whoWin)
                                {
                                    case 0:
                                        ShowCapotNone(_showDataList[mostIndex]);
                                        break;
                                    case 1:
                                        ShowCapotHost(_showDataList[mostIndex]);
                                        break;
                                    case 2:
                                        ShowCapotAway(_showDataList[mostIndex]);
                                        break;
                                }
                            }
                            break;
                        case BetType.HalfConcedePoints:
                            // 暂不支持
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 更新整个界面的显示内容
        /// </summary>
        /// <param name="odd"></param>
        private void UpdateUIData(Dictionary<string, List<XPJOddData>> odd)
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
        /// 判断新分值总和是否为单值
        /// </summary>
        private bool JudgeIsOdd(string scoreC, string scoreH)
        {
            int total = 1;
            total += (String.IsNullOrEmpty(scoreC) ? 0 : Int32.Parse(scoreC));
            total += (String.IsNullOrEmpty(scoreH) ? 0 : Int32.Parse(scoreH));
            return (total % 2) == 1;
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

        private void UpdateGDVData(List<XPJOddData> _showDataList)
        {
            DGV_Info.Rows.Clear();
            if (_showDataList != null)
            {
                DataGridViewRow dr;
                string tmpS;
                foreach (XPJOddData d in _showDataList)
                {
                    dr = new DataGridViewRow();
                    dr.CreateCells(DGV_Info);
                    if (String.IsNullOrEmpty(d.retimeset))
                    {
                        dr.Cells[0].Value = TimeUtil.FormatTime(d.openTime, "MM/dd HH:mm");
                    }
                    else
                    {
                        if (d.retimeset.StartsWith("1H"))
                        {
                            dr.Cells[0].Value = String.Format("上半场 {0}'", d.retimeset.Substring(3));
                        }
                        else if (d.retimeset.StartsWith("2H"))
                        {
                            dr.Cells[0].Value = String.Format("下半场 {0}'", d.retimeset.Substring(3));
                        }
                        else
                        {
                            dr.Cells[0].Value = "半场休息";
                        }
                    }
                    if (!String.IsNullOrEmpty(d.scoreC) && !String.IsNullOrEmpty(d.scoreH))
                        dr.Cells[1].Value = String.Format("{0} : {1}", d.scoreH, d.scoreC);
                    else
                        dr.Cells[1].Value = "0 : 0";

                    dr.Cells[2].Value = d.league;
                    dr.Cells[3].Value = d.home;
                    dr.Cells[4].Value = d.guest;
                    if (RB_Whole.Checked)
                    {
                        // 全场
                        if (d.ior_EOO != 0 && d.ior_EOE != 0)
                            dr.Cells[5].Value = String.Format("【单{0:N2}】\n【双{1:N2}】", d.ior_EOO, d.ior_EOE);

                        if (d.ior_OUH != 0 && d.ior_OUC != 0)
                            dr.Cells[6].Value = String.Format("【{0}】\n【大{1:N2}】\n【小{2:N2}】", d.CON_OUH, d.ior_OUH, d.ior_OUC);
                      
                        if (d.ior_MC != 0 && d.ior_MN != 0 && d.ior_MH != 0)
                            dr.Cells[7].Value = String.Format("【主{0}】\n【客{1:N2}】\n【和{2:N2}】", d.ior_MH, d.ior_MC, d.ior_MN);

                        if (d.ior_RC != 0 && d.ior_RH != 0)
                        {
                            if (d.CON_RH.StartsWith("-"))
                            {
                                tmpS = d.CON_RH.Substring(1);
                                dr.Cells[8].Value = String.Format("【客让{0}】\n【大{1:N2}】\n【小{2:N2}】", tmpS, d.ior_RC, d.ior_RH);
                            }
                            else
                            {
                                dr.Cells[8].Value = String.Format("【主让{0}】\n【大{1:N2}】\n【小{2:N2}】", d.CON_RH, d.ior_RH, d.ior_RC);
                            }
                        }

                    }
                    else
                    {
                        if (d.ior_HOUH != 0 && d.ior_HOUC != 0)
                            dr.Cells[6].Value = String.Format("【{0}】\n【大{1:N2}】\n【小{2:N2}】", d.CON_HOUH, d.ior_HOUH, d.ior_HOUC);
                        
                        if (d.ior_HMC != 0 && d.ior_HMN != 0 && d.ior_HMN != 0)
                            dr.Cells[7].Value = String.Format("【主{0:N2}】\n【客{1:N2}】\n【和{2:N2}】", d.ior_HMH, d.ior_HMC, d.ior_HMN);

                        if (d.ior_HRC != 0 && d.ior_HRH != 0)
                        {
                            if (d.CON_HRH.StartsWith("-"))
                            {
                                tmpS = d.CON_HRH.Substring(1);
                                dr.Cells[8].Value = String.Format("【客让{0}】\n【大{1:N2}】\n【小{2:N2}】", tmpS, d.ior_HRC, d.ior_HRH);
                            }
                            else
                                dr.Cells[8].Value = String.Format("【主让{0}】\n【大{1:N2}】\n【小{2:N2}】", d.CON_HRH, d.ior_HRH, d.ior_HRC);
                        }
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

        private List<XPJOddData> GetShowDataDefault()
        {
            return GetShowDataBySource(_searchText, _selectLeagueVal, _oddDataDict, _allSourceData);
        }

        private List<XPJOddData> GetShowDataBySource(string searchText, string leagueVal, Dictionary<string, List<XPJOddData>> odd, List<XPJOddData> source)
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
            _client.GetAllOddData(_gameType,
                    _sortType,
                    (ret) =>
                    {
                        Dictionary<string, List<XPJOddData>> dict = XPJDataParser.TransformRespDataToXPJOddDataDict(ret, out _allSourceData);
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

        private void ShowOdd(XPJOddData oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            dialog.Update(oddData, BTN_Odd.Text, "ior_EOO", oddData.ior_EOO, "");
        }

        private void BTN_Odd_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Odd.Enabled = false;
            ShowOdd(_showDataList[DGV_Info.SelectedRows[0].Index]);
            BTN_Odd.Enabled = true;

        }

        private void ShowEven(XPJOddData oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            dialog.Update(oddData, BTN_Even.Text, "ior_EOE", oddData.ior_EOE, "");
        }

        private void BTN_Even_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Even.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            ShowEven(oddData);

            BTN_Even.Enabled = true;
        }

        private void BTN_BigOrSmall_Host_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_BigOrSmall_Host.Enabled = false;
            ShowBigOrSmallBig(_showDataList[DGV_Info.SelectedRows[0].Index], false);
            BTN_BigOrSmall_Host.Enabled = true;
        }

        private void ShowBigOrSmallBig(XPJOddData oddData, bool isAuto)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_BigOrSmall_Host.Text, "ior_OUH", oddData.ior_OUH, oddData.CON_OUH, isAuto);
            }
            else
            {
                dialog.Update(oddData, BTN_BigOrSmall_Host.Text, "ior_HOUH", oddData.ior_HOUH, oddData.CON_HOUH, isAuto);
            }
        }

        private void ShowBigOrSmallSmall(XPJOddData oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_BigOrSmall_Away.Text, "ior_OUC", oddData.ior_OUC, oddData.CON_OUH);
            }
            else
            {
                dialog.Update(oddData, BTN_BigOrSmall_Away.Text, "ior_HOUC", oddData.ior_HOUC, oddData.CON_HOUH);
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
            ShowCapotHost(_showDataList[DGV_Info.SelectedRows[0].Index]);

            BTN_Capot_Host.Enabled = true;
        }

        private void ShowCapotNone(XPJOddData oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_Capot_None.Text, "ior_MN", oddData.ior_MN, "");
            }
            else
            {
                dialog.Update(oddData, BTN_Capot_None.Text, "ior_HMN", oddData.ior_HMN, "");
            }
        }

        private void BTN_Capot_None_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Capot_None.Enabled = false;
            ShowCapotNone(_showDataList[DGV_Info.SelectedRows[0].Index]);

            BTN_Capot_None.Enabled = true;
        }

        private void ShowCapotAway(XPJOddData oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_Capot_Away.Text, "ior_MC", oddData.ior_MC, "");
            }
            else
            {
                dialog.Update(oddData, BTN_Capot_Away.Text, "ior_HMC", oddData.ior_HMC, "");
            }
        }

        private void BTN_Capot_Away_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Capot_Away.Enabled = false;
            ShowCapotAway(_showDataList[DGV_Info.SelectedRows[0].Index]);

            BTN_Capot_Away.Enabled = true;
        }

        private void ShowConcedePointsHost(XPJOddData oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_ConcedePoints_Host.Text, "ior_RH", oddData.ior_RH, oddData.CON_RH);
            }
            else
            {
                dialog.Update(oddData, BTN_ConcedePoints_Host.Text, "ior_HRH", oddData.ior_HRH, oddData.CON_RH);
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

        private void ShowConcedePointsAway(XPJOddData oddData)
        {
            DialogConfirm dialog = DialogConfirm.newInstance();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_ConcedePoints_Away.Text, "ior_RC", oddData.ior_RC, oddData.CON_RH);
            }
            else
            {
                dialog.Update(oddData, BTN_ConcedePoints_Away.Text, "ior_HRC", oddData.ior_HRC, oddData.CON_RH);
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
                XPJOddData data = _showDataList[DGV_Info.SelectedRows[0].Index];
                if (RB_Whole.Checked)
                {
                    if (data.ior_EOE != 0 && data.ior_EOO != 0)
                    {
                        BTN_Even.Enabled = true;
                        BTN_Odd.Enabled = true;
                        BTN_Even.Text = String.Format("【全场-单双】\n【双】\n【赔率：{0:N2}】", data.ior_EOE);
                        BTN_Odd.Text = String.Format("【全场-单双】\n【单】\n【赔率：{0:N2}】", data.ior_EOO);
                    }
                    if (data.ior_MC != 0 && data.ior_MH != 0 && data.ior_MN != 0)
                    {
                        BTN_Capot_Away.Enabled = true;
                        BTN_Capot_Host.Enabled = true;
                        BTN_Capot_None.Enabled = true;
                        BTN_Capot_Host.Text = String.Format("【全场-独赢】\n【主赢】\n【赔率：{0:N2}】", data.ior_MH);
                        BTN_Capot_Away.Text = String.Format("【全场-独赢】\n【客赢】\n【赔率：{0:N2}】", data.ior_MN);
                        BTN_Capot_None.Text = String.Format("【全场-独赢】\n【和局】\n【赔率：{0:N2}】", data.ior_MC);
                    }
                    if (data.ior_OUC != 0 && data.ior_OUH != 0)
                    {
                        BTN_BigOrSmall_Away.Enabled = true;
                        BTN_BigOrSmall_Host.Enabled = true;
                        BTN_BigOrSmall_Host.Text = String.Format("【全场-大小】\n【大{0}】\n【赔率：{1:N2}】", data.CON_OUH, data.ior_OUH);
                        BTN_BigOrSmall_Away.Text = String.Format("【全场-大小】\n【小{0}】\n【赔率：{1:N2}】", data.CON_OUH, data.ior_OUC);
                    }
                    if (data.ior_RC != 0 && data.ior_RH != 0)
                    {
                        BTN_ConcedePoints_Host.Enabled = true;
                        BTN_ConcedePoints_Away.Enabled = true;
                        if (data.CON_RH.StartsWith("-"))
                        {
                            String tmpS = data.CON_RH.Substring(1);
                            BTN_ConcedePoints_Host.Text = String.Format("【全场-让球】\n【客让{0}】\n【主】【赔率：{1:N2}】", tmpS, data.ior_RH);
                            BTN_ConcedePoints_Away.Text = String.Format("【全场-让球】\n【客让{0}】\n【客】【赔率：{1:N2}】", tmpS, data.ior_RC);
                        }
                        else
                        {
                            BTN_ConcedePoints_Host.Text = String.Format("【全场-让球】\n【主让{0}】\n【主】【赔率：{1:N2}】", data.CON_RH, data.ior_RH);
                            BTN_ConcedePoints_Away.Text = String.Format("【全场-让球】\n【主让{0}】\n【客】【赔率：{1:N2}】", data.CON_RH, data.ior_RC);
                        }
                    }
                }
                else
                {
                    if (data.ior_HMC != 0 && data.ior_HMH != 0 && data.ior_HMN != 0)
                    {
                        BTN_Capot_Away.Enabled = true;
                        BTN_Capot_Host.Enabled = true;
                        BTN_Capot_None.Enabled = true;
                        BTN_Capot_Host.Text = String.Format("【半场-独赢】\n【主赢】\n【赔率：{0:N2}】", data.ior_HMH);
                        BTN_Capot_Away.Text = String.Format("【半场-独赢】\n【客赢】\n【赔率：{0:N2}】", data.ior_HMN);
                        BTN_Capot_None.Text = String.Format("【半场-独赢】\n【和局】\n【赔率：{0:N2}】", data.ior_HMC);
                    }
                    if (data.ior_HOUC != 0 && data.ior_HOUH != 0)
                    {
                        BTN_BigOrSmall_Away.Enabled = true;
                        BTN_BigOrSmall_Host.Enabled = true;
                        BTN_BigOrSmall_Host.Text = String.Format("【半场-大小】\n【大{0}】\n【赔率：{1:N2}】", data.CON_HOUH, data.ior_HOUH);
                        BTN_BigOrSmall_Away.Text = String.Format("【半场-大小】\n【小{0}】\n【赔率：{1:N2}】", data.CON_HOUH, data.ior_HOUC);
                    }
                    if (data.ior_HRC != 0 && data.ior_HRH != 0)
                    {
                        BTN_ConcedePoints_Host.Enabled = true;
                        BTN_ConcedePoints_Away.Enabled = true;
                        if (data.CON_HRH.StartsWith("-"))
                        {
                            String tmpS = data.CON_HRH.Substring(1);
                            BTN_ConcedePoints_Host.Text = String.Format("【半场-让球】\n【客让{0}】\n【主】【赔率：{1:N2}】", tmpS, data.ior_HRH);
                            BTN_ConcedePoints_Away.Text = String.Format("【半场-让球】\n【客让{0}】\n【客】【赔率：{1:N2}】", tmpS, data.ior_HRC);
                        }
                        else
                        {
                            BTN_ConcedePoints_Host.Text = String.Format("【半场-让球】\n【主让{0}】\n【主】【赔率：{1:N2}】", data.CON_HRH, data.ior_HRH);
                            BTN_ConcedePoints_Away.Text = String.Format("【半场-让球】\n【主让{0}】\n【客】【赔率：{1:N2}】", data.CON_HRH, data.ior_HRC);
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
            BTN_Even.Text = "无";
            BTN_Odd.Text = "无";

            BTN_BigOrSmall_Away.Enabled = enabled;
            BTN_BigOrSmall_Host.Enabled = enabled;
            BTN_Capot_Away.Enabled = enabled;
            BTN_Capot_Host.Enabled = enabled;
            BTN_Capot_None.Enabled = enabled;
            BTN_ConcedePoints_Away.Enabled = enabled;
            BTN_ConcedePoints_Host.Enabled = enabled;
            BTN_Even.Enabled = enabled;
            BTN_Odd.Enabled = enabled;
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

        private void CB_GameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(CB_GameType.SelectedIndex)
            {
                case 0:
                    _gameType = XPJClient.GameType.FT_RB_MN;
                    break;
                case 1:
                    _gameType = XPJClient.GameType.FT_TD_MN;
                    break;
                case 2:
                    _gameType = XPJClient.GameType.FT_FT_MN;
                    break;
            }
            BTN_Refresh_Click(sender, e);
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
