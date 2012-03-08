#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxD6Joint : IPhysicConstraint
	{
        public D6Joint D6Joint
        {
            get;
            internal set;
        }

        public D6JointDescription D6JointDescription
        {
            get;
            internal set;
        }

        public PhysxD6Joint(D6JointDescription D6JointDescription)
        {
            this.D6JointDescription = D6JointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif