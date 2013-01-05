#if WINDOWS && !MONO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulletSharp;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Physics
{
    public class BulletPhysicObject : IPhysicObject
    {
        private CollisionFilterGroups collisionFilterGroup;

        public CollisionFilterGroups CollisionFilterGroup
        {
            get { return collisionFilterGroup; }
            internal set { collisionFilterGroup = value; }
        }
         private CollisionFilterGroups collisionFilterMask;

         public CollisionFilterGroups CollisionFilterMask
         {
             get { return collisionFilterMask; }
             internal set { collisionFilterMask = value; }
         }
         

        RigidBody body;

        public RigidBody Body
        {
            get { return body; }
            protected set { body = value; }
        }
        CollisionShape shape;

        public CollisionShape Shape
        {
            get { return shape; }
            protected set { shape = value; }
        }
        CollisionObject obj;

        public CollisionObject Object
        {
            get { return obj; }
            protected set { obj = value; }
        }

        protected BulletPhysicObject() { }

        float mass;
        public BulletPhysicObject(CollisionShape CollisionShape, Vector3 translation, Matrix rotation, Vector3 Scale, float mass, CollisionFilterGroups CollisionFilterGroup = CollisionFilterGroups.DefaultFilter,CollisionFilterGroups collisionFilterMask = CollisionFilterGroups.DefaultFilter)
        {
            shape  = CollisionShape;
            this.scale = Scale;
            this.mass = mass;
            shape.LocalScaling = Scale;
            obj = LocalCreateRigidBody(mass, Matrix.CreateTranslation(translation) * rotation, CollisionShape);            

        }

        protected RigidBody LocalCreateRigidBody(float mass, Matrix startTransform, CollisionShape shape)
        {
            bool isDynamic = (mass != 0.0f);

            Vector3 localInertia = Vector3.Zero;
            if (isDynamic)
                shape.CalculateLocalInertia(mass, out localInertia);

            DefaultMotionState myMotionState = new DefaultMotionState(startTransform);

            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            body = new RigidBody(rbInfo);
            
            return body;
        }

        public override Vector3 Position
        {
            get
            {
                return body.CenterOfMassPosition;                
            }
            set
            {
                body.Translate(value);                
            }
        }

        Vector3 scale;
        public override Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }            
        }

        public override Matrix Rotation
        {
            get
            {
                return Matrix.CreateFromQuaternion(body.Orientation);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override Vector3 FaceVector
        {
            get { return WorldMatrix.Forward; }
        }

        public override Matrix WorldMatrix
        {
            get { return Matrix.CreateScale(scale) * body.WorldTransform; }
        }

        public override Vector3 Velocity
        {
            get
            {
                return body.LinearVelocity;
            }
            set
            {
                body.LinearVelocity = value;
            }
        }

        public override bool isMotionLess
        {
            get
            {
                return body.IsStaticOrKinematicObject;                
            }
            set
            {
                if (value == true)
                {
                    body.SetMassProps(mass, Vector3.Zero);
                }
                else
                {
                    Vector3 localInertia;
                    shape.CalculateLocalInertia(mass, out localInertia);
                    body.SetMassProps(mass, localInertia);
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
            body.ApplyForce(force, position);
        }

        public override BoundingBox? BoundingBox
        {
            get { return new BoundingBox(body.BroadphaseProxy.AabbMin, body.BroadphaseProxy.AabbMax); }
        }

        public override Vector3 AngularVelocity
        {
            get
            {
                return body.AngularVelocity;                
            }
            set
            {
                body.AngularVelocity = value;
            }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
#endif