using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amaryllis
{
    abstract class INetwork
    {
        public abstract void OnValidPacket(StateObject Sender);
        public abstract void OnDisconnection(StateObject Sender);
    }
}
