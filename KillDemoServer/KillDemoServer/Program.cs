using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Servers;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 88);
            server.Start();

            Console.ReadKey();
        }
    }
}
