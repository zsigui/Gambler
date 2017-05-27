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
            if (String.IsNullOrEmpty(TB_Account.Text))
            {
                EP_Account.SetError(TB_Password, "用户名不能为空");
            } else
            {
                EP_Account.SetError(TB_Password, "");
            }
        }
    }
}
