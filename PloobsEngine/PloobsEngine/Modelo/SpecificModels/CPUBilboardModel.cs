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
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Cameras;

namespace PloobsEngine.Modelo
{
    public class CPUBilboardModel : IModelo
    {
        public CPUBilboardModel(GraphicFactory factory, String BilboardsName, String diffuseTextureName, List<Vector3> positions, Vector2 ScaleTexture)
            : base(factory, BilboardsName,false)
        {

            System.Diagnostics.Debug.Assert(positions != null && positions.Count != 0);
            this.positions = positions;
            this.diffuseTextureName = diffuseTextureName;
            LoadModelo(factory);
            this.Scaletexture = ScaleTexture;
        }

        private Vector2 Scaletexture;
        string diffuseTextureName;
        private float modelRadius = 0;
        List<Vector3> positions;

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformations)
        {
            int vertCount = positions.Count * 4;
            int indexCount = positions.Count * 6;
            int noVertices = vertCount;
            int noTriangles = indexCount / 3;

            VertexBuffer vertexBufferS = factory.CreateDynamicVertexBuffer(VertexPositionTexture.VertexDeclaration, vertCount, BufferUsage.WriteOnly);
            IndexBuffer IndexBufferS = factory.CreateDynamicIndexBuffer(IndexElementSize.SixteenBits, indexCount, BufferUsage.WriteOnly);

            BatchInformations = new BatchInformation[1][];
            BatchInformation[] b = new BatchInformation[1];
            b[0] = new BatchInformation(0, vertCount, noTriangles, 0, 0, VertexPositionTexture.VertexDeclaration, VertexPositionTexture.VertexDeclaration.VertexStride, BatchType.INDEXED);
            b[0].ModelLocalTransformation = Matrix.Identity;
            b[0].VertexBuffer = vertexBufferS;
            b[0].IndexBuffer = IndexBufferS;
            BatchInformations[0] = b;

            TextureInformations = new TextureInformation[1][];
            TextureInformations[0] = new TextureInformation[1];
            
            TextureInformations[0][0] = new TextureInformation(isInternal, factory, diffuseTextureName, null, null, null);
            TextureInformations[0][0].LoadTexture();
        }

        public override void Update(GameTime gameTime, IWorld world)
        {
            ICamera cam = world.CameraManager.ActiveCamera;
            Texture2D tex= TextureInformations[0][0].getTexture(TextureType.DIFFUSE);
            // Create billboard vertices.
            VertexPositionTexture[] vertices = new VertexPositionTexture[positions.Count * 4];
            int index = 0;

            float width = tex.Width * Scaletexture.X;
            float height = tex.Height* Scaletexture.Y;
            
            foreach (var item in positions)
	        {
	                
                    Matrix billboard = Matrix.CreateConstrainedBillboard(
                      item, cam.Position, Vector3.Up, cam.Target - cam.Position, null);

                    vertices[index].Position =
                      Vector3.Transform(new Vector3(width, height, 0), billboard);
                    vertices[index++].TextureCoordinate = new Vector2(0, 0);

                    vertices[index].Position =
                      Vector3.Transform(new Vector3(-width, height, 0), billboard);
                    vertices[index++].TextureCoordinate = new Vector2(1, 0);

                    vertices[index].Position =
                      Vector3.Transform(new Vector3(-width, -height, 0), billboard);
                    vertices[index++].TextureCoordinate = new Vector2(1, 1);

                    vertices[index].Position =
                      Vector3.Transform(new Vector3(width, -height, 0), billboard);
                    vertices[index++].TextureCoordinate = new Vector2(0, 1);
            }

            // Create billboard indices.
            short[] indices = new short[positions.Count  * 6];
            short currentVertex = 0;
            index = 0;

            while (index < indices.Length)
            {
                indices[index++] = currentVertex;
                indices[index++] = (short)(currentVertex + 1);
                indices[index++] = (short)(currentVertex + 2);

                indices[index++] = currentVertex;
                indices[index++] = (short)(currentVertex + 2);
                indices[index++] = (short)(currentVertex + 3);

                currentVertex += 4;
            }

            BatchInformations[0][0].VertexBuffer.SetData<VertexPositionTexture>(vertices);
            BatchInformations[0][0].IndexBuffer.SetData<short>(indices);
            
        } 

        public override int MeshNumber
        {
            get { return 1; }
        }

        public override void CleanUp(GraphicFactory factory)
        {
            base.CleanUp(factory);
            BatchInformations[0][0].VertexBuffer.Dispose();
            BatchInformations[0][0].IndexBuffer.Dispose();
            TextureInformations[0][0].CleanUp(factory);
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }
    }
    
}
