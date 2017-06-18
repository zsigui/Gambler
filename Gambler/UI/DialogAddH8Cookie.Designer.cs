namespace Gambler.UI
{
    partial class DialogAddH8Cookie
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TB_Session = new System.Windows.Forms.TextBox();
            this.TB_Auth = new System.Windows.Forms.TextBox();
            this.BTN_Confirm = new System.Windows.Forms.Button();
            this.RB_Cookie = new System.Windows.Forms.RadioButton();
            this.RB_Account = new System.Windows.Forms.RadioButton();
            this.LB_Auth = new System.Windows.Forms.Label();
            this.LB_Session = new System.Windows.Forms.Label();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TB_Session
            // 
            this.TB_Session.Location = new System.Drawing.Point(27, 38);
            this.TB_Session.Name = "TB_Session";
            this.TB_Session.Size = new System.Drawing.Size(164, 21);
            this.TB_Session.TabIndex = 0;
            // 
            // TB_Auth
            // 
            this.TB_Auth.Location = new System.Drawing.Point(27, 98);
            this.TB_Auth.Name = "TB_Auth";
            this.TB_Auth.Size = new System.Drawing.Size(164, 21);
            this.TB_Auth.TabIndex = 1;
            // 
            // BTN_Confirm
            // 
            this.BTN_Confirm.Location = new System.Drawing.Point(116, 159);
            this.BTN_Confirm.Name = "BTN_Confirm";
            this.BTN_Confirm.Size = new System.Drawing.Size(75, 23);
            this.BTN_Confirm.TabIndex = 2;
            this.BTN_Confirm.Text = "确认";
            this.BTN_Confirm.UseVisualStyleBackColor = true;
            this.BTN_Confirm.Click += new System.EventHandler(this.BTN_Confirm_Click);
            // 
            // RB_Cookie
            // 
            this.RB_Cookie.AutoSize = true;
            this.RB_Cookie.Location = new System.Drawing.Point(29, 131);
            this.RB_Cookie.Name = "RB_Cookie";
            this.RB_Cookie.Size = new System.Drawing.Size(77, 16);
            this.RB_Cookie.TabIndex = 3;
            this.RB_Cookie.TabStop = true;
            this.RB_Cookie.Text = "H8 Cookie";
            this.RB_Cookie.UseVisualStyleBackColor = true;
            // 
            // RB_Account
            // 
            this.RB_Account.AutoSize = true;
            this.RB_Account.Location = new System.Drawing.Point(122, 131);
            this.RB_Account.Name = "RB_Account";
            this.RB_Account.Size = new System.Drawing.Size(71, 16);
            this.RB_Account.TabIndex = 4;
            this.RB_Account.TabStop = true;
            this.RB_Account.Text = "鸿发账号";
            this.RB_Account.UseVisualStyleBackColor = true;
            this.RB_Account.CheckedChanged += new System.EventHandler(this.RB_Account_CheckedChanged);
            // 
            // LB_Auth
            // 
            this.LB_Auth.AutoSize = true;
            this.LB_Auth.Location = new System.Drawing.Point(27, 74);
            this.LB_Auth.Name = "LB_Auth";
            this.LB_Auth.Size = new System.Drawing.Size(41, 12);
            this.LB_Auth.TabIndex = 5;
            this.LB_Auth.Text = "密码：";
            // 
            // LB_Session
            // 
            this.LB_Session.AutoSize = true;
            this.LB_Session.Location = new System.Drawing.Point(25, 14);
            this.LB_Session.Name = "LB_Session";
            this.LB_Session.Size = new System.Drawing.Size(53, 12);
            this.LB_Session.TabIndex = 6;
            this.LB_Session.Text = "用户名：";
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Location = new System.Drawing.Point(27, 159);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BTN_Cancel.TabIndex = 7;
            this.BTN_Cancel.Text = "取消";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // DialogAddH8Cookie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 194);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.LB_Session);
            this.Controls.Add(this.LB_Auth);
            this.Controls.Add(this.RB_Account);
            this.Controls.Add(this.RB_Cookie);
            this.Controls.Add(this.BTN_Confirm);
            this.Controls.Add(this.TB_Auth);
            this.Controls.Add(this.TB_Session);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogAddH8Cookie";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DialogAddH8Cookie";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Session;
        private System.Windows.Forms.TextBox TB_Auth;
        private System.Windows.Forms.Button BTN_Confirm;
        private System.Windows.Forms.RadioButton RB_Cookie;
        private System.Windows.Forms.RadioButton RB_Account;
        private System.Windows.Forms.Label LB_Auth;
        private System.Windows.Forms.Label LB_Session;
        private System.Windows.Forms.Button BTN_Cancel;
    }
}