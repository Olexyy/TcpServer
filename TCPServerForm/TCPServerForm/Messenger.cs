using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.Mail;
using System.Net;

namespace TCPServer
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
            this.BinaryData = (binaryData == null) ? new byte[1] : binaryData;
        }
        public Message() { }
    }
    public class User : IEquatable<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public User(int id, string name, string group)
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
    public class UserDatabase
    {
        public List<User> Users { get; set; }
        public UserDatabase()
        {
            this.Users = new List<User>();
            this.Users.Add(new User(1, "Ivy", "Group1"));
            this.Users.Add(new User(2, "Max", "Group1"));
            this.Users.Add(new User(3, "Ann", "Group2"));
        }
    }
    public class Messenger
    {
        private object locker = new object();
        private UserDatabase DB { get; set; }
        private Dictionary<TCPServerClientInfo, User> LoggedUsers { get; set; }
        public void MessageHandler(object o, TCPServerMessageEventArgs args)
        {
            TCPServer server = o as TCPServer;
            switch (args.MessageType)
            {
                case TCPServerMessages.ClientFail:
                    if (this.LoggedUsers.ContainsKey(args.ClientInfo))
                    {
                        User user = this.LoggedUsers[args.ClientInfo];
                        this.LoggedUsers.Remove(args.ClientInfo);
                        this.PushMessageToGroup(MessageTypes.group, user, server, this.CountGroup(user.Group));
                    }    
                    break;
                default:
                    break;
            }
        }
        public Messenger()
        {
            this.DB = new UserDatabase();
            this.LoggedUsers = new Dictionary<TCPServerClientInfo, User>();
            this.locker = new object();
        }
        public void InteractHandler(object o, TCPServerInteractEventArgs args)
        {
            Message message = args.Object as Message;
            User DbUser = this.Authenticate(message.User);
            TCPServer server = o as TCPServer;
            switch (message.MessageType)
            {
                case MessageTypes.login:
                    if (DbUser != null)
                    {
                        this.Login(args.ClientInfo, DbUser);
                        this.PushMessageToGroup(MessageTypes.group, DbUser, server, this.CountGroup(DbUser.Group));
                        args.Object = new Message(MessageTypes.login_success, DbUser);
                        args.KeepAlive = true;
                        args.CallBack = true;
                    }        
                    else
                        args.KeepAlive = false;
                    break;
                case MessageTypes.logout:
                    if (DbUser != null && this.LoggedIn(DbUser))
                    {
                        this.Logout(args.ClientInfo);
                        this.PushMessageToGroup(MessageTypes.group, DbUser, server, this.CountGroup(DbUser.Group));
                        args.Object = new Message(MessageTypes.logout_success, DbUser);
                        args.KeepAlive = true;
                        args.CallBack = true;
                    }
                    else
                        args.KeepAlive = false;
                    break;
                case MessageTypes.post:
                    if (DbUser != null && this.LoggedIn(DbUser))
                    {
                        this.PushMessageToGroup(MessageTypes.post, DbUser, server, message.Text);
                        args.Object = new Message(MessageTypes.post_success, DbUser);
                        args.KeepAlive = true;
                        args.CallBack = true;
                    }
                    else
                        args.KeepAlive = false;
                    break;
                case MessageTypes.cast:
                    if (DbUser != null && this.LoggedIn(DbUser))
                    {
                        this.PushDataToGroup(MessageTypes.cast, DbUser, server, message.BinaryData);
                        args.Object = new Message(MessageTypes.cast_success, DbUser);
                        args.KeepAlive = true;
                        args.CallBack = true;
                    }
                    else
                        args.KeepAlive = false;
                    break;
                case MessageTypes.cast_end:
                    if (DbUser != null && this.LoggedIn(DbUser))
                    {
                        this.PushMessageToGroup(MessageTypes.cast_end, DbUser, server);
                        args.Object = new Message(MessageTypes.cast_end_success, DbUser);
                        args.KeepAlive = true;
                        args.CallBack = true;
                    }
                    else
                        args.KeepAlive = false;
                    break;
                case MessageTypes.mail:
                    if (DbUser != null && this.LoggedIn(DbUser))
                    {
                        this.Mail();
                        args.Object = new Message(MessageTypes.mail_success, DbUser);
                        args.KeepAlive = true;
                        args.CallBack = true;
                    }
                    else
                        args.KeepAlive = false;
                    break;
                default:
                    args.KeepAlive = false;
                    break;
            }
        }
        private User Authenticate(User user)
        {
            foreach (User DbUser in this.DB.Users)
                if (DbUser.Name == user.Name)
                    return DbUser;
            return null;
        }
        private bool LoggedIn(User user)
        {
            lock (this.locker)
            {
                foreach (User loggedUser in this.LoggedUsers.Values)
                    if (loggedUser.Name == user.Name)
                        return true;
            }
            return false;
        }
        private User Login(TCPServerClientInfo clientInfo, User user)
        {

            lock (this.locker)
            {
                this.LoggedUsers.Add(clientInfo, user);
            }
            return user;
        }
        private void Logout(TCPServerClientInfo clientInfo)
        {
            lock (this.locker)
            {
                this.LoggedUsers.Remove(clientInfo);
            }
        }
        private void PushMessageToGroup(MessageTypes type, User user, TCPServer server, string message = "")
        {
            lock (this.locker)
            {
                this.LoggedUsers.Where(i => i.Value.Group == user.Group).ToList().ForEach(i => this.PushMessageToUser(type, i.Key, user, server, message));
            }
        }
        private void PushDataToGroup(MessageTypes type, User user, TCPServer server, byte[] binaryData = null)
        {
            lock (this.locker)
            {
                this.LoggedUsers.Where(i => i.Value.Group == user.Group).ToList().ForEach(i => this.PushDataToUser(type, i.Key, user, server, binaryData));
            }
        }
        private void PushDataToUser(MessageTypes type, TCPServerClientInfo clientInfo, User user, TCPServer server, byte[] binaryData = null)
        {
            Message msg = new Message(type, user, null, binaryData);
            TCPServerInteractEventArgs args = new TCPServerInteractEventArgs(msg, clientInfo, false, false);
            server.Send(args);
        }
        private void PushMessageToUser(MessageTypes type, TCPServerClientInfo clientInfo, User user, TCPServer server, string message = "" )
        {
            Message msg = new Message(type, user, message);
            TCPServerInteractEventArgs args = new TCPServerInteractEventArgs(msg, clientInfo, false, false);
            server.Send(args);
        }
        private string CountGroup(string group)
        {
            return this.LoggedUsers.Where(i => i.Value.Group == group).Count().ToString();
        }
        private void Mail()
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("testtcpserver@gmail.com", "zaq1ZAQ!");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new MailMessage(
                "testtcpserver@gmail.com",  // From field
                "testtcpserver@gmail.com",  // Recipient field
                "Screen cast",              // Subject of the email message
                "Screen cast init."         // Email message body
                );
                client.Send(message);
            }
        }
    }
}
