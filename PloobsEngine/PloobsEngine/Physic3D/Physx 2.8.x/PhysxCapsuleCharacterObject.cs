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
    public class PhysxCapsuleCharacterObject : IPhysicObject
    {
        public CapsuleController Controller
        {
            get;
            internal set;
        }

        public CapsuleControllerDescription CapsuleControllerDescription
        {
            get;
            internal set;
        }

        protected PhysxCapsuleCharacterObject() { }
        Vector3 scale;
        public PhysxCapsuleCharacterObject(CapsuleControllerDescription CapsuleControllerDescription,Vector3 position, Matrix rotation,Vector3 scale)
        {
            this.scale = scale;
            this.CapsuleControllerDescription = CapsuleControllerDescription;
            this.CapsuleControllerDescription.Position = position.AsPhysX();
            CapsuleControllerDescription.Height *= scale.Y;
            CapsuleControllerDescription.Radius *= scale.X;
            this.rotation = rotation;
        }

        public void RotateYByAngleRadians(float angle)
        {
            rotation = Matrix.CreateRotationY(angle) * rotation;
        }

        public void RotateYByAngleDegrees(float angle)
        {
            rotation = Matrix.CreateRotationY(MathHelper.ToRadians(angle)) * rotation;
        }

        Matrix rotation;
        public override Vector3 Position
        {
            get
            {
                return Controller.FilteredPosition.AsXNA();
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
                return scale;
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
                return rotation;
            }
            set
            {
                this.rotation = value;
            }
        }

        public override Vector3 FaceVector
        {
            get { return rotation.Forward; }
        }

        public override Matrix WorldMatrix
        {
            get {
                return Matrix.CreateScale(Scale) * Rotation * Matrix.CreateTranslation(Controller.FilteredPosition.AsXNA());                
            }
        }

        public override Vector3 Velocity
        {
            get
            {
                return Controller.Actor.LinearVelocity.AsXNA();                
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
                return false;
            }
            set
            {
                if(value == false)
                    ActiveLogger.LogMessage("Character is never motion less", LogLevel.Warning);
            }
        }

        public override SceneControl.IObject ObjectOwner
        {
            get;
            set;
        }

        public override PhysicObjectTypes PhysicObjectTypes
        {
            get { return Physics.PhysicObjectTypes.CHARACTEROBJECT; }
        }

        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            throw new NotSupportedException();
        }

        public override BoundingBox? BoundingBox
        {
            get {
                BoundingBox BoundingBox = new BoundingBox();
                foreach (var item in Controller.Actor.Shapes)
                {
                    BoundingBox = BoundingBox.CreateMerged(BoundingBox, new BoundingBox(item.WorldSpaceBounds.Min.AsXNA(), item.WorldSpaceBounds.Max.AsXNA()));
                }

                return BoundingBox; 
            }
        }

        public override Vector3 AngularVelocity
        {
            get
            {
                return Controller.Actor.AngularVelocity.AsXNA();
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