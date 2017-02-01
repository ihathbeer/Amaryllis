namespace Amaryllis.IPacket {
    abstract class Packet {
        public abstract void HandlePacket(StateObject Client, string Input);
    }
}
