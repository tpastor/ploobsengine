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
    public class PhysxClothObject : IPhysicObject
    {
        public Cloth Cloth
        {
            get;
            internal set;
        }

        public ClothDescription ClothDesc
        {
            get;
            internal set;
        }

        protected PhysxClothObject() { }
        public PhysxClothObject(ClothDescription ClothDesc,Matrix world)
        {
            Scale = Vector3.One;
            this.ClothDesc = ClothDesc;
            this.ClothDesc.GlobalPose = world.AsPhysX();            
        }

        public override Vector3 Position
        {
            get
            {
                return new Vector3(ClothDesc.GlobalPose.M41, ClothDesc.GlobalPose.M42, ClothDesc.GlobalPose.M43);
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override Vector3 Scale
        {
            get;
            set;
        }

        public override Matrix Rotation
        {
            get
            {   
                Microsoft.Xna.Framework.Matrix rot = ClothDesc.GlobalPose.AsXNA();
                rot.M41 = 0;
                rot.M42 = 0;
                rot.M43 = 0;                
                return rot;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override Vector3 FaceVector
        {
            get { return new Vector3(ClothDesc.GlobalPose.M31, ClothDesc.GlobalPose.M32, ClothDesc.GlobalPose.M33); }
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
            get { return Physics.PhysicObjectTypes.OTHER; }
        }

        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            throw new NotSupportedException();
        }

        public override BoundingBox? BoundingBox
        {
            get {                                
                return new BoundingBox(Cloth.WorldBounds.Min.AsXNA(), Cloth.WorldBounds.Max.AsXNA());                 
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