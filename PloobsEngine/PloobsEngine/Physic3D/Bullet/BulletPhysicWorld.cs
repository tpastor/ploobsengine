#if WINDOWS
#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using BulletSharp;
using Microsoft.Xna.Framework;
using PloobsEngine.Physic.Constraints;
using System.Collections.Generic;
namespace PloobsEngine.Physics
{
    class contactcallbac : CollisionWorld.ContactResultCallback 
    {
        public List<IPhysicObject> objs = new List<IPhysicObject>();
        public override float AddSingleResult(ManifoldPoint cp, CollisionObject colObj0, int partId0, int index0, CollisionObject colObj1, int partId1, int index1)
        {
            if (colObj0.UserObject != null && colObj0.UserObject is IPhysicObject)
            {
                objs.Add(colObj0.UserObject as IPhysicObject);
            }
            return 0;
        }
    }

    /// <summary>
    /// Bepu Implementation of a Physic World
    /// </summary>
    public class BulletPhysicWorld : IPhysicWorld
    {
        DynamicsWorld world;

        public DynamicsWorld World
        {
            get { return world; }            
        }

        CollisionDispatcher Dispatcher;
        BroadphaseInterface Broadphase;                
        CollisionConfiguration collisionConf;
        ConstraintSolver constraintSolver;

        private List<IPhysicObject> objs;
        private List<IPhysicConstraint> ctns;

        public BulletPhysicWorld(DynamicsWorld CollisionWorld)
        {            
            objs = new List<IPhysicObject>();
            ctns = new List<IPhysicConstraint>();
            world = CollisionWorld;            
        }

        public BulletPhysicWorld(Vector3 gravity,Dispatcher dispatcher, BroadphaseInterface pairCache, ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
        {
            objs = new List<IPhysicObject>();
            ctns = new List<IPhysicConstraint>();

            world = new DiscreteDynamicsWorld(dispatcher, pairCache, constraintSolver, collisionConfiguration);
            world.Gravity = gravity;
        }

        public BulletPhysicWorld(Vector3 gravity)
        {
            collisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(collisionConf);
            Broadphase = new DbvtBroadphase();
            constraintSolver = new SequentialImpulseConstraintSolver();

            objs = new List<IPhysicObject>();
            ctns = new List<IPhysicConstraint>();

            world = new DiscreteDynamicsWorld(Dispatcher, Broadphase ,constraintSolver, collisionConf);
            world.Gravity = gravity;            
        }


        public override System.Collections.Generic.List<IPhysicObject> PhysicObjects
        {
            get { return objs; }
        }

        public override System.Collections.Generic.List<Physic.Constraints.IPhysicConstraint> PhysicConstraints
        {
            get { return ctns; }
        }

        protected override void DebugDrawn(SceneControl.RenderHelper render, GameTime gt, Cameras.ICamera cam)
        {
            throw new System.NotImplementedException();
        }

        protected override void Update(GameTime gt)
        {
            world.StepSimulation((float)gt.ElapsedGameTime.TotalSeconds);
        }

        public override void AddObject(IPhysicObject obj)
        {
            if (obj is BulletPhysicObject && obj.PhysicObjectTypes != PhysicObjectTypes.GHOST)
            {
                BulletPhysicObject BulletPhysicObject = obj as BulletPhysicObject;
                world.AddRigidBody(BulletPhysicObject.Body, BulletPhysicObject.CollisionFilterGroup, BulletPhysicObject.CollisionFilterMask);
                BulletPhysicObject.Body.UserObject = obj;                                                
            }
            objs.Add(obj);
        }

        public override void RemoveObject(IPhysicObject obj)
        {
            if (obj is BulletPhysicObject && obj.PhysicObjectTypes != PhysicObjectTypes.GHOST)
            {
                BulletPhysicObject BulletPhysicObject = obj as BulletPhysicObject;
                world.RemoveRigidBody(BulletPhysicObject.Body);
                BulletPhysicObject.Body.UserObject = null;                
            }
            objs.Remove(obj);
        }

        public override void AddConstraint(Physic.Constraints.IPhysicConstraint ctn)
        {
            if (ctn is BulletConstraint)
            {
                BulletConstraint BulletConstraint = ctn as BulletConstraint;
                world.AddConstraint(BulletConstraint.Constraint,BulletConstraint.DisableCollisionsBetweenLinkedBodies);
            }
            ctns.Add(ctn);
        }

        public override void RemoveConstraint(Physic.Constraints.IPhysicConstraint ctn)
        {
            if (ctn is BulletConstraint)
            {
                BulletConstraint BulletConstraint = ctn as BulletConstraint;
                world.RemoveConstraint(BulletConstraint.Constraint);                
            }
            ctns.Remove(ctn);
        }

        public override SegmentInterceptInfo SegmentIntersect(Ray raio, System.Func<IPhysicObject, bool> filter, float maxDistance)
        {
            BulletSharp.CollisionWorld.ClosestRayResultCallback RayResultCallback = new BulletSharp.CollisionWorld.ClosestRayResultCallback(raio.Position, raio.Direction * maxDistance);
            world.RayTest(raio.Position, raio.Direction * maxDistance, RayResultCallback);
            if (RayResultCallback.HasHit)
            {
                SegmentInterceptInfo SegmentInterceptInfo = new SegmentInterceptInfo();
                SegmentInterceptInfo.Distance = RayResultCallback.ClosestHitFraction * maxDistance;
                SegmentInterceptInfo.PhysicObject = RayResultCallback.CollisionObject.UserObject as IPhysicObject;
                SegmentInterceptInfo.ImpactNormal = RayResultCallback.HitNormalWorld;
                SegmentInterceptInfo.ImpactPosition = RayResultCallback.HitPointWorld;
                return SegmentInterceptInfo;
            }
            else
            {
                return null;
            }
        }

        contactcallbac contactcallbac = new contactcallbac();
        public override void DetectCollisions(IPhysicObject po, System.Collections.Generic.List<IPhysicObject> resp)
        { 
            BulletPhysicObject  BulletPhysicObject = po as BulletPhysicObject;
            contactcallbac.objs = new List<IPhysicObject>();
            world.ContactTest(BulletPhysicObject.Body, contactcallbac);
            resp = contactcallbac.objs;            
        }

        SphereShape SphereShape = new BulletSharp.SphereShape(1);
        CollisionObject CollisionObject = new BulletSharp.CollisionObject();
        public override void GetPhysicsObjectsInRange(IPhysicObject po, float distance, CullerConditionAvaliator<IPhysicObject, SceneControl.IObject> CullerAvaliator, System.Collections.Generic.List<IPhysicObject> resp)
        {
            SphereShape.SetUnscaledRadius(distance);
            CollisionObject.WorldTransform = Matrix.CreateTranslation(po.Position);
            CollisionObject.CollisionShape = SphereShape;
            contactcallbac.objs = new List<IPhysicObject>();
            world.ContactTest(CollisionObject, contactcallbac);
            foreach (var item in contactcallbac.objs)
            {
                if (CullerAvaliator(item, item.ObjectOwner) == true)
                {
                    resp.Add(item);
                }
            }
        }

        public override void GetPhysicsObjectsInRange(Vector3 position, float distance, CullerConditionAvaliator<IPhysicObject, SceneControl.IObject> CullerAvaliator, System.Collections.Generic.List<IPhysicObject> resp)
        {
            SphereShape.SetUnscaledRadius(distance);
            CollisionObject.WorldTransform = Matrix.CreateTranslation(position);
            CollisionObject.CollisionShape = SphereShape;
            contactcallbac.objs = new List<IPhysicObject>();
            world.ContactTest(CollisionObject, contactcallbac);
            foreach (var item in contactcallbac.objs)
            {
                if (CullerAvaliator(item, item.ObjectOwner) == true)
                {
                    resp.Add(item);
                }
            }
             
        }
               

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif