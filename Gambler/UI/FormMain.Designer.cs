namespace Gambler
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TSMI_File = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_User = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_User_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_File,
            this.TSMI_User});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(897, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TSMI_File
            // 
            this.TSMI_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_File_Exit});
            this.TSMI_File.Name = "TSMI_File";
            this.TSMI_File.Size = new System.Drawing.Size(58, 21);
            this.TSMI_File.Text = "文件(&F)";
            // 
            // TSMI_File_Exit
            // 
            this.TSMI_File_Exit.Name = "TSMI_File_Exit";
            this.TSMI_File_Exit.ShortcutKeyDisplayString = "";
            this.TSMI_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.TSMI_File_Exit.Size = new System.Drawing.Size(161, 22);
            this.TSMI_File_Exit.Text = "退出(&X)";
            this.TSMI_File_Exit.Click += new System.EventHandler(this.TSMI_File_Exit_Click);
            // 
            // TSMI_User
            // 
            this.TSMI_User.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_User_Add});
            this.TSMI_User.Name = "TSMI_User";
            this.TSMI_User.Size = new System.Drawing.Size(61, 21);
            this.TSMI_User.Text = "账户(&U)";
            // 
            // TSMI_User_Add
            // 
            this.TSMI_User_Add.Name = "TSMI_User_Add";
            this.TSMI_User_Add.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.TSMI_User_Add.Size = new System.Drawing.Size(185, 22);
            this.TSMI_User_Add.Text = "增加账户(&A)";
            this.TSMI_User_Add.Click += new System.EventHandler(this.TSMI_User_Add_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 518);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "赌徒";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_File;
        private System.Windows.Forms.ToolStripMenuItem TSMI_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem TSMI_User;
        private System.Windows.Forms.ToolStripMenuItem TSMI_User_Add;
    }
}

