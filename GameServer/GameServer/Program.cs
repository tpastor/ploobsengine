using System;
using Lidgren.Network;
using System.Threading;
using PloobsEngine.NetWorking;

namespace GameServer
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server");
            NetworkServer server = new NetworkServer();
            while (true)
            {
                server.ProccessMessageSync();                
            }
        }
    }
#endif
}

