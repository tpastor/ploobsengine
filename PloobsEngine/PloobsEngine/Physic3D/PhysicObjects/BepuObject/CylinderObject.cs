using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BEPUphysics.Entities;
using BEPUphysics.Entities.Prefabs;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics.Bepu
{
    public class CylinderObject : BepuEntityObject
    {
        public CylinderObject(Vector3 pos, float altura, float raio, float mass,Matrix orientation,MaterialDescription md)
            : base(md,mass)
        {
            entity = new Cylinder(pos, altura, raio, mass);
            entity.Orientation = Quaternion.CreateFromRotationMatrix(orientation);            
        }
        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return PhysicObjectTypes.CYLINDEROBJECT; }
        }
        public override Vector3 Scale
        {
            get
            {
                return base.Scale;
            }
            set
            {
                ActiveLogger.LogMessage("Cant Set Capsule Scale, adjust the values in the construtor", LogLevel.RecoverableError);
                base.Scale = Vector3.One;
            }
        }

#if !WINDOWS_PHONE
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif
    }
}
