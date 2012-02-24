#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PhysX;
using PloobsEngine.Physics;

namespace PloobsEngine.Physics3x
{
    public class PhysxPhysicObject : IPhysicObject
    {
        protected RigidDynamic rigidActor;

        public RigidDynamic RigidActor
        {
            get { return rigidActor; }            
        }

        protected PhysX.Material material;

        protected PhysxPhysicObject() { }
        public PhysxPhysicObject(Scene scene, Geometry geometry, float mass, Matrix localTransformation, Matrix worldTransformation, Vector3 scale, MaterialDescription MaterialDescription)
        {            
            Scale = scale;
            rigidActor = scene.Physics.CreateRigidDynamic(worldTransformation.AsPhysX());            
            material = scene.Physics.CreateMaterial(MaterialDescription.StaticFriction, MaterialDescription.DynamicFriction, MaterialDescription.Bounciness);            
            var boxShape = rigidActor.CreateShape(geometry, material, localTransformation.AsPhysX());                                                
            rigidActor.SetMassAndUpdateInertia(mass);                        
        }

        public override Vector3 Position
        {
            get
            {
                return rigidActor.GlobalPose.AsXNA().Translation;
            }
            set
            {
                PhysX.Math.Matrix m = rigidActor.GlobalPose;
                m.M41 = value.X;
                m.M42 = value.Y;
                m.M43 = value.Z;
                rigidActor.GlobalPose = m;
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
                Matrix rot = rigidActor.GlobalPose.AsXNA();
                rot.M41 = 0;
                rot.M42 = 0;
                rot.M43 = 0;
                rot.M44 = 1;
                return rot;
            }
            set
            {
                PhysX.Math.Matrix m = value.AsPhysX();
                m.M41 = rigidActor.GlobalPose.M41;
                m.M42 = rigidActor.GlobalPose.M42;
                m.M43 = rigidActor.GlobalPose.M43;
                rigidActor.GlobalPose = m;
            }
        }

        public override Vector3 FaceVector
        {
            get { return new Vector3( rigidActor.GlobalPose.M31,rigidActor.GlobalPose.M32,rigidActor.GlobalPose.M33); }
        }

        public override Matrix WorldMatrix
        {
            get { return Matrix.CreateScale(Scale) * rigidActor.GlobalPose.AsXNA(); }
        }

        public override Vector3 Velocity
        {
            get
            {
                return rigidActor.LinearVelocity.AsXNA();
            }
            set
            {
                rigidActor.LinearVelocity = value.AsPhysX();
            }
        }

        public override bool isMotionLess
        {
            get
            {
                return (rigidActor.Flags & RigidDynamicFlags.Kinematic) == RigidDynamicFlags.Kinematic;
            }
            set
            {
                if (value == true)
                {
                    rigidActor.Flags |= RigidDynamicFlags.Kinematic;
                }
                else
                {
                    rigidActor.Flags &= ~RigidDynamicFlags.Kinematic;
                }
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
            rigidActor.AddForceAtLocalPosition(force.AsPhysX(),position.AsPhysX());
        }

        public override BoundingBox? BoundingBox
        {
            get { return new BoundingBox(rigidActor.WorldBounds.Min.AsXNA(), rigidActor.WorldBounds.Max.AsXNA()); }
        }

        public override Vector3 AngularVelocity
        {
            get
            {
                return rigidActor.AngularVelocity.AsXNA();
            }
            set
            {
                this.rigidActor.AngularVelocity = value.AsPhysX();
            }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
#endif