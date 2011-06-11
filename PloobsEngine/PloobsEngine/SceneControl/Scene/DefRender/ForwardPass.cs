using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Material;
using PloobsEngine.Cameras;

namespace PloobsEngine.SceneControl
{
    internal class comparer : IComparer<IObject>
    {   
        #region IComparer<IObject> Members

        public  Vector3 CameraPosition;

        public int Compare(IObject x, IObject y)
        {
            float d1 = Vector3.DistanceSquared(CameraPosition, x.PhysicObject.Position);
            float d2 = Vector3.DistanceSquared(CameraPosition, y.PhysicObject.Position);
            
            if (d1 > d2)
            {
                return 1;
            }
            else if (d1 == d2)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }

    public struct ForwardPassDescription
    {
        public ForwardPassDescription(bool ForwardDrawPass, bool ForwarPosDrawPass, bool DeferredPosDrawPass, bool DeferredSortByCameraDistance, bool ForwardSortByCameraDistance)
        {
            this.ForwardDrawPass = ForwardDrawPass;
            this.ForwarPosDrawPass = ForwarPosDrawPass;
            this.DeferredPosDrawPass = DeferredPosDrawPass;
            this.ForwardSortByCameraDistance = ForwardSortByCameraDistance;
            this.DeferredSortByCameraDistance = DeferredSortByCameraDistance;
        }

        public static ForwardPassDescription Default()
        {
            return new ForwardPassDescription(true, true, true,false,false);            
        }

        public bool ForwardDrawPass;
        public bool ForwarPosDrawPass;
        public bool DeferredPosDrawPass;
        public bool ForwardSortByCameraDistance;
        public bool DeferredSortByCameraDistance;

    }


    public class ForwardPass : IForwardPass
    {
        ForwardPassDescription ForwardPassDescription;

        public ForwardPassDescription GetForwardPassDescription()
        {
            return ForwardPassDescription;
        }

        public void ApplyForwardPassDescription(ForwardPassDescription desc)
        {
            this.ForwardPassDescription = desc;
        }

        public ForwardPass(ForwardPassDescription ForwardPassDescription)
        {
            this.ForwardPassDescription = ForwardPassDescription;
        }
        
        comparer c = new comparer();

        public void Draw(GameTime gt, IWorld world, RenderHelper render, List<IObject> deferred, List<IObject> forward)        
        {            
            if (ForwardPassDescription.DeferredPosDrawPass)
            {
                
                if (ForwardPassDescription.DeferredSortByCameraDistance)
                {
                    c.CameraPosition = world.CameraManager.ActiveCamera.Position;
                    deferred.Sort(c);
                }

                foreach (IObject item in deferred)
                {
                    if(item.Material.IsVisible)
                        item.Material.PosDrawnPhase(gt, item, world.CameraManager.ActiveCamera, world.Lights, render);
                }
            }

            if (ForwardPassDescription.ForwardDrawPass || ForwardPassDescription.ForwarPosDrawPass)
            {                
                if (ForwardPassDescription.ForwardSortByCameraDistance)
                {
                    c.CameraPosition = world.CameraManager.ActiveCamera.Position;
                    forward.Sort(c);
                }

                if (ForwardPassDescription.ForwardDrawPass)
                {
                    foreach (IObject item in forward)
                    {
                        item.Material.Drawn(gt, item, world.CameraManager.ActiveCamera, world.Lights, render);
                    }
                }

                if (ForwardPassDescription.ForwarPosDrawPass)
                {
                    foreach (IObject item in forward)
                    {
                        item.Material.PosDrawnPhase(gt, item, world.CameraManager.ActiveCamera, world.Lights, render);
                    }
                }
            }

        }
        
    }
}
