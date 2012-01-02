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
#if !WINDOWS_PHONE && !REACH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Bloom Post Effect
    /// </summary>
    public class BloomPostEffect : IPostEffect
    {
        public BloomPostEffect() : base(PostEffectType.All) { }

#region IPostEffect Members
        
        Effect Saturate = null;
        Effect Combine;
        RenderTarget2D renderTarget0;
        RenderTarget2D renderTarget1;
        GaussianBlurPostEffect gaussian;        
        float bloomThreshold = 0.4f;

        /// <summary>
        /// Bloom Parameter
        /// </summary>
        public float BloomThreshold
        {
            get { return bloomThreshold; }
            set
            {
                bloomThreshold = value;
                if (Saturate != null)
                {
                    Saturate.Parameters["BloomThreshold"].SetValue(bloomThreshold);
                }
            }
        }

        public override void Draw(Texture2D ImageToProcess,RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {            
            rHelper.PushRenderTarget(renderTarget1);
            Saturate.Parameters["current"].SetValue(ImageToProcess);
            Saturate.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
            if(useFloatingBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(Saturate,SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(Saturate, SamplerState.LinearClamp);

            Texture2D t = rHelper.PopRenderTargetAsSingleRenderTarget2D();

            rHelper.PushRenderTarget(renderTarget0);
            gaussian.Draw(t, rHelper, gt, GraphicInfo, world,useFloatingBuffer);
            Texture2D x = rHelper.PopRenderTargetAsSingleRenderTarget2D();
            rHelper.Clear(Color.Black);

            Combine.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
            Combine.Parameters["base"].SetValue(ImageToProcess);
            Combine.Parameters["last"].SetValue(x);
            if (useFloatingBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(Combine , SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(Combine, GraphicInfo.SamplerState);
        }


        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            
            Saturate = factory.GetEffect("Saturate",false,true);
            Saturate.Parameters["BloomThreshold"].SetValue(bloomThreshold);
            Combine = factory.GetEffect("Combine",false,true);

            renderTarget0 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight,SurfaceFormat.Color,ginfo.UseMipMap,DepthFormat.None,ginfo.MultiSample);
            renderTarget1 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample);
            
            gaussian = new GaussianBlurPostEffect();            
            gaussian.Init(ginfo,factory); 
            
        }  

        #endregion
    }
}
#else
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;


namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Bloom Post Effect
    /// </summary>
    public class BloomPostEffect : IPostEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BloomPostEffect"/> class.
        /// </summary>
        public BloomPostEffect() : base(PostEffectType.WindowsPhoneAndReach) { }

        GraphicFactory factory;
        #region IPostEffect Members

        int brightNess = 70;

        // <summary>
        /// Bloom Parameter
        /// 0 TO 255
        /// </summary>
        public int BrightNess
        {
            get { return brightNess; }
            set
            {
                brightNess = value;
            }
        }

        int bloomThreshold = 60;

        /// <summary>
        /// Bloom Parameter
        /// 0 TO 255
        /// </summary>
        public int BloomThreshold
        {
            get { return bloomThreshold; }
            set
            {
                bloomThreshold = value;                
            }
        }
        Engine.GraphicInfo ginfo;
        BlendState additiveBlend;
        BlendState subBlend;
        RenderTarget2D rt;

        /// <summary>
        /// Draws
        /// </summary>
        /// <param name="ImageToProcess">The image to process.</param>
        /// <param name="render">The render.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="world">The world.</param>
        /// <param name="useFloatingBuffer">if set to <c>true</c> [use floating buffer].</param>
        public override void Draw(Texture2D ImageToProcess, RenderHelper render, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            ///NOSSO threshold FILTER KKKKK =P
            render.PushRenderTarget(rt);
            render.Clear(Color.FromNonPremultiplied(bloomThreshold, bloomThreshold, bloomThreshold, 255));
            render.RenderTextureComplete(ImageToProcess, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.LinearClamp, subBlend);
            render.PopRenderTarget();

            render.Clear(Color.Black);
            render.RenderTextureComplete(ImageToProcess, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity);

            ///NOSSO BLUR KKKKK (mais KKKK)
            render.RenderTextureComplete(rt, Color.FromNonPremultiplied(255, 255, 255, brightNess), GraphicInfo.FullScreenRectangle, Matrix.CreateTranslation(2, 2, 0), null, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp, additiveBlend);
            render.RenderTextureComplete(rt, Color.FromNonPremultiplied(255, 255, 255, brightNess), GraphicInfo.FullScreenRectangle, Matrix.CreateTranslation(-2, -2, 0), null, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp, additiveBlend);
            render.RenderTextureComplete(rt, Color.FromNonPremultiplied(255, 255, 255, brightNess), GraphicInfo.FullScreenRectangle, Matrix.CreateTranslation(2, -2, 0), null, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp, additiveBlend);
            render.RenderTextureComplete(rt, Color.FromNonPremultiplied(255, 255, 255, brightNess), GraphicInfo.FullScreenRectangle, Matrix.CreateTranslation(-2, 2, 0), null, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp, additiveBlend);                              
            
        }


        /// <summary>
        /// Initiates the specified Post Effect.
        /// Called by the engine
        /// </summary>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            additiveBlend = new BlendState();
            additiveBlend.AlphaBlendFunction = BlendFunction.Add;
            additiveBlend.AlphaSourceBlend = Blend.One;
            additiveBlend.AlphaDestinationBlend = Blend.One;
            additiveBlend.ColorBlendFunction = BlendFunction.Add;
            additiveBlend.ColorSourceBlend = Blend.One;
            additiveBlend.ColorDestinationBlend = Blend.One;

            subBlend = new BlendState();
            subBlend.AlphaBlendFunction = BlendFunction.Subtract;
            subBlend.AlphaSourceBlend = Blend.One;
            subBlend.AlphaDestinationBlend = Blend.One;
            subBlend.ColorBlendFunction = BlendFunction.Subtract;
            subBlend.ColorSourceBlend = Blend.One;
            subBlend.ColorDestinationBlend = Blend.One;

            rt = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            ginfo.OnGraphicInfoChange += ginfo_OnGraphicInfoChange;
            this.factory = factory;
            this.ginfo = ginfo;
            
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
            ginfo.OnGraphicInfoChange -= ginfo_OnGraphicInfoChange;
            base.CleanUp();
        }


        #endregion
    }
}

#endif