using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EngineTestes.Decal
{
    public class Decal
    {
        public Decal(Texture2D texture, Matrix view, Matrix Projection)
        {
            this.tex = texture;
            this.Projection = Projection;
            this.view = view;
        }

        public Matrix view;
        public Matrix Projection;
        public Texture2D tex;
    }
}
