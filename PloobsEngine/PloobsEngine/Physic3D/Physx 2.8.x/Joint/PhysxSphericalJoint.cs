#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxSphericalJoint : IPhysicConstraint
	{
        public SphericalJoint SphericalJoint
        {
            get;
            internal set;
        }

        public SphericalJointDescription SphericalJointDescription
        {
            get;
            internal set;
        }

        public PhysxSphericalJoint(SphericalJointDescription SphericalJointDescription)
        {
            this.SphericalJointDescription = SphericalJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif