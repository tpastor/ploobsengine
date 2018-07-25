#if WINDOWS && !MONO && !MONODX
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using BulletSharp;

namespace PloobsEngine.Physics
{    
    public class BulletConstraint : IPhysicConstraint
    {
        public BulletConstraint(bool DisableCollisionsBetweenLinkedBodies)
        {
            this.DisableCollisionsBetweenLinkedBodies = DisableCollisionsBetweenLinkedBodies;
        }

        protected TypedConstraint constraint;
        public TypedConstraint Constraint
        {
            get { return constraint; }
            set { constraint = value; }
        }

        public bool DisableCollisionsBetweenLinkedBodies
        {
            get;
            internal set;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.OTHER; }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {            
        }
    }
}
#endif