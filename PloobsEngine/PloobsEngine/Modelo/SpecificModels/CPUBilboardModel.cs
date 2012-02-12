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
    /// <summary>
    /// Represents each billboard
    /// </summary>
    public struct Billboard
    {
        public Billboard(Vector3 Position, Vector2 Scale, bool Enabled = true)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Enabled = Enabled;
        }

        public Vector3 Position;
        public Vector2 Scale;
        public bool Enabled;
    }

    public class CPUBillboardModel : IModelo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CPUBillboardModel"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="BilboardsName">Name of the bilboards.</param>
        /// <param name="diffuseTextureName">Name of the diffuse texture.</param>
        /// <param name="bilboards">The bilboards.</param>
        public CPUBillboardModel(GraphicFactory factory, String BilboardsName, String diffuseTextureName, Billboard[] bilboards)
            : base(factory, BilboardsName,false)
        {

            System.Diagnostics.Debug.Assert(bilboards != null && bilboards.Count() != 0);
            this.bilboards = bilboards;
            this.diffuseTextureName = diffuseTextureName;
            LoadModelo(factory);
            
        }

        string diffuseTextureName;
        private float modelRadius = 0;
        Billboard[] bilboards;
        bool forceUpdate = true;

        /// <summary>
        /// Loads the model.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="BatchInformations">The batch informations.</param>
        /// <param name="TextureInformations">The texture informations.</param>
        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformations)
        {
            int vertCount = bilboards.Count() * 4;
            int indexCount = bilboards.Count() * 6;
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

        /// <summary>
        /// Force Billboards Update in this frame
        /// Even if the Camera does not move
        /// </summary>
        public void ForceUpdate()
        {
            forceUpdate = true;
        }


        /// <summary>
        /// Gets the bilboard instances.
        /// </summary>
        public Billboard[] BilboardInstances
        {
            get
            {
                return bilboards;
            }
        }

        /// <summary>
        /// Updates the Model
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="world">The world.</param>
        public override void Update(GameTime gameTime, IWorld world)
        {
            ICamera cam = world.CameraManager.ActiveCamera;
            if (forceUpdate || cam.Hasmoved)
            {
                forceUpdate = false;
                int active = 0;
                for (int i = 0; i < bilboards.Count(); i++)
                {                
                    if (bilboards[i].Enabled == true)
                    {
                        active++;
                    }
                }

                int vertCount = active * 4;
                int indexCount = active * 6;
                int noVertices = vertCount;
                int noTriangles = indexCount / 3;
                BatchInformations[0][0].NumVertices = noVertices;
                BatchInformations[0][0].PrimitiveCount = noTriangles;                
                
                if (active == 0)
                {
                    ActiveLogger.LogMessage("CPUBillboard with 0 active billboard ... Disable its material, or remve it from the world", LogLevel.Warning);
                    return;
                }                    
                

                Texture2D tex = TextureInformations[0][0].getTexture(TextureType.DIFFUSE);                
                // Create billboard vertices.
                VertexPositionTexture[] vertices = new VertexPositionTexture[active * 4];
                int index = 0;                

                foreach (var item in bilboards)
                {
                    if (item.Enabled == false)
                        continue;

                    float width = tex.Width * item.Scale.X;
                    float height = tex.Height * item.Scale.Y;

                    Matrix billboard = Matrix.CreateConstrainedBillboard(
                      item.Position, cam.Position, Vector3.Up, cam.Target - cam.Position, null);

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
                short[] indices = new short[active * 6];
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
