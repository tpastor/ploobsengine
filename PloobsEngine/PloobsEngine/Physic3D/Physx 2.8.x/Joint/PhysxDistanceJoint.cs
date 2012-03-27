#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxDistanceJoint : IPhysicConstraint
	{
        public DistanceJoint DistanceJoint
        {
            get;
            internal set;
        }

        public DistanceJointDescription DistanceJointDescription
        {
            get;
            internal set;
        }

        public PhysxDistanceJoint(DistanceJointDescription DistanceJointDescription)
        {
            this.DistanceJointDescription = DistanceJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif