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
    public partial class FormSetting : Form
    {
        private string _content = "";
        private bool _isInit = true;

        public FormSetting()
        {
            InitializeComponent();
            GlobalSetting gs = GlobalSetting.GetInstance();
            TB_AutoRefreshTime.Text = gs.AutoRefreshTime.ToString();
            CB_AutoSaveUser.Checked = gs.IsAutoSaveUser;
            CB_AutoBet.Checked = gs.IsAutoBet;
            CB_ShowBetDialog.Checked = gs.IsShowBetDialog;
            _isInit = true;
        }

        private void TB_AutoRefreshTime_TextChanged(object sender, EventArgs e)
        {
            Match m = Regex.Match(TB_AutoRefreshTime.Text, @"^\d{0,3}$");
            if (!m.Success)
            {
                TB_AutoRefreshTime.Text = _content;
                TB_AutoRefreshTime.SelectionStart = TB_AutoRefreshTime.Text.Length;
            }
            else
            {
                _content = TB_AutoRefreshTime.Text;
            }
        }

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            int time;
            if (Int32.TryParse(_content, out time)
                && time != GlobalSetting.GetInstance().AutoRefreshTime
                && time > 0)
            {
                GlobalSetting.GetInstance().AutoRefreshTime = time;

                // 说明需要触发刷新间隔重置
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void CB_AutoBet_CheckedChanged(object sender, EventArgs e)
        {
            if (_isInit)
            {
                _isInit = false;
                return;
            }
            if (CB_AutoBet.Checked)
            {
                FormAutoBetSetting dialog = new FormAutoBetSetting();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalSetting.GetInstance().IsAutoBet = CB_AutoBet.Checked = true;
                }
                else
                {
                    GlobalSetting.GetInstance().IsAutoBet = CB_AutoBet.Checked = false;
                }
            }
        }

        private void CB_AutoSaveUser_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSetting.GetInstance().IsAutoSaveUser = CB_AutoSaveUser.Checked;
        }

        private void CB_AutoBetDialog_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSetting.GetInstance().IsShowBetDialog = CB_ShowBetDialog.Checked;
        }
    }
}
