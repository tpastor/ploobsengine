using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Physics;
using PloobsEngine.Material;
using PloobsEngine.Utils;
using PloobsEngine.Cameras;
//using PloobsEngine.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Culler that uses the bepu scene control
    /// </summary>
    public class SimpleCuller : ICuller
    {
        public SimpleCuller()
        {            
        }       

        #region ICuller Members
        List<IObject> deferred = new List<IObject>();
        List<IObject> forward = new List<IObject>();
        public override void StartFrame(Matrix view, Matrix projection, BoundingFrustum frustrum)
        {
            forward.Clear();
            deferred.Clear();
            
            foreach (var item in world.Objects)
            {
                if (item.Material.MaterialType == MaterialType.DEFERRED)
                {
                    if (item.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.GHOST || item.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.NONE)
                    {
                        deferred.Add(item);
                        continue;
                    }

                    if (frustrum.Contains(item.PhysicObject.BoundingBox) != Microsoft.Xna.Framework.ContainmentType.Disjoint)
                    {
                        deferred.Add(item);
                    }
                }
                else
                {
                    if (item.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.GHOST || item.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.NONE)
                    {
                        forward.Add(item);
                        continue;
                    }

                    //if (cam.BoundingFrustum.Contains(item.PhysicObject.BoundingBox) != Microsoft.Xna.Framework.ContainmentType.Disjoint)
                    {
                        forward.Add(item);
                    }    
                }
            }
            num = forward.Count + deferred.Count;            
        }

        public override IEnumerable<IObject> GetNotCulledObjectsList(MaterialType? Filter)
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
            
        }

        public override void onObjectRemoved(IObject obj)
        {
            
        }

        #endregion

        #region ICuller Members
        int num;
        public override int RenderedObjectThisFrame
        {
            get { return num; }
        }

        #endregion        
    }
}
