using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Light2D
{
    public class PointLight2D : Light2D
    {        
        public PointLight2D(Vector2 position, Color color, float intensisty = 1, ShadowmapSize ShadowmapSize = ShadowmapSize.Size512)
            : base(position, color, intensisty, ShadowmapSize)
        {            
        }
    }
}
