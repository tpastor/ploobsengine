using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public interface IObjectComparer : IComparer<IObject>
    {
        Vector3 CameraPosition
        {
            get;
            set;
        }
    }

    class ComparerBackToFront : IObjectComparer
    {
        #region IComparer<IObject> Members        

        public int Compare(IObject x, IObject y)
        {
            float d1 = Vector3.DistanceSquared(CameraPosition, x.PhysicObject.Position);
            float d2 = Vector3.DistanceSquared(CameraPosition, y.PhysicObject.Position);

            if (d1 < d2)
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

        #region IObjectComparer Members

        public Vector3 CameraPosition
        {
            get;
            set;
        }

        #endregion
    }

    class ComparerFrontToBack : IObjectComparer
    {
        #region IComparer<IObject> Members

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
        public Vector3 CameraPosition
        {
            get;
            set;
        }

        #endregion
    }
}
