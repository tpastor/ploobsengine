

using System.Collections.Generic;
using BEPUphysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics.CollisionShapes;
using BEPUphysics.CollisionShapes.ConvexShapes;
using System;
using BEPUphysics.Collidables.MobileCollidables;

namespace PloobsEngine.Physics
{
    /// <summary>
    /// Helper class that can create shape mesh data.
    /// </summary>
    internal static class DisplayConvexHull
    {

        public static DebugInfo GetDebugInfo(BEPUphysics.Entities.Entity DisplayedObject, Color color)
        {
            var convexHullShape = DisplayedObject.CollisionInformation.Shape as ConvexHullShape;
            if (convexHullShape == null)
                throw new ArgumentException("Wrong shape type.");

            var hullTriangleVertices = new List<Vector3>();
            var hullTriangleIndices = new List<int>();
            Toolbox.GetConvexHull(convexHullShape.Vertices, hullTriangleIndices, hullTriangleVertices);
            //The hull triangle vertices are used as a dummy to get the unnecessary hull vertices, which are cleared afterwards.
            hullTriangleVertices.Clear();
            foreach (int i in hullTriangleIndices)
            {
                hullTriangleVertices.Add(convexHullShape.Vertices[i]);
            }

            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<short> indices = new List<short>();
                        
            Vector3 normal;
            for (short i = 0; i < hullTriangleVertices.Count; i += 3)
            {
                normal = Vector3.Normalize(Vector3.Cross(hullTriangleVertices[i + 2] - hullTriangleVertices[i], hullTriangleVertices[i + 1] - hullTriangleVertices[i]));
                vertices.Add(new VertexPositionColor(hullTriangleVertices[i], color));
                vertices.Add(new VertexPositionColor(hullTriangleVertices[i + 1], color));
                vertices.Add(new VertexPositionColor(hullTriangleVertices[i + 2],color));
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