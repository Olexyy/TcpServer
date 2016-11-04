namespace TCPServerForm
{
    partial class TCPServerForm
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.SocketList = new System.Windows.Forms.ListView();
            this.column_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_thread = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_ip_address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.labelBuffer1 = new System.Windows.Forms.Label();
            this.textBoxBuffer = new System.Windows.Forms.TextBox();
            this.labelSocketLimit1 = new System.Windows.Forms.Label();
            this.textBoxSocketLimit = new System.Windows.Forms.TextBox();
            this.labelPort1 = new System.Windows.Forms.Label();
            this.labelIpAddress1 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Message = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelStateValue = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.labelBufferValue = new System.Windows.Forms.Label();
            this.labelIpAddressValue = new System.Windows.Forms.Label();
            this.labelSocketLimitValue = new System.Windows.Forms.Label();
            this.labelPortValue = new System.Windows.Forms.Label();
            this.labelBuffer2 = new System.Windows.Forms.Label();
            this.labelSocketLimit2 = new System.Windows.Forms.Label();
            this.labelPort2 = new System.Windows.Forms.Label();
            this.labelIpAddress2 = new System.Windows.Forms.Label();
            this.groupBoxControls.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(7, 48);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(112, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(7, 77);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(112, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Controls.Add(this.buttonStop);
            this.groupBoxControls.Controls.Add(this.buttonInitialize);
            this.groupBoxControls.Controls.Add(this.buttonStart);
            this.groupBoxControls.Location = new System.Drawing.Point(300, 327);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(125, 105);
            this.groupBoxControls.TabIndex = 3;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Actions";
            // 
            // SocketList
            // 
            this.SocketList.AutoArrange = false;
            this.SocketList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SocketList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_id,
            this.column_thread,
            this.column_port,
            this.column_ip_address});
            this.SocketList.GridLines = true;
            this.SocketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.SocketList.Location = new System.Drawing.Point(12, 12);
            this.SocketList.MultiSelect = false;
            this.SocketList.Name = "SocketList";
            this.SocketList.Size = new System.Drawing.Size(276, 420);
            this.SocketList.TabIndex = 4;
            this.SocketList.UseCompatibleStateImageBehavior = false;
            this.SocketList.View = System.Windows.Forms.View.Details;
            // 
            // column_id
            // 
            this.column_id.Text = "Thread";
            // 
            // column_thread
            // 
            this.column_thread.Text = "Id";
            this.column_thread.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // column_port
            // 
            this.column_port.Text = "Port";
            // 
            // column_ip_address
            // 
            this.column_ip_address.Text = "Address";
            this.column_ip_address.Width = 95;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.labelBuffer1);
            this.groupBoxSettings.Controls.Add(this.textBoxBuffer);
            this.groupBoxSettings.Controls.Add(this.labelSocketLimit1);
            this.groupBoxSettings.Controls.Add(this.textBoxSocketLimit);
            this.groupBoxSettings.Controls.Add(this.labelPort1);
            this.groupBoxSettings.Controls.Add(this.labelIpAddress1);
            this.groupBoxSettings.Controls.Add(this.textBoxPort);
            this.groupBoxSettings.Controls.Add(this.textBoxIpAddress);
            this.groupBoxSettings.Location = new System.Drawing.Point(300, 12);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(125, 183);
            this.groupBoxSettings.TabIndex = 5;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // labelBuffer1
            // 
            this.labelBuffer1.AutoSize = true;
            this.labelBuffer1.Location = new System.Drawing.Point(8, 141);
            this.labelBuffer1.Name = "labelBuffer1";
            this.labelBuffer1.Size = new System.Drawing.Size(59, 13);
            this.labelBuffer1.TabIndex = 7;
            this.labelBuffer1.Text = "Buffer size:";
            // 
            // textBoxBuffer
            // 
            this.textBoxBuffer.Location = new System.Drawing.Point(9, 157);
            this.textBoxBuffer.Name = "textBoxBuffer";
            this.textBoxBuffer.Size = new System.Drawing.Size(108, 20);
            this.textBoxBuffer.TabIndex = 6;
            this.textBoxBuffer.Text = "Default (1024)";
            // 
            // labelSocketLimit1
            // 
            this.labelSocketLimit1.AutoSize = true;
            this.labelSocketLimit1.Location = new System.Drawing.Point(6, 100);
            this.labelSocketLimit1.Name = "labelSocketLimit1";
            this.labelSocketLimit1.Size = new System.Drawing.Size(64, 13);
            this.labelSocketLimit1.TabIndex = 5;
            this.labelSocketLimit1.Text = "Socket limit:";
            // 
            // textBoxSocketLimit
            // 
            this.textBoxSocketLimit.Location = new System.Drawing.Point(9, 116);
            this.textBoxSocketLimit.Name = "textBoxSocketLimit";
            this.textBoxSocketLimit.Size = new System.Drawing.Size(108, 20);
            this.textBoxSocketLimit.TabIndex = 4;
            this.textBoxSocketLimit.Text = "None";
            // 
            // labelPort1
            // 
            this.labelPort1.AutoSize = true;
            this.labelPort1.Location = new System.Drawing.Point(6, 58);
            this.labelPort1.Name = "labelPort1";
            this.labelPort1.Size = new System.Drawing.Size(29, 13);
            this.labelPort1.TabIndex = 3;
            this.labelPort1.Text = "Port:";
            // 
            // labelIpAddress1
            // 
            this.labelIpAddress1.AutoSize = true;
            this.labelIpAddress1.Location = new System.Drawing.Point(6, 16);
            this.labelIpAddress1.Name = "labelIpAddress1";
            this.labelIpAddress1.Size = new System.Drawing.Size(59, 13);
            this.labelIpAddress1.TabIndex = 2;
            this.labelIpAddress1.Text = "Ip address:";
            this.labelIpAddress1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(9, 74);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(108, 20);
            this.textBoxPort.TabIndex = 1;
            this.textBoxPort.Text = "Random";
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(9, 32);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(109, 20);
            this.textBoxIpAddress.TabIndex = 0;
            this.textBoxIpAddress.Text = "Any";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Message});
            this.statusStrip1.Location = new System.Drawing.Point(0, 443);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelStateValue);
            this.groupBox1.Controls.Add(this.labelState);
            this.groupBox1.Controls.Add(this.labelBufferValue);
            this.groupBox1.Controls.Add(this.labelIpAddressValue);
            this.groupBox1.Controls.Add(this.labelSocketLimitValue);
            this.groupBox1.Controls.Add(this.labelPortValue);
            this.groupBox1.Controls.Add(this.labelBuffer2);
            this.groupBox1.Controls.Add(this.labelSocketLimit2);
            this.groupBox1.Controls.Add(this.labelPort2);
            this.groupBox1.Controls.Add(this.labelIpAddress2);
            this.groupBox1.Location = new System.Drawing.Point(300, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(125, 120);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current status";
            // 
            // labelStateValue
            // 
            this.labelStateValue.AutoSize = true;
            this.labelStateValue.Location = new System.Drawing.Point(71, 100);
            this.labelStateValue.Name = "labelStateValue";
            this.labelStateValue.Size = new System.Drawing.Size(33, 13);
            this.labelStateValue.TabIndex = 11;
            this.labelStateValue.Text = "N/A  ";
            this.labelStateValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Enabled = false;
            this.labelState.Location = new System.Drawing.Point(6, 100);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(35, 13);
            this.labelState.TabIndex = 12;
            this.labelState.Text = "State:";
            this.labelState.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelBufferValue
            // 
            this.labelBufferValue.AutoSize = true;
            this.labelBufferValue.Location = new System.Drawing.Point(71, 79);
            this.labelBufferValue.Name = "labelBufferValue";
            this.labelBufferValue.Size = new System.Drawing.Size(27, 13);
            this.labelBufferValue.TabIndex = 4;
            this.labelBufferValue.Text = "N/A";
            this.labelBufferValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelIpAddressValue
            // 
            this.labelIpAddressValue.AutoSize = true;
            this.labelIpAddressValue.Location = new System.Drawing.Point(71, 20);
            this.labelIpAddressValue.Name = "labelIpAddressValue";
            this.labelIpAddressValue.Size = new System.Drawing.Size(27, 13);
            this.labelIpAddressValue.TabIndex = 5;
            this.labelIpAddressValue.Text = "N/A";
            this.labelIpAddressValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSocketLimitValue
            // 
            this.labelSocketLimitValue.AutoSize = true;
            this.labelSocketLimitValue.Location = new System.Drawing.Point(71, 60);
            this.labelSocketLimitValue.Name = "labelSocketLimitValue";
            this.labelSocketLimitValue.Size = new System.Drawing.Size(27, 13);
            this.labelSocketLimitValue.TabIndex = 10;
            this.labelSocketLimitValue.Text = "N/A";
            this.labelSocketLimitValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPortValue
            // 
            this.labelPortValue.AutoSize = true;
            this.labelPortValue.Location = new System.Drawing.Point(71, 40);
            this.labelPortValue.Name = "labelPortValue";
            this.labelPortValue.Size = new System.Drawing.Size(27, 13);
            this.labelPortValue.TabIndex = 9;
            this.labelPortValue.Text = "N/A";
            this.labelPortValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelBuffer2
            // 
            this.labelBuffer2.AutoSize = true;
            this.labelBuffer2.Enabled = false;
            this.labelBuffer2.Location = new System.Drawing.Point(6, 79);
            this.labelBuffer2.Name = "labelBuffer2";
            this.labelBuffer2.Size = new System.Drawing.Size(38, 13);
            this.labelBuffer2.TabIndex = 8;
            this.labelBuffer2.Text = "Buffer:";
            this.labelBuffer2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSocketLimit2
            // 
            this.labelSocketLimit2.AutoSize = true;
            this.labelSocketLimit2.Enabled = false;
            this.labelSocketLimit2.Location = new System.Drawing.Point(6, 60);
            this.labelSocketLimit2.Name = "labelSocketLimit2";
            this.labelSocketLimit2.Size = new System.Drawing.Size(64, 13);
            this.labelSocketLimit2.TabIndex = 7;
            this.labelSocketLimit2.Text = "Socket limit:";
            // 
            // labelPort2
            // 
            this.labelPort2.AutoSize = true;
            this.labelPort2.Enabled = false;
            this.labelPort2.Location = new System.Drawing.Point(6, 40);
            this.labelPort2.Name = "labelPort2";
            this.labelPort2.Size = new System.Drawing.Size(29, 13);
            this.labelPort2.TabIndex = 6;
            this.labelPort2.Text = "Port:";
            // 
            // labelIpAddress2
            // 
            this.labelIpAddress2.AutoSize = true;
            this.labelIpAddress2.Enabled = false;
            this.labelIpAddress2.Location = new System.Drawing.Point(6, 20);
            this.labelIpAddress2.Name = "labelIpAddress2";
            this.labelIpAddress2.Size = new System.Drawing.Size(59, 13);
            this.labelIpAddress2.TabIndex = 3;
            this.labelIpAddress2.Text = "Ip address:";
            this.labelIpAddress2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TCPServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 465);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.SocketList);
            this.Controls.Add(this.groupBoxControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "TCPServerForm";
            this.ShowIcon = false;
            this.Text = "TCP Server";
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInitialize;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.GroupBox groupBoxControls;
        private System.Windows.Forms.ListView SocketList;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.ColumnHeader column_id;
        private System.Windows.Forms.ColumnHeader column_thread;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label labelIpAddress1;
        private System.Windows.Forms.Label labelPort1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Message;
        private System.Windows.Forms.Label labelSocketLimit1;
        private System.Windows.Forms.TextBox textBoxSocketLimit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelBuffer2;
        private System.Windows.Forms.Label labelSocketLimit2;
        private System.Windows.Forms.Label labelPort2;
        private System.Windows.Forms.Label labelIpAddress2;
        private System.Windows.Forms.Label labelBufferValue;
        private System.Windows.Forms.Label labelIpAddressValue;
        private System.Windows.Forms.Label labelSocketLimitValue;
        private System.Windows.Forms.Label labelPortValue;
        private System.Windows.Forms.TextBox textBoxBuffer;
        private System.Windows.Forms.Label labelBuffer1;
        private System.Windows.Forms.Label labelStateValue;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.ColumnHeader column_port;
        private System.Windows.Forms.ColumnHeader column_ip_address;
    }
}

