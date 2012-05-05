#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxPulleyJoint : IPhysicConstraint
	{
        public PulleyJoint PulleyJoint
        {
            get;
            internal set;
        }

        public PulleyJointDescription PulleyJointDescription
        {
            get;
            internal set;
        }

        public PhysxPulleyJoint(PulleyJointDescription PulleyJointDescription)
        {
            this.PulleyJointDescription = PulleyJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
	}
}
#endif