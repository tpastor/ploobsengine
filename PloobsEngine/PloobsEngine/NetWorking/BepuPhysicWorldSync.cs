using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physics;
using Lidgren.Network;
using BEPUphysics.Entities;

namespace PloobsEngine.NetWorking
{
    public static class BepuPhysicWorldSync
    {        
        public static void SendSyncPhysicMessages(this BepuPhysicWorld BepuPhysicWorld, NetServer server)
        {            
            foreach (var item in BepuPhysicWorld.Space.Entities)
	        {
                foreach (var con in server.Connections)
                {
                    NetOutgoingMessage mes = server.CreateMessage();
                    mes.Write((short)NetMessageType.PhysicSync);
                    mes.Write(item.GetHashCode());
                    mes.Write(item.Position);                    
                    mes.WriteRotation(item.Orientation);
                    mes.Write(item.LinearVelocity);
                    mes.Write(item.AngularVelocity);
                    con.SendMessage(mes,NetDeliveryMethod.Unreliable,0);                    
                }
	        } 
        }

        public static void StartRecieveSyncPhysicMessages(this BepuPhysicWorld BepuPhysicWorld,NetMessageType type, NetIncomingMessage mes)
        {
                    BEPUphysics.Entities.Entity ent = BepuPhysicWorld.Space.Entities.First(a => a.GetHashCode() == mes.ReadInt32());
                    ent.Position = mes.ReadVector3();
                    ent.Orientation = mes.ReadRotation();
                    ent.LinearVelocity = mes.ReadVector3();
                    ent.AngularVelocity = mes.ReadVector3();            
        }

    }
}

