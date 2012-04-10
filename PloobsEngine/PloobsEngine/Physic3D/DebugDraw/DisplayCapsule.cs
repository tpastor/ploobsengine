using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics.CollisionShapes.ConvexShapes;
using BEPUphysics.CollisionShapes;
using BEPUphysics.Collidables.MobileCollidables;
using PloobsEngine.Physics;

namespace PloobsEngine.Physics
{
    /// <summary>
    /// Helper class that can create shape mesh data.
    /// </summary>
    internal static class DisplayCapsule
    {
        /// <summary>
        /// Number of sides to build geometry with.
        /// </summary>
        public static int NumSides = 24;


        public static DebugInfo GetDebugInfo(BEPUphysics.Entities.Entity DisplayedObject, Color color)
        {
            var capsuleShape = DisplayedObject.CollisionInformation.Shape as CapsuleShape;
            if (capsuleShape == null)
                throw new ArgumentException("Wrong shape type.");

            var n = new Vector3();
            var offset = new Vector3(0, capsuleShape.Length / 2, 0);
            float angleBetweenFacets = MathHelper.TwoPi / NumSides;
            float radius = capsuleShape.Radius;

            //Create the vertex list
            //Top
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<short> indices = new List<short>();
            vertices.Add(new VertexPositionColor(new Vector3(0, radius + capsuleShape.Length / 2, 0),color));
            //Upper hemisphere
            for (int i = 1; i <= NumSides / 4; i++)
            {
                float phi = MathHelper.PiOver2 - i * angleBetweenFacets;
                var sinPhi = (float)Math.Sin(phi);
                var cosPhi = (float)Math.Cos(phi);

                for (int j = 0; j < NumSides; j++)
                {
                    float theta = j * angleBetweenFacets;

                    n.X = (float)Math.Cos(theta) * cosPhi;
                    n.Y = sinPhi;
                    n.Z = (float)Math.Sin(theta) * cosPhi;

                    vertices.Add(new VertexPositionColor(n * radius + offset, color));
                }
            }
            //Lower hemisphere
            for (int i = NumSides / 4; i < NumSides / 2; i++)
            {
                float phi = MathHelper.PiOver2 - i * angleBetweenFacets;
                var sinPhi = (float)Math.Sin(phi);
                var cosPhi = (float)Math.Cos(phi);

                for (int j = 0; j < NumSides; j++)
                {
                    float theta = j * angleBetweenFacets;

                    n.X = (float)Math.Cos(theta) * cosPhi;
                    n.Y = sinPhi;
                    n.Z = (float)Math.Sin(theta) * cosPhi;

                    vertices.Add(new VertexPositionColor(n * radius - offset, color));
                }
            }
            //Bottom
            vertices.Add(new VertexPositionColor(new Vector3(0, -radius - capsuleShape.Length / 2, 0), color));


            //Create the index list
            for (int i = 0; i < NumSides; i++)
            {
                indices.Add((short)(vertices.Count - 1));
                indices.Add((short)(vertices.Count - 2 - i));
                indices.Add((short)(vertices.Count - 2 - (i + 1) % NumSides));
            }

            for (int i = 0; i < NumSides / 2 - 1; i++)
            {
                for (int j = 0; j < NumSides; j++)
                {
                    int nextColumn = (j + 1) % NumSides;

                    indices.Add((short)(i * NumSides + nextColumn + 1));
                    indices.Add((short)(i * NumSides + j + 1));
                    indices.Add((short)((i + 1) * NumSides + j + 1));

                    indices.Add((short)((i + 1) * NumSides + nextColumn + 1));
                    indices.Add((short)(i * NumSides + nextColumn + 1));
                    indices.Add((short)((i + 1) * NumSides + j + 1));
                }
            }

            for (int i = 0; i < NumSides; i++)
            {
                indices.Add(0);
                indices.Add((short)(i + 1));
                indices.Add((short)((i + 1) % NumSides + 1));
            }
            DebugInfo DebugInfo = new DebugInfo();
            DebugInfo.vertices = vertices.ToArray();
            DebugInfo.indices = indices.ToArray();
            return DebugInfo;
        }        
    }
}