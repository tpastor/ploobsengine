using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Input;

namespace EngineTestes
{
    public class MLAntiAliasingPostEffect : IPostEffect 
    {

        /// <summary>
        /// MLAntiAliasingEdgeDetectionMode
        /// </summary>
        public enum MLAntiAliasingEdgeDetectionMode
        {
            /// <summary>
            /// using color buffer
            /// </summary>
            Color,
            /// <summary>
            /// Using Depth buffer
            /// </summary>
            Depth
        }
        public MLAntiAliasingPostEffect(MLAntiAliasingEdgeDetectionMode mLAntiAliasingEdgeDetectionMode = MLAntiAliasingEdgeDetectionMode.Depth)
            : base(PostEffectType.Deferred)
        {
            this.mLAntiAliasingEdgeDetectionMode = mLAntiAliasingEdgeDetectionMode;
            if (mLAntiAliasingEdgeDetectionMode == MLAntiAliasingEdgeDetectionMode.Color)
            {
                this.threshold = 0.07f;
            }
            else
            {
                this.threshold = 0.0008f;
            }
            
            this.priority = float.MaxValue;
        }

        MLAntiAliasingEdgeDetectionMode mLAntiAliasingEdgeDetectionMode;
        RenderTarget2D rt0;
        RenderTarget2D rt1;
        Texture2D tex;
        private Effect pass;
        private float threshold;

        /// <summary>
        /// Gets or sets the threshold.
        /// Default: 0.0008f for Depth 0.07 for Color
        /// </summary>
        /// <value>
        /// The threshold.
        /// </value>
        public float Threshold
        {
            get { return threshold; }
            set { threshold = value; }
        }
        
        Effect effect = null;
        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    rHelper.RenderTextureComplete(ImageToProcess);
            //    return;
            //}

                rHelper.PushRenderTarget(rt0);
                rHelper.Clear(Color.Transparent);
                if (mLAntiAliasingEdgeDetectionMode == MLAntiAliasingEdgeDetectionMode.Color)
                    effect.CurrentTechnique = effect.Techniques["ColorEdgeDetection"];
                else
                {
                    effect.CurrentTechnique = effect.Techniques["DepthEdgeDetection"];
                    effect.Parameters["depthTex"].SetValue(rHelper[PrincipalConstants.DephRT]);
                }
                effect.Parameters["PIXEL_SIZE"].SetValue(GraphicInfo.HalfPixel * 2);                
                effect.Parameters["colorTex"].SetValue(rHelper[PrincipalConstants.CombinedImage]);
                effect.Parameters["threshold"].SetValue(0.0008f);
                rHelper.RenderFullScreenQuadVertexPixel(effect);
                Texture edges = rHelper.PopRenderTargetAsSingleRenderTarget2D();                
  
                rHelper.PushRenderTarget(rt1);              
                rHelper.Clear(Color.Transparent);
                effect.CurrentTechnique = effect.Techniques["BlendWeightCalculation"];
                effect.Parameters["areaTex"].SetValue(tex);
                effect.Parameters["edgesTex"].SetValue(edges);
                effect.Parameters["MAX_SEARCH_STEPS"].SetValue(16);
                effect.Parameters["MAX_DISTANCE"].SetValue(65);                
            
                rHelper.RenderFullScreenQuadVertexPixel(effect);
                Texture2D tt = rHelper.PopRenderTargetAsSingleRenderTarget2D();
                        
                rHelper.Clear(Color.Transparent);                
                pass.Parameters["cena"].SetValue(ImageToProcess);                
                
                //if (Keyboard.GetState().IsKeyDown(Keys.RightAlt))
                //{
                //    rHelper.RenderTextureComplete(tt);
                //    return;
                //}
                //else
                //{
                    //pass.Parameters["cena"].SetValue(ImageToProcess);
                    rHelper.RenderFullScreenQuadVertexPixel(pass);                 
                //}
                
                effect.CurrentTechnique = effect.Techniques["NeighborhoodBlending"];
                effect.Parameters["colorTex"].SetValue(ImageToProcess);
                effect.Parameters["blendTex"].SetValue(tt);                
                rHelper.RenderFullScreenQuadVertexPixel(effect);            
                
        }

        public override void Init(GraphicInfo ginfo, GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/mlaa");
            pass = factory.GetEffect("Effects/Effect1");
            rt0 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            rt1 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            tex = factory.GetTexture2D("AA//AreaMap65");
            pass.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
            effect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);            
        }

    }
}
