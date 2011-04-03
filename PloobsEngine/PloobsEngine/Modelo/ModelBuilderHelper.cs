using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Helper to extract Information from XNA MOdels
    /// </summary>
    public class ModelBuilderHelper
    {
        /// <summary>
        /// Creaters a BATCHInformation from a xna Model
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="batchInformationS">The batch information S.</param>
        public static void Extract(Model model,out BatchInformation[][] batchInformationS)
        {
            batchInformationS = new BatchInformation[model.Meshes.Count][];
            Matrix[] boneabsolute = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneabsolute);
                

            for (int j = 0; j < model.Meshes.Count; j++)
            {
                ModelMesh mesh = model.Meshes[j];
                
                BatchInformation[] b = new BatchInformation[mesh.MeshParts.Count];
                for (int i = 0; i < mesh.MeshParts.Count; i++)
                {                 
                    b[i] = new BatchInformation(mesh.MeshParts[i].VertexOffset, mesh.MeshParts[i].NumVertices, mesh.MeshParts[i].PrimitiveCount, mesh.MeshParts[i].StartIndex, mesh.MeshParts[i].VertexOffset , mesh.MeshParts[i].VertexBuffer.VertexDeclaration, mesh.MeshParts[i].VertexBuffer.VertexDeclaration.VertexStride);
                    b[i].IndexBuffer = mesh.MeshParts[i].IndexBuffer;
                    b[i].VertexBuffer = mesh.MeshParts[i].VertexBuffer;
                    b[i].ModelLocalTransformation = boneabsolute[mesh.ParentBone.Index];
                }
                batchInformationS[j] = b;
            }
        }
        /// <summary>
        /// Creates a BatchInformation from a XNA ModelMesh
        /// </summary>
        /// <param name="BonesAbsoluteTransforms">The bones absolute transforms.</param>
        /// <param name="mesh">The mesh.</param>
        /// <param name="batchInformationS">The batch information S.</param>
        public static void Extract(Matrix[] BonesAbsoluteTransforms, ModelMesh mesh, out BatchInformation[][] batchInformationS)
        {            
                batchInformationS = new BatchInformation[1][];                                        
                
                BatchInformation[] b = new BatchInformation[mesh.MeshParts.Count];
                for (int i = 0; i < mesh.MeshParts.Count; i++)
                {                    
                    b[i] = new BatchInformation(mesh.MeshParts[i].VertexOffset, mesh.MeshParts[i].NumVertices, mesh.MeshParts[i].PrimitiveCount, mesh.MeshParts[i].StartIndex, mesh.MeshParts[i].VertexOffset, mesh.MeshParts[i].VertexBuffer.VertexDeclaration, mesh.MeshParts[i].VertexBuffer.VertexDeclaration.VertexStride);
                    b[i].IndexBuffer = mesh.MeshParts[i].IndexBuffer;
                    b[i].VertexBuffer = mesh.MeshParts[i].VertexBuffer;
                    b[i].ModelLocalTransformation = BonesAbsoluteTransforms[mesh.ParentBone.Index];
                }

                batchInformationS[0] = b;
        }


        /// <summary>
        /// Extracts the model mesh part data.
        /// </summary>
        /// <param name="meshPart">The mesh part.</param>
        /// <param name="transform">The transform.</param>
        /// <param name="vertices">The vertices.</param>
        /// <param name="indices">The indices.</param>
        public static void ExtractModelMeshPartData(ModelMeshPart meshPart, ref Matrix transform,
            List<Vector3> vertices, List<int> indices)
        {
            // Before we add any more where are we starting from  
            int offset = vertices.Count;

            // == Vertices (Changed for XNA 4.0)  

            // Read the format of the vertex buffer  
            VertexDeclaration declaration = meshPart.VertexBuffer.VertexDeclaration;
            VertexElement[] vertexElements = declaration.GetVertexElements();
            // Find the element that holds the position  
            VertexElement vertexPosition = new VertexElement();
            foreach (VertexElement vert in vertexElements)
            {
                if (vert.VertexElementUsage == VertexElementUsage.Position &&
                    vert.VertexElementFormat == VertexElementFormat.Vector3)
                {
                    vertexPosition = vert;
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
            Vector3[] allVertex = new Vector3[meshPart.NumVertices];
            // Read the vertices from the buffer in to the array  
            meshPart.VertexBuffer.GetData<Vector3>(
                meshPart.VertexOffset * declaration.VertexStride + vertexPosition.Offset,
                allVertex,
                0,
                meshPart.NumVertices,
                declaration.VertexStride);
            // Transform them based on the relative bone location and the world if provided  
            for (int i = 0; i != allVertex.Length; ++i)
            {
                Vector3.Transform(ref allVertex[i], ref transform, out allVertex[i]);
            }
            // Store the transformed vertices with those from all the other meshes in this model  
            vertices.AddRange(allVertex);

            // == Indices (Changed for XNA 4)  

            // Find out which vertices make up which triangles  
            if (meshPart.IndexBuffer.IndexElementSize != IndexElementSize.SixteenBits)
            {
                // This could probably be handled by using int in place of short but is unnecessary  
                throw new Exception("Model uses 32-bit indices, which are not supported.");
            }
            // Each primitive is a triangle  
            short[] indexElements = new short[meshPart.PrimitiveCount * 3];
            meshPart.IndexBuffer.GetData<short>(
                meshPart.StartIndex * 2,
                indexElements,
                0,
                meshPart.PrimitiveCount * 3);
            // Each TriangleVertexIndices holds the three indexes to each vertex that makes up a triangle  
            if (meshPart.IndexBuffer.IndexElementSize != IndexElementSize.SixteenBits)
            {
                int[] s = new int[meshPart.PrimitiveCount * 3];
                meshPart.IndexBuffer.GetData<int>(meshPart.StartIndex * 2, s, 0, meshPart.PrimitiveCount * 3);
                for (int k = 0; k != meshPart.PrimitiveCount; ++k)
                {
                    indices.Add(s[k * 3 + 2] + offset);
                    indices.Add(s[k * 3 + 1] + offset);
                    indices.Add(s[k * 3 + 0] + offset);
                }
            }
            else
            {
                short[] s = new short[meshPart.PrimitiveCount * 3];
                meshPart.IndexBuffer.GetData<short>(meshPart.StartIndex * 2, s, 0, meshPart.PrimitiveCount * 3);
                for (int k = 0; k != meshPart.PrimitiveCount; ++k)
                {
                    indices.Add(s[k * 3 + 2] + offset);
                    indices.Add(s[k * 3 + 1] + offset);
                    indices.Add(s[k * 3 + 0] + offset);
                }
            }                                    
        }  
    }
}
