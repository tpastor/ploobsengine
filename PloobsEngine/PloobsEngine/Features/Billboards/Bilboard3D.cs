using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Features.Billboard
{
    public class Billboard3D
    {
        public Billboard3D(Texture2D tex, Vector3 pos, Vector2 scale)
        {
            this.Texture = tex;
            this.Position = pos;
            this.Scale = scale;
        }
        public Vector2 Scale;
        public Texture2D Texture;
        public Vector3 Position;

    }


    public class TextBillboard3D
    {
        public TextBillboard3D(String tex, Color color, Vector3 pos, float scale, SpriteFont SpriteFont = null)
        {
            this.Color = color;
            this.Message = tex;
            this.Position = pos;
            this.Scale = scale;
            this.SpriteFont = SpriteFont;
        }
        public SpriteFont SpriteFont;
        public Color Color;
        public float Scale;
        public String Message;
        public Vector3 Position;

    }
}
