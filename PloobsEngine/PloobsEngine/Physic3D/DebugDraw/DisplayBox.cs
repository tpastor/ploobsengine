using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics.CollisionShapes.ConvexShapes;
using System;
using BEPUphysics.CollisionShapes;
using BEPUphysics.Collidables.MobileCollidables;
using BEPUphysics.MathExtensions;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Physics
{
    /// <summary>
    /// Helper class that can create shape mesh data.
    /// </summary>
    internal static class DisplayBox
    {
        public static DebugInfo GetDebugInfo(BEPUphysics.Entities.Entity DisplayedObject, Color color)
        {
            var boxShape = DisplayedObject.CollisionInformation.Shape as BoxShape;


            var boundingBox = new BoundingBox(
                new Vector3(-boxShape.HalfWidth,
                            -boxShape.HalfHeight,
                            -boxShape.HalfLength),
                new Vector3(boxShape.HalfWidth,
                            boxShape.HalfHeight,
                            boxShape.HalfLength));


            Vector3[] corners = boundingBox.GetCorners();


            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<short> indices = new List<short>();

            vertices.Add(new VertexPositionColor(corners[0], color));
            vertices.Add(new VertexPositionColor(corners[1], color));
            vertices.Add(new VertexPositionColor(corners[2], color));
            vertices.Add(new VertexPositionColor(corners[3], color));
            indices.Add(0);
            indices.Add(1);
            indices.Add(2);
            indices.Add(0);
            indices.Add(2);
            indices.Add(3);

            vertices.Add(new VertexPositionColor(corners[1], color));
            vertices.Add(new VertexPositionColor(corners[2], color));
            vertices.Add(new VertexPositionColor(corners[5], color));
            vertices.Add(new VertexPositionColor(corners[6], color));
            indices.Add(4);
            indices.Add(6);
            indices.Add(7);
            indices.Add(4);
            indices.Add(7);
            indices.Add(5);

            vertices.Add(new VertexPositionColor(corners[4], color));
            vertices.Add(new VertexPositionColor(corners[5], color));
            vertices.Add(new VertexPositionColor(corners[6], color));
            vertices.Add(new VertexPositionColor(corners[7], color));
            indices.Add(9);
            indices.Add(8);
            indices.Add(11);
            indices.Add(9);
            indices.Add(11);
            indices.Add(10);

            vertices.Add(new VertexPositionColor(corners[0], color));
            vertices.Add(new VertexPositionColor(corners[3], color));
            vertices.Add(new VertexPositionColor(corners[4], color));
            vertices.Add(new VertexPositionColor(corners[7], color));
            indices.Add(14);
            indices.Add(12);
            indices.Add(13);
            indices.Add(14);
            indices.Add(13);
            indices.Add(15);

            vertices.Add(new VertexPositionColor(corners[0], color));
            vertices.Add(new VertexPositionColor(corners[1], color));
            vertices.Add(new VertexPositionColor(corners[4], color));
            vertices.Add(new VertexPositionColor(corners[5], color));
            indices.Add(16);
            indices.Add(19);
            indices.Add(17);
            indices.Add(16);
            indices.Add(18);
            indices.Add(19);

            vertices.Add(new VertexPositionColor(corners[2], color));
            vertices.Add(new VertexPositionColor(corners[3], color));
            vertices.Add(new VertexPositionColor(corners[6], color));
            vertices.Add(new VertexPositionColor(corners[7], color));
            indices.Add(21);
            indices.Add(20);
            indices.Add(22);
            indices.Add(21);
            indices.Add(22);
            indices.Add(23);

            DebugInfo DebugInfo = new DebugInfo();
            DebugInfo.vertices = vertices.ToArray();
            DebugInfo.indices = indices.ToArray();
            return DebugInfo;
        }
    }
}