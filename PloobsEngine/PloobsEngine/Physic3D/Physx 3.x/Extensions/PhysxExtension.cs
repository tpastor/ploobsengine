#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Physics3x
{
    public static class MathExtension
    {
        public static PhysX.Math.Vector3 AsPhysX(this Vector3 vector)
        {
            return new PhysX.Math.Vector3(vector.X, vector.Y, vector.Z);
        }

        public static Vector3 AsXNA(this PhysX.Math.Vector3 vector )
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static Matrix AsXNA(this PhysX.Math.Matrix m)
        {
            return new Matrix
            (
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            );
        }

        public static PhysX.Math.Matrix AsPhysX(this Matrix m)
        {
            return new PhysX.Math.Matrix
            (
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            );
        }
    }
}
#endif