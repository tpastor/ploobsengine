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
using PloobsEngine.Utils;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Lots of Vector Helpers
    /// </summary>
    public static class VectorUtils
    {
        /// <summary>
        /// Return the Perpendicular 2D normalized vector.
        /// </summary>
        /// <param name="vx">The base vector.</param>
        /// <returns></returns>
        public static Vector2 Perpendicular2DNormalized(Vector2 vx)
        {
            vx.Normalize();
            return new Vector2(-vx.Y, vx.X);
        }

        /// <summary>
        /// transforms the 3dvector to 2d, 
        /// throw away the Y component
        /// Projects to XZ PLANE
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <returns></returns>
        public static Vector2 ToVector2(Vector3 vec)
        {
            return new Vector2(vec.X, vec.Z);
        }

        /// <summary>
        /// Transforms a 2D vector to vector3.
        /// THE Y COMPONENT IS SET TO 0 -> Vector3(vec.X, 0,vec.Y);
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <returns></returns>
        public static Vector3 ToVector3(Vector2 vec)
        {
            return new Vector3(vec.X, 0,vec.Y);
        }

        /// <summary>
        /// Creates a 3D vector -> Vector3(vec.X, y, vec.Y);
        /// </summary>
        /// <param name="vec">The vec.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static Vector3 ToVector3(Vector2 vec,float y)
        {
            return new Vector3(vec.X, y, vec.Y);
        }

        /// <summary>
        /// Finds the angle between two vectors2.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns></returns>
        public static double FindAngleBetweenTwoVectors2D(Vector3 v1, Vector3 v2)
        {
            return FindAngleBetweenTwoVectors2D(ToVector2(v1), ToVector2(v2));
        }

        /// <summary>
        /// Rotates the vector by an arbitrary angle
        /// </summary>
        /// <param name="vector">Vector to rotate</param>
        /// <param name="angle">Rotation angle</param>
        /// <returns>
        /// The rotated vector
        /// </returns>
        public static Vector2 rotateVector2(Vector2 vector, double angle)
        {
            float sinAngle = (float) Math.Sin(angle);
            float cosAngle = (float) Math.Cos(angle);

            return new Vector2((vector.X * cosAngle) - (vector.Y * sinAngle), (vector.Y * cosAngle) + (vector.X * sinAngle));
        }

        /// <summary>
        /// Angle between 2 vectors WITH SIGNAL
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns></returns>
        public static double FindAngleBetweenTwoVectors2D(Vector2 v1, Vector2 v2)
        {
            double angle;
            // turn vectors into unit vectors   
            if (v1 == Vector2.Zero || v2 == Vector2.Zero)
            {
                return 0;
            }

            v1.Normalize();
            v2.Normalize();

            float an = MathUtils.Clip(Vector2.Dot(v1, v2), -1, 1);

            angle = System.Math.Acos(an);
            if (System.Math.Abs(angle) < 0.0001)
                return 0;
            angle *= signal(v1, v2);

            return angle;
        }

        
        private static int signal(Vector2 v1, Vector2 v2)
        {
            return (v1.Y * v2.X - v2.Y * v1.X) > 0 ? 1 : -1;
        }


        /// <summary>
        /// Find the angle between two vectors. This will not only give the angle difference, but the direction.
        /// For example, it may give you -1 radian, or 1 radian, depending on the direction. Angle given will be the
        /// angle from the FromVector to the DestVector, in radians.
        /// </summary>
        /// <param name="FromVector">Vector to start at.</param>
        /// <param name="DestVector">Destination vector.</param>
        /// <param name="DestVectorsRight">Right vector of the destination vector</param>
        /// <returns>
        /// Signed angle, in radians
        /// </returns>
        /// <remarks>
        /// All three vectors must lie along the same plane.
        /// </remarks>
        public static double GetSignedAngleBetween2DVectors(Vector3 FromVector, Vector3 DestVector, Vector3 DestVectorsRight)
        {
            FromVector.Normalize();
            DestVector.Normalize();
            DestVectorsRight.Normalize();

            float forwardDot = Vector3.Dot(FromVector, DestVector);
            float rightDot = Vector3.Dot(FromVector, DestVectorsRight);

            // Keep dot in range to prevent rounding errors
            forwardDot = MathHelper.Clamp(forwardDot, -1.0f, 1.0f);

            double angleBetween = Math.Acos(forwardDot);
            

            if (rightDot < 0.0f)
                angleBetween *= -1.0f;

            return angleBetween;
        }

        /// <summary>
        /// Unsigneds the angle between two v3.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <returns></returns>
        public static float UnsignedAngleBetweenTwoV3(Vector3 v1, Vector3 v2)
        {
            v1.Normalize();
            v2.Normalize();
            double Angle = (float)Math.Acos(Vector3.Dot(v1, v2));
            return (float)Angle;
        }

        /// <summary>
        /// Returns velocity of deflection
        /// </summary>
        /// <param name="CurrentVelocity">Velocity vector if item to be bounced</param>
        /// <param name="Elasticity">Elasticity of item to be bounced</param>
        /// <param name="CollisionNormal">Normal vector of plane the item is bouncing off of</param>
        /// <returns>
        /// Velocity vector of deflection
        /// </returns>
        public static Vector3 CalculateDeflection(Vector3 CurrentVelocity, float Elasticity, Vector3 CollisionNormal)
        {
            Vector3 newDirection = Elasticity * (-2 * Vector3.Dot(CurrentVelocity, CollisionNormal) * CollisionNormal + CurrentVelocity);

            return newDirection;
        }


        /// <summary>
        /// Translates a point around an origin
        /// </summary>
        /// <param name="point">Point that is going to be translated</param>
        /// <param name="originPoint">Origin of rotation</param>
        /// <param name="rotationAxis">Axis to rotate around, this Vector should be a unit vector (normalized)</param>
        /// <param name="radiansToRotate">Radians to rotate</param>
        /// <returns>
        /// Translated point
        /// </returns>
        public static Vector3 RotateAroundPoint(Vector3 point, Vector3 originPoint, Vector3 rotationAxis, float radiansToRotate)
        {
            Vector3 diffVect = point - originPoint;

            Vector3 rotatedVect = Vector3.Transform(diffVect, Matrix.CreateFromAxisAngle(rotationAxis, radiansToRotate));

            rotatedVect += originPoint;

            return rotatedVect;
        }

        /// <summary>
        /// Quaternions to euler.
        /// </summary>
        /// <param name="q">The q.</param>
        /// <returns></returns>
        public static Vector3 QuaternionToEuler(Quaternion q)
        {
            Vector3 v = Vector3.Zero;

            v.X = (float)Math.Atan2
            (
                2 * q.Y * q.W - 2 * q.X * q.Z,
                1 - 2 * Math.Pow(q.Y, 2) - 2 * Math.Pow(q.Z, 2)
            );

            v.Z = (float)Math.Asin
            (
                2 * q.X * q.Y + 2 * q.Z * q.W
            );

            v.Y = (float)Math.Atan2
            (
                2 * q.X * q.W - 2 * q.Y * q.Z,
                1 - 2 * Math.Pow(q.X, 2) - 2 * Math.Pow(q.Z, 2)
            );

            if (q.X * q.Y + q.Z * q.W == 0.5)
            {
                v.X = (float)(2 * Math.Atan2(q.X, q.W));
                v.Y = 0;
            }

            else if (q.X * q.Y + q.Z * q.W == -0.5)
            {
                v.X = (float)(-2 * Math.Atan2(q.X, q.W));
                v.Y = 0;
            }

            return v;
        }

        /// <summary>
        /// Calculate the rotation for one vector to face another vector
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="lookat">The lookat.</param>
        /// <returns></returns>
        public static Matrix LookAt(Vector3 position, Vector3 lookat)
        {
            Matrix rotation = new Matrix();
            rotation.Forward = Vector3.Normalize(lookat - position);
            rotation.Right = Vector3.Normalize(Vector3.Cross(rotation.Forward, Vector3.Up));
            rotation.Up = Vector3.Normalize(Vector3.Cross(rotation.Right, rotation.Forward));
            return rotation;
        }

        /// <summary>
        /// return component of vector parallel to a unit basis vector
        /// IMPORTANT NOTE: assumes "basis" has unit magnitude (length == 1)        
        /// Parallels the component.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="unitBasis">The unit basis.</param>
        /// <returns></returns>
        public static Vector3 ParallelComponent(Vector3 vector, Vector3 unitBasis)
        {
            float projection = Vector3.Dot(vector, unitBasis);
            return unitBasis * projection;
        }

        
        /// <summary>
        /// return component of vector perpendicular to a unit basis vector
        /// IMPORTANT NOTE: assumes "basis" has unit magnitude(length==1)
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="unitBasis">The unit basis.</param>
        /// <returns></returns>
        public static Vector3 PerpendicularComponent(Vector3 vector, Vector3 unitBasis)
        {
            return (vector - ParallelComponent(vector, unitBasis));
        }

        
        /// <summary>
        /// clamps the length of a given vector to maxLength.  If the vector is
        /// shorter its value is returned unaltered, if the vector is longer
        /// the value returned has length of maxLength and is paralle to the
        /// original input.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns></returns>
        public static Vector3 TruncateLength(Vector3 vector, float maxLength)
        {
            float maxLengthSquared = maxLength * maxLength;
            float vecLengthSquared = vector.LengthSquared();
            if (vecLengthSquared <= maxLengthSquared)
                return vector;
            else
                return (vector * (maxLength / (float)Math.Sqrt(vecLengthSquared)));
        }



        /// <summary>
        //// forces a 3d position onto the XZ (aka y=0) plane
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public static Vector3 SetYtoZero(Vector3 vector)
        {
            return new Vector3(vector.X, 0, vector.Z);
        }

        
        /// <summary>
        /// rotate this vector about the global Y (up) axis by the given angle
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public static Vector3 RotateAboutGlobalY(Vector3 vector, float angle)
        {
            float s = (float)Math.Sin(angle);
            float c = (float)Math.Cos(angle);
            return new Vector3((vector.X * c) + (vector.Z * s), (vector.Y), (vector.Z * c) - (vector.X * s));
        }

        
        /// <summary>
        /// version for caching sin/cos computation
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="sin">The sin.</param>
        /// <param name="cos">The cos.</param>
        /// <returns></returns>
        public static Vector3 RotateAboutGlobalY(Vector3 vector, float angle, ref float sin, ref float cos)
        {
            // is both are zero, they have not be initialized yet
            if (sin == 0 && cos == 0)
            {
                sin = (float)Math.Sin(angle);
                cos = (float)Math.Cos(angle);
            }
            return new Vector3((vector.X * cos) + (vector.Z * sin), vector.Y, (vector.Z * cos) - (vector.X * sin));
        }

        
        /// <summary>
        /// Spherical WrapAround.
        /// if this position is outside sphere, push it back in by one diameter
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <returns></returns>
        public static Vector3 SphericalWrapAround(Vector3 vector, Vector3 center, float radius)
        {
            Vector3 offset = vector - center;
            float r = offset.Length();
            if (r > radius)
                return vector + ((offset / r) * radius * -2);
            else
                return vector;
        }

               
        /// <summary>
        /// Returns a position randomly distributed on a disk of unit radius
        /// on the XZ (Y=0) plane, centered at the origin.  Orientation will be
        /// random and length will range between 0 and 1
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomVectorOnUnitRadiusXZDisk()
        {
            Vector3 v = new Vector3();
            do
            {
                v.X = (StaticRandom.Random() * 2) - 1;
                v.Y = 0;
                v.Z = (StaticRandom.Random() * 2) - 1;
            }
            while (v.Length() >= 1);

            return v;
        }

        
        /// <summary>
        /// Returns a position randomly distributed inside a sphere of unit radius
        /// centered at the origin.  Orientation will be random and length will range
        /// between 0 and 1
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomVectorInUnitRadiusSphere()
        {
            Vector3 v = new Vector3();
            do
            {
                v.X = (StaticRandom.Random() * 2) - 1;
                v.Y = (StaticRandom.Random() * 2) - 1;
                v.Z = (StaticRandom.Random() * 2) - 1;
            }
            while (v.Length() >= 1);

            return v;
        }

        /// <summary>
        /// Returns a position randomly distributed on the surface of a sphere
        /// of unit radius centered at the origin.  Orientation will be random
        /// and length will be 1
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomUnitVector()
        {
            Vector3 temp = RandomVectorInUnitRadiusSphere();
            temp.Normalize();

            return temp;
        }

        /// <summary>
        /// Returns a position randomly distributed on a circle of unit radius
        /// on the XZ (Y=0) plane, centered at the origin.  Orientation will be
        /// random and length will be 1
        /// </summary>
        /// <returns></returns>
        public static Vector3 RandomUnitVectorOnXZPlane()
        {
            Vector3 temp = RandomVectorInUnitRadiusSphere();

            temp.Y = 0;
            temp.Normalize();

            return temp;
        }


        /// <summary>
        /// used by limitMaxDeviationAngle / limitMinDeviationAngle below
        /// </summary>
        /// <param name="insideOrOutside">if set to <c>true</c> [inside or outside].</param>
        /// <param name="source">The source.</param>
        /// <param name="cosineOfConeAngle">The cosine of cone angle.</param>
        /// <param name="basis">The basis.</param>
        /// <returns></returns>
        public static Vector3 LimitDeviationAngleUtility(bool insideOrOutside, Vector3 source, float cosineOfConeAngle, Vector3 basis)
        {
            // immediately return zero length input vectors
            float sourceLength = source.Length();

            if (sourceLength == 0)
                return source;

            // measure the angular diviation of "source" from "basis"
            Vector3 direction = source / sourceLength;

            float cosineOfSourceAngle = Vector3.Dot(direction, basis);

            // Simply return "source" if it already meets the angle criteria.
            // (note: we hope this top "if" gets compiled out since the flag
            // is a constant when the function is inlined into its caller)
            if (insideOrOutside)
            {
                // source vector is already inside the cone, just return it
                if (cosineOfSourceAngle >= cosineOfConeAngle)
                    return source;
            }
            else if (cosineOfSourceAngle <= cosineOfConeAngle)
                return source;

            // find the portion of "source" that is perpendicular to "basis"
            Vector3 perp = PerpendicularComponent(source, basis);

            // normalize that perpendicular
            Vector3 unitPerp = perp;
            unitPerp.Normalize();

            // construct a new vector whose length equals the source vector,
            // and lies on the intersection of a plane (formed the source and
            // basis vectors) and a cone (whose axis is "basis" and whose
            // angle corresponds to cosineOfConeAngle)
            float perpDist = (float)Math.Sqrt(1 - (cosineOfConeAngle * cosineOfConeAngle));
            Vector3 c0 = basis * cosineOfConeAngle;
            Vector3 c1 = unitPerp * perpDist;
            return (c0 + c1) * sourceLength;
        }

        /// <summary>
        /// Enforce an upper bound on the angle by which a given arbitrary vector
        /// diviates from a given reference direction (specified by a unit basis
        /// vector).  The effect is to clip the "source" vector to be inside a cone
        /// defined by the basis and an angle.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="cosineOfConeAngle">The cosine of cone angle.</param>
        /// <param name="basis">The basis.</param>
        /// <returns></returns>
        public static Vector3 LimitMaxDeviationAngle(Vector3 source, float cosineOfConeAngle, Vector3 basis)
        {
            return LimitDeviationAngleUtility(true, // force source INSIDE cone
                source, cosineOfConeAngle, basis);
        }



        /// <summary>
        /// Enforce a lower bound on the angle by which a given arbitrary vector
        /// diviates from a given reference direction (specified by a unit basis
        /// vector).  The effect is to clip the "source" vector to be outside a cone
        /// defined by the basis and an angle.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="cosineOfConeAngle">The cosine of cone angle.</param>
        /// <param name="basis">The basis.</param>
        /// <returns></returns>
        public static Vector3 LimitMinDeviationAngle(Vector3 source, float cosineOfConeAngle, Vector3 basis)
        {
            return LimitDeviationAngleUtility(false, // force source OUTSIDE cone
                source, cosineOfConeAngle, basis);
        }


        /// <summary>
        /// Returns the distance between a point and a line.  The line is defined in
        /// terms of a point on the line ("lineOrigin") and a UNIT vector parallel to
        /// the line ("lineUnitTangent")
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="lineOrigin">The line origin.</param>
        /// <param name="lineUnitTangent">The line unit tangent.</param>
        /// <returns></returns>
        public static float DistanceFromLine(Vector3 point, Vector3 lineOrigin, Vector3 lineUnitTangent)
        {
            Vector3 offset = point - lineOrigin;
            Vector3 perp = VectorUtils.PerpendicularComponent(offset, lineUnitTangent);
            return perp.Length();
        }

        /// <summary>
        /// given a vector, return a vector perpendicular to it (note that this
        /// arbitrarily selects one of the infinitude of perpendicular vectors)
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static Vector3 FindPerpendicularIn3d(Vector3 direction)
        {
            // to be filled in:
            Vector3 quasiPerp;  // a direction which is "almost perpendicular"
            Vector3 result = new Vector3();     // the computed perpendicular to be returned

            // three mutually perpendicular basis vectors
            Vector3 i = Vector3.Right;
            Vector3 j = Vector3.Up;
            Vector3 k = Vector3.Backward;

            // measure the projection of "direction" onto each of the axes
            float id = Vector3.Dot(i, direction);
            float jd = Vector3.Dot(j, direction);
            float kd = Vector3.Dot(k, direction);

            // set quasiPerp to the basis which is least parallel to "direction"
            if ((id <= jd) && (id <= kd))
            {
                quasiPerp = i;               // projection onto i was the smallest
            }
            else
            {
                if ((jd <= id) && (jd <= kd))
                    quasiPerp = j;           // projection onto j was the smallest
                else
                    quasiPerp = k;           // projection onto k was the smallest
            }

            // return the cross product (direction x quasiPerp)
            // which is guaranteed to be perpendicular to both of them
            Vector3.Cross(ref direction, ref quasiPerp, out result);

            return result;
        }

    }
}
