using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amaryllis
{
    class Program
    {
        static void Main(String[] args)
        {
            Int32 ServerPortInput = Int32.Parse(Console.ReadLine());
            IServer AuthenticationServer = new IServer(ServerPortInput);
            AuthenticationServer.Run();
            Console.Read();
        }
    }
}
