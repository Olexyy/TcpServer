using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.IO;

namespace TCPServerClient
{
    public partial class TCPClientForm : Form
    {
        private TCPServerClient Client { get; set; }
        private MessengerClient MessengerClient { get; set; }
        private DesktopForm desktopForm;
        public TCPClientForm()
        {
            this.InitializeComponent();
            this.MessengerClient = new MessengerClient();
            this.desktopForm = new DesktopForm();
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (this.ValidateInput())
            {
                int port = Convert.ToInt32(this.textBoxPort.Text);
                int buffer = (this.textBoxBufferSize.Text == "Default (65535)") ? 65535 : Convert.ToInt32(this.textBoxBufferSize.Text);
                string ipAddress = (this.textBoxIpAddress.Text == "Enter IP Address ...") ? "127.0.0.1" : this.textBoxIpAddress.Text;
                TCPServerClientSettings clientSettings = new TCPServerClientSettings(new TCPServerClientInteractHandler[] { this.InteractHandler, this.MessengerClient.InteractHandler }, new TCPServerClientMessageHandler[] { this.MessageHandler }, typeof(Message));
                if (this.Client != null && this.Client.BaseSocket != null)
                    this.Client.Dispose();
                this.Client = new TCPServerClient(clientSettings);
                this.Client.Initialize(port, ipAddress, buffer);
                this.ButtonsPattern(false, true, false, false, false, false, false);
                this.Client.Connect();
            }
            else
                this.Message.Text = "Input error";
        }
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            this.Client.Disonnect();
            // TODO: This should be handled by async message handler of server
            while (this.Client.Connected);
            this.ButtonsPattern(false, true, false, false, false, false, false);
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
            if (this.textBoxBufferSize.Text != "Default (65535)")
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
                        this.ButtonsPattern(true, false, false, false, true, true, true);
                    }));
                    break;
                case MessageTypes.logout_success:
                    this.Invoke(new Action(() => {
                        this.Message.Text = "[Messenger]: Logged out...";
                        this.ButtonsPattern(false, false, true, true, false, false, false);
                        this.textBoxFriendsOnline.Text = "0";
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
                    }));
                    break;
                case MessageTypes.group:
                    this.Invoke(new Action(() => {
                        this.Message.Text = "[Messenger]: Changes in group...";
                        this.textBoxFriendsOnline.Text = message.Text;
                    }));
                    break;
                case MessageTypes.cast_success:
                    this.Invoke(new Action(() => {
                        this.buttonShowDesktop.Text = "Stop streaming";
                    }));
                    break;
                case MessageTypes.cast_end_success:
                    this.Invoke(new Action(() => {
                        this.buttonShowDesktop.Text = "Show screen";
                    }));         
                    break;
                case MessageTypes.cast:
                    Image image = Image.FromStream(new MemoryStream(message.BinaryData));
                    this.Invoke(new Action(() => {
                        if (!this.desktopForm.Visible)
                            this.desktopForm.Show();
                        if (message.User.Id != this.MessengerClient.User.Id)
                            this.buttonShowDesktop.Enabled = false;
                        this.desktopForm.SetScreen(image);
                    }));
                    break;
                case MessageTypes.cast_end:
                    this.Invoke(new Action(() => {
                        if (this.desktopForm.Visible)
                            this.desktopForm.Hide();
                        if (!this.buttonShowDesktop.Enabled)
                            this.buttonShowDesktop.Enabled = true;
                    }));
                    break;
                case MessageTypes.mail_success:
                    this.Invoke(new Action(() => {
                        this.Message.Text = "[Messenger]: Mail sent...";
                    }));
                    break;
                default:
                    break;
            }
        }
        private void MessageHandler(object o, TCPServerClientMessageEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() => this.Message.Text = args.Message));
                switch (args.MessageType)
                {
                    case TCPServerClientMessages.Connected:
                        this.Invoke(new Action(() => this.ButtonsPattern(false, false, true, true, false, false, false)));
                        break;
                    case TCPServerClientMessages.Disconnected:
                        this.Invoke(new Action(() =>
                        {
                            this.ButtonsPattern(false, true, false, false, false, false, false);
                            this.textBoxFriendsOnline.Text = "0";
                        }));
                        if (this.Client != null && this.Client.BaseSocket != null)
                            this.Client.Dispose();
                        break;
                    default: break;
                }
            }catch(Exception ex) { }
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
        private void ButtonsPattern(bool showDesktop, bool connect, bool disconnect, bool login, bool logout, bool post, bool clear)
        {
            this.buttonShowDesktop.Enabled = showDesktop;
            this.buttonConnect.Enabled = connect;
            this.buttonDisconnect.Enabled = disconnect;
            this.buttonLogin.Enabled = login;
            this.buttonLogout.Enabled = logout;
            this.buttonPost.Enabled = post;
            this.buttonClear.Enabled = clear;
        }
        private void TCPClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.MessengerClient.IsStreamer)
                this.buttonShowDesktop_Click(null, null);
            if (this.Client != null && this.Client.BaseSocket != null)
                this.Client.Dispose();
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.textBoxNewMessage.Text = String.Empty;
        }
        private void buttonShowDesktop_Click(object sender, EventArgs e)
        {
            if(!this.MessengerClient.IsStreamer)
            {
                new Thread(this.ScreenSender).Start();
            }
            else
            {
                Message message = new Message(MessageTypes.cast_end, this.MessengerClient.User);
                TCPServerClientInteractEventArgs args = new TCPServerClientInteractEventArgs(message, true, true);
                this.Client.Send(args);
            }
        }
        private void ScreenSender()
        {
            Message mes = new Message(MessageTypes.mail, this.MessengerClient.User);
            TCPServerClientInteractEventArgs arg = new TCPServerClientInteractEventArgs(mes, true, true);
            this.Client.Send(arg);
            do
            {
                using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                    {
                        g.CopyFromScreen(0, 0, 0, 0, bmpScreenCapture.Size);
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            Bitmap resized = new Bitmap(bmpScreenCapture, new Size(240, 160));
                            resized.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            byte[] binaryData = memoryStream.ToArray();
                            Message message = new Message(MessageTypes.cast, this.MessengerClient.User, "", binaryData);
                            TCPServerClientInteractEventArgs args = new TCPServerClientInteractEventArgs(message, true, true);
                            this.Client.Send(args);
                        }
                    }
                }
                Thread.Sleep(2000);
            } while (this.Visible && this.MessengerClient.IsStreamer);
        }
    }
}
