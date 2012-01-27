
using System;
using System.Collections.Generic;
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
    internal static class DisplayCylinder
    {
        /// <summary>
        /// Number of sides to build geometry with.
        /// </summary>
        public static int NumSides = 24;


        public static DebugInfo GetDebugInfo(BEPUphysics.Entities.Entity DisplayedObject, Color color)
        {
            var cylinderShape = DisplayedObject.CollisionInformation.Shape as CylinderShape;
            if (cylinderShape == null)
                throw new ArgumentException("Wrong shape type.");

            float verticalOffset = cylinderShape.Height / 2;
            float angleBetweenFacets = MathHelper.TwoPi / NumSides;
            float radius = cylinderShape.Radius;

            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<short> indices = new List<short>();

            //Create the vertex list
            for (int i = 0; i < NumSides; i++)
            {
                float theta = i * angleBetweenFacets;
                float x = (float)Math.Cos(theta) * radius;
                float z = (float)Math.Sin(theta) * radius;
                //Top cap
                vertices.Add(new VertexPositionColor(new Vector3(x, verticalOffset, z), color));
                //Top part of body
                vertices.Add(new VertexPositionColor(new Vector3(x, verticalOffset, z), color));
                //Bottom part of body
                vertices.Add(new VertexPositionColor(new Vector3(x, -verticalOffset, z), color));
                //Bottom cap
                vertices.Add(new VertexPositionColor(new Vector3(x, -verticalOffset, z), color));
            }


            //Create the index list
            //The vertices are arranged a little nonintuitively.
            //0 is part of the top cap, 1 is the upper body, 2 is lower body, and 3 is bottom cap.
            for (short i = 0; i < vertices.Count; i += 4)
            {
                //Each iteration, the loop advances to the next vertex 'column.'
                //Four triangles per column (except for the four degenerate cap triangles).

                //Top cap triangles
                var nextIndex = (short)((i + 4) % vertices.Count);
                if (nextIndex != 0) //Don't add cap indices if it's going to be a degenerate triangle.
                {
                    indices.Add(i);
                    indices.Add(nextIndex);
                    indices.Add(0);
                }

                //Body triangles
                nextIndex = (short)((i + 5) % vertices.Count);
                indices.Add((short)(i + 1));
                indices.Add((short)(i + 2));
                indices.Add(nextIndex);

                indices.Add(nextIndex);
                indices.Add((short)(i + 2));
                indices.Add((short)((i + 6) % vertices.Count));

                //Bottom cap triangles.
                nextIndex = (short)((i + 7) % vertices.Count);
                if (nextIndex != 3) //Don't add cap indices if it's going to be a degenerate triangle.
                {
                    indices.Add((short)(i + 3));
                    indices.Add(3);
                    indices.Add(nextIndex);
                }
            }
            DebugInfo DebugInfo = new DebugInfo();
            DebugInfo.vertices = vertices.ToArray();
            DebugInfo.indices = indices.ToArray();
            return DebugInfo;
        }
    }
}