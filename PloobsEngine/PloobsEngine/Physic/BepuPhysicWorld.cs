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

namespace PloobsEngine.Physics
{

    /// <summary>
    /// Bepu Implementation of a Physic World
    /// </summary>
    public class BepuPhysicWorld : IPhysicWorld
    {
        Space space;                
        private List<IPhysicObject> objs;

        public BepuPhysicWorld(float gravity = -9.8f)
        {
            space = new Space();
            objs = new List<IPhysicObject>();
            space.ForceUpdater.Gravity = new Vector3(0, gravity, 0);                        
        }

        public Space Space
        {
            get { return space; }
            set { space = value; }
        }        

        #region IPhysicWorld Members
               

        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {            
            space.Update(gt.ElapsedGameTime.Milliseconds);
        
        }

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
                //ObjectMover m = (ObjectMover)obj;
                //space.Add(m.BepuEntityObject.Entity);
                //m.BepuEntityObject.Entity.Tag = obj;
                //space.Add(m.Mover);
                //space.Add(m.Rotator);
                //objs.Add(m.BepuEntityObject);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.TERRAIN)
            {
                //TerrainObject t = obj as TerrainObject;
                //space.Add(t.Terrain);
                //t.Terrain.Tag = obj;
                //objs.Add(obj);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.DETECTOROBJECT)
            {
                //DetectorVolumeObject m = (DetectorVolumeObject)obj;
                //space.Add(m.DetectorVolume);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.CHARACTEROBJECT)
            {
                //CharacterController cc = (CharacterController)obj;
                //cc.Body.Tag = obj;
                //space.Add(cc);
                //objs.Add(obj);
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
                //ObjectMover m = (ObjectMover)obj;
                //space.Remove(m.BepuEntityObject.Entity);
                //m.BepuEntityObject.Entity.Tag = null;
                //space.Remove(m.Mover);
                //space.Remove(m.Rotator);
                //objs.Remove(m.BepuEntityObject);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.DETECTOROBJECT)
            {
                //DetectorVolumeObject m = (DetectorVolumeObject)obj;
                //space.Remove(m.DetectorVolume);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.TERRAIN)
            {
                //TerrainObject t = obj as TerrainObject;
                //space.Remove(t.Terrain);
                //t.Terrain.Tag = null;
                //objs.Remove(obj);
            }
            else if (obj.PhysicObjectTypes == PhysicObjectTypes.CHARACTEROBJECT)
            {
                //CharacterController cc = (CharacterController)obj;
                //cc.Body.Tag = null;
                //space.Remove(cc);
                //objs.Remove(obj);
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

        protected override void DebugDrawn(GameTime gt, ICamera cam)
        {           
            
        }


        public override SegmentInterceptInfo SegmentIntersect(Ray raio, Func<IPhysicObject,bool> filter , float maxDistance)
        {
            RayCastResult result;
            if (space.RayCast(raio, maxDistance, (a) => { return filter(RecoverFromBroadPhaseEntry(a)); }, out result))
            {
                SegmentInterceptInfo resp = new SegmentInterceptInfo();
                resp.Distance = Vector3.Distance(result.HitData.Location, raio.Position);
                resp.ImpactNormal = result.HitData.Normal;
                resp.ImpactPosition = result.HitData.Location;
                resp.PhysicObject = RecoverFromBroadPhaseEntry(result.HitObject);                
                return resp;
            }                       
            return null;            
        }        

        internal static IPhysicObject RecoverFromBroadPhaseEntry(BroadPhaseEntry entry)
        {
            IPhysicObject phyObj = null;
            if (entry is Collidable)
            {         
                Collidable collidable = (entry as Collidable);
                phyObj = collidable.Tag as IPhysicObject;
            }
            return phyObj;
        }

        internal static IPhysicObject RecoverFromCollidable(Collidable entry)
        {
                IPhysicObject phyObj = null;
                Collidable collidable = (entry as Collidable);                
                phyObj = collidable.Tag as IPhysicObject;                                                    
                return phyObj;
        }


        public override void DetectCollisions(IPhysicObject po,List<IPhysicObject> col)
        {
            col.Clear();
            BepuEntityObject bo = (BepuEntityObject)po;            
            
            foreach (var item in bo.Entity.CollisionInformation.OverlappedCollidables) 
	        {                
                IPhysicObject candidate = RecoverFromCollidable(item);
                if(candidate!=null)
                     col.Add(candidate);         
	        }                     
        }

        public override void GetPhysicsObjectsInRange(IPhysicObject po, float distance, CullerConditionAvaliator<IPhysicObject, IObject> condition,List<IPhysicObject> resp)
        {
            resp.Clear();
            List<BroadPhaseEntry> ent = new List<BroadPhaseEntry>();            
            space.BroadPhase.QueryAccelerator.GetEntries(new BoundingSphere(po.Position, distance), ent);
            foreach (var item in ent)
            {
                    IPhysicObject phyObj  = RecoverFromBroadPhaseEntry(item);                
                    if (phyObj != null)
                    {
                        if (condition(phyObj, phyObj.ObjectOwner))
                        {
                            resp.Add(phyObj);
                        }
                    }                
            }
        }        

        public void GetIObjectsInRange(BoundingFrustum frustrum, CullerConditionAvaliator<IPhysicObject, IObject> condition,List<IObject> resp)
        {
            resp.Clear();
            List<BroadPhaseEntry> ent = new List<BroadPhaseEntry>();            
            space.BroadPhase.QueryAccelerator.GetEntries(frustrum, ent);            
            foreach (var item in ent)
            {
                    IPhysicObject phyObj = RecoverFromBroadPhaseEntry(item);                
                    if (phyObj != null)
                    {
                        if (condition(phyObj, phyObj.ObjectOwner))
                        {
                            resp.Add(phyObj.ObjectOwner);
                        }
                    }                
            }            
        }        
        

        public override List<IPhysicObject> PhysicObjects
        {
            get { return objs; }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }

        #endregion
    }
}
