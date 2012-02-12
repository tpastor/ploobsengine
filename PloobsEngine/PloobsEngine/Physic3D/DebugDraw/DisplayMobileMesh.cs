using System.Collections.Generic;
using BEPUphysics.Collidables;
using BEPUphysics.MathExtensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BEPUphysics.CollisionShapes;
using BEPUphysics.Collidables.MobileCollidables;

namespace PloobsEngine.Physics
{
    /// <summary>
    /// Simple display object for triangles.
    /// </summary>
    internal class DisplayMobileMesh
    {
        public static DebugInfo GetDebugInfo(BEPUphysics.Entities.Entity DisplayedObject, Color color)
        {
            MobileMeshShape shape = DisplayedObject.CollisionInformation.Shape as MobileMeshShape;
            var tempVertices = new VertexPositionNormalTexture[shape.TriangleMesh.Data.Vertices.Length];
            for (int i = 0; i < shape.TriangleMesh.Data.Vertices.Length; i++)
            {
                Vector3 position;
                shape.TriangleMesh.Data.GetVertexPosition(i, out position);
                tempVertices[i] = new VertexPositionNormalTexture(
                    position,
                    Vector3.Zero, 
                    Vector2.Zero);
            }

            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<short> indices = new List<short>();

            for (int i = 0; i < shape.TriangleMesh.Data.Indices.Length; i++)
            {
                indices.Add((short)shape.TriangleMesh.Data.Indices[i]);
            }
            for (int i = 0; i < indices.Count; i += 3)
            {
                int a = indices[i];
                int b = indices[i + 1];
                int c = indices[i + 2];
                Vector3 normal = Vector3.Normalize(Vector3.Cross(
                    tempVertices[c].Position - tempVertices[a].Position,
                    tempVertices[b].Position - tempVertices[a].Position));
                tempVertices[a].Normal += normal;
                tempVertices[b].Normal += normal;
                tempVertices[c].Normal += normal;
            }

            for (int i = 0; i < tempVertices.Length; i++)
            {
                tempVertices[i].Normal.Normalize();
                
                vertices.Add(new VertexPositionColor(tempVertices[i].Position,color));
            }

            DebugInfo DebugInfo = new DebugInfo();
            DebugInfo.vertices = vertices.ToArray();
            DebugInfo.indices = indices.ToArray();
            return DebugInfo;
        }
        

    }
}