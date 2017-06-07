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
            this.label3 = new System.Windows.Forms.Label();
            this.CB_Type = new System.Windows.Forms.ComboBox();
            this.TB_LeastMoney = new System.Windows.Forms.TextBox();
            this.TB_MostMoney = new System.Windows.Forms.TextBox();
            this.RB_EveryTime = new System.Windows.Forms.RadioButton();
            this.RB_Random = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.BTN_Confirm = new System.Windows.Forms.Button();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_Rate = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "下注类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "最小下注金额(元)：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "最大下注金额(元)：";
            // 
            // CB_Type
            // 
            this.CB_Type.FormattingEnabled = true;
            this.CB_Type.Items.AddRange(new object[] {
            "单双",
            "大小（全场）",
            "让球（全场）",
            "独赢（全场）",
            "大小（半场）",
            "让球（半场）",
            "独赢（半场）"});
            this.CB_Type.Location = new System.Drawing.Point(153, 30);
            this.CB_Type.Name = "CB_Type";
            this.CB_Type.Size = new System.Drawing.Size(121, 20);
            this.CB_Type.TabIndex = 3;
            // 
            // TB_LeastMoney
            // 
            this.TB_LeastMoney.Location = new System.Drawing.Point(153, 60);
            this.TB_LeastMoney.Name = "TB_LeastMoney";
            this.TB_LeastMoney.Size = new System.Drawing.Size(121, 21);
            this.TB_LeastMoney.TabIndex = 4;
            this.TB_LeastMoney.TextChanged += new System.EventHandler(this.TB_LeastMoney_TextChanged);
            // 
            // TB_MostMoney
            // 
            this.TB_MostMoney.Location = new System.Drawing.Point(153, 90);
            this.TB_MostMoney.Name = "TB_MostMoney";
            this.TB_MostMoney.Size = new System.Drawing.Size(121, 21);
            this.TB_MostMoney.TabIndex = 5;
            this.TB_MostMoney.TextChanged += new System.EventHandler(this.TB_MostMoney_TextChanged);
            // 
            // RB_EveryTime
            // 
            this.RB_EveryTime.AutoSize = true;
            this.RB_EveryTime.Location = new System.Drawing.Point(153, 121);
            this.RB_EveryTime.Name = "RB_EveryTime";
            this.RB_EveryTime.Size = new System.Drawing.Size(47, 16);
            this.RB_EveryTime.TabIndex = 6;
            this.RB_EveryTime.TabStop = true;
            this.RB_EveryTime.Text = "每次";
            this.RB_EveryTime.UseVisualStyleBackColor = true;
            // 
            // RB_Random
            // 
            this.RB_Random.AutoSize = true;
            this.RB_Random.Location = new System.Drawing.Point(215, 121);
            this.RB_Random.Name = "RB_Random";
            this.RB_Random.Size = new System.Drawing.Size(47, 16);
            this.RB_Random.TabIndex = 7;
            this.RB_Random.TabStop = true;
            this.RB_Random.Text = "随机";
            this.RB_Random.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(37, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(337, 33);
            this.label4.TabIndex = 8;
            this.label4.Text = "（P.S. 最小最大金额需要不小于赌场下注限额，否则会下注失败；两者相同或者最小大于最大表示固定为最小下注额度；当最小下注额大于当前账户金额，则全部下注；不填表示" +
    "不限制金额）";
            // 
            // BTN_Confirm
            // 
            this.BTN_Confirm.Location = new System.Drawing.Point(215, 231);
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
            this.BTN_Cancel.Location = new System.Drawing.Point(115, 231);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(75, 34);
            this.BTN_Cancel.TabIndex = 10;
            this.BTN_Cancel.Text = "取消";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(81, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "下注方式：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(81, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "下注倍率：";
            // 
            // TB_Rate
            // 
            this.TB_Rate.Location = new System.Drawing.Point(153, 147);
            this.TB_Rate.Name = "TB_Rate";
            this.TB_Rate.Size = new System.Drawing.Size(121, 21);
            this.TB_Rate.TabIndex = 13;
            // 
            // FormAutoBetSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BTN_Cancel;
            this.ClientSize = new System.Drawing.Size(402, 277);
            this.ControlBox = false;
            this.Controls.Add(this.TB_Rate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.BTN_Confirm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RB_Random);
            this.Controls.Add(this.RB_EveryTime);
            this.Controls.Add(this.TB_MostMoney);
            this.Controls.Add(this.TB_LeastMoney);
            this.Controls.Add(this.CB_Type);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CB_Type;
        private System.Windows.Forms.TextBox TB_LeastMoney;
        private System.Windows.Forms.TextBox TB_MostMoney;
        private System.Windows.Forms.RadioButton RB_EveryTime;
        private System.Windows.Forms.RadioButton RB_Random;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BTN_Confirm;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_Rate;
    }
}