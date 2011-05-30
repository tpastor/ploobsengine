using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics.Constraints.TwoEntity.Joints;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physic.Constraints.BepuConstraint
{
    public class PointPointConstraint : BepuPhysicConstraint
    {

        

        public PointPointConstraint(Vector3 position, IPhysicObject obA, IPhysicObject obB)
        {
            BepuEntityObject objA,objB;

            if (obA.PhysicObjectTypes != PhysicObjectTypes.TRIANGLEMESHOBJECT && obB.PhysicObjectTypes != PhysicObjectTypes.TRIANGLEMESHOBJECT)
            {
                objA = (BepuEntityObject)obA;
                objB = (BepuEntityObject)obB;
                joint = new BallSocketJoint(objA.Entity, objB.Entity, position);                
            }
            
        }


       

        public override PhysicConstraintTypes PhysicConstraintType
        {
            get { return PhysicConstraintTypes.POINTPOINT; }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
    }
}
