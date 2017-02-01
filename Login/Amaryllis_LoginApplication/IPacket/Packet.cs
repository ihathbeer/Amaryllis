using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amaryllis.IPacket
{
    abstract class Packet
    {
        abstract public void HandlePacket(StateObject Client, String Input);
    }
}
