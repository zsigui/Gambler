﻿using Gambler.Config;
using Gambler.Module.X469;
using Gambler.Module.XPJ.Model;
using Gambler.Module.YL5;
using Gambler.Utils;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler.UI
{
    public partial class FormAddUser : Form
    {

        private static FormAddUser sInstance = null;

        public static FormAddUser newInstance()
        {
            if (sInstance == null)
            {
                sInstance = new FormAddUser();
            } else
            {
                sInstance.Focus();
            }
            return sInstance;
        }

        private IntegratedAccount _account;

        public FormAddUser()
        {
            InitializeComponent();
            CB_Type.SelectedIndex = 0;
        }

        /// <summary>
        /// 新添加的账号
        /// </summary>
        public IntegratedAccount Account
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
            if (!BTN_Add.Enabled)
                return;

            if (_account == null)
            {
                MessageBox.Show("请先输入登录账号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(_account.Password)
                || String.IsNullOrEmpty(_account.Account))
            {
                MessageBox.Show("账号或者密码不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 表示设置了代理地址
            if (!String.IsNullOrEmpty(TB_ProxyAdress.Text))
            {
                int port = 80;
                if (!String.IsNullOrEmpty(TB_Port.Text) && !Int32.TryParse(TB_Port.Text.Trim(), out port))
                {
                    MessageBox.Show("代理端口输入错误，端口号应该是1~65356的整形", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _account.Proxy = new System.Net.WebProxy(TB_ProxyAdress.Text.Trim(), port);
                if (!String.IsNullOrEmpty(TB_ProxyAccount.Text)
                    && !String.IsNullOrEmpty(TB_ProxyPwd.Text))
                {
                    // 如果不需要账号密码，放空不处理
                    _account.Proxy.Credentials = new NetworkCredential(TB_ProxyAccount.Text.Trim(), 
                        TB_ProxyPwd.Text.Trim());
                }
            }

            BTN_Add.Enabled = false;
            _account.Type = CB_Type.SelectedIndex;
            // 开启线程验证登录 
            ThreadUtil.RunOnThread(() =>
            {
                switch (_account.Type)
                {
                    case AcccountType.XPJ155:
                        #region 新葡京155
                        _account.newClient<XPJClient>().Login(
                            (data) =>
                            {
                                _account.GetClient<XPJClient>().GetUserInfo((d) =>
                                {
                                    Console.WriteLine("当前金币余额: " + d.money);
                                    _account.Money = d.money;
                                }, null, null);
                                MessageBox.Show("添加用户成功!");
                                Invoke(new Action(() =>
                                {
                                    FormMain.GetInstance().AddUserToList(_account);
                                    Close();
                                }));
                            },
                            (status, code, msg) =>
                            {
                                Invoke(new Action(() =>
                                {
                                    BTN_Add.Enabled = true;
                                }));
                                MessageBox.Show("登录失败，请检查后重试！");
                            },
                            (err) =>
                            {
                                Invoke(new Action(() =>
                                {
                                    BTN_Add.Enabled = true;
                                }));
                                LogUtil.Write(err);
                                MessageBox.Show("登录失败，请检查后重试！");
                            });
                        #endregion
                        break;
                    case AcccountType.XPJ469:
                        #region 新葡京469
                        _account.newClient<X469Client>().Login(
                           (data) =>
                           {
                               _account.GetClient<X469Client>().GetUserInfo((d) =>
                               {
                                   Console.WriteLine("当前金币余额: " + d.money);
                                   _account.Money = Convert.ToDouble(d.money);
                               }, null, null);
                               MessageBox.Show("添加用户成功!");
                               Invoke(new Action(() =>
                               {
                                   FormMain.GetInstance().AddUserToList(_account);
                                   Close();
                               }));
                           },
                           (status, code, msg) =>
                           {
                               Invoke(new Action(() =>
                               {
                                   BTN_Add.Enabled = true;
                               }));
                               MessageBox.Show("登录失败，请检查后重试！");
                           },
                           (err) =>
                           {
                               Invoke(new Action(() =>
                               {
                                   BTN_Add.Enabled = true;
                               }));
                               LogUtil.Write(err);
                               MessageBox.Show("登录失败，请检查后重试！");
                           });
                        #endregion
                        break;
                    case AcccountType.YL5789:
                        #region YL5789
                        _account.newClient<YL5Client>().Login(
                           (data) =>
                           {
                               _account.GetClient<YL5Client>().GetUserInfo((d) =>
                               {
                                   Console.WriteLine("当前金币余额: " + d.money);
                                   _account.Money = Convert.ToDouble(d.money);
                               }, null, null);
                               MessageBox.Show("添加用户成功!");
                               Invoke(new Action(() =>
                               {
                                   FormMain.GetInstance().AddUserToList(_account);
                                   Close();
                               }));
                           },
                           (status, code, msg) =>
                           {
                               Invoke(new Action(() =>
                               {
                                   BTN_Add.Enabled = true;
                               }));
                               MessageBox.Show("登录失败，请检查后重试！");
                           },
                           (err) =>
                           {
                               Invoke(new Action(() =>
                               {
                                   BTN_Add.Enabled = true;
                               }));
                               LogUtil.Write(err);
                               MessageBox.Show("登录失败，请检查后重试！");
                           });
                        #endregion
                        break;
                }
            });

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
                _account = new IntegratedAccount();
            };
        }

        private void FormAddUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            sInstance = null;
        }

        private void FormAddUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BTN_Add.Focus();
                BTN_Add_Click(sender, e);
            }
        }

        private void TB_Account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TB_Password.Focus();
            }
        }
    }
}
