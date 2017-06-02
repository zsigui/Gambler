using Gambler.Config;
using Gambler.Module.HF;
using Gambler.Module.HF.Model;
using Gambler.Module.XPJ.Model;
using Gambler.UI;
using Gambler.Utils;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Gambler
{
    public partial class FormMain : Form
    {

        public static readonly int TAB_XPJ = 0;

        private static FormMain sInstance;
        public static FormMain GetInstance() {
            if (sInstance == null)
            {
                sInstance = new FormMain();
            }
            return sInstance;
        }

        #region 账号区
        private Dictionary<string, XPJAccount> _xpjUserDict;

        private void AddXPJAccount(XPJAccount user)
        {
            if (_xpjUserDict == null)
            {
                _xpjUserDict = new Dictionary<string, XPJAccount>();
            }
            RemoveXPJAccount(user.Account);
            _xpjUserDict.Add(user.Account, user);
        }

        private void RemoveXPJAccount(string name)
        {
            if (_xpjUserDict != null && _xpjUserDict.ContainsKey(name))
            {
                _xpjUserDict.Remove(name);
            }
        }

        public void SaveXPJAccount()
        {

        }

        /// <summary>
        /// 添加账号到主界面的列表中
        /// </summary>
        /// <param name="user">新增账号</param>
        public void AddXPJUserToList(XPJAccount user)
        {
            if (_xpjUserDict != null && _xpjUserDict.ContainsKey(user.Account))
            {
                CLB_XPJUser.Items.Remove(user.Account);
            }
            AddXPJAccount(user);
            CLB_XPJUser.SetItemChecked(CLB_XPJUser.Items.Add(user.Account), true);
        }
        #endregion


        public FormMain()
        {
            InitializeComponent();
            RefreshTime(GlobalSetting.GetInstance().AutoRefreshTime);
            OnStartInit();
            //TestHF2();
        }

        #region 初始化
        private void OnStartInit()
        {
            DataTable dt = new DataTable();
        }

        private System.Threading.Timer _timer;
        private HFClient client;
        public void TestHF2()
        {
            ThreadUtil.RunOnThread(() =>
            {
                // 从服务器直接获取 h8 cookie，就可以不用登录了，每天cookie只会变动一次。
                client = new HFClient(null, null);
                client.AddH8Cookie("0xyb0lvxrnoisayogzbze0aa", 
                    "1E1AC1D784637066274C2C59159D97559D38B66D391587DDD48E427901FE1B3C5455DF02802A811B5F1EF6329D3F2112577DCDB6D37372BB06012F83196D43B0BAF545F7F477CACDB007A8DA18F16E94223E3541FAC1C4F5C3C56D392D6922F88C6E4F23A85A400F5B186930C6A20C595BD011D69905BCBBF190FEFBAB6CA0821F341C40");
                client.GetOddData(
                                      (matchData) =>
                                      {
                                          Console.WriteLine("OddData: 获取Odd数据!");
                                          _timer = ThreadUtil.RunOnTimer((state) =>
                                          {
                                              Console.WriteLine("TimerCallback 执行中...");
                                              if (client.LiveMatchs != null)
                                              {
                                                  foreach (KeyValuePair<string, HFSimpleMatch> entry in client.LiveMatchs)
                                                  {
                                                      HFSimpleMatch m = entry.Value;
                                                      client.GetSpecLiveEvent(entry.Key,
                                                          (eventData) =>
                                                          {
                                                              Console.WriteLine(String.Format("联赛:{0}，{1}（主队） vs {2} (客队)，事件信息：{3}，事件ID：{4}，下一事件请求ID：{5}",
                                                                  m.League, m.Home, m.Away, eventData.Info, eventData.CID, eventData.EID));
                                                              HFLiveEventIdNote note = new HFLiveEventIdNote();
                                                              if (eventData.CID.Equals(note.POSSIBLE_PENALTY))
                                                              {
                                                                  MessageBox.Show(String.Format("可能点球！", m.Home));
                                                              }
                                                              else if (eventData.CID.Equals(note.PEN1))
                                                              {
                                                                  MessageBox.Show(String.Format("{0}（主队）点球！", m.Home));
                                                              }
                                                              else if (eventData.CID.Equals(note.PEN2))
                                                              {
                                                                  MessageBox.Show(String.Format("{0}（客队）点球！", m.Away));
                                                              }
                                                              else if (eventData.CID.Equals(note.CPEN1))
                                                              {
                                                                  MessageBox.Show(String.Format("{0}（主队）点球取消！", m.Home));
                                                              }
                                                              else if (eventData.CID.Equals(note.CPEN2))
                                                              {
                                                                  MessageBox.Show(String.Format("{0}（客队）点球取消！", m.Away));
                                                              }
                                                          }, null, null);
                                                  }
                                              }
                                              else
                                              {
                                                  Console.Write("当前直播列表为空");
                                                  _timer.Change(Timeout.Infinite, 0);
                                              }
                                          },
                                          null,
                                          500,
                                          1000);
                                      },
                                      (status, code, msg) =>
                                      {
                                          Console.WriteLine("OddData : Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                                      },
                                      (e) =>
                                      {
                                          Console.WriteLine("OddData : " + e.Message);
                                      });
            });
        }

        public void TestHF()
        {
            ThreadUtil.RunOnThread(() =>
            {

                Console.WriteLine("开始执行");
                client = new HFClient("kaokkyyzz", "kaokkyyzz");
                client.Login(
                    (data) =>
                      {
                          Console.WriteLine("登录成功!");
                          client.LoginForH8((d) =>
                              {
                                  Console.WriteLine("H8登录成功!");
                                  
                              },
                              (status, code, msg) =>
                              {
                                  Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                              },
                              (e) =>
                              {
                                  Console.WriteLine(e.Message);
                              });
                      },
                      (status, code, msg) =>
                      {
                          Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                      },
                      (e) =>
                      {
                          Console.WriteLine(e.Message);
                      });

            });
        }
        #endregion

        private void TSMI_File_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TSMI_User_Add_Click(object sender, EventArgs e)
        {
            FormAddUser.newInstance().Show();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void TSMI_User_Remove_Click(object sender, EventArgs e)
        {
            if (TC_User.SelectedIndex == TAB_XPJ)
            {
                if (CLB_XPJUser.SelectedIndices != null && CLB_XPJUser.SelectedIndices.Count > 0)
                {
                    foreach (object item in CLB_XPJUser.SelectedItems)
                    {
                        if (item is string)
                        {
                            RemoveXPJAccount((string)item);
                        }
                    }
                }
                CLB_XPJUser.SelectedItems.Clear();
            }
        }

        private void TSMI_File_Setting_Click(object sender, EventArgs e)
        {
            FormSetting setting = new FormSetting();
            if (setting.ShowDialog() == DialogResult.OK)
            {
                // 重置刷新
                RefreshTime(GlobalSetting.GetInstance().AutoRefreshTime);
            }
        }

        private void RefreshTime(int t)
        {
            BTN_Refresh.Text = String.Format("刷新：{0}s", t);
        }
    }
}
