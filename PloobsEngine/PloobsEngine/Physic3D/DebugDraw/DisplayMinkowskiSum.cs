using System;
using System.Collections.Generic;
using BEPUphysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics.CollisionShapes.ConvexShapes;
using BEPUphysics.CollisionShapes;
using BEPUphysics.Collidables.MobileCollidables;

namespace PloobsEngine.Physics
{
    /// <summary>
    /// Helper class that can create shape mesh data.
    /// </summary>
    internal static class DisplayMinkowskiSum
    {
        /// <summary>
        /// Number of sides of spherical sampling to take when creating graphical representations.
        /// </summary>
        public static int NumSamples = 20;


        public static DebugInfo GetDebugInfo(BEPUphysics.Entities.Entity DisplayedObject, Color color)
        {
            var minkowskiShape = DisplayedObject.CollisionInformation.Shape as MinkowskiSumShape;
            if (minkowskiShape == null)
                throw new ArgumentException("Wrong shape type.");

            var points = new List<Vector3>();
            Vector3 max;
            var direction = new Vector3();
            float angleChange = MathHelper.TwoPi / NumSamples;

            for (int i = 1; i < NumSamples / 2 - 1; i++)
            {
                float phi = MathHelper.PiOver2 - i * angleChange;
                var sinPhi = (float)Math.Sin(phi);
                var cosPhi = (float)Math.Cos(phi);
                for (int j = 0; j < NumSamples; j++)
                {
                    float theta = j * angleChange;
                    direction.X = (float)Math.Cos(theta) * cosPhi;
                    direction.Y = sinPhi;
                    direction.Z = (float)Math.Sin(theta) * cosPhi;

                    minkowskiShape.GetLocalExtremePoint(direction, out max);
                    points.Add(max);
                }
            }

            minkowskiShape.GetLocalExtremePoint(Toolbox.UpVector, out max);
            points.Add(max);
            minkowskiShape.GetLocalExtremePoint(Toolbox.DownVector, out max);
            points.Add(max);


            var hullTriangleVertices = new List<Vector3>();
            var hullTriangleIndices = new List<int>();
            Toolbox.GetConvexHull(points, hullTriangleIndices, hullTriangleVertices);
            //The hull triangle vertices are used as a dummy to get the unnecessary hull vertices, which are cleared afterwards.
            hullTriangleVertices.Clear();
            foreach (int i in hullTriangleIndices)
            {
                hullTriangleVertices.Add(points[i]);
            }

            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<short> indices = new List<short>();
            Vector3 normal;
            for (short i = 0; i < hullTriangleVertices.Count; i += 3)
            {
                normal = Vector3.Normalize(Vector3.Cross(hullTriangleVertices[i + 2] - hullTriangleVertices[i], hullTriangleVertices[i + 1] - hullTriangleVertices[i]));
                vertices.Add(new VertexPositionColor(hullTriangleVertices[i], color));
                vertices.Add(new VertexPositionColor(hullTriangleVertices[i + 1], color));
                vertices.Add(new VertexPositionColor(hullTriangleVertices[i + 2], color));
                indices.Add(i);
                indices.Add((short)(i + 1));
                indices.Add((short)(i + 2));
            }
            DebugInfo DebugInfo = new DebugInfo();
            DebugInfo.vertices = vertices.ToArray();
            DebugInfo.indices = indices.ToArray();
            return DebugInfo;
        }
    }
}