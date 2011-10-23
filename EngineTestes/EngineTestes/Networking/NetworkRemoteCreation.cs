using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;
using PloobsEngine.NetWorking;
using PloobsEngine.Physics;
using PloobsEngine.Entity;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics.Bepu;

namespace EngineTestes.Networking
{
    public static class NetworkRemoteCreation
    {
        public static void SendTriangleMeshCreationOrder(this NetworkCliente cliente, int id, String objectName, Vector3 pos, Matrix rotation, Vector3 scale, MaterialDescription materialDescription)
        {
            NetOutgoingMessage mes = cliente.CreateMessage(NetMessageType.PhysicCreate);            
            mes.Write((int)PhysicObjectTypes.TRIANGLEMESHOBJECT);
            mes.Write(id);
            mes.Write(objectName);
            mes.Write(pos);
            mes.WriteRotation(Quaternion.CreateFromRotationMatrix(rotation));
            mes.Write(scale);
            mes.Write(materialDescription.Bounciness);
            mes.Write(materialDescription.DinamicFriction);
            mes.Write(materialDescription.StaticFriction);
            cliente.SendMessage(mes, NetDeliveryMethod.ReliableUnordered);
        }

        public static void SendSphereCreationOrder(this NetworkCliente cliente, int id, Vector3 pos, float raio, float mass, float scale, MaterialDescription materialDescription)
        {
            NetOutgoingMessage mes = cliente.CreateMessage(NetMessageType.PhysicCreate);                        
            mes.Write((int)PhysicObjectTypes.SPHEREOBJECT);
            mes.Write(id);
            mes.Write(pos);
            mes.Write(raio);
            mes.Write(mass);
            mes.Write(scale);
            mes.Write(materialDescription.Bounciness);
            mes.Write(materialDescription.DinamicFriction);
            mes.Write(materialDescription.StaticFriction);
            cliente.SendMessage(mes, NetDeliveryMethod.ReliableUnordered);
        }

        public static void StartRecieveSyncPhysicMessages(this BepuPhysicWorld BepuPhysicWorld, NetMessageType type, NetIncomingMessage mes)
        {
            int id= mes.ReadInt32();
            IObject obj = (IObject)EntityMapper.getInstance().getEntity(id);

            if (obj.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.TRIANGLEMESHOBJECT)
            {
                ///nao recevera este tipo de warning ...
            }
            else if (obj.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.SPHEREOBJECT
                || obj.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.BOXOBJECT
                )
            {
                BEPUphysics.Entities.Entity ent = (obj.PhysicObject as BepuEntityObject).Entity;
                ent.Position = mes.ReadVector3();
                ent.Orientation = mes.ReadRotation();
                ent.LinearVelocity = mes.ReadVector3();
                ent.AngularVelocity = mes.ReadVector3();
            }
            
        }

    }
}
