using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TCPServerClientForm
{
    public enum MessageTypes { login, login_success, post, logout }
    public class Message
    {
        public MessageTypes MessageType { get; private set; }
        public User User { get; private set; }
        public string Text { get; private set; }
        public Message(MessageTypes messageType, User User, string text = "")
        {
            this.MessageType = messageType;
            this.Text = text;
            this.User = User;
        }
    }
    public class User : IEquatable<User>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Group { get; private set; }
        public User(string name, int id = 0 , string group = "")
        {
            this.Id = id;
            this.Name = name;
            this.Group = group;
        }
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
        public bool LoggedIn { get; private set; }
        private User User { get; set; }
        public  void InteractHandler(object o, TCPServerClientInteractEventArgs args)
        {
            Message message = args.Object as Message;
            switch (message.MessageType)
            {
                case MessageTypes.login_success:
                    this.LoggedIn = true;
                    this.User = message.User;
                    break;
                default:
                    break;
            }
        }
    }
}
