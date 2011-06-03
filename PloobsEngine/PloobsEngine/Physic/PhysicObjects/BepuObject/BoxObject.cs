using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics.Entities;
using Microsoft.Xna.Framework;
using BEPUphysics.DataStructures;
using BEPUphysics.Entities.Prefabs;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics.Bepu
{
    public class BoxObject : BepuEntityObject
    {
        public BoxObject(Vector3 pos, float xlen, float ylen, float zlen, float mass, Vector3 scale, Matrix orientation, MaterialDescription materialDescription)
            : base(materialDescription, mass)
        {
            this.scale = scale;
            entity = new Box(pos, xlen * scale.X, ylen* scale.Y, zlen* scale.Z, mass);
            entity.Orientation = Quaternion.CreateFromRotationMatrix(orientation);
            SetMaterialDescription(materialDescription);
        }        
        
        public override PhysicObjectTypes PhysicObjectTypes
        {
            get
            {
                return PhysicObjectTypes.BOXOBJECT;
            }            
        }
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }

    }
}
