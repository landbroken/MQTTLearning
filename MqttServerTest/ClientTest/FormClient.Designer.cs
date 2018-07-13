namespace MqttServerTest
{
    partial class FormClient
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
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.txtSubTopic = new System.Windows.Forms.TextBox();
            this.BtnPublish = new System.Windows.Forms.Button();
            this.txtPubTopic = new System.Windows.Forms.TextBox();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.txtReceiveMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPsw = new System.Windows.Forms.TextBox();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(15, 394);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(122, 23);
            this.btnSubscribe.TabIndex = 0;
            this.btnSubscribe.Text = "btnSubscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.BtnSubscribe_ClickAsync);
            // 
            // txtSubTopic
            // 
            this.txtSubTopic.Location = new System.Drawing.Point(15, 275);
            this.txtSubTopic.Multiline = true;
            this.txtSubTopic.Name = "txtSubTopic";
            this.txtSubTopic.Size = new System.Drawing.Size(403, 101);
            this.txtSubTopic.TabIndex = 1;
            // 
            // BtnPublish
            // 
            this.BtnPublish.Location = new System.Drawing.Point(566, 306);
            this.BtnPublish.Name = "BtnPublish";
            this.BtnPublish.Size = new System.Drawing.Size(75, 23);
            this.BtnPublish.TabIndex = 2;
            this.BtnPublish.Text = "BtnPublish";
            this.BtnPublish.UseVisualStyleBackColor = true;
            this.BtnPublish.Click += new System.EventHandler(this.BtnPublish_Click);
            // 
            // txtPubTopic
            // 
            this.txtPubTopic.Location = new System.Drawing.Point(466, 42);
            this.txtPubTopic.Multiline = true;
            this.txtPubTopic.Name = "txtPubTopic";
            this.txtPubTopic.Size = new System.Drawing.Size(341, 42);
            this.txtPubTopic.TabIndex = 3;
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.Location = new System.Drawing.Point(466, 134);
            this.txtSendMessage.Multiline = true;
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(341, 166);
            this.txtSendMessage.TabIndex = 4;
            // 
            // txtReceiveMessage
            // 
            this.txtReceiveMessage.Location = new System.Drawing.Point(813, 42);
            this.txtReceiveMessage.Multiline = true;
            this.txtReceiveMessage.Name = "txtReceiveMessage";
            this.txtReceiveMessage.Size = new System.Drawing.Size(450, 405);
            this.txtReceiveMessage.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(463, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "please input send mssage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(865, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "receiveMsg";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(99, 57);
            this.txtUsername.Multiline = true;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(115, 31);
            this.txtUsername.TabIndex = 8;
            this.txtUsername.Text = "username001";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "psw";
            // 
            // txtPsw
            // 
            this.txtPsw.Location = new System.Drawing.Point(99, 94);
            this.txtPsw.Multiline = true;
            this.txtPsw.Name = "txtPsw";
            this.txtPsw.Size = new System.Drawing.Size(115, 31);
            this.txtPsw.TabIndex = 11;
            this.txtPsw.Text = "psw001";
            // 
            // btnLogIn
            // 
            this.btnLogIn.Location = new System.Drawing.Point(23, 150);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(121, 23);
            this.btnLogIn.TabIndex = 12;
            this.btnLogIn.Text = "btnLogIn";
            this.btnLogIn.UseVisualStyleBackColor = true;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(23, 180);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(121, 23);
            this.btnLogout.TabIndex = 13;
            this.btnLogout.Text = "btnLogout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(463, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "please input public topic";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtIp);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtClientId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.btnLogout);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnLogIn);
            this.groupBox1.Controls.Add(this.txtPsw);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 247);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "UserInfo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(220, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "ip";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(284, 13);
            this.txtIp.Multiline = true;
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(115, 31);
            this.txtIp.TabIndex = 18;
            this.txtIp.Text = "192.168.88.3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(220, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(284, 57);
            this.txtPort.Multiline = true;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(115, 31);
            this.txtPort.TabIndex = 16;
            this.txtPort.Text = "8222";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "clientId";
            // 
            // txtClientId
            // 
            this.txtClientId.Location = new System.Drawing.Point(99, 20);
            this.txtClientId.Multiline = true;
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(115, 31);
            this.txtClientId.TabIndex = 14;
            this.txtClientId.Text = "client001";
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 471);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReceiveMessage);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.txtPubTopic);
            this.Controls.Add(this.BtnPublish);
            this.Controls.Add(this.txtSubTopic);
            this.Controls.Add(this.btnSubscribe);
            this.Name = "FormClient";
            this.Text = "FormClient";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.TextBox txtSubTopic;
        private System.Windows.Forms.Button BtnPublish;
        private System.Windows.Forms.TextBox txtPubTopic;
        private System.Windows.Forms.TextBox txtSendMessage;
        private System.Windows.Forms.TextBox txtReceiveMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPsw;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIp;
    }
}

