using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics.Constraints.TwoEntity.Joints;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;

namespace PloobsEngine.Physic.Constraints.BepuConstraint
{
    public class PointPointConstraint : IPhysicConstraint
    {

        Joint joint;

        public Joint Joint
        {
            get { return joint; }
            set { joint = value; }
        }

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


        public override Microsoft.Xna.Framework.Vector3 Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override Physics.IPhysicObject BodyA
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override Physics.IPhysicObject BodyB
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override PhysicConstraintTypes PhysicConstraintType
        {
            get { return PhysicConstraintTypes.POINTPOINT; }
        }

        #if !WINDOWS_PHONE
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
