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
    /// <summary>
    /// Model Constructed by adding vertices / indices
    /// Cant be change after building it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TConstructiveModelo<T> : IModelo where T : struct,IVertexType
    {
        public TConstructiveModelo(GraphicFactory factory,Matrix transformation, String diffuseTextureName = null, float radius = -1, bool useIndices = true, BufferUsage BufferUsage = BufferUsage.WriteOnly)            
        {
            this.factory = factory;
            this.BufferUsage = BufferUsage;
            this.radius = radius;
            this.transformation = transformation;
            this.diffuseTextureName = diffuseTextureName;
            this.useIndices  = useIndices ;
            vertices = new List<T>();
            if (useIndices)
                indices = new List<int>();
        }

        bool useIndices = true;
        Matrix transformation;
        String diffuseTextureName;
        BufferUsage BufferUsage;

        public void AddTriangle(int x, int y, int z)
        {
            if (added)
                ActiveLogger.LogMessage("cant change the model after build it",LogLevel.RecoverableError);
            else
            {
                indices.Add(x);
                indices.Add(y);
                indices.Add(z);
            }
        }

        public void AddVertex(T vertex)
        {
            if (added)
                ActiveLogger.LogMessage("cant change the model after build it", LogLevel.RecoverableError);
            else
            {
                vertices.Add(vertex);
            }
        }

        List<T> vertices;

        public List<T> Vertices
        {
            get { return vertices; }            
        }
        List<int> indices;

        public  List<int> Indices
        {
            get { return indices; }
        }
        float radius;
        bool added = false;

        public void BuildModel()
        {
            added = true;
            int vertCount = vertices.Count();
            int indexCount = 0;
            if (useIndices)
                indexCount = indices.Count();
            int noVertices = vertCount;
            int noTriangles = 0;
            if (useIndices)
            {
                noTriangles = indexCount / 3;
            }
            else
            {
                noTriangles = vertCount / 3;
            }

            VertexBuffer vertexBufferS;
            IndexBuffer IndexBufferS = null;
            vertexBufferS = factory.CreateVertexBuffer(vertices[0].VertexDeclaration, vertCount, BufferUsage);
            vertexBufferS.SetData<T>(vertices.ToArray());

            if (useIndices)
            {
                IndexBufferS = factory.CreateIndexBuffer(IndexElementSize.ThirtyTwoBits, indexCount, BufferUsage);
                IndexBufferS.SetData<int>(indices.ToArray());
            }

            BatchInformations = new BatchInformation[1][];
            BatchInformation[] b = new BatchInformation[1];
            b[0] = new BatchInformation(0, vertCount, noTriangles, 0, 0, vertices[0].VertexDeclaration, vertices[0].VertexDeclaration.VertexStride,
                useIndices == true ? BatchType.INDEXED : BatchType.NORMAL);
            b[0].ModelLocalTransformation = transformation;
            b[0].VertexBuffer = vertexBufferS;
            if (useIndices)
                b[0].IndexBuffer = IndexBufferS;
            BatchInformations[0] = b;

            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[1];

            TextureInformations[0][0] = new TextureInformation(isInternal, factory, diffuseTextureName, null, null, null);
            TextureInformations[0][0].LoadTexture();
            TextureInformations = TextureInformations;


            vertices = null;
            indices = null;
        }

        protected override void AfterAdded(SceneControl.IObject obj)
        {
            if (added == false)
                BuildModel();
            base.AfterAdded(obj);
        }
        
        public override int MeshNumber
        {
            get { return 1; }
        }

        public override float GetModelRadius()
        {
            return radius;
        }

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformation)
        {
            BatchInformations = null;
            TextureInformation = null;
        }
    }
}
