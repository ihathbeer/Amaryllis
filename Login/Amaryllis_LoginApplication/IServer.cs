using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Amaryllis
{
    class IServer
    {
        private Int32 MaximumConnectionsQueue = 200;
        private Int32 MinimumPacketLength = 0;
        private readonly Int32 PortNumber;
        private readonly Socket Socket;
        private delegate void HandleDisconnection(StateObject Sender);
        private delegate void HandleIO(StateObject Sender);
        private event HandleDisconnection FireObject;
        private event HandleIO ExposeIO;
        private IPacket.XMLPacket XMLPacket = new IPacket.XMLPacket();

        public IServer(Int32 Port)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            PortNumber = Port;
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            Socket.Bind(new IPEndPoint(IPAddress.Any, PortNumber));
            Socket.Listen(MaximumConnectionsQueue);
            Database.Init();
        }

        public void Run()
        {
            Utils.Log.Info("Amaryllis LS" , " Connections are being accepted");
            FireObject += new HandleDisconnection(OnDisconnection);
            ExposeIO += new HandleIO(OnValidPacket);
            Socket.BeginAccept(new AsyncCallback(IAccept), null);
        }

        private void IAccept(IAsyncResult IAsyncResult)
        {
            try
            {
                Socket.BeginAccept(new AsyncCallback(IAccept), null);
                Utils.Log.Trace("IServer", "A new client has connected");
                StateObject Client = new StateObject(Socket.EndAccept(IAsyncResult));

                try
                {
                    Client.SCK.BeginReceive(Client.DataReceived, 0, Client.DataReceived.Length, 0, new AsyncCallback(i => IReceive(i, Client)), null);
                }

                catch (Exception Exception)
                {
                    Utils.Log.Error("Client Socket", Exception.Message);
                    FireObject(Client);
                }
            }

            catch { }
        }

        private void IReceive(IAsyncResult IAsyncResult, StateObject Client)
        {
            try
            {
                Client.AmountOfDataReceived = Client.SCK.EndReceive(IAsyncResult);
            }


            catch (Exception Exception)
            {
                Utils.Log.Error("Client Socket", Exception.Message);
                FireObject(Client);
            }

            if (Client.AmountOfDataReceived == MinimumPacketLength)
            {
                FireObject(Client);
                return;
            }

            ExposeIO(Client); //Upon success
            try
            {
                Client.SCK.BeginReceive(Client.DataReceived, 0, Client.DataReceived.Length, 0, new AsyncCallback(i => IReceive(i, Client)), null);
            }

            catch (Exception Exception)
            {
                Utils.Log.Error("Client Socket", Exception.Message);
                FireObject(Client);
            }
        }

        private void OnDisconnection(StateObject Sender)
        {
            try
            {
                Sender.SCK.Close();
                Utils.Log.Info("OnDisconnection", " Client was disconnected");
            }

            catch (Exception Exception)
            {
                Utils.Log.Error("OnDisconnection", Exception.Message);
            }
        }

        private void OnValidPacket(StateObject Sender)
        {
            String StringReceived = ASCIIEncoding.ASCII.GetString(Sender.DataReceived);
            if (StringReceived.Contains("<")) XMLPacket.HandlePacket(Sender, StringReceived);
        }
    }
}
