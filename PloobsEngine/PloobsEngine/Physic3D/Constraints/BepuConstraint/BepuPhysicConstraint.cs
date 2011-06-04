using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics.Constraints.TwoEntity.Joints;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physic.Constraints.BepuConstraint
{
    public class BepuPhysicConstraint : IPhysicConstraint
    {

        protected Joint joint;

        public Joint Joint
        {
            get { return joint; }
            set { joint = value; }
        }




        public override Microsoft.Xna.Framework.Vector3 Position
        {
            get
            {
                return Position;
            }
            set
            {
                Position = value;
            }
        }

        public override Physics.IPhysicObject BodyA
        {
            set;
            get;
        }

        public override Physics.IPhysicObject BodyB
        {
            set;
            get;
        }


        public override PhysicConstraintTypes PhysicConstraintType
        {
            set;
            get;
        }
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
    }
}
