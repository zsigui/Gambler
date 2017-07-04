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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MS_Menu = new System.Windows.Forms.MenuStrip();
            this.TSMI_File = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_File_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_File_Map = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_User = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_User_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_User_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Tool_Map = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RTB_Output = new System.Windows.Forms.RichTextBox();
            this.CMS_Output = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_Output_Clear = new System.Windows.Forms.ToolStripMenuItem();
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
            this.CB_MoreEvent = new System.Windows.Forms.CheckBox();
            this.DGV_Live = new System.Windows.Forms.DataGridView();
            this.DGC_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Scroe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_League = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Host = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Away = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Attention = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CMS_Live = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_Live_CheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_AddCookie = new System.Windows.Forms.ToolStripMenuItem();
            this.BTN_Refresh = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SS_Bottom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSPB_Load = new System.Windows.Forms.ToolStripProgressBar();
            this.MS_Menu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.CMS_Output.SuspendLayout();
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
            this.CMS_Live.SuspendLayout();
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
            this.TSMI_User,
            this.TSMI_Tool});
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
            this.TSMI_File_Map,
            this.TSMI_File_Exit});
            this.TSMI_File.Name = "TSMI_File";
            this.TSMI_File.Size = new System.Drawing.Size(58, 21);
            this.TSMI_File.Text = "文件(&F)";
            // 
            // TSMI_File_Setting
            // 
            this.TSMI_File_Setting.Name = "TSMI_File_Setting";
            this.TSMI_File_Setting.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.TSMI_File_Setting.Size = new System.Drawing.Size(193, 22);
            this.TSMI_File_Setting.Text = "设置(S)";
            this.TSMI_File_Setting.Click += new System.EventHandler(this.TSMI_File_Setting_Click);
            // 
            // TSMI_File_Map
            // 
            this.TSMI_File_Map.Name = "TSMI_File_Map";
            this.TSMI_File_Map.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.TSMI_File_Map.Size = new System.Drawing.Size(193, 22);
            this.TSMI_File_Map.Text = "名称映射(&M)";
            this.TSMI_File_Map.Click += new System.EventHandler(this.TSMI_File_Map_Click);
            // 
            // TSMI_File_Exit
            // 
            this.TSMI_File_Exit.Name = "TSMI_File_Exit";
            this.TSMI_File_Exit.ShortcutKeyDisplayString = "";
            this.TSMI_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.TSMI_File_Exit.Size = new System.Drawing.Size(193, 22);
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
            this.TSMI_User_Add.Size = new System.Drawing.Size(211, 22);
            this.TSMI_User_Add.Text = "增加账户(&A)";
            this.TSMI_User_Add.Click += new System.EventHandler(this.TSMI_User_Add_Click);
            // 
            // TSMI_User_Remove
            // 
            this.TSMI_User_Remove.Name = "TSMI_User_Remove";
            this.TSMI_User_Remove.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D)));
            this.TSMI_User_Remove.Size = new System.Drawing.Size(211, 22);
            this.TSMI_User_Remove.Text = "删除选中(&D)";
            this.TSMI_User_Remove.Click += new System.EventHandler(this.TSMI_User_Remove_Click);
            // 
            // TSMI_Tool
            // 
            this.TSMI_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_Tool_Map});
            this.TSMI_Tool.Name = "TSMI_Tool";
            this.TSMI_Tool.Size = new System.Drawing.Size(71, 21);
            this.TSMI_Tool.Text = "小工具(&T)";
            // 
            // TSMI_Tool_Map
            // 
            this.TSMI_Tool_Map.Name = "TSMI_Tool_Map";
            this.TSMI_Tool_Map.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.TSMI_Tool_Map.Size = new System.Drawing.Size(257, 22);
            this.TSMI_Tool_Map.Text = "名称映射查询工具(&C)";
            this.TSMI_Tool_Map.Click += new System.EventHandler(this.TSMI_Tool_Map_Click);
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
            this.RTB_Output.ContextMenuStrip = this.CMS_Output;
            this.RTB_Output.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RTB_Output.Location = new System.Drawing.Point(10, 20);
            this.RTB_Output.Name = "RTB_Output";
            this.RTB_Output.ReadOnly = true;
            this.RTB_Output.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RTB_Output.Size = new System.Drawing.Size(1038, 150);
            this.RTB_Output.TabIndex = 0;
            this.RTB_Output.Text = "";
            this.RTB_Output.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RTB_Output_MouseDoubleClick);
            // 
            // CMS_Output
            // 
            this.CMS_Output.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_Output_Clear});
            this.CMS_Output.Name = "CMS_Output";
            this.CMS_Output.Size = new System.Drawing.Size(101, 26);
            // 
            // TSMI_Output_Clear
            // 
            this.TSMI_Output_Clear.Name = "TSMI_Output_Clear";
            this.TSMI_Output_Clear.Size = new System.Drawing.Size(100, 22);
            this.TSMI_Output_Clear.Text = "清空";
            this.TSMI_Output_Clear.Click += new System.EventHandler(this.TSMI_Output_Clear_Click);
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
            this.BTN_JumpBet.Click += new System.EventHandler(this.BTN_JumpBet_Click);
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
            this.CLB_XPJUser.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CLB_XPJUser_ItemCheck);
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
            this.groupBox1.Controls.Add(this.CB_MoreEvent);
            this.groupBox1.Controls.Add(this.DGV_Live);
            this.groupBox1.Controls.Add(this.BTN_Refresh);
            this.groupBox1.Location = new System.Drawing.Point(3, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(741, 305);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "直播列表（可获取）";
            // 
            // CB_MoreEvent
            // 
            this.CB_MoreEvent.AutoSize = true;
            this.CB_MoreEvent.Checked = true;
            this.CB_MoreEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_MoreEvent.Location = new System.Drawing.Point(476, 24);
            this.CB_MoreEvent.Name = "CB_MoreEvent";
            this.CB_MoreEvent.Size = new System.Drawing.Size(96, 16);
            this.CB_MoreEvent.TabIndex = 3;
            this.CB_MoreEvent.Text = "输出普通事件";
            this.CB_MoreEvent.UseVisualStyleBackColor = true;
            // 
            // DGV_Live
            // 
            this.DGV_Live.AllowUserToAddRows = false;
            this.DGV_Live.AllowUserToDeleteRows = false;
            this.DGV_Live.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_Live.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Live.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Live.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Live.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DGC_ID,
            this.DGC_Time,
            this.DGC_Scroe,
            this.DGC_League,
            this.DGC_Host,
            this.DGC_Away,
            this.DGC_Attention});
            this.DGV_Live.ContextMenuStrip = this.CMS_Live;
            this.DGV_Live.Location = new System.Drawing.Point(10, 49);
            this.DGV_Live.Name = "DGV_Live";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Live.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.DGV_Live.RowHeadersVisible = false;
            this.DGV_Live.RowTemplate.Height = 23;
            this.DGV_Live.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV_Live.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Live.Size = new System.Drawing.Size(720, 245);
            this.DGV_Live.TabIndex = 1;
            this.DGV_Live.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_Live_CellMouseDoubleClick);
            // 
            // DGC_ID
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGC_ID.HeaderText = "ID";
            this.DGC_ID.Name = "DGC_ID";
            // 
            // DGC_Time
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Time.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGC_Time.HeaderText = "时间";
            this.DGC_Time.Name = "DGC_Time";
            // 
            // DGC_Scroe
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Scroe.DefaultCellStyle = dataGridViewCellStyle4;
            this.DGC_Scroe.HeaderText = "比分";
            this.DGC_Scroe.Name = "DGC_Scroe";
            this.DGC_Scroe.ReadOnly = true;
            // 
            // DGC_League
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_League.DefaultCellStyle = dataGridViewCellStyle5;
            this.DGC_League.HeaderText = "联赛";
            this.DGC_League.Name = "DGC_League";
            // 
            // DGC_Host
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Host.DefaultCellStyle = dataGridViewCellStyle6;
            this.DGC_Host.HeaderText = "主队";
            this.DGC_Host.Name = "DGC_Host";
            // 
            // DGC_Away
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Away.DefaultCellStyle = dataGridViewCellStyle7;
            this.DGC_Away.HeaderText = "客队";
            this.DGC_Away.Name = "DGC_Away";
            // 
            // DGC_Attention
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = false;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Attention.DefaultCellStyle = dataGridViewCellStyle8;
            this.DGC_Attention.HeaderText = "是否关注";
            this.DGC_Attention.Name = "DGC_Attention";
            // 
            // CMS_Live
            // 
            this.CMS_Live.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_Live_CheckAll,
            this.TSMI_AddCookie});
            this.CMS_Live.Name = "CMS_";
            this.CMS_Live.Size = new System.Drawing.Size(195, 48);
            // 
            // TSMI_Live_CheckAll
            // 
            this.TSMI_Live_CheckAll.Name = "TSMI_Live_CheckAll";
            this.TSMI_Live_CheckAll.Size = new System.Drawing.Size(194, 22);
            this.TSMI_Live_CheckAll.Text = "全选/全不选";
            this.TSMI_Live_CheckAll.Click += new System.EventHandler(this.TSMI_Live_CheckAll_Click);
            // 
            // TSMI_AddCookie
            // 
            this.TSMI_AddCookie.Name = "TSMI_AddCookie";
            this.TSMI_AddCookie.Size = new System.Drawing.Size(194, 22);
            this.TSMI_AddCookie.Text = "设置鸿发账号/Cookie";
            this.TSMI_AddCookie.Click += new System.EventHandler(this.TSMI_AddCookie_Click);
            // 
            // BTN_Refresh
            // 
            this.BTN_Refresh.Location = new System.Drawing.Point(594, 20);
            this.BTN_Refresh.Name = "BTN_Refresh";
            this.BTN_Refresh.Size = new System.Drawing.Size(136, 23);
            this.BTN_Refresh.TabIndex = 0;
            this.BTN_Refresh.Text = "刷新";
            this.BTN_Refresh.UseVisualStyleBackColor = true;
            this.BTN_Refresh.Click += new System.EventHandler(this.BTN_Refresh_Click);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.MS_Menu.ResumeLayout(false);
            this.MS_Menu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.CMS_Output.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.TC_User.ResumeLayout(false);
            this.TP_XPJ.ResumeLayout(false);
            this.CMS_User.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Live)).EndInit();
            this.CMS_Live.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip CMS_Live;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Live_CheckAll;
        private System.Windows.Forms.ToolStripMenuItem TSMI_AddCookie;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Scroe;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_League;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Host;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Away;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DGC_Attention;
        private System.Windows.Forms.CheckBox CB_MoreEvent;
        private System.Windows.Forms.ContextMenuStrip CMS_Output;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Output_Clear;
        private System.Windows.Forms.ToolStripMenuItem TSMI_File_Map;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Tool;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Tool_Map;
    }
}

