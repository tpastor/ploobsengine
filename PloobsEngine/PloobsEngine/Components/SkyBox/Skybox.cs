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
        private GraphicsDevice device;
        private ICamera cam;
        public static readonly String MyName = "SkyBox";
        GraphicFactory factory;
        BatchInformation bi;
        bool enable = false;
        
        private bool initialized = false;

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
            initialized = true;
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

            render.PushDepthState(DepthStencilState.None);
            render.PushRasterizerState(RasterizerState.CullNone);
            render.RenderBatch(bi, effect);
            render.PopRasterizerState();
            render.PopDepthStencilState();

        }

        #endregion
    }
}
