using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using PloobsEngine.NetWorking;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physics.Bepu;
using Lidgren.Network;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;

namespace GameServer
{
    class GameClass : Game
    {
        NetworkServer server;        
        float elap = 0;

        public GameClass()
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);            
        }

        NetworkServerWorld world;
        protected override void Initialize()
        {
            
            base.Initialize();
            this.IsFixedTimeStep = false;
            Content.RootDirectory = "Content";

            base.LoadContent();
            Console.WriteLine("Starting server");

            world = new NetworkServerWorld(new BepuPhysicWorld(-9f, true, 1, true),false);
            
            server = new NetworkServer(world);

            NetWorkServerObject NetWorkServerObject1 = new NetWorkServerObject("tmesh",
                (mes) =>
                    {
                        TriangleMeshObject tmesh = mes.ReadTrianglemesh(Content);
                        return new ServerIObject(tmesh);
                    }
                    ,
                    (obj, min, mout) =>
                    {
                        return mout.CopyIncommingMessage(min, NetWorkingConstants.HeaderSizeinBytes );                        
                    }
                );
            server.CreateServerObject(NetWorkServerObject1);

            NetWorkServerObject NetWorkServerObject2 = new NetWorkServerObject("simpleball",
                (mes) =>
                {
                    SphereObject tmesh = mes.ReadSphere();
                    return new ServerIObject(tmesh);
                }
                    ,
                    (obj, min, mout) =>
                    {
                        return mout.CopyIncommingMessage(min, NetWorkingConstants.HeaderSizeinBytes);
                    }
                );
            server.CreateServerObject(NetWorkServerObject2);
        }

        
        protected override void Update(GameTime gameTime)
        {
                base.Update(gameTime);            

                    server.ProccessMessageSync();
                    world.Update(gameTime);

                    elap += (float)gameTime.ElapsedGameTime.Milliseconds;
                    if (elap > 500)
                    {
                        server.SyncAllClients();
                        elap = 0;
                    }
        }

         

    }
}

