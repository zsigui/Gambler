using Gambler.Config;
using Gambler.Model;
using Gambler.Model.XPJ;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler.UI
{
    public partial class DialogConfirm : Form
    {
        private static DialogConfirm sInstance;

        public static DialogConfirm newInstance()
        {
            if (sInstance == null)
            {
                sInstance = new DialogConfirm();
            }
            else
            {
                sInstance.Focus();
            }
            return sInstance;
        }

        private List<XPJAccount> _accounts;
        private List<float> _moneys;
        private ReqBetData _requestData;

        public DialogConfirm()
        {
            InitializeComponent();
        }

        public void Update(XPJOddData oddData, string oddInfo, string type, float odd, string project)
        {
            Update(oddData, oddInfo, type, odd, project, false);
        }

        public void Update(XPJOddData oddData, string oddInfo, string type, float odd, string project, bool isAuto)
        {
            LB_League.Text = oddData.league;
            LB_Match.Text = String.Format("{0}（主） v.s.{1}（客）", oddData.home, oddData.guest);
            LB_Score.Text = String.Format("{0} : {1}", oddData.scoreH, oddData.scoreC);
            if (oddData.retimeset.StartsWith("1H"))
                LB_Time.Text = String.Format("上半场 {0}'", oddData.retimeset.Substring(3));
            else if (oddData.retimeset.StartsWith("2H"))
                LB_Time.Text = String.Format("上半场 {0}'", oddData.retimeset.Substring(3));
            else
                LB_Time.Text = "半场休息";
            LB_Odd.Text = oddInfo;
            DataGridViewRow dr;
            int money;
            DGV_BetUser.Rows.Clear();
            _accounts = new List<XPJAccount>();
            _moneys = new List<float>();
            BTN_Confirm.Enabled = true;
            foreach (XPJAccount account in FormMain.GetInstance().ObtainAccounts())
            {
                money = account.IsChecked ? 0 : CalculateBetMoney((int)account.Money);
                _accounts.Add(account);
                _moneys.Add(money);
                dr = new DataGridViewRow();
                dr.CreateCells(DGV_BetUser);
                dr.Cells[0].Value = account.Account;
                dr.Cells[1].Value = String.Format("{0:N2}", money);
                DGV_BetUser.Rows.Add(dr);
            }
           
            CreateRequestBetData(oddData, type, odd, project);
            if (isAuto)
            {
                DoRequest(true);
            }
        }

        private void CreateRequestBetData(XPJOddData oddData, string type, float odd, string project)
        {
            ReqBetItem item = new ReqBetItem();
            item.gid = oddData.gid;
            item.odds = odd.ToString();
            item.project = project;
            item.scoreH = oddData.scoreH;
            item.scoreC = oddData.scoreC;
            item.type = type;
            ReqBetData data = new ReqBetData();
            data.acceptBestOdds = GlobalSetting.GetInstance().IsAutoAcceptBestOdd;
            data.items = new List<ReqBetItem>() { item };
            data.gameType = XPJClient.GameType.FT_RB_MN;
            _requestData = data;
        }

        private int CalculateBetMoney(int money)
        {
            int most = GlobalSetting.GetInstance().MostBetMoney;
            int least = GlobalSetting.GetInstance().LeastBetMoney;
            most = Math.Min(most, money);
            least = Math.Min(most, least);
            if (most == least)
                return most;
            return new Random().Next(least, most);
        }

        private void BTN_Confirm_Click(object sender, EventArgs e)
        {
            DoRequest(false);
        }

        private void DoRequest(bool isAuto)
        {
            if (_accounts == null)
            {
                MessageBox.Show("获取不到用户数据，请重新进入");
                return;
            }
            SetBtnEnabled(false);
            ThreadUtil.RunOnThread(() =>
            {
                XPJAccount tmpAccount;
                for (int i = 0; i < _accounts.Count; i++)
                {
                    tmpAccount = _accounts[i];
                    if (_moneys[i] <= 0)
                    {
                        UpdateDGVCellOnThread(i, "-");
                        continue;
                    }
                    _requestData.money = String.Format("{0:N2}", _moneys[i]);
                    tmpAccount.GetClient().DoBet(_requestData,
                            (ret) =>
                            {
                                UpdateDGVCellOnThread(i, "√");
                            },
                            (statusCode, err, errMsg) =>
                            {
                                LogUtil.Write(String.Format("Http状态值：{0}，错误码：{1}，错误信息：{2}", statusCode, err, errMsg));
                                UpdateDGVCellOnThread(i, "×," + errMsg);
                            },
                            (err) =>
                            {
                                LogUtil.Write(err);
                                UpdateDGVCellOnThread(i, "×," + "执行出错");
                            });
                }
                if (isAuto && !GlobalSetting.GetInstance().IsShowBetDialog)
                {
                    Invoke(new Action(()=> { Hide(); }));
                }
                else
                {
                    MessageBox.Show("投注操作完成!");
                }

            });
        }

        private void UpdateDGVCellOnThread(int rowIndex, string val)
        {
            Invoke(new Action<int, string>((i, v) => {
                if (DGV_BetUser.Rows.Count > i)
                    DGV_BetUser.Rows[i].Cells[2].Value = v;
            }), rowIndex, val);
        }

        private void SetBtnEnabled(bool enabled)
        {
            ThreadUtil.WorkOnUI(this,
                new Action<bool>((e)=> {
                    BTN_Confirm.Enabled = e;
                }),
                enabled);
        }

        private void DGV_BetUser_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                _moneys.RemoveAt(e.RowIndex);
                _accounts.RemoveAt(e.RowIndex);
            }
            if (DGV_BetUser.Rows.Count <= 0)
                BTN_Confirm.Enabled = false;
        }

        private void DGV_BetUser_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex != -1)
            {
                object newVal = DGV_BetUser.Rows[e.RowIndex].Cells[1].Value;
                if (newVal != null && !String.IsNullOrEmpty(newVal.ToString()))
                {
                    float n = float.Parse(newVal.ToString());
                    float acountMoney = _accounts[e.RowIndex].Money;
                    if (acountMoney > 0 &&  n > acountMoney)
                    {
                        _moneys[e.RowIndex] = acountMoney;
                    }
                    else
                    {
                        _moneys[e.RowIndex] = n;
                    }
                    DGV_BetUser.Rows[e.RowIndex].Cells[1].Value = String.Format("{0:N2}", _moneys[e.RowIndex]);
                }
            }
        }

        private void DialogConfirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            sInstance = null;
        }
    }
}

