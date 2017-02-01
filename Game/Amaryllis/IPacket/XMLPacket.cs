namespace Amaryllis.IPacket {
    class XMLPacket : Packet {
        public override void HandlePacket(StateObject Client, string Input) {
            if (Input.Contains("policy")) SendPolicy(Client, Input);
            else if (Input.Contains("verChk")) SendVersion(Client, Input);
            else if (Input.Contains("rndK")) SetKey(Client, Input);
            else if (Input.Contains("login")) VerifyClient(Client, Input);
        }

        private void SendPolicy(StateObject Client, string Input) {
            Client.Send("<cross-domain-policy><allow-access-from domain='*' to-ports='*' /></cross-domain-policy>");
        }

        private void SendVersion(StateObject Client, string Input) {
            Client.Send("<msg t='sys'><body action='apiOK' r='0'></body></msg>");
        }

        private void SetKey(StateObject Client, string Input) {
            Client.Key = new System.Random().Next(11000, 200000).ToString();
            Client.Send("<msg t='sys'><body action='rndK' r='-1'><k>" + Client.Key + "</k></body></msg>");
        }

        private void VerifyClient(StateObject Client, string Input) {
            try {
                object[] ClientInformation = 
                {
                    Cryptography.Expressions.Replace (Input, "<nick><![CDATA[", "]]"),
                    Cryptography.Expressions.Replace (Input, "<pword><![CDATA[", "]]")
                };

                if (!Database.Database.CheckForObjectExistence(ClientInformation[0].ToString(), false)) {
                    Client.Send("%xt%e%-1%100%");
                    return;
                }

                string[] ClientDetails = Database.Database.GetObjectProperties(new string[] 
                {
                    "ID", "lkey" 
                }, ClientInformation[0].ToString(), false);


                if (ClientInformation[1].ToString() != Cryptography.Expressions.Swap(Cryptography.Expressions.MD5(ClientDetails[1] + Client.Key)) + ClientDetails[1]) {
                    Client.Send("%xt%e%-1%101%");
                    return;
                }

                Client.Name = ClientInformation[0].ToString();
                Client.ID = ClientDetails[0];
                Client.Send("%xt%l%-1%");
                Client.Setup();
            }
            catch (System.IndexOutOfRangeException) {

            }
        }
    }
}
