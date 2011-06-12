using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Modelo2D
{
    public class AnimationClass
    {
        public Rectangle[] Rectangles;        
        public bool IsLooping = true;
        public int Frames;
        public float Rotation;
    }
}
