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
        private string _moneyContent;

        public FormAutoBetSetting()
        {
            InitializeComponent();

            InitVal();
        }

        private void InitVal()
        {
            switch(GlobalSetting.GetInstance().GameBetType)
            {
                case BetType.BigOrSmall:
                    CB_Type.SelectedIndex = 0;
                    break;
                case BetType.HalfBigOrSmall:
                    CB_Type.SelectedIndex = 1;
                    break;
                case BetType.HalfConcedePoints:
                    CB_Type.SelectedIndex = 3;
                    break;
                case BetType.ConcedePoints:
                default:
                    CB_Type.SelectedIndex = 2;
                    break;
            }
            switch(GlobalSetting.GetInstance().SecondBetType)
            {
                case BetType.BigOrSmall:
                    CB_SecondType.SelectedIndex = 1;
                    break;
                case BetType.HalfBigOrSmall:
                    CB_SecondType.SelectedIndex = 2;
                    break;
                case BetType.ConcedePoints:
                    CB_SecondType.SelectedIndex = 3;
                    break;
                case BetType.HalfConcedePoints:
                    CB_SecondType.SelectedIndex = 4;
                    break;
                case BetType.None:
                default:
                    CB_SecondType.SelectedIndex = 0;
                    break;
            }
            TB_Money.Text = GlobalSetting.GetInstance().BetMoney.ToString();
            TB_Rate.Text = GlobalSetting.GetInstance().AutoBetRate.ToString();
            CB_Behavior.SelectedIndex = GlobalSetting.GetInstance().BetBehavior;
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
            gs.BetMoney = (String.IsNullOrEmpty(_moneyContent) ? MIN_MONEY : Int32.Parse(_moneyContent));
            switch(CB_Type.SelectedIndex)
            {
                case 0:
                    gs.GameBetType = BetType.BigOrSmall;
                    break;
                case 1:
                    gs.GameBetType = BetType.HalfBigOrSmall;
                    break;
                case 3:
                    gs.GameBetType = BetType.HalfConcedePoints;
                    break;
                case 2:
                default:
                    gs.GameBetType = BetType.ConcedePoints;
                    break;
            }
            switch(CB_SecondType.SelectedIndex)
            {
                case 1:
                    gs.SecondBetType = BetType.BigOrSmall;
                    break;
                case 2:
                    gs.SecondBetType = BetType.HalfBigOrSmall;
                    break;
                case 3:
                    gs.SecondBetType = BetType.ConcedePoints;
                    break;
                case 4:
                    gs.SecondBetType = BetType.HalfConcedePoints;
                    break;
                case 0:
                default:
                    gs.SecondBetType = BetType.None;
                    break;
            }
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

        private void TB_Money_TextChanged(object sender, EventArgs e)
        {
            Match m = Regex.Match(TB_Money.Text, @"^\d*$");
            if (!m.Success)
            {
                TB_Money.Text = _moneyContent;
                TB_Money.SelectionStart = TB_Money.Text.Length;
            }
            else
            {
                _moneyContent = TB_Money.Text;
            }
        }

        private void CB_Behavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_Behavior.SelectedIndex > -1)
                GlobalSetting.GetInstance().BetBehavior = CB_Behavior.SelectedIndex;
        }
    }
}
