using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using TCPServerClient;

namespace TCPServerClient
{
    public enum MessageTypes { login, login_success, post, post_success, logout, logout_success, group,
        cast, cast_success, cast_end, cast_end_success, mail, mail_success }
    public class Message
    {
        public MessageTypes MessageType { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public byte[] BinaryData { get; set; }
        public Message(MessageTypes messageType, User User, string text = "", byte[] binaryData = null)
        {
            this.MessageType = messageType;
            this.Text = text;
            this.User = User;
            this.BinaryData =  (binaryData == null)? new byte[1]:binaryData;
        }
        public Message() { }
    }
    public class User : IEquatable<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public User(string name, int id = 0 , string group = "")
        {
            this.Id = id;
            this.Name = name;
            this.Group = group;
        }
        public User() { }
        public override int GetHashCode()
        {
            return (int)this.Id;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as User);
        }
        public bool Equals(User obj)
        {
            return obj != null && this.Id == obj.Id;
        }
    }
    public class MessengerClient
    {
        public bool IsStreamer { get; set; }
        private object locker;
        public bool LoggedIn { get; private set; }
        public User User { get; private set; }
        public MessengerClient()
        {
            this.locker = new object();
        }
        public void InteractHandler(object o, TCPServerClientInteractEventArgs args)
        {
            Message message = args.Object as Message;
            switch (message.MessageType)
            {
                case MessageTypes.login_success:
                    lock (this.locker)
                    {
                        this.LoggedIn = true;
                        this.User = message.User;
                    }
                    break;
                case MessageTypes.logout_success:
                    lock (this.locker)
                    {
                        this.LoggedIn = false;
                        this.User = null;
                    }
                    break;
                case MessageTypes.cast_success:
                    lock (this.locker)
                    {
                        this.IsStreamer = true;
                    }
                    break;
                case MessageTypes.cast_end_success:
                    lock (this.locker)
                    {
                        this.IsStreamer = false;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
