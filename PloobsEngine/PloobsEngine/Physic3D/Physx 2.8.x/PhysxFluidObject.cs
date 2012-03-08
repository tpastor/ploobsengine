#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using StillDesign.PhysX;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Physics
{
    public class PhysxFluidObject : IPhysicObject
    {
        public Fluid Fluid
        {
            get;
            internal set;
        }

        public FluidDescription FluidDesc
        {
            get;
            internal set;
        }

        protected PhysxFluidObject() { }
        public PhysxFluidObject(FluidDescription FluidDescription)
        {
            this.FluidDesc = FluidDescription;                    
        }

        public override Vector3 Position
        {
            get
            {
                return Fluid.GetWorldBounds().Center.AsXNA();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override Vector3 Scale
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override Matrix Rotation
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override Vector3 FaceVector
        {
            get { throw new NotSupportedException(); }
        }

        public override Matrix WorldMatrix
        {
            get { return Matrix.Identity; }
        }

        public override Vector3 Velocity
        {
            get
            {                
                return Vector3.Zero;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override bool isMotionLess
        {
            get
            {
                return true;
            }
            set
            {
                if(value == false)
                    ActiveLogger.LogMessage("Cloth is always motion less", LogLevel.Warning);
            }
        }

        public override SceneControl.IObject ObjectOwner
        {
            get;
            set;
        }

        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return Physics.PhysicObjectTypes.FLUID; }
        }

        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            throw new NotSupportedException();
        }

        public override BoundingBox? BoundingBox
        {
            get {
                return new BoundingBox(Fluid.GetWorldBounds().Min.AsXNA(), Fluid.GetWorldBounds().Max.AsXNA());                 
            }
        }

        public override Vector3 AngularVelocity
        {
            get
            {
                return Vector3.Zero;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
#endif