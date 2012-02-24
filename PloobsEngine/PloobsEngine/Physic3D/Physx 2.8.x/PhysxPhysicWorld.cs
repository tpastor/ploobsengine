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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physics;
using StillDesign.PhysX;
using System;

namespace PloobsEngine.Physics
{    
    public class PhysxPhysicWorld : PloobsEngine.Physics.IPhysicWorld
    {
        private List<IPhysicObject> objs;
        private List<IPhysicConstraint> ctns;
    
        public Scene Scene
        {
            get;
            private set;
        }

        public Core Core
        {
            get;
            private set;
        }

        BasicEffect BasicEffect;
        public override bool isDebugDraw
        {
            get
            {
                return base.isDebugDraw;
            }
            set
            {
                base.isDebugDraw = value;
                if (isDebugDraw == false)
                {
                    if (BasicEffect != null && !BasicEffect.IsDisposed)
                    {
                        BasicEffect.Dispose();
                    }                    
                }
            }
        }


        public PhysxPhysicWorld(CoreDescription CoreDescription ,SceneDescription SceneDescription, bool connectToRemoteDebugger = false)
        {
            UserOutput output = new UserOutput();
            this.Core = new Core(CoreDescription, output);

            Core core = this.Core;
            core.SetParameter(PhysicsParameter.VisualizationScale, 2.0f);
            core.SetParameter(PhysicsParameter.VisualizeCollisionShapes, true);
            core.SetParameter(PhysicsParameter.VisualizeClothMesh, true);
            core.SetParameter(PhysicsParameter.VisualizeJointLocalAxes, true);
            core.SetParameter(PhysicsParameter.VisualizeJointLimits, true);
            core.SetParameter(PhysicsParameter.VisualizeFluidPosition, true);
            core.SetParameter(PhysicsParameter.VisualizeFluidEmitters, false); // Slows down rendering a bit too much
            core.SetParameter(PhysicsParameter.VisualizeForceFields, true);
            core.SetParameter(PhysicsParameter.VisualizeSoftBodyMesh, true);

            this.Scene = core.CreateScene(SceneDescription);

            // Connect to the remote debugger if it's there
            if (connectToRemoteDebugger)
                core.Foundation.RemoteDebugger.Connect("localhost");

            objs = new List<IPhysicObject>();
            ctns = new List<IPhysicConstraint>();

        }
        public PhysxPhysicWorld(Vector3 gravity, bool connectToRemoteDebugger = false)
        {

            CoreDescription coreDesc = new CoreDescription();
            UserOutput output = new UserOutput();

            this.Core = new Core(coreDesc, output);

            Core core = this.Core;
            core.SetParameter(PhysicsParameter.VisualizationScale, 2.0f);
            core.SetParameter(PhysicsParameter.VisualizeCollisionShapes, true);
            core.SetParameter(PhysicsParameter.VisualizeClothMesh, true);
            core.SetParameter(PhysicsParameter.VisualizeJointLocalAxes, true);
            core.SetParameter(PhysicsParameter.VisualizeJointLimits, true);
            core.SetParameter(PhysicsParameter.VisualizeFluidPosition, true);
            core.SetParameter(PhysicsParameter.VisualizeFluidEmitters, false); // Slows down rendering a bit too much
            core.SetParameter(PhysicsParameter.VisualizeForceFields, true);
            core.SetParameter(PhysicsParameter.VisualizeSoftBodyMesh, true);

            SceneDescription sceneDesc = new SceneDescription()
            {
                Gravity = gravity.AsPhysX(),                                
            };

            this.Scene = core.CreateScene(sceneDesc);

            // Connect to the remote debugger if it's there
            if(connectToRemoteDebugger)
                core.Foundation.RemoteDebugger.Connect("localhost");
            
            objs = new List<IPhysicObject>();
            ctns = new List<IPhysicConstraint>();

            
        }

        public override List<IPhysicObject> PhysicObjects
        {
            get { return objs; }
        }

        public override List<IPhysicConstraint> PhysicConstraints
        {
            get { return ctns; }
        }

        VertexPositionColor[] VertexPositionColor1;
        VertexPositionColor[] VertexPositionColor2;
        protected override void DebugDrawn(SceneControl.RenderHelper render, GameTime gt, Cameras.ICamera cam)
        {
            if (BasicEffect == null)
            {
                BasicEffect = new BasicEffect(render.device);
                BasicEffect.VertexColorEnabled = true;
                BasicEffect.TextureEnabled = false;
            }

            if (_fetchedResults == false)
            {
                DebugRenderable RenderBuffer = Scene.GetDebugRenderable();
                if (RenderBuffer == null || (RenderBuffer.TriangleCount == 0 && RenderBuffer.LineCount == 0))
                    return;                

                Color c = Color.Red;
                if (RenderBuffer.TriangleCount > 0)
                {
                    VertexPositionColor1 = new VertexPositionColor[RenderBuffer.TriangleCount * 3];
                    for (int i = 0, j = 0; i < RenderBuffer.TriangleCount; i += 3, j++)
                    {
                        VertexPositionColor1[i].Color = c;
                        VertexPositionColor1[i].Position = RenderBuffer.GetDebugTriangles()[j].Point0.AsXNA();

                        VertexPositionColor1[i + 1].Color = c;
                        VertexPositionColor1[i + 1].Position = RenderBuffer.GetDebugTriangles()[j].Point1.AsXNA();

                        VertexPositionColor1[i + 2].Color = c;
                        VertexPositionColor1[i + 2].Position = RenderBuffer.GetDebugTriangles()[j].Point2.AsXNA();
                    }


                    
                }

                if (RenderBuffer.LineCount > 0)
                {
                    VertexPositionColor2 = new VertexPositionColor[RenderBuffer.LineCount * 2];
                    for (int i = 0, j = 0; i < RenderBuffer.LineCount; i += 2, j++)
                    {
                        VertexPositionColor2[i].Color = c;
                        VertexPositionColor2[i].Position = RenderBuffer.GetDebugLines()[j].Point0.AsXNA();

                        VertexPositionColor2[i + 1].Color = c;
                        VertexPositionColor2[i + 1].Position = RenderBuffer.GetDebugLines()[j].Point1.AsXNA();
                    }
             
                }
            }

            BasicEffect.View = cam.View;
            BasicEffect.Projection = cam.Projection;
            BasicEffect.World = Matrix.Identity;

            if(VertexPositionColor2 != null)
                render.RenderUserPrimitive<VertexPositionColor>(BasicEffect, PrimitiveType.LineList, VertexPositionColor2, 0, VertexPositionColor2.Length / 2);

            if (VertexPositionColor1 != null)
            render.RenderUserPrimitive<VertexPositionColor>(BasicEffect, PrimitiveType.TriangleList, VertexPositionColor1, 0, VertexPositionColor1.Length / 3);

        }


        private bool _fetchedResults = true;
        protected override void Update(GameTime gt)
        {
            _fetchedResults = !this.Scene.FetchResults(SimulationStatus.RigidBodyFinished,false);
            
            if (_fetchedResults)
                this.Scene.Simulate((float)gt.ElapsedGameTime.TotalSeconds);
        }

        public override void AddObject(IPhysicObject obj)
        {
            if (obj is PhysxPhysicObject)
            {
                PhysxPhysicObject PhysxPhysicObject = obj as PhysxPhysicObject;
                PhysxPhysicObject.Actor = Scene.CreateActor(PhysxPhysicObject.ActorDesc);
                PhysxPhysicObject.Actor.UserData = obj;
            }            
            
            objs.Add(obj);
        }

        public override void RemoveObject(IPhysicObject obj)
        {
            if (obj is PhysxPhysicObject)
            {
                PhysxPhysicObject PhysxPhysicObject = obj as PhysxPhysicObject;
                PhysxPhysicObject.Actor.UserData = null;
                PhysxPhysicObject.Actor.Dispose();
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

        private class InternalUserRaycastReport : UserRaycastReport
        {
            public System.Func<IPhysicObject, bool> filter;
            public SegmentInterceptInfo SegmentInterceptInfo;
            public override bool OnHit(RaycastHit hits)
            {
                if (hits.Shape.Actor.UserData is IPhysicObject)
                {
                    if (filter(hits.Shape.Actor.UserData as IPhysicObject))
                    {
                        SegmentInterceptInfo = new SegmentInterceptInfo();
                        SegmentInterceptInfo.Distance = hits.Distance;
                        SegmentInterceptInfo.ImpactNormal = hits.WorldNormal.AsXNA();
                        SegmentInterceptInfo.ImpactPosition = hits.WorldImpact.AsXNA();
                        SegmentInterceptInfo.PhysicObject = hits.Shape.Actor.UserData as IPhysicObject;                        
                        return false;
                    }
                }
                return true;
            }
        }

        InternalUserRaycastReport internalUserRaycastReport = new InternalUserRaycastReport();
        public override SegmentInterceptInfo SegmentIntersect(Microsoft.Xna.Framework.Ray raio, System.Func<IPhysicObject, bool> filter, float maxDistance)
        {
            internalUserRaycastReport.filter = filter;
            internalUserRaycastReport.SegmentInterceptInfo = null;
            Scene.RaycastAllShapes(new StillDesign.PhysX.Ray(raio.Position.AsPhysX(), raio.Direction.AsPhysX()),internalUserRaycastReport, ShapesType.All,0 ,maxDistance,RaycastBit.Distance | RaycastBit.Normal | RaycastBit.Shape | RaycastBit.Impact,null);
            return internalUserRaycastReport.SegmentInterceptInfo;
        }

        public override void DetectCollisions(IPhysicObject po, List<IPhysicObject> resp)
        {
            throw new Exception("not implemented, attach the callback to the Right Actor");            
        }

        public override void GetPhysicsObjectsInRange(IPhysicObject po, float distance, CullerConditionAvaliator<IPhysicObject, SceneControl.IObject> CullerAvaliator, List<IPhysicObject> resp)
        {                
            foreach (var item in  Scene.OverlappedShapes(new Sphere(distance,po.Position.AsPhysX()),ShapesType.All))
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
            foreach (var item in Scene.OverlappedShapes(new Sphere(distance, position.AsPhysX()), ShapesType.All))
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