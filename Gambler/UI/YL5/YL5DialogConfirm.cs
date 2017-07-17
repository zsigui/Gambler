using Gambler.Config;
using Gambler.Model;
using Gambler.Model.XPJ;
using Gambler.Module.X469;
using Gambler.Module.X469.Model;
using Gambler.Module.XPJ.Model;
using Gambler.Utils;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gambler.UI
{
    public partial class YL5DialogConfirm : Form
    {
        private static YL5DialogConfirm sInstance;

        public static YL5DialogConfirm newInstance()
        {
            if (sInstance == null)
            {
                sInstance = new YL5DialogConfirm();
            }
            else
            {
                sInstance.Focus();
            }
            return sInstance;
        }

        private List<IntegratedAccount> _accounts;
        private List<double> _moneys;
        private X469ReqBetData _requestData;

        public YL5DialogConfirm()
        {
            InitializeComponent();
        }

        public void Update(X469OddItem oddData, string oddInfo, string ltype, string bet, bool autoOpt, string odd)
        {
            Update(oddData, oddInfo, ltype, bet, autoOpt, odd, false);
        }

        public void Update(X469OddItem oddData, string oddInfo, string ltype, string bet, bool autoOpt, string odd, bool isAuto)
        {
            LB_League.Text = oddData.a26;
            LB_Match.Text = String.Format("{0}（主） v.s.{1}（客）", oddData.a2, oddData.a3);
            if (!String.IsNullOrEmpty(oddData.a16) && !String.IsNullOrEmpty(oddData.a17))
                LB_Score.Text = String.Format("{0} : {1}", oddData.a16, oddData.a17);
            else
                LB_Score.Text = "0 : 0";

            LB_Time.Text = String.Format("{0}分钟", oddData.a19);

            LB_Odd.Text = oddInfo;
            DataGridViewRow dr;
            int money;
            DGV_BetUser.Rows.Clear();
            _accounts = new List<IntegratedAccount>();
            _moneys = new List<double>();
            BTN_Confirm.Enabled = true;
            foreach (IntegratedAccount account in FormMain.GetInstance().ObtainAccounts(-1))
            {
                if (account.Type != AcccountType.YL5789)
                    continue;

                money = account.IsChecked ? 0 : CalculateBetMoney((int)account.Money);
                _accounts.Add(account);
                _moneys.Add(money);
                dr = new DataGridViewRow();
                dr.CreateCells(DGV_BetUser);
                dr.Cells[0].Value = account.Account;
                dr.Cells[1].Value = String.Format("{0:N2}", money);
                DGV_BetUser.Rows.Add(dr);
            }
           
            CreateRequestBetData(oddData, ltype, bet, autoOpt, odd);
            if (isAuto)
            {
                DoRequest(true);
            }
        }

        private void CreateRequestBetData(X469OddItem oddData, string ltype, string bet, bool auto, string odd)
        {
            _requestData = new X469ReqBetData();
            _requestData.autoOpt = true;
            _requestData.bet = bet;
            _requestData.mid = oddData.mid;
            _requestData.ltype = ltype;
            _requestData.autoOpt = auto;
            _requestData.rate = odd;
        }

        private int CalculateBetMoney(int money)
        {
            if (GlobalSetting.GetInstance().BetMoney > money)
                return money;
            return GlobalSetting.GetInstance().BetMoney;
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
                IntegratedAccount tmpAccount;
                for (int i = 0; i < _accounts.Count; i++)
                {
                    tmpAccount = _accounts[i];
                    if (_moneys[i] <= 0)
                    {
                        UpdateDGVCellOnThread(i, "-");
                        continue;
                    }
                    _requestData.money = _moneys[i];
                    tmpAccount.GetClient<YL5Client>().DoBet(_requestData,
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
                    double acountMoney = _accounts[e.RowIndex].Money;
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

