#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxPointOnLineJoint : IPhysicConstraint
	{
        public PointOnLineJoint PointOnLineJoint
        {
            get;
            internal set;
        }

        public PointOnLineJointDescription PointOnLineJointDescription
        {
            get;
            internal set;
        }

        public PhysxPointOnLineJoint(PointOnLineJointDescription PointOnLineJointDescription)
        {
            this.PointOnLineJointDescription = PointOnLineJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif