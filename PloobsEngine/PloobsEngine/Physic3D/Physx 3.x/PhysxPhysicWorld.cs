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

using Microsoft.Xna.Framework;
using PloobsEngine.Physic.Constraints;
using System.Collections.Generic;
using PhysX;
using PhysX.VisualDebugger;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physics;
using System;

namespace PloobsEngine.Physics3x
{
    public class ErrorCallbackImp : ErrorCallback
    {
        public override void ReportError(ErrorCode errorCode, string message, string file, int lineNumber)
        {
            PloobsEngine.Engine.Logger.ActiveLogger.LogMessage(message, Engine.Logger.LogLevel.FatalError);
        }
    }

    public class PhysxPhysicWorld : PloobsEngine.Physics.IPhysicWorld
    {
        private List<IPhysicObject> objs;
        private List<IPhysicConstraint> ctns;
        PhysX.Physics physix;

        public PhysX.Physics Physix
        {
            get { return physix; }
        }
        Scene scene;

        public Scene Scene
        {
            get { return scene; }
        }

        Cooking cooking;
        
        public Cooking Cooking
        {
            get { return cooking; }
        }

        public PhysxPhysicWorld(Vector3 gravity, bool connectToRemoteDebugger = false)
        {
            physix = new PhysX.Physics(new ErrorCallbackImp(), true);            
            
            var sceneDesc = new SceneDesc()
            {
                Gravity = gravity.AsPhysX()                
            };
                        
            scene = physix.CreateScene(sceneDesc);
                                    
            this.scene.SetVisualizationParameter(VisualizationParameter.Scale, 1.0f);
            this.scene.SetVisualizationParameter(VisualizationParameter.CollisionShapes, true);
            this.scene.SetVisualizationParameter(VisualizationParameter.JointLocalFrames, true);
            this.scene.SetVisualizationParameter(VisualizationParameter.JointLimits, true);
            this.scene.SetVisualizationParameter(VisualizationParameter.ParticleSystemPosition, true);
            this.scene.SetVisualizationParameter(VisualizationParameter.ActorAxes, true);            

            // Connect to the remote debugger if it's there            
            if (connectToRemoteDebugger)
            {
                physix.ConnectToRemoteDebugger("localhost");
            }
            
            objs = new List<IPhysicObject>();
            ctns = new List<IPhysicConstraint>();

            cooking = physix.CreateCooking();

        }

        public override List<IPhysicObject> PhysicObjects
        {
            get { return objs; }
        }

        public override List<IPhysicConstraint> PhysicConstraints
        {
            get { return ctns; }
        }

        protected override void DebugDrawn(SceneControl.RenderHelper render, GameTime gt, Cameras.ICamera cam)
        {            
        }

        public override bool isDebugDraw
        {
            get
            {
                return base.isDebugDraw;
            }
            set
            {
                base.isDebugDraw = value;
                if (value == true)
                {
                    physix.ConnectToRemoteDebugger("localhost");
                }
            }
        }

        private bool _fetchedResults = true;
        protected override void Update(GameTime gt)
        {
            _fetchedResults = !this.scene.FetchResults();


            if (_fetchedResults)
                this.scene.Simulate((float)gt.ElapsedGameTime.TotalSeconds);

            //gScene->simulate(myTimestep);
            ////...perform useful work here using previous frame's state data        
            //while (!gScene->fetchResults())
            //{
            //    // do something useful        
            //}

        }

        public override void AddObject(IPhysicObject obj)
        {
            if (obj is PhysxPhysicObject)
            {
                PhysxPhysicObject PhysxPhysicObject = obj as PhysxPhysicObject;
                PhysxPhysicObject.RigidActor.UserData = obj;
                scene.AddActor(PhysxPhysicObject.RigidActor);
            }
            else if (obj is PhysxTriangleMesh)
            {
                PhysxTriangleMesh PhysxTriangleMesh = obj as PhysxTriangleMesh;
                PhysxTriangleMesh.StaticActor.UserData = obj;
                scene.AddActor(PhysxTriangleMesh.StaticActor);
            }
            else if (obj is PhysxStaticActor)
            {
                PhysxStaticActor PhysxTriangleMesh = obj as PhysxStaticActor;
                PhysxTriangleMesh.StaticActor.UserData = obj;
                scene.AddActor(PhysxTriangleMesh.StaticActor);
            }            
            objs.Add(obj);
        }

        public override void RemoveObject(IPhysicObject obj)
        {
            if (obj is PhysxPhysicObject)
            {
                PhysxPhysicObject PhysxPhysicObject = obj as PhysxPhysicObject;
                PhysxPhysicObject.RigidActor.UserData = null;
                scene.RemoveActor(PhysxPhysicObject.RigidActor);
            }
            else if (obj is PhysxTriangleMesh)
            {
                PhysxTriangleMesh PhysxTriangleMesh = obj as PhysxTriangleMesh;
                PhysxTriangleMesh.StaticActor.UserData = null;
                scene.RemoveActor(PhysxTriangleMesh.StaticActor);
            }
            else if (obj is PhysxStaticActor)
            {
                PhysxStaticActor PhysxTriangleMesh = obj as PhysxStaticActor;
                PhysxTriangleMesh.StaticActor.UserData = null;
                scene.RemoveActor(PhysxTriangleMesh.StaticActor);
            }
            
            objs.Remove(obj);
        }

        public override void AddConstraint(IPhysicConstraint ctn)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveConstraint(IPhysicConstraint ctn)
        {
            throw new System.NotImplementedException();
        }

        public override SegmentInterceptInfo SegmentIntersect(Ray raio, System.Func<IPhysicObject, bool> filter, float maxDistance)
        {

            SceneQueryFlags outputFlags = SceneQueryFlags.Distance | SceneQueryFlags.Impact | SceneQueryFlags.Impact;
            RaycastHit[] hit = scene.RaycastMultiple(raio.Position.AsPhysX(), raio.Direction.AsPhysX(), maxDistance, outputFlags,256);
            foreach (var item in hit)
            {                           
                if(item.Shape.Actor.UserData is IPhysicObject)
                {
                    if (filter(item.Shape.Actor.UserData as IPhysicObject))
                    {
                        SegmentInterceptInfo SegmentInterceptInfo = new SegmentInterceptInfo();
                        SegmentInterceptInfo.Distance = item.Distance;
                        SegmentInterceptInfo.ImpactNormal = item.Normal.AsXNA();
                        SegmentInterceptInfo.ImpactPosition = item.Impact.AsXNA();
                        SegmentInterceptInfo.PhysicObject = item.Shape.Actor.UserData as IPhysicObject;
                        return SegmentInterceptInfo;
                    }
                }
            }            
            return null;
        }

        public override void DetectCollisions(IPhysicObject po, List<IPhysicObject> resp)
        {            
            PhysxPhysicObject PhysxPhysicObject = po as PhysxPhysicObject;            
            
        }

        public override void GetPhysicsObjectsInRange(IPhysicObject po, float distance, CullerConditionAvaliator<IPhysicObject, SceneControl.IObject> CullerAvaliator, List<IPhysicObject> resp)
        {            
            SphereGeometry SphereGeometry = new PhysX.SphereGeometry(distance);
            foreach (var item in scene.OverlapMultiple(SphereGeometry, PhysX.Math.Matrix.Translation(po.Position.AsPhysX())))
            {
                if (item.UserData is IPhysicObject)
                {
                    IPhysicObject IPhysicObject =item.UserData as IPhysicObject;
                    if (CullerAvaliator(IPhysicObject, IPhysicObject.ObjectOwner))
                    {
                        resp.Add(IPhysicObject);
                    }
                }
            }
        }

        public override void GetPhysicsObjectsInRange(Vector3 position, float distance, CullerConditionAvaliator<IPhysicObject, SceneControl.IObject> CullerAvaliator, List<IPhysicObject> resp)
        {
            SphereGeometry SphereGeometry = new PhysX.SphereGeometry(distance);
            foreach (var item in scene.OverlapMultiple(SphereGeometry, PhysX.Math.Matrix.Translation(position.AsPhysX())))
            {
                if (item.UserData is IPhysicObject)
                {
                    IPhysicObject IPhysicObject = item.UserData as IPhysicObject;
                    if (CullerAvaliator(IPhysicObject, IPhysicObject.ObjectOwner))
                    {
                        resp.Add(IPhysicObject);
                    }
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