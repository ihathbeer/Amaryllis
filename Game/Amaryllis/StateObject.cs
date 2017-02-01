namespace Amaryllis {
    using Sockets = System.Net.Sockets;
    class StateObject {
        public IServer IServer;
        public volatile bool ConnectionFired = false;
        public Sockets.Socket Sock;
        public byte[] DataReceived = new byte[355];
        public int AmountOfDataReceived, AmountOfDataSent = 0;
        public string Key, ID, Name, Head, Face, Neck, Body, Hands, Feet, Color, Pin, Photo, Mood, Rank, Frame, X, Y, Coins, Inventory, ZoneId, GameZone = "none", ParticipantID = "none", GameZoneID = "none", Igloo, Music, Floor, IglooFurniture, IglooInventory;
        public string[] Buddies;
        public Sockets.SocketAsyncEventArgs ArgumentStack;

        public StateObject(Sockets.Socket Socket, IServer Provider) {
            Sock = Socket;
            IServer = Provider;
            if (Program.Debug) Utils.Log.Trace("Client Object", "Initialization has been made");
        }

        public void Send(string Input) {
            try {
                byte[] Output = System.Text.ASCIIEncoding.ASCII.GetBytes(Input + "\0");
                if (!ConnectionFired && IPacket.SocketUtils.IsConnected(Sock)) Sock.BeginSend(Output, 0, Output.Length, 0, new System.AsyncCallback(SendCallback), Sockets.SocketFlags.None);
            }
            catch (System.Exception Exception) {
                IServer.OnDisconnection(this);
                if (Program.Debug) Utils.Log.Error("Client Socket [Send][" + Input + "] ", Exception.Message);
            }
        }

        private void SendCallback(System.IAsyncResult Result) {
            try {
                AmountOfDataSent = Sock.EndSend(Result);
            }
            catch (System.Exception) { }
        }

        public void SendXt(string[] Input) {
            string Packet = "%xt%";
            for (int i = 0; i < Input.Length; i++) Packet += Input[i] + "%";
            Send(Packet);
        }

        public void SendRoomXt(string[] Input) {
            try {
                System.Collections.Generic.List<StateObject> Clients = null;
                lock (IServer.Clients) {
                    Clients = IServer.Clients.FindAll(o => o.ZoneId == ZoneId);
                }
                foreach (StateObject Client in Clients) Client.SendXt(Input);
            }
            catch (System.Exception) {

            }
        }

        public void Setup() {
            string[] Properties = Database.Database.GetObjectProperties(new string[] { "Head", "Face", "Neck", "Body", "Hands", "Feet", "Pin", "Color", "Photo", "Inventory", "Buddies", "Rank", "Mood", "Coins", "Igloo", "Music", "Floor", "IglooFurniture", "IglooInventory" }, ID, true);
            try {
                Head = Properties[0];
                Face = Properties[1];
                Neck = Properties[2];
                Body = Properties[3];
                Hands = Properties[4];
                Feet = Properties[5];
                Pin = Properties[6];
                Color = Properties[7];
                Photo = Properties[8];
                Inventory = Properties[9];
                Buddies = Properties[10].Split(',');
                Rank = Properties[11];
                Mood = Properties[12];
                Coins = Properties[13];
                Igloo = Properties[14];
                Music = Properties[15];
                Floor = Properties[16];
                IglooFurniture = Properties[17];
                IglooInventory = Properties[18];
            }

            catch { }
        }

        public void JoinZone(string Zone, string DefaultX = "330", string DefaultY = "300") {
            SendRoomXt(new string[] 
            {
                "rp", "-1", ID
            });

            Frame = "0";
            ZoneId = Zone;
            X = DefaultX;
            Y = DefaultY;

            int ZoneInt;
            if (int.TryParse(Zone, out ZoneInt)) {
                if (ZoneInt < 900 || ZoneInt > 1000) {
                    if (CountUsers(Zone) <= 60) {
                        string ZoneClients = "%xt%jr%-1%" + Zone + "%" + GetProperties() + "%";
                        ZoneClients += "0|Bot|1|4|1035|187|0|0|0|0|0|0|380|300|0|1|999|0|0|I love Rock N Roll%";
                        ZoneClients += GetAreaClients();
                        Send(ZoneClients);
                        SendRoomXt(new string[] 
                        {
                            "ap", "-1" , GetProperties()
                        });
                    }
                    else SendXt(new string[] { "e", "-1", "210" });
                }
                else {
                    SendXt(new string[]
                    {
                        "jg", Zone, Zone
                    });
                }
            }
        }

        private int CountUsers(string Zone) {
            System.Collections.Generic.List<StateObject> ClientsInRoom = null;
            lock (IServer.Clients) {
                ClientsInRoom = IServer.Clients.FindAll(i => i.ZoneId == Zone);
            }
            if (ClientsInRoom != null) return ClientsInRoom.Count;
            else return 0;
        }

        public string GetProperties() {
            return string.Join("|", new string[] 
            {
                ID, Name, "1", Color, Head, Face, Neck, Body, Hands, Feet, Pin, Photo, X, Y, Frame, "1", Rank, "0", "0", Mood 
            });
        }

        public string GetAreaClients() {
            try {
                string Summary = null;
                System.Collections.Generic.List<StateObject> ZonedClients = null;
                lock (IServer.Clients) {
                    ZonedClients = IServer.Clients.FindAll(i => i.ZoneId == ZoneId);
                }
                foreach (StateObject Client in ZonedClients) if (!Client.ConnectionFired) Summary += Client.GetProperties() + "%";
                return Summary;
            }
            catch (System.Exception Exception) {
                if (Program.Debug) Utils.Log.Warning("Area", Exception.Message);
                return null;
            }
        }
    }
}
