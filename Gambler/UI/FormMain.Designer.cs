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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MS_Menu = new System.Windows.Forms.MenuStrip();
            this.TSMI_File = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_File_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_User = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_User_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_User_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RTB_Output = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BTN_JumpBet = new System.Windows.Forms.Button();
            this.TC_User = new System.Windows.Forms.TabControl();
            this.TP_XPJ = new System.Windows.Forms.TabPage();
            this.CLB_XPJUser = new System.Windows.Forms.CheckedListBox();
            this.CMS_User = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_UserAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_UserRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DGV_Live = new System.Windows.Forms.DataGridView();
            this.BTN_Refresh = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SS_Bottom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSPB_Load = new System.Windows.Forms.ToolStripProgressBar();
            this.DGC_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_League = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Host = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Away = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Attention = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MS_Menu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.TC_User.SuspendLayout();
            this.TP_XPJ.SuspendLayout();
            this.CMS_User.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Live)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SS_Bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // MS_Menu
            // 
            this.MS_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_File,
            this.TSMI_User});
            this.MS_Menu.Location = new System.Drawing.Point(0, 0);
            this.MS_Menu.Name = "MS_Menu";
            this.MS_Menu.Size = new System.Drawing.Size(1083, 25);
            this.MS_Menu.TabIndex = 0;
            this.MS_Menu.Text = "menuStrip1";
            // 
            // TSMI_File
            // 
            this.TSMI_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_File_Setting,
            this.TSMI_File_Exit});
            this.TSMI_File.Name = "TSMI_File";
            this.TSMI_File.Size = new System.Drawing.Size(58, 21);
            this.TSMI_File.Text = "文件(&F)";
            // 
            // TSMI_File_Setting
            // 
            this.TSMI_File_Setting.Name = "TSMI_File_Setting";
            this.TSMI_File_Setting.Size = new System.Drawing.Size(161, 22);
            this.TSMI_File_Setting.Text = "设置(S)";
            this.TSMI_File_Setting.Click += new System.EventHandler(this.TSMI_File_Setting_Click);
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
            this.TSMI_User_Add,
            this.TSMI_User_Remove});
            this.TSMI_User.Name = "TSMI_User";
            this.TSMI_User.Size = new System.Drawing.Size(61, 21);
            this.TSMI_User.Text = "账户(&U)";
            // 
            // TSMI_User_Add
            // 
            this.TSMI_User_Add.Name = "TSMI_User_Add";
            this.TSMI_User_Add.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.TSMI_User_Add.Size = new System.Drawing.Size(187, 22);
            this.TSMI_User_Add.Text = "增加账户(&A)";
            this.TSMI_User_Add.Click += new System.EventHandler(this.TSMI_User_Add_Click);
            // 
            // TSMI_User_Remove
            // 
            this.TSMI_User_Remove.Name = "TSMI_User_Remove";
            this.TSMI_User_Remove.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.TSMI_User_Remove.Size = new System.Drawing.Size(187, 22);
            this.TSMI_User_Remove.Text = "删除选中(&D)";
            this.TSMI_User_Remove.Click += new System.EventHandler(this.TSMI_User_Remove_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RTB_Output);
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1059, 183);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出区域";
            // 
            // RTB_Output
            // 
            this.RTB_Output.Location = new System.Drawing.Point(10, 20);
            this.RTB_Output.Name = "RTB_Output";
            this.RTB_Output.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RTB_Output.Size = new System.Drawing.Size(1038, 150);
            this.RTB_Output.TabIndex = 0;
            this.RTB_Output.Text = "";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(1083, 317);
            this.splitContainer2.SplitterDistance = 323;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.BTN_JumpBet);
            this.groupBox3.Controls.Add(this.TC_User);
            this.groupBox3.Location = new System.Drawing.Point(12, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 305);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "用户列表";
            // 
            // BTN_JumpBet
            // 
            this.BTN_JumpBet.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_JumpBet.Location = new System.Drawing.Point(10, 264);
            this.BTN_JumpBet.Name = "BTN_JumpBet";
            this.BTN_JumpBet.Size = new System.Drawing.Size(286, 31);
            this.BTN_JumpBet.TabIndex = 3;
            this.BTN_JumpBet.Text = "跳转对应下注界面";
            this.BTN_JumpBet.UseVisualStyleBackColor = true;
            // 
            // TC_User
            // 
            this.TC_User.Controls.Add(this.TP_XPJ);
            this.TC_User.Location = new System.Drawing.Point(11, 22);
            this.TC_User.Name = "TC_User";
            this.TC_User.SelectedIndex = 0;
            this.TC_User.Size = new System.Drawing.Size(285, 239);
            this.TC_User.TabIndex = 0;
            // 
            // TP_XPJ
            // 
            this.TP_XPJ.Controls.Add(this.CLB_XPJUser);
            this.TP_XPJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TP_XPJ.Location = new System.Drawing.Point(4, 22);
            this.TP_XPJ.Name = "TP_XPJ";
            this.TP_XPJ.Padding = new System.Windows.Forms.Padding(3);
            this.TP_XPJ.Size = new System.Drawing.Size(277, 213);
            this.TP_XPJ.TabIndex = 0;
            this.TP_XPJ.Text = "新葡京";
            this.TP_XPJ.UseVisualStyleBackColor = true;
            // 
            // CLB_XPJUser
            // 
            this.CLB_XPJUser.ContextMenuStrip = this.CMS_User;
            this.CLB_XPJUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CLB_XPJUser.FormattingEnabled = true;
            this.CLB_XPJUser.Location = new System.Drawing.Point(3, 3);
            this.CLB_XPJUser.Name = "CLB_XPJUser";
            this.CLB_XPJUser.Size = new System.Drawing.Size(271, 207);
            this.CLB_XPJUser.TabIndex = 0;
            // 
            // CMS_User
            // 
            this.CMS_User.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_UserAdd,
            this.TSMI_UserRemove});
            this.CMS_User.Name = "CMS_User";
            this.CMS_User.Size = new System.Drawing.Size(142, 48);
            // 
            // TSMI_UserAdd
            // 
            this.TSMI_UserAdd.Name = "TSMI_UserAdd";
            this.TSMI_UserAdd.Size = new System.Drawing.Size(141, 22);
            this.TSMI_UserAdd.Text = "添加账户(&A)";
            this.TSMI_UserAdd.Click += new System.EventHandler(this.TSMI_User_Add_Click);
            // 
            // TSMI_UserRemove
            // 
            this.TSMI_UserRemove.Name = "TSMI_UserRemove";
            this.TSMI_UserRemove.Size = new System.Drawing.Size(141, 22);
            this.TSMI_UserRemove.Text = "删除选中(&D)";
            this.TSMI_UserRemove.Click += new System.EventHandler(this.TSMI_User_Remove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DGV_Live);
            this.groupBox1.Controls.Add(this.BTN_Refresh);
            this.groupBox1.Location = new System.Drawing.Point(3, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(741, 305);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "直播列表（可获取）";
            // 
            // DGV_Live
            // 
            this.DGV_Live.AllowUserToAddRows = false;
            this.DGV_Live.AllowUserToDeleteRows = false;
            this.DGV_Live.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_Live.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Live.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DGV_Live.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Live.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DGC_ID,
            this.DGC_Time,
            this.DGC_League,
            this.DGC_Host,
            this.DGC_Away,
            this.DGC_Attention});
            this.DGV_Live.Location = new System.Drawing.Point(10, 49);
            this.DGV_Live.Name = "DGV_Live";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Live.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.DGV_Live.RowHeadersVisible = false;
            this.DGV_Live.RowTemplate.Height = 23;
            this.DGV_Live.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV_Live.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Live.Size = new System.Drawing.Size(720, 245);
            this.DGV_Live.TabIndex = 1;
            // 
            // BTN_Refresh
            // 
            this.BTN_Refresh.Location = new System.Drawing.Point(637, 20);
            this.BTN_Refresh.Name = "BTN_Refresh";
            this.BTN_Refresh.Size = new System.Drawing.Size(93, 23);
            this.BTN_Refresh.TabIndex = 0;
            this.BTN_Refresh.Text = "刷新：5s";
            this.BTN_Refresh.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SS_Bottom);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1083, 552);
            this.splitContainer1.SplitterDistance = 317;
            this.splitContainer1.TabIndex = 1;
            // 
            // SS_Bottom
            // 
            this.SS_Bottom.AllowItemReorder = true;
            this.SS_Bottom.AutoSize = false;
            this.SS_Bottom.Dock = System.Windows.Forms.DockStyle.None;
            this.SS_Bottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.TSPB_Load});
            this.SS_Bottom.Location = new System.Drawing.Point(0, 201);
            this.SS_Bottom.Name = "SS_Bottom";
            this.SS_Bottom.ShowItemToolTips = true;
            this.SS_Bottom.Size = new System.Drawing.Size(1083, 30);
            this.SS_Bottom.SizingGrip = false;
            this.SS_Bottom.TabIndex = 1;
            this.SS_Bottom.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1068, 25);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // TSPB_Load
            // 
            this.TSPB_Load.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TSPB_Load.CausesValidation = false;
            this.TSPB_Load.Enabled = false;
            this.TSPB_Load.MarqueeAnimationSpeed = 50;
            this.TSPB_Load.Name = "TSPB_Load";
            this.TSPB_Load.Size = new System.Drawing.Size(100, 24);
            this.TSPB_Load.Step = 5;
            this.TSPB_Load.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.TSPB_Load.Visible = false;
            // 
            // DGC_ID
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_ID.DefaultCellStyle = dataGridViewCellStyle9;
            this.DGC_ID.HeaderText = "ID";
            this.DGC_ID.Name = "DGC_ID";
            this.DGC_ID.ReadOnly = true;
            // 
            // DGC_Time
            // 
            this.DGC_Time.HeaderText = "时间";
            this.DGC_Time.Name = "DGC_Time";
            this.DGC_Time.ReadOnly = true;
            // 
            // DGC_League
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_League.DefaultCellStyle = dataGridViewCellStyle10;
            this.DGC_League.HeaderText = "联赛";
            this.DGC_League.Name = "DGC_League";
            this.DGC_League.ReadOnly = true;
            // 
            // DGC_Host
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Host.DefaultCellStyle = dataGridViewCellStyle11;
            this.DGC_Host.HeaderText = "主队";
            this.DGC_Host.Name = "DGC_Host";
            this.DGC_Host.ReadOnly = true;
            // 
            // DGC_Away
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Away.DefaultCellStyle = dataGridViewCellStyle12;
            this.DGC_Away.HeaderText = "客队";
            this.DGC_Away.Name = "DGC_Away";
            this.DGC_Away.ReadOnly = true;
            // 
            // DGC_Attention
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.NullValue = false;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Attention.DefaultCellStyle = dataGridViewCellStyle13;
            this.DGC_Attention.HeaderText = "是否关注";
            this.DGC_Attention.Name = "DGC_Attention";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 577);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.MS_Menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "赌徒";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.MS_Menu.ResumeLayout(false);
            this.MS_Menu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.TC_User.ResumeLayout(false);
            this.TP_XPJ.ResumeLayout(false);
            this.CMS_User.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Live)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.SS_Bottom.ResumeLayout(false);
            this.SS_Bottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MS_Menu;
        private System.Windows.Forms.ToolStripMenuItem TSMI_File;
        private System.Windows.Forms.ToolStripMenuItem TSMI_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem TSMI_User;
        private System.Windows.Forms.ToolStripMenuItem TSMI_User_Add;
        private System.Windows.Forms.ToolStripMenuItem TSMI_File_Setting;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox RTB_Output;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BTN_JumpBet;
        private System.Windows.Forms.TabControl TC_User;
        private System.Windows.Forms.TabPage TP_XPJ;
        private System.Windows.Forms.CheckedListBox CLB_XPJUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DGV_Live;
        private System.Windows.Forms.Button BTN_Refresh;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip SS_Bottom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar TSPB_Load;
        private System.Windows.Forms.ContextMenuStrip CMS_User;
        private System.Windows.Forms.ToolStripMenuItem TSMI_UserAdd;
        private System.Windows.Forms.ToolStripMenuItem TSMI_UserRemove;
        private System.Windows.Forms.ToolStripMenuItem TSMI_User_Remove;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_League;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Host;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Away;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DGC_Attention;
    }
}

