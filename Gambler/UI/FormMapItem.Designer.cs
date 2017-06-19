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
            this.BTN_AddMapItem = new System.Windows.Forms.Button();
            this.TB_MapKeyItem = new System.Windows.Forms.TextBox();
            this.BTN_Save = new System.Windows.Forms.Button();
            this.BTN_DelMapItem = new System.Windows.Forms.Button();
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
            // BTN_AddMapItem
            // 
            this.BTN_AddMapItem.Location = new System.Drawing.Point(525, 50);
            this.BTN_AddMapItem.Name = "BTN_AddMapItem";
            this.BTN_AddMapItem.Size = new System.Drawing.Size(87, 23);
            this.BTN_AddMapItem.TabIndex = 5;
            this.BTN_AddMapItem.Text = "添加新映射组";
            this.BTN_AddMapItem.UseVisualStyleBackColor = true;
            this.BTN_AddMapItem.Click += new System.EventHandler(this.BTN_AddMapItem_Click);
            // 
            // TB_MapKeyItem
            // 
            this.TB_MapKeyItem.Location = new System.Drawing.Point(512, 23);
            this.TB_MapKeyItem.Name = "TB_MapKeyItem";
            this.TB_MapKeyItem.Size = new System.Drawing.Size(100, 21);
            this.TB_MapKeyItem.TabIndex = 6;
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
            // BTN_DelMapItem
            // 
            this.BTN_DelMapItem.Location = new System.Drawing.Point(308, 22);
            this.BTN_DelMapItem.Name = "BTN_DelMapItem";
            this.BTN_DelMapItem.Size = new System.Drawing.Size(100, 22);
            this.BTN_DelMapItem.TabIndex = 8;
            this.BTN_DelMapItem.Text = "删除当前映射组";
            this.BTN_DelMapItem.UseVisualStyleBackColor = true;
            this.BTN_DelMapItem.Click += new System.EventHandler(this.BTN_DelMapItem_Click);
            // 
            // FormMapItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 404);
            this.Controls.Add(this.BTN_DelMapItem);
            this.Controls.Add(this.BTN_Save);
            this.Controls.Add(this.TB_MapKeyItem);
            this.Controls.Add(this.BTN_AddMapItem);
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
        private System.Windows.Forms.Button BTN_AddMapItem;
        private System.Windows.Forms.TextBox TB_MapKeyItem;
        private System.Windows.Forms.Button BTN_Save;
        private System.Windows.Forms.Button BTN_DelMapItem;
    }
}