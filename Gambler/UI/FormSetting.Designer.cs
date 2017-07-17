namespace Gambler.UI
{
    partial class FormSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.TB_AutoRefreshTime = new System.Windows.Forms.TextBox();
            this.CB_AutoSaveUser = new System.Windows.Forms.CheckBox();
            this.CB_AutoBet = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "自动刷新间隔(秒)：";
            // 
            // TB_AutoRefreshTime
            // 
            this.TB_AutoRefreshTime.Location = new System.Drawing.Point(159, 29);
            this.TB_AutoRefreshTime.Name = "TB_AutoRefreshTime";
            this.TB_AutoRefreshTime.Size = new System.Drawing.Size(120, 21);
            this.TB_AutoRefreshTime.TabIndex = 1;
            this.TB_AutoRefreshTime.TextChanged += new System.EventHandler(this.TB_AutoRefreshTime_TextChanged);
            // 
            // CB_AutoSaveUser
            // 
            this.CB_AutoSaveUser.AutoSize = true;
            this.CB_AutoSaveUser.Location = new System.Drawing.Point(42, 68);
            this.CB_AutoSaveUser.Name = "CB_AutoSaveUser";
            this.CB_AutoSaveUser.Size = new System.Drawing.Size(120, 16);
            this.CB_AutoSaveUser.TabIndex = 2;
            this.CB_AutoSaveUser.Text = "自动保存新增用户";
            this.CB_AutoSaveUser.UseVisualStyleBackColor = true;
            this.CB_AutoSaveUser.CheckedChanged += new System.EventHandler(this.CB_AutoSaveUser_CheckedChanged);
            // 
            // CB_AutoBet
            // 
            this.CB_AutoBet.AutoSize = true;
            this.CB_AutoBet.Location = new System.Drawing.Point(42, 103);
            this.CB_AutoBet.Name = "CB_AutoBet";
            this.CB_AutoBet.Size = new System.Drawing.Size(72, 16);
            this.CB_AutoBet.TabIndex = 3;
            this.CB_AutoBet.Text = "自动下注";
            this.CB_AutoBet.UseVisualStyleBackColor = true;
            this.CB_AutoBet.CheckedChanged += new System.EventHandler(this.CB_AutoBet_CheckedChanged);
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 135);
            this.Controls.Add(this.CB_AutoBet);
            this.Controls.Add(this.CB_AutoSaveUser);
            this.Controls.Add(this.TB_AutoRefreshTime);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FormSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetting_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_AutoRefreshTime;
        private System.Windows.Forms.CheckBox CB_AutoSaveUser;
        private System.Windows.Forms.CheckBox CB_AutoBet;
    }
}