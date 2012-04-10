#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxPointInPlaneJoint : IPhysicConstraint
	{
        public PointInPlaneJoint PointInPlaneJoint
        {
            get;
            internal set;
        }

        public PointInPlaneJointDescription PointInPlaneJointDescription
        {
            get;
            internal set;
        }

        public PhysxPointInPlaneJoint(PointInPlaneJointDescription PointInPlaneJointDescription)
        {
            this.PointInPlaneJointDescription = PointInPlaneJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif