using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TCPServerClient
{
    public enum TCPServerClientMessages
    {
        Undefined, Initialized, AddressFail, Connected, ConnectFail, ServerFail, NoHandler,
        ParseFail, SendFail, ReceiveFail, DisconnectFail, Disconnected, 
    }
    public class TCPServerClientInteractEventArgs : EventArgs
    {
        public int BytesCount { get; set; }
        public byte[] Bytes { get; set; }
        public object Object { get; set; }
        public bool KeepAlive { get; set; }
        public bool CallBack { get; set; }
        public TCPServerClientInteractEventArgs(object message, bool keepAlive = true, bool callback = true)
        {
            this.Object = message;
            this.KeepAlive = keepAlive;
            this.CallBack = callback;
        }
        public TCPServerClientInteractEventArgs() { }
    }
    public class TCPServerClientMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
        public TCPServerClientMessages MessageType { get; set; }
        public TCPServerClientMessageEventArgs(TCPServerClientMessages messageType, string message)
        {
            this.MessageType = messageType;
            this.Message = message;
        }
        public TCPServerClientMessageEventArgs() { }
    }
    public class TCPServerClientSettings
    {
        public TCPServerClientInteractHandler[] InteractHandlers;
        public TCPServerClientMessageHandler[] MessageHandlers;
        public Type Type;
        public TCPServerClientSettings(TCPServerClientInteractHandler[] interactHandlers, TCPServerClientMessageHandler[] messageHandlers = null, Type type = null)
        {
            this.InteractHandlers = interactHandlers;
            this.MessageHandlers = messageHandlers;
            this.Type = type;
        }
    }

    public delegate void TCPServerClientMessageHandler(object sender, TCPServerClientMessageEventArgs args);
    public delegate void TCPServerClientInteractHandler(object sender, TCPServerClientInteractEventArgs args);

    public class TCPServerClient : IDisposable {
        public event TCPServerClientInteractHandler ClientInteractEvent;
        public event TCPServerClientMessageHandler ClientMessageEvent;
        public IPEndPoint EndPoint { get; private set; }
        public int BufferSize { get; private set; }
        public Type DataType { get; set; }
        public List<string> MessageLog { get; private set; }
        public Socket BaseSocket { get; private set; }
        public bool Connected { get; set; }
        public TCPServerClient(TCPServerClientSettings clientSettings) {
            this.MessageLog = new List<string>();
            if (clientSettings.Type != null)
                this.DataType = clientSettings.Type;
            if (clientSettings.InteractHandlers != null)
                clientSettings.InteractHandlers.ToList().ForEach(i => this.ClientInteractEvent += i);
            if (clientSettings.MessageHandlers != null)
                clientSettings.MessageHandlers.ToList().ForEach(i => this.ClientMessageEvent += i);
        }
        public TCPServerClient(TCPServerClientInteractHandler interactHandler, TCPServerClientMessageHandler messageHandler = null, Type type = null) {
            this.MessageLog = new List<string>();
            if (type != null)
                this.DataType = type;
            if (interactHandler != null)
                this.ClientInteractEvent += interactHandler;
            if (messageHandler != null)
                this.ClientMessageEvent += messageHandler;
        }
        public TCPServerClient(TCPServerClientInteractHandler[] interactHandlers, TCPServerClientMessageHandler[] messageHandlers = null, Type type = null)
        {
            this.MessageLog = new List<string>();
            if (type != null)
                this.DataType = type;
            if (interactHandlers != null)
                interactHandlers.ToList().ForEach(i => this.ClientInteractEvent += i);
            if (messageHandlers != null)
                messageHandlers.ToList().ForEach(i => this.ClientMessageEvent += i);
        }
        public void Initialize(int port, string ipAddress, int bufferSize = 1024, bool connect = false) {
            this.BufferSize = bufferSize;
            this.SetEndpoint(ipAddress, port);
            if (connect)
                this.Connect();
        }
        private void SetEndpoint(string ipAddress, int port) {
            IPAddress address = IPAddress.None;
            if (IPAddress.TryParse(ipAddress, out address)) {
                this.EndPoint = new IPEndPoint(address, port);
                this.BaseSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.WatchDog(TCPServerClientMessages.Initialized);
            }
            else {
                this.WatchDog(TCPServerClientMessages.AddressFail);
            }
        }
        public void Connect() {
            if (this.ClientInteractEvent == null) {
                this.WatchDog(TCPServerClientMessages.NoHandler);
                return;
            }
            try {
                IAsyncResult result = this.BaseSocket.BeginConnect(this.EndPoint, this.ProcessConnect, this.BaseSocket);
            }
                catch (Exception ex) {
                    this.WatchDog(TCPServerClientMessages.ConnectFail);
                } 
        }
        private void ProcessConnect(IAsyncResult state) {
            Socket baseSocket = state.AsyncState as Socket;
            try {
                baseSocket.EndConnect(state);
                this.Connected = true;
                this.WatchDog(TCPServerClientMessages.Connected);
                new Thread(this.AsyncInteract).Start(this.BaseSocket);
            }
            catch (Exception ex) {
                this.WatchDog(TCPServerClientMessages.ConnectFail);
            }
        }
        public void Disonnect(bool reuseSocket = false)
        {
            try
            {
                IAsyncResult result = this.BaseSocket.BeginDisconnect(reuseSocket, this.ProcessDisconnect, this.BaseSocket);
            }
            catch (Exception ex)
            {
                this.WatchDog(TCPServerClientMessages.DisconnectFail);
            }
        }
        private void ProcessDisconnect(IAsyncResult state)
        {
            Socket baseSocket = state.AsyncState as Socket;
            try
            {
                baseSocket.EndDisconnect(state);
                this.Connected = false;
                this.WatchDog(TCPServerClientMessages.Disconnected);
            }
            catch (Exception ex)
            {
                this.WatchDog(TCPServerClientMessages.DisconnectFail);
            }
        }
        private void AsyncInteract(object param)
        {
            Socket baseSocket = param as Socket;
            try
            {
                while (true)
                {
                    byte[] bytes = new byte[this.BufferSize];
                    int bytesCount = baseSocket.Receive(bytes);
                    TCPServerClientInteractEventArgs args = new TCPServerClientInteractEventArgs();
                    args.Bytes = bytes;
                    args.BytesCount = bytesCount;
                    if (this.DataType != null)
                    {
                        MethodInfo method = typeof(TCPServerClient).GetMethod("GetObject", BindingFlags.Instance | BindingFlags.NonPublic);
                        try
                        {
                            method.MakeGenericMethod(this.DataType).Invoke(this, new object[] { args });
                        }
                        catch (Exception ex)
                        {
                            this.WatchDog(TCPServerClientMessages.ParseFail);
                        }
                    }
                    if (this.ClientInteractEvent != null)
                        this.ClientInteractEvent(this, args);
                    if (args.CallBack)
                    {
                        if (this.DataType != null)
                            this.SetObject(args);
                        bytesCount = baseSocket.Send(args.Bytes);
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                this.WatchDog(TCPServerClientMessages.Disconnected);
            }
            catch (Exception ex)
            {
                this.WatchDog(TCPServerClientMessages.ReceiveFail);
            }
        }
        public void Send(TCPServerClientInteractEventArgs args) {
            try {
                if (this.DataType != null)
                    this.SetObject(args);
                int bytesCount = this.BaseSocket.Send(args.Bytes);
            }
            catch (Exception ex) {
                this.WatchDog(TCPServerClientMessages.SendFail);
            }
        }
        private void GetObject<T>(TCPServerClientInteractEventArgs args) {
            string data = Encoding.UTF8.GetString(args.Bytes, 0, args.BytesCount);
            T dataObject = new JavaScriptSerializer().Deserialize<T>(data);
            args.Object = dataObject;
        }
        private void SetObject(TCPServerClientInteractEventArgs args) {
            string serialized = new JavaScriptSerializer().Serialize(args.Object); 
            args.Bytes = Encoding.UTF8.GetBytes(serialized);
        }
        private void WatchDog(TCPServerClientMessages state) {
            if (state == TCPServerClientMessages.Undefined)
                return;
            string message = String.Empty;
            if (state == TCPServerClientMessages.Initialized)
                message = "Initialized";
            else if (state == TCPServerClientMessages.Connected)
                message = "Connected to " + this.EndPoint.Address.ToString() + " : " + this.EndPoint.Port.ToString();
            else if (state == TCPServerClientMessages.Disconnected)
                message = "Disconnected";
            else if (state == TCPServerClientMessages.AddressFail)
                message = "Ip address fail";
            else if (state == TCPServerClientMessages.ConnectFail)
                message = "Connect fail";
            else if (state == TCPServerClientMessages.Disconnected)
                message = "Disconnected";
            else if (state == TCPServerClientMessages.DisconnectFail)
                message = "Disconnect fail";
            else if (state == TCPServerClientMessages.ReceiveFail)
                message = "Receive fail";
            else if (state == TCPServerClientMessages.SendFail)
                message = "Send fail";
            else if (state == TCPServerClientMessages.NoHandler)
                message = "No server handler assigned";
            else if (state == TCPServerClientMessages.ParseFail)
                message = "Data parsing fail";
            string fullMessage = DateTime.Now.ToUniversalTime() + " : " + message;
            this.MessageLog.Add(fullMessage);
            TCPServerClientMessageEventArgs argument = new TCPServerClientMessageEventArgs(state, fullMessage);
            if (this.ClientMessageEvent != null)
                this.ClientMessageEvent(this, argument);
        }
        public void Dispose() {
            if (this.BaseSocket != null)
                this.BaseSocket.Close();
        }
    }
}