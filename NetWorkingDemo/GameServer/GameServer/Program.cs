using System;
using Lidgren.Network;
using System.Threading;
using PloobsEngine.NetWorking;
using PloobsEngine.Physics;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;

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

            GameClass game = new GameClass();
            game.Run();



        }
    }
#endif
}

