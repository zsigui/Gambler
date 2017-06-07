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
        public static FormMain GetInstance()
        {
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

        public void LoadXPJAccounts()
        {
            ThreadUtil.RunOnThread(() =>
            {
                LogUtil.Write("执行LoadXPJAccounts");
                string data = FileUtil.ReadContentFromFilePath(GlobalSetting.XPJ_USER_PATH);
                LogUtil.Write("LoadXPJAccounts数据：data = " + data);
                if (String.IsNullOrEmpty(data))
                    return;
                Dictionary<string, XPJAccount> accounts = JsonUtil.fromJson<Dictionary<string, XPJAccount>>(data);
                LogUtil.Write("经过data转换后的dict对象: " + accounts);
                if (accounts != null && accounts.Count > 0)
                {
                    // 对所有账号执行登录操作
                    Dictionary<string, XPJAccount> tmpAccounts = new Dictionary<string, XPJAccount>();
                    foreach (KeyValuePair<string, XPJAccount> entry in accounts)
                    {
                        entry.Value.newClient().Login((ret) =>
                        {
                            LogUtil.Write("登录成功，添加项: " + entry.Key);
                            entry.Value.GetClient().GetUserInfo((d) =>
                            {
                                Console.WriteLine(entry.Value.Account + "金币余额: " + d.money);
                                entry.Value.Money = d.money;
                            }, null, null);
                            // 将返回成功的添加的字典中
                            tmpAccounts.Add(entry.Key, entry.Value);
                        }, null, null);
                    }
                    Invoke(new Action(() => {
                        foreach (XPJAccount account in accounts.Values)
                        {
                            AddXPJUserToList(account);
                        }
                    }));
                    
                }
                LogUtil.Write("从XpjUser文件中读取数据操作完毕");
            });
        }

        public void SaveXPJAccounts()
        {
            string data = "";
            if (HasXPJAccount())
                data = JsonUtil.toJson(_xpjUserDict);
            FileUtil.WriteContentToFilePath(GlobalSetting.XPJ_USER_PATH, data);
            LogUtil.Write("将数据写入了XpjUser文件中操作完毕");
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
            CLB_XPJUser.SetItemChecked(CLB_XPJUser.Items.Add(user.Account), user.IsChecked);
        }

        public bool HasXPJAccount()
        {
            return _xpjUserDict != null && _xpjUserDict.Count > 0;
        }

        /// <summary>
        /// 获取当前存在的XPJ账号列表，需要先调用HasXPJAccount()进行判断
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XPJAccount> ObtainAccounts()
        {
            return _xpjUserDict.Values;
        }

        /// <summary>
        /// 获取指定账号名的账号，需要先调用HasXPJAccount()进行判断
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XPJAccount ObtainAccountByName(string name)
        {
            if (!_xpjUserDict.ContainsKey(name))
                return null;
            return _xpjUserDict[name];
        }
        #endregion


        public FormMain()
        {
            InitializeComponent();
            RefreshTime(GlobalSetting.GetInstance().AutoRefreshTime);
            OnStartInit();
        }

        #region 初始化
        private void OnStartInit()
        {
            RefreshState(false, false);
            DoInitAndLoginH8();
        }

        private static readonly long DEFAULT_EMPTY_TIME_DIFF = 60000;
        // 记录上一次获取到非空数据的时间
        private long _lastGetDataTime;
        // 自动刷新的倒计时
        private int _autoRefreshCuountdown;
        private System.Threading.Timer _liveTimer;
        private System.Threading.Timer _h8DataTimer;
        private HFClient _h8Client;
        private HFLiveEventIdNote _eventIdInfo = new HFLiveEventIdNote();
        // 选择关注的直播赛事列表
        private List<string> _checkedLiveList = new List<string>(); 

        private void UpdateDGVData(Dictionary<string, HFSimpleMatch> matchs)
        {
            ThreadUtil.WorkOnUI(
                this,
                new Action<Dictionary<string, HFSimpleMatch>>((data) => {

                    DGV_Live.Rows.Clear();
                    if (data == null || data.Count == 0)
                    {
                        _checkedLiveList.Clear();
                    }
                    else
                    {
                        _checkedLiveList.Clear();
                        DataGridViewRow dr;
                        foreach (HFSimpleMatch m in data.Values)
                        {
                            _checkedLiveList.Add(m.MID);
                            dr = new DataGridViewRow();
                            dr.CreateCells(DGV_Live);
                            dr.Cells[0].Value = m.MID;
                            dr.Cells[1].Value = String.Format("{0} / {1}", m.Score, m.Time);
                            dr.Cells[2].Value = m.League;
                            dr.Cells[3].Value = m.Home;
                            dr.Cells[4].Value = m.Away;
                            dr.Cells[5].Value = true;
                            DGV_Live.Rows.Add(dr);
                        }
                    }
                }),
                matchs);
            
        }

        private void DoGetLiveMatchAfterGetData(bool resetTimer)
        {
            if (_h8Client == null || !_h8Client.IsH8Login)
            {
                DoInitAndLoginH8();
                return;
            }
            if (_liveTimer != null)
            {
                if (resetTimer)
                {
                    _liveTimer.Change(0, 1000);
                }
                return;
            }
            _liveTimer = ThreadUtil.RunOnTimer((state) =>
            {
                Console.WriteLine("TimerCallback 执行中...");
                if (_h8Client.LiveMatchs != null)
                {
                    Dictionary<string, HFSimpleMatch> lives = new Dictionary<string, HFSimpleMatch>(_h8Client.LiveMatchs);
                    foreach (KeyValuePair<string, HFSimpleMatch> entry in lives)
                    {
                        HFSimpleMatch m = entry.Value;
                        _h8Client.GetSpecLiveEvent(entry.Key,
                            (eventData) =>
                            {
                                Console.WriteLine(String.Format("联赛:{0}，{1}（主队） vs {2} (客队)，事件信息：{3}，事件ID：{4}，下一事件请求ID：{5}",
                                    m.League, m.Home, m.Away, eventData.Info, eventData.CID, eventData.EID));
                                if (_checkedLiveList.Contains(eventData.MID.ToString()))
                                {
                                    DealWithTargetMatch(m, eventData.CID);
                                }

                            }, null, null);
                    }
                }
                else
                {
                    Console.Write("当前直播列表为空，停止直播任务事件监听");
                    _liveTimer.Change(Timeout.Infinite, 1000);
                }
            },
            null,
            0,
            1000);
        }

        private static readonly string REGEX_FORMAT = "【{0}】-【{1}】-【{2}】-【{3}】（主）vs.【{4}】（客）-【{5}】\n\n";
        private void DealWithTargetMatch(HFSimpleMatch m, string cid)
        {
            ThreadUtil.WorkOnUI<object>(this, new Action(()=> {
                if (_eventIdInfo.POSSIBLE_PENALTY.Equals(cid))
                {
                    // 可能点球
                    RTB_Output.AppendText(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "可能发生点球事件"));
                }
                else if (_eventIdInfo.PEN1.Equals(cid))
                {
                    // 主队点球
                    MessageBox.Show(String.Format("【{0}】{1}(主队)-进行点球", m.League, m.Home),
                        "点球提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RTB_Output.AppendText(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "主队点球事件"));
                    HandleAutoBetEvent(m, true);
                }
                else if (_eventIdInfo.PEN2.Equals(cid))
                {
                    // 客队点球
                    MessageBox.Show(String.Format("【{0}】{1}(客队)-进行点球", m.League, m.Away),
                        "点球提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RTB_Output.AppendText(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "客队点球事件"));
                    HandleAutoBetEvent(m, false);
                }
                else if (_eventIdInfo.PENALTY_MISS.Equals(cid)
                    || _eventIdInfo.CPEN1.Equals(cid) || _eventIdInfo.CPEN2.Equals(cid))
                {
                    // 点球取消
                    RTB_Output.AppendText(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "点球取消事件"));
                }
                else if (_eventIdInfo.MPEN1.Equals(cid))
                {
                    RTB_Output.AppendText(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "主队点球失误事件"));
                }
                else if (_eventIdInfo.MPEN2.Equals(cid))
                {
                    RTB_Output.AppendText(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "客队点球失误事件"));
                }
                else if (_eventIdInfo.GOAL_PEN1.Equals(cid))
                {
                    // 主队点球得分
                    MessageBox.Show(String.Format("【{0}】{1}(主队)-点球得分", m.League, m.Home),
                        "点球提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RTB_Output.AppendText(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "主队点球得分事件"));
                }
                else if (_eventIdInfo.GOAL_PEN2.Equals(cid))
                {
                    // 客队点球得分
                    MessageBox.Show(String.Format("【{0}】{1}(客队)-点球得分", m.League, m.Away),
                        "点球提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RTB_Output.AppendText(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "客队点球得分事件"));
                }
//                 else if(_eventIdInfo.SAFE.Equals(cid) || _eventIdInfo.SAFE1.Equals(cid) || _eventIdInfo.SAFE2.Equals(cid))
//                 {
//                     RTB_Output.AppendText(String.Format(REGEX_FORMAT,
//                        m.Score, m.Time, m.League, m.Home, m.Away, "控球事件"));
//                     HandleAutoBetEvent(m, false);
//                 }
            }));
        }

        /// <summary>
        /// 判断处理点球时的下注情况
        /// </summary>
        private void HandleAutoBetEvent(HFSimpleMatch m, bool isHome)
        {
            // 执行自动下注请求
            // 新葡京
            // 没有账号的时候不进行下注
            if (HasXPJAccount())
            {
                FormInfo.NewInstance().Show();
                FormInfo.NewInstance().AutoRequest(new string[] { m.League, m.Home, m.Away }, isHome);
            }

            // 其他
        }

        #region 获取Live数据方式1，需要回传获取中文数据的Cookie

        private void DoGetLiveDataAfterInitAndLogin(bool resetTimer)
        {
            if (_h8Client == null || !_h8Client.IsH8Login)
            {
                DoInitAndLoginH8();
                return;
            }
            if (_h8DataTimer != null)
            {
                if (resetTimer)
                {
                    // 重置定时器自动刷新时间s
                    _autoRefreshCuountdown = 1;
                    _h8DataTimer.Change(0, CB_IsAutoRefresh.Checked ? 1000 : Timeout.Infinite);
                }
                return;
            }

            _h8DataTimer = ThreadUtil.RunOnTimer(
                (tobj) =>
                {
                    RefreshTime(--_autoRefreshCuountdown);
                    if (_autoRefreshCuountdown > 0)
                    {
                        return;
                    }
                    _autoRefreshCuountdown = GlobalSetting.GetInstance().AutoRefreshTime;
                    RefreshState(false, false);

                    _h8Client.GetOddData(
                        (matchData) =>
                        {
                            RefreshState(true, false);
                            Console.WriteLine("OddData: 获取Odd数据!");
                            /// matchData 表示这一轮获取到全部数据
                            /// _h8Client.LiveMatchs 表示根据该数据解析到的 Live数据列表
                            long curTime = TimeUtil.CurrentTimeMillis();
                            if (matchData != null && matchData.Count > 0
                                && _h8Client.LiveMatchs != null && _h8Client.LiveMatchs.Count > 0)
                            {
                                // 两边都有数据，说明此时实际获取到了，重新设置timer执行查找新的数据
                                _lastGetDataTime = curTime;
                                UpdateDGVData(_h8Client.LiveMatchs);
                                DoGetLiveMatchAfterGetData(true);
                            }
                            else if (curTime - _lastGetDataTime > DEFAULT_EMPTY_TIME_DIFF)
                            {
                                // 更新空数据
                                UpdateDGVData(null);
                            }
                            
                        },
                        (status, code, msg) =>
                        {
                            LogUtil.Write("OddData : Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                            RefreshState(true, false);
                        },
                        (e) =>
                        {
                            RefreshState(true, false);
                            LogUtil.Write("OddData : " + e.Message);
                        });
                },
                null,
                0,
                CB_IsAutoRefresh.Checked ? 1000 : Timeout.Infinite);

        }

        public void DoInitAndLoginH8T()
        {
            ThreadUtil.RunOnThread(() =>
            {
                // 从服务器直接获取 h8 cookie，就可以不用登录了，每天cookie只会变动一次

                _h8Client = new HFClient(null, null);
                _h8Client.IsH8Login = true;
                _h8Client.AddH8Cookie("gtmj1d45tp5gmu55mzdyjw45",
                    "65323DF70346C454FA1EAAADD38FE805C7E5C71EECAB7101DC33C95C6310ADD6B7301B91579171E58FB274039112725F87EA60483028C3DBC06DCB56CBBB2FFB6E5A8F800C0D72E23F47B88E5BC85C09B3954154E3DD474F801F51285E23E1A91ABDE8A30AA6FDFDC6AE21E3DDB8E4255555609B3AAB445679F1432EB30E9C414964DC0B");
                DoGetLiveDataAfterInitAndLogin(true);
            });
        }
        #endregion

        #region 获取Live数据方式2，需要登录账号
        public void DoInitAndLoginH8()
        {
            ThreadUtil.RunOnThread(() =>
            {

                Console.WriteLine("开始执行");
                if (_h8Client == null)
                {
                    _h8Client = new HFClient("kaokkyyzz", "kaokkyyzz");
                }
                _h8Client.Login(
                    (data) =>
                      {
                          Console.WriteLine("登录成功!");
                          _h8Client.LoginForH8((d) =>
                              {
                                  Console.WriteLine("H8登录成功!");
                                  DoGetLiveDataAfterInitAndLogin(true);
                              },
                              (status, code, msg) =>
                              {
                                  RefreshState(true, true);
                                  Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                              },
                              (e) =>
                              {
                                  RefreshState(true, true);
                                  Console.WriteLine(e.Message);
                              });
                      },
                      (status, code, msg) =>
                      {
                          RefreshState(true, true);
                          Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                      },
                      (e) =>
                      {
                          RefreshState(true, true);
                          Console.WriteLine(e.Message);
                      });

            });
        }
        #endregion

        #endregion

        #region 事件
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
                    List<string> toBeRemoved = new List<string>(CLB_XPJUser.SelectedItems.Count);
                    foreach (object item in CLB_XPJUser.SelectedItems)
                    {
                        if (item is string)
                        {
                            toBeRemoved.Add((string)item);
                        }
                    }

                    foreach (string item in toBeRemoved)
                    {
                        RemoveXPJAccount(item);
                        CLB_XPJUser.Items.Remove(item);
                    }
                }
               
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

        private void TSMI_Live_CheckAll_Click(object sender, EventArgs e)
        {
            if (DGV_Live.Rows.Count == 0 || _h8Client == null
                || _h8Client.LiveMatchs == null)
                return;
            if (_checkedLiveList != null && _checkedLiveList.Count == DGV_Live.Rows.Count)
            {
                foreach (DataGridViewRow r in DGV_Live.Rows)
                {
                    r.Cells[5].Value = false;
                }
                _checkedLiveList.Clear();
            }
            else
            {
                foreach (DataGridViewRow r in DGV_Live.Rows)
                {
                    r.Cells[5].Value = true;
                }
                _checkedLiveList.AddRange(_h8Client.LiveMatchs.Keys);
            }
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            RefreshState(false, false);
            DoGetLiveDataAfterInitAndLogin(true);
        }
        
        private void CB_IsAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            BTN_Refresh_Click(sender, e);
        }
        
        private void BTN_JumpBet_Click(object sender, EventArgs e)
        {
            FormInfo.NewInstance().Show();
        }
        #endregion

        private void RefreshTime(int t)
        {
            ThreadUtil.WorkOnUI(this, new Action<int>((time) =>
            {
                BTN_Refresh.Text = String.Format("刷新：{0}s", t);
            }),
            t);
        }

        private void RefreshState(bool e, bool err)
        {
            ThreadUtil.WorkOnUI(this, new Action<bool, bool>((enabled, er) =>
            {

                BTN_Refresh.Enabled = enabled;
                if (!enabled)
                    BTN_Refresh.Text = "刷新中ing";
                else
                {
                    if (er)
                    {
                        BTN_Refresh.Text = "出错，请手动刷新";
                    } else if (CB_IsAutoRefresh.Checked)
                    {
                        BTN_Refresh.Text = String.Format("刷新：{0}s", _autoRefreshCuountdown);
                    }
                    else
                    {
                        BTN_Refresh.Text = "点击刷新";
                    }
                }
            }),
            e, err);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadXPJAccounts();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveXPJAccounts();
            GlobalSetting.GetInstance().Save();
        }

        private void CLB_XPJUser_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (HasXPJAccount())
                _xpjUserDict[CLB_XPJUser.Items[e.Index].ToString()].IsChecked = (e.NewValue == CheckState.Checked);
        }
    }
}
