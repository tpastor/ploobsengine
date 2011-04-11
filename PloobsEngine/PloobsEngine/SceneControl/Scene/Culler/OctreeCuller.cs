using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
//using PloobsEngine.Draw;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.Cameras;

namespace PloobsEngine.SceneControl
{
    public class OctreeCuller : ICuller
    {

        public OctreeCuller(float worldSize, float loose, int maxDepth , Vector3 center)
        {
            oct = new Octree<IObject>(worldSize, loose, maxDepth, center);            
        }

        /// <summary>
        /// Enable Debug
        /// </summary>
        /// <param name="worldSize"></param>
        /// <param name="loose"></param>
        /// <param name="maxDepth"></param>
        /// <param name="center"></param>
        /// <param name="mundo"></param>
        /// <param name="engine"></param>
        public OctreeCuller(float worldSize, float loose, int maxDepth, Vector3 center, bool debug)
        {
            oct = new Octree<IObject>(worldSize, loose, maxDepth, center);
            oct.IsDebug = debug;
            this.debug = debug;            
        }

        public bool isDebug
        {
            get { return debug; }
            set { debug = value; }
        }

        #region ICuller Members
        bool debug = false;        
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
                if (debug)
                {
                    //Drawing.Draw2dTextAt2dLocation("Deferred " + deferred.Count, new Vector2(20, 60), Color.White);
                    //Drawing.Draw2dTextAt2dLocation("Forward " + forward.Count, new Vector2(20, 80), Color.White);
                }
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

            var octAdd = oct.Add(obj, obj.Position, obj.Modelo.GetModelRadius());
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

            if (objOctree[obj].StillInside(obj, obj.Position, obj.Modelo.GetModelRadius()))
            {
                return;
            }
            else
            {
                objOctree[obj].Remove(obj);
                objOctree.Remove(obj);
                var octAdd = oct.Add(obj, obj.Position, obj.Modelo.GetModelRadius());
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
