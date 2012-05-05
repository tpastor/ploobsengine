#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physic.Constraints;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{

    public class PhysxPrismaticJoint : IPhysicConstraint
    {
        public PrismaticJoint PrismaticJoint
        {
            get;
            internal set;
        }

        public PrismaticJointDescription PrismaticJointDescription
        {
            get;
            internal set;
        }

        public PhysxPrismaticJoint(PrismaticJointDescription PrismaticJointDescription)
        {
            this.PrismaticJointDescription = PrismaticJointDescription;
        }

        public override PhysicConstraintType PhysicConstraintType
        {
            get { return Physic.Constraints.PhysicConstraintType.JOINT; }
        }
    }
}
#endif