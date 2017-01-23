using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TCPServerClientForm
{
    public enum MessageTypes { login, login_success, post, post_success, logout, logout_success }
    public class Message
    {
        public MessageTypes MessageType { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public Message(MessageTypes messageType, User User, string text = "")
        {
            this.MessageType = messageType;
            this.Text = text;
            this.User = User;
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
                default:
                    break;
            }
        }
    }
}
