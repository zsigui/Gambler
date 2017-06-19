using Gambler.Config;
using Gambler.Utils;
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
    public partial class FormMapItem : Form
    {

        private static FormMapItem sInstance;

        public static FormMapItem NewInstance()
        {
            if (sInstance == null)
            {
                sInstance = new FormMapItem();
            }
            else
            {
                sInstance.Focus();
            }
            return sInstance;
        }


        public FormMapItem()
        {
            InitializeComponent();

            Init();
        }

        private void SetNoDataUI()
        {
            CB_ItemKey.Text = "暂无";
            RTB_MapItems.Text = "";
            RTB_MapItems.Enabled = false;
            CB_ItemKey.Enabled = false;
            BTN_DelMapItem.Enabled = false;
        }

        private void ResetTextUI(Dictionary<string, string> dict)
        {

            RTB_MapItems.Text = "";
            if (dict != null)
            {
                foreach (KeyValuePair<string, string> pair in dict)
                {
                    RTB_MapItems.AppendText(String.Format("{0}={1}\n", pair.Key, pair.Value));
                }
            }
            BTN_DelMapItem.Enabled = true;
            RTB_MapItems.Enabled = true;
            BTN_Save.Enabled = true;
        }

        private void Init()
        {
            Dictionary<string, Dictionary<string, string>> mapDict = GlobalSetting.GetInstance().MapItemDict;
            CB_ItemKey.Items.Clear();
            if (mapDict.Keys.Count > 0)
            {
                string selKey = GlobalSetting.GetInstance().MapItemKey;
                int i = -1;
                int selIndex = -1;
                foreach (string key in mapDict.Keys)
                {
                    if (String.IsNullOrEmpty(key))
                        continue;

                    i++;
                    CB_ItemKey.Items.Add(key);
                    if (key.Equals(selKey))
                    {
                        selIndex = i;
                    }
                }
                if (CB_ItemKey.Items.Count == 0)
                {
                    SetNoDataUI();
                    return;
                }

                if (selIndex == -1) {
                    CB_ItemKey.SelectedIndex = 0;
                    selKey = CB_ItemKey.SelectedItem.ToString();
                }
                else
                {
                    CB_ItemKey.SelectedIndex = selIndex;
                }
                CB_ItemKey.Enabled = true;
                RTB_MapItems.Enabled = true;
                Dictionary<string, string> dict = mapDict[selKey];
                ResetTextUI(dict);
                
            }
            else
            {
                SetNoDataUI();

            }

        }

        private void BTN_AddMapItem_Click(object sender, EventArgs e)
        {
            string itemKey = TB_MapKeyItem.Text;
            if (String.IsNullOrEmpty(itemKey))
            {
                MessageBox.Show("请输入新映射组的组名");
                return;
            }
            GlobalSetting.GetInstance().AddItemKey(itemKey);
            int index = CB_ItemKey.Items.Add(itemKey);
            CB_ItemKey.SelectedIndex = index;
            CB_ItemKey.Enabled = true;
            TB_MapKeyItem.Text = "";
        }

        private void BTN_DelMapItem_Click(object sender, EventArgs e)
        {
            if (CB_ItemKey.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要删除的组别");
                return;
            }
            string delItem = CB_ItemKey.SelectedItem.ToString();
            int selIndex = CB_ItemKey.SelectedIndex;
            CB_ItemKey.Items.RemoveAt(selIndex);
            GlobalSetting.GetInstance().DelItemKey(delItem);
            if (CB_ItemKey.Items.Count == 0)
            {
                SetNoDataUI();
            }
            else
            {
                CB_ItemKey.SelectedIndex = 0;
            }
        }

        private void BTN_Save_Click(object sender, EventArgs e)
        {
            string content = RTB_MapItems.Text;
            string itemKey = CB_ItemKey.SelectedItem.ToString();
            LogUtil.Write("content = " + RTB_MapItems.Text + ", selInde = " + CB_ItemKey.SelectedIndex + ", SelItem = " + CB_ItemKey.SelectedItem.ToString());
            if (!String.IsNullOrEmpty(content) && !String.IsNullOrEmpty(itemKey))
            {
                Dictionary<string, string> item = new Dictionary<string, string>();
                string[] items = content.Split('\n');
                string[] keyVal;
                foreach(string it in items)
                {
                    if (String.IsNullOrEmpty(it))
                        continue;
                    keyVal = it.Split('=');
                    if (keyVal.Length != 2)
                        continue;
                    if (!item.ContainsKey(keyVal[0]))
                    {
                        item.Add(keyVal[0], keyVal[1]);
                    }
                    else
                    {
                        item[keyVal[0]] = keyVal[1];
                    }
                }
                GlobalSetting.GetInstance().AddNewMapItem(itemKey, item);
                MessageBox.Show("保存成功!");
            }
            
        }

        private void CB_ItemKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ItemKey.SelectedIndex != -1)
            {
                GlobalSetting.GetInstance().MapItemKey = CB_ItemKey.SelectedItem.ToString();
                Dictionary<string, string> tmp = GlobalSetting.GetInstance().GetMapItem(CB_ItemKey.SelectedItem.ToString());
                ResetTextUI(tmp);
            }
            else
            {
                RTB_MapItems.Text = "";
                CB_ItemKey.Text = "";
                RTB_MapItems.Enabled = false;
                BTN_Save.Enabled = false;
                BTN_DelMapItem.Enabled = false;
            }
        }

        private void FormMapItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            sInstance = null;
        }
    }
}
