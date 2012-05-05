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
using PloobsEngine.Trigger;
using StillDesign.PhysX.Utilities;

namespace PloobsEngine.Physics
{    
    public class PhysxPhysicWorld : PloobsEngine.Physics.IPhysicWorld
    {
        private TriggerReport TriggerReport;
        private List<IPhysicObject> objs;
        private List<IPhysicConstraint> ctns;
        public ControllerManager ControllerManager
        {
            set;
            get;
        }
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

        bool safeAndSlowUpdate = true;
        public StillDesign.PhysX.Material CreatePhysicMaterial(StillDesign.PhysX.MaterialDescription MaterialDescription, bool safeAndSlowUpdate = true)
        {
            this.safeAndSlowUpdate = safeAndSlowUpdate;
            return Scene.CreateMaterial(MaterialDescription);
        }

        public PhysxPhysicWorld(CoreDescription CoreDescription, SceneDescription SceneDescription, bool connectToRemoteDebugger = false, bool safeAndSlowUpdate = true)
        {
            this.safeAndSlowUpdate = safeAndSlowUpdate;
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

            ControllerManager = Scene.CreateControllerManager();
            objs = new List<IPhysicObject>();
            ctns = new List<IPhysicConstraint>();
            TriggerReport = new Physics.TriggerReport();
            Scene.UserTriggerReport = TriggerReport;
            Utilities.ErrorReport = errorReport;
        }


        public PhysxPhysicWorld(Vector3 gravity, bool connectToRemoteDebugger = false, bool safeAndSlowUpdate = true)
        {
            this.safeAndSlowUpdate = safeAndSlowUpdate;
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
                Gravity = gravity.AsPhysX()     ,                
            };

            this.Scene = core.CreateScene(sceneDesc);

            // Connect to the remote debugger if it's there
            if(connectToRemoteDebugger)
                core.Foundation.RemoteDebugger.Connect("localhost");

            ControllerManager = Scene.CreateControllerManager();
            objs = new List<IPhysicObject>();
            ctns = new List<IPhysicConstraint>();

            TriggerReport = new Physics.TriggerReport();
            Scene.UserTriggerReport = TriggerReport;
            Utilities.ErrorReport = errorReport;
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

            //if (_fetchedResults == false)
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

        
        bool simulated = false;
        bool ended = true;
        protected override void Update(GameTime gt)
        {
            if (safeAndSlowUpdate)
            {
                this.Scene.Simulate((float)gt.ElapsedGameTime.TotalSeconds);
                this.Scene.FlushStream();
                while (!this.Scene.FetchResults(SimulationStatus.RigidBodyFinished, false)) ;
            }
            else
            {
                if (simulated)
                {
                    this.Scene.FlushStream();
                    this.Scene.FlushCaches();
                    while (!this.Scene.FetchResults(SimulationStatus.RigidBodyFinished, false)) ;
                    ended = true;
                    simulated = false;
                }

                if (ended == true)
                {

                    this.Scene.Simulate((float)gt.ElapsedGameTime.TotalSeconds);

                    simulated = true;
                }
            }                

            
        }

        public override void AddObject(IPhysicObject obj)
        {           
            if (obj is PhysxPhysicObject)
            {
                PhysxPhysicObject PhysxPhysicObject = obj as PhysxPhysicObject;
                if (PhysxPhysicObject.Actor == null)
                {
                    PhysxPhysicObject.Actor = Scene.CreateActor(PhysxPhysicObject.ActorDesc, int.MaxValue);
                    PhysxPhysicObject.Actor.UserData = obj;

                    for (int i = 0; i < PhysxPhysicObject.ActorDesc.Shapes.Count; i++)
                    {
                        PhysxPhysicObject.Actor.Shapes[i].UserData = PhysxPhysicObject.ActorDesc.Shapes[i].UserData;
                    }
                }

            }

            else if (obj is PhysxClothObject)
            {
                PhysxClothObject PhysxPhysicObject = obj as PhysxClothObject;
                PhysxPhysicObject.Cloth = Scene.CreateCloth(PhysxPhysicObject.ClothDesc);
                PhysxPhysicObject.Cloth.UserData = obj;
            }

            else if (obj is PhysxFluidObject)
            {
                PhysxFluidObject PhysxPhysicObject = obj as PhysxFluidObject;
                PhysxPhysicObject.Fluid = Scene.CreateFluid(PhysxPhysicObject.FluidDesc);
                PhysxPhysicObject.Fluid.UserData = obj;
            }

            else if (obj is PhysxCapsuleCharacterObject)
            {
                PhysxCapsuleCharacterObject PhysxPhysicObject = obj as PhysxCapsuleCharacterObject;
                PhysxPhysicObject.Controller = ControllerManager.CreateController<CapsuleController>(PhysxPhysicObject.CapsuleControllerDescription);
                PhysxPhysicObject.Controller.UserData = obj;
            }      

            else if (obj is PhysxBoxCharacterObject)
            {
                PhysxBoxCharacterObject PhysxPhysicObject = obj as PhysxBoxCharacterObject;
                PhysxPhysicObject.Controller = ControllerManager.CreateController<BoxController>((PhysxPhysicObject.BoxControllerDescription));
                PhysxPhysicObject.Controller.UserData = obj;
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
            else if (obj is PhysxClothObject)
            {
                PhysxClothObject PhysxPhysicObject = obj as PhysxClothObject;
                PhysxPhysicObject.Cloth.UserData = null;
                PhysxPhysicObject.Cloth.Dispose();
            }
            else if (obj is PhysxFluidObject)
            {
                PhysxFluidObject PhysxPhysicObject = obj as PhysxFluidObject;
                PhysxPhysicObject.Fluid.UserData = null;
                PhysxPhysicObject.Fluid.Dispose();
            }

            else if (obj is PhysxCapsuleCharacterObject)
            {
                PhysxCapsuleCharacterObject PhysxPhysicObject = obj as PhysxCapsuleCharacterObject;                
                PhysxPhysicObject.Controller.UserData = null;
                PhysxPhysicObject.Controller.Dispose();
            }

            else if (obj is PhysxBoxCharacterObject)
            {
                PhysxBoxCharacterObject PhysxPhysicObject = obj as PhysxBoxCharacterObject;
                PhysxPhysicObject.Controller.UserData = null;
                PhysxPhysicObject.Controller.Dispose();
            }   
            
            objs.Remove(obj);
        }

        public override void AddConstraint(IPhysicConstraint ctn)
        {
            if (ctn is PhysxCylindricalJoint)
            {
                PhysxCylindricalJoint PhysxCylindricalJoint = ctn as PhysxCylindricalJoint;
                PhysxCylindricalJoint.CylindricalJoint = Scene.CreateJoint<CylindricalJoint>(PhysxCylindricalJoint.CylindricalJointDescription);
            }
            else if (ctn is PhysxD6Joint)
            {
                PhysxD6Joint PhysxCylindricalJoint = ctn as PhysxD6Joint;
                PhysxCylindricalJoint.D6Joint = Scene.CreateJoint<D6Joint>(PhysxCylindricalJoint.D6JointDescription);
            }
            else if (ctn is PhysxDistanceJoint)
            {
                PhysxDistanceJoint PhysxCylindricalJoint = ctn as PhysxDistanceJoint;
                PhysxCylindricalJoint.DistanceJoint = Scene.CreateJoint<DistanceJoint>(PhysxCylindricalJoint.DistanceJointDescription);
            }
            else if (ctn is PhysxFixedJoint)
            {
                PhysxFixedJoint PhysxCylindricalJoint = ctn as PhysxFixedJoint;
                PhysxCylindricalJoint.FixedJoint = Scene.CreateJoint<FixedJoint>(PhysxCylindricalJoint.FixedJointDescription);
            }

            else if (ctn is PhysxPointOnLineJoint)
            {
                PhysxPointOnLineJoint PhysxCylindricalJoint = ctn as PhysxPointOnLineJoint;
                PhysxCylindricalJoint.PointOnLineJoint = Scene.CreateJoint<PointOnLineJoint>(PhysxCylindricalJoint.PointOnLineJointDescription);
            }

            else if (ctn is PhysxPrismaticJoint)
            {
                PhysxPrismaticJoint PhysxCylindricalJoint = ctn as PhysxPrismaticJoint;
                PhysxCylindricalJoint.PrismaticJoint = Scene.CreateJoint<PrismaticJoint>(PhysxCylindricalJoint.PrismaticJointDescription);
            }

            else if (ctn is PhysxPulleyJoint)
            {
                PhysxPulleyJoint PhysxCylindricalJoint = ctn as PhysxPulleyJoint;
                PhysxCylindricalJoint.PulleyJoint = Scene.CreateJoint<PulleyJoint>(PhysxCylindricalJoint.PulleyJointDescription);
            }

            else if (ctn is PhysxRevoluteJoint)
            {
                PhysxRevoluteJoint PhysxCylindricalJoint = ctn as PhysxRevoluteJoint;
                PhysxCylindricalJoint.RevoluteJoint = Scene.CreateJoint<RevoluteJoint>(PhysxCylindricalJoint.RevoluteJointDescription);
            }

            else if (ctn is PhysxSphericalJoint)
            {
                PhysxSphericalJoint PhysxCylindricalJoint = ctn as PhysxSphericalJoint;
                PhysxCylindricalJoint.SphericalJoint = Scene.CreateJoint<SphericalJoint>(PhysxCylindricalJoint.SphericalJointDescription);
            }

            ctns.Add(ctn);
        }

        public override void RemoveConstraint(IPhysicConstraint ctn)
        {
            if (ctn is PhysxCylindricalJoint)
            {
                PhysxCylindricalJoint PhysxCylindricalJoint = ctn as PhysxCylindricalJoint;
                PhysxCylindricalJoint.CylindricalJoint.Dispose();
            }
            else if (ctn is PhysxD6Joint)
            {
                PhysxD6Joint PhysxCylindricalJoint = ctn as PhysxD6Joint;
                PhysxCylindricalJoint.D6Joint.Dispose();
            }
            else if (ctn is PhysxDistanceJoint)
            {
                PhysxDistanceJoint PhysxCylindricalJoint = ctn as PhysxDistanceJoint;
                PhysxCylindricalJoint.DistanceJoint.Dispose();
            }
            else if (ctn is PhysxFixedJoint)
            {
                PhysxFixedJoint PhysxCylindricalJoint = ctn as PhysxFixedJoint;
                PhysxCylindricalJoint.FixedJoint.Dispose();
            }

            else if (ctn is PhysxPointOnLineJoint)
            {
                PhysxPointOnLineJoint PhysxCylindricalJoint = ctn as PhysxPointOnLineJoint;
                PhysxCylindricalJoint.PointOnLineJoint.Dispose();
            }

            else if (ctn is PhysxPrismaticJoint)
            {
                PhysxPrismaticJoint PhysxCylindricalJoint = ctn as PhysxPrismaticJoint;
                PhysxCylindricalJoint.PrismaticJoint.Dispose();
            }

            else if (ctn is PhysxPulleyJoint)
            {
                PhysxPulleyJoint PhysxCylindricalJoint = ctn as PhysxPulleyJoint;
                PhysxCylindricalJoint.PulleyJoint.Dispose();
            }

            else if (ctn is PhysxRevoluteJoint)
            {
                PhysxRevoluteJoint PhysxCylindricalJoint = ctn as PhysxRevoluteJoint;
                PhysxCylindricalJoint.RevoluteJoint.Dispose();
            }

            else if (ctn is PhysxSphericalJoint)
            {
                PhysxSphericalJoint PhysxCylindricalJoint = ctn as PhysxSphericalJoint;
                PhysxCylindricalJoint.SphericalJoint.Dispose();
            }
            ctns.Remove(ctn);
        }

        public override SegmentInterceptInfo SegmentIntersect(Microsoft.Xna.Framework.Ray raio, System.Func<IPhysicObject, bool> filter, float maxDistance)
        {

            SegmentInterceptInfo SegmentInterceptInfo = null;
            foreach (var hits in Scene.RaycastAllShapes(new StillDesign.PhysX.Ray(raio.Position.AsPhysX(), raio.Direction.AsPhysX()), ShapesType.All))
            {
                if (filter(hits.Shape.Actor.UserData as IPhysicObject))
                {
                    if (SegmentInterceptInfo == null || SegmentInterceptInfo.Distance > hits.Distance)
                    {
                        SegmentInterceptInfo = new SegmentInterceptInfo();
                        SegmentInterceptInfo.Distance = hits.Distance;
                        SegmentInterceptInfo.ImpactNormal = hits.WorldNormal.AsXNA();
                        SegmentInterceptInfo.ImpactPosition = hits.WorldImpact.AsXNA();
                        SegmentInterceptInfo.PhysicObject = hits.Shape.Actor.UserData as IPhysicObject;                     
                    }
                }
            }
            return SegmentInterceptInfo;
        }

        public bool SaveCore(String fileName, UtilitiesFileType UtilitiesFileType = UtilitiesFileType.Xml)
        {
            return Utilities.CoreDump(Core, fileName, UtilitiesFileType);
        }

        public PhysicsCollection ExtractCollection()
        {
            return Utilities.ExtractCollectionFromScene(Scene);
        }

        public bool SaveCollection(String fileName, PhysicsCollection PhysicsCollection = null, UtilitiesFileType UtilitiesFileType = UtilitiesFileType.Xml, bool saveDefaults = false, bool saveCookedData = true)
        {
            if (PhysicsCollection == null)
                PhysicsCollection = ExtractCollection();
            return Utilities.SaveCollection(PhysicsCollection, fileName, UtilitiesFileType, saveDefaults, saveCookedData);
        }

        public PhysicsCollection LoadCollection(String fileName, UtilitiesFileType UtilitiesFileType = UtilitiesFileType.Xml)
        {
            return Utilities.LoadCollection(fileName, UtilitiesFileType);
        }


        iErrorReport errorReport = new iErrorReport();
        internal class iErrorReport : ErrorReport
        {
            public override void ErrorMessage(bool isError, string message)
            {
                PloobsEngine.Engine.Logger.ActiveLogger.LogMessage(message, isError ? Engine.Logger.LogLevel.FatalError : Engine.Logger.LogLevel.Warning);
            }
        }

        public override void DetectCollisions(IPhysicObject po, List<IPhysicObject> resp)
        {
            if (po.BoundingBox.HasValue)
            {
                foreach (var item in Scene.OverlappedShapes(new Bounds3(po.BoundingBox.Value.Min.AsPhysX(), po.BoundingBox.Value.Max.AsPhysX()), ShapesType.All))
                {
                    if (item.Actor.UserData is IPhysicObject)
                    {
                        IPhysicObject IPhysicObject = item.Actor.UserData as IPhysicObject;
                        resp.Add(IPhysicObject);
                    }
                }
            }
        }

        public override void GetPhysicsObjectsInRange(IPhysicObject po, float distance, CullerConditionAvaliator<IPhysicObject, SceneControl.IObject> CullerAvaliator, List<IPhysicObject> resp)
        {
            foreach (var item in Scene.OverlappedShapes(new Sphere(distance, po.Position.AsPhysX()), ShapesType.All))
            {
                if (item.Actor.UserData is IPhysicObject)
                {
                    IPhysicObject IPhysicObject = item.Actor.UserData as IPhysicObject;
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
                if (item.Actor.UserData is IPhysicObject)
                {
                    IPhysicObject IPhysicObject = item.Actor.UserData as IPhysicObject;
                    if (CullerAvaliator(IPhysicObject, IPhysicObject.ObjectOwner))
                    {
                        resp.Add(IPhysicObject);
                    }
                }
            }
        }

        protected override void CleanUp()
        {
            if(this.ControllerManager!= null && !this.ControllerManager.IsDisposed)
                this.ControllerManager.Dispose();

            if (this.Scene != null && !this.Scene.IsDisposed)
                this.Scene.Dispose();
            
            if(this.Core != null && !this.Core.IsDisposed)
                this.Core.Dispose();
            
            base.CleanUp();
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class TriggerReport : UserTriggerReport
    {
        public override void OnTrigger(Shape triggerShape, Shape otherShape, TriggerFlag status)
        {
            if (triggerShape.UserData != null)
            {
                PhysxTrigger PhysxTrigger = triggerShape.UserData as PhysxTrigger;
                PhysxTrigger.fireEvent(status, otherShape.Actor.UserData as IPhysicObject);                
            }
        }
    }
}
#endif