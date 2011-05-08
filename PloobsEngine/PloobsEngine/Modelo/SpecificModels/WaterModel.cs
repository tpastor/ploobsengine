using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Modelo
{
    public class OceanModel : IModelo
    {
        public OceanModel(GraphicFactory factory,String WaterName,Vector3 basePosition, int width = 128, int height = 128)
            : base(factory, WaterName,null,null,null,null,false)
        {                        
            this.myWidth = width;
            this.myHeight = height;
            this.basePosition = basePosition;

            LoadBatchInfo(factory, out BatchInformations);
        }                
        
        private float modelRadius;
        int myWidth = 128;
        int myHeight = 128;
        Vector3 basePosition;

        protected override void LoadBatchInfo(GraphicFactory factory, out BatchInformation[][] BatchInformations)
        {
            Vector3 myPosition = new Vector3(basePosition.X - (myWidth / 2), basePosition.Y, basePosition.Z - (myHeight / 2));
            
            Vector3[] pos = new Vector3[myWidth * myHeight];
            // Vertices
            VertexMultitextured[] myVertices = new VertexMultitextured[myWidth * myHeight];

            for (int x = 0; x < myWidth; x++)
                for (int y = 0; y < myHeight; y++)
                {
                    myVertices[x + y * myWidth].Position = new Vector3(y, 0, x);
                    pos[x + y * myWidth] = new Vector3(y, 0, x); 
                    myVertices[x + y * myWidth].Normal = new Vector3(0, -1, 0);
                    myVertices[x + y * myWidth].TextureCoordinate.X = (float)x / 30.0f;
                    myVertices[x + y * myWidth].TextureCoordinate.Y = (float)y / 30.0f;                    
                }

            modelRadius = BoundingSphere.CreateFromPoints(pos).Radius;            

            // Calc Tangent and Bi Normals.
            for (int x = 0; x < myWidth; x++)
                for (int y = 0; y < myHeight; y++)
                {
                    // Tangent Data.
                    if (x != 0 && x < myWidth - 1)
                        myVertices[x + y * myWidth].Tangent = myVertices[x - 1 + y * myWidth].Position - myVertices[x + 1 + y * myWidth].Position;
                    else
                        if (x == 0)
                            myVertices[x + y * myWidth].Tangent = myVertices[x + y * myWidth].Position - myVertices[x + 1 + y * myWidth].Position;
                        else
                            myVertices[x + y * myWidth].Tangent = myVertices[x - 1 + y * myWidth].Position - myVertices[x + y * myWidth].Position;

                    // Bi Normal Data.
                    if (y != 0 && y < myHeight - 1)
                        myVertices[x + y * myWidth].BiNormal = myVertices[x + (y - 1) * myWidth].Position - myVertices[x + (y + 1) * myWidth].Position;
                    else
                        if (y == 0)
                            myVertices[x + y * myWidth].BiNormal = myVertices[x + y * myWidth].Position - myVertices[x + (y + 1) * myWidth].Position;
                        else
                            myVertices[x + y * myWidth].BiNormal = myVertices[x + (y - 1) * myWidth].Position - myVertices[x + y * myWidth].Position;
            }


            VertexDeclaration vd = new VertexDeclaration(VertexMultitextured.SizeInBytes,VertexMultitextured.VertexElements);
            VertexBuffer vb = factory.CreateVertexBuffer(vd, myWidth * myHeight, BufferUsage.WriteOnly);
            vb.SetData(myVertices);

            short[] terrainIndices = new short[(myWidth - 1) * (myHeight - 1) * 6];
            for (short x = 0; x < myWidth - 1; x++)
            {
                for (short y = 0; y < myHeight - 1; y++)
                {
                    terrainIndices[(x + y * (myWidth - 1)) * 6] = (short)((x + 1) + (y + 1) * myWidth);
                    terrainIndices[(x + y * (myWidth - 1)) * 6 + 1] = (short)((x + 1) + y * myWidth);
                    terrainIndices[(x + y * (myWidth - 1)) * 6 + 2] = (short)(x + y * myWidth);

                    terrainIndices[(x + y * (myWidth - 1)) * 6 + 3] = (short)((x + 1) + (y + 1) * myWidth);
                    terrainIndices[(x + y * (myWidth - 1)) * 6 + 4] = (short)(x + y * myWidth);
                    terrainIndices[(x + y * (myWidth - 1)) * 6 + 5] = (short)(x + (y + 1) * myWidth);
                }
            }

            IndexBuffer ib = factory.CreateIndexBuffer(IndexElementSize.SixteenBits, (myWidth - 1) * (myHeight - 1) * 6, BufferUsage.WriteOnly);
            ib.SetData(terrainIndices);

            BatchInformations = new BatchInformation[1][];
            BatchInformation[] b = new BatchInformation[1];
            b[0] = new BatchInformation(0,myVertices.Count(),terrainIndices.Count() /3,0,0,vd,VertexMultitextured.SizeInBytes);
            b[0].VertexBuffer = vb;
            b[0].IndexBuffer = ib;
            BatchInformations[0] = b;
        }

        public override int MeshNumber
        {
            get { return 1; }
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }

        public override BatchInformation[] GetBatchInformation(int meshNumber)
        {
            System.Diagnostics.Debug.Assert(meshNumber == 0);
            return BatchInformations[0];   
        }
    }

    /// <summary>
    /// Vertex Struct used in the Water shader
    /// </summary>
    public struct VertexMultitextured
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoordinate;
        public Vector3 Tangent;
        public Vector3 BiNormal;

        /// <summary>
        /// Size in bytes
        /// </summary>
        public static int SizeInBytes = (3 + 3 + 2 + 3 + 3) * 4;
        /// <summary>
        /// Vertex Elements
        /// </summary>
        public static VertexElement[] VertexElements = new VertexElement[]
             {
                 new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0 ),
                 new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0 ),
                 new VertexElement(sizeof(float) * 6, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0 ),
                 new VertexElement(sizeof(float) * 8, VertexElementFormat.Vector3, VertexElementUsage.Tangent, 0 ),
                 new VertexElement(sizeof(float) * 11, VertexElementFormat.Vector3,VertexElementUsage.Binormal, 0 ),
             };

    }

}
