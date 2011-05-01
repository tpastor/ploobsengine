using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEPUphysics;
using Microsoft.Xna.Framework;
using BEPUphysics.Entities;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Cameras;
using PloobsEngine.SceneControl;
using BEPUphysics.BroadPhaseSystems;
using BEPUphysics.Collidables.MobileCollidables;
using BEPUphysics.Collidables;
using PloobsEngine.Engine.Logger;
using BEPUphysics.Settings;
using BEPUphysics.Constraints;
using BEPUphysics.PositionUpdating;
using BEPUphysics.CollisionTests.CollisionAlgorithms;

namespace PloobsEngine.Physics
{

    /// <summary>
    /// Bepu Implementation of a Physic World
    /// </summary>
    public class BepuPhysicWorld : IPhysicWorld
    {
        Space space;                
        private List<IPhysicObject> objs;

        /// <summary>
        /// Initializes a new instance of the <see cref="BepuPhysicWorld"/> class.
        /// </summary>
        /// <param name="gravity">The gravity.</param>
        /// <param name="useRealElapsedTimeStep">if set to <c>true</c> [use real elapsed time step] in the simulation.</param>
        /// <param name="PhysicElapsedTimeMultiplier">If useRealElapsedTimeStep is true, multiply the elapsedtime by this value.</param>
        /// <param name="multiThread">if set to <c>true</c> [multi thread].</param>
        public BepuPhysicWorld(float gravity = -9.8f, bool useRealElapsedTimeStep = false, float PhysicElapsedTimeMultiplier = 1, bool multiThread = false)
        {
            space = new Space();
            objs = new List<IPhysicObject>();
            space.ForceUpdater.Gravity = new Vector3(0, gravity, 0);            

            if(PhysicElapsedTimeMultiplier <= 0)            
            {
                ActiveLogger.LogMessage("PhysicElapesedTimeMultiplier must be bigger than zero, forced to default one", LogLevel.RecoverableError);
            }

            this.useRealElapsedTimeStep = useRealElapsedTimeStep;
            this.PhysicElapesedTimeMultiplier = PhysicElapsedTimeMultiplier;
            
            space.ForceUpdater.AllowMultithreading = multiThread;

            if (multiThread)
            {
#if XBOX360
            //Note that not all four available hardware threads are used.
            //Currently, BEPUphysics will allocate an equal amount of work to each thread on the xbox360.
            //If two threads are put on one core, it will bottleneck the engine and run significantly slower than using 3 hardware threads.
            Space.ThreadManager.AddThread(delegate { Thread.CurrentThread.SetProcessorAffinity(new[] { 1 }); }, null);
            Space.ThreadManager.AddThread(delegate { Thread.CurrentThread.SetProcessorAffinity(new[] { 3 }); }, null);
            Space.ThreadManager.AddThread(delegate { Thread.CurrentThread.SetProcessorAffinity(new[] { 5 }); }, null);

#else
                if (Environment.ProcessorCount > 1)
                {
                    for (int i = 0; i < Environment.ProcessorCount; i++)
                    {
                        Space.ThreadManager.AddThread();
                    }
                }
#endif

            }
        }

        bool useRealElapsedTimeStep ;
        float PhysicElapesedTimeMultiplier;

        /// <summary>
        /// Gets or sets the space.
        /// </summary>
        /// <value>
        /// The space.
        /// </value>
        public Space Space
        {
            get { return space; }
            set { space = value; }
        }        

        #region IPhysicWorld Members


        /// <summary>
        /// Updates
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            var dt = (float)gt.ElapsedGameTime.TotalSeconds;
            if(useRealElapsedTimeStep)
                space.Update(dt * PhysicElapesedTimeMultiplier);
            space.Update();        
        }

        /// <summary>
        /// Adds the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public override void AddObject(IPhysicObject obj)
        {            
            if (obj.PhysicObjectTypes == PhysicObjectTypes.TRIANGLEMESHOBJECT)
            {
                TriangleMeshObject bo = (TriangleMeshObject)obj;
                bo.StaticMesh.Tag = obj;
                space.Add(bo.StaticMesh);
                objs.Add(obj);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.SPECIALIZEDMOVER)
            {
                ObjectMover m = (ObjectMover)obj;
                space.Add(m.BepuEntityObject.Entity);
                m.BepuEntityObject.Entity.CollisionInformation.Tag = obj;
                space.Add(m.Mover);
                space.Add(m.Rotator);
                objs.Add(m.BepuEntityObject);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.TERRAIN)
            {
                TerrainObject t = obj as TerrainObject;
                space.Add(t.Terrain);
                t.Terrain.Tag = obj;                
                objs.Add(obj);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.DETECTOROBJECT)
            {
                DetectorVolumeObject m = (DetectorVolumeObject)obj;
                space.Add(m.DetectorVolume);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.CHARACTEROBJECT)
            {
                CharacterObject cc = (CharacterObject)obj;
                cc.CharacterController.Body.CollisionInformation.Tag = obj;
                space.Add(cc.CharacterController);
                objs.Add(obj);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.GHOST)
            {
                //if (obj is AgregatedPhysicObject)
                //{
                //    objs.Add(obj);
                //    AgregatedPhysicObject ag = obj as AgregatedPhysicObject;
                //    foreach (var item in ag.PhysicsObjects)
                //    {
                //        item.ObjectOwner = obj.ObjectOwner;
                //        this.AddObject(item);
                //    }
                //}
                //else
                //{
                    objs.Add(obj);
                //}
            }
            else
            {
                BepuEntityObject bo = (BepuEntityObject)obj;
                bo.Entity.CollisionInformation.Tag = obj;
                space.Add(bo.Entity);
                objs.Add(obj);
            }
            
        }

        /// <summary>
        /// Removes the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public override void RemoveObject(IPhysicObject obj)
        {
            if (obj.PhysicObjectTypes == PhysicObjectTypes.TRIANGLEMESHOBJECT)
            {
                TriangleMeshObject bo = (TriangleMeshObject)obj;
                bo.StaticMesh.Tag = null;
                space.Remove(bo.StaticMesh);
                objs.Remove(obj);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.SPECIALIZEDMOVER)
            {
                ObjectMover m = (ObjectMover)obj;
                space.Remove(m.BepuEntityObject.Entity);
                m.BepuEntityObject.Entity.CollisionInformation.Tag = null;
                space.Remove(m.Mover);
                space.Remove(m.Rotator);
                objs.Remove(m.BepuEntityObject);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.DETECTOROBJECT)
            {
                DetectorVolumeObject m = (DetectorVolumeObject)obj;
                space.Remove(m.DetectorVolume);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.TERRAIN)
            {
                TerrainObject t = obj as TerrainObject;
                space.Remove(t.Terrain);
                t.Terrain.Tag = null;
                objs.Remove(obj);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.CHARACTEROBJECT)
            {
                CharacterObject cc = (CharacterObject)obj;
                cc.CharacterController.Body.CollisionInformation.Tag = null;
                space.Remove(cc.CharacterController);
                objs.Remove(obj);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.GHOST)
            {
                //if (obj is AgregatedPhysicObject)
                //{
                //    objs.Remove(obj);
                //    AgregatedPhysicObject ag = obj as AgregatedPhysicObject;
                //    foreach (var item in ag.PhysicsObjects)
                //    {
                //        item.ObjectOwner = null;
                //        this.RemoveObject(item);
                //    }
                //}
                //else
                //{
                    objs.Remove(obj);
                //}
            }
            else
            {
                BepuEntityObject bo = (BepuEntityObject)obj;
                bo.Entity.CollisionInformation.Tag = null;
                space.Remove(bo.Entity);
                objs.Remove(obj);
            }            
         
        }

        /// <summary>
        /// Draw the physic world in debug mode.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="cam">The cam.</param>
        protected override void DebugDrawn(GameTime gt, ICamera cam)
        {           
            
        }


        /// <summary>
        /// Raycast
        /// </summary>
        /// <param name="raio">The raio.</param>
        /// <param name="filter"></param>
        /// <param name="maxDistance">The max distance.</param>
        /// <returns></returns>
        public override SegmentInterceptInfo SegmentIntersect(Ray raio, Func<IPhysicObject,bool> filter , float maxDistance)
        {
            RayCastResult result;
            if (space.RayCast(raio, maxDistance, (a) => { return filter(BepuEntityObject.RecoverIPhysicObjectFromBroadPhase(a)); }, out result))
            {
                SegmentInterceptInfo resp = new SegmentInterceptInfo();
                resp.Distance = Vector3.Distance(result.HitData.Location, raio.Position);
                resp.ImpactNormal = result.HitData.Normal;
                resp.ImpactPosition = result.HitData.Location;
                resp.PhysicObject = BepuEntityObject.RecoverIPhysicObjectFromBroadPhase(result.HitObject);                
                return resp;
            }                       
            return null;            
        }        


        public override void DetectCollisions(IPhysicObject po,List<IPhysicObject> col)
        {
            col.Clear();
            BepuEntityObject bo = (BepuEntityObject)po;            
            
            foreach (var item in bo.Entity.CollisionInformation.OverlappedCollidables) 
	        {
                IPhysicObject candidate = BepuEntityObject.RecoverIPhysicObjectFromCollidable(item);
                if(candidate!=null)
                     col.Add(candidate);         
	        }                     
        }

        /// <summary>
        /// Gets the physics objects in range.
        /// </summary>
        /// <param name="po">The po.</param>
        /// <param name="distance">The distance.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="resp">The resp.</param>
        public override void GetPhysicsObjectsInRange(IPhysicObject po, float distance, CullerConditionAvaliator<IPhysicObject, IObject> condition,List<IPhysicObject> resp)
        {
            resp.Clear();
            List<BroadPhaseEntry> ent = new List<BroadPhaseEntry>();            
            space.BroadPhase.QueryAccelerator.GetEntries(new BoundingSphere(po.Position, distance), ent);
            foreach (var item in ent)
            {
                    IPhysicObject phyObj  =BepuEntityObject.RecoverIPhysicObjectFromBroadPhase(item);                
                    if (phyObj != null)
                    {
                        if (condition(phyObj, phyObj.ObjectOwner))
                        {
                            resp.Add(phyObj);
                        }
                    }                
            }
        }

        /// <summary>
        /// Gets the Iobjects in range.
        /// </summary>
        /// <param name="frustrum">The frustrum.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="resp">The resp.</param>
        public void GetIObjectsInRange(BoundingFrustum frustrum, CullerConditionAvaliator<IPhysicObject, IObject> condition,List<IObject> resp)
        {
            resp.Clear();
            List<BroadPhaseEntry> ent = new List<BroadPhaseEntry>();            
            space.BroadPhase.QueryAccelerator.GetEntries(frustrum, ent);            
            foreach (var item in ent)
            {
                    IPhysicObject phyObj = BepuEntityObject.RecoverIPhysicObjectFromBroadPhase(item);                
                    if (phyObj != null)
                    {
                        if (condition(phyObj, phyObj.ObjectOwner))
                        {
                            resp.Add(phyObj.ObjectOwner);
                        }
                    }                
            }            
        }


        /// <summary>
        /// Gets the physic objects.
        /// </summary>
        public override List<IPhysicObject> PhysicObjects
        {
            get { return objs; }
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }

        /// <summary>
        /// Applies the default settings to the space.
        /// These values are what the engine starts with; they don't have to be applied unless you just want to get back to the defaults.
        /// This doesn't cover every single tunable field in the entire engine, just the main ones that this helper class is messing with.
        /// </summary>
        /// <param name="space">Space to configure.</param>
        public static void ApplyDefaultSettings(BepuPhysicWorld world)
        {
            MotionSettings.ConserveAngularMomentum = false;
            MotionSettings.DefaultPositionUpdateMode = PositionUpdateMode.Discrete;
            MotionSettings.UseRk4AngularIntegration = false;
            SolverSettings.DefaultMinimumIterations = 1;
            world.Space.Solver.IterationLimit = 10;
            GeneralConvexPairTester.UseSimplexCaching = false;
            MotionSettings.UseExtraExpansionForContinuousBoundingBoxes = false;
        }

        /// <summary>
        /// Applies some rotation-related settings.
        /// With these settings enabled, rotation generally behaves better with long shapes.
        /// Angular motion is more realistic since the momentum is conserved.
        /// However, these settings can also cause some instability to sneak into the simulation.
        /// Try using these settings on the Saw Contraption demo to see an example of what can go
        /// wrong when conservation is enabled.
        /// </summary>
        public static void ApplyRotationSettings()
        {
            MotionSettings.ConserveAngularMomentum = true;
            MotionSettings.UseRk4AngularIntegration = true;
        }

        /// <summary>
        /// Applies slightly higher speed settings.
        /// The only change here is the default minimum iterations.
        /// In many simulations, having a minimum iteration count of 0 works just fine.
        /// It's a quick and still fairly robust way to get some extra performance.
        /// An example of where this might introduce some issues is sphere stacking.
        /// </summary>
        public static void ApplySemiSpeedySettings()
        {
            SolverSettings.DefaultMinimumIterations = 0;
        }

        /// <summary>
        /// Applies some low quality, high speed settings.
        /// The main benefit comes from the very low iteration cap.
        /// By enabling simplex caching, general convex collision detection
        /// gets a nice chunk faster, but some curved shapes lose collision detection robustness.
        /// </summary>
        /// <param name="space">Space to configure.</param>
        public static void ApplySuperSpeedySettings(BepuPhysicWorld world)
        {
            SolverSettings.DefaultMinimumIterations = 0;
            world.Space.Solver.IterationLimit = 5;
            GeneralConvexPairTester.UseSimplexCaching = true;

        }

        /// <summary>
        /// Applies some higher quality settings.
        /// By using universal continuous collision detection, missed collisions
        /// will be much, much rarer.  This actually doesn't have a huge performance cost.
        /// The increased iterations put this as a midpoint between the normal and high stability settings.
        /// </summary>
        /// <param name="space">Space to configure.</param>
        public static void ApplyMediumHighStabilitySettings(BepuPhysicWorld world)
        {
            MotionSettings.DefaultPositionUpdateMode = PositionUpdateMode.Continuous;
            SolverSettings.DefaultMinimumIterations = 2;
            world.Space.Solver.IterationLimit = 15;

        }

        /// <summary>
        /// Applies some high quality, low performance settings.
        /// By using universal continuous collision detection, missed collisions
        /// will be much, much rarer.  This actually doesn't have a huge performance cost.
        /// However, increasing the iteration limit and the minimum iterations to 5x the default
        /// will incur a pretty hefty overhead.
        /// On the upside, pretty much every simulation will be rock-solid.
        /// </summary>
        /// <param name="space">Space to configure.</param>
        public static void ApplyHighStabilitySettings(BepuPhysicWorld world)
        {
            MotionSettings.DefaultPositionUpdateMode = PositionUpdateMode.Continuous;
            MotionSettings.UseExtraExpansionForContinuousBoundingBoxes = true;
            SolverSettings.DefaultMinimumIterations = 5;
            world.Space.Solver.IterationLimit = 50;

        }

        #endregion
    }
}
