namespace Amaryllis.IPacket {
    class RawPacket : Packet {
        public char[] PacketSeparators = 
        { 
            '#',
            '%' 
        };

        public override void HandlePacket(StateObject Client, string Input) {
            try {
                if (!Input.Contains(PacketSeparators[1].ToString())) return;
                string[] PacketType = Input.Split('%');
                if (PacketType[2] == "s") HandleStandardPacket(Client, Input);
                else if (PacketType[2] == "z") HandleGamePacket(Client, Input);
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleStandardPacket(StateObject Client, string Input) {
            if (Input.Contains("j#js")) {
                HandleJoinServer(Client, Input);
                return;
            }

            try {
                string[] PacketLevel = Input.Split('%')[3].Split('#');
                switch (PacketLevel[0]) {
                    case "j":
                        HandleJoinPacket(Client, Input, PacketLevel);
                        break;
                    case "f":
                        HandleEPFPacket(Client, Input, PacketLevel);
                        break;
                    case "i":
                        HandleInventoryPacket(Client, Input, PacketLevel);
                        break;
                    case "u":
                        HandleUserPacket(Client, Input, PacketLevel);
                        break;
                    case "m":
                        HandleMessagePacket(Client, Input, PacketLevel);
                        break;
                    case "s":
                        HandleSettingPacket(Client, Input, PacketLevel);
                        break;
                    case "b":
                        HandleBuddyPacket(Client, Input, PacketLevel);
                        break;
                    case "g":
                        HandleIglooPacket(Client, Input, PacketLevel);
                        break;
                    case "p":
                        HandlePetPacket(Client, Input, PacketLevel);
                        break;
                    case "w":
                        HandleWaddlePacket(Client, Input, PacketLevel);
                        break;
                    case "iCP":
                        HandleIcpPacket(Client, Input, PacketLevel);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleGamePacket(StateObject Client, string Input) {
            Client.IServer.GameManager.HandleGamePacket(Client, Input);
        }

        //<summary>
        // Level1 Methods
        //</summary>

        public void HandleJoinPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "jr":
                        HandleJoinRoom(Client, Input);
                        break;
                    case "jp":
                        HandleJoinPlayer(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleInventoryPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "gi":
                        HandleGetItems(Client, Input);
                        break;
                    case "ai":
                        HandleAddItem(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleEPFPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "epfgf":
                        // HandleJoinServer(Client, Input); //It is required
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUserPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "h":
                        HandleHeartBeat(Client, Input);
                        break;
                    case "sp":
                        HandleSendPosition(Client, Input);
                        break;
                    case "gp":
                        HandleGetPlayer(Client, Input);
                        break;
                    case "sa":
                        HandleSendAction(Client, Input);
                        break;
                    case "se":
                        HandleSendEmoticon(Client, Input);
                        break;
                    case "sf":
                        HandleSendFrame(Client, Input);
                        break;
                    case "sb":
                        HandleSendBall(Client, Input);
                        break;
                }
            }
            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleMessagePacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "sm":
                        HandleSendMessage(Client, Input);
                        break;
                }
            }
            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleSettingPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "upc":
                        HandleUpdateColor(Client, Input);
                        break;
                    case "uph":
                        HandleUpdateHead(Client, Input);
                        break;
                    case "upf":
                        HandleUpdateFace(Client, Input);
                        break;
                    case "upn":
                        HandleUpdateNeck(Client, Input);
                        break;
                    case "upb":
                        HandleUpdateBody(Client, Input);
                        break;
                    case "upa":
                        HandleUpdateHands(Client, Input);
                        break;
                    case "upe":
                        HandleUpdateFeet(Client, Input);
                        break;
                    case "upp":
                        HandleUpdatePhoto(Client, Input);
                        break;
                    case "upl":
                        HandleUpdatePin(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleBuddyPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {

                    case "gb":
                        HandleGetBuddies(Client, Input);
                        break;
                    case "br":
                        HandleBuddyRequest(Client, Input);
                        break;
                    case "ba":
                        HandleBuddyAccept(Client, Input);
                        break;
                    case "rb":
                        HandleRemoveBuddy(Client, Input);
                        break;
                    case "bf":
                        HandleBuddyFind(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleIglooPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "gm":
                        HandleGetIglooFurniture(Client, Input);
                        break;
                    case "go":
                        HandleEditIgloo(Client, Input);
                        break;
                    case "gf":
                        HandleGetFurniture(Client, Input);
                        break;
                    case "af":
                        HandleAddFurniture(Client, Input);
                        break;
                    case "ur":
                        HandleUpdateRoom(Client, Input);
                        break;
                    case "au":
                        HandleUpdateIgloo(Client, Input);
                        break;
                    case "um":
                        HandleUpdateMusic(Client, Input);
                        break;
                    case "ag":
                        HandleUpdateFloor(Client, Input);
                        break;
                    case "or":
                        HandleOpenIgloo(Client, Input);
                        break;
                    case "cr":
                        HandleCloseIgloo(Client, Input);
                        break;
                    case "gr":
                        HandleGetIglooList(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandlePetPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "pg":
                        HandleGetPuffles(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleWaddlePacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "jx":
                        Client.IServer.GameManager.HandleJoinGame(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleIcpPacket(StateObject Client, string Input, string[] PacketLevel) {
            try {
                switch (PacketLevel[1]) {
                    case "umo":
                        HandleUpdateMood(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        //<summary>
        // Level2 Methods
        //</summary>

        public void HandleJoinServer(StateObject Client, string Input) {
            Client.SendXt(new string[] { 
                "js", "-1", "0", "1", "0", "0" 
            });
        }

        public void HandleJoinRoom(StateObject Client, string Input) {
            try {
                string[] RoomDetails = Input.Split('%');
                Client.JoinZone(RoomDetails[5], RoomDetails[6], RoomDetails[7]);
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleJoinPlayer(StateObject Client, string Input) {
            try {
                string[] PlayerDetails = Input.Split('%');
                Client.SendXt(new string[]
                {
                    "jp", "-1", PlayerDetails[5]
                });
                Client.JoinZone(PlayerDetails[5]);
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleGetItems(StateObject Client, string Input) {
            Client.SendXt(new string[] { 
                "gps", "-1", Client.ID, "9|10|11|14|20|183" 
            });

            Client.SendXt(new string[] { 
                "glr", "-1", "3555" 
            });

            Client.SendXt(new string[] { 
                "lp", "-1", Client.GetProperties() , Client.Coins, "0", "1440", "1200000000000", "1997", "4", "1997", " ", "7"
            });

            Client.SendXt(new string[] { 
                 "gi", "-1", Client.Inventory
            });

            string[] Rooms = new string[]
            {
               "100", "110", "800", "805"
            };

            System.Random Picker = new System.Random();
            int RoomIndex = Picker.Next(0, Rooms.Length - 1);
            Client.JoinZone(Rooms[RoomIndex]);
        }

        public void HandleAddItem(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = new string[9];
                if (!Root) ItemDetails = Input.Split('%');
                string[] ItemArray = Client.Inventory.Split('%');
                if(!Cryptography.Expressions.CheckStringContent(ItemArray, (Root ? Input : ItemDetails[5]))){
                    Client.Inventory += "%" + (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Inventory", Client.Inventory, Client.ID);
                    Client.SendXt(new string[]
                    {
                        "ai", "-1", (Root ? Input : ItemDetails[5])
                    });
                }
                else {
                    Client.SendXt(new string[]
                    {
                        "e", "-1", "400"
                    });
                }

            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleSendPosition(StateObject Client, string Input) {
            try {
                string[] PositionDetails = Input.Split('%');
                Client.X = PositionDetails[5];
                Client.Y = PositionDetails[6];
                Client.SendRoomXt(new string[]
                {
                    "sp", "-1", Client.ID, Client.X, Client.Y
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }


        public void HandleHeartBeat(StateObject Client, string Input) {
            try {
                string[] BeatDetails = Input.Split('%');
                Client.SendXt(new string[]
                {
                    "h", BeatDetails[4]
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleSendMessage(StateObject Client, string Input) {
            try {
                string[] MessageDetails = Input.Split('%');
                Client.SendRoomXt(new string[]
                {
                    "sm", "-1", MessageDetails[5], MessageDetails[6]
                });

                if (MessageDetails[6].StartsWith("!")) {
                    string[] ArgumentDetails = null;
                    bool ContainsArgument = MessageDetails[6].Contains(" ") ? true : false;
                    if (ContainsArgument) ArgumentDetails = MessageDetails[6].Split(' ');
                    MessageDetails[6] = MessageDetails[6].Trim();
                    if (MessageDetails[6].ToUpper() == "!PING") {
                        Client.SendXt(new string[]
                        {
                            "sm", "-1", "0", "Pong"
                        });
                        return;
                    }
                    else if (MessageDetails[6].ToUpper() == "!USERS") {
                        Client.SendXt(new string[]
                        {
                            "sm", "-1", "0", "There are " + Client.IServer.Clients.Count.ToString() + " players on this server"
                        });
                        return;
                    }
                    if (ArgumentDetails[0].ToUpper() == "!ADD" || ArgumentDetails[0].ToUpper() == "!AI") {
                        if (ContainsArgument) {
                            HandleAddItem(Client, ArgumentDetails[1], true);
                            return;
                        }
                    }
                    else if (ArgumentDetails[0].ToUpper() == "!AC") {
                        if (ContainsArgument) {
                            HandleAddCoins(Client, ArgumentDetails[1]);
                            return;
                        }
                    }
                    else if (ArgumentDetails[0].ToUpper() == "!PLYM") {
                        if (ContainsArgument && Cryptography.Expressions.CheckCollectionContent(Client.IServer.Administrators, Client.Name.ToLower())) {
                            Client.SendRoomXt(new string[]
                            {
                                "lm", "-1", "http://atlanticpengu.in/jay/music.swf?id=" + ArgumentDetails[1]
                            });
                        }
                    }
                    else if (ArgumentDetails[0].ToUpper() == "!SWF") {
                        if (ContainsArgument && Cryptography.Expressions.CheckCollectionContent(Client.IServer.Administrators, Client.Name.ToLower())) {
                            Client.SendRoomXt(new string[]
                            {
                                "lm", "-1", ArgumentDetails[1]
                            });
                        }
                    }
                    else if (ArgumentDetails[0].ToUpper() == "!UP") {
                        if (ContainsArgument) {
                            switch (ArgumentDetails[1].ToUpper()) {
                                case "GARY":
                                case "GA":
                                    HandleUpdateColor(Client, "1", true);
                                    HandleUpdateHead(Client, "0", true);
                                    HandleUpdateFace(Client, "115", true);
                                    HandleUpdateNeck(Client, "0", true);
                                    HandleUpdateBody(Client, "4022", true);
                                    HandleUpdateHands(Client, "0", true);
                                    HandleUpdateFeet(Client, "0", true);
                                    HandleUpdatePhoto(Client, "0", true);
                                    break;
                                case "AUNT":
                                case "AA":
                                    HandleUpdateColor(Client, "2", true);
                                    HandleUpdateHead(Client, "1044", true);
                                    HandleUpdateFace(Client, "2007", true);
                                    HandleUpdateNeck(Client, "0", true);
                                    HandleUpdateBody(Client, "0", true);
                                    HandleUpdateHands(Client, "0", true);
                                    HandleUpdateFeet(Client, "0", true);
                                    HandleUpdatePhoto(Client, "0", true);
                                    break;
                                case "ROCKHOPPER":
                                case "RH":
                                    HandleUpdateColor(Client, "5", true);
                                    HandleUpdateHead(Client, "442", true);
                                    HandleUpdateFace(Client, "152", true);
                                    HandleUpdateNeck(Client, "161", true);
                                    HandleUpdateBody(Client, "0", true);
                                    HandleUpdateHands(Client, "5020", true);
                                    HandleUpdateFeet(Client, "0", true);
                                    HandleUpdatePhoto(Client, "0", true);
                                    break;
                                case "SENSEI":
                                case "SS":
                                    HandleUpdateColor(Client, "14", true);
                                    HandleUpdateHead(Client, "1200", true);
                                    HandleUpdateFace(Client, "2009", true);
                                    HandleUpdateNeck(Client, "0", true);
                                    HandleUpdateBody(Client, "4281", true);
                                    HandleUpdateHands(Client, "0", true);
                                    HandleUpdateFeet(Client, "0", true);
                                    HandleUpdatePhoto(Client, "0", true);
                                    break;
                                case "CADENCE":
                                case "CD":
                                    HandleUpdateColor(Client, "10", true);
                                    HandleUpdateHead(Client, "1032", true);
                                    HandleUpdateFace(Client, "1033", true);
                                    HandleUpdateNeck(Client, "3011", true);
                                    HandleUpdateBody(Client, "0", true);
                                    HandleUpdateHands(Client, "1034", true);
                                    HandleUpdateFeet(Client, "0", true);
                                    HandleUpdatePhoto(Client, "0", true);
                                    break;
                            }
                        }
                    }
                    else if (ArgumentDetails[0].ToUpper() == "!AF") {
                        if (ContainsArgument) {
                            HandleAddFurniture(Client, ArgumentDetails[1], true);
                        }
                    }
                    else if (ArgumentDetails[0].ToUpper() == "!UI" || ArgumentDetails[0].ToUpper() == "!IGLOO") {
                        if (ContainsArgument) {
                            HandleUpdateIgloo(Client, ArgumentDetails[1], true);
                        }
                    }
                    else if (ArgumentDetails[0].ToUpper() == "!UM" || ArgumentDetails[0].ToUpper() == "!MUSIC") {
                        if (ContainsArgument) {
                            HandleUpdateMusic(Client, ArgumentDetails[1], true);
                        }
                    }
                    else if (ArgumentDetails[0].ToUpper() == "!UF" || ArgumentDetails[0].ToUpper() == "!FLOOR") {
                        if (ContainsArgument) {
                            HandleUpdateFloor(Client, ArgumentDetails[1], true);
                        }
                    }
                }
            }

            catch (System.Exception) {

            }
        }

        public void HandleUpdateColor(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Color != (Root ? Input : ItemDetails[5])) {
                    Client.Color = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Color", Client.Color, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "upc", "-1", Client.ID, Client.Color
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateHead(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Head != (Root ? Input : ItemDetails[5])) {
                    Client.Head = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Head", Client.Head, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "uph", "-1", Client.ID, Client.Head
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateFace(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Face != (Root ? Input : ItemDetails[5])) {
                    Client.Face = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Face", Client.Face, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "upf", "-1", Client.ID, Client.Face
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateNeck(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Neck != (Root ? Input : ItemDetails[5])) {
                    Client.Neck = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Neck", Client.Neck, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "upn", "-1", Client.ID, Client.Neck
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateBody(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Body != (Root ? Input : ItemDetails[5])) {
                    Client.Body = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Body", Client.Body, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "upb", "-1", Client.ID, Client.Body
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateHands(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Hands != (Root ? Input : ItemDetails[5])) {
                    Client.Hands = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Hands", Client.Hands, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "upa", "-1", Client.ID, Client.Hands
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateFeet(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Feet != (Root ? Input : ItemDetails[5])) {
                    Client.Feet = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Feet", Client.Feet, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "upe", "-1", Client.ID, Client.Feet
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdatePhoto(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Photo != (Root ? Input : ItemDetails[5])) {
                    Client.Photo = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Photo", Client.Photo, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "upp", "-1", Client.ID, Client.Photo
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdatePin(StateObject Client, string Input, bool Root = false) {
            try {
                string[] ItemDetails = Input.Split('%');
                if (Client.Pin != (Root ? Input : ItemDetails[5])) {
                    Client.Pin = (Root ? Input : ItemDetails[5]);
                    Database.Database.UpdateObjectProperty("Pin", Client.Pin, Client.ID);
                    Client.SendRoomXt(new string[]
                    {
                        "upl", "-1", Client.ID, Client.Pin
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleGetBuddies(StateObject Client, string Input) {
            try {
                string BuddyString = string.Empty;
                foreach (string Buddy in Client.Buddies) {
                    if (Database.Database.CheckForObjectExistence(Buddy)) {
                        string BuddyName = Database.Database.GetObjectProperty("Name", Buddy);
                        bool Online = false;
                        System.Collections.Generic.List<StateObject> CurrentClients = null;
                        lock (Client.IServer.Clients) {
                            CurrentClients = Client.IServer.Clients.GetRange(0, Client.IServer.Clients.Count);
                        }
                        foreach (StateObject Object in CurrentClients) {
                            if (Object.ID == Buddy) {
                                if (!Client.ConnectionFired) {
                                    Online = true;
                                    break;
                                }
                            }
                        }

                        BuddyString += string.Join("|", new string[] { Buddy, BuddyName, Online ? "1" : "0" }) + "%";
                    }
                    else {
                        BuddyString = string.Empty;
                        Database.Database.UpdateObjectProperty("Buddies", BuddyString, Buddy);
                        break;
                    }
                }
                if (BuddyString == string.Empty) BuddyString = ""; //%
                Client.SendXt(new string[]{
                	"gb", "-1", BuddyString
            	});

            }
            catch (System.Exception) {

            }
        }

        public void HandleBuddyRequest(StateObject Client, string Input) {
            try {
                string[] BuddyDetails = Input.Split('%');
                System.Collections.Generic.List<StateObject> CurrentClients = null;
                lock (Client.IServer.Clients) {
                    CurrentClients = Client.IServer.Clients.GetRange(0, Client.IServer.Clients.Count);
                }
                foreach (StateObject Object in CurrentClients) {
                    if (Object.ID == BuddyDetails[5]) {
                        if (!Client.ConnectionFired) {
                            Object.SendXt(new string[] 
                            {
                                "br", "-1", Client.ID, Client.Name 
                            });
                        }
                        break;
                    }
                }
            }

            catch (System.Exception) {

            }
        }

        public void HandleBuddyAccept(StateObject Client, string Input) {
            try {
                string[] BuddyDetails = Input.Split('%');

                System.Collections.Generic.List<StateObject> CurrentClients = null;
                lock (Client.IServer.Clients) {
                    CurrentClients = Client.IServer.Clients.GetRange(0, Client.IServer.Clients.Count);
                }
                foreach (StateObject Object in CurrentClients) {
                    if (Object.ID == BuddyDetails[5]) {
                        string MyBuddies = string.Join(",", Client.Buddies);
                        string HisBuddies = string.Join(",", Object.Buddies);

                        Database.Database.UpdateObjectProperty("Buddies", (MyBuddies == "0" || MyBuddies == null || MyBuddies == "") ? Object.ID : MyBuddies + "," + Object.ID, Client.ID);
                        Database.Database.UpdateObjectProperty("Buddies", (HisBuddies == "0" || HisBuddies == null || HisBuddies == "") ? Client.ID : HisBuddies + "," + Client.ID, Object.ID);
                        Object.SendXt(new string[]
                        {
                            "ba", "-1", Client.ID, Client.Name
                        });
                        break;
                    }
                }
            }

            catch (System.Exception) {

            }
        }

        public void HandleRemoveBuddy(StateObject Client, string Input) {
            try {
                string[] BuddyDetails = Input.Split('%');
                string[] MyBuddies = Database.Database.GetObjectProperty("Buddies", Client.ID).Split(',');
                string[] HisBuddies = Database.Database.GetObjectProperty("Buddies", BuddyDetails[5]).Split(',');

                System.Collections.Generic.List<string> MyBuddies_LIST = new System.Collections.Generic.List<string>(MyBuddies);
                System.Collections.Generic.List<string> HisBuddies_LIST = new System.Collections.Generic.List<string>(HisBuddies);

                foreach (string BuddyID in MyBuddies) {
                    if (BuddyID == BuddyDetails[5]) {
                        MyBuddies_LIST.Remove(BuddyID);
                        break;
                    }
                }

                foreach (string BuddyID in HisBuddies) {
                    if (BuddyID == Client.ID) {
                        HisBuddies_LIST.Remove(BuddyID);
                        break;
                    }
                }

                Database.Database.UpdateObjectProperty("Buddies", string.Join(",", MyBuddies_LIST.ToArray()), Client.ID);
                Database.Database.UpdateObjectProperty("Buddies", string.Join(",", HisBuddies_LIST.ToArray()), BuddyDetails[5]);
            }

            catch (System.Exception) {

            }
        }

        public void HandleBuddyFind(StateObject Client, string Input) {
            try {
                bool Found = false;
                string[] BuddyDetails = Input.Split('%');
                System.Collections.Generic.List<StateObject> CurrentClients = null;
                lock (Client.IServer.Clients) {
                    CurrentClients = Client.IServer.Clients.GetRange(0, Client.IServer.Clients.Count);
                }
                foreach (StateObject Object in CurrentClients) {
                    if (Object.ID == BuddyDetails[5]) {
                        Client.SendXt(new string[]
                        {
                            "bf", "-1", Object.ZoneId
                        });
                        Found = true;
                        break;
                    }
                }

                if (!Found) {
                    Client.SendXt(new string[]
                    {
                        "bf", "-1", "100"
                    });
                }
            }

            catch (System.Exception) {

            }
        }

        public void HandleGetPlayer(StateObject Client, string Input) {
            try {
                string[] PlayerDetails = Input.Split('%');
                string[] PlayerProperties = Database.Database.GetObjectProperties(new string[] { "Name", "Properties", "Color", "Head", "Face", "Neck", "Body", "Hands", "Feet", "Pin", "Photo", "Rank", "Mood" }, PlayerDetails[5]);
                Client.SendXt(new string[] { "gp", "-1", PlayerDetails[5] + "|" + string.Join("|", PlayerProperties) });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleSendAction(StateObject Client, string Input) {
            try {
                string[] ActionDetails = Input.Split('%');
                Client.SendRoomXt(new string[]
                {
                    "sa", "-1", Client.ID, ActionDetails[5]
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleSendEmoticon(StateObject Client, string Input) {
            try {
                string[] EmoticonDetails = Input.Split('%');
                Client.SendRoomXt(new string[]
                {
                    "se", "-1", Client.ID, EmoticonDetails[5]
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleSendFrame(StateObject Client, string Input) {
            try {
                string[] FrameDetails = Input.Split('%');
                if (FrameDetails[5] != Client.Frame) {
                    Client.Frame = FrameDetails[5];
                    Client.SendRoomXt(new string[]
                    {
                        "sf", "-1", Client.ID, FrameDetails[5]
                    });
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        private void HandleSendBall(StateObject Client, string Input) {
            try {
                string[] BallDetails = Input.Split('%');
                Client.SendRoomXt(new string[]
                {
                    "sb", Client.ZoneId, Client.ID, BallDetails[5], BallDetails[6]
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleGetIglooFurniture(StateObject Client, string Input) {
            try {
                string[] IglooDetails = Input.Split('%');
                string[] IglooProperties = Database.Database.GetObjectProperties(new string[] 
                {
                    "Igloo", "Music", "Floor", "IglooFurniture"
                }, IglooDetails[5]);

                Client.SendXt(new string[]
                {
                    "gm", Client.ZoneId, IglooDetails[5], IglooProperties[0], IglooProperties[1], IglooProperties[2], IglooProperties[3]
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleEditIgloo(StateObject Client, string Input) {
            Client.SendXt(new string[]
            {
                "go", Client.ZoneId, "1"
            });
        }

        public void HandleGetFurniture(StateObject Client, string Input) {
            Client.SendXt(new string[]
            {
                "gf", Client.ZoneId, Client.IglooInventory
            });
        }

        public void HandleAddFurniture(StateObject Client, string Input, bool Root = false) {
            try {
                string[] FurnitureDetails = Input.Split('%');
                if (Client.IglooInventory != string.Empty) {
                    Client.IglooInventory += "%" + (Root ? Input : FurnitureDetails[5]) + "|1";
                    Database.Database.UpdateObjectProperty("IglooInventory", Client.IglooInventory, Client.ID);
                }
                else {
                    Client.IglooInventory += (Root ? Input : FurnitureDetails[5]) + "|1";
                    Database.Database.UpdateObjectProperty("IglooInventory", Client.IglooInventory, Client.ID);
                }
                Client.SendXt(new string[] 
                {
                    "af", Client.ZoneId, (Root ? Input : FurnitureDetails[5]), Client.Coins
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateRoom(StateObject Client, string Input) {
            try {
                string SerializedString = string.Empty;
                int Position = 0;
                string[] RoomDetails = Input.Split('%');

                foreach (string Property in RoomDetails) {
                    if (Position >= 5) {
                        string Comma = (Client.IglooFurniture == null || Client.IglooFurniture == "") ? "" : ",";
                        SerializedString += Comma + Property;
                    }
                    Position++;
                }
                Client.IglooFurniture = SerializedString;
                Database.Database.UpdateObjectProperty("IglooFurniture", SerializedString, Client.ID);
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateMusic(StateObject Client, string Input, bool Root = false) {
            try {
                string[] MusicDetails = Input.Split('%');
                Database.Database.UpdateObjectProperty("Music", (Root ? Input : MusicDetails[5]), Client.ID);
                Client.Music = (Root ? Input : MusicDetails[5]);
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateFloor(StateObject Client, string Input, bool Root = false) {
            try {
                string[] FloorDetails = Input.Split('%');
                Database.Database.UpdateObjectProperty("Floor", (Root ? Input : FloorDetails[5]), Client.ID);
                Client.Floor = (Root ? Input : FloorDetails[5]);
                Client.SendXt(new string[]
                {
                    "ag", Client.ZoneId, (Root ? Input : FloorDetails[5]), Client.Coins
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleUpdateIgloo(StateObject Client, string Input, bool Root = false) {
            try {
                string[] IglooDetails = Input.Split('%');
                Database.Database.UpdateObjectProperty("Igloo", (Root ? Input : IglooDetails[5]), Client.ID);
                Client.Igloo = (Root ? Input : IglooDetails[5]);
                Database.Database.UpdateObjectProperty("IglooFurniture", string.Empty, Client.ID);
                Client.IglooFurniture = string.Empty;
                Database.Database.UpdateObjectProperty("Floor", "0", Client.ID);
                Database.Database.UpdateObjectProperty("Music", "0", Client.ID);
                Client.Floor = "0";
                Client.Music = "0";
                Client.SendXt(new string[]
                {
                    "au", Client.ZoneId, (Root ? Input : IglooDetails[5]), Client.Coins
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void HandleOpenIgloo(StateObject Client, string Input) {
            Client.IServer.OpenIgloos.Add(string.Join("|", new string[] { Client.ID, Client.Name }));
        }

        public void HandleCloseIgloo(StateObject Client, string Input) {
            if (Client.IServer.OpenIgloos.Contains(string.Join("|", new string[] { Client.ID, Client.Name }))) {
                Client.IServer.OpenIgloos.Remove(string.Join("|", new string[] { Client.ID, Client.Name }));
            }
        }

        public void HandleGetIglooList(StateObject Client, string Input) {
            var Igloos = Client.IServer.OpenIgloos.GetRange(0, Client.IServer.OpenIgloos.Count);
            string OpenIgloos = string.Empty;
            if (Igloos.Count > 0) {
                foreach (string Igloo in Igloos) {
                    OpenIgloos += Igloo + "%";
                }
            }
            Client.Send("%xt%gr%" + Client.ZoneId + "%" + (OpenIgloos == string.Empty ? "" : OpenIgloos));
        }

        public void HandleGetPuffles(StateObject Client, string Input) {
            Client.SendXt(new string[]
            {
                "pg", Client.ZoneId, "0"
            });
        }

        public void HandleAddCoins(StateObject Client, string Amount) {
            try {
                if (Amount.Length <= 6) Client.Coins = (int.Parse(Client.Coins) + int.Parse(Amount)).ToString();
                else Client.Coins = (int.Parse(Client.Coins) + 10000).ToString();
                Client.SendXt(new string[]
                {
                    "zo", Client.Coins, Amount
                });

                Database.Database.UpdateObjectProperty("Coins", Client.Coins, Client.ID);
            }

            catch (System.Exception Exception) {
                if (Program.Debug) Utils.Log.Warning("Handler", Exception.Message);
            }
        }

        public void HandleUpdateMood(StateObject Client, string Input) {
            try {
                string[] MoodDetails = Input.Split('%');
                Client.Mood = MoodDetails[5];
                Database.Database.UpdateObjectProperty("Mood", Client.Mood, Client.ID);
                Client.SendXt(new string[]
                {
                    "umo", Client.ID, Client.Mood
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }
    }
}
