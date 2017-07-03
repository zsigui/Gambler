namespace Gambler.UI
{
    partial class FormToolMap
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
            this.TB_Search = new System.Windows.Forms.TextBox();
            this.BTN_SEARCH = new System.Windows.Forms.Button();
            this.LB_Result = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BTN_CheckAndAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_Search
            // 
            this.TB_Search.Location = new System.Drawing.Point(12, 12);
            this.TB_Search.Name = "TB_Search";
            this.TB_Search.Size = new System.Drawing.Size(215, 21);
            this.TB_Search.TabIndex = 0;
            // 
            // BTN_SEARCH
            // 
            this.BTN_SEARCH.Location = new System.Drawing.Point(233, 10);
            this.BTN_SEARCH.Name = "BTN_SEARCH";
            this.BTN_SEARCH.Size = new System.Drawing.Size(75, 23);
            this.BTN_SEARCH.TabIndex = 1;
            this.BTN_SEARCH.Text = "搜索";
            this.BTN_SEARCH.UseVisualStyleBackColor = true;
            // 
            // LB_Result
            // 
            this.LB_Result.ForeColor = System.Drawing.Color.DarkBlue;
            this.LB_Result.FormattingEnabled = true;
            this.LB_Result.ItemHeight = 12;
            this.LB_Result.Location = new System.Drawing.Point(6, 24);
            this.LB_Result.Name = "LB_Result";
            this.LB_Result.Size = new System.Drawing.Size(279, 100);
            this.LB_Result.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LB_Result);
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 134);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索结果";
            // 
            // BTN_CheckAndAdd
            // 
            this.BTN_CheckAndAdd.Location = new System.Drawing.Point(12, 189);
            this.BTN_CheckAndAdd.Name = "BTN_CheckAndAdd";
            this.BTN_CheckAndAdd.Size = new System.Drawing.Size(296, 36);
            this.BTN_CheckAndAdd.TabIndex = 4;
            this.BTN_CheckAndAdd.Text = "查看设置映射列表";
            this.BTN_CheckAndAdd.UseVisualStyleBackColor = true;
            // 
            // FormToolMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 233);
            this.Controls.Add(this.BTN_CheckAndAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BTN_SEARCH);
            this.Controls.Add(this.TB_Search);
            this.Name = "FormToolMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "名称映射查询工具";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Search;
        private System.Windows.Forms.Button BTN_SEARCH;
        private System.Windows.Forms.ListBox LB_Result;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BTN_CheckAndAdd;
    }
}