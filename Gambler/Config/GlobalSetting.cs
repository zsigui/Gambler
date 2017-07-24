using Gambler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
        }

        public static readonly string CONFIG_PATH = System.Windows.Forms.Application.StartupPath + "//Config";
        public static readonly string SETTING_PATH = CONFIG_PATH + "//settings.txt";
        public static readonly string XPJ_USER_PATH = CONFIG_PATH + "//user_xpj.txt";
        public static readonly string MAP_PATH = CONFIG_PATH + "//map.txt";

        private readonly string AUTO_REFRESH_TIME = "autoRefreshTime";
        private readonly string IS_AUTO_SAVE = "isAutoSaveUser";
        private readonly string IS_AUTO_BET = "isAutoBet";
        private readonly string IS_SHOW_BET_DIALOG = "isShowBetDialog";
        private readonly string BET_TYPE = "betType";
        private readonly string SECOND_BET_TYPE = "secondBetType";
        private readonly string BET_MONEY = "betMoney";
        private readonly string AUTO_BET_RATE = "autoBetRate";
        private readonly string BET_METHOD = "betMethod";
        private readonly string IS_SHOW_HALF_ODD_FIRST = "isShowHalfOddFirst";
        private readonly string IS_AUTO_ACCEPT_BEST_ODD = "isAutoAcceptBestOdd";
        private readonly string BET_BEHAVIOR = "betBehavior";
        private readonly string HF_ACCOUNT = "hfAccount";
        /// <summary>
        /// 首选映射组的键名
        /// </summary>
        private readonly string FIRST_COPY_MAP = "firstCopyMap";

        public const string X469_KEY = "469355.com";
        public const string X159_KEY = "1559501.com";
        public const string YL5_KEY = "yl5789.com";
        private Dictionary<string, object> _settings;


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
                _settings.Add(SECOND_BET_TYPE, null);
                _settings.Add(BET_MONEY, null);
                _settings.Add(BET_METHOD, null);
                _settings.Add(IS_SHOW_HALF_ODD_FIRST, null);
                _settings.Add(IS_AUTO_ACCEPT_BEST_ODD, null);
                _settings.Add(HF_ACCOUNT, null);
                _settings.Add(AUTO_BET_RATE, null);
                _settings.Add(FIRST_COPY_MAP, X469_KEY);
                _settings.Add(BET_BEHAVIOR, 0);
            }
            LoadMap();
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
                SaveMap();
            }
            catch
            (Exception e)
            {
                LogUtil.Write(e);
            }
        }

        #region 属性
        /// <summary>
        /// 直播赛事列表页面的自动刷新间隔时间（单位：秒），默认60
        /// </summary>
        public int AutoRefreshTime
        {
            get
            {
                object o = _settings[AUTO_REFRESH_TIME];
                return o == null ? 60 : Convert.ToInt32(o);
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
            get
            {
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
        /// 自动下注时启动，下注金额，单位：元
        /// </summary>
        public int BetMoney
        {
            get
            {
                object o = _settings[BET_MONEY];
                return o == null ? 10 : Convert.ToInt32(o);
            }
            set
            {
                _settings[BET_MONEY] = value;
            }
        }

        /// <summary>
        /// 自动下注的最低倍率限制，即是低于此倍率无效
        /// </summary>
        public float AutoBetRate
        {
            get
            {
                object o = _settings[AUTO_BET_RATE];
                return o == null ? 1.0f : (float)Convert.ToDouble(o);
            }
            set
            {
                _settings[AUTO_BET_RATE] = value;
            }
        }

        public int BetBehavior
        {
            get
            {
                if (!_settings.ContainsKey(BET_BEHAVIOR))
                {
                    _settings.Add(BET_BEHAVIOR, 0);
                }
                object o = _settings[BET_BEHAVIOR];
                return Convert.ToInt32(o);
            }
            set
            {
                _settings[AUTO_BET_RATE] = value;
            }
        }

        /// <summary>
        /// 自动下注时启动，首选下注类型
        /// </summary>
        public BetType GameBetType
        {
            get
            {

                object o = _settings[BET_TYPE];
                return o == null ? BetType.ConcedePoints : (BetType)(Enum.ToObject(typeof(BetType), o));
            }
            set
            {
                _settings[BET_TYPE] = value;
            }
        }

        /// <summary>
        /// 自动下注时启动，下注类型
        /// </summary>
        public BetType SecondBetType
        {
            get
            {

                object o = _settings[SECOND_BET_TYPE];
                return o == null ? BetType.None : (BetType)(Enum.ToObject(typeof(BetType), o));
            }
            set
            {
                _settings[SECOND_BET_TYPE] = value;
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
                return o == null ? BetMethod.EveryTime : (BetMethod)(Enum.ToObject(typeof(BetMethod), o));
            }
            set
            {
                _settings[BET_METHOD] = value;
            }
        }

        /// <summary>
        /// 显示赔率列表时是否显示为半场赔率
        /// </summary>
        public bool IsShowHalfOddFirst
        {
            get
            {
                object o = _settings[IS_SHOW_HALF_ODD_FIRST];
                return o != null && (bool)o;
            }
            set
            {
                _settings[IS_SHOW_HALF_ODD_FIRST] = value;
            }
        }

        /// <summary>
        /// 是否自动接受最佳赔率
        /// </summary>
        public bool IsAutoAcceptBestOdd
        {
            get
            {
                object o = _settings[IS_AUTO_ACCEPT_BEST_ODD];
                return o == null || (bool)o;
            }
            set
            {
                _settings[IS_AUTO_ACCEPT_BEST_ODD] = value;
            }
        }

        public string HFAccount
        {
            get
            {
                object o = _settings[IS_AUTO_ACCEPT_BEST_ODD];
                return (o == null || o.Equals("")) ? "kaokkyyzz:kaokkyyzz" : (string)o;
            }
            set
            {
                _settings[IS_AUTO_ACCEPT_BEST_ODD] = value;
            }
        }

        public string FirstMapKey
        {
            get
            {
                object o = _settings[FIRST_COPY_MAP];
                return o == null ? X469_KEY : (string)o;
            }
            set
            {
                _settings[FIRST_COPY_MAP] = value;
            }
        }
        #endregion


        #region 名称映射

        /// <summary>
        /// 进行名称映射
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> _mapDict;

        private void LoadMap()
        {
            string content = FileUtil.ReadContentFromFilePath(MAP_PATH);
            if (!String.IsNullOrEmpty(content))
                _mapDict = JsonUtil.fromJson<Dictionary<string, Dictionary<string, string>>>(content);

            if (_mapDict == null)
            {
                _mapDict = new Dictionary<string, Dictionary<string, string>>();
            }
            if (!_mapDict.ContainsKey(X469_KEY))
            {
                _mapDict.Clear();
                _mapDict.Add(X469_KEY, new Dictionary<string, string>());
                _mapDict.Add(X159_KEY, new Dictionary<string, string>());
                _mapDict.Add(YL5_KEY, new Dictionary<string, string>());
            }
        }

        private void SaveMap()
        {

            string content = JsonUtil.toJson(_mapDict);
            FileUtil.WriteContentToFilePath(MAP_PATH, content);
        }

        public Dictionary<string, Dictionary<string, string>> MapItemDict
        {
            get { return _mapDict; }
        }

        public void AddItemKey(string key)
        {
            if (!_mapDict.ContainsKey(key))
            {
                _mapDict.Add(key, null);
            }
        }

        public void AddNewMapItem(string itemKey, Dictionary<string, string> item)
        {
            AddItemKey(itemKey);
            _mapDict[itemKey] = item;
        }

        public void AddNewMapItem(string itemKey, string key, string val)
        {
            AddItemKey(itemKey);
            Dictionary<string, string> tmp = _mapDict[itemKey];
            if (tmp == null)
            {
                tmp = new Dictionary<string, string>();
                _mapDict[itemKey] = tmp;
            }
            if (tmp.ContainsKey(key))
            {
                tmp[key] = val;
            }
            else
            {
                tmp.Add(key, val);
            }

        }

        public Dictionary<string, string> GetMapItem(string itemKey)
        {
            if (String.IsNullOrEmpty(itemKey))
                return null;
            Dictionary<string, string> tmpKeyVal;
            if (_mapDict.TryGetValue(itemKey, out tmpKeyVal))
            {
                return tmpKeyVal;
            }
            return null;
        }

        public string GetMapValue(string itemKey, string key)
        {
            if (String.IsNullOrEmpty(itemKey))
                return key;
            Dictionary<string, string> tmpKeyVal;
            if (_mapDict.TryGetValue(itemKey, out tmpKeyVal))
            {
                string val;
                if (tmpKeyVal.TryGetValue(key, out val))
                {
                    return val;
                }
            }
            return key;
        }

        /// <summary>
        /// 尝试获取映射表中的映射值
        /// </summary>
        /// <param name="key"></param>
        /// <returns>当获取不到映射值，返回传入的key值</returns>
        public string GetMapValue(string key)
        {
            return GetMapValue(FirstMapKey, key);
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

        None,

        /// <summary>
        /// 大小 - 全场
        /// </summary>
        BigOrSmall,

        /// <summary>
        /// 让球 - 全场
        /// </summary>
        ConcedePoints,

        /// <summary>
        /// 独赢 - 全场
        /// </summary>
        Capot,

        /// <summary>
        /// 大小 - 半场
        /// </summary>
        HalfBigOrSmall,

        /// <summary>
        /// 让球 - 半场
        /// </summary>
        HalfConcedePoints,

        /// <summary>
        /// 独赢 - 半场
        /// </summary>
        HalfCapot,
    }
    #endregion

    public class BetBehavior
    {
        public const int BOTH = 0;

        public const int FIRST_PEN = 1;

        public const int FIRST_TEAM = 2;
    }
}
