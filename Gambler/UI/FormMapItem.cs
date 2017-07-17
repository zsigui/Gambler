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
            RTB_MapItems.Enabled = true;
            BTN_Save.Enabled = true;
        }

        private void Init()
        {
            CB_ItemKey.SelectedIndex = 0;
            string key = GlobalSetting.GetInstance().FirstMapKey;
            if (!String.IsNullOrEmpty(key))
            {
                if (key.Equals(CB_ForCopy.Items[0].ToString()))
                {
                    CB_ForCopy.SelectedIndex = 0;
                }
                else if (key.Equals(CB_ForCopy.Items[1].ToString()))
                {
                    CB_ForCopy.SelectedIndex = 1;
                }
                else
                {
                    CB_ForCopy.SelectedIndex = 2;
                }
            }
        }
        
        private void BTN_Save_Click(object sender, EventArgs e)
        {
            string content = RTB_MapItems.Text;
            string itemKey = CB_ItemKey.SelectedItem.ToString();
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
                Dictionary<string, string> tmp = GlobalSetting.GetInstance().GetMapItem(CB_ItemKey.SelectedItem.ToString());
                ResetTextUI(tmp);
            }
            else
            {
                RTB_MapItems.Text = "";
                CB_ItemKey.Text = "";
                RTB_MapItems.Enabled = false;
                BTN_Save.Enabled = false;
            }
        }

        private void FormMapItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            sInstance = null;
        }

        private void CB_ForCopy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_ForCopy.SelectedIndex > -1)
                GlobalSetting.GetInstance().FirstMapKey = CB_ForCopy.SelectedItem.ToString();
        }
    }
}
