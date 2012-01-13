using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EngineTestes.Bilboard
{
    public class SphericalBillboard3D
    {
        public SphericalBillboard3D(Texture2D tex, Vector3 pos, Vector2 scale)
        {
            this.Texture = tex;
            this.Position = pos;
            this.Scale = scale;
        }
        public Vector2 Scale;
        public Texture2D Texture;
        public Vector3 Position;

    }
}
