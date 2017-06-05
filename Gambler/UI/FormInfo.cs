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

        public static FormInfo sInstance;

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
        private List<XPJOddData> _showDataList;

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
                RefreshOrInitTimer(true);
            }
        }

        private void RefreshOrInitTimer(bool forceReset)
        {

            if (forceReset)
                _refreshCountdown = 0;

            if (_refreshTimer != null)
            {
                _refreshTimer.Change(0, 1000);
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
            _showDataList = GetShowDataBySource(odd);
            ThreadUtil.WorkOnUI<object>(this,
                new Action(() =>{
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
                                dr.Cells[0].Value = String.Format("上半场{1} / {2}", d.retimeset.Substring(2), d.score);
                            }
                            else
                            {
                                dr.Cells[0].Value = String.Format("下半场{1} / {2}", d.retimeset.Substring(2), d.score);
                            }
                            dr.Cells[1].Value = d.home;
                            dr.Cells[2].Value = d.guest;
                            dr.Cells[3].Value = d.league;
                            if (RB_Whole.Checked)
                            {
                                // 全场
                                dr.Cells[4].Value = String.Format("{1}\n【大{2}】\n【小{3}】", d.CON_OUH, d.ior_OUH, d.ior_OUC);
                                if (d.CON_RH.StartsWith("-"))
                                {
                                    tmpS = d.CON_RH.Substring(1);
                                    dr.Cells[5].Value = String.Format("【客让{1}】\n【大{2}】\n【小{3}】", d.CON_RH, d.ior_RC, d.ior_RH);
                                }
                                else
                                    dr.Cells[5].Value = String.Format("【主让{1}】\n【大{2}】\n【小{3}】", d.CON_RH, d.ior_RH, d.ior_RC);
                                dr.Cells[6].Value = String.Format("【主{1}】\n【客{2}】\n【和{3}】", d.ior_MH, d.ior_MC, d.ior_MN);
                            }
                            else
                            {
                                dr.Cells[4].Value = String.Format("{1}\n【大{2}】\n【小{3}】", d.CON_HOUH, d.ior_HOUH, d.ior_HOUC);
                                if (d.CON_RH.StartsWith("-"))
                                {
                                    tmpS = d.CON_RH.Substring(1);
                                    dr.Cells[5].Value = String.Format("【客让{1}】\n【大{2}】\n【小{3}】", d.CON_HRH, d.ior_HRC, d.ior_HRH);
                                }
                                else
                                    dr.Cells[5].Value = String.Format("【主让{1}】\n【大{2}】\n【小{3}】", d.CON_HRH, d.ior_HRH, d.ior_HRC);
                                dr.Cells[6].Value = String.Format("【主{1}】\n【客{2}】\n【和{3}】", d.ior_HMH, d.ior_HMC, d.ior_HMN);
                            }
                            dr.Cells[7].Value = String.Format("【单{1}】\n【双{2}】", d.ior_EOO, d.ior_EOE);
                            DGV_Info.Rows.Add(dr);
                        }
                    }
                }));
        }

        private List<XPJOddData> GetShowDataBySource(Dictionary<string, List<XPJOddData>> odd)
        {
            List<XPJOddData> listData = new List<XPJOddData>();
            if (CB_Leagues.SelectedIndex == -1 && String.IsNullOrEmpty(TB_Search.Text))
            {
                foreach (List<XPJOddData> list in odd.Values)
                {
                    foreach (XPJOddData d in list)
                    {
                        listData.Add(d);
                    }
                }
            }
            
            return listData;
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
                    (ret) =>
                    {
                        Dictionary<string, List<XPJOddData>> dict = XPJDataParser.TransformRespDataToXPJOddDataDict(ret);
                        UpdateUIData(dict);
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

        }

        private void BTN_Even_Click(object sender, EventArgs e)
        {

        }

        private void BTN_BigOrSmall_Host_Click(object sender, EventArgs e)
        {

        }

        private void BTN_BigOrSmall_Away_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Capot_Host_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Capot_None_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Capot_Away_Click(object sender, EventArgs e)
        {

        }

        private void BTN_ConcedePoints_Host_Click(object sender, EventArgs e)
        {

        }

        private void BTN_ConcedePoints_Away_Click(object sender, EventArgs e)
        {

        }

        private void FormInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            sInstance = null;
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Whole.Checked)
            {

            }
            else
            {

            }
        }
    }
}
