using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using TCPServer;

namespace TCPServer
{

    public partial class TCPServerForm : Form
    {
        private TCPServer Server { get; set; }
        private bool NeedRefresh { get; set; }
        private Timer Timer { get; set; }
        private Messenger Messenger { get; set; }
        public TCPServerForm()
        {
            this.InitializeComponent();
            this.Messenger = new Messenger();
            TCPServerSettings serverSettings = new TCPServerSettings(new TCPServerInteractHandler[] { this.Messenger.InteractHandler }, new TCPServerMessageHandler[] { this.MessageHandler, this.Messenger.MessageHandler }, typeof(Message));
            this.Server = new TCPServer(serverSettings);
            this.Timer = new Timer();
            this.Timer.Interval = 1000;
            this.Timer.Tick += this.SyncSocketList;
            this.Timer.Start();
        }

        private void SetCurrentStatus(string ipAddress, int port, int buffer)
        {
            this.labelStateValue.Text = this.Server.ServerStatus;
            this.labelSocketLimitValue.Text = (this.Server.SocketLimit == 0) ? "N/A" : this.Server.SocketLimit.ToString();
            this.labelPortValue.Text = port.ToString();
            this.labelBufferValue.Text = buffer.ToString();
            this.labelIpAddressValue.Text = ipAddress;
        }

        private void buttonInitialize_Click(object sender, EventArgs e)
        {
            if (this.ValidateInput())
            {
                int port = (this.textBoxPort.Text == "Random") ? 0 : Convert.ToInt32(this.textBoxPort.Text);
                int buffer = (this.textBoxBuffer.Text == "Default (65535)") ? 65535 : Convert.ToInt32(this.textBoxBuffer.Text);
                string ipAddress = (this.textBoxIpAddress.Text == "Any") ? "0.0.0.0" : this.textBoxIpAddress.Text;
                uint socketLimit = (this.textBoxSocketLimit.Text == "None") ? 0 : Convert.ToUInt32(this.textBoxSocketLimit.Text);
                this.Server.Initialize(port, ipAddress, buffer, socketLimit);
                if (this.Server.Initialized)
                {
                    this.buttonStart.Enabled = true;
                    this.buttonInitialize.Enabled = false;
                    this.buttonStop.Enabled = false;
                }
            }
            else
                this.Message.Text = "Input error";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.Server.Start();
            if (this.Server.Started)
            {
                this.buttonStart.Enabled = false;
                this.buttonInitialize.Enabled = false;
                this.buttonStop.Enabled = true;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.buttonStart.Enabled = false;
            this.buttonInitialize.Enabled = true;
            this.buttonStop.Enabled = false;
            this.Server.Stop();
        }

        private bool ValidateInput()
        {
            string expressionIpAddress = @"^(\d)+\.(\d)+\.(\d)+\.(\d)+$";
            string expressionPort = @"^(\d)+$";
            Regex regex = new Regex(expressionIpAddress);
            if (this.textBoxIpAddress.Text != "Any")
                if (!regex.Match(this.textBoxIpAddress.Text).Success)
                    return false;
            regex = new Regex(expressionPort);
            if (this.textBoxPort.Text != "Random")
                if (!regex.Match(this.textBoxPort.Text).Success)
                    return false;
            if (this.textBoxSocketLimit.Text != "None")
            {
                if (!regex.Match(this.textBoxSocketLimit.Text).Success)
                    return false;
                int number = Convert.ToInt32(this.textBoxSocketLimit.Text);
                if (number <= 0)
                    return false;
            }
            if (this.textBoxBuffer.Text != "Default (65535)")
            {
                if (!regex.Match(this.textBoxBuffer.Text).Success)
                    return false;
                int number = Convert.ToInt32(this.textBoxBuffer.Text);
                if (number <= 0)
                    return false;
            }
            if (this.textBoxSocketLimit.Text != "None")
            {
                if (!regex.Match(this.textBoxSocketLimit.Text).Success)
                    return false;
                uint number = Convert.ToUInt32(this.textBoxSocketLimit.Text);
                if (number < 0)
                    return false;
            }
            return true;
        }

        private void MessageHandler(object o, TCPServerMessageEventArgs args)
        {
            this.Message.Text = args.Message;
            this.NeedRefresh = true;
        }

        private void SyncSocketList(object o, EventArgs args)
        {
            if (this.NeedRefresh)
            {
                if (this.Server!=null && this.Server.EndPoint != null)
                    this.SetCurrentStatus(this.Server.EndPoint.Address.ToString(), this.Server.EndPoint.Port, this.Server.BufferSize);
                this.SocketList.Items.Clear();
                foreach (var pair in this.Server.Sockets)
                {
                    var item = new ListViewItem(pair.Key.ToString());
                    item.SubItems.Add(pair.Value.Id.ToString());
                    item.SubItems.Add(pair.Value.Port);
                    item.SubItems.Add(pair.Value.IpAddress);
                    this.SocketList.Items.Add(item);
                }
                this.NeedRefresh = false;
            }
        }
    }
}
