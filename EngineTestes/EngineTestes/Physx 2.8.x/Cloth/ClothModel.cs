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
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using StillDesign.PhysX;
using StillDesign.PhysX.MathPrimitives;
using XNA = Microsoft.Xna.Framework;
using System.IO;
using PloobsEngine.Physics;

namespace PloobsEngine.Modelo
{    
    public class ClothModel : IModelo
    {
        public int VerticesNum
        {
            get;
            set;
        }

        public int IndicesNum
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleModel"/> class.
        /// </summary>
        /// <param name="factory">The graphic factory.</param>
        /// <param name="PhysxPhysicWorld">The physx physic world.</param>
        /// <param name="clothMeshDesc">The cloth mesh desc.</param>
        /// <param name="Points">The points.</param>
        /// <param name="TextCoords">The text coords.</param>
        /// <param name="Indices">The indices.</param>
        /// <param name="diffuseTextureName">Name of the diffuse texture.</param>
        public ClothModel(GraphicFactory factory, PhysxPhysicWorld PhysxPhysicWorld, ClothMeshDescription clothMeshDesc,  
            Vector3[] Points ,
            Vector2[] TextCoords ,
            int[] Indices ,
            String diffuseTextureName = null)
            : base(factory, "Cloth", false)
        {
            this._diffuseName = diffuseTextureName;            

            VerticesNum = Points.Length;
            IndicesNum = Indices.Length;
            
            clothMeshDesc.AllocateVertices<Vector3>(VerticesNum);
            clothMeshDesc.AllocateTriangles<int>(IndicesNum / 3);

            clothMeshDesc.VertexCount = VerticesNum;
            clothMeshDesc.TriangleCount = IndicesNum / 3;

            BatchInformation = new PloobsEngine.Modelo.BatchInformation(0, VerticesNum, IndicesNum / 3, 0, 0,
                VertexPositionNormalTexture.VertexDeclaration, VertexPositionNormalTexture.VertexDeclaration.VertexStride, PrimitiveType.TriangleList);
            BatchInformation.ModelLocalTransformation = XNA.Matrix.Identity;

            vertexPositionNormalTexture = new VertexPositionNormalTexture[VerticesNum];

            BatchInformation.VertexBuffer = factory.CreateDynamicVertexBuffer(VertexPositionNormalTexture.VertexDeclaration, VerticesNum + (int)(1.2 * VerticesNum), BufferUsage.WriteOnly);
            BatchInformation.IndexBuffer = factory.CreateDynamicIndexBuffer(IndexElementSize.ThirtyTwoBits, IndicesNum + (int)(1.2 * IndicesNum), BufferUsage.WriteOnly);

            BatchInformation.IndexBuffer.SetData<int>(Indices);
            clothMeshDesc.VerticesStream.SetData(Points);
            clothMeshDesc.TriangleStream.SetData(Indices);


            XNA.Vector3[] pts = new XNA.Vector3[BatchInformation.NumVertices];
            for (int i = 0; i < BatchInformation.NumVertices; i++)
            {
                vertexPositionNormalTexture[i].TextureCoordinate = TextCoords[i].AsXNA();
                vertexPositionNormalTexture[i].Position = Points[i].AsXNA();
                pts[i] = vertexPositionNormalTexture[i].Position;
            }
                        
            modelRadius = Microsoft.Xna.Framework.BoundingSphere.CreateFromPoints(pts).Radius;
            pts = null;


            // We are using 32 bit integers for our indices, so make sure the 16 bit flag is removed.
            // 32 bits are the default, so this isn't technically needed, but it's good to show in a sample
            clothMeshDesc.Flags &= ~MeshFlag.Indices16Bit;
            //clothMeshDesc.Flags |= (MeshFlag)((int)clothMeshDesc.Flags | (int)ClothMeshFlag.Tearable);

            // Write the cooked data to memory
            using (var memoryStream = new MemoryStream())
            {
                Cooking.InitializeCooking();
                Cooking.CookClothMesh(clothMeshDesc, memoryStream);
                Cooking.CloseCooking();

                // Need to reset the position of the stream to the beginning
                memoryStream.Position = 0;

                ClothMesh = PhysxPhysicWorld.Core.CreateClothMesh(memoryStream);
            }

            
            LoadModel(factory, out BatchInformations, out TextureInformations);
        }



        public ClothModel(GraphicFactory factory, PhysxPhysicWorld PhysxPhysicWorld, ClothMeshDescription clothMeshDesc,
            String ModelName,XNA.Vector3 scale,
            String diffuseTextureName = null)
            : base(factory, ModelName, false)
        {
            this._diffuseName = diffuseTextureName;
            Model model = factory.GetModel(ModelName);
            SimpleModel SimpleModel = new Modelo.SimpleModel(factory, ModelName,null,null,null,null,false);

            Vector3[] verts = null;
            Vector2[] tex = null;
            int[] inds = null;
            ExtractData(ref verts, ref inds, ref tex, SimpleModel,scale);
            SimpleModel.CleanUp(factory);

            VerticesNum = verts.Length;
            IndicesNum = inds.Length;

            clothMeshDesc.AllocateVertices<Vector3>(VerticesNum);
            clothMeshDesc.AllocateTriangles<int>(IndicesNum / 3);

            clothMeshDesc.VertexCount = VerticesNum;
            clothMeshDesc.TriangleCount = IndicesNum / 3;            

            BatchInformation = new PloobsEngine.Modelo.BatchInformation(0, VerticesNum, IndicesNum / 3, 0, 0,
                VertexPositionNormalTexture.VertexDeclaration, VertexPositionNormalTexture.VertexDeclaration.VertexStride, PrimitiveType.TriangleList);
            BatchInformation.ModelLocalTransformation = XNA.Matrix.Identity;

            vertexPositionNormalTexture = new VertexPositionNormalTexture[VerticesNum];

            BatchInformation.VertexBuffer = factory.CreateDynamicVertexBuffer(VertexPositionNormalTexture.VertexDeclaration, VerticesNum + (int)(1.2 * VerticesNum), BufferUsage.WriteOnly);
            BatchInformation.IndexBuffer = factory.CreateDynamicIndexBuffer(IndexElementSize.ThirtyTwoBits, IndicesNum + (int)(1.2 * IndicesNum), BufferUsage.WriteOnly);

            BatchInformation.IndexBuffer.SetData<int>(inds);
            clothMeshDesc.VerticesStream.SetData(verts);
            clothMeshDesc.TriangleStream.SetData(inds);


            XNA.Vector3[] pts = new XNA.Vector3[BatchInformation.NumVertices];
            for (int i = 0; i < BatchInformation.NumVertices; i++)
            {
                vertexPositionNormalTexture[i].TextureCoordinate = tex[i].AsXNA();
                vertexPositionNormalTexture[i].Position = verts[i].AsXNA();
                pts[i] = vertexPositionNormalTexture[i].Position;
            }

            modelRadius = Microsoft.Xna.Framework.BoundingSphere.CreateFromPoints(pts).Radius;
            pts = null;


            // We are using 32 bit integers for our indices, so make sure the 16 bit flag is removed.
            // 32 bits are the default, so this isn't technically needed, but it's good to show in a sample
            clothMeshDesc.Flags &= ~MeshFlag.Indices16Bit;
            //clothMeshDesc.Flags |= (MeshFlag)((int)clothMeshDesc.Flags | (int)ClothMeshFlag.Tearable);

            // Write the cooked data to memory
            using (var memoryStream = new MemoryStream())
            {
                Cooking.InitializeCooking();
                Cooking.CookClothMesh(clothMeshDesc, memoryStream);
                Cooking.CloseCooking();

                // Need to reset the position of the stream to the beginning
                memoryStream.Position = 0;

                ClothMesh = PhysxPhysicWorld.Core.CreateClothMesh(memoryStream);
            }


            LoadModel(factory, out BatchInformations, out TextureInformations);
        }

        public ClothModel(GraphicFactory factory, PhysxPhysicWorld PhysxPhysicWorld, ClothMeshDescription clothMeshDesc,
            XNA.Vector3[] Points,
            XNA.Vector2[] TextCoords,
            int[] Indices,
            String diffuseTextureName = null)
            : base(factory, "Cloth", false)
        {
            this._diffuseName = diffuseTextureName;

            VerticesNum = Points.Length;
            IndicesNum = Indices.Length;

            clothMeshDesc.AllocateVertices<Vector3>(VerticesNum);
            clothMeshDesc.AllocateTriangles<int>(IndicesNum / 3);
                        
            clothMeshDesc.VertexCount = VerticesNum;
            clothMeshDesc.TriangleCount = IndicesNum / 3;

            BatchInformation = new PloobsEngine.Modelo.BatchInformation(0, VerticesNum, IndicesNum / 3, 0, 0,
                VertexPositionNormalTexture.VertexDeclaration, VertexPositionNormalTexture.VertexDeclaration.VertexStride, PrimitiveType.TriangleList);
            BatchInformation.ModelLocalTransformation = XNA.Matrix.Identity;

            vertexPositionNormalTexture = new VertexPositionNormalTexture[VerticesNum];

            BatchInformation.VertexBuffer = factory.CreateDynamicVertexBuffer(VertexPositionNormalTexture.VertexDeclaration, VerticesNum + (int)(1.2 * VerticesNum), BufferUsage.WriteOnly);
            BatchInformation.IndexBuffer = factory.CreateDynamicIndexBuffer(IndexElementSize.ThirtyTwoBits, IndicesNum + (int)(1.2 * IndicesNum), BufferUsage.WriteOnly);

            BatchInformation.IndexBuffer.SetData<int>(Indices);
            clothMeshDesc.VerticesStream.SetData(Points);
            clothMeshDesc.TriangleStream.SetData(Indices);

            for (int i = 0; i < BatchInformation.NumVertices; i++)
            {
                vertexPositionNormalTexture[i].TextureCoordinate = TextCoords[i];
                vertexPositionNormalTexture[i].Position = Points[i];
            }



            // We are using 32 bit integers for our indices, so make sure the 16 bit flag is removed.
            // 32 bits are the default, so this isn't technically needed, but it's good to show in a sample
            clothMeshDesc.Flags &= ~MeshFlag.Indices16Bit;
            //clothMeshDesc.Flags |= (MeshFlag)((int)clothMeshDesc.Flags | (int)ClothMeshFlag.Tearable);

            // Write the cooked data to memory
            using (var memoryStream = new MemoryStream())
            {
                Cooking.InitializeCooking();
                Cooking.CookClothMesh(clothMeshDesc, memoryStream);
                Cooking.CloseCooking();

                // Need to reset the position of the stream to the beginning
                memoryStream.Position = 0;

                ClothMesh = PhysxPhysicWorld.Core.CreateClothMesh(memoryStream);
            }

            modelRadius = Microsoft.Xna.Framework.BoundingSphere.CreateFromPoints(Points).Radius; 
            LoadModel(factory, out BatchInformations, out TextureInformations);
                       
        }


        public ClothMesh ClothMesh
        {
            get;
            set;
        }

        string _diffuseName = null;
        private float modelRadius;
        public BatchInformation BatchInformation
        {
            set;
            get;
        }

        public VertexPositionNormalTexture[] vertexPositionNormalTexture;
        
        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformation)
        {                        
            BatchInformations = new BatchInformation[1][];
            BatchInformations[0] = new BatchInformation[1];
            BatchInformations[0][0] = BatchInformation;

            TextureInformation = new TextureInformation[1][];
            TextureInformation[0] = new TextureInformation[1];
            TextureInformation[0][0] = new TextureInformation(false,factory,_diffuseName);
            TextureInformation[0][0].LoadTexture();
        }

        public override int MeshNumber
        {
            get { return 1; }
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }

        private static void ExtractData(ref Vector3[] vert, ref int[] ind, ref Vector2[] tex, IModelo model, XNA.Vector3 scale)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> texcoords = new List<Vector2>();
            List<int> indices = new List<int>();

            for (int i = 0; i < model.MeshNumber; i++)
            {

                BatchInformation[] bi = model.GetBatchInformation(i);
                for (int j = 0; j < bi.Length; j++)
                {
                    BatchInformation info = bi[j];
                    int offset = vertices.Count;
                    Microsoft.Xna.Framework.Vector3[] a = new Microsoft.Xna.Framework.Vector3[info.NumVertices];

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
                    bi[j].VertexBuffer.GetData<Microsoft.Xna.Framework.Vector3>(
                        bi[j].BaseVertex * declaration.VertexStride + vertexPosition.Offset,
                        a,
                        0,
                        bi[j].NumVertices,
                        declaration.VertexStride);

                    for (int k = 0; k != a.Length; ++k)
                    {
                        XNA.Matrix tra = info.ModelLocalTransformation * XNA.Matrix.CreateScale(scale);
                        Microsoft.Xna.Framework.Vector3.Transform(ref a[k], ref tra, out a[k]);
                        vertices.Add(a[k].AsPhysX());
                    }                    

                    foreach (VertexElement elem in vertexElements)
                    {
                        if (elem.VertexElementUsage == VertexElementUsage.TextureCoordinate &&
                            elem.VertexElementFormat == VertexElementFormat.Vector2)
                        {
                            vertexPosition = elem;
                            // There should only be one  
                            break;
                        }
                    }
                    Microsoft.Xna.Framework.Vector2[] b = new Microsoft.Xna.Framework.Vector2[info.NumVertices];
                    // This where we store the vertices until transformed                      
                    // Read the vertices from the buffer in to the array  
                    bi[j].VertexBuffer.GetData<Microsoft.Xna.Framework.Vector2>(
                        bi[j].BaseVertex * declaration.VertexStride + vertexPosition.Offset,
                        b,
                        0,
                        bi[j].NumVertices,
                        declaration.VertexStride);

                    for (int k = 0; k != b.Length; ++k)
                    {                        
                        texcoords.Add(b[k].AsPhysX());                        
                    }                    

                    
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
            tex =  texcoords.ToArray();
        }

        public override void CleanUp(GraphicFactory factory)
        {
            base.CleanUp(factory);
        }
    }
}
