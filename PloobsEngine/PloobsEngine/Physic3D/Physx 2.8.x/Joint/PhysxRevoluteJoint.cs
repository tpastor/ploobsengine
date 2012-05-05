#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxRevoluteJoint : IPhysicConstraint
	{
        public RevoluteJoint RevoluteJoint
        {
            get;
            internal set;
        }

        public RevoluteJointDescription RevoluteJointDescription
        {
            get;
            internal set;
        }

        public PhysxRevoluteJoint(RevoluteJointDescription RevoluteJointDescription)
        {
            this.RevoluteJointDescription = RevoluteJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif