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
    public partial class DialogNotify : Form
    {
        public DialogNotify(string top, string middle, string bottom)
        {
            InitializeComponent();
            TB_Top.Text = top;
            TB_Middle.Text = middle;
            TB_Bottom.Text = bottom;
        }

        private void BTN_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TB_Middle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CommonUtil.SelectText(TB_Middle);
            CommonUtil.Copy(TB_Middle.SelectedText);
        }

        private void TB_Top_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CommonUtil.SelectText(TB_Top);
            CommonUtil.Copy(TB_Top.SelectedText);
        }

        private void TB_Bottom_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CommonUtil.SelectText(TB_Bottom);
        }
    }
}
