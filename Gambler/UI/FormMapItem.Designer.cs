namespace Gambler.UI
{
    partial class FormMapItem
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
            this.CB_ItemKey = new System.Windows.Forms.ComboBox();
            this.RTB_MapItems = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BTN_Save = new System.Windows.Forms.Button();
            this.CB_ForCopy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前选择的映射组：";
            // 
            // CB_ItemKey
            // 
            this.CB_ItemKey.FormattingEnabled = true;
            this.CB_ItemKey.Items.AddRange(new object[] {
            "469355.com",
            "1559501.com"});
            this.CB_ItemKey.Location = new System.Drawing.Point(154, 24);
            this.CB_ItemKey.Name = "CB_ItemKey";
            this.CB_ItemKey.Size = new System.Drawing.Size(136, 20);
            this.CB_ItemKey.TabIndex = 1;
            this.CB_ItemKey.SelectedIndexChanged += new System.EventHandler(this.CB_ItemKey_SelectedIndexChanged);
            // 
            // RTB_MapItems
            // 
            this.RTB_MapItems.Location = new System.Drawing.Point(37, 82);
            this.RTB_MapItems.Name = "RTB_MapItems";
            this.RTB_MapItems.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RTB_MapItems.Size = new System.Drawing.Size(575, 236);
            this.RTB_MapItems.TabIndex = 2;
            this.RTB_MapItems.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "该映射组里的映射关系：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(35, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(437, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "（P.S. 映射格式形如 “key=val”，一行一个，注意键值之间只有一个“=”号）";
            // 
            // BTN_Save
            // 
            this.BTN_Save.Location = new System.Drawing.Point(230, 360);
            this.BTN_Save.Name = "BTN_Save";
            this.BTN_Save.Size = new System.Drawing.Size(187, 32);
            this.BTN_Save.TabIndex = 7;
            this.BTN_Save.Text = "保存当前映射组变更";
            this.BTN_Save.UseVisualStyleBackColor = true;
            this.BTN_Save.Click += new System.EventHandler(this.BTN_Save_Click);
            // 
            // CB_ForCopy
            // 
            this.CB_ForCopy.FormattingEnabled = true;
            this.CB_ForCopy.Items.AddRange(new object[] {
            "469355.com",
            "1559501.com"});
            this.CB_ForCopy.Location = new System.Drawing.Point(473, 24);
            this.CB_ForCopy.Name = "CB_ForCopy";
            this.CB_ForCopy.Size = new System.Drawing.Size(121, 20);
            this.CB_ForCopy.TabIndex = 8;
            this.CB_ForCopy.SelectedIndexChanged += new System.EventHandler(this.CB_ForCopy_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(342, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "自动复制首选映射组：";
            // 
            // FormMapItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 404);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CB_ForCopy);
            this.Controls.Add(this.BTN_Save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RTB_MapItems);
            this.Controls.Add(this.CB_ItemKey);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMapItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加映射信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMapItem_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_ItemKey;
        private System.Windows.Forms.RichTextBox RTB_MapItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BTN_Save;
        private System.Windows.Forms.ComboBox CB_ForCopy;
        private System.Windows.Forms.Label label4;
    }
}