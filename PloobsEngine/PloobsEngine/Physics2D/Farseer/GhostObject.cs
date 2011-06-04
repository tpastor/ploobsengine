using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Physics2D.Farseer
{
    public class GhostObject : I2DPhysicObject
    {
        public GhostObject(Vector2 Position, float rotation = 0)
        {
            this.Enabled = false;
            this.Position = Position;
            this.Rotation = rotation;
            this.Origin = Vector2.Zero;
        }

        public override bool isDynamic
        {
            get
            {
                return false;
            }
            set
            {                
            }
        }
    }
}
