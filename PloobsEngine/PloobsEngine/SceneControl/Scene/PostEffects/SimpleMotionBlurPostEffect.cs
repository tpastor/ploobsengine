using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Simple Motion Blur post effect using alpha blending
    /// Can be used in all Plataforms
    /// </summary>
    public class SimpleMotionBlurPostEffect : IPostEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleMotionBlurPostEffect"/> class.
        /// </summary>
        public SimpleMotionBlurPostEffect() : base(PostEffectType.All) { }

        Texture2D end = null;
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            //mix with last frame downsampled
            if (tex != null)
            {
                rHelper.PushRenderTarget(rtend);
                rHelper.Clear(Color.Black);
                if (useFloatBuffer)
                {
                    rHelper.RenderTextureComplete(ImageToProcess, Color.FromNonPremultiplied(255, 255, 255, 255), GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp);
                    rHelper.RenderTextureComplete(tex, Color.FromNonPremultiplied(255, 255, 255, Amount), GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp, BlendState.AlphaBlend);
                }
                else
                {
                    rHelper.RenderTextureComplete(ImageToProcess, Color.FromNonPremultiplied(255, 255, 255, 255), GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, GraphicInfo.SamplerState);
                    rHelper.RenderTextureComplete(tex, Color.FromNonPremultiplied(255, 255, 255, Amount), GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, GraphicInfo.SamplerState, BlendState.AlphaBlend);
                }
                end = rHelper.PopRenderTargetAsSingleRenderTarget2D();
            }

            //DownSample
            rHelper.PushRenderTarget(rt);
            rHelper.Clear(Color.Black);
            if (end == null)
            {
                if (useFloatBuffer)
                {
                    rHelper.RenderTextureComplete(ImageToProcess, Color.White, rt.Bounds, Matrix.Identity, ImageToProcess.Bounds, true, SpriteSortMode.Deferred, SamplerState.PointClamp );
                }
                else
                {
                    rHelper.RenderTextureComplete(ImageToProcess, Color.White, rt.Bounds, Matrix.Identity, ImageToProcess.Bounds, true, SpriteSortMode.Deferred, GraphicInfo.SamplerState);
                }
            }
            else
            {
                if (useFloatBuffer)
                {
                    rHelper.RenderTextureComplete(end, Color.White, rt.Bounds, Matrix.Identity, ImageToProcess.Bounds, true, SpriteSortMode.Deferred, SamplerState.PointClamp);
                }
                else
                {
                    rHelper.RenderTextureComplete(end, Color.White, rt.Bounds, Matrix.Identity, ImageToProcess.Bounds, true, SpriteSortMode.Deferred, GraphicInfo.SamplerState);
                }
            }
            tex = rHelper.PopRenderTargetAsSingleRenderTarget2D();

            rHelper.Clear(Color.Black);
            if (end != null)
            {
                if (useFloatBuffer)
                {
                    rHelper.RenderTextureComplete(end, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp);
                }
                else
                {
                    rHelper.RenderTextureComplete(end, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, GraphicInfo.SamplerState);
                }
            }
            else
            {
                if (useFloatBuffer)
                {
                    rHelper.RenderTextureComplete(ImageToProcess, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp);
                }
                else
                {
                    rHelper.RenderTextureComplete(ImageToProcess, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, GraphicInfo.SamplerState);
                }
                
            }

        }

        public int Amount
        {
            get;
            set;

        }

        Texture2D tex;
        RenderTarget2D rt;
        RenderTarget2D rtend;
        GraphicFactory factory;
        GraphicInfo info;

        /// <summary>
        /// Initiates the specified Post Effect.
        /// Called by the engine
        /// </summary>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            this.info = ginfo;
            this.factory = factory;
            rt = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            rtend = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            ginfo.OnGraphicInfoChange += ginfo_OnGraphicInfoChange;
            Amount = 100;
            
        }

        void ginfo_OnGraphicInfoChange(object sender, EventArgs e)
        {
            GraphicInfo newGraphicInfo = (GraphicInfo)sender;
            if (rt != null)
            {
                rt.Dispose();
                rt = factory.CreateRenderTarget(newGraphicInfo.BackBufferWidth, newGraphicInfo.BackBufferHeight);
            }
        }

        public override void CleanUp()
        {
            info.OnGraphicInfoChange -= ginfo_OnGraphicInfoChange;
            if (rt != null && rtend != null)
            {
                rt.Dispose();
                rtend.Dispose();                   
            }
            base.CleanUp();
        }



    }
}
//The blend equation works like this:
//finalColor.rgb = (srcColor.rgb * SrcBlend) (BlendOp) (destColor.rgb * DestBlend);
//finalColor.a = (srcColor.a * SrcBlendAlpha) (BlendOpAlpha) (destColor.a * DestBlendAlpha);
