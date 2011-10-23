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

namespace GameServer
{
    class GameClass : Game
    {
        NetworkServer server;
        BepuPhysicWorld pw;
        float elap = 0;

        public GameClass()
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);            
        }

        protected override void Initialize()
        {
            
            base.Initialize();
            this.IsFixedTimeStep = false;
            Content.RootDirectory = "Content";

            base.LoadContent();
            Console.WriteLine("Starting server");

            pw = new BepuPhysicWorld(-0.97f, false, 1, true);
            
            server = new NetworkServer();
            server.AddMessageHandler(NetMessageType.PhysicCreate, RecievePhysicObjects);                   
        }

        public void RecievePhysicObjects( NetMessageType mestype, NetIncomingMessage message)
        {
            PhysicObjectTypes type = (PhysicObjectTypes)message.ReadInt32();

            if (type == PhysicObjectTypes.TRIANGLEMESHOBJECT)
            {
                int id = message.ReadInt32();
                String objectName = message.ReadString();
                Vector3 pos = message.ReadVector3();
                Matrix oro = Matrix.CreateFromQuaternion(message.ReadRotation());
                Vector3 scale = message.ReadVector3();
                float Bounciness = message.ReadFloat();
                float DinamicFriction = message.ReadFloat();
                float StaticFriction = message.ReadFloat();
                

                TriangleMeshObject tmesh = new TriangleMeshObject(Content.Load<Model>(objectName), pos, oro, scale, new MaterialDescription(StaticFriction, DinamicFriction, Bounciness));
                pw.AddObject(tmesh);
                tmesh.StaticMesh.Tag = id;
            }
            else if (type == PhysicObjectTypes.SPHEREOBJECT)
            {
                int id = message.ReadInt32();
                Vector3 pos = message.ReadVector3();
                float raio = message.ReadFloat();
                float mass = message.ReadFloat();
                float scale = message.ReadFloat();
                float Bounciness = message.ReadFloat();
                float DinamicFriction = message.ReadFloat();
                float StaticFriction = message.ReadFloat();
                SphereObject tmesh = new SphereObject(pos, raio, mass, scale, new MaterialDescription(StaticFriction, DinamicFriction, Bounciness));
                pw.AddObject(tmesh);
                tmesh.Entity.Tag = id;
            }
        }       

        
        protected override void Update(GameTime gameTime)
        {
                base.Update(gameTime);            

                    server.ProccessMessageSync();            
                    pw.Update( gameTime);

                    elap += (float)gameTime.ElapsedGameTime.Milliseconds;
                    if (elap > 30)
                    {
                        pw.SendSyncPhysicMessages(server);
                        elap = 0;
                    }
        }
    }
}
