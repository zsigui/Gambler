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
    public partial class FormInfo : Form
    {

        public static FormInfo sInstance;

        public static FormInfo newInstance()
        {
            if (sInstance == null)
            {
                sInstance = new FormInfo();
            } else
            {
                sInstance.Focus();
            }
            return sInstance;
        }

        public FormInfo()
        {
            InitializeComponent();
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            
        }

        private void BTN_Odd_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Even_Click(object sender, EventArgs e)
        {

        }

        private void BTN_BigOrSmall_Host_Click(object sender, EventArgs e)
        {

        }

        private void BTN_BigOrSmall_Away_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Capot_Host_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Capot_None_Click(object sender, EventArgs e)
        {

        }

        private void BTN_Capot_Away_Click(object sender, EventArgs e)
        {

        }

        private void BTN_ConcedePoints_Host_Click(object sender, EventArgs e)
        {

        }

        private void BTN_ConcedePoints_Away_Click(object sender, EventArgs e)
        {

        }

        private void FormInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            sInstance = null;
        }
    }
}
