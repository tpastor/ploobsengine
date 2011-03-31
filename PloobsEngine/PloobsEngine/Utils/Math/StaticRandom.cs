using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Lots of Random Helpers
    /// </summary>
    public static class StaticRandom
    {
        private static Random random = new Random();

        /// <summary>
        /// Gets or sets the random instance.
        /// </summary>
        /// <value>
        /// The random instance.
        /// </value>
        public static Random RandomInstance
        {
            get { return StaticRandom.random; }
            set { StaticRandom.random = value; }
        }        

        
        /// <summary>
        // Returns a float randomly distributed between 0 and 1
        /// </summary>
        /// <returns></returns>
        public static float Random()
        {
            return (float)random.NextDouble();
        }

        
        /// <summary>
        /// Returns a float randomly distributed between lowerBound and upperBound
        /// </summary>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns></returns>
        public static float Random(float lowerBound, float upperBound)
        {
            return lowerBound + (Random() * (upperBound - lowerBound));
        }

        /// <summary>
        /// Return True or false depending of the probability passed in the parameter
        /// </summary>
        /// <param name="chanceOfTrue">Entre 0 e 1</param>
        /// <returns></returns>
        public static bool RandomChoice(float chanceOfTrue)
        {
            if (random.NextDouble() > chanceOfTrue)
                return false;
            return true;
        }


        /// <summary>
        /// Random between two parameters
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns></returns>
        public static int RandomIntInterval(int min, int max)
        {
            return random.Next(min, max);
        }


        /// <summary>
        /// Helper function chooses a random location on a triangle.
        /// </summary>
        /// <param name="position1">The position1.</param>
        /// <param name="position2">The position2.</param>
        /// <param name="position3">The position3.</param>
        /// <returns></returns>
        public static Vector3 PickRandomPoint(
                  Vector3 position1,
                  Vector3 position2,
                  Vector3 position3)
        {            

            float a = (float)random.NextDouble();
            float b = (float)random.NextDouble();

            if (a + b > 1)
            {
                a = 1 - a;
                b = 1 - b;
            }

            return Vector3.Barycentric(position1, position2, position3, a, b);
        }

        /// <summary>
        /// Pick a 3D Random Direction
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomDirection()
        {         

            Vector3 direction = new Vector3(
                    RandomBetween(-1.0f, 1.0f),
                    RandomBetween(-1.0f, 1.0f),
                    RandomBetween(-1.0f, 1.0f));
            direction.Normalize();

            return direction;
        }

        /// <summary>
        /// Returns a number between two values.
        /// </summary>
        /// <param name="min">Lower bound value</param>
        /// <param name="max">Upper bound value</param>
        /// <returns>
        /// Random number between bounds.
        /// </returns>
        public static float RandomBetween(double min, double max)
        {            
            return (float)(min + (float)random.NextDouble() * (max - min));
        }

        /// <summary>
        /// Return a Random Color
        /// </summary>
        /// <returns></returns>
        public static Color RandomColor()
        {
            return new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }

        /// <summary>
        /// Random Position inside a Box
        /// </summary>
        /// <param name="minBoxPos">The min box pos.</param>
        /// <param name="maxBoxPos">The max box pos.</param>
        /// <returns></returns>
        public static Vector3 RandomPosition(Vector3 minBoxPos, Vector3 maxBoxPos)
        {            

            return new Vector3(
                     RandomBetween(minBoxPos.X, maxBoxPos.X),
                     RandomBetween(minBoxPos.Y, maxBoxPos.Y),
                     RandomBetween(minBoxPos.Z, maxBoxPos.Z));
        }

    }
}
