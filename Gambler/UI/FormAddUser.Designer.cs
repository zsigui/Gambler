namespace Gambler.UI
{
    partial class FormAddUser
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CB_Type = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Account = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_ProxyPwd = new System.Windows.Forms.TextBox();
            this.TB_ProxyAccount = new System.Windows.Forms.TextBox();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.TB_ProxyAdress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BTN_Add = new System.Windows.Forms.Button();
            this.EP_Validated = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_Validated)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_Type);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.TB_Password);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TB_Account);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "账号信息";
            // 
            // CB_Type
            // 
            this.CB_Type.FormattingEnabled = true;
            this.CB_Type.Items.AddRange(new object[] {
            "新葡京155",
            "新葡京469"});
            this.CB_Type.Location = new System.Drawing.Point(114, 97);
            this.CB_Type.Name = "CB_Type";
            this.CB_Type.Size = new System.Drawing.Size(121, 20);
            this.CB_Type.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "类型： ";
            // 
            // TB_Password
            // 
            this.TB_Password.Location = new System.Drawing.Point(114, 63);
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.Size = new System.Drawing.Size(220, 21);
            this.TB_Password.TabIndex = 3;
            this.TB_Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAddUser_KeyDown);
            this.TB_Password.Validating += new System.ComponentModel.CancelEventHandler(this.TB_Password_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名：";
            // 
            // TB_Account
            // 
            this.TB_Account.Location = new System.Drawing.Point(114, 30);
            this.TB_Account.MaxLength = 40;
            this.TB_Account.Name = "TB_Account";
            this.TB_Account.ShortcutsEnabled = false;
            this.TB_Account.Size = new System.Drawing.Size(220, 21);
            this.TB_Account.TabIndex = 0;
            this.TB_Account.WordWrap = false;
            this.TB_Account.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Account_KeyDown);
            this.TB_Account.Validating += new System.ComponentModel.CancelEventHandler(this.TB_Account_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_ProxyPwd);
            this.groupBox2.Controls.Add(this.TB_ProxyAccount);
            this.groupBox2.Controls.Add(this.TB_Port);
            this.groupBox2.Controls.Add(this.TB_ProxyAdress);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 169);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "代理信息（选填）";
            // 
            // TB_ProxyPwd
            // 
            this.TB_ProxyPwd.Location = new System.Drawing.Point(114, 130);
            this.TB_ProxyPwd.Name = "TB_ProxyPwd";
            this.TB_ProxyPwd.Size = new System.Drawing.Size(220, 21);
            this.TB_ProxyPwd.TabIndex = 7;
            // 
            // TB_ProxyAccount
            // 
            this.TB_ProxyAccount.Location = new System.Drawing.Point(114, 97);
            this.TB_ProxyAccount.Name = "TB_ProxyAccount";
            this.TB_ProxyAccount.Size = new System.Drawing.Size(220, 21);
            this.TB_ProxyAccount.TabIndex = 6;
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(114, 64);
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(220, 21);
            this.TB_Port.TabIndex = 5;
            // 
            // TB_ProxyAdress
            // 
            this.TB_ProxyAdress.Location = new System.Drawing.Point(114, 31);
            this.TB_ProxyAdress.Name = "TB_ProxyAdress";
            this.TB_ProxyAdress.Size = new System.Drawing.Size(220, 21);
            this.TB_ProxyAdress.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "密码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "用户名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "端口：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "代理地址：";
            // 
            // BTN_Add
            // 
            this.BTN_Add.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BTN_Add.Location = new System.Drawing.Point(167, 342);
            this.BTN_Add.Name = "BTN_Add";
            this.BTN_Add.Size = new System.Drawing.Size(105, 30);
            this.BTN_Add.TabIndex = 2;
            this.BTN_Add.Text = "确  定";
            this.BTN_Add.UseVisualStyleBackColor = true;
            this.BTN_Add.Click += new System.EventHandler(this.BTN_Add_Click);
            // 
            // EP_Validated
            // 
            this.EP_Validated.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.EP_Validated.ContainerControl = this;
            // 
            // FormAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(438, 388);
            this.Controls.Add(this.BTN_Add);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddUser";
            this.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加账户";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAddUser_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAddUser_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP_Validated)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Account;
        private System.Windows.Forms.TextBox TB_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TB_ProxyPwd;
        private System.Windows.Forms.TextBox TB_ProxyAccount;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.TextBox TB_ProxyAdress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BTN_Add;
        private System.Windows.Forms.ErrorProvider EP_Validated;
        private System.Windows.Forms.ComboBox CB_Type;
        private System.Windows.Forms.Label label7;
    }
}