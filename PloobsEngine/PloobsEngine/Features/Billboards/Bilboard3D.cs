using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

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

        public Billboard3D(Texture2D tex,IObject obj, Vector3 displacement, Vector2 scale)
        {
            this.Texture = tex;
            this.Position = displacement;
            this.obj = obj;
            this.Scale = scale;
        }
        public bool Enabled = true;
        public Vector2 Scale;
        public Texture2D Texture;
        private Vector3 position;
        IObject obj;
        public Vector3 Position
        {
            get
            {
                return obj == null ? position : obj.PhysicObject.Position + position;
            }
            set
            {
                this.position = value;
                obj = null;
            }
        }

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

        public TextBillboard3D(String tex, Color color, IObject obj, Vector3 displacement, float scale, SpriteFont SpriteFont = null)
        {
            this.Color = color;
            this.Message = tex;
            this.obj = obj;
            this.position = displacement;
            this.Scale = scale;
            this.SpriteFont = SpriteFont;
        }
        public bool Enabled = true;
        public SpriteFont SpriteFont;
        public Color Color;
        public float Scale;
        public String Message;
        IObject obj;

        private Vector3 position;
        public Vector3 Position
        {
            get
            {
                return obj == null ? position : obj.PhysicObject.Position + position;
            }
            set
            {
                this.position = value;
                obj = null;
            }
        }
    }
}
