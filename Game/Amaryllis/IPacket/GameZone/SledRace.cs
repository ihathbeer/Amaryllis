namespace Amaryllis.IPacket.GameZone {
    using Generics = System.Collections.Generic;
    class SledRace : Manager {
        public Generics.Dictionary<int, Generics.List<StateObject>> Participants = new Generics.Dictionary<int, Generics.List<StateObject>>();
        public Generics.List<Generics.List<StateObject>> LinkedParticipants = new Generics.List<Generics.List<StateObject>> { };

        public SledRace() {
            /*<summary>
                Initialization is required to do further operations
            </summary>*/
            for (int i = 100; i <= 103; i++) Participants.Add(i, new Generics.List<StateObject>());
            if (Program.Debug) Utils.Log.Trace("GameZone", "Initialization has been made");
        }

        public override void HandleGetWaddle(StateObject Client, string Input) {
            try {
                string CurrentParticipants = string.Empty;
                Generics.List<int> ParticipantsList = new Generics.List<int>(Participants.Keys);
                foreach (int WaddleID in ParticipantsList) {
                    switch (WaddleID) {
                        case 103:
                            if (Participants[WaddleID].Count == 0) CurrentParticipants += WaddleID + "|" + "," + "%";
                            else {
                                var IParticipants = Participants[WaddleID].GetRange(0, Participants[WaddleID].Count);
                                Generics.List<string> ParticipantsNameList = new Generics.List<string> { };
                                foreach (var Participant in IParticipants) ParticipantsNameList.Add(Participant.Name);
                                if (ParticipantsNameList.Count == 1) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + ",%";
                                else CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + "%";
                            }
                            break;
                        case 102:
                            if (Participants[WaddleID].Count == 0) CurrentParticipants += WaddleID + "|" + "," + "%";
                            else {
                                var IParticipants = Participants[WaddleID].GetRange(0, Participants[WaddleID].Count);
                                Generics.List<string> ParticipantsNameList = new Generics.List<string> { };
                                foreach (var Participant in IParticipants) ParticipantsNameList.Add(Participant.Name);
                                if (ParticipantsNameList.Count == 1) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + ",%";
                                else CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + "%";
                            }
                            break;
                        case 101:
                            if (Participants[WaddleID].Count == 0) CurrentParticipants += WaddleID + "|" + ",," + "%";
                            else {
                                var IParticipants = Participants[WaddleID].GetRange(0, Participants[WaddleID].Count);
                                Generics.List<string> ParticipantsNameList = new Generics.List<string> { };
                                foreach (var Participant in IParticipants) ParticipantsNameList.Add(Participant.Name);
                                if (ParticipantsNameList.Count == 1) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + ",,%";
                                else if (ParticipantsNameList.Count == 2) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + ",%";
                                else if (ParticipantsNameList.Count == 3) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + "%";
                            }
                            break;
                        case 100:
                            if (Participants[WaddleID].Count == 0) CurrentParticipants += WaddleID + "|" + ",,," + "%";
                            else {
                                var IParticipants = Participants[WaddleID].GetRange(0, Participants[WaddleID].Count);
                                Generics.List<string> ParticipantsNameList = new Generics.List<string> { };
                                foreach (var Participant in IParticipants) ParticipantsNameList.Add(Participant.Name);
                                if (ParticipantsNameList.Count == 1) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + ",,,%";
                                else if (ParticipantsNameList.Count == 2) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + ",,%";
                                else if (ParticipantsNameList.Count == 3) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + ",%";
                                else if (ParticipantsNameList.Count == 4) CurrentParticipants += WaddleID + "|" + string.Join(",", ParticipantsNameList) + "%";
                            }
                            break;
                    }
                }

                Client.SendXt(new string[]
                {
                    "gw", Client.ZoneId, CurrentParticipants
                });

            }

            catch (System.Exception) {

            }
        }

        public override void HandleJoinWaddle(StateObject Client, string Input) {
            //Note: Remove request source if already in dc
            //Note: ID (which must be sent) is the amount of participants in array
            try {
                string RequestedWaddleID = Input.Split('%')[5];
                int WaddleKey = int.Parse(RequestedWaddleID);
                int ParticipantID = Participants[WaddleKey].Count;
                //Note: Add to dc

                Participants[WaddleKey].Add(Client);
                Client.GameZone = "sr";
                Client.GameZoneID = RequestedWaddleID;
                Client.ParticipantID = ParticipantID.ToString();
                /*<summary>
                    Property update
                </summary>*/
                switch (WaddleKey) {
                    case 103:
                        if (Participants[WaddleKey].Count == 2) {
                            Client.SendRoomXt(new string[]
                            {
                                "sw", Client.ZoneId, "999", (1 % 16384).ToString(), Participants[WaddleKey].Count.ToString()
                            });
                        }
                        break;
                    case 102:
                        if (Participants[WaddleKey].Count == 2) {
                            Client.SendRoomXt(new string[]
                            {
                                "sw", Client.ZoneId, "999", (1 % 16384).ToString(), Participants[WaddleKey].Count.ToString()
                            });
                        }
                        break;
                    case 101:
                        if (Participants[WaddleKey].Count == 3) {
                            Client.SendRoomXt(new string[]
                            {
                                "sw", Client.ZoneId, "999", (1 % 16384).ToString(), Participants[WaddleKey].Count.ToString()
                            });
                        }
                        break;
                    case 100:
                        if (Participants[WaddleKey].Count == 4) {
                            Client.SendRoomXt(new string[]
                            {
                                "sw", Client.ZoneId, "999", (1 % 16384).ToString(), Participants[WaddleKey].Count.ToString()
                            });
                        }
                        break;

                }

                Client.SendXt(new string[]
                {
                    "jw", Client.ZoneId, ParticipantID.ToString()
                });

                SendToParticipants(Client, Client.GameZoneID, new string[]
                {
                    "uw", "-1", RequestedWaddleID, ParticipantID.ToString(), Client.Name
                });
            }

            catch (System.IndexOutOfRangeException) {

            }
            catch (System.FormatException) {

            }
        }

        public override void HandleLeaveWaddle(StateObject Client, string Input) {
            try {
                if (Client.ParticipantID != "none") {
                    string WaddleID = string.Empty;
                    var ParticipantsDictionary = Participants;
                    for (int i = 100; i <= 103; i++) {
                        if (ParticipantsDictionary[i].Contains(Client)) {
                            Participants[i].Remove(Client);
                            WaddleID = i.ToString();
                            break;
                        }
                    }

                    SendToParticipants(Client, Client.GameZoneID, new string[]
                    {
                        "uw", "-1", WaddleID, Client.ParticipantID, string.Empty
                    });

                    Client.GameZone = "none";
                    Client.ParticipantID = "none";
                    Client.GameZoneID = "none";
                }
            }
            catch (System.Exception) {

            }
        }

        public override void HandleJoinZone(StateObject Client, string Input) {
            var ParticipantsInWaddle = Client.IServer.Clients.FindAll(o => o.GameZoneID == Client.GameZoneID);
            Generics.List<string> PlayerStringDetails = new Generics.List<string>();
            foreach (var Participant in ParticipantsInWaddle) PlayerStringDetails.Add(string.Join("|", new string[] { Participant.Name, Participant.Color, "15007", Participant.Name }));
            Client.SendXt(new string[] { "uz", "-1", "4", string.Join("%", PlayerStringDetails) });
        }

        public override void HandleJoinGame(StateObject Client, string Input) {
            try {
                if (Client.GameZone != "none") {
                    Client.SendRoomXt(new string[]
                    {
                        "rp", "-1", Client.ID, "0"
                    });

                    SendToParticipants(Client, Client.GameZoneID, new string[]
                    {
                        "uw", "-1", Client.GameZoneID, Client.ParticipantID, string.Empty
                    });

                    Client.ZoneId = "01";
                    Client.ParticipantID = "none";

                    string[] GameDetails = Input.Split('%');
                    SendToParticipants(Client, Client.GameZoneID, new string[] { "jx", GameDetails[4], "999" });
                    UpdateScore(Client);
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public override void HandleSendMove(StateObject Client, string Input) {
            try {
                string[] MoveDetails = Input.Split('%');
                SendToParticipants(Client, Client.GameZoneID, new string[] { "zm", MoveDetails[4], MoveDetails[5], MoveDetails[6], MoveDetails[7], MoveDetails[8] });
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        public void SendToParticipants(StateObject Client, string WaddleID, string[] Input) {
            try {
                var ParticipantsInWaddle = Client.IServer.Clients.FindAll(o => o.GameZoneID == WaddleID);
                foreach (var Participant in ParticipantsInWaddle) {
                    if (Participant.GameZone != "none") {
                        Participant.SendXt(Input);
                    }
                }
            }
            catch (System.Exception) {

            }
        }

        private void UpdateScore(StateObject Client) {
            try {
                if (Database.Database.CheckForObjectExistence(Client.Name, false, "scores")) {
                    int CurrentScore = int.Parse(Database.Database.GetObjectProperty("games", Client.Name, false, "scores"));
                    CurrentScore += 100;
                    Database.Database.UpdateObjectProperty("games", CurrentScore.ToString(), Client.Name, false, "scores");
                }
                else
                    Database.Database.InsertObjectProperty("games", new string[] { Client.Name, "100", "1" }, "scores");
            }
            catch (System.Exception) {

            }
        }
    }
}
