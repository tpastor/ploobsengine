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
            float d1 = Vector3.Distance(CameraPosition, x.Position);
            float d2 = Vector3.Distance(CameraPosition, y.Position);
            
            if (d1 > d2)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }

    public class ForwardPass : IForwardPass
    {
        public ForwardPass(bool sortByCameraDistance = false)
        {
            this.sortByCameraDistance = sortByCameraDistance;
        }

        bool sortByCameraDistance = false;
        comparer c = new comparer();

        public void Draw(GameTime gt, IWorld world,RenderHelper render)        
        {
            IEnumerable<IObject> list = world.Culler.GetNotCulledObjectsList(world.CameraManager.ActiveCamera,MaterialType.FORWARD );            
            if(sortByCameraDistance)
                list.OrderBy((a) => a,c);
            
            foreach (IObject item in list)
	        {
                    item.Material.PosDrawnPhase(gt, item,world.CameraManager.ActiveCamera, world.Lights, render);
	        } 

        }
        
    }
}
