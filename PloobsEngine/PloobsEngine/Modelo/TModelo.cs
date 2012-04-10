using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Modelo
{
    public class TModelo<T> : IModelo where T : struct,IVertexType
    {
        public TModelo(GraphicFactory factory, T[] vertices, int[] indices, Matrix transformation, String diffuseTextureName = null, float radius = -1, bool isDynamic = false, float dynamicMultiplier = 1, BufferUsage BufferUsage = BufferUsage.WriteOnly)            
        {
            this.BufferUsage = BufferUsage;
            this.radius = -1;
            this.vertices = vertices;
            this.indices = indices;
            this.transformation = transformation;
            this.diffuseTextureName = diffuseTextureName;
            this.isDynamic = isDynamic;
            this.dynamicMultiplier = dynamicMultiplier;
            LoadModel(factory, out BatchInformations, out TextureInformations);
        }
        Matrix transformation;
        String diffuseTextureName;
        float dynamicMultiplier;
        bool isDynamic;
        BufferUsage BufferUsage;

        public void SetVertices(T[] vertices)
        {
            if (isDynamic)
            {
                BatchInformations[0][0].VertexBuffer.SetData<T>(vertices);
            }
            else
            {
                ActiveLogger.LogMessage("Cant set indices/vertices on Non Dynamic Modelo", LogLevel.RecoverableError);
            }
        }

        public void SetIndices(int[] indices)
        {
            if (isDynamic)
            {
                BatchInformations[0][0].IndexBuffer.SetData<int>(indices);
            }
            else
            {
                ActiveLogger.LogMessage("Cant set indices/vertices on Non Dynamic Modelo", LogLevel.RecoverableError);
            }
        }

        T[] vertices;
        int[] indices;
        float radius;

        public T[] Vertices
        {
            get { return vertices; }
        }        

        public int[] Indices
        {
            get { return indices; }
        }        

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformation)
        {
            int vertCount = vertices.Count();
            int indexCount = 0;
            if(indices != null)
                indexCount = indices.Count();
            int noVertices = vertCount;
            int noTriangles = 0;
            if (indices != null)
            {
                noTriangles = indexCount / 3;
            }
            else
            {
                noTriangles = vertCount/ 3;
            }

            VertexBuffer vertexBufferS;
            IndexBuffer IndexBufferS = null;
            if (isDynamic)
            {
                vertexBufferS = factory.CreateDynamicVertexBuffer(vertices[0].VertexDeclaration, (int)(vertCount * dynamicMultiplier), BufferUsage);
                if(indices!=null)
                    IndexBufferS = factory.CreateDynamicIndexBuffer(IndexElementSize.ThirtyTwoBits, (int)(indexCount * dynamicMultiplier), BufferUsage);
            }
            else
            {
                vertexBufferS = factory.CreateVertexBuffer(vertices[0].VertexDeclaration, vertCount, BufferUsage);
                if (indices != null)
                    IndexBufferS = factory.CreateIndexBuffer(IndexElementSize.ThirtyTwoBits, indexCount, BufferUsage);
            }

            vertexBufferS.SetData<T>(vertices);
            if (indices != null)
                IndexBufferS.SetData<int>(indices);

            BatchInformations = new BatchInformation[1][];
            BatchInformation[] b = new BatchInformation[1];
            b[0] = new BatchInformation(0, vertCount, noTriangles, 0, 0, vertices[0].VertexDeclaration, vertices[0].VertexDeclaration.VertexStride, 
                indices!= null ? BatchType.INDEXED : BatchType.NORMAL);
            b[0].ModelLocalTransformation = transformation;
            b[0].VertexBuffer = vertexBufferS;
            if (indices != null)
                b[0].IndexBuffer = IndexBufferS;
            BatchInformations[0] = b;
            
            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[1];

            TextureInformations[0][0] = new TextureInformation(isInternal, factory, diffuseTextureName, null, null, null);
            TextureInformations[0][0].LoadTexture();
            TextureInformation = TextureInformations;

            if (!isDynamic)
            {
                vertices = null;
                indices = null;
            }
        }

        public override int MeshNumber
        {
            get { return 1; }
        }

        public override float GetModelRadius()
        {
            return radius;
        }
    }
}
