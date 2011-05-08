using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Batch Type
    /// </summary>
    public enum BatchType
    {
        /// <summary>
        /// Draw Indexed
        /// </summary>
        INDEXED,
        /// <summary>
        /// Draw without index
        /// </summary>
        NORMAL,

        /// <summary>
        /// Draw Instanced Geometry
        /// </summary>
        INSTANCED,
    }



    /// <summary>
    /// Batch Information
    /// Contains Draw data from a model (vertices, indexes) and info about how to draw them
    /// </summary>
    public class BatchInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchInformation"/> class.
        /// Indexed or Normal
        /// Use OTHER constructor to created INSTANCED
        /// </summary>
        /// <param name="BaseVertex">The base vertex.</param>
        /// <param name="NumVertices">The num vertices.</param>
        /// <param name="PrimitiveCount">The primitive count.</param>
        /// <param name="StartIndexOrVertex">The start index or vertex.</param>
        /// <param name="StreamOffset">The stream offset.</param>
        /// <param name="VertexDeclaration">The vertex declaration.</param>
        /// <param name="VertexStride">The vertex stride.</param>
        /// <param name="BatchType">Type of the batch.</param>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        public BatchInformation(int BaseVertex, int NumVertices, int PrimitiveCount, int StartIndexOrVertex, int StreamOffset, VertexDeclaration VertexDeclaration, int VertexStride, BatchType BatchType,PrimitiveType PrimitiveType = PrimitiveType.TriangleList)
        {
            this.BaseVertex = BaseVertex;
            this.NumVertices = NumVertices;
            this.PrimitiveCount = PrimitiveCount;            
            this.StreamOffset = StreamOffset;
            this.VertexDeclaration = VertexDeclaration;
            this.VertexStride = VertexStride;
            this.BatchType = BatchType;
            if (BatchType == BatchType.INDEXED)
            {
                this.StartIndex = StartIndexOrVertex;
            }
            else
            {
                this.StartVertex = StartIndexOrVertex;
            }
            this.PrimitiveType = PrimitiveType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchInformation"/> class.
        /// Indexed
        /// </summary>
        /// <param name="BaseVertex">The base vertex.</param>
        /// <param name="NumVertices">The num vertices.</param>
        /// <param name="PrimitiveCount">The primitive count.</param>
        /// <param name="StartIndex">The start index.</param>
        /// <param name="StreamOffset">The stream offset.</param>
        /// <param name="VertexDeclaration">The vertex declaration.</param>
        /// <param name="VertexStride">The vertex stride.</param>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        public BatchInformation(int BaseVertex, int NumVertices, int PrimitiveCount, int StartIndex, int StreamOffset, VertexDeclaration VertexDeclaration, int VertexStride, PrimitiveType PrimitiveType = Microsoft.Xna.Framework.Graphics.PrimitiveType.TriangleList)
        {
            this.BaseVertex = BaseVertex;
            this.NumVertices = NumVertices;
            this.PrimitiveCount = PrimitiveCount;
            this.StartIndex = StartIndex;
            this.StreamOffset = StreamOffset;
            this.VertexDeclaration = VertexDeclaration;
            this.VertexStride = VertexStride;
            this.PrimitiveType = PrimitiveType;
            this.BatchType = BatchType.INDEXED;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchInformation"/> class.
        /// INSTACED CONSTRUCTOR
        /// </summary>
        /// <param name="BaseVertex">The base vertex.</param>
        /// <param name="NumVertices">The num vertices.</param>
        /// <param name="PrimitiveCount">The primitive count.</param>
        /// <param name="StartIndex">The start index.</param>
        /// <param name="StreamOffset">The stream offset.</param>
        /// <param name="VertexDeclaration">The vertex declaration.</param>
        /// <param name="VertexStride">The vertex stride.</param>
        /// <param name="instanceCount">The instance count.</param>
        /// <param name="PrimitiveType">Type of the primitive.</param>
        public BatchInformation(int BaseVertex, int NumVertices, int PrimitiveCount, int StartIndex, int StreamOffset, VertexDeclaration VertexDeclaration, int VertexStride, int instanceCount ,PrimitiveType PrimitiveType = Microsoft.Xna.Framework.Graphics.PrimitiveType.TriangleList)
        {
            this.BaseVertex = BaseVertex;
            this.NumVertices = NumVertices;
            this.PrimitiveCount = PrimitiveCount;
            this.StartIndex = StartIndex;
            this.StreamOffset = StreamOffset;
            this.VertexDeclaration = VertexDeclaration;
            this.VertexStride = VertexStride;
            this.PrimitiveType = PrimitiveType;            
            this.InstanceCount = instanceCount;
            this.BatchType = BatchType.INSTANCED;
        }

        /// <summary>
        /// For Instancing, the number of objects
        /// </summary>
        public int InstanceCount;

        /// <summary>
        /// Primitive Type
        /// </summary>
        public PrimitiveType PrimitiveType; 

        /// <summary>
        /// Batch Type
        /// </summary>
        public BatchType BatchType;

        /// <summary>
        /// Base Verter
        /// </summary>
        public int BaseVertex;

        /// <summary>
        /// Num Vertices
        /// </summary>
        public int NumVertices;

        /// <summary>
        /// Primitive COunt
        /// </summary>
        public int PrimitiveCount;

        /// <summary>
        /// Start Index
        /// </summary>
        public int StartIndex;

        /// <summary>
        /// Start Vertex
        /// </summary>
        public int StartVertex;

        /// <summary>
        /// Stream Offset
        /// </summary>
        public int StreamOffset;

        /// <summary>
        /// Vertex Declaration
        /// </summary>
        public VertexDeclaration VertexDeclaration;

        /// <summary>
        /// VertexStride
        /// </summary>
        public int VertexStride;

        /// <summary>
        /// VertexBuffer
        /// </summary>
        public VertexBuffer VertexBuffer;

        /// <summary>
        /// VertexBuffer
        /// </summary>
        public VertexBuffer InstancedVertexBuffer = null;

        /// <summary>
        /// IndexBuffer
        /// </summary>
        public IndexBuffer IndexBuffer;

        /// <summary>
        /// Local Transformation
        /// </summary>
        public Matrix ModelLocalTransformation;
        
    }
}
