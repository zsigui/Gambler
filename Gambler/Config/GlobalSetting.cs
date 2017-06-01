using Gambler.Utils;
using System;
using System.Collections.Generic;

namespace Gambler.Config
{
    /// <summary>
    /// 存放全局配置
    /// </summary>
    public class GlobalSetting
    {
        private static GlobalSetting sInstance = new GlobalSetting();

        public static GlobalSetting GetInstance()
        {
            return sInstance;
        }

        private GlobalSetting()
        {
            Initital();
        }

        private static readonly string SETTING_PATH = System.Windows.Forms.Application.StartupPath + "//Config";
        private readonly string AUTO_REFRESH_TIME = "autoRefreshTime";
        private readonly string IS_AUTO_SAVE = "isAutoSaveUser";
        private readonly string IS_AUTO_BET = "isAutoBet";
        private readonly string IS_SHOW_BET_DIALOG = "isShowBetDialog";
        private readonly string BET_TYPE = "betType";
        private readonly string BET_MONEY_LEAST = "leastBetMoney";
        private readonly string BET_MONEY_MOST = "mostBetMoney";
        private readonly string BET_METHOD = "betMethod";

        private Dictionary<string, object> _settings;

        /// <summary>
        /// 默认初始化时调用
        /// </summary>
        private void Initital()
        {
            Load();
        }

        /// <summary>
        /// 从文件中加载设置属性
        /// </summary>
        public void Load()
        {
            string content = FileUtil.ReadContentFromFilePath(SETTING_PATH);
            if (!String.IsNullOrEmpty(content))
            {
                _settings = JsonUtil.fromJson<Dictionary<string, object>>(content);
            }

            // 保证 settings 调用不为空
            if (_settings == null)
            {
                _settings = new Dictionary<string, object>();
                _settings.Add(AUTO_REFRESH_TIME, null);
                _settings.Add(IS_AUTO_SAVE, null);
                _settings.Add(IS_AUTO_BET, null);
                _settings.Add(IS_SHOW_BET_DIALOG, null);
                _settings.Add(BET_TYPE, null);
                _settings.Add(BET_MONEY_LEAST, null);
                _settings.Add(BET_MONEY_MOST, null);
                _settings.Add(BET_METHOD, null);
            }
        }

        /// <summary>
        /// 将设置属性保存到文件中
        /// </summary>
        public void Save()
        {
            try
            {
                string content = JsonUtil.toJson(_settings);
                FileUtil.WriteContentToFilePath(SETTING_PATH, content);
            }
            catch
            (Exception e)
            {
                LogUtil.Write(e);
            }
        }

        #region 属性
        /// <summary>
        /// 直播赛事列表页面的自动刷新间隔时间（单位：秒），默认10
        /// </summary>
        public int AutoRefreshTime
        {
            get
            {
                object o = _settings[AUTO_REFRESH_TIME];
                return o == null ? 10 : (int)o;
            }
            set
            {
                _settings[AUTO_REFRESH_TIME] = value;
            }
        }

        /// <summary>
        /// 是否自动保存添加用户，默认自动保存
        /// </summary>
        /// <returns></returns>
        public bool IsAutoSaveUser
        {
            get {
                object o = _settings[IS_AUTO_SAVE];
                return o == null || (bool)o;
            }
            set
            {
                _settings[IS_AUTO_SAVE] = value;
            }
        }

        /// <summary>
        /// 是否自动下注，默认否
        /// </summary>
        public bool IsAutoBet
        {
            get
            {
                object o = _settings[IS_AUTO_BET];
                return o != null && (bool)o;
            }
            set
            {
                _settings[IS_AUTO_BET] = value;
            }
        }

        /// <summary>
        /// 是否显示下注确认框，默认是
        /// </summary>
        public bool IsShowBetDialog
        {
            get
            {
                object o = _settings[IS_SHOW_BET_DIALOG];
                return o == null || (bool)o;
            }
            set
            {
                _settings[IS_SHOW_BET_DIALOG] = value;
            }
        }

        /// <summary>
        /// 自动下注时启动，最大下注金额，单位：元
        /// 注意当比最小下注金额低时，视同等于最小下注金额
        /// </summary>
        public int MostBetMoney
        {
            get
            {
                object o = _settings[BET_MONEY_MOST];
                return o == null ? 50 : (int)o;
            }
            set
            {
                _settings[BET_MONEY_MOST] = value;
            }
        }

        /// <summary>
        /// 自动下注时启动，最小下注金额，单位：元
        /// </summary>
        public int LeastBetMoney
        {
            get
            {
                object o = _settings[BET_MONEY_LEAST];
                return o == null ? 50 : (int)o;
            }
            set
            {
                _settings[BET_MONEY_LEAST] = value;
            }
        }

        /// <summary>
        /// 自动下注时启动，下注类型
        /// </summary>
        public BetType GameBetType
        {
            get
            {
                
                object o = _settings[BET_TYPE];
                return o == null ? BetType.BigOrSmall : (BetType)o;
            }
            set
            {
                _settings[BET_TYPE] = value;
            }
        }

        /// <summary>
        /// 自动下注时启用，判断事件触发时的下注行为方式
        /// </summary>
        public BetMethod GameBetMethod
        {
            get
            {
                object o = _settings[BET_METHOD];
                return o == null ? BetMethod.EveryTime : (BetMethod)o;
            }
            set
            {
                _settings[BET_METHOD] = value;
            }
        }
        #endregion

    }

    #region BetMethod
    /// <summary>
    /// 触发事件后自动下注的方式
    /// </summary>
    public enum BetMethod
    {
        /// <summary>
        /// 每次下注
        /// </summary>
        EveryTime = 1,

        /// <summary>
        /// 随机判断是否下注
        /// </summary>
        Random = 2
    }
    #endregion

    #region BetType
    /// <summary>
    /// 自动下注的类型，暂时先只支持大小
    /// </summary>
    public enum BetType
    {
        
        /// <summary>
        /// 单双
        /// </summary>
        OddOrEven = 1,

        /// <summary>
        /// 大小 - 全场
        /// </summary>
        BigOrSmall = 2,

        /// <summary>
        /// 让球 - 全场
        /// </summary>
        ConcedePoints = 3,

        /// <summary>
        /// 独赢 - 全场
        /// </summary>
        Capot = 4,

        /// <summary>
        /// 大小 - 半场
        /// </summary>
        HalfBigOrSmall = 5,

        /// <summary>
        /// 让球 - 半场
        /// </summary>
        HalfConcedePoints = 6,

        /// <summary>
        /// 独赢 - 半场
        /// </summary>
        HalfCapot = 7,
    }
    #endregion
}
