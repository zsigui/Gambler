﻿namespace Gambler.UI
{
    partial class DialogConfirm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.LB_Match = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LB_Odd = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BTN_Confirm = new System.Windows.Forms.Button();
            this.LB_League = new System.Windows.Forms.Label();
            this.LB_Score = new System.Windows.Forms.Label();
            this.LB_Time = new System.Windows.Forms.Label();
            this.LB_User = new System.Windows.Forms.ListBox();
            this.DGV_BetUser = new System.Windows.Forms.DataGridView();
            this.DGC_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGC_Money = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_BetUser)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(31, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "赛事信息：";
            // 
            // LB_Match
            // 
            this.LB_Match.ForeColor = System.Drawing.Color.Brown;
            this.LB_Match.Location = new System.Drawing.Point(31, 79);
            this.LB_Match.Name = "LB_Match";
            this.LB_Match.Size = new System.Drawing.Size(281, 15);
            this.LB_Match.TabIndex = 1;
            this.LB_Match.Text = "意大利名罗勒（主）v.s. 苏格兰非洲雄鹰（客）";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(31, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "盘口信息：";
            // 
            // LB_Odd
            // 
            this.LB_Odd.AutoSize = true;
            this.LB_Odd.ForeColor = System.Drawing.Color.OrangeRed;
            this.LB_Odd.Location = new System.Drawing.Point(26, 171);
            this.LB_Odd.Name = "LB_Odd";
            this.LB_Odd.Size = new System.Drawing.Size(107, 12);
            this.LB_Odd.TabIndex = 3;
            this.LB_Odd.Text = "【大小】-【1.73】";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(31, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "下注账号与金额：";
            // 
            // BTN_Confirm
            // 
            this.BTN_Confirm.Location = new System.Drawing.Point(118, 318);
            this.BTN_Confirm.Name = "BTN_Confirm";
            this.BTN_Confirm.Size = new System.Drawing.Size(96, 32);
            this.BTN_Confirm.TabIndex = 6;
            this.BTN_Confirm.Text = "确认下注";
            this.BTN_Confirm.UseVisualStyleBackColor = true;
            // 
            // LB_League
            // 
            this.LB_League.AutoSize = true;
            this.LB_League.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LB_League.Location = new System.Drawing.Point(31, 56);
            this.LB_League.Name = "LB_League";
            this.LB_League.Size = new System.Drawing.Size(77, 12);
            this.LB_League.TabIndex = 7;
            this.LB_League.Text = "苏格兰国家杯";
            // 
            // LB_Score
            // 
            this.LB_Score.AutoSize = true;
            this.LB_Score.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LB_Score.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.LB_Score.Location = new System.Drawing.Point(32, 103);
            this.LB_Score.Name = "LB_Score";
            this.LB_Score.Size = new System.Drawing.Size(40, 12);
            this.LB_Score.TabIndex = 8;
            this.LB_Score.Text = "3 - 2";
            // 
            // LB_Time
            // 
            this.LB_Time.AutoSize = true;
            this.LB_Time.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LB_Time.Location = new System.Drawing.Point(107, 103);
            this.LB_Time.Name = "LB_Time";
            this.LB_Time.Size = new System.Drawing.Size(35, 12);
            this.LB_Time.TabIndex = 9;
            this.LB_Time.Text = "1H23\'";
            // 
            // LB_User
            // 
            this.LB_User.FormattingEnabled = true;
            this.LB_User.ItemHeight = 12;
            this.LB_User.Location = new System.Drawing.Point(33, 239);
            this.LB_User.Name = "LB_User";
            this.LB_User.ScrollAlwaysVisible = true;
            this.LB_User.Size = new System.Drawing.Size(264, 64);
            this.LB_User.TabIndex = 10;
            this.LB_User.Visible = false;
            // 
            // DGV_BetUser
            // 
            this.DGV_BetUser.AllowUserToAddRows = false;
            this.DGV_BetUser.AllowUserToDeleteRows = false;
            this.DGV_BetUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_BetUser.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGV_BetUser.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_BetUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.DGV_BetUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV_BetUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DGC_Name,
            this.DGC_Money});
            this.DGV_BetUser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DGV_BetUser.EnableHeadersVisualStyles = false;
            this.DGV_BetUser.Location = new System.Drawing.Point(33, 239);
            this.DGV_BetUser.MultiSelect = false;
            this.DGV_BetUser.Name = "DGV_BetUser";
            this.DGV_BetUser.RowHeadersVisible = false;
            this.DGV_BetUser.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DGV_BetUser.RowTemplate.Height = 23;
            this.DGV_BetUser.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV_BetUser.Size = new System.Drawing.Size(264, 64);
            this.DGV_BetUser.TabIndex = 11;
            // 
            // DGC_Name
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Name.DefaultCellStyle = dataGridViewCellStyle20;
            this.DGC_Name.HeaderText = "用户名";
            this.DGC_Name.Name = "DGC_Name";
            this.DGC_Name.ReadOnly = true;
            // 
            // DGC_Money
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.Format = "N2";
            dataGridViewCellStyle21.NullValue = "0";
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGC_Money.DefaultCellStyle = dataGridViewCellStyle21;
            this.DGC_Money.HeaderText = "下注金额(元)";
            this.DGC_Money.Name = "DGC_Money";
            // 
            // DialogConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 362);
            this.Controls.Add(this.DGV_BetUser);
            this.Controls.Add(this.LB_User);
            this.Controls.Add(this.LB_Time);
            this.Controls.Add(this.LB_Score);
            this.Controls.Add(this.LB_League);
            this.Controls.Add(this.BTN_Confirm);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LB_Odd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LB_Match);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下注确认框";
            ((System.ComponentModel.ISupportInitialize)(this.DGV_BetUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LB_Match;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LB_Odd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BTN_Confirm;
        private System.Windows.Forms.Label LB_League;
        private System.Windows.Forms.Label LB_Score;
        private System.Windows.Forms.Label LB_Time;
        private System.Windows.Forms.ListBox LB_User;
        private System.Windows.Forms.DataGridView DGV_BetUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGC_Money;
    }
}