using Gambler.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler.UI
{
    public partial class FormAutoBetSetting : Form
    {
        // 1千万
        private readonly int MIN_MONEY = 50;
        private readonly int MOST_MONEY = 10000000;
        private string _leastContent;
        private string _mostContent;

        public FormAutoBetSetting()
        {
            InitializeComponent();

            InitVal();
        }

        private void InitVal()
        {
            CB_Type.SelectedIndex = (int)GlobalSetting.GetInstance().GameBetType - 1;
            if (GlobalSetting.GetInstance().GameBetMethod == BetMethod.EveryTime)
            {
                RB_EveryTime.Checked = true;
            }
            else
            {
                RB_Random.Checked = true;
            }
            TB_LeastMoney.Text = GlobalSetting.GetInstance().LeastBetMoney.ToString();
            TB_MostMoney.Text = GlobalSetting.GetInstance().MostBetMoney.ToString();
            TB_Rate.Text = GlobalSetting.GetInstance().AutoBetRate.ToString();
        }

        private void BTN_Confirm_Click(object sender, EventArgs e)
        {
            SaveConfig();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveConfig()
        {
            GlobalSetting gs = GlobalSetting.GetInstance();
            gs.LeastBetMoney = (String.IsNullOrEmpty(_leastContent) ? MIN_MONEY : Int32.Parse(_leastContent));
            gs.MostBetMoney = (String.IsNullOrEmpty(_mostContent) ? MOST_MONEY : Int32.Parse(_mostContent));
            gs.GameBetMethod = (RB_EveryTime.Checked ? BetMethod.EveryTime : BetMethod.Random);
            gs.GameBetType = (CB_Type.SelectedIndex == -1 ? BetType.BigOrSmall : (BetType)(CB_Type.SelectedIndex + 1));
            float rate = gs.AutoBetRate;
            if (!String.IsNullOrEmpty(TB_Rate.Text))
            {
                float.TryParse(TB_Rate.Text, out rate);
            }
            gs.AutoBetRate = rate;
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TB_MostMoney_TextChanged(object sender, EventArgs e)
        {
            Match m = Regex.Match(TB_MostMoney.Text, @"^\d*$");
            if (!m.Success)
            {
                TB_MostMoney.Text = _leastContent;
                TB_MostMoney.SelectionStart = TB_MostMoney.Text.Length;
            }
            else
            {
                _leastContent = TB_MostMoney.Text;
            }
        }

        private void TB_LeastMoney_TextChanged(object sender, EventArgs e)
        {
            Match m = Regex.Match(TB_LeastMoney.Text, @"^\d*$");
            if (!m.Success)
            {
                TB_LeastMoney.Text = _mostContent;
                TB_LeastMoney.SelectionStart = TB_LeastMoney.Text.Length;
            }
            else
            {
                _mostContent = TB_LeastMoney.Text;
            }
        }
    }
}
