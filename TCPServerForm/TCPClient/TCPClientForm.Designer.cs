namespace TCPServerClient
{
    partial class TCPClientForm
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
            this.buttonInitialize = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.MessageList = new System.Windows.Forms.ListView();
            this.column_user = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_text = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBoxFriendsOnline = new System.Windows.Forms.TextBox();
            this.labelBufferSize = new System.Windows.Forms.Label();
            this.textBoxBufferSize = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Message = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBoxNewMessage = new System.Windows.Forms.TextBox();
            this.buttonPost = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxControls.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInitialize
            // 
            this.buttonInitialize.Location = new System.Drawing.Point(7, 19);
            this.buttonInitialize.Name = "buttonInitialize";
            this.buttonInitialize.Size = new System.Drawing.Size(112, 23);
            this.buttonInitialize.TabIndex = 0;
            this.buttonInitialize.Text = "Initialize";
            this.buttonInitialize.UseVisualStyleBackColor = true;
            this.buttonInitialize.Click += new System.EventHandler(this.buttonInitialize_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(7, 48);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(112, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(5, 135);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(112, 23);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Controls.Add(this.buttonLogout);
            this.groupBoxControls.Controls.Add(this.buttonLogin);
            this.groupBoxControls.Controls.Add(this.buttonDisconnect);
            this.groupBoxControls.Controls.Add(this.buttonInitialize);
            this.groupBoxControls.Controls.Add(this.buttonConnect);
            this.groupBoxControls.Location = new System.Drawing.Point(300, 277);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(125, 168);
            this.groupBoxControls.TabIndex = 3;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Actions";
            // 
            // buttonLogout
            // 
            this.buttonLogout.Enabled = false;
            this.buttonLogout.Location = new System.Drawing.Point(7, 106);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(112, 23);
            this.buttonLogout.TabIndex = 4;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Enabled = false;
            this.buttonLogin.Location = new System.Drawing.Point(6, 77);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(112, 23);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // MessageList
            // 
            this.MessageList.AutoArrange = false;
            this.MessageList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MessageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_user,
            this.column_time,
            this.column_text});
            this.MessageList.GridLines = true;
            this.MessageList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.MessageList.Location = new System.Drawing.Point(12, 11);
            this.MessageList.MultiSelect = false;
            this.MessageList.Name = "MessageList";
            this.MessageList.Size = new System.Drawing.Size(274, 316);
            this.MessageList.TabIndex = 4;
            this.MessageList.UseCompatibleStateImageBehavior = false;
            this.MessageList.View = System.Windows.Forms.View.Details;
            // 
            // column_user
            // 
            this.column_user.Text = "User";
            // 
            // column_time
            // 
            this.column_time.Text = "Time";
            this.column_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // column_text
            // 
            this.column_text.Text = "Text";
            this.column_text.Width = 151;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.labelBufferSize);
            this.groupBoxSettings.Controls.Add(this.textBoxBufferSize);
            this.groupBoxSettings.Controls.Add(this.labelPort);
            this.groupBoxSettings.Controls.Add(this.labelIpAddress);
            this.groupBoxSettings.Controls.Add(this.textBoxPort);
            this.groupBoxSettings.Controls.Add(this.textBoxIpAddress);
            this.groupBoxSettings.Location = new System.Drawing.Point(300, 12);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(125, 148);
            this.groupBoxSettings.TabIndex = 5;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(5, 60);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(75, 13);
            this.labelUserName.TabIndex = 9;
            this.labelUserName.Text = "Friends online:";
            // 
            // textBoxFriendsOnline
            // 
            this.textBoxFriendsOnline.Enabled = false;
            this.textBoxFriendsOnline.Location = new System.Drawing.Point(6, 76);
            this.textBoxFriendsOnline.Name = "textBoxFriendsOnline";
            this.textBoxFriendsOnline.Size = new System.Drawing.Size(108, 20);
            this.textBoxFriendsOnline.TabIndex = 8;
            this.textBoxFriendsOnline.Text = "0";
            // 
            // labelBufferSize
            // 
            this.labelBufferSize.AutoSize = true;
            this.labelBufferSize.Location = new System.Drawing.Point(8, 100);
            this.labelBufferSize.Name = "labelBufferSize";
            this.labelBufferSize.Size = new System.Drawing.Size(59, 13);
            this.labelBufferSize.TabIndex = 7;
            this.labelBufferSize.Text = "Buffer size:";
            // 
            // textBoxBufferSize
            // 
            this.textBoxBufferSize.Location = new System.Drawing.Point(7, 116);
            this.textBoxBufferSize.Name = "textBoxBufferSize";
            this.textBoxBufferSize.Size = new System.Drawing.Size(108, 20);
            this.textBoxBufferSize.TabIndex = 6;
            this.textBoxBufferSize.Text = "Default (1024)";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(6, 58);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 13);
            this.labelPort.TabIndex = 3;
            this.labelPort.Text = "Port:";
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(6, 16);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(59, 13);
            this.labelIpAddress.TabIndex = 2;
            this.labelIpAddress.Text = "Ip address:";
            this.labelIpAddress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(9, 74);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(108, 20);
            this.textBoxPort.TabIndex = 1;
            this.textBoxPort.Text = "Enter port ...";
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(9, 32);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(109, 20);
            this.textBoxIpAddress.TabIndex = 0;
            this.textBoxIpAddress.Text = "Enter IP Address ...";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Message});
            this.statusStrip1.Location = new System.Drawing.Point(0, 454);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(436, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Message
            // 
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(160, 17);
            this.Message.Text = "Select settings and initialize...";
            // 
            // textBoxNewMessage
            // 
            this.textBoxNewMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNewMessage.Location = new System.Drawing.Point(6, 18);
            this.textBoxNewMessage.Multiline = true;
            this.textBoxNewMessage.Name = "textBoxNewMessage";
            this.textBoxNewMessage.Size = new System.Drawing.Size(262, 52);
            this.textBoxNewMessage.TabIndex = 7;
            // 
            // buttonPost
            // 
            this.buttonPost.Enabled = false;
            this.buttonPost.Location = new System.Drawing.Point(146, 76);
            this.buttonPost.Name = "buttonPost";
            this.buttonPost.Size = new System.Drawing.Size(122, 23);
            this.buttonPost.TabIndex = 9;
            this.buttonPost.Text = "Post";
            this.buttonPost.UseVisualStyleBackColor = true;
            this.buttonPost.Click += new System.EventHandler(this.buttonPost_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Enabled = false;
            this.buttonClear.Location = new System.Drawing.Point(6, 76);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(134, 23);
            this.buttonClear.TabIndex = 10;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "User name:";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(5, 32);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(108, 20);
            this.textBoxUserName.TabIndex = 11;
            this.textBoxUserName.Text = "Enter username ...";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPost);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.textBoxNewMessage);
            this.groupBox1.Location = new System.Drawing.Point(12, 333);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 112);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Posting new message";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxFriendsOnline);
            this.groupBox2.Controls.Add(this.textBoxUserName);
            this.groupBox2.Controls.Add(this.labelUserName);
            this.groupBox2.Location = new System.Drawing.Point(300, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(125, 105);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Messenger";
            // 
            // TCPClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 476);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.MessageList);
            this.Controls.Add(this.groupBoxControls);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "TCPClientForm";
            this.ShowIcon = false;
            this.Text = "TCP Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TCPClientForm_FormClosed);
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInitialize;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.GroupBox groupBoxControls;
        private System.Windows.Forms.ListView MessageList;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.ColumnHeader column_user;
        private System.Windows.Forms.ColumnHeader column_time;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label labelIpAddress;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Message;
        private System.Windows.Forms.TextBox textBoxBufferSize;
        private System.Windows.Forms.Label labelBufferSize;
        private System.Windows.Forms.ColumnHeader column_text;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.TextBox textBoxFriendsOnline;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxNewMessage;
        private System.Windows.Forms.Button buttonPost;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

