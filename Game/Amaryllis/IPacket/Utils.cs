namespace Amaryllis.IPacket {
    using Sockets = System.Net.Sockets;
    static class SocketUtils {
        public static bool IsConnected(this Sockets.Socket Socket) {
            try {
                return !(Socket.Poll(1, Sockets.SelectMode.SelectRead) && Socket.Available == 0);
            }
            catch (Sockets.SocketException) {
                return false;
            }
        }
    }
}
