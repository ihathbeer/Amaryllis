namespace Amaryllis.IPacket.GameZone {
    class Manager {
        public SledRace SledRace;

        public virtual void HandleGetWaddle(StateObject Client, string Input) {
            try {
                string Level2Packet = Input.Split('%')[5];
                switch (int.Parse(Level2Packet)) {
                    case 100:
                        SledRace.HandleGetWaddle(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
            catch (System.FormatException) {

            }
        }
        public virtual void HandleJoinWaddle(StateObject Client, string Input) {
            try {
                string Level2Packet = Input.Split('%')[5];
                if (int.Parse(Level2Packet) <= 103) {
                    SledRace.HandleJoinWaddle(Client, Input);
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
            catch (System.FormatException) {

            }
        }

        public virtual void HandleLeaveWaddle(StateObject Client, string Input) {
            switch (Client.GameZone) {
                case "none":
                    break;
                case "sr":
                    SledRace.HandleLeaveWaddle(Client, Input);
                    break;
            }
        }

        public virtual void HandleJoinZone(StateObject Client, string Input) {
            switch (Client.GameZone) {
                case "none":
                    break;
                case "sr":
                    SledRace.HandleJoinZone(Client, Input);
                    break;
            }
        }

        public virtual void HandleSendMove(StateObject Client, string Input) {
            switch (Client.GameZone) {
                case "none":
                    break;
                case "sr":
                    SledRace.HandleSendMove(Client, Input);
                    break;
            }
        }

        public void HandleGamePacket(StateObject Client, string Input) {
            try {
                string Level1Packet = Input.Split('%')[3];
                switch (Level1Packet) {
                    case "gw":
                        RemoveFromGames(Client);
                        HandleGetWaddle(Client, Input);
                        break;
                    case "jw":
                        HandleJoinWaddle(Client, Input);
                        break;
                    case "lw":
                        HandleLeaveWaddle(Client, Input);
                        break;
                    case "jz":
                        HandleJoinZone(Client, Input);
                        break;
                    case "zm":
                        HandleSendMove(Client, Input);
                        break;
                    case "zo":
                        HandleGetCoins(Client, Input);
                        break;
                }
            }

            catch (System.IndexOutOfRangeException) {

            }
        }

        private void HandleGetCoins(StateObject Client, string Input) {
            Client.SendXt(new string[] { "zo", Client.ZoneId, Client.Coins });
        }

        public virtual void HandleJoinGame(StateObject Client, string Input) {
            if (Input.Contains("999")) SledRace.HandleJoinGame(Client, Input);
        }

        public void RemoveFromGames(StateObject Client) {
            SledRace.SendToParticipants(Client, Client.GameZoneID, new string[]
            {
                "uw", "-1", Client.GameZoneID, Client.ParticipantID, string.Empty
            });
            Client.GameZone = "none";
            Client.GameZoneID = "none";
            Client.ParticipantID = "none";
            var SledParticipants = SledRace.Participants;
            for (int i = 100; i <= 103; i++) if (SledParticipants[i].Contains(Client)) SledRace.Participants[i].Remove(Client);
        }
    }
}
