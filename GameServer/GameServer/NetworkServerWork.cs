using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physics;
using PloobsEngine.NetWorking;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;

namespace GameServer
{
    public static class NetworkServerWork
    {                
        public static void SendSyncPhysicMessages(this BepuPhysicWorld BepuPhysicWorld, NetworkServer server)
        {
            foreach (var item in BepuPhysicWorld.Space.Entities)
            {                
                NetOutgoingMessage mes = server.CreateMessage(NetMessageType.PhysicInternalSync);                
                mes.Write((int)item.Tag);
                mes.Write(item.Position);
                mes.WriteRotation(item.Orientation);
                mes.Write(item.LinearVelocity);
                mes.Write(item.AngularVelocity);
                server.SendMessageToAllClients(mes, NetDeliveryMethod.Unreliable);
            }
        }        
    }
}
