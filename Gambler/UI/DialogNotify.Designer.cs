namespace Gambler.UI
{
    partial class DialogNotify
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
            this.BTN_Close = new System.Windows.Forms.Button();
            this.TB_Top = new System.Windows.Forms.TextBox();
            this.TB_Middle = new System.Windows.Forms.TextBox();
            this.TB_Bottom = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BTN_Close
            // 
            this.BTN_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_Close.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BTN_Close.Location = new System.Drawing.Point(90, 136);
            this.BTN_Close.Name = "BTN_Close";
            this.BTN_Close.Size = new System.Drawing.Size(103, 36);
            this.BTN_Close.TabIndex = 0;
            this.BTN_Close.Text = "关  闭";
            this.BTN_Close.UseVisualStyleBackColor = true;
            this.BTN_Close.Click += new System.EventHandler(this.BTN_Close_Click);
            // 
            // TB_Top
            // 
            this.TB_Top.BackColor = System.Drawing.SystemColors.Control;
            this.TB_Top.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Top.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TB_Top.ForeColor = System.Drawing.Color.OrangeRed;
            this.TB_Top.Location = new System.Drawing.Point(2, 24);
            this.TB_Top.Multiline = true;
            this.TB_Top.Name = "TB_Top";
            this.TB_Top.ReadOnly = true;
            this.TB_Top.Size = new System.Drawing.Size(281, 27);
            this.TB_Top.TabIndex = 1;
            this.TB_Top.Text = "韩国男亚杯联赛";
            this.TB_Top.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Top.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TB_Top_MouseDoubleClick);
            // 
            // TB_Middle
            // 
            this.TB_Middle.BackColor = System.Drawing.SystemColors.Control;
            this.TB_Middle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Middle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TB_Middle.ForeColor = System.Drawing.Color.Blue;
            this.TB_Middle.Location = new System.Drawing.Point(2, 58);
            this.TB_Middle.Multiline = true;
            this.TB_Middle.Name = "TB_Middle";
            this.TB_Middle.ReadOnly = true;
            this.TB_Middle.Size = new System.Drawing.Size(281, 27);
            this.TB_Middle.TabIndex = 2;
            this.TB_Middle.Text = "尤克里里突击队U21（主队）";
            this.TB_Middle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Middle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TB_Middle_MouseDoubleClick);
            // 
            // TB_Bottom
            // 
            this.TB_Bottom.BackColor = System.Drawing.SystemColors.Control;
            this.TB_Bottom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Bottom.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TB_Bottom.ForeColor = System.Drawing.Color.Red;
            this.TB_Bottom.Location = new System.Drawing.Point(2, 94);
            this.TB_Bottom.Multiline = true;
            this.TB_Bottom.Name = "TB_Bottom";
            this.TB_Bottom.ReadOnly = true;
            this.TB_Bottom.Size = new System.Drawing.Size(281, 27);
            this.TB_Bottom.TabIndex = 3;
            this.TB_Bottom.Text = "【事件】可能进行点球";
            this.TB_Bottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_Bottom.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TB_Bottom_MouseDoubleClick);
            // 
            // DialogNotify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BTN_Close;
            this.ClientSize = new System.Drawing.Size(284, 187);
            this.Controls.Add(this.TB_Bottom);
            this.Controls.Add(this.TB_Middle);
            this.Controls.Add(this.TB_Top);
            this.Controls.Add(this.BTN_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogNotify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "通知！！";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Close;
        private System.Windows.Forms.TextBox TB_Top;
        private System.Windows.Forms.TextBox TB_Middle;
        private System.Windows.Forms.TextBox TB_Bottom;
    }
}