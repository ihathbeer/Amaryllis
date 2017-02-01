using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Amaryllis
{
    class StateObject
    {
        public Socket SCK;
        public Byte[] DataReceived = new Byte[255];
        public Int32 AmountOfDataReceived;
        public Int32 AmountOfDataSent;
        public String Key;

        public StateObject(Socket Socket)
        {
            SCK = Socket;
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            Utils.Log.Trace("Client Object", "Initialization has been made");
        }

        public void Send(String Input)
        {
            try
            {
                Byte[] BytesRequested = ASCIIEncoding.ASCII.GetBytes(Input + "\0");
                SCK.BeginSend(BytesRequested, 0, BytesRequested.Length, 0, new AsyncCallback(SendCallback), null);
            }

            catch (Exception Exception)
            {
                Utils.Log.Error("Client Socket", Exception.Message);
            }
        }

        private void SendCallback(IAsyncResult IAsyncResult)
        {
            try
            {
                AmountOfDataSent = SCK.EndSend(IAsyncResult);
            }

            catch (Exception Exception)
            {
                Utils.Log.Error("Client Socket", Exception.Message);
            }
        }
    }
}
