using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsProjectTemplate
{
    public class VoxelTerrainModel : IModelo
    {
        int[] indices;
        VertexPositionTexture[] vertices;

        public VertexPositionTexture[] Vertices
        {
            get { return vertices; }
            set { vertices = value;
            vertexBufferS.SetData(value);
            }
        }

        public int[] Indices
        {
            get { return indices; }
            set { indices = value;
            indexBufferS.SetData(indices);
            }
        }
        string diffuseName;
        public VoxelTerrainModel(GraphicFactory factory, String modelName,String diffuseName,int[] indices, VertexPositionTexture[] VertexPositionTexture)
            : base(factory, modelName,false)
        {
            this.indices = indices;
            this.vertices = VertexPositionTexture;
            this.diffuseName = diffuseName;
            LoadModel(factory, out BatchInformations, out TextureInformations);
        }

        DynamicIndexBuffer indexBufferS;
        DynamicVertexBuffer vertexBufferS; 
        protected override void  LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformation)
         	
        {
            vertexBufferS = factory.CreateDynamicVertexBuffer(VertexPositionTexture.VertexDeclaration, vertices.Count(), BufferUsage.None);
            vertexBufferS.SetData(vertices);
            int noVertices = vertices.Count();
            int noTriangles = indices.Count() / 3;

            indexBufferS = factory.CreateDynamicIndexBuffer(IndexElementSize.ThirtyTwoBits, indices.Count(), BufferUsage.None);
            indexBufferS.SetData(indices);

            BatchInformations = new BatchInformation[1][];
            BatchInformation[] b = new BatchInformation[1];
            b[0] = new BatchInformation(0, noVertices, noTriangles, 0, 0, VertexPositionTexture.VertexDeclaration, VertexPositionTexture.VertexDeclaration.VertexStride, BatchType.INDEXED);
            b[0].VertexBuffer = vertexBufferS;
            b[0].IndexBuffer = indexBufferS;
            BatchInformations[0] = b;

            TextureInformation = new TextureInformation[1][];
            TextureInformation[0] = new TextureInformation[1];
            TextureInformation[0][0] = new TextureInformation(false, factory);
            TextureInformation[0][0].SetTexture(diffuseName, TextureType.DIFFUSE);
        }

        /// <summary>
        /// Calculate the radius of your mesh here =P
        /// i just put a random value ....
        /// </summary>
        /// <returns></returns>
        public override float GetModelRadius()
        {
            return 100;
        }

        /// <summary>
        /// one in your case
        /// </summary>
        public override int MeshNumber
        {
            get { return 1; }
        }
    }
}
