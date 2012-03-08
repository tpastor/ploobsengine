#if WINDOWS
using  System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhysX;
using PhysX.Math;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Physics;


namespace PloobsEngine.Physics3x
{
    public class PhysxStaticActor : IPhysicObject
    {
        protected PhysX.Material material;
        RigidStatic staticActor;

        public RigidStatic StaticActor
        {
            get { return staticActor; }
        }
        public PhysxStaticActor(PhysxPhysicWorld PhysxPhysicWorld ,Geometry geometry, Microsoft.Xna.Framework.Matrix localTransformation, Microsoft.Xna.Framework.Matrix worldTransformation, Microsoft.Xna.Framework.Vector3 scale, MaterialDescription MaterialDescription)
        {            
            staticActor = PhysxPhysicWorld.Physix.CreateRigidStatic(worldTransformation.AsPhysX());
            material = PhysxPhysicWorld.Physix.CreateMaterial(MaterialDescription.StaticFriction, MaterialDescription.DynamicFriction, MaterialDescription.Bounciness);
            Shape aTriMeshShape = staticActor.CreateShape(geometry, material, localTransformation.AsPhysX());
        }

        public override Microsoft.Xna.Framework.Vector3 Position
        {
            get
            {
                return staticActor.GlobalPose.AsXNA().Translation;
            }
            set
            {
                PhysX.Math.Matrix m = staticActor.GlobalPose;
                m.M41 = value.X;
                m.M42 = value.Y;
                m.M43 = value.Z;
                staticActor.GlobalPose = m;
            }
        }

        public override Microsoft.Xna.Framework.Vector3 Scale
        {
            get;
            set;
        }

        public override Microsoft.Xna.Framework.Matrix Rotation
        {
            get
            {
                Microsoft.Xna.Framework.Matrix rot = staticActor.GlobalPose.AsXNA();
                rot.M41 = 0;
                rot.M42 = 0;
                rot.M43 = 0;
                rot.M44 = 1;
                return rot;
            }
            set
            {
                PhysX.Math.Matrix m = value.AsPhysX();
                m.M41 = staticActor.GlobalPose.M41;
                m.M42 = staticActor.GlobalPose.M42;
                m.M43 = staticActor.GlobalPose.M43;
                staticActor.GlobalPose = m;
            }
        }

        public override Microsoft.Xna.Framework.Vector3 FaceVector
        {
            get { return new Microsoft.Xna.Framework.Vector3(staticActor.GlobalPose.M31, staticActor.GlobalPose.M32, staticActor.GlobalPose.M33); }
        }

        public override Microsoft.Xna.Framework.Matrix WorldMatrix
        {
            get { return Microsoft.Xna.Framework.Matrix.CreateScale(Scale) * staticActor.GlobalPose.AsXNA(); }
        }

        public override Microsoft.Xna.Framework.Vector3 Velocity
        {
            get
            {
                ActiveLogger.LogMessage("Cant set/get velocity on Triangle Meshes", LogLevel.RecoverableError);
                return Microsoft.Xna.Framework.Vector3.Zero;
            }
            set
            {
                ActiveLogger.LogMessage("Cant set/get velocity on Triangle Meshes", LogLevel.RecoverableError);                
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
                ActiveLogger.LogMessage("Triangle Meshes are always MotionLess", LogLevel.Warning);                
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

        public override void ApplyImpulse(Microsoft.Xna.Framework.Vector3 position, Microsoft.Xna.Framework.Vector3 force)
        {
            ActiveLogger.LogMessage("Cant apply impulse on Triangle Meshes", LogLevel.RecoverableError);
        }

        public override Microsoft.Xna.Framework.BoundingBox? BoundingBox
        {
            get { return new Microsoft.Xna.Framework.BoundingBox(staticActor.WorldBounds.Min.AsXNA(), staticActor.WorldBounds.Max.AsXNA()); }
        }

        public override Microsoft.Xna.Framework.Vector3 AngularVelocity
        {
            get
            {
                ActiveLogger.LogMessage("Cant set/get velocity on Triangle Meshes", LogLevel.RecoverableError);
                return Microsoft.Xna.Framework.Vector3.Zero;
            }
            set
            {
                ActiveLogger.LogMessage("Cant set/get velocity on Triangle Meshes", LogLevel.RecoverableError);
            }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
#endif