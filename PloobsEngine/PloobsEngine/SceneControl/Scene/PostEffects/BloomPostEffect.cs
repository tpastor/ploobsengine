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
#endif