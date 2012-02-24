#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using StillDesign.PhysX;

namespace PloobsEngine.Physics
{
    public class PhysxPhysicObject : IPhysicObject
    {
        public Actor Actor
        {
            get;
            internal set;
        }

        public ActorDescription ActorDesc
        {
            get;
            internal set;
        }

        protected PhysxPhysicObject() { }
        public PhysxPhysicObject(ShapeDescription ShapeDescription, float density, Matrix worldTransformation, Vector3 scale, String name = null)
        {            
            Scale = scale;
            ActorDesc = new ActorDescription()
            {
                Name = name,
                BodyDescription = new BodyDescription(),
                Density = density,
                GlobalPose = worldTransformation.AsPhysX(),
                Shapes = { ShapeDescription },                                
            };                        
        }

        public override Vector3 Position
        {
            get
            {
                return Actor.GlobalPosition.AsXNA();
            }
            set
            {
                Actor.GlobalPosition = value.AsPhysX();
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
                return Actor.GlobalOrientation.AsXNA();
            }
            set
            {
                Actor.GlobalOrientation = value.AsPhysX();
            }
        }

        public override Vector3 FaceVector
        {
            get { return new Vector3(Actor.GlobalPose.M31, Actor.GlobalPose.M32, Actor.GlobalPose.M33); }
        }

        public override Matrix WorldMatrix
        {
            get { return Matrix.CreateScale(Scale) * Actor.GlobalPose.AsXNA(); }
        }

        public override Vector3 Velocity
        {
            get
            {
                return Actor.LinearVelocity.AsXNA();
            }
            set
            {
                Actor.LinearVelocity = value.AsPhysX();
            }
        }

        public override bool isMotionLess
        {
            get
            {
                return Actor.BodyFlags.Kinematic; 
            }
            set
            {
                Actor.RaiseBodyFlag(BodyFlag.Kinematic);
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
            Actor.AddForceAtLocalPosition(force.AsPhysX(), position.AsPhysX());
        }

        public override BoundingBox? BoundingBox
        {
            get {

                BoundingBox BoundingBox = new BoundingBox();
                foreach (var item in Actor.Shapes)
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
                return Actor.AngularVelocity.AsXNA();
            }
            set
            {
                this.Actor.AngularVelocity = value.AsPhysX();
            }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
#endif