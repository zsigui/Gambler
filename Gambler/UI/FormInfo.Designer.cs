namespace Gambler.UI
{
    partial class FormInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DGV_Info = new System.Windows.Forms.DataGridView();
            this.DGC_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Host = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Away = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_League = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_BigOrSmall = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_ConcedePoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Capot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_OddOrEven = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BTN_BigOrSmall_Away = new System.Windows.Forms.Button();
            this.BTN_BigOrSmall_Host = new System.Windows.Forms.Button();
            this.BTN_Even = new System.Windows.Forms.Button();
            this.BTN_ConcedePoints_Host = new System.Windows.Forms.Button();
            this.BTN_Capot_None = new System.Windows.Forms.Button();
            this.BTN_ConcedePoints_Away = new System.Windows.Forms.Button();
            this.BTN_Capot_Away = new System.Windows.Forms.Button();
            this.BTN_Capot_Host = new System.Windows.Forms.Button();
            this.BTN_Odd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CB_AcceptOpt = new System.Windows.Forms.CheckBox();
            this.BTN_Refresh = new System.Windows.Forms.Button();
            this.RB_Half = new System.Windows.Forms.RadioButton();
            this.RB_Whole = new System.Windows.Forms.RadioButton();
            this.CB_OrderBy = new System.Windows.Forms.ComboBox();
            this.CB_Leagues = new System.Windows.Forms.ComboBox();
            this.TB_Search = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Info)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20);
            this.panel1.Size = new System.Drawing.Size(1159, 561);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.DGV_Info);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(20, 47);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.panel3.Size = new System.Drawing.Size(1119, 331);
            this.panel3.TabIndex = 2;
            // 
            // DGV_Info
            // 
            this.DGV_Info.AllowUserToAddRows = false;
            this.DGV_Info.AllowUserToDeleteRows = false;
            this.DGV_Info.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_Info.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Info.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Info.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DGC_Time,
            this.DGC_Score,
            this.DGC_Host,
            this.DGC_Away,
            this.DGC_League,
            this.DGC_BigOrSmall,
            this.DGC_ConcedePoints,
            this.DGC_Capot,
            this.DGC_OddOrEven});
            this.DGV_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Info.Location = new System.Drawing.Point(0, 15);
            this.DGV_Info.MultiSelect = false;
            this.DGV_Info.Name = "DGV_Info";
            this.DGV_Info.ReadOnly = true;
            this.DGV_Info.RowHeadersVisible = false;
            this.DGV_Info.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGV_Info.RowTemplate.Height = 23;
            this.DGV_Info.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV_Info.Size = new System.Drawing.Size(1119, 301);
            this.DGV_Info.TabIndex = 0;
            this.DGV_Info.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DGV_Info_CellPainting);
            this.DGV_Info.SelectionChanged += new System.EventHandler(this.DGV_Info_SelectionChanged);
            // 
            // DGC_Time
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Time.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGC_Time.HeaderText = "时间";
            this.DGC_Time.Name = "DGC_Time";
            this.DGC_Time.ReadOnly = true;
            // 
            // DGC_Score
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Score.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGC_Score.HeaderText = "比分";
            this.DGC_Score.Name = "DGC_Score";
            this.DGC_Score.ReadOnly = true;
            // 
            // DGC_Host
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Host.DefaultCellStyle = dataGridViewCellStyle4;
            this.DGC_Host.HeaderText = "主队（+）";
            this.DGC_Host.Name = "DGC_Host";
            this.DGC_Host.ReadOnly = true;
            // 
            // DGC_Away
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Away.DefaultCellStyle = dataGridViewCellStyle5;
            this.DGC_Away.HeaderText = "客队（-）";
            this.DGC_Away.Name = "DGC_Away";
            this.DGC_Away.ReadOnly = true;
            // 
            // DGC_League
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_League.DefaultCellStyle = dataGridViewCellStyle6;
            this.DGC_League.HeaderText = "联赛";
            this.DGC_League.Name = "DGC_League";
            this.DGC_League.ReadOnly = true;
            // 
            // DGC_BigOrSmall
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_BigOrSmall.DefaultCellStyle = dataGridViewCellStyle7;
            this.DGC_BigOrSmall.HeaderText = "大小";
            this.DGC_BigOrSmall.Name = "DGC_BigOrSmall";
            this.DGC_BigOrSmall.ReadOnly = true;
            // 
            // DGC_ConcedePoints
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_ConcedePoints.DefaultCellStyle = dataGridViewCellStyle8;
            this.DGC_ConcedePoints.HeaderText = "让球";
            this.DGC_ConcedePoints.Name = "DGC_ConcedePoints";
            this.DGC_ConcedePoints.ReadOnly = true;
            // 
            // DGC_Capot
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Capot.DefaultCellStyle = dataGridViewCellStyle9;
            this.DGC_Capot.HeaderText = "独赢";
            this.DGC_Capot.Name = "DGC_Capot";
            this.DGC_Capot.ReadOnly = true;
            // 
            // DGC_OddOrEven
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_OddOrEven.DefaultCellStyle = dataGridViewCellStyle10;
            this.DGC_OddOrEven.HeaderText = "单双";
            this.DGC_OddOrEven.Name = "DGC_OddOrEven";
            this.DGC_OddOrEven.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(20, 378);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(20, 5, 20, 5);
            this.groupBox1.Size = new System.Drawing.Size(1119, 163);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下注框";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.BTN_BigOrSmall_Away, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_BigOrSmall_Host, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_Even, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_ConcedePoints_Host, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.BTN_Capot_None, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.BTN_ConcedePoints_Away, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.BTN_Capot_Away, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.BTN_Capot_Host, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.BTN_Odd, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1079, 139);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // BTN_BigOrSmall_Away
            // 
            this.BTN_BigOrSmall_Away.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_BigOrSmall_Away.Location = new System.Drawing.Point(880, 10);
            this.BTN_BigOrSmall_Away.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_BigOrSmall_Away.Name = "BTN_BigOrSmall_Away";
            this.BTN_BigOrSmall_Away.Size = new System.Drawing.Size(179, 49);
            this.BTN_BigOrSmall_Away.TabIndex = 10;
            this.BTN_BigOrSmall_Away.Text = "大小（客）";
            this.BTN_BigOrSmall_Away.UseVisualStyleBackColor = true;
            this.BTN_BigOrSmall_Away.Click += new System.EventHandler(this.BTN_BigOrSmall_Away_Click);
            // 
            // BTN_BigOrSmall_Host
            // 
            this.BTN_BigOrSmall_Host.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_BigOrSmall_Host.Location = new System.Drawing.Point(665, 10);
            this.BTN_BigOrSmall_Host.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_BigOrSmall_Host.Name = "BTN_BigOrSmall_Host";
            this.BTN_BigOrSmall_Host.Size = new System.Drawing.Size(175, 49);
            this.BTN_BigOrSmall_Host.TabIndex = 9;
            this.BTN_BigOrSmall_Host.Text = "大小（主）";
            this.BTN_BigOrSmall_Host.UseVisualStyleBackColor = true;
            this.BTN_BigOrSmall_Host.Click += new System.EventHandler(this.BTN_BigOrSmall_Host_Click);
            // 
            // BTN_Even
            // 
            this.BTN_Even.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_Even.Location = new System.Drawing.Point(450, 10);
            this.BTN_Even.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_Even.Name = "BTN_Even";
            this.BTN_Even.Size = new System.Drawing.Size(175, 49);
            this.BTN_Even.TabIndex = 8;
            this.BTN_Even.Text = "单双（双）";
            this.BTN_Even.UseVisualStyleBackColor = true;
            this.BTN_Even.Click += new System.EventHandler(this.BTN_Even_Click);
            // 
            // BTN_ConcedePoints_Host
            // 
            this.BTN_ConcedePoints_Host.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_ConcedePoints_Host.Location = new System.Drawing.Point(665, 79);
            this.BTN_ConcedePoints_Host.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_ConcedePoints_Host.Name = "BTN_ConcedePoints_Host";
            this.BTN_ConcedePoints_Host.Size = new System.Drawing.Size(175, 50);
            this.BTN_ConcedePoints_Host.TabIndex = 6;
            this.BTN_ConcedePoints_Host.Text = "让球（主）";
            this.BTN_ConcedePoints_Host.UseVisualStyleBackColor = true;
            this.BTN_ConcedePoints_Host.Click += new System.EventHandler(this.BTN_ConcedePoints_Host_Click);
            // 
            // BTN_Capot_None
            // 
            this.BTN_Capot_None.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_Capot_None.Location = new System.Drawing.Point(235, 79);
            this.BTN_Capot_None.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_Capot_None.Name = "BTN_Capot_None";
            this.BTN_Capot_None.Size = new System.Drawing.Size(175, 50);
            this.BTN_Capot_None.TabIndex = 5;
            this.BTN_Capot_None.Text = "独赢（和）";
            this.BTN_Capot_None.UseVisualStyleBackColor = true;
            this.BTN_Capot_None.Click += new System.EventHandler(this.BTN_Capot_None_Click);
            // 
            // BTN_ConcedePoints_Away
            // 
            this.BTN_ConcedePoints_Away.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_ConcedePoints_Away.Location = new System.Drawing.Point(880, 79);
            this.BTN_ConcedePoints_Away.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_ConcedePoints_Away.Name = "BTN_ConcedePoints_Away";
            this.BTN_ConcedePoints_Away.Size = new System.Drawing.Size(179, 50);
            this.BTN_ConcedePoints_Away.TabIndex = 4;
            this.BTN_ConcedePoints_Away.Text = "让球（客）";
            this.BTN_ConcedePoints_Away.UseVisualStyleBackColor = true;
            this.BTN_ConcedePoints_Away.Click += new System.EventHandler(this.BTN_ConcedePoints_Away_Click);
            // 
            // BTN_Capot_Away
            // 
            this.BTN_Capot_Away.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_Capot_Away.Location = new System.Drawing.Point(450, 79);
            this.BTN_Capot_Away.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_Capot_Away.Name = "BTN_Capot_Away";
            this.BTN_Capot_Away.Size = new System.Drawing.Size(175, 50);
            this.BTN_Capot_Away.TabIndex = 3;
            this.BTN_Capot_Away.Text = "独赢（客）";
            this.BTN_Capot_Away.UseVisualStyleBackColor = true;
            this.BTN_Capot_Away.Click += new System.EventHandler(this.BTN_Capot_Away_Click);
            // 
            // BTN_Capot_Host
            // 
            this.BTN_Capot_Host.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_Capot_Host.Location = new System.Drawing.Point(20, 79);
            this.BTN_Capot_Host.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_Capot_Host.Name = "BTN_Capot_Host";
            this.BTN_Capot_Host.Size = new System.Drawing.Size(175, 50);
            this.BTN_Capot_Host.TabIndex = 2;
            this.BTN_Capot_Host.Text = "独赢（主）";
            this.BTN_Capot_Host.UseVisualStyleBackColor = true;
            this.BTN_Capot_Host.Click += new System.EventHandler(this.BTN_Capot_Host_Click);
            // 
            // BTN_Odd
            // 
            this.BTN_Odd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BTN_Odd.Location = new System.Drawing.Point(20, 10);
            this.BTN_Odd.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.BTN_Odd.Name = "BTN_Odd";
            this.BTN_Odd.Size = new System.Drawing.Size(175, 49);
            this.BTN_Odd.TabIndex = 1;
            this.BTN_Odd.Text = "单双（单）";
            this.BTN_Odd.UseVisualStyleBackColor = true;
            this.BTN_Odd.Click += new System.EventHandler(this.BTN_Odd_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.CB_AcceptOpt);
            this.panel2.Controls.Add(this.BTN_Refresh);
            this.panel2.Controls.Add(this.RB_Half);
            this.panel2.Controls.Add(this.RB_Whole);
            this.panel2.Controls.Add(this.CB_OrderBy);
            this.panel2.Controls.Add(this.CB_Leagues);
            this.panel2.Controls.Add(this.TB_Search);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1119, 27);
            this.panel2.TabIndex = 0;
            // 
            // CB_AcceptOpt
            // 
            this.CB_AcceptOpt.AutoSize = true;
            this.CB_AcceptOpt.Checked = true;
            this.CB_AcceptOpt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_AcceptOpt.Location = new System.Drawing.Point(717, 5);
            this.CB_AcceptOpt.Name = "CB_AcceptOpt";
            this.CB_AcceptOpt.Size = new System.Drawing.Size(120, 16);
            this.CB_AcceptOpt.TabIndex = 11;
            this.CB_AcceptOpt.Text = "自动接受最佳赔率";
            this.CB_AcceptOpt.UseVisualStyleBackColor = true;
            // 
            // BTN_Refresh
            // 
            this.BTN_Refresh.Location = new System.Drawing.Point(591, 1);
            this.BTN_Refresh.Name = "BTN_Refresh";
            this.BTN_Refresh.Size = new System.Drawing.Size(75, 23);
            this.BTN_Refresh.TabIndex = 10;
            this.BTN_Refresh.Text = "刷新：5s";
            this.BTN_Refresh.UseVisualStyleBackColor = true;
            this.BTN_Refresh.Click += new System.EventHandler(this.BTN_Refresh_Click);
            // 
            // RB_Half
            // 
            this.RB_Half.AutoSize = true;
            this.RB_Half.Location = new System.Drawing.Point(522, 4);
            this.RB_Half.Name = "RB_Half";
            this.RB_Half.Size = new System.Drawing.Size(47, 16);
            this.RB_Half.TabIndex = 9;
            this.RB_Half.TabStop = true;
            this.RB_Half.Text = "半场";
            this.RB_Half.UseVisualStyleBackColor = true;
            this.RB_Half.CheckedChanged += new System.EventHandler(this.RB_CheckedChanged);
            // 
            // RB_Whole
            // 
            this.RB_Whole.AutoSize = true;
            this.RB_Whole.Location = new System.Drawing.Point(469, 4);
            this.RB_Whole.Name = "RB_Whole";
            this.RB_Whole.Size = new System.Drawing.Size(47, 16);
            this.RB_Whole.TabIndex = 8;
            this.RB_Whole.TabStop = true;
            this.RB_Whole.Text = "全场";
            this.RB_Whole.UseVisualStyleBackColor = true;
            this.RB_Whole.CheckedChanged += new System.EventHandler(this.RB_CheckedChanged);
            // 
            // CB_OrderBy
            // 
            this.CB_OrderBy.FormattingEnabled = true;
            this.CB_OrderBy.Items.AddRange(new object[] {
            "按时间排序",
            "按联盟赛排序"});
            this.CB_OrderBy.Location = new System.Drawing.Point(326, 3);
            this.CB_OrderBy.Name = "CB_OrderBy";
            this.CB_OrderBy.Size = new System.Drawing.Size(121, 20);
            this.CB_OrderBy.TabIndex = 7;
            this.CB_OrderBy.Text = "排序方式";
            this.CB_OrderBy.SelectedIndexChanged += new System.EventHandler(this.CB_OrderBy_SelectedIndexChanged);
            // 
            // CB_Leagues
            // 
            this.CB_Leagues.FormattingEnabled = true;
            this.CB_Leagues.Items.AddRange(new object[] {
            "联盟赛列表"});
            this.CB_Leagues.Location = new System.Drawing.Point(185, 3);
            this.CB_Leagues.Name = "CB_Leagues";
            this.CB_Leagues.Size = new System.Drawing.Size(121, 20);
            this.CB_Leagues.TabIndex = 6;
            this.CB_Leagues.Text = "联盟赛列表";
            this.CB_Leagues.SelectedIndexChanged += new System.EventHandler(this.CB_Leagues_SelectedIndexChanged);
            // 
            // TB_Search
            // 
            this.TB_Search.Location = new System.Drawing.Point(1, 3);
            this.TB_Search.Name = "TB_Search";
            this.TB_Search.Size = new System.Drawing.Size(166, 21);
            this.TB_Search.TabIndex = 5;
            this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
            this.TB_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Search_KeyDown);
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 561);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormInfo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下注信息界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInfo_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormInfo_FormClosed);
            this.Load += new System.EventHandler(this.FormInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Info)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView DGV_Info;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button BTN_BigOrSmall_Away;
        private System.Windows.Forms.Button BTN_BigOrSmall_Host;
        private System.Windows.Forms.Button BTN_Even;
        private System.Windows.Forms.Button BTN_Capot_None;
        private System.Windows.Forms.Button BTN_ConcedePoints_Away;
        private System.Windows.Forms.Button BTN_Capot_Away;
        private System.Windows.Forms.Button BTN_Capot_Host;
        private System.Windows.Forms.Button BTN_Odd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BTN_Refresh;
        private System.Windows.Forms.RadioButton RB_Half;
        private System.Windows.Forms.RadioButton RB_Whole;
        private System.Windows.Forms.ComboBox CB_Leagues;
        private System.Windows.Forms.TextBox TB_Search;
        private System.Windows.Forms.Button BTN_ConcedePoints_Host;
        private System.Windows.Forms.CheckBox CB_AcceptOpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Host;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Away;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_League;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_BigOrSmall;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_ConcedePoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Capot;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_OddOrEven;
        private System.Windows.Forms.ComboBox CB_OrderBy;
    }
}