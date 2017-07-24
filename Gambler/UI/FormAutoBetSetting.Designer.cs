namespace Gambler.UI
{
    partial class FormAutoBetSetting
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
            this.label2 = new System.Windows.Forms.Label();
            this.CB_Type = new System.Windows.Forms.ComboBox();
            this.TB_Money = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BTN_Confirm = new System.Windows.Forms.Button();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_Rate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CB_SecondType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CB_Behavior = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "首选下注类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "每次下注金额(元)：";
            // 
            // CB_Type
            // 
            this.CB_Type.FormattingEnabled = true;
            this.CB_Type.Items.AddRange(new object[] {
            "大小（全场）",
            "大小（半场）",
            "让球（全场）",
            "让球（半场）"});
            this.CB_Type.Location = new System.Drawing.Point(153, 30);
            this.CB_Type.Name = "CB_Type";
            this.CB_Type.Size = new System.Drawing.Size(121, 20);
            this.CB_Type.TabIndex = 3;
            // 
            // TB_Money
            // 
            this.TB_Money.Location = new System.Drawing.Point(153, 92);
            this.TB_Money.Name = "TB_Money";
            this.TB_Money.Size = new System.Drawing.Size(121, 21);
            this.TB_Money.TabIndex = 4;
            this.TB_Money.TextChanged += new System.EventHandler(this.TB_Money_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(37, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(337, 33);
            this.label4.TabIndex = 8;
            this.label4.Text = "（P.S. 优先判断首选下注类型，如果首选的不满足，再判断备选下注类型）";
            // 
            // BTN_Confirm
            // 
            this.BTN_Confirm.Location = new System.Drawing.Point(216, 230);
            this.BTN_Confirm.Name = "BTN_Confirm";
            this.BTN_Confirm.Size = new System.Drawing.Size(79, 34);
            this.BTN_Confirm.TabIndex = 9;
            this.BTN_Confirm.Text = "确定";
            this.BTN_Confirm.UseVisualStyleBackColor = true;
            this.BTN_Confirm.Click += new System.EventHandler(this.BTN_Confirm_Click);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_Cancel.Location = new System.Drawing.Point(116, 230);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(75, 34);
            this.BTN_Cancel.TabIndex = 10;
            this.BTN_Cancel.Text = "取消";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "下注最低倍率：";
            // 
            // TB_Rate
            // 
            this.TB_Rate.Location = new System.Drawing.Point(153, 123);
            this.TB_Rate.Name = "TB_Rate";
            this.TB_Rate.Size = new System.Drawing.Size(121, 21);
            this.TB_Rate.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "备选下注类型：";
            // 
            // CB_SecondType
            // 
            this.CB_SecondType.FormattingEnabled = true;
            this.CB_SecondType.Items.AddRange(new object[] {
            "无",
            "大小（全场）",
            "大小（半场）",
            "让球（全场）",
            "让球（半场）"});
            this.CB_SecondType.Location = new System.Drawing.Point(153, 61);
            this.CB_SecondType.Name = "CB_SecondType";
            this.CB_SecondType.Size = new System.Drawing.Size(121, 20);
            this.CB_SecondType.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(81, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "下注行为：";
            // 
            // CB_Behavior
            // 
            this.CB_Behavior.FormattingEnabled = true;
            this.CB_Behavior.Items.AddRange(new object[] {
            "两边下注",
            "首次下注",
            "首队下注"});
            this.CB_Behavior.Location = new System.Drawing.Point(153, 155);
            this.CB_Behavior.Name = "CB_Behavior";
            this.CB_Behavior.Size = new System.Drawing.Size(121, 20);
            this.CB_Behavior.TabIndex = 17;
            this.CB_Behavior.SelectedIndexChanged += new System.EventHandler(this.CB_Behavior_SelectedIndexChanged);
            // 
            // FormAutoBetSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BTN_Cancel;
            this.ClientSize = new System.Drawing.Size(402, 277);
            this.ControlBox = false;
            this.Controls.Add(this.CB_Behavior);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CB_SecondType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_Rate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.BTN_Confirm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_Money);
            this.Controls.Add(this.CB_Type);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAutoBetSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动下注配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CB_Type;
        private System.Windows.Forms.TextBox TB_Money;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BTN_Confirm;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_Rate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CB_SecondType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CB_Behavior;
    }
}