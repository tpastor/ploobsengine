using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Utils
{
    /// <summary> 
    /// Represents a 2D circle. 
    /// </summary> 
    public struct Circle
    {
        private Vector2 v;
        private Vector2 direction;
        private float distanceSquared;

        /// <summary> 
        /// Center position of the circle. 
        /// </summary> 
        public Vector2 Center;

        /// <summary> 
        /// Radius of the circle. 
        /// </summary> 
        public float Radius;

        /// <summary> 
        /// Constructs a new circle. 
        /// </summary> 
        public Circle(Vector2 position, float radius)
        {
            this.distanceSquared = 0f;
            this.direction = Vector2.Zero;
            this.v = Vector2.Zero;
            this.Center = position;
            this.Radius = radius;
        }

        /// <summary> 
        /// Determines if a circle intersects a rectangle. 
        /// </summary> 
        /// <returns>True if the circle and rectangle overlap. False otherwise.</returns> 
        public bool Intersects(Rectangle rectangle)
        {
            this.v = new Vector2(MathHelper.Clamp(Center.X, rectangle.Left, rectangle.Right),
                                    MathHelper.Clamp(Center.Y, rectangle.Top, rectangle.Bottom));

            this.direction = Center - v;
            this.distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < Radius * Radius));
        }
    }
}
