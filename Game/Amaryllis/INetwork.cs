namespace Amaryllis {
    abstract class INetwork {
        public abstract void OnValidPacket(StateObject Sender);
        public abstract void OnDisconnection(StateObject Sender);
    }
}
