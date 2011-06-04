using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl._2DScene;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Physics2D
{
    public abstract class I2DPhysicObject
    {
        Vector2 origin;

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        

        public I2DObject Owner
        {
            internal set;
            get;
        }

        public virtual bool isDynamic
        {
            get;
            set;
        }

        public virtual Vector2 Position
        {
            set;
            get;
        }
        public virtual float Rotation
        {
            set;
            get;
        }

        public virtual bool Enabled
        {
            internal set;
            get;
        }
        
        private Vector2 pos;
        private float rot;
        internal bool HasMoved()
        {
            if (!isDynamic)
                return false;

            if (Position != pos || Rotation != rot)
            {
                pos = Position;
                rot = Rotation;
                return true;
            }
            return false;
        }
    
    }
}
