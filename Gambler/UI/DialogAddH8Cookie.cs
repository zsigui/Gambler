using Gambler.Config;
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
    public partial class DialogAddH8Cookie : Form
    {
        public bool IsAccount
        {
            get;
            set;
        }

        public string UserOrSession
        {
            get;
            set;
        }

        public string PwdOrAuth
        {
            get;
            set;
        }

        public DialogAddH8Cookie()
        {
            InitializeComponent();

            RB_Account.Checked = true;
        }

        private void BTN_Confirm_Click(object sender, EventArgs e)
        {
            string session = TB_Session.Text;
            string auth = TB_Auth.Text;
            if (String.IsNullOrEmpty(session)
                || String.IsNullOrEmpty(auth))
            {
                MessageBox.Show("不能留空");
                return;
            }
            if (RB_Account.Checked)
            {
                GlobalSetting.GetInstance().HFAccount = session + ":" + auth;
            }

            IsAccount = RB_Account.Checked;
            UserOrSession = session;
            PwdOrAuth = auth;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RB_Account_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Account.Checked)
            {
                LB_Session.Text = "用户名：";
                LB_Auth.Text = "密码：";
            }
            else
            {
                LB_Session.Text = "Session：";
                LB_Auth.Text = "Auth：";
            }
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
