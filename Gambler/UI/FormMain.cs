using Gambler.Bet;
using Gambler.Bet.Task;
using Gambler.Config;
using Gambler.Module;
using Gambler.Module.HF;
using Gambler.Module.HF.Model;
using Gambler.Module.X469;
using Gambler.Module.XPJ.Model;
using Gambler.UI;
using Gambler.Utils;
using Gambler.Utils.Manager;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        private List<IntegratedAccount> _xpjUserDict;

        private void AddAccount(IntegratedAccount user)
        {
            if (_xpjUserDict == null)
            {
                _xpjUserDict = new List<IntegratedAccount>();
            }
            RemoveAccount(user);
            _xpjUserDict.Add(user);
        }

        private void RemoveAccount(IntegratedAccount account)
        {
            int index = FindIndex(account);
            if (index > -1)
            {
                _xpjUserDict.RemoveAt(index);
            }
        }

        private void RemoveAccountByName(string name)
        {
            if (HasAccount())
            {
                IntegratedAccount account;
                for (int i = _xpjUserDict.Count - 1; i >= 0; i--)
                {
                    account = _xpjUserDict[i];
                    if (account.Account.Equals(name))
                        _xpjUserDict.RemoveAt(i);
                }
            }
        }

        private int FindIndex(IntegratedAccount account)
        {
            if (account != null && HasAccount())
            {
                IntegratedAccount tmp;
                for (int i = _xpjUserDict.Count - 1; i >= 0; i--)
                {
                    tmp = _xpjUserDict[i];
                    if (account.Account.Equals(tmp.Account)
                        && account.Type == tmp.Type)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public void LoadAccounts()
        {
            ThreadUtil.RunOnThread(() =>
            {
                LogUtil.Write("执行LoadXPJAccounts");
                string data = FileUtil.ReadContentFromFilePath(GlobalSetting.XPJ_USER_PATH);
                LogUtil.Write("LoadXPJAccounts数据：data = " + data);
                if (String.IsNullOrEmpty(data))
                    return;
                List<IntegratedAccount> accounts = JsonUtil.fromJson<List<IntegratedAccount>>(data);
                LogUtil.Write("经过data转换后的dict对象: " + accounts);
                if (accounts != null && accounts.Count > 0)
                {
                    // 对所有账号执行登录操作
                    List<IntegratedAccount> tmpAccounts = new List<IntegratedAccount>();
                    foreach (IntegratedAccount account in accounts)
                    {
                        if (account.Type == AcccountType.XPJ155)
                        {
                            account.GetClient<XPJClient>().Login((ret) =>
                            {
                                LogUtil.Write("登录成功，添加项: " + account.Account);
                                account.GetClient<XPJClient>().GetUserInfo((d) =>
                                {
                                    account.Money = d.money;
                                }, null, null);
                            // 将返回成功的添加的字典中
                            tmpAccounts.Add(account);
                            }, null, null);
                        }
                        else if (account.Type == AcccountType.XPJ469)
                        {
                            account.GetClient<X469Client>().Login((ret) =>
                            {
                                LogUtil.Write("登录成功，添加项: " + account.Account);
                                account.GetClient<X469Client>().GetUserInfo((d) =>
                                {
                                    account.Money = Convert.ToDouble(d.money);
                                }, null, null);
                                // 将返回成功的添加的字典中
                                tmpAccounts.Add(account);
                            }, null, null);
                        }
                        else if (account.Type == AcccountType.YL5789)
                        {
                            account.GetClient<YL5Client>().Login((ret) =>
                            {
                                LogUtil.Write("登录成功，添加项: " + account.Account);
                                account.GetClient<YL5Client>().GetUserInfo((d) =>
                                {
                                    account.Money = Convert.ToDouble(d.money);
                                }, null, null);
                                // 将返回成功的添加的字典中
                                tmpAccounts.Add(account);
                            }, null, null);
                        }
                    }
                    Invoke(new Action(() => {
                        foreach (IntegratedAccount account in tmpAccounts)
                        {
                            AddUserToList(account);
                        }
                    }));
                    
                }
                LogUtil.Write("从XpjUser文件中读取数据操作完毕");
            });
        }

        public void SaveAccounts()
        {
            string data = "";
            if (HasAccount())
                data = JsonUtil.toJson(_xpjUserDict);
            FileUtil.WriteContentToFilePath(GlobalSetting.XPJ_USER_PATH, data);
            LogUtil.Write("将数据写入了XpjUser文件中操作完毕");
        }

        /// <summary>
        /// 添加账号到主界面的列表中
        /// </summary>
        /// <param name="user">新增账号</param>
        public void AddUserToList(IntegratedAccount user)
        {
            int index = FindIndex(user);
            if (index > - 1)
            {
                _xpjUserDict.RemoveAt(index);
                CLB_XPJUser.Items.RemoveAt(index);
            }
            AddAccount(user);
            string type = "";
            switch (user.Type)
            {
                case AcccountType.XPJ155:
                    type = GlobalSetting.X159_KEY;
                    break;
                case AcccountType.XPJ469:
                    type = GlobalSetting.X469_KEY;
                    break;
                case AcccountType.YL5789:
                    type = GlobalSetting.YL5_KEY;
                    break;
            }
            CLB_XPJUser.SetItemChecked(CLB_XPJUser.Items.Add(String.Format("{0} [{1}]",
                user.Account, type)), user.IsChecked);
        }

        public bool[] ContainsAccount(int[] type)
        {
            bool[] result = new bool[type.Length];
            if (_xpjUserDict == null)
                return result;
                int i;
            int count = 0;
            foreach (IntegratedAccount acc in _xpjUserDict)
            {
                for (i = type.Length - 1; i >= 0; i--)
                {
                    if (!result[i] &&  type[i] == acc.Type)
                    {
                        result[i] = true;
                        count++;
                        if (count == type.Length)
                            return result;
                    }
                }
            }
            return result;
        }

        public bool ContainsAccount(int type)
        {
            if (_xpjUserDict == null)
                return false;
            foreach (IntegratedAccount acc in _xpjUserDict)
            {
                if (acc.Type == type)
                    return true;
            }
            return false;
        }

        public bool HasAccount()
        {
            return _xpjUserDict != null && _xpjUserDict.Count > 0;
        }

        /// <summary>
        /// 获取当前存在的XPJ账号列表，需要先调用HasXPJAccount()进行判断
        /// </summary>
        /// <returns></returns>
        public List<IntegratedAccount> ObtainAccounts(int type)
        {
            if (type == -1)
                return _xpjUserDict;

            if (HasAccount())
            {
                List<IntegratedAccount> accounts = new List<IntegratedAccount>();
                foreach (IntegratedAccount account in _xpjUserDict)
                {
                    if (type == account.Type && account.IsChecked)
                        accounts.Add(account);
                }
                return accounts;
            }
            return null;
        }
        
        #endregion


        public FormMain()
        {

            GlobalSetting.GetInstance().Load();
            InitializeComponent();
            OnStartInit();
        }

        #region 初始化
        private void OnStartInit()
        {
            BTN_Refresh_Click(null, null);
        }

        private static readonly long DEFAULT_EMPTY_TIME_DIFF = 60000;
        // 记录上一次获取到非空数据的时间
        private long _lastGetDataTime;
        // 自动刷新的倒计时
        private int _autoRefreshCuountdown;
        private System.Threading.Timer _liveTimer;
        private System.Threading.Timer _h8DataTimer;
        private HFClient _h8Client;
        // 选择关注的直播赛事列表
        private List<string> _uncheckedList = new List<string>();
        private List<string> _midList = new List<string>();
        private bool _isInErr = false;

        private void UpdateDGVData(Dictionary<string, HFSimpleMatch> matchs)
        {
            ThreadUtil.WorkOnUI(
                this,
                new Action<Dictionary<string, HFSimpleMatch>>((data) => {

                    DGV_Live.Rows.Clear();
                    _midList.Clear();
                    if (data != null && data.Count > 0)
                    {
                        DataGridViewRow dr;
                        foreach (HFSimpleMatch m in data.Values)
                        {
                            _midList.Add(m.MID);
                            dr = new DataGridViewRow();
                            dr.CreateCells(DGV_Live);
                            dr.Cells[0].Value = m.MID;
                            string time = "半场休息";
                            if (m.Time.StartsWith("1H"))
                            {
                                time = String.Format("上半场 {0}", m.Time.Substring(2).Trim());
                            }
                            else if (m.Time.StartsWith("2H"))
                            {
                                time = String.Format("下半场 {0}", m.Time.Substring(2).Trim());
                            }
                            dr.Cells[1].Value = time;
                            dr.Cells[2].Value = m.Score;
                            dr.Cells[3].Value = m.League;
                            dr.Cells[4].Value = m.Home;
                            dr.Cells[5].Value = m.Away;
                            dr.Cells[6].Value = !_uncheckedList.Contains(m.MID);
                            DGV_Live.Rows.Add(dr);
                        }
                        for (int i = _uncheckedList.Count - 1; i >= 0; i--)
                        {
                            if (!_midList.Contains(_uncheckedList[i]))
                                _uncheckedList.RemoveAt(i);
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

            LogUtil.Write("1");
            if ((_h8Client.LiveMatchs == null || _h8Client.LiveMatchs.Count == 0)
                && LiveThreadManager.Instance.isRunning)
                LiveThreadManager.Instance.Stop();
            LogUtil.Write("2");
            if (LiveThreadManager.Instance.isRunning)
                return;
            LogUtil.Write("3");
            LiveThreadManager.Instance.Start(_h8Client);
        }

        private static readonly string REGEX_FORMAT = "【{0}】-【{1}】-【{2}】-【{3}】（主）vs.【{4}】（客）-【{5}】";

        private void PlayVoice(string path)
        {
            WMP_Player.URL = Application.StartupPath + path;
            WMP_Player.Ctlcontrols.play();
        }

        #region 球赛事件

        public void UpdateLiveMatch(HFLiveEvent eventData, HFSimpleMatch m)
        {
            if (!_uncheckedList.Contains(m.MID))
            {
                LogUtil.Write(String.Format("联赛:{0}，{1}（主队） vs {2} (客队)，事件信息：{3}，事件ID：{4}, 下一请求ID：{5}",
                     m.League, m.Home, m.Away, eventData.Info, eventData.CID, eventData.EID));
                DealWithTargetMatch(m, eventData.CID, eventData.Info);
            }
        }
        private void DealWithTargetMatch(HFSimpleMatch m, string cid, string info)
        {
            ThreadUtil.WorkOnUI<object>(this, new Action(() =>
            {
                if (HFLiveEventIdNote.POSSIBLE_PENALTY.Equals(cid))
                {
                    PlayVoice("\\Resources\\Voice\\ke_neng_dian_qiu.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "可能点球"), Color.DarkOrange);
                    HandleAutoBetEvent(m, true, false);
                    new DialogNotify(m.League, String.Format("{0}(主) v.s. {1}(客)", m.Home, m.Away),
                        "【事件】可能点球").Show();
                }
                else if (HFLiveEventIdNote.POSSIBLE_PEN1_S.Equals(cid))
                {
                    PlayVoice("\\Resources\\Voice\\ke_neng_zhu_dui_zha_dan.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "可能主队炸弹"), Color.DarkOrange);
                    HandleAutoBetEvent(m, true, false);
                    ForceMessageDialog(m.League, m.Home, "", "主队可能炸弹");
                }
                else if (HFLiveEventIdNote.POSSIBLE_PEN2_S.Equals(cid))
                {
                    PlayVoice("\\Resources\\Voice\\ke_neng_ke_dui_zha_dan.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "可能客队炸弹"), Color.DarkOrange);
                    HandleAutoBetEvent(m, true, false);
                    ForceMessageDialog(m.League, "", m.Away, "客队可能炸弹");
                }
                else if ((HFLiveEventIdNote.PPEN1.Equals(cid)) || (HFLiveEventIdNote.PEN1.Equals(cid) && !info.Contains(HFLiveEventIdNote.CONFIRM)
                && !info.Contains(HFLiveEventIdNote.CANCEL)))
                {
                    PlayVoice("\\Resources\\Voice\\zhu_dui_ke_neng_dian_qiu.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "主队可能点球"), Color.DarkOrange);
                    HandleAutoBetEvent(m, true, false);
                    ForceMessageDialog(m.League, m.Home, "", "主队可能点球");
                }
                else if ((HFLiveEventIdNote.PPEN2.Equals(cid)) || (HFLiveEventIdNote.PEN2.Equals(cid) && !info.Contains(HFLiveEventIdNote.CONFIRM)
                && !info.Contains(HFLiveEventIdNote.CANCEL)))
                {
                    PlayVoice("\\Resources\\Voice\\ke_dui_ke_neng_dian_qiu.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "客队可能点球"), Color.DarkOrange);
                    HandleAutoBetEvent(m, true, false);
                    ForceMessageDialog(m.League, "", m.Away, "客队可能点球");
                }
                else if ((HFLiveEventIdNote.PEN1.Equals(cid) && info.EndsWith(HFLiveEventIdNote.CONFIRM))
                    || info.Contains("penaltyhome"))
                {
                    // 主队点球
                    PlayVoice("\\Resources\\Voice\\zhu_dui_dian_qiu.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "主队点球"), Color.Red);
                    HandleAutoBetEvent(m, true);
                    ForceMessageDialog(m.League, m.Home, "", "主队进行点球");
                }
                else if ((HFLiveEventIdNote.PEN2.Equals(cid) && info.EndsWith(HFLiveEventIdNote.CONFIRM))
                    || info.Contains("penaltyaway"))
                {
                    // 客队点球
                    PlayVoice("\\Resources\\Voice\\ke_dui_dian_qiu.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "客队点球"), Color.Red);
                    HandleAutoBetEvent(m, false);
                    ForceMessageDialog(m.League, "", m.Away, "客队进行点球");
                }
                else if (HFLiveEventIdNote.PEN1.Equals(cid) && info.EndsWith(HFLiveEventIdNote.CANCEL))
                {
                    PlayVoice("\\Resources\\Voice\\ke_neng_dian_qiu_qu_xiao.mp3");
                    // 主队点球取消
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "主队点球取消"), Color.Red);
                    ForceMessageDialog(m.League, m.Home, "", "主队取消点球");
                }
                else if (HFLiveEventIdNote.PEN2.Equals(cid) && info.EndsWith(HFLiveEventIdNote.CANCEL))
                {
                    // 客队点球取消
                    PlayVoice("\\Resources\\Voice\\ke_neng_dian_qiu_qu_xiao.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "客队点球取消"), Color.Red);
                    ForceMessageDialog(m.League, "", m.Away, "客队取消点球");
                }
                else if (HFLiveEventIdNote.MPEN1.Equals(cid))
                {
                    PlayVoice("\\Resources\\Voice\\zhu_dui_dian_qiu_shi_wu.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "主队点球失误"), Color.OliveDrab);
                    ForceMessageDialog(m.League, m.Home, "", "主队点球失误");
                }
                else if (HFLiveEventIdNote.MPEN2.Equals(cid))
                {
                    PlayVoice("\\Resources\\Voice\\ke_dui_dian_qiu_shi_wu.mp3");
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "客队点球失误"), Color.OliveDrab);
                    ForceMessageDialog(m.League, "", m.Away, "客队点球失误");
                }
                else if (HFLiveEventIdNote.GOAL_PEN1.Equals(cid) && info.EndsWith(HFLiveEventIdNote.CONFIRM))
                {
                    // 主队点球得分
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "主队点球得分"), Color.DarkRed);
                    ForceMessageDialog(m.League, m.Home, "", "主队点球得分");
                }
                else if (HFLiveEventIdNote.GOAL_PEN2.Equals(cid) && info.EndsWith(HFLiveEventIdNote.CONFIRM))
                {
                    // 客队点球得分
                    Output(String.Format(REGEX_FORMAT,
                        m.Score, m.Time, m.League, m.Home, m.Away, "客队点球得分"), Color.DarkRed);
                    ForceMessageDialog(m.League, "", m.Away, "客队点球得分");
                }
                else if (CB_MoreEvent.Checked)
                {
                    switch (cid)
                    {
                        case HFLiveEventIdNote.SAFE1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队控球"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.SAFE2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队控球"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.CFGOAL1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队进球得分"), Color.OrangeRed);
                            break;
                        case HFLiveEventIdNote.CFGOAL2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队进球得分"), Color.OrangeRed);
                            break;
                        case HFLiveEventIdNote.AT1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队发起进攻"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.AT2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队发起进攻"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.DANGER1:
                        case HFLiveEventIdNote.DAT1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队发起威胁性进攻"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.DANGER2:
                        case HFLiveEventIdNote.DAT2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队发起威胁性进攻"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.DFK1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队威胁性任意球机会"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.DFK2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队威胁性任意球机会"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.RC1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队受到红牌惩罚"), Color.OrangeRed);
                            break;
                        case HFLiveEventIdNote.RC2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队受到红牌惩罚"), Color.OrangeRed);
                            break;
                        case HFLiveEventIdNote.YRC1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队受到黄牌警告"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.YRC2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队受到黄牌警告"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.CYC_RC1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队取消红/黄牌惩罚"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.CYC_RC2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队取消红/黄牌惩罚"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.CR1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队获得角球机会"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.CR2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队获得角球机会"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.CCR1:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "主队角球机会取消"), Color.DarkGreen);
                            break;
                        case HFLiveEventIdNote.CCR2:
                            Output(String.Format(REGEX_FORMAT,
                                m.Score, m.Time, m.League, m.Home, m.Away, "客队角球机会取消"), Color.DarkGreen);
                            break;
                        default:
                            break;
                    }
                }
            }));
        }
        #endregion

        private void HandleAutoBetEvent(HFSimpleMatch m, bool isHome)
        {
            HandleAutoBetEvent(m, isHome, true);
        }

        /// <summary>
        /// 判断处理点球时的下注情况
        /// </summary>
        private void HandleAutoBetEvent(HFSimpleMatch m, bool isHome, bool showBet)
        {

            if (GlobalSetting.GetInstance().IsAutoBet)
            {

                // 修改为只有设置了自动下注才自动显示
                LogUtil.Write("此处出现添加任务情况!");
                if (showBet)
                {
                    BetMatchInfo info = new BetMatchInfo();
                    info.league = m.League;
                    info.home = m.Home;
                    info.away = m.Away;
                    info.isHomePen = isHome;
                    bool[] result = ContainsAccount(new int[] { AcccountType.XPJ155, AcccountType.XPJ469, AcccountType.YL5789 });
                    if (result[0])
                        BManager.Instance.Start(new X159BetTask(info));
                    if (result[1])
                        BManager.Instance.Start(new X469BetTask(info));
                    if (result[2])
                        BManager.Instance.Start(new YL5BetTask(info));
                }
            }

            // 将比赛名复制到
            string league;
            GlobalSetting setting = GlobalSetting.GetInstance();
            league = setting.GetMapValue(m.League);
            CommonUtil.Copy(league);
        }

        #region 获取Live数据方式1，需要回传获取中文数据的Cookie

        private void DoGetLiveDataAfterInitAndLogin(bool resetTimer)
        {
            RefreshH8TimerState(1);
            if (_h8Client == null || !_h8Client.IsH8Login)
            {
                DoInitAndLoginH8();
                return;
            }
            if (resetTimer)
            {
                _autoRefreshCuountdown = 0;
            }
            if (_h8DataTimer != null)
            {
                _h8DataTimer.Change(resetTimer? 0: 1000, 1000);
                return;
            }

            _h8DataTimer = ThreadUtil.RunOnTimer(
                (tobj) =>
                {
                    
                    if (_autoRefreshCuountdown > 0)
                    {
                        _autoRefreshCuountdown--;
                        RefreshBtn(0);
                        return;
                    }
                    if (GlobalSetting.GetInstance().IsAutoBet)
                    {
                        bool[] contains = ContainsAccount(new int[] { AcccountType.XPJ155, AcccountType.XPJ469, AcccountType.YL5789 });
                        if (contains[0])
                            BManager.Instance.Start(new X159ValidDataTask());
                        if (contains[1] || contains[2])
                            BManager.Instance.Start(new X469ValidDataTask());
                    }

                    _h8DataTimer.Change(Timeout.Infinite, 1000);
                    _autoRefreshCuountdown = GlobalSetting.GetInstance().AutoRefreshTime;
                    
                    _h8Client.GetOddData(
                        (matchData) =>
                        {
                            _isInErr = false;
                            RefreshH8TimerState(0);
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
                            RefreshH8TimerState(2);
                        },
                        (e) =>
                        {
                            RefreshH8TimerState(2);
                            LogUtil.Write(e);
                        });
                },
                null,
                0,
                1000);

        }

        public void DoInitAndLoginH8T(string sessionid, string auth)
        {
            ThreadUtil.RunOnThread(() =>
            {
                // 从服务器直接获取 h8 cookie，就可以不用登录了，每天cookie只会变动一次

                if (_h8Client == null)
                {
                    _h8Client = new HFClient(null, null);
                }
                _h8Client.IsH8Login = true;
                _h8Client.AddH8Cookie(sessionid, auth);
                DoGetLiveDataAfterInitAndLogin(true);
            });
        }
        #endregion

        #region 获取Live数据方式2，需要登录账号
        public void DoInitAndLoginH8()
        {
            ThreadUtil.RunOnThread(() =>
            {

                LogUtil.Write("开始执行");
                if (_h8Client == null)
                {
                    string[] key = GlobalSetting.GetInstance().HFAccount.Split(':');
                    _h8Client = new HFClient(key[0], key[1]);
                }
                _h8Client.Login(
                    (data) =>
                      {
                          LogUtil.Write("登录成功!");
                          _h8Client.LoginForH8((d) =>
                              {
                                  LogUtil.Write("H8登录成功!");
                                  DoGetLiveDataAfterInitAndLogin(true);
                              },
                              (status, code, msg) =>
                              {
                                  RefreshH8TimerState(2);
                                  LogUtil.Write("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                              },
                              (e) =>
                              {
                                  RefreshH8TimerState(2);
                                  LogUtil.Write(e.Message);
                              });
                      },
                      (status, code, msg) =>
                      {
                          RefreshH8TimerState(2);
                          LogUtil.Write("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                      },
                      (e) =>
                      {
                          RefreshH8TimerState(2);
                          LogUtil.Write(e.Message);
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
            if (CLB_XPJUser.SelectedIndices != null && CLB_XPJUser.SelectedIndices.Count > 0)
            {
                
                for (int index = CLB_XPJUser.SelectedIndices.Count - 1; index >= 0; index--)
                {
                    _xpjUserDict.RemoveAt(index);
                    CLB_XPJUser.Items.RemoveAt(index);
                }
            }
        }

        private void TSMI_File_Setting_Click(object sender, EventArgs e)
        {
            FormSetting setting = new FormSetting();
            if (setting.ShowDialog() == DialogResult.OK)
            {
                BTN_Refresh_Click(sender, e);
            }
        }

        private void TSMI_Live_CheckAll_Click(object sender, EventArgs e)
        {
            if (_midList.Count == 0)
                return;

            
            if (_uncheckedList.Count == 0)
            {
                foreach (DataGridViewRow r in DGV_Live.Rows)
                {
                    r.Cells[6].Value = false;
                }
                _uncheckedList.Clear();
                _uncheckedList.AddRange(_midList);
            }
            else
            {
                foreach (DataGridViewRow r in DGV_Live.Rows)
                {
                    r.Cells[6].Value = true;
                }
                _uncheckedList.Clear();
            }
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            _autoRefreshCuountdown = GlobalSetting.GetInstance().AutoRefreshTime;
            DoGetLiveDataAfterInitAndLogin(true);
        }
        
        private void CB_IsAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            BTN_Refresh_Click(sender, e);
        }
        
        private void BTN_JumpBet_Click(object sender, EventArgs e)
        {
            int index = CLB_XPJUser.SelectedIndex;
            if (index > -1)
            {
                IntegratedAccount account = _xpjUserDict[index];
                switch (account.Type)
                {
                    case AcccountType.XPJ155:
                        FormInfo.NewInstance().Show(account.GetClient<XPJClient>());
                        break;
                    case AcccountType.XPJ469:
                        FormX469Info.NewInstance().Show(account.GetClient<X469Client>());
                        break;
                    case AcccountType.YL5789:
                        FormYL5Info.NewInstance().Show(account.GetClient<YL5Client>());
                        break;
                    default:
                        MessageBox.Show("对不起，此类型网站暂不支持UI显示，仅限于自动下注");
                        break;
                }
            }
        }
        #endregion
        

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            LiveThreadManager.Instance.Stop();
            BManager.Instance.Stop();
            SaveAccounts();
            GlobalSetting.GetInstance().Save();
        }

        private void CLB_XPJUser_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (HasAccount())
                _xpjUserDict[e.Index].IsChecked = (e.NewValue == CheckState.Checked);
        }

        private void TSMI_AddCookie_Click(object sender, EventArgs e)
        {
            DialogAddH8Cookie dialog = new DialogAddH8Cookie();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.IsAccount)
                {
                    _h8Client = null;
                    DoInitAndLoginH8();
                }
                else
                {
                    DoInitAndLoginH8T(dialog.UserOrSession, dialog.PwdOrAuth);
                }
            }
        }

        public void Output(string content, Color fontColor)
        {
            ThreadUtil.WorkOnUI<object>(this, new Action(() =>
            {
                int p1 = RTB_Output.Text.Length;
                content = String.Format("[{0}] {1}\n", DateTime.Now.ToString("HH:mm:ss"), content);
                RTB_Output.AppendText(content);
                RTB_Output.Select(p1, content.Length);
                RTB_Output.SelectionColor = fontColor;
                RTB_Output.Refresh();
                RTB_Output.ScrollToCaret();
            }));
        }

        private void TSMI_Output_Clear_Click(object sender, EventArgs e)
        {
            RTB_Output.Clear();
        }

        private void ForceMessageBox(string msg)
        {
            this.TopMost = true;
            MessageBox.Show(msg, "点球提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.TopMost = false;
        }

        private void ForceMessageDialog(string league, string home, string away, string ev)
        {
            new DialogNotify(league, String.IsNullOrEmpty(home) ?
                String.Format("{0}（客队）", away): String.Format("{0}（主队）", home),
                String.Format("【事件】{0}", ev)).Show();
        } 

        private void RefreshBtn(int state)
        {
            ThreadUtil.WorkOnUI(this, new Action<int>((s) =>
            {

                switch (s)
                {
                    case 0:
                        BTN_Refresh.Enabled = true;
                        BTN_Refresh.Text = String.Format("刷新：{0}s", _autoRefreshCuountdown);
                        break;
                    case 1:
                        BTN_Refresh.Enabled = false;
                        BTN_Refresh.Text = "刷新中...";
                        break;
                    case 2:
                        BTN_Refresh.Enabled = true;
                        BTN_Refresh.Text = "出错，手动立即刷新";
                        break;
                }
            }),
            state);
        }

        private void RefreshH8TimerState(int state)
        {
            switch(state)
            {
                case 0:
                    if (_h8DataTimer != null)
                        _h8DataTimer.Change(1000, 1000);
                    if (!_isInErr)
                        RefreshBtn(0);
                    break;
                case 1:
                    if (_h8DataTimer != null)
                        _h8DataTimer.Change(Timeout.Infinite, 1000);
                    RefreshBtn(1);
                    break;
                case 2:
                    RefreshBtn(2);
                    _isInErr = true;
                    break;
            }
        }

        private void RefreshLiveTimerState(int state)
        {
            switch (state)
            {
                case 0:
                    if (_liveTimer != null)
                    {
                        _liveTimer.Change(Timeout.Infinite, 1000);
                    }
                    break;
                case 1:
                    if (_liveTimer != null)
                        _liveTimer.Change(1000, 1000);
                    break;
            }
        }

        private void TSMI_File_Map_Click(object sender, EventArgs e)
        {
            FormMapItem.NewInstance().Show();
        }

        private void DGV_Live_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridViewCell cell = DGV_Live.Rows[e.RowIndex].Cells[e.ColumnIndex];
                CommonUtil.Copy(cell.Value.ToString());
            }
        }

        private void RTB_Output_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CommonUtil.SelectText(RTB_Output);
        }

        private void TSMI_Tool_Map_Click(object sender, EventArgs e)
        {

        }

        private bool _canScroll = true;
        private void RTB_Output_MouseEnter(object sender, EventArgs e)
        {
            _canScroll = false;
        }

        private void RTB_Output_MouseLeave(object sender, EventArgs e)
        {
            _canScroll = true;
        }

        private void DGV_Live_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 6)
            {
                DataGridViewCell cell = DGV_Live.Rows[e.RowIndex].Cells[e.ColumnIndex];
                bool after = (bool)cell.EditedFormattedValue;
                string key = _midList[e.RowIndex];

                if (after)
                {
                    _uncheckedList.Remove(key);
                }
                else
                {
                    _uncheckedList.Add(key);
                }
            }
        }
    }
}
