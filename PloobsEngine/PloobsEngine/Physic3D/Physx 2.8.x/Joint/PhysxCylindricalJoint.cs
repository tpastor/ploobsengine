#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxCylindricalJoint : IPhysicConstraint
	{
        public CylindricalJoint CylindricalJoint
        {
            get;
            internal set;
        }

        public CylindricalJointDescription CylindricalJointDescription
        {
            get;
            internal set;
        }

        public PhysxCylindricalJoint(CylindricalJointDescription CylindricalJointDescription)
        {
            this.CylindricalJointDescription = CylindricalJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif