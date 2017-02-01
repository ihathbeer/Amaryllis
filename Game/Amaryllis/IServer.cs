namespace Amaryllis {
    using Sockets = System.Net.Sockets;
    class IServer : INetwork {
        private readonly Sockets.Socket Socket;
        public readonly int ServerID, PortNumber = 3131;
        private const int MaximumClients = 450, MaximumConnectionsQueue = 2, MinimumPacketLength = 0;
        /* AO Delegated Objects */
        public event HandleDisconnection FireObject;
        public delegate void HandleIO(StateObject Sender);
        public event HandleIO EnableIOCommunicationTunneling;
        public delegate void HandleDisconnection(StateObject Sender);
        /* Game specific addons */
        private readonly System.Timers.Timer StatisticsUpdateTimer;
        private IPacket.RawPacket RawPacket = new IPacket.RawPacket();
        private IPacket.XMLPacket XMLPacketHandler = new IPacket.XMLPacket();
        public IPacket.GameZone.Manager GameManager = new IPacket.GameZone.Manager();
        public System.Collections.Generic.List<string> OpenIgloos = new System.Collections.Generic.List<string> { };
        public System.Collections.Generic.List<StateObject> Clients = new System.Collections.Generic.List<StateObject>();
        public System.Collections.Generic.Dictionary<string, string> Administrators = new System.Collections.Generic.Dictionary<string, string> { };

        public IServer(int ID, string IPAddressString, int Port, string MySQLHostAddress, int MySQLHostPort, string[] MySQLAuthenticationDetails) {
            ServerID = ID;
            PortNumber = Port;
            SetupManager();
            Socket = new Sockets.Socket(Sockets.AddressFamily.InterNetwork, Sockets.SocketType.Stream, Sockets.ProtocolType.Tcp);
            Socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(IPAddressString), PortNumber));
            Socket.Listen(MaximumConnectionsQueue);
            Socket.SetSocketOption(Sockets.SocketOptionLevel.Socket, Sockets.SocketOptionName.KeepAlive, true);
            EnableIOCommunicationTunneling += new HandleIO(OnValidPacket);
            FireObject += new HandleDisconnection(OnDisconnection);
            StatisticsUpdateTimer = new System.Timers.Timer(1000 * 240) { AutoReset = true };
            StatisticsUpdateTimer.Elapsed += UpdateStatistics;
            if (Database.Database.Init(MySQLHostAddress, MySQLHostPort, MySQLAuthenticationDetails)) Utils.Log.Info("MySQL", "A connection to the database has been made");
            Utils.Log.Debug(ServerID.ToString(), "Amaryllis is running");
        }

        public void Run() {
            StatisticsUpdateTimer.Start();
            AcceptLoop(null);
        }

        void AcceptLoop(Sockets.SocketAsyncEventArgs EventStatement) {
            try {
                if (EventStatement == null) {
                    EventStatement = new Sockets.SocketAsyncEventArgs();
                    EventStatement.Completed += AcceptEvent_Completed;
                }
                else EventStatement.AcceptSocket = null;
                bool EventTrigger = Socket.AcceptAsync(EventStatement); ;
                if (!EventTrigger) AcceptClientObject(EventStatement);
            }
            catch (System.Exception) { }
        }

        private void AcceptClientObject(Sockets.SocketAsyncEventArgs EventStatement) {
            try {
                EventStatement.AcceptSocket.SetSocketOption(Sockets.SocketOptionLevel.Socket, Sockets.SocketOptionName.KeepAlive, true);
                StateObject ClientObject = new StateObject(EventStatement.AcceptSocket, this);
                ClientObject.ArgumentStack = new Sockets.SocketAsyncEventArgs();
                ClientObject.ArgumentStack.Completed += IOCompleted;
                ClientObject.ArgumentStack.SetBuffer(ClientObject.DataReceived, 0, ClientObject.DataReceived.Length);
                ClientObject.ArgumentStack.AcceptSocket = EventStatement.AcceptSocket;
                ClientObject.ArgumentStack.SocketFlags = Sockets.SocketFlags.None;
                ClientObject.ArgumentStack.UserToken = ClientObject;
                lock (Clients) {
                    Clients.Add(ClientObject);
                }
                bool EventTrigger = ClientObject.Sock.ReceiveAsync(ClientObject.ArgumentStack);
                AcceptLoop(EventStatement);
                if (!EventTrigger) ReadClientInput(ClientObject);
            }
            catch (System.Exception) { }
        }

        private void AcceptEvent_Completed(object Sender, Sockets.SocketAsyncEventArgs EventStatement) {
            AcceptClientObject(EventStatement);
        }

        private void IOCompleted(object Sender, Sockets.SocketAsyncEventArgs EventStatement) {
            switch (EventStatement.LastOperation) {
                case Sockets.SocketAsyncOperation.Receive:
                    StateObject ClientSource = EventStatement.UserToken as StateObject;
                    ReadClientInput(ClientSource);
                    break;
                default: break;
            }
        }

        private void ReadClientInput(StateObject ClientObject) {
            try {
                if (ClientObject.ArgumentStack.SocketError != Sockets.SocketError.Success || ClientObject.ArgumentStack.BytesTransferred == 0 || ClientObject.ArgumentStack.Buffer == null || ClientObject.ArgumentStack.Buffer.Length == 0) {
                    FireObject(ClientObject);
                    return;
                }
                var BytesList = new System.Collections.Generic.List<byte>();
                ClientObject.AmountOfDataReceived = ClientObject.ArgumentStack.BytesTransferred;
                for (var i = 0; i < ClientObject.ArgumentStack.BytesTransferred; i++) BytesList.Add(ClientObject.ArgumentStack.Buffer[i]);
                var Bytes = BytesList.ToArray();
                ClientObject.DataReceived = Bytes;
                EnableIOCommunicationTunneling(ClientObject);
                EndClientInput(ClientObject);
            }

            catch (System.Exception) { }
        }

        void EndClientInput(StateObject ClientObject) {
            try {
                ClientObject.ArgumentStack.SocketFlags = Sockets.SocketFlags.None;
                for (int i = 0; i < ClientObject.ArgumentStack.Buffer.Length; i++) ClientObject.ArgumentStack.Buffer[i] = 0;
                ClientObject.ArgumentStack.SetBuffer(0, ClientObject.ArgumentStack.Buffer.Length);
                bool EventTrigger = ClientObject.ArgumentStack.AcceptSocket.ReceiveAsync(ClientObject.ArgumentStack);
                if (!EventTrigger) ReadClientInput(ClientObject);
            }

            catch (System.Exception) {
                FireObject(ClientObject);
            }
        }
        /* Sockets end here */
        public override void OnValidPacket(StateObject Sender) {
            string StringReceived = System.Text.ASCIIEncoding.ASCII.GetString(Sender.DataReceived, 0, Sender.AmountOfDataReceived);
            if (StringReceived.Contains("<")) XMLPacketHandler.HandlePacket(Sender, System.Text.ASCIIEncoding.ASCII.GetString(Sender.DataReceived, 0, Sender.AmountOfDataReceived));
            else if (StringReceived.Contains("%")) RawPacket.HandlePacket(Sender, System.Text.ASCIIEncoding.ASCII.GetString(Sender.DataReceived, 0, Sender.AmountOfDataReceived));

        }

        public override void OnDisconnection(StateObject Sender) {
            try {
                Sender.ConnectionFired = true;
                lock (Clients) {
                    Clients.Remove(Sender);
                }
                Sender.Sock.Close();
                Sender.SendRoomXt(new string[]
                {
                    "rp", "-1", Sender.ID, "0"
                });
                GameManager.RemoveFromGames(Sender);
                if (OpenIgloos.Contains(string.Join("|", new string[] { Sender.ID, Sender.Name }))) OpenIgloos.Remove(string.Join("|", new string[] { Sender.ID, Sender.Name }));
                if (Program.Debug) Utils.Log.Info("OnDisconnection", " Client was disconnected");
            }

            catch (System.Exception Exception) {
                if (Program.Debug) Utils.Log.Error("OnDisconnection", Exception.Message);
            }
        }

        private void UpdateStatistics(object Sender, System.EventArgs Arguments) {
            UpdateServerPopulation();
            if (Program.Debug) Utils.Log.Info("UpdateStatistics", "The population of the server has been updated");
        }

        private void SetupManager() {
            Administrators.Add("1", "******");
            Administrators.Add("2", "*********");
            Administrators.Add("1195351", "****");
            Administrators.Add("1739857", "******");
            GameManager.SledRace = new IPacket.GameZone.SledRace();
        }

        private void UpdateServerPopulation() {
            Database.Database.UpdateObjectProperty("Population", Clients.Count.ToString(), ServerID.ToString(), true, "population");
        }
    }
}
