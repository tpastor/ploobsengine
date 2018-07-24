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
using Microsoft.Xna.Framework;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Represents a Curve3D
    /// using one XNA Curve for each axis
    /// </summary>
    #if WINDOWS
    [Serializable]
#endif
    public class Curve3D
    {
        /// <summary>
        /// Curve x
        /// </summary>
        public Curve curveX = new Curve();
        /// <summary>
        /// curve y
        /// </summary>
        public Curve curveY = new Curve();
        /// <summary>
        /// curve z
        /// </summary>
        public Curve curveZ = new Curve();

        /// <summary>
        /// Initializes a new instance of the <see cref="Curve3D"/> class.
        /// </summary>
        /// <param name="CurveLoopType">Type of the curve loop.</param>
        public Curve3D(CurveLoopType CurveLoopType)
        {
            curveX.PostLoop = CurveLoopType;
            curveY.PostLoop = CurveLoopType;
            curveZ.PostLoop = CurveLoopType;

            curveX.PreLoop = CurveLoopType;
            curveY.PreLoop = CurveLoopType;
            curveZ.PreLoop = CurveLoopType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Curve3D"/> class.
        /// Linear Curve Loop Type
        /// </summary>
        public Curve3D()
        {
            curveX.PostLoop = CurveLoopType.Linear;
            curveY.PostLoop = CurveLoopType.Linear;
            curveZ.PostLoop = CurveLoopType.Linear;

            curveX.PreLoop = CurveLoopType.Linear;
            curveY.PreLoop = CurveLoopType.Linear;
            curveZ.PreLoop = CurveLoopType.Linear;
        }

        /// <summary>
        /// Sets the tangents.
        /// </summary>
        public void SetTangents()
        {
            CurveKey prev;
            CurveKey current;
            CurveKey next;
            int prevIndex;
            int nextIndex;
            for (int i = 0; i < curveX.Keys.Count; i++)
            {
                prevIndex = i - 1;
                if (prevIndex < 0) prevIndex = i;

                nextIndex = i + 1;
                if (nextIndex == curveX.Keys.Count) nextIndex = i;

                prev = curveX.Keys[prevIndex];
                next = curveX.Keys[nextIndex];
                current = curveX.Keys[i];
                SetCurveKeyTangent(ref prev, ref current, ref next);
                curveX.Keys[i] = current;
                prev = curveY.Keys[prevIndex];
                next = curveY.Keys[nextIndex];
                current = curveY.Keys[i];
                SetCurveKeyTangent(ref prev, ref current, ref next);
                curveY.Keys[i] = current;

                prev = curveZ.Keys[prevIndex];
                next = curveZ.Keys[nextIndex];
                current = curveZ.Keys[i];
                SetCurveKeyTangent(ref prev, ref current, ref next);
                curveZ.Keys[i] = current;
            }
        }
        /// <summary>
        /// Sets the curve key tangent.
        /// </summary>
        /// <param name="prev">The prev.</param>
        /// <param name="cur">The cur.</param>
        /// <param name="next">The next.</param>
        static void SetCurveKeyTangent(ref CurveKey prev, ref CurveKey cur,
            ref CurveKey next)
        {
            float dt = next.Position - prev.Position;
            float dv = next.Value - prev.Value;
            if (Math.Abs(dv) < float.Epsilon)
            {
                cur.TangentIn = 0;
                cur.TangentOut = 0;
            }
            else
            {
                // The in and out tangents should be equal to the 
                // slope between the adjacent keys.
                cur.TangentIn = dv * (cur.Position - prev.Position) / dt;
                cur.TangentOut = dv * (next.Position - cur.Position) / dt;
            }
        }

        /// <summary>
        /// Adds the point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="time">The time.</param>
        public void AddPoint(Vector3 point, float time)
        {
            curveX.Keys.Add(new CurveKey(time, point.X));
            curveY.Keys.Add(new CurveKey(time, point.Y));
            curveZ.Keys.Add(new CurveKey(time, point.Z));
        }

        /// <summary>
        /// Gets the point on curve.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public Vector3 GetPointOnCurve(float time)
        {
            Vector3 point = new Vector3();
            point.X = curveX.Evaluate(time);
            point.Y = curveY.Evaluate(time);
            point.Z = curveZ.Evaluate(time);
            return point;
        }
    }
}
