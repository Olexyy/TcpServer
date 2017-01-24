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

namespace TCPServerClientForm
{
    public partial class TCPClientForm : Form
    {
        private TCPServerClient Client { get; set; }
        private MessengerClient MessengerClient { get; set; }
        public TCPClientForm()
        {
            this.InitializeComponent();
            this.MessengerClient = new MessengerClient();
        }
        private void buttonInitialize_Click(object sender, EventArgs e)
        {
            if (this.ValidateInput())
            {
                int port =  Convert.ToInt32(this.textBoxPort.Text);
                int buffer = (this.textBoxBufferSize.Text == "Default (1024)") ? 1024 : Convert.ToInt32(this.textBoxBufferSize.Text);
                string ipAddress = (this.textBoxIpAddress.Text == "Enter IP Address ...") ? "127.0.0.1" : this.textBoxIpAddress.Text;
                TCPServerClientSettings clientSettings = new TCPServerClientSettings(this.InteractHandler, this.MessageHandler, typeof(Message));
                if (this.Client != null && this.Client.BaseSocket != null)
                    this.Client.Dispose();
                this.Client = new TCPServerClient(clientSettings);
                this.Client.ClientInteractEvent += this.MessengerClient.InteractHandler;
                this.Client.Initialize(port, ipAddress, buffer);
                this.ButtonsPattern(true, true, false, false, false, false, false);
            }
            else
                this.Message.Text = "Input error";
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (this.Client == null)
                this.buttonInitialize_Click(null, null);
            this.Client.Connect();
            // TODO: This should be handled by async message handler of server
            while (!this.Client.Connected);
            this.ButtonsPattern(false, false, true, true, false, false, false);
        }
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            this.Client.Disonnect();
            // TODO: This should be handled by async message handler of server
            while (this.Client.Connected);
            this.ButtonsPattern(true, true, false, false, false, false, false);
            if(this.Client != null && this.Client.BaseSocket != null)
                this.Client.Dispose();
            this.Client = null;
        }
        private bool ValidateInput()
        {
            string expressionIpAddress = @"^(\d)+\.(\d)+\.(\d)+\.(\d)+$";
            string expressionPort = @"^(\d)+$";
            Regex regex = new Regex(expressionIpAddress);
            if ((this.textBoxIpAddress.Text == String.Empty || !regex.Match(this.textBoxIpAddress.Text).Success) && this.textBoxIpAddress.Text != "Enter IP Address ...")
                return false;
            regex = new Regex(expressionPort);
            if (this.textBoxPort.Text == String.Empty || !regex.Match(this.textBoxPort.Text).Success)
                return false;
            if (this.textBoxBufferSize.Text != "Default (1024)")
            {
                if (!regex.Match(this.textBoxBufferSize.Text).Success)
                    return false;
                int number = Convert.ToInt32(this.textBoxBufferSize.Text);
                if (number <= 0)
                    return false;
            }
            return true;
        }
        private void InteractHandler(object o, TCPServerClientInteractEventArgs args)
        {
            Message message = args.Object as Message;
            switch (message.MessageType)
            {
                case MessageTypes.login_success:
                    this.Invoke(new Action(()=> {
                        this.Message.Text = "[Messenger]: Logged in...";
                        this.ButtonsPattern(false, false, true, false, true, true, true);
                    }));
                    break;
                case MessageTypes.logout_success:
                    this.Invoke(new Action(() => {
                        this.Message.Text = "[Messenger]: Logged out...";
                        this.ButtonsPattern(false, false, true, true, false, false, false);
                    }));
                    break;
                case MessageTypes.post:
                    this.Invoke(new Action(() => {
                        this.Message.Text = "[Messenger]: New messages received...";
                        this.MessageList.Items.Add(message.User.Name);
                        this.MessageList.Items[this.MessageList.Items.Count - 1].SubItems.Add(DateTime.Now.ToShortDateString());
                        this.MessageList.Items[this.MessageList.Items.Count - 1].SubItems.Add(message.Text);
                    }));
                    break;
                case MessageTypes.post_success:
                    this.Invoke(new Action(() => {
                        this.Message.Text = "[Messenger]: Message posted...";
                        this.ButtonsPattern(false, false, true, false, true, true, true);
                    }));
                    break;
                default:
                    break;
            }
        }
        private void MessageHandler(object o, TCPServerClientMessageEventArgs args)
        {
            this.Invoke(new Action(()=>this.Message.Text = args.Message));
            switch (args.MessageType)
            {
                case TCPServerClientMessages.Disconnected:
                    this.Invoke(new Action(()=> {
                        this.ButtonsPattern(true, true, false, false, false, false, false);
                    }));
                    if (this.Client.BaseSocket != null)
                        this.Client.Dispose();
                    break;
                default: break;
            }
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            User user = new User(this.textBoxUserName.Text);
            Message message = new Message(MessageTypes.login, user);
            TCPServerClientInteractEventArgs args = new TCPServerClientInteractEventArgs(message, true, true);
            this.Client.Send(args);
        }
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Message message = new Message(MessageTypes.logout, this.MessengerClient.User);
            TCPServerClientInteractEventArgs args = new TCPServerClientInteractEventArgs(message, true, true);
            this.Client.Send(args);
        }
        private void buttonPost_Click(object sender, EventArgs e)
        {
            if (this.textBoxNewMessage.Text != String.Empty)
            {
                Message message = new Message(MessageTypes.post, this.MessengerClient.User, this.textBoxNewMessage.Text);
                TCPServerClientInteractEventArgs args = new TCPServerClientInteractEventArgs(message, true, true);
                this.Client.Send(args);
                this.Invoke(new Action(() => this.textBoxNewMessage.Text = String.Empty));
            }
            else
                MessageBox.Show("Text is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ButtonsPattern(bool initialize, bool connect, bool disconnect, bool login, bool logout, bool post, bool clear)
        {
            this.buttonInitialize.Enabled = initialize;
            this.buttonConnect.Enabled = connect;
            this.buttonDisconnect.Enabled = disconnect;
            this.buttonLogin.Enabled = login;
            this.buttonLogout.Enabled = logout;
            this.buttonPost.Enabled = post;
            this.buttonClear.Enabled = clear;
        }
        private void TCPClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(this.Client != null && this.Client.BaseSocket != null)
                this.Client.Dispose();
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.textBoxNewMessage.Text = String.Empty;
        }
    }
}
