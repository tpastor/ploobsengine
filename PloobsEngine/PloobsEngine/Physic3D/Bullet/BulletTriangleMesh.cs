#if WINDOWS && !MONO && !MONODX
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulletSharp;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Physics
{
    public class BulletTriangleMesh : BulletPhysicObject
    {
        public BulletTriangleMesh(IModelo model, Vector3 pos, Matrix rotation, Vector3 scale)
        {
            TriangleMesh TriangleMesh = new BulletSharp.TriangleMesh(true,false);
            Vector3[] vertices = null;
            int[] indices = null;
            ExtractData(ref vertices, ref indices, model);                                    
            for (int i = 0; i < indices.Count(); i+=3)
            {

                TriangleMesh.AddTriangle(vertices[indices[i]], vertices[indices[i+1]], vertices[indices[i+2]]);
            }
            vertices = null;
            indices = null;     
                        
            BvhTriangleMeshShape = new BvhTriangleMeshShape(TriangleMesh, true, true);
            this.Shape = BvhTriangleMeshShape;
            
            this.Scale = scale;
            Shape.LocalScaling = scale;
            Object = LocalCreateRigidBody(0, Matrix.CreateTranslation(pos) * rotation, Shape);
        }

        BvhTriangleMeshShape BvhTriangleMeshShape;
        private void ExtractData(ref Vector3[] vert, ref int[] ind, IModelo model)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();

            for (int i = 0; i < model.MeshNumber; i++)
            {

                BatchInformation[] bi = model.GetBatchInformation(i);
                for (int j = 0; j < bi.Length; j++)
                {
                    BatchInformation info = bi[j];
                    int offset = vertices.Count;
                    Vector3[] a = new Vector3[info.NumVertices];

                    // Read the format of the vertex buffer  
                    VertexDeclaration declaration = bi[j].VertexBuffer.VertexDeclaration;
                    VertexElement[] vertexElements = declaration.GetVertexElements();
                    // Find the element that holds the position  
                    VertexElement vertexPosition = new VertexElement();
                    foreach (VertexElement elem in vertexElements)
                    {
                        if (elem.VertexElementUsage == VertexElementUsage.Position &&
                            elem.VertexElementFormat == VertexElementFormat.Vector3)
                        {
                            vertexPosition = elem;
                            // There should only be one  
                            break;
                        }
                    }
                    // Check the position element found is valid  
                    if (vertexPosition == null ||
                        vertexPosition.VertexElementUsage != VertexElementUsage.Position ||
                        vertexPosition.VertexElementFormat != VertexElementFormat.Vector3)
                    {
                        throw new Exception("Model uses unsupported vertex format!");
                    }
                    // This where we store the vertices until transformed                      
                    // Read the vertices from the buffer in to the array  
                    bi[j].VertexBuffer.GetData<Vector3>(
                        bi[j].BaseVertex * declaration.VertexStride + vertexPosition.Offset,
                        a,
                        0,
                        bi[j].NumVertices,
                        declaration.VertexStride);

                    for (int k = 0; k != a.Length; ++k)
                    {
                        Vector3.Transform(ref a[k], ref info.ModelLocalTransformation, out a[k]);
                    }
                    vertices.AddRange(a);

                    if (info.IndexBuffer.IndexElementSize != IndexElementSize.SixteenBits)
                    {
                        int[] s = new int[info.PrimitiveCount * 3];
                        info.IndexBuffer.GetData<int>(info.StartIndex * 2, s, 0, info.PrimitiveCount * 3);
                        for (int k = 0; k != info.PrimitiveCount; ++k)
                        {
                            indices.Add(s[k * 3 + 2] + offset);
                            indices.Add(s[k * 3 + 1] + offset);
                            indices.Add(s[k * 3 + 0] + offset);
                        }
                    }
                    else
                    {
                        short[] s = new short[info.PrimitiveCount * 3];
                        info.IndexBuffer.GetData<short>(info.StartIndex * 2, s, 0, info.PrimitiveCount * 3);
                        for (int k = 0; k != info.PrimitiveCount; ++k)
                        {
                            indices.Add(s[k * 3 + 2] + offset);
                            indices.Add(s[k * 3 + 1] + offset);
                            indices.Add(s[k * 3 + 0] + offset);
                        }
                    }
                }
            }

            ind = indices.ToArray();
            vert = vertices.ToArray();
        }

    }
}
#endif