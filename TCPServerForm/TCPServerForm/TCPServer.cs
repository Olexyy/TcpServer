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

namespace System.Net.Sockets {
    public enum TCPServerStates { Undefined, Initialized, Started, Stopping, Stopped, }
    public enum TCPServerMessages
    {
        Undefined, Initialized, AlreadyInitialized, AddressFail, EndPointFail, ClientConnected, ClientFail, ParseFail,
        Started, Stopped, StartFail, AcceptFail, NotInitialized, ClientDisconnected, EndAccepting, NoHandler, SocketLimit,
    }
    public class TCPServerMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
        public TCPServerMessages MessageType { get; set; }
        public TCPServerClientInfo ClientInfo { get; set; }
    }
    public class TCPServerInteractEventArgs : EventArgs
    {
        public int BytesCount { get; set; }
        public byte[] Bytes { get; set; }
        public object Object { get; set; }
        public bool KeepAlive { get; set; }
        public bool CallBack { get; set; }
        public TCPServerClientInfo ClientInfo { get; set; }
    }
    public class TCPServerClientInfo : IEquatable<TCPServerClientInfo>
    {
        public Socket Socket { get; set; }
        public string Port;
        public string IpAddress;
        public ulong Id;
        public override int GetHashCode()
        {
            return (int)this.Id;
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as TCPServerClientInfo);
        }
        public bool Equals(TCPServerClientInfo obj)
        {
            return obj != null && this.Id == obj.Id;
        }
    }
    public class TCPServerSettings
    {
        public TCPServerInteractHandler InteractHandler;
        public TCPServerMessageHandler MessageHandler;
        public Type Type;
        public TCPServerSettings(TCPServerInteractHandler interactHandler, TCPServerMessageHandler messageHandler = null, Type type = null)
        {
            this.InteractHandler = interactHandler;
            this.MessageHandler = messageHandler;
            this.Type = type;
        }
    }
    public delegate void TCPServerMessageHandler(object sender, TCPServerMessageEventArgs args);
    public delegate void TCPServerInteractHandler(object sender, TCPServerInteractEventArgs args);
    class TCPServer : IDisposable {
        public event TCPServerInteractHandler ServerInteractEvent;
        public event TCPServerMessageHandler ServerMessageEvent;
        public Dictionary<int, TCPServerClientInfo> Sockets { get; private set; }
        public IPEndPoint EndPoint { get; private set; }
        public int BufferSize { get; private set; }
        public bool Initialized { get; private set; }
        public bool Started { get; private set; }
        public bool Stopping { get; private set; }
        public uint SocketLimit { get; private set; }
        public Type DataType { get; set; }
        public List<string> MessageLog { get; private set; }
        private Socket BaseSocket { get; set; }
        private UInt64 Counter { get; set; }
        public TCPServer(TCPServerSettings serverSettings) {
            this.Counter = 0;
            this.Sockets = new Dictionary<int, TCPServerClientInfo>();
            this.MessageLog = new List<string>();
            if (serverSettings.Type != null)
                this.DataType = serverSettings.Type;
            if (serverSettings.InteractHandler != null)
                this.ServerInteractEvent += serverSettings.InteractHandler;
            if (serverSettings.MessageHandler != null)
                this.ServerMessageEvent += serverSettings.MessageHandler;
        }
        public TCPServer(TCPServerInteractHandler interactHandler, TCPServerMessageHandler messageHandler = null, Type type = null) {
            this.Counter = 0;
            this.Sockets = new Dictionary<int, TCPServerClientInfo>();
            this.MessageLog = new List<string>();
            if (type != null)
                this.DataType = type;
            if (interactHandler != null)
                this.ServerInteractEvent += interactHandler;
            if (messageHandler != null)
                this.ServerMessageEvent += messageHandler;
        }
        public void Initialize(int port = 0, string ipAddress = "0.0.0.0", int bufferSize = 1024, uint socketLimit = 0) {
            Random rand = new Random();
            if (port == 0) 
                port = rand.Next(48654, 49150); // free ports form *wikipedia*
            this.BufferSize = bufferSize;
            this.SocketLimit = socketLimit;
            this.SetEndpoint(ipAddress, port);
        }
        private void SetEndpoint(string ipAddress, int port) {
            IPAddress address = IPAddress.None;
            if (IPAddress.TryParse(ipAddress, out address)) {
                try {
                    this.EndPoint = new IPEndPoint(address, port);
                    if (this.Initialized || this.Stopping || this.BaseSocket != null) {
                        this.WatchDog(TCPServerMessages.AlreadyInitialized);
                        return;
                    }
                    this.BaseSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                    this.BaseSocket.Bind(this.EndPoint);
                    this.Initialized = true;
                    this.WatchDog(TCPServerMessages.Initialized);
                }
                catch (Exception ex) {
                    this.WatchDog(TCPServerMessages.EndPointFail);
                    this.Initialized = false;
                }
            }
            else {
                this.WatchDog(TCPServerMessages.AddressFail);
            }
        }
        public void Start() {
            if (this.ServerInteractEvent == null) {
                this.WatchDog(TCPServerMessages.NoHandler);
                return;
            }
            if (this.Initialized) {
                try {
                    this.BaseSocket.Listen(10);
                    if (this.ServerInteractEvent == null)
                        throw new Exception();
                    this.AddSocket(this.BaseSocket);
                    this.Started = true;
                    this.WatchDog(TCPServerMessages.Started);
                    this.NewThread();
                }
                catch (Exception ex) {
                    this.WatchDog(TCPServerMessages.StartFail);
                } 
            }
            else {
                this.WatchDog(TCPServerMessages.NotInitialized);
            }
        }
        private void NewThread() {
            if (this.Stopping)
                return;
            if (this.Sockets.Count > 0 && (uint)this.Sockets.Count - 1 >= this.SocketLimit && this.SocketLimit != 0) {
                this.WatchDog(TCPServerMessages.SocketLimit);
                return;
            }
            try {
                IAsyncResult result = this.BaseSocket.BeginAccept(this.Connect, this.BaseSocket);
            }
            catch (Exception ex) {
                this.WatchDog(TCPServerMessages.AcceptFail);
            }
        }
        private void Connect(IAsyncResult state) {
            Socket baseSocket = state.AsyncState as Socket;
            Socket client = null;
            try {
                client = baseSocket.EndAccept(state);
                TCPServerClientInfo clientInfo = this.AddSocket(client);
                this.NewThread();
                this.Interact(clientInfo);
                client.Shutdown(SocketShutdown.Both); // todo think of throwing;
                this.RemoveSocket();
            }
            catch (ObjectDisposedException ex) {
                this.WatchDog(TCPServerMessages.EndAccepting);
                this.CheckIfStopped();
            }
            catch (Exception ex) {
                this.WatchDog(TCPServerMessages.AcceptFail);
            }
            finally {
                if (client != null)
                    client.Close();
            }
        }
        private void Interact(TCPServerClientInfo clientInfo) {
            Socket client = clientInfo.Socket;
            bool keepAlive;
            try {
                do {
                    byte[] bytes = new byte[this.BufferSize];
                    int bytesCount = client.Receive(bytes);
                    TCPServerInteractEventArgs args = new TCPServerInteractEventArgs();
                    args.Bytes = bytes;
                    args.BytesCount = bytesCount;
                    args.ClientInfo = clientInfo;
                    if (this.DataType != null) {
                        MethodInfo method = typeof(TCPServer).GetMethod("GetObject", BindingFlags.Instance | BindingFlags.NonPublic);
                        try {
                            method.MakeGenericMethod(this.DataType).Invoke(this, new object[] { args });
                        }
                        catch (Exception ex) {
                            this.WatchDog(TCPServerMessages.ParseFail, this.Sockets[Thread.CurrentThread.ManagedThreadId]);
                        }
                    }
                    if (this.ServerInteractEvent != null)
                        this.ServerInteractEvent(this, args);
                    if (args.CallBack) {
                        if (this.DataType != null)
                            this.SetObject(args);
                        bytesCount = client.Send(args.Bytes);
                    }
                    keepAlive = args.KeepAlive;
                } while (keepAlive); // we may add here stopping condition;
            }
            catch (Exception ex) {
                this.WatchDog(TCPServerMessages.ClientFail, this.Sockets[Thread.CurrentThread.ManagedThreadId]);
            }
        }
        private void GetObject<T>(TCPServerInteractEventArgs args) {
            string data = Encoding.UTF8.GetString(args.Bytes, 0, args.BytesCount);
            T dataObject = new JavaScriptSerializer().Deserialize<T>(data);
            args.Object = dataObject;
        }
        private void SetObject(TCPServerInteractEventArgs args) {
            string serialized = new JavaScriptSerializer().Serialize(args.Object); 
            args.Bytes = Encoding.UTF8.GetBytes(serialized);
        }
        public void Stop() {
            this.RemoveSocket(true);
            this.BaseSocket.Close();
            this.BaseSocket = null;
            this.Stopping = true;
        }
        private TCPServerClientInfo AddSocket(Socket socket) {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            TCPServerClientInfo info = new TCPServerClientInfo();
            info.Socket = socket;
            info.Id = this.Counter;
            this.Counter++;
            if (!socket.Connected) {
                info.Port = this.EndPoint.Port.ToString();
                info.IpAddress = this.EndPoint.Address.ToString();
            }
            else {
                string endPoint = socket.RemoteEndPoint.ToString();
                string expressionIpAddress = @"(\d)+\.(\d)+\.(\d)+\.(\d)+";
                string expressionPort = @"(\d)+$";
                Regex regex = new Regex(expressionIpAddress);
                info.IpAddress = regex.Match(endPoint).Value;
                regex = new Regex(expressionPort);
                info.Port = regex.Match(endPoint).Value;
                this.WatchDog(TCPServerMessages.ClientConnected, info);
            }
            this.Sockets.Add(threadId, info);
            return info;
        }
        private void RemoveSocket(bool basicSocket = false) {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            if(!basicSocket)
                this.WatchDog(TCPServerMessages.ClientDisconnected, this.Sockets[threadId]);
            this.Sockets.Remove(threadId);
            if (!this.Stopping && (this.SocketLimit > 0 && (uint)this.Sockets.Count - 1 < this.SocketLimit && this.SocketLimit != 0))
                this.NewThread();
            this.CheckIfStopped();
        }
        private void CheckIfStopped() {
            if (this.Sockets.Count == 0 && this.Stopping) {
                this.Stopping = false;
                this.Started = false;
                this.Initialized = false;
                this.WatchDog(TCPServerMessages.Stopped);
            }
        }
        private void WatchDog(TCPServerMessages state, TCPServerClientInfo client = null) {
            if (state == TCPServerMessages.Undefined)
                return;
            string message = String.Empty;
            if (state == TCPServerMessages.Initialized)
                message = "Initialized";
            else if (state == TCPServerMessages.Started)
                message = "Server started at " + this.EndPoint.Address.ToString() + " : " + this.EndPoint.Port.ToString();
            else if (state == TCPServerMessages.Stopped)
                message = "Stopped";
            else if (state == TCPServerMessages.AddressFail)
                message = "Ip address fail";
            else if (state == TCPServerMessages.EndPointFail)
                message = "Endpoint fail";
            else if (state == TCPServerMessages.StartFail)
                message = "Start fail";
            else if (state == TCPServerMessages.NotInitialized)
                message = "Not initialized";
            else if (state == TCPServerMessages.AcceptFail)
                message = "Accept fail";
            else if (state == TCPServerMessages.ClientConnected)
                message = "Client " + client.Id.ToString() + " connected";
            else if (state == TCPServerMessages.ClientDisconnected)
                message = "Client " + client.Id.ToString() + " disconnected";
            else if (state == TCPServerMessages.ClientFail)
                message = "Client " + client.Id.ToString() + " fail";
            else if (state == TCPServerMessages.AlreadyInitialized)
                message = "Server already initialized";
            else if (state == TCPServerMessages.EndAccepting)
                message = "End accepting sockets";
            else if (state == TCPServerMessages.NoHandler)
                message = "No server handler assigned";
            else if (state == TCPServerMessages.ParseFail)
                message = "Data parsing fail, client " + client.Id.ToString();
            else if (state == TCPServerMessages.SocketLimit)
                message = "Socket limit reached";
            string fullMessage = DateTime.Now.ToUniversalTime() + " : " + message;
            this.MessageLog.Add(fullMessage);
            TCPServerMessageEventArgs argument = new TCPServerMessageEventArgs();
            argument.Message = fullMessage;
            argument.MessageType = state;
            argument.ClientInfo = client;
            if (this.ServerMessageEvent != null)
                this.ServerMessageEvent(this, argument);
        }
        public string ServerStatus { get {
                if (this.Initialized && !this.Started)
                    return TCPServerStates.Initialized.ToString();
                else if (this.Started && !this.Stopping)
                    return TCPServerStates.Started.ToString();
                else if (this.Started && this.Stopping)
                    return TCPServerStates.Stopping.ToString();
                else
                    return TCPServerStates.Stopped.ToString();
            }}
        public void Dispose() {
            if (this.Sockets.Count > 0)
                foreach (var clientInfo in this.Sockets)
                    clientInfo.Value.Socket.Close();
            if (this.BaseSocket != null)
                this.BaseSocket.Close();
        }
    }
}