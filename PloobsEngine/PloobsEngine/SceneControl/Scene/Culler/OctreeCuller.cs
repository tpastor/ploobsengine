using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
//using PloobsEngine.Draw;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Features.DebugDraw;

namespace PloobsEngine.SceneControl
{
    public class OctreeCuller : ICuller
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OctreeCuller"/> class.
        /// </summary>
        /// <param name="worldSize">Size of the world.</param>
        /// <param name="loose">The loose.</param>
        /// <param name="maxDepth">The max depth.</param>
        /// <param name="center">The center.</param>
        /// <param name="DebugDrawer">The debug drawer. We strong recomend you to DONT JUST this DebugDrawer for nothing anymore, if you need, create another</param>
        public OctreeCuller(float worldSize, float loose, int maxDepth, Vector3 center, DebugShapesDrawer DebugDrawer = null)
        {
            oct = new Octree<IObject>(worldSize, loose, maxDepth, center);
            if (DebugDrawer != null)
            {
                DebugDrawer.DrawAllShapesEachFrame = false;
                oct.DebugDraw = DebugDrawer;                
            }            
        }        

        
        #region ICuller Members        
        private Octree<IObject> oct;
        private Dictionary<IObject, Octree<IObject>> objOctree = new Dictionary<IObject, Octree<IObject>>();                

        List<IObject> ghostForward = new List<IObject>();
        List<IObject> ghostDeferred = new List<IObject>();
        List<IObject> deferred = new List<IObject>();
        List<IObject> forward = new List<IObject>();

        public override void StartFrame(Matrix view, Matrix projection, BoundingFrustum frustrum)
        {
                forward.Clear();
                deferred.Clear();
                List<IObject> obs = new List<IObject>();
                oct.Draw(view, projection, obs);

                foreach (var item in obs)
                {
                    if (item.Material.MaterialType == PloobsEngine.Material.MaterialType.DEFERRED)
                        deferred.Add(item);
                    else
                        forward.Add(item);
                }

                deferred.AddRange(ghostDeferred);
                forward.AddRange(ghostForward);

                num = deferred.Count + forward.Count;                
        }

        public override IEnumerable<IObject> GetNotCulledObjectsList(PloobsEngine.Material.MaterialType? Filter)
        {
            if (Filter == PloobsEngine.Material.MaterialType.DEFERRED)
                return deferred;
            else if (Filter == PloobsEngine.Material.MaterialType.FORWARD)
                return forward;
            else
            {
                List<IObject> objs = new List<IObject>();
                objs.AddRange(deferred);
                objs.AddRange(forward);
                return objs;
            }
        }

        public override void onObjectAdded(IObject obj)
        {
            if (obj.PhysicObject.PhysicObjectTypes == PloobsEngine.Physics.PhysicObjectTypes.GHOST || obj.PhysicObject.PhysicObjectTypes == PloobsEngine.Physics.PhysicObjectTypes.NONE)
            {
                if (obj.Material.MaterialType == PloobsEngine.Material.MaterialType.FORWARD)
                    ghostForward.Add(obj);
                else
                    ghostDeferred.Add(obj);
                return;
            }

            var octAdd = oct.Add(obj, obj.PhysicObject.Position, obj.Modelo.GetModelRadius());
            if (octAdd == null)
            {
                return;
            }
            objOctree.Add(obj, octAdd);
            obj.OnHasMoved += new OnHasMoved(obj_OnHasMoved);
        }

        private void  obj_OnHasMoved(IObject obj)
        {
            if (!objOctree.ContainsKey(obj))
                return;

            if (objOctree[obj].StillInside(obj, obj.PhysicObject.Position, obj.Modelo.GetModelRadius()))
            {
                return;
            }
            else
            {
                objOctree[obj].Remove(obj);
                objOctree.Remove(obj);
                var octAdd = oct.Add(obj, obj.PhysicObject.Position, obj.Modelo.GetModelRadius());
                if (octAdd == null)
                {
                    return;
                }
                objOctree.Add(obj, octAdd);
            }
        }

        public override void onObjectRemoved(IObject obj)
        {
            if (obj.PhysicObject.PhysicObjectTypes == PloobsEngine.Physics.PhysicObjectTypes.GHOST || obj.PhysicObject.PhysicObjectTypes == PloobsEngine.Physics.PhysicObjectTypes.NONE)
            {
                if (obj.Material.MaterialType == PloobsEngine.Material.MaterialType.FORWARD)
                    ghostForward.Remove(obj);
                else                
                    ghostDeferred.Remove(obj);
                return;
            }

            objOctree[obj].Remove(obj);
            objOctree.Remove(obj);
        }

        int num;
        public override int RenderedObjectThisFrame
        {
            get { return num; }
        }

        #endregion

    }
}
