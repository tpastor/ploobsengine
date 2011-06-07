using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Modelo2D
{
    public enum ModelType
    {
        Texture,Vertices
    }
     

    public abstract class IModelo2D
    {
        public IModelo2D(ModelType ModelType)
        {
            this.ModelType = ModelType;
            LayerDepth = 0;
        }

        public ModelType ModelType
        {
            get;
            internal set;
        }

        public Texture2D Texture
        {
            set;
            get;
        }

        public Vector2 Origin
        {
            get;
            set;
        }

        public float LayerDepth
        {
            set;
            get;
        }
    }
}
