using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Light2D
{
    public class SpotLight2D : Light2D
    {
        public Vector2 Direction;
        public float Angle;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotLight2D"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="angle">The angle. IN RADIANS !!!!!!!!!!!!!!!</param>
        /// <param name="intensity">The intensity.</param>
        /// <param name="ShadowmapSize">Size of the shadowmap.</param>
        public SpotLight2D(Vector2 position, Color color, Vector2 direction ,float angle,float intensity = 1, ShadowmapSize ShadowmapSize = ShadowmapSize.Size512)
            : base(position, color, intensity, ShadowmapSize)
        {
            this.Angle = angle;
            this.Direction = direction;            
        }
    }
}
