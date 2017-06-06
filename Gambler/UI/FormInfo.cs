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

        public static FormInfo newInstance()
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

        /// <summary>
        /// 1 按时间排序 2 按联盟排序
        /// </summary>
        private int _sortType = 1;
        private string _selectLeagueVal = null;
        private string _searchText = "";

        /// <summary>
        /// 初始化配置
        /// </summary>
        private void InitOnStart()
        {
            if (!FormMain.GetInstance().HasXPJAccount())
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

        /// <summary>
        /// 更新整个界面的显示内容
        /// </summary>
        /// <param name="odd"></param>
        private void UpdateUIData(Dictionary<string, List<XPJOddData>> odd)
        {
            _oddDataDict = odd;
            ThreadUtil.WorkOnUI<object>(this,
                new Action(() =>{
                    UpdateLeague(_oddDataDict.Keys);
                    _showDataList = GetShowDataBySource(_oddDataDict, _allSourceData);
                    UpdateGDVData(_showDataList);
                }));
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
                    dr.Cells[1].Value = d.score;
                    dr.Cells[2].Value = d.home;
                    dr.Cells[3].Value = d.guest;
                    dr.Cells[4].Value = d.league;
                    if (RB_Whole.Checked)
                    {
                        // 全场
                        if (d.ior_OUH != 0 && d.ior_OUC != 0)
                            dr.Cells[5].Value = String.Format("【{0}】\n【大{1:N2}】\n【小{2:N2}】", d.CON_OUH, d.ior_OUH, d.ior_OUC);
                        if (d.ior_RC != 0 && d.ior_RH != 0)
                        {
                            if (d.CON_RH.StartsWith("-"))
                            {
                                tmpS = d.CON_RH.Substring(1);
                                dr.Cells[6].Value = String.Format("【客让{0}】\n【大{1:N2}】\n【小{2:N2}】", tmpS, d.ior_RC, d.ior_RH);
                            }
                            else
                            {
                                dr.Cells[6].Value = String.Format("【主让{0}】\n【大{1:N2}】\n【小{2:N2}】", d.CON_RH, d.ior_RH, d.ior_RC);
                            }
                        }
                        if (d.ior_MC != 0 && d.ior_MN != 0 && d.ior_MH != 0)
                            dr.Cells[7].Value = String.Format("【主{0}】\n【客{1:N2}】\n【和{2:N2}】", d.ior_MH, d.ior_MC, d.ior_MN);

                        if (d.ior_EOO != 0 && d.ior_EOE != 0)
                            dr.Cells[8].Value = String.Format("【单{0:N2}】\n【双{1:N2}】", d.ior_EOO, d.ior_EOE);
                    }
                    else
                    {
                        if (d.ior_HOUH != 0 && d.ior_HOUC != 0)
                            dr.Cells[5].Value = String.Format("【{0}】\n【大{1:N2}】\n【小{2:N2}】", d.CON_HOUH, d.ior_HOUH, d.ior_HOUC);
                        if (d.ior_HRC != 0 && d.ior_HRH != 0)
                        {
                            if (d.CON_HRH.StartsWith("-"))
                            {
                                tmpS = d.CON_HRH.Substring(1);
                                dr.Cells[6].Value = String.Format("【客让{0}】\n【大{1:N2}】\n【小{2:N2}】", tmpS, d.ior_HRC, d.ior_HRH);
                            }
                            else
                                dr.Cells[6].Value = String.Format("【主让{0}】\n【大{1:N2}】\n【小{2:N2}】", d.CON_HRH, d.ior_HRH, d.ior_HRC);
                        }
                        if (d.ior_HMC != 0 && d.ior_HMN != 0 && d.ior_HMN != 0)
                            dr.Cells[7].Value = String.Format("【主{0:N2}】\n【客{1:N2}】\n【和{2:N2}】", d.ior_HMH, d.ior_HMC, d.ior_HMN);
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

        private List<XPJOddData> GetShowDataBySource(Dictionary<string, List<XPJOddData>> odd, List<XPJOddData> source)
        {
            return FilterBySearch(FilterByLeague(odd, source));
        }

        private List<XPJOddData> FilterByLeague(Dictionary<string, List<XPJOddData>> odd, List<XPJOddData> source)
        {
            List<XPJOddData> listData = new List<XPJOddData>();
            if (!String.IsNullOrEmpty(_selectLeagueVal) && odd.ContainsKey(_selectLeagueVal))
            {
                foreach (XPJOddData d in odd[_selectLeagueVal])
                {
                    listData.Add(d);
                }
            }
            else
            {
                foreach (XPJOddData d in source)
                {
                    listData.Add(d);
                }
            }
            return listData;
        }

        private List<XPJOddData> FilterBySearch(List<XPJOddData> source)
        {
            List<XPJOddData> listData = new List<XPJOddData>();
            if (!String.IsNullOrEmpty(_searchText))
            {
                string[] keys = _searchText.Split(' ');
                foreach (XPJOddData d in source)
                {
                    if (FindContains(d, keys))
                    {
                        listData.Add(d);
                    }
                }
            }
            else
            {
                foreach (XPJOddData d in source)
                {
                    listData.Add(d);
                }
            }
            return listData;
        }

        private bool FindContains(XPJOddData d, string[] keys)
        {
            foreach (string k in keys)
            {
                if (!String.IsNullOrEmpty(k)
                    && (d.league.Contains(k)
                    || d.home.Contains(k)
                    || d.guest.Contains(k)))
                    return true;
            }
            return false;
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
            FormMain.GetInstance().ObtainAccounts().First().GetClient()
                    .GetOddData(XPJClient.GameType.FT_RB_MN,
                    1,
                    _sortType,
                    null,
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
                    _refreshTimer.Change(Timeout.Infinite, 1000);
                    RefreshBtn(0);
                    break;
                case 1:
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

        private void BTN_Odd_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Odd.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            dialog.Update(oddData, BTN_Odd.Text, "ior_EOO", oddData.ior_EOO, "");
            BTN_Odd.Enabled = true;

        }

        private void BTN_Even_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Even.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            dialog.Update(oddData, BTN_Even.Text, "ior_EOE", oddData.ior_EOE, "");
            BTN_Even.Enabled = true;
        }

        private void BTN_BigOrSmall_Host_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_BigOrSmall_Host.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_BigOrSmall_Host.Text, "ior_OUH", oddData.ior_OUH, oddData.CON_OUH);
            }
            else
            {
                dialog.Update(oddData, BTN_BigOrSmall_Host.Text, "ior_HOUH", oddData.ior_HOUH, oddData.CON_HOUH);
            }
            BTN_BigOrSmall_Host.Enabled = true;
        }

        private void BTN_BigOrSmall_Away_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_BigOrSmall_Away.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_BigOrSmall_Away.Text, "ior_OUC", oddData.ior_OUC, oddData.CON_OUH);
            }
            else
            {
                dialog.Update(oddData, BTN_BigOrSmall_Away.Text, "ior_HOUC", oddData.ior_HOUC, oddData.CON_HOUH);
            }
            BTN_BigOrSmall_Away.Enabled = true;
        }

        private void BTN_Capot_Host_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Capot_Host.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_Capot_Host.Text, "ior_MH", oddData.ior_MH, "");
            }
            else
            {
                dialog.Update(oddData, BTN_Capot_Host.Text, "ior_HMH", oddData.ior_HMH, "");
            }
            BTN_Capot_Host.Enabled = true;
        }

        private void BTN_Capot_None_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Capot_None.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_Capot_None.Text, "ior_MN", oddData.ior_MN, "");
            }
            else
            {
                dialog.Update(oddData, BTN_Capot_None.Text, "ior_HMN", oddData.ior_HMN, "");
            }
            BTN_Capot_None.Enabled = true;
        }

        private void BTN_Capot_Away_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_Capot_Away.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_Capot_Away.Text, "ior_MC", oddData.ior_MC, "");
            }
            else
            {
                dialog.Update(oddData, BTN_Capot_Away.Text, "ior_HMC", oddData.ior_HMC, "");
            }
            BTN_Capot_Away.Enabled = true;
        }

        private void BTN_ConcedePoints_Host_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_ConcedePoints_Host.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_ConcedePoints_Host.Text, "ior_RH", oddData.ior_RH, oddData.CON_RH);
            }
            else
            {
                dialog.Update(oddData, BTN_ConcedePoints_Host.Text, "ior_HRH", oddData.ior_HRH, oddData.CON_RH);
            }
            BTN_ConcedePoints_Host.Enabled = true;
        }

        private void BTN_ConcedePoints_Away_Click(object sender, EventArgs e)
        {
            if (DGV_Info.SelectedRows == null || DGV_Info.SelectedRows.Count <= 0)
                return;
            BTN_ConcedePoints_Away.Enabled = false;
            XPJOddData oddData = _showDataList[DGV_Info.SelectedRows[0].Index];
            DialogConfirm dialog = new DialogConfirm();
            dialog.Show();
            if (RB_Whole.Checked)
            {
                dialog.Update(oddData, BTN_ConcedePoints_Away.Text, "ior_RC", oddData.ior_RC, oddData.CON_RH);
            }
            else
            {
                dialog.Update(oddData, BTN_ConcedePoints_Away.Text, "ior_HRC", oddData.ior_HRC, oddData.CON_RH);
            }
            BTN_ConcedePoints_Away.Enabled = true;
        }

        private void FormInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            sInstance = null;
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGDVData(_showDataList);
        }

        private void CB_Leagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectLeagueVal = (CB_Leagues.SelectedIndex <= 0 ? null : CB_Leagues.SelectedItem.ToString());
            _showDataList = GetShowDataBySource(_oddDataDict, _allSourceData);
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
                _showDataList = GetShowDataBySource(_oddDataDict, _allSourceData);
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
                        BTN_Even.Text = String.Format("【单双】\n【双】【赔率：{0:N2}】", data.ior_EOE);
                        BTN_Even.Text = String.Format("【单双】\n【单】【赔率：{0:N2}】", data.ior_EOE);
                    }
                    if (data.ior_MC != 0 && data.ior_MH != 0 && data.ior_MN != 0)
                    {
                        BTN_Capot_Away.Enabled = true;
                        BTN_Capot_Host.Enabled = true;
                        BTN_Capot_None.Enabled = true;
                        BTN_Capot_Host.Text = String.Format("【独赢】\n【主赢】【赔率：{0:N2}】", data.ior_MH);
                        BTN_Capot_Away.Text = String.Format("【独赢】\n【客赢】【赔率：{0:N2}】", data.ior_MN);
                        BTN_Capot_None.Text = String.Format("【独赢】\n【和局】【赔率：{0:N2}】", data.ior_MC);
                    }
                    if (data.ior_OUC != 0 && data.ior_OUH != 0)
                    {
                        BTN_BigOrSmall_Away.Enabled = true;
                        BTN_BigOrSmall_Host.Enabled = true;
                        BTN_BigOrSmall_Host.Text = String.Format("【大小】\n【大{0}】【赔率：{1:N2}】", data.CON_OUH, data.ior_OUH);
                        BTN_BigOrSmall_Away.Text = String.Format("【大小】\n【小{0}】【赔率：{1:N2}】", data.CON_OUH, data.ior_OUC);
                    }
                    if (data.ior_RC != 0 && data.ior_RH != 0)
                    {
                        BTN_ConcedePoints_Host.Enabled = true;
                        BTN_ConcedePoints_Away.Enabled = true;
                        if (data.CON_RH.StartsWith("-"))
                        {
                            String tmpS = data.CON_RH.Substring(1);
                            BTN_ConcedePoints_Host.Text = String.Format("【让球】\n【客让{0}】\n【大】【赔率：{1:N2}】", tmpS, data.ior_RH);
                            BTN_ConcedePoints_Away.Text = String.Format("【让球】\n【客让{0}】\n【小】【赔率：{1:N2}】", tmpS, data.ior_RC);
                        }
                        else
                        {
                            BTN_ConcedePoints_Host.Text = String.Format("【让球】\n【主让{0}】\n【大】【赔率：{1:N2}】", data.CON_RH, data.ior_RH);
                            BTN_ConcedePoints_Away.Text = String.Format("【让球】\n【主让{0}】\n【小】【赔率：{1:N2}】", data.CON_RH, data.ior_RC);
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
                        BTN_Capot_Host.Text = String.Format("【独赢】\n【主赢】【赔率：{0:N2}】", data.ior_HMH);
                        BTN_Capot_Away.Text = String.Format("【独赢】\n【客赢】【赔率：{0:N2}】", data.ior_HMN);
                        BTN_Capot_None.Text = String.Format("【独赢】\n【和局】【赔率：{0:N2}】", data.ior_HMC);
                    }
                    if (data.ior_HOUC != 0 && data.ior_HOUH != 0)
                    {
                        BTN_BigOrSmall_Away.Enabled = true;
                        BTN_BigOrSmall_Host.Enabled = true;
                        BTN_BigOrSmall_Host.Text = String.Format("【大小】\n【大{0}】【赔率：{1:N2}】", data.CON_HOUH, data.ior_HOUH);
                        BTN_BigOrSmall_Away.Text = String.Format("【大小】\n【小{0}】【赔率：{1:N2}】", data.CON_HOUH, data.ior_HOUC);
                    }
                    if (data.ior_HRC != 0 && data.ior_HRH != 0)
                    {
                        BTN_ConcedePoints_Host.Enabled = true;
                        BTN_ConcedePoints_Away.Enabled = true;
                        if (data.CON_HRH.StartsWith("-"))
                        {
                            String tmpS = data.CON_HRH.Substring(1);
                            BTN_ConcedePoints_Host.Text = String.Format("【让球】【客让{0}】\n【大】【赔率：{1:N2}】", tmpS, data.ior_HRH);
                            BTN_ConcedePoints_Away.Text = String.Format("【让球】【客让{0}】\n【小】【赔率：{1:N2}】", tmpS, data.ior_HRC);
                        }
                        else
                        {
                            BTN_ConcedePoints_Host.Text = String.Format("【让球】【主让{0}】\n【大】【赔率：{1:N2}】", data.CON_HRH, data.ior_HRH);
                            BTN_ConcedePoints_Away.Text = String.Format("【让球】【主让{0}】\n【小】【赔率：{1:N2}】", data.CON_HRH, data.ior_HRC);
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
    }
}
