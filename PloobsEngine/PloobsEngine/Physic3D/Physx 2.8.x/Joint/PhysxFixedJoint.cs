#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxFixedJoint : IPhysicConstraint
	{
        public FixedJoint FixedJoint
        {
            get;
            internal set;
        }

        public FixedJointDescription FixedJointDescription
        {
            get;
            internal set;
        }

        public PhysxFixedJoint(FixedJointDescription FixedJointDescription)
        {
            this.FixedJointDescription = FixedJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif