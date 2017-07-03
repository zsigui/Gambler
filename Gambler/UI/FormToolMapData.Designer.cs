namespace Gambler.UI
{
    partial class FormToolMapData
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
            this.LB_Data = new System.Windows.Forms.ListBox();
            this.BTN_Del = new System.Windows.Forms.Button();
            this.BTN_Copy = new System.Windows.Forms.Button();
            this.BTN_Clear = new System.Windows.Forms.Button();
            this.TB_Key = new System.Windows.Forms.TextBox();
            this.TB_Val = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BTN_AddOne = new System.Windows.Forms.Button();
            this.BTN_AddFromFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LB_Data
            // 
            this.LB_Data.FormattingEnabled = true;
            this.LB_Data.ItemHeight = 12;
            this.LB_Data.Location = new System.Drawing.Point(12, 120);
            this.LB_Data.Name = "LB_Data";
            this.LB_Data.Size = new System.Drawing.Size(357, 100);
            this.LB_Data.TabIndex = 0;
            // 
            // BTN_Del
            // 
            this.BTN_Del.Location = new System.Drawing.Point(13, 233);
            this.BTN_Del.Name = "BTN_Del";
            this.BTN_Del.Size = new System.Drawing.Size(75, 23);
            this.BTN_Del.TabIndex = 1;
            this.BTN_Del.Text = "删除选中项";
            this.BTN_Del.UseVisualStyleBackColor = true;
            // 
            // BTN_Copy
            // 
            this.BTN_Copy.Location = new System.Drawing.Point(294, 233);
            this.BTN_Copy.Name = "BTN_Copy";
            this.BTN_Copy.Size = new System.Drawing.Size(75, 23);
            this.BTN_Copy.TabIndex = 2;
            this.BTN_Copy.Text = "复制";
            this.BTN_Copy.UseVisualStyleBackColor = true;
            // 
            // BTN_Clear
            // 
            this.BTN_Clear.Location = new System.Drawing.Point(94, 233);
            this.BTN_Clear.Name = "BTN_Clear";
            this.BTN_Clear.Size = new System.Drawing.Size(75, 23);
            this.BTN_Clear.TabIndex = 3;
            this.BTN_Clear.Text = "全部清空";
            this.BTN_Clear.UseVisualStyleBackColor = true;
            // 
            // TB_Key
            // 
            this.TB_Key.Location = new System.Drawing.Point(13, 42);
            this.TB_Key.Name = "TB_Key";
            this.TB_Key.Size = new System.Drawing.Size(100, 21);
            this.TB_Key.TabIndex = 4;
            // 
            // TB_Val
            // 
            this.TB_Val.Location = new System.Drawing.Point(164, 42);
            this.TB_Val.Name = "TB_Val";
            this.TB_Val.Size = new System.Drawing.Size(100, 21);
            this.TB_Val.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "原键名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "对应值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "->";
            // 
            // BTN_AddOne
            // 
            this.BTN_AddOne.Location = new System.Drawing.Point(294, 40);
            this.BTN_AddOne.Name = "BTN_AddOne";
            this.BTN_AddOne.Size = new System.Drawing.Size(75, 23);
            this.BTN_AddOne.TabIndex = 9;
            this.BTN_AddOne.Text = "添加";
            this.BTN_AddOne.UseVisualStyleBackColor = true;
            // 
            // BTN_AddFromFile
            // 
            this.BTN_AddFromFile.Location = new System.Drawing.Point(249, 78);
            this.BTN_AddFromFile.Name = "BTN_AddFromFile";
            this.BTN_AddFromFile.Size = new System.Drawing.Size(120, 23);
            this.BTN_AddFromFile.TabIndex = 10;
            this.BTN_AddFromFile.Text = "从文件添加";
            this.BTN_AddFromFile.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(12, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(357, 88);
            this.label4.TabIndex = 11;
            this.label4.Text = "注意点：\n\n1. 映射键值不能为空\n\n2. 文件添加的话需要保证一行一个键值，且为\"key=val\"\n\n3. 支持一键多值，不过查找默认复制找到的第一个，故尽量一" +
    "对一";
            // 
            // FormToolMapData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 352);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BTN_AddFromFile);
            this.Controls.Add(this.BTN_AddOne);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_Val);
            this.Controls.Add(this.TB_Key);
            this.Controls.Add(this.BTN_Clear);
            this.Controls.Add(this.BTN_Copy);
            this.Controls.Add(this.BTN_Del);
            this.Controls.Add(this.LB_Data);
            this.Name = "FormToolMapData";
            this.Text = "查看添加源";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LB_Data;
        private System.Windows.Forms.Button BTN_Del;
        private System.Windows.Forms.Button BTN_Copy;
        private System.Windows.Forms.Button BTN_Clear;
        private System.Windows.Forms.TextBox TB_Key;
        private System.Windows.Forms.TextBox TB_Val;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BTN_AddOne;
        private System.Windows.Forms.Button BTN_AddFromFile;
        private System.Windows.Forms.Label label4;
    }
}