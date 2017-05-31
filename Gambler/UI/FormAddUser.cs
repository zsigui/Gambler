using Gambler.Module.XPJ.Model;
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
    public partial class FormAddUser : Form
    {

        private XPJAccount _account;

        public FormAddUser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 新添加的账号
        /// </summary>
        public XPJAccount Account
        {
            get
            {
                return _account;
            }

            set
            {
                _account = value;
            }
        }

        private void BTN_Add_Click(object sender, EventArgs e)
        {
        }

        private void TB_Account_Validating(object sender, CancelEventArgs e)
        {
            string text = TB_Account.Text.Trim();
            if (String.IsNullOrEmpty(text))
            {
                EP_Validated.SetError(TB_Account, "用户名不能为空");
                return;
            }

            EP_Validated.SetError(TB_Account, null);
            CreateAccountIfNeed();
            _account.Account = text;
        }

        private void TB_Password_Validating(object sender, CancelEventArgs e)
        {
            string text = TB_Password.Text.Trim();
            if (String.IsNullOrEmpty(text))
            {
                EP_Validated.SetError(TB_Password, "密码不能为空");
                return;
            }

            EP_Validated.SetError(TB_Password, "");
            CreateAccountIfNeed();
            _account.Password = text;
        }

        private void CreateAccountIfNeed()
        {
            if (_account == null)
            {
                _account = new XPJAccount();
            };
        }
    }
}
