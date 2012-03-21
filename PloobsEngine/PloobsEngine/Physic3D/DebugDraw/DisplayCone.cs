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
    internal class DisplayCone
    {
        /// <summary>
        /// Number of sides to build geometry with.
        /// </summary>
        public static int NumSides = 24;


        public static DebugInfo GetDebugInfo(BEPUphysics.Entities.Entity DisplayedObject, Color color)
        {
            ConeShape coneShape = DisplayedObject.CollisionInformation.Shape as ConeShape;
            if (coneShape == null)
                throw new ArgumentException("Wrong shape type.");

            float verticalOffset = -coneShape.Height / 4;
            float angleBetweenFacets = MathHelper.TwoPi / NumSides;
            float radius = coneShape.Radius;

            //Create the vertex list

            var topVertexPosition = new Vector3(0, coneShape.Height + verticalOffset, 0);
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<short> indices = new List<short>();

            for (int i = 0; i < NumSides; i++)
            {
                float theta = i * angleBetweenFacets;
                var position = new Vector3((float)Math.Cos(theta) * radius, verticalOffset, (float)Math.Sin(theta) * radius);
                Vector3 offset = topVertexPosition - position;
                Vector3 normal = Vector3.Normalize(Vector3.Cross(Vector3.Cross(offset, Vector3.Up), offset));
                //Top vertex
                vertices.Add(new VertexPositionColor(topVertexPosition, color));
                //Sloped vertices
                vertices.Add(new VertexPositionColor(position, color));
                //Bottom vertices
                vertices.Add(new VertexPositionColor(position, color));
            }


            //Create the index list
            for (short i = 0; i < vertices.Count; i += 3)
            {
                //Each iteration, the loop advances to the next vertex 'column.'
                //Four triangles per column (except for the four degenerate cap triangles).

                //Sloped Triangles
                indices.Add(i);
                indices.Add((short)(i + 1));
                indices.Add((short)((i + 4) % vertices.Count));

                //Bottom cap triangles.
                var nextIndex = (short)((i + 5) % vertices.Count);
                if (nextIndex != 2) //Don't add cap indices if it's going to be a degenerate triangle.
                {
                    indices.Add((short)(i + 2));
                    indices.Add(2);
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