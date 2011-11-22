#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Lots of Math Helper Functions
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Check if a point lies inside a <see cref="BoundingBox"/>
        /// </summary>
        /// <param name="point">3D Point</param>
        /// <param name="box">Bounding box</param>
        /// <returns>
        /// True if point lies inside the bounding box
        /// </returns>
        public static bool PointInsideBoundingBox(Vector3 point, BoundingBox box)
        {
            if (point.X < box.Min.X)
            {
                return false;
            }

            if (point.Y < box.Min.Y)
            {
                return false;
            }

            if (point.Z < box.Min.Z)
            {
                return false;
            }

            if (point.X > box.Max.X)
            {
                return false;
            }

            if (point.Y > box.Max.Y)
            {
                return false;
            }

            if (point.Z > box.Max.Z)
            {
                return false;
            }

            // Point must be inside box
            return true;
        }

        /// <summary>
        /// Check if a point lies inside a conical region. Good for checking if a point lies in something's
        /// field-of-view cone.
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <param name="coneOrigin">Cone's origin</param>
        /// <param name="coneDirection">Cone's forward direction</param>
        /// <param name="coneAngle">Cone's theta angle (radians)</param>
        /// <returns>
        /// True if point is inside the conical region
        /// </returns>
        public static bool PointInsideCone(Vector3 point, Vector3 coneOrigin, Vector3 coneDirection, double coneAngle)
        {
            Vector3 tempVect = Vector3.Normalize(point - coneOrigin);

            return Vector3.Dot(coneDirection, tempVect) >= Math.Cos(coneAngle);
        }

        /// <summary>
        /// Check if a point lies inside of a <see cref="BoundingSphere"/>.
        /// </summary>
        /// <param name="point">3D Point</param>
        /// <param name="sphere">Sphere to check against</param>
        /// <returns>
        /// True if point is inside of the sphere
        /// </returns>
        public static bool PointInsideBoundingSphere(Vector3 point, BoundingSphere sphere)
        {
            Vector3 diffVect = point - sphere.Center;

            return diffVect.Length() < sphere.Radius;
        }

        /// <summary>
        /// Check if a point lies in a sphere. Good for checking is a point lies within a specific
        /// distance of another point, like proximity checking.
        /// </summary>
        /// <param name="point">3D Point</param>
        /// <param name="sphereCenter">Sphere's center</param>
        /// <param name="sphereRadius">Sphere's radius</param>
        /// <returns>
        /// True if point is inside of the sphere
        /// </returns>
        public static bool PointInsideSphere(Vector3 point, Vector3 sphereCenter, float sphereRadius)
        {
            Vector3 diffVect = point - sphereCenter;

            return diffVect.Length() < sphereRadius;
        }



        /// <summary>
        /// Check if the parameter is power of two
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is power of two] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPowerOfTwo(int Value)
        {
            if (Value < 2)
            {
                return false;
            }

            if ((Value & (Value - 1)) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SImple Linear interpolation
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="x0">The x0.</param>
        /// <param name="x1">The x1.</param>
        /// <returns></returns>
        public static float Interpolate(float alpha, float x0, float x1)
        {
            return x0 + ((x1 - x0) * alpha);
        }

        /// <summary>
        /// Linear Interpolates with the specified alpha.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="x0">The x0.</param>
        /// <param name="x1">The x1.</param>
        /// <returns></returns>
        public static Vector3 Interpolate(float alpha, Vector3 x0, Vector3 x1)
        {
            return x0 + ((x1 - x0) * alpha);
        }

        /// <summary>
        /// Constrain a given value (x) to be between two (ordered) bounds min
        /// and max.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>
        /// Returns x if it is between the bounds, otherwise returns the nearer bound.
        /// </returns>
        public static float Clip(float x, float min, float max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }        
        
        /// <summary>
        /// remap a value specified relative to a pair of bounding values
        /// to the corresponding value relative to another pair of bounds.
        /// Inspired by (dyna:remap-interval y y0 y1 z0 z1)
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="in0">The in0.</param>
        /// <param name="in1">The in1.</param>
        /// <param name="out0">The out0.</param>
        /// <param name="out1">The out1.</param>
        /// <returns></returns>
        public static float RemapInterval(float x, float in0, float in1, float out0, float out1)
        {
            // uninterpolate: what is x relative to the interval in0:in1?
            float relative = (x - in0) / (in1 - in0);

            // now interpolate between output interval based on relative x
            return Interpolate(relative, out0, out1);
        }

        
        /// <summary>
        /// Like remapInterval but the result is clipped to remain between
        /// out0 and out1
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="in0">The in0.</param>
        /// <param name="in1">The in1.</param>
        /// <param name="out0">The out0.</param>
        /// <param name="out1">The out1.</param>
        /// <returns></returns>
        public static float RemapIntervalClip(float x, float in0, float in1, float out0, float out1)
        {
            // uninterpolate: what is x relative to the interval in0:in1?
            float relative = (x - in0) / (in1 - in0);

            // now interpolate between output interval based on relative x
            return Interpolate(Clip(relative, 0, 1), out0, out1);
        }

        /// <summary>
        /// classify a value relative to the interval between two bounds:
        ///     returns -1 when below the lower bound
        ///     returns  0 when between the bounds (inside the interval)
        ///     returns +1 when above the upper bound
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns></returns>
        public static int IntervalComparison(float x, float lowerBound, float upperBound)
        {
            if (x < lowerBound) return -1;
            if (x > upperBound) return +1;
            return 0;
        }

        /// <summary>
        /// Scalar random walk.
        /// </summary>
        /// <param name="initial">The initial.</param>
        /// <param name="walkspeed">The walkspeed.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns></returns>
        public static float ScalarRandomWalk(float initial, float walkspeed, float min, float max)
        {
            float next = initial + ((( StaticRandom.Random() * 2) - 1) * walkspeed);
            if (next < min) return min;
            if (next > max) return max;
            return next;
        }

        /// <summary>
        /// blends new values into an accumulator to produce a smoothed time series
        /// </summary>
        /// <param name="smoothRate">The smooth rate.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="smoothedAccumulator">The smoothed accumulator.</param>
        /// <example>blendIntoAccumulator (dt * 0.4f, currentFPS, smoothedFPS)</example>
        /// <remarks>
        /// Modifies its third argument, a reference to the float accumulator holding
        /// the "smoothed time series."
        /// The first argument (smoothRate) is typically made proportional to "dt" the
        /// simulation time step.  If smoothRate is 0 the accumulator will not change,
        /// if smoothRate is 1 the accumulator will be set to the new value with no
        /// smoothing.  Useful values are "near zero".
        /// </remarks>
        public static void BlendIntoAccumulator(float smoothRate, float newValue, ref float smoothedAccumulator)
        {
            smoothedAccumulator = Interpolate(Clip(smoothRate, 0, 1), smoothedAccumulator, newValue);
        }

        /// <summary>
        /// Blends the parameter into the accumulator.
        /// </summary>
        /// <param name="smoothRate">The smooth rate.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="smoothedAccumulator">The smoothed accumulator.</param>
        public static void BlendIntoAccumulator(float smoothRate, Vector3 newValue, ref Vector3 smoothedAccumulator)
        {
            smoothedAccumulator = Interpolate(Clip(smoothRate, 0, 1), smoothedAccumulator, newValue);
        }

               
       public static Matrix CreateRotationFromLine(Vector3 start, Vector3 end)
       {
           Vector3 p; // this vector should not be between start and end
           p.X = (float) StaticRandom.RandomInstance.NextDouble();
           p.Y = (float) StaticRandom.RandomInstance.NextDouble();
           p.Z = (float) StaticRandom.RandomInstance.NextDouble();
           //
           Vector3 r = Vector3.Cross(p - start, end - start);
           Vector3 s = Vector3.Cross(r, end - start);
           r.Normalize();
           s.Normalize();
           //
           Vector3 forward = end - start;
           forward.Normalize();
           //
           float theta = 0;
           float rCosTheta = (float)Math.Cos(theta), 
                 rSinTheta = (float)Math.Sin(theta);
           //
           Vector3 up = new Vector3()
           {
               X = start.X + rCosTheta * r.X + rSinTheta * s.X,
               Y = start.Y + rCosTheta * r.Y + rSinTheta * s.Y,
               Z = start.Z + rCosTheta * r.Z + rSinTheta * s.Z
           };
           return Matrix.CreateWorld(start, forward, up);
       }


    }
}
