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
                string ipAddress = this.textBoxIpAddress.Text;
                TCPServerClientSettings clientSettings = new TCPServerClientSettings(this.InteractHandler, this.MessageHandler, typeof(Message));
                this.Client = new TCPServerClient(clientSettings);
                this.Client.ClientInteractEvent += this.MessengerClient.InteractHandler;
                this.Client.Initialize(port, ipAddress, buffer);
                this.buttonConnect.Enabled = true;
                this.buttonInitialize.Enabled = false;
                this.buttonDisconnect.Enabled = false;
            }
            else
                this.Message.Text = "Input error";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (this.Client == null)
                this.buttonInitialize_Click(null, null);
            this.Client.Connect();
            while (!this.Client.Connected);
            this.buttonLogin.Enabled = true;
            this.buttonConnect.Enabled = false;
            this.buttonInitialize.Enabled = false;
            this.buttonDisconnect.Enabled = true;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            this.Client.Disonnect();
            while (this.Client.Connected);
            this.buttonConnect.Enabled = true;
            this.buttonInitialize.Enabled = true;
            this.buttonDisconnect.Enabled = false;
            this.Client = null;
        }

        private bool ValidateInput()
        {
            string expressionIpAddress = @"^(\d)+\.(\d)+\.(\d)+\.(\d)+$";
            string expressionPort = @"^(\d)+$";
            Regex regex = new Regex(expressionIpAddress);
            if (this.textBoxIpAddress.Text == String.Empty || !regex.Match(this.textBoxIpAddress.Text).Success)
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
                    this.Invoke(new Action(()=>this.Message.Text = "[Messenger]: LOGGED IN..."));
                    break;
                case MessageTypes.post:
                    this.Invoke(new Action(() => { this.MessageList.Items.Add(message.User.Name);
                        this.MessageList.Items[this.MessageList.Items.Count - 1].SubItems.Add(DateTime.Now.ToShortDateString());
                        this.MessageList.Items[this.MessageList.Items.Count - 1].SubItems.Add(message.Text);
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
                    //TODO : marshl to thread!!!
                    this.buttonLogin.Enabled = false;
                    this.buttonLogout.Enabled = false;
                    this.buttonDisconnect.Enabled = false;
                    this.buttonInitialize.Enabled = true;
                    this.buttonConnect.Enabled = true;
                    break;
                default: break;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            User user = new User(this.textBoxUserName.Text);
            Message message = new Message(MessageTypes.login, user);
            TCPServerClientInteractEventArgs args = new TCPServerClientInteractEventArgs();
            args.Object = message;
            args.KeepAlive = true;
            this.Client.Send(args);
        }
    }
}
