#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using PloobsEngine.Engine;

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
    /// The use should just use the Constructor to change Data, dont use the accessor public methods
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
            if (BatchType == BatchType.INSTANCED)
                throw new Exception("This constructor cannot be used to creat instanced BatchInformation, use the other one");

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
        public readonly PrimitiveType PrimitiveType; 

        /// <summary>
        /// Batch Type
        /// </summary>
        public readonly BatchType BatchType;

        /// <summary>
        /// Base Verter
        /// </summary>
        public readonly int BaseVertex;

        /// <summary>
        /// Num Vertices
        /// </summary>
        public  int NumVertices;

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
        public readonly int StreamOffset;

        /// <summary>
        /// Vertex Declaration
        /// </summary>
        public readonly VertexDeclaration VertexDeclaration;

        /// <summary>
        /// VertexStride
        /// </summary>
        public readonly int VertexStride;

        /// <summary>
        /// VertexBuffer
        /// </summary>
        public VertexBuffer VertexBuffer;

        /// <summary>
        /// Instanced VertexBuffer
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


        //[Conditional("DEBUG")]
        static void CheckBatchInformationsConsistency<T>(params BatchInformation[] ExtraBatchInformation)
            where T : IVertexType
        {
            System.Diagnostics.Debug.Assert(ExtraBatchInformation.Count() > 2);
            for (int i = 0; i < ExtraBatchInformation.Count(); i++)
            {
                System.Diagnostics.Debug.Assert(ExtraBatchInformation[i].BatchType == BatchType.INDEXED);
                System.Diagnostics.Debug.Assert(ExtraBatchInformation[i].VertexDeclaration.GetType() == typeof(T));
            }            
        }


        /// <summary>
        /// Merges some indexedbatchinformation objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="GraphicFactory">The graphic factory.</param>
        /// <param name="IndexElementSize">Size of the index element.</param>
        /// <param name="ExtraBatchInformation">The extra batch information.</param>
        public static void MergeIndexedBatchInformation<T>(GraphicFactory GraphicFactory, IndexElementSize IndexElementSize, params BatchInformation[] ExtraBatchInformation)
        where T : struct,IVertexType        
        {   
                CheckBatchInformationsConsistency<T>(ExtraBatchInformation);
                int NumVerts = 0;
                int NumIndexes = 0;
                for (int i = 0; i < ExtraBatchInformation.Count(); i++)
                {
                    NumVerts += ExtraBatchInformation[i].VertexBuffer.VertexCount;
                    NumIndexes += ExtraBatchInformation[i].IndexBuffer.IndexCount;
                }

                //complete
                int[] completeindex = new int[NumVerts];
                T[] completevertex = new T[NumIndexes];
                VertexDeclaration VertexDeclaration = ExtraBatchInformation[0].VertexDeclaration;


                //mandatory            
                int[] indexbuffer = new int[ExtraBatchInformation[0].IndexBuffer.IndexCount];
                switch (IndexElementSize)
                {
                    case IndexElementSize.SixteenBits:
                        short[] tempindex = new short[ExtraBatchInformation[0].IndexBuffer.IndexCount];
                        ExtraBatchInformation[0].IndexBuffer.GetData<short>(tempindex);
                        for (int i = 0; i < tempindex.Count(); i++)
                        {
                            indexbuffer[i] = tempindex[i];
                        }
                        break;
                    case IndexElementSize.ThirtyTwoBits:
                        ExtraBatchInformation[0].IndexBuffer.GetData<int>(indexbuffer);
                        break;
                    default:
                        break;
                }
        
                //mandatory
                T[] vertexBuffer = new T[ExtraBatchInformation[0].VertexBuffer.VertexCount];
                ExtraBatchInformation[0].VertexBuffer.GetData<T>(vertexBuffer);

                ExtraBatchInformation[0].VertexBuffer.Dispose();
                ExtraBatchInformation[0].IndexBuffer.Dispose();

                int VertexOffset = 0;
                int IndexOffset = 0;
                for (int i = 1; i < ExtraBatchInformation.Count(); i++)
                {
                    //extra vertex
                    T[] ExtravertexBuffer = new T[ExtraBatchInformation[i].VertexBuffer.VertexCount];
                    ExtraBatchInformation[i].VertexBuffer.GetData<T>(vertexBuffer);

                    //extra index
                    int[] ExtraindexBuffer = new int[ExtraBatchInformation[i].IndexBuffer.IndexCount];                    
                    switch (IndexElementSize)
                    {
                        case IndexElementSize.SixteenBits:
                            short[] tempindex = new short[ExtraBatchInformation[0].IndexBuffer.IndexCount];
                            ExtraBatchInformation[0].IndexBuffer.GetData<short>(tempindex);
                            for (int q = 0; q < ExtraindexBuffer.Count(); q++)
                            {
                                ExtraindexBuffer[q] = tempindex[q];
                            }
                            break;
                        case IndexElementSize.ThirtyTwoBits:
                            ExtraBatchInformation[i].IndexBuffer.GetData<int>(ExtraindexBuffer);
                            break;
                        default:
                            break;
                    }


                    for (int v = 0; v < ExtraBatchInformation[i].VertexBuffer.VertexCount; v++)
                    {                        
                        completevertex[VertexOffset + v] = ExtravertexBuffer[v];
                    }

                    for (int j = 0; j < ExtraBatchInformation[i].IndexBuffer.IndexCount; j++)
                    {
                        completeindex[IndexOffset+i] = ExtraindexBuffer[j] + VertexOffset;
                    }

                    ExtraBatchInformation[i].VertexBuffer.Dispose();
                    ExtraBatchInformation[i].IndexBuffer.Dispose();
                    ExtraBatchInformation[i].StartIndex = IndexOffset;
                    ExtraBatchInformation[i].StartVertex = VertexOffset;
                    VertexOffset += ExtravertexBuffer.Count();
                    IndexOffset += ExtraindexBuffer.Count();
                }                

                IndexBuffer IndexBuffer = GraphicFactory.CreateIndexBuffer(IndexElementSize.ThirtyTwoBits,NumIndexes,BufferUsage.None);
                VertexBuffer VertexBuffer = GraphicFactory.CreateVertexBuffer(VertexDeclaration,NumIndexes,BufferUsage.None);

                for (int i = 0; i < ExtraBatchInformation.Count(); i++)
                {
                    ExtraBatchInformation[i].IndexBuffer = IndexBuffer;
                    ExtraBatchInformation[i].VertexBuffer = VertexBuffer;
                }
        }
    }
}
