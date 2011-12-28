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
#if (WINDOWS || XBOX) && !REACH
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PloobsEngine.Cameras;
using PloobsEngine.Components;
using PloobsEngine.Engine;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Features
{
    /// <summary>
    /// SkyBox Component
    /// </summary>
    public class SkyBox : IComponent
    {
        private VertexPositionNormalTexture[] cubeVertices;
        private VertexBuffer vertexBuffer;
        private TextureCube texture;
        private Effect effect;       
        
        public static readonly String MyName = "SkyBox";
        GraphicFactory factory;
        BatchInformation bi;
        bool enable = false;
        
        #region Initialize        
        

        protected override void  LoadContent(Engine.GraphicInfo GraphicInfo,GraphicFactory factory)
        {
            base.LoadContent(GraphicInfo, factory);
            effect = factory.GetEffect("skybox",false,true);
            this.factory = factory;
            cubeVertices = new VertexPositionNormalTexture[36];

            Vector3 topLeftFront = new Vector3(-1.0f, 1.0f, 1.0f);
            Vector3 bottomLeftFront = new Vector3(-1.0f, -1.0f, 1.0f);
            Vector3 topRightFront = new Vector3(1.0f, 1.0f, 1.0f);
            Vector3 bottomRightFront = new Vector3(1.0f, -1.0f, 1.0f);
            Vector3 topLeftBack = new Vector3(-1.0f, 1.0f, -1.0f);
            Vector3 topRightBack = new Vector3(1.0f, 1.0f, -1.0f);
            Vector3 bottomLeftBack = new Vector3(-1.0f, -1.0f, -1.0f);
            Vector3 bottomRightBack = new Vector3(1.0f, -1.0f, -1.0f);

            Vector2 textureTopLeft = new Vector2(0.0f, 0.0f);
            Vector2 textureTopRight = new Vector2(1.0f, 0.0f);
            Vector2 textureBottomLeft = new Vector2(0.0f, 1.0f);
            Vector2 textureBottomRight = new Vector2(1.0f, 1.0f);

            Vector3 frontNormal = new Vector3(0.0f, 0.0f, 1.0f);
            Vector3 backNormal = new Vector3(0.0f, 0.0f, -1.0f);
            Vector3 topNormal = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 bottomNormal = new Vector3(0.0f, -1.0f, 0.0f);
            Vector3 leftNormal = new Vector3(-1.0f, 0.0f, 0.0f);
            Vector3 rightNormal = new Vector3(1.0f, 0.0f, 0.0f);

            // Front face
            cubeVertices[0] =
                new VertexPositionNormalTexture(
                topLeftFront, frontNormal, textureTopLeft);
            cubeVertices[1] =
                new VertexPositionNormalTexture(
                bottomLeftFront, frontNormal, textureBottomLeft);
            cubeVertices[2] =
                new VertexPositionNormalTexture(
                topRightFront, frontNormal, textureTopRight);
            cubeVertices[3] =
                new VertexPositionNormalTexture(
                bottomLeftFront, frontNormal, textureBottomLeft);
            cubeVertices[4] =
                new VertexPositionNormalTexture(
                bottomRightFront, frontNormal, textureBottomRight);
            cubeVertices[5] =
                new VertexPositionNormalTexture(
                topRightFront, frontNormal, textureTopRight);

            // Back face 
            cubeVertices[6] =
                new VertexPositionNormalTexture(
                topLeftBack, backNormal, textureTopRight);
            cubeVertices[7] =
                new VertexPositionNormalTexture(
                topRightBack, backNormal, textureTopLeft);
            cubeVertices[8] =
                new VertexPositionNormalTexture(
                bottomLeftBack, backNormal, textureBottomRight);
            cubeVertices[9] =
                new VertexPositionNormalTexture(
                bottomLeftBack, backNormal, textureBottomRight);
            cubeVertices[10] =
                new VertexPositionNormalTexture(
                topRightBack, backNormal, textureTopLeft);
            cubeVertices[11] =
                new VertexPositionNormalTexture(
                bottomRightBack, backNormal, textureBottomLeft);

            // Top face
            cubeVertices[12] =
                new VertexPositionNormalTexture(
                topLeftFront, topNormal, textureBottomLeft);
            cubeVertices[13] =
                new VertexPositionNormalTexture(
                topRightBack, topNormal, textureTopRight);
            cubeVertices[14] =
                new VertexPositionNormalTexture(
                topLeftBack, topNormal, textureTopLeft);
            cubeVertices[15] =
                new VertexPositionNormalTexture(
                topLeftFront, topNormal, textureBottomLeft);
            cubeVertices[16] =
                new VertexPositionNormalTexture(
                topRightFront, topNormal, textureBottomRight);
            cubeVertices[17] =
                new VertexPositionNormalTexture(
                topRightBack, topNormal, textureTopRight);

            // Bottom face 
            cubeVertices[18] =
                new VertexPositionNormalTexture(
                bottomLeftFront, bottomNormal, textureTopLeft);
            cubeVertices[19] =
                new VertexPositionNormalTexture(
                bottomLeftBack, bottomNormal, textureBottomLeft);
            cubeVertices[20] =
                new VertexPositionNormalTexture(
                bottomRightBack, bottomNormal, textureBottomRight);
            cubeVertices[21] =
                new VertexPositionNormalTexture(
                bottomLeftFront, bottomNormal, textureTopLeft);
            cubeVertices[22] =
                new VertexPositionNormalTexture(
                bottomRightBack, bottomNormal, textureBottomRight);
            cubeVertices[23] =
                new VertexPositionNormalTexture(
                bottomRightFront, bottomNormal, textureTopRight);

            // Left face
            cubeVertices[24] =
                new VertexPositionNormalTexture(
                topLeftFront, leftNormal, textureTopRight);
            cubeVertices[25] =
                new VertexPositionNormalTexture(
                bottomLeftBack, leftNormal, textureBottomLeft);
            cubeVertices[26] =
                new VertexPositionNormalTexture(
                bottomLeftFront, leftNormal, textureBottomRight);
            cubeVertices[27] =
                new VertexPositionNormalTexture(
                topLeftBack, leftNormal, textureTopLeft);
            cubeVertices[28] =
                new VertexPositionNormalTexture(
                bottomLeftBack, leftNormal, textureBottomLeft);
            cubeVertices[29] =
                new VertexPositionNormalTexture(
                topLeftFront, leftNormal, textureTopRight);

            // Right face 
            cubeVertices[30] =
                new VertexPositionNormalTexture(
                topRightFront, rightNormal, textureTopLeft);
            cubeVertices[31] =
                new VertexPositionNormalTexture(
                bottomRightFront, rightNormal, textureBottomLeft);
            cubeVertices[32] =
                new VertexPositionNormalTexture(
                bottomRightBack, rightNormal, textureBottomRight);
            cubeVertices[33] =
                new VertexPositionNormalTexture(
                topRightBack, rightNormal, textureTopRight);
            cubeVertices[34] =
                new VertexPositionNormalTexture(
                topRightFront, rightNormal, textureTopLeft);
            cubeVertices[35] =
                new VertexPositionNormalTexture(
                bottomRightBack, rightNormal, textureBottomRight);

            vertexBuffer = factory.CreateVertexBuffer(VertexPositionNormalTexture.VertexDeclaration,36, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionNormalTexture>(cubeVertices);
            bi = new BatchInformation(0, 36, 23, 0, 0, VertexPositionNormalTexture.VertexDeclaration, VertexPositionNormalTexture.VertexDeclaration.VertexStride, BatchType.NORMAL);
            bi.VertexBuffer = vertexBuffer;            
        }

        #endregion

        internal void setParameters(String texCubeName)
        {
            this.texture = factory.GetTextureCube(texCubeName);
        }

        internal void setParameters(bool enable)
        {
            this.enable = enable;
        }        
        
        #region IReciever Members

        public override string getMyName()
        {
            return MyName;
        }
                
        #endregion

        #region IComponent Members

        public override ComponentType ComponentType
        {
            get { return ComponentType.PRE_DRAWABLE; }
        }

        protected override void  PreDraw(RenderHelper render,GameTime gt, Matrix activeView, Matrix activeProjection)
        {
            if (!enable)
                return;

 	        base.PreDraw(render,gt, activeView, activeProjection);
            
            Matrix x = Matrix.Invert(activeView);
            Vector3 camPos = new Vector3(x.M41, x.M42, x.M43);
            
            effect.CurrentTechnique = effect.Techniques["RenderScene"];
            effect.Parameters["WorldViewProjection"].SetValue(Matrix.CreateTranslation(camPos) * activeView * activeProjection);
            effect.Parameters["textureDiffuse"].SetValue(texture);

            render.PushDepthStencilState(DepthStencilState.None);
            render.PushRasterizerState(RasterizerState.CullNone);
            render.RenderBatch(bi, effect);
            render.PopRasterizerState();
            render.PopDepthStencilState();

        }

        #endregion
    }
}
#else 
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using PloobsEngine.Cameras;
using PloobsEngine.Components;

namespace PloobsEngine.Features
{
    public class SkyBox : IComponent
    {
        String textureCubeName;
        bool enable = false;        
        
        internal void setParameters(String texCubeName)
        {
            Cube = GraphicFactory.GetTextureCube(texCubeName);             
            CubeFaces = new Texture2D[6];
            PositionOffset = new Vector3(20, 20, 20);
            CreateGraphic(512);
            StripTexturesFromCube();
            InitializeData();
        }
        
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory)
        {
            base.LoadContent(GraphicInfo, factory);
            this.GraphicFactory = factory;
        }


        #region Properties
        GraphicFactory GraphicFactory;
        public Vector3 PositionOffset { get; set; }
        public Vector3 Position { get; set; }
        public TextureCube Cube { get; set; }
        public Color[] PixelArray { get; set; }
        public Texture2D[] CubeFaces { get; set; }
        public VertexBuffer VertexBuffer { get; set; }
        public IndexBuffer IndexBuffer { get; set; }
        public BasicEffect Effect { get; set; }
        #endregion

        internal void setParameters(bool enable)
        {
            this.enable = enable;
        }        
        

        public override string getMyName()
        {
            return MyName;
        }

        public static readonly String MyName = "SkyBox";

        public override ComponentType ComponentType
        {
            get { return PloobsEngine.Components.ComponentType.PRE_DRAWABLE; }
        }

        protected override void PreDraw(RenderHelper render, GameTime gt, Matrix activeView, Matrix activeProjection)
        {
            if (!enable || Cube == null)
                return;

            Matrix viewIT = Matrix.Invert(Matrix.Transpose(activeView));
            Vector3 position = new Vector3(viewIT.M14, viewIT.M24, viewIT.M34);

            this.Position = position + PositionOffset;
            Effect.View = activeView;
            Effect.Projection = activeProjection;
            Effect.World = Matrix.Identity;

            render.PushDepthStencilState(new DepthStencilState() { DepthBufferEnable = false });
            render.PushRasterizerState(new RasterizerState() { CullMode = CullMode.None });

            var graphics = Effect.GraphicsDevice;
            graphics.SetVertexBuffer(VertexBuffer);
            graphics.Indices = IndexBuffer;

            Effect.Texture = CubeFaces[0];
            Effect.CurrentTechnique.Passes[0].Apply();
            graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertices.Count, 0, 2);
            Effect.Texture = CubeFaces[1];
            Effect.CurrentTechnique.Passes[0].Apply();
            graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertices.Count, 6, 2);
            Effect.Texture = CubeFaces[2];
            Effect.CurrentTechnique.Passes[0].Apply();
            graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertices.Count, 12, 2);
            Effect.Texture = CubeFaces[3];
            Effect.CurrentTechnique.Passes[0].Apply();
            graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertices.Count, 18, 2);
            Effect.Texture = CubeFaces[4];
            Effect.CurrentTechnique.Passes[0].Apply();
            graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertices.Count, 24, 2);
            Effect.Texture = CubeFaces[5];
            Effect.CurrentTechnique.Passes[0].Apply();
            graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertices.Count, 30, 2);

            render.PopDepthStencilState();
            render.PopRasterizerState();

        }
        #region Fields

        private List<VertexPositionNormalTexture> _vertices = new List<VertexPositionNormalTexture>();
        private List<ushort> _indices = new List<ushort>();
        #endregion
        #region Private methods
        private void InitializeData()
        {
            VertexBuffer = GraphicFactory.CreateVertexBuffer(VertexPositionNormalTexture.VertexDeclaration, _vertices.Count, BufferUsage.None);
            VertexBuffer.SetData<VertexPositionNormalTexture>(_vertices.ToArray());
            IndexBuffer = GraphicFactory.CreateIndexBuffer(IndexElementSize.SixteenBits, _indices.Count, BufferUsage.None);
            IndexBuffer.SetData<ushort>(_indices.ToArray());
            Effect = GraphicFactory.GetBasicEffect();
            Effect.TextureEnabled = true;
            //Effect.EnableDefaultLighting();         
        }


        private void CreateGraphic(float size)
        {
            Vector3[] normals = {                                     
                                    Vector3.Right,                                     
                                    Vector3.Left,                                     
                                    Vector3.Up,
                                    Vector3.Down,
                                    Vector3.Backward,                                     
                                    Vector3.Forward,                                 
                                };
            Vector2[] textureCoordinates = {   
                                               Vector2.One, Vector2.UnitY, Vector2.Zero, Vector2.UnitX,
                                               Vector2.Zero, Vector2.UnitX, Vector2.One, Vector2.UnitY,  
                                               Vector2.Zero, Vector2.UnitX, Vector2.One, Vector2.UnitY,  
                                               Vector2.Zero, Vector2.UnitX, Vector2.One, Vector2.UnitY,  
                                               Vector2.UnitY, Vector2.Zero, Vector2.UnitX, Vector2.One,  
                                               Vector2.UnitY, Vector2.Zero, Vector2.UnitX, Vector2.One,  
                                           };
            var index = 0;
            foreach (var normal in normals)
            {
                var side1 = new Vector3(normal.Z, normal.X, normal.Y);
                var side2 = Vector3.Cross(normal, side1);
                AddIndex(CurrentVertex + 0);
                AddIndex(CurrentVertex + 1);
                AddIndex(CurrentVertex + 2);
                AddIndex(CurrentVertex + 0);
                AddIndex(CurrentVertex + 2);
                AddIndex(CurrentVertex + 3);
                AddVertex((normal - side1 - side2) * size / 2, normal, textureCoordinates[index++]);
                AddVertex((normal - side1 + side2) * size / 2, normal, textureCoordinates[index++]);
                AddVertex((normal + side1 + side2) * size / 2, normal, textureCoordinates[index++]);
                AddVertex((normal + side1 - side2) * size / 2, normal, textureCoordinates[index++]);
            }
        }
        protected void StripTexturesFromCube()
        {
            PixelArray = new Color[Cube.Size * Cube.Size];
            for (int s = 0; s < CubeFaces.Length; s++)
            {
                CubeFaces[s] = GraphicFactory.CreateTexture2D(Cube.Size, Cube.Size, false, SurfaceFormat.Color);
                switch (s)
                {
                    case 0:
                        Cube.GetData<Color>(CubeMapFace.PositiveX, PixelArray);
                        CubeFaces[s].SetData<Color>(PixelArray);
                        break;
                    case 1:
                        Cube.GetData(CubeMapFace.NegativeX, PixelArray);
                        CubeFaces[s].SetData(PixelArray);
                        break;
                    case 2:
                        Cube.GetData(CubeMapFace.PositiveY, PixelArray);
                        CubeFaces[s].SetData(PixelArray);
                        break;
                    case 3:
                        Cube.GetData(CubeMapFace.NegativeY, PixelArray);
                        CubeFaces[s].SetData(PixelArray);
                        break;
                    case 4:
                        Cube.GetData(CubeMapFace.PositiveZ, PixelArray);
                        CubeFaces[s].SetData(PixelArray);
                        break;
                    case 5:
                        Cube.GetData(CubeMapFace.NegativeZ, PixelArray);
                        CubeFaces[s].SetData(PixelArray);
                        break;
                }
            }
        }
        protected void AddVertex(Vector3 position, Vector3 normal, Vector2 textureCoordinates)
        {
            _vertices.Add(new VertexPositionNormalTexture(position, normal, textureCoordinates));
        }
        protected void AddIndex(int index)
        {
            if (index > ushort.MaxValue)
                throw new ArgumentOutOfRangeException("index");
            _indices.Add((ushort)index);
        }

        protected int CurrentVertex
        {
            get { return _vertices.Count; }
        }
        #endregion
    }
} 

#endif