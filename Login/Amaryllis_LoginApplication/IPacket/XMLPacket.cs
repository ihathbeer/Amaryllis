using System;
using System.Linq;
using System.Text;
using Amaryllis.IPacket;
using System.Collections.Generic;

namespace Amaryllis.IPacket
{
    class XMLPacket : Packet
    {
        public override void HandlePacket(StateObject Client, String Input)
        {
            if (Input.Contains("policy")) SendPolicy(Client, Input);
            else if (Input.Contains("verChk")) SendVersion(Client, Input);
            else if (Input.Contains("rndK")) SetKey(Client, Input);
            else if (Input.Contains("login")) VerifyClient(Client, Input);
        }

        private void SendPolicy(StateObject Client, String Input)
        {
            Client.Send("<cross-domain-policy><allow-access-from domain='*' to-ports='*' /></cross-domain-policy>");
        }

        private void SendVersion(StateObject Client, String Input)
        {
            Client.Send("<msg t='sys'><body action='apiOK' r='0'></body></msg>");
        }

        private void SetKey(StateObject Client, String Input)
        {
            Client.Key = new Random().Next(11000, 200000).ToString();
            Client.Send("<msg t='sys'><body action='rndK' r='-1'><k>" + Client.Key + "</k></body></msg>");
        }

        private void VerifyClient(StateObject Client, String Input)
        {
            try
            {
                Object[] ClientInformation = 
                {
                    Cryptography.Expressions.Replace (Input, "<nick><![CDATA[", "]]"),
                    Cryptography.Expressions.Replace (Input, "<pword><![CDATA[", "]]")
                };

                if (!Database.CheckForObjectExistence(ClientInformation[0].ToString(), false))
                {
                    Client.Send("%xt%e%-1%100%");
                    return;
                }

                String[] ClientDetails = Database.GetObjectProperties(new String[] 
                {
                    "ID", "Password", "Properties"
                }, ClientInformation[0].ToString(), false);


                if (ClientInformation[1].ToString() != Cryptography.Expressions.Swap(Cryptography.Expressions.MD5(Cryptography.Expressions.Swap(ClientDetails[1]) + Client.Key + "Y(02.>\'H}t\":E1")))
                {
                    Client.Send("%xt%e%-1%101%");
                    return;
                }

                if (ClientDetails[2].ToString() == "1") {
                    Client.Send("%xt%e%-1%603%");
                    return;
                }

                Char[] KeyChar = Client.Key.ToCharArray();
                KeyChar.Reverse();
                String PublicKey = Cryptography.Expressions.MD5(new String(KeyChar));
                Database.UpdateObjectProperty("lkey", PublicKey, ClientDetails[0]);
                Client.Send("%xt%l%-1%" + ClientDetails[0] + "%" + PublicKey + "%");
            }

            catch (IndexOutOfRangeException)
            {

            }
        }
    }
}
