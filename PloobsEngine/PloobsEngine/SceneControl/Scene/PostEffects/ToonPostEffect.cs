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
#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class ToonPostEffect : IPostEffect
    {
        public ToonPostEffect() : base(PostEffectType.Deferred) { }

        #region IPostEffect Members

        private Effect toon;
        private Effect line;
        private Texture2D cell;        
        private RenderTarget2D target;
        private bool useLight = true;
        private bool changeColors= true;

        public bool UseLight
        {
            set
            {
                this.useLight = value;
            }
            get
            {
                return this.useLight;
            }
        }

        public bool ChangeColors
        {
            set
            {
                this.changeColors = value;
            }
            get
            {
                return this.changeColors;
            }
        }


        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            this.toon = factory.GetEffect("toon1",false,true);
            this.line = factory.GetEffect("LineDetection",false,true);
            this.cell = factory.GetTexture2D("Toon",true);            
            target = factory.CreateRenderTarget(ginfo.BackBufferWidth,ginfo.BackBufferHeight,SurfaceFormat.Color,ginfo.UseMipMap,DepthFormat.None,ginfo.MultiSample);
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            if (changeColors)
            {
                rHelper.PushRenderTarget(target);                

                toon.Parameters["ColorMap"].SetValue(ImageToProcess);
                toon.Parameters["useLight"].SetValue(useLight);
                toon.Parameters["TexColor"].SetValue(rHelper[PrincipalConstants.colorRT]);
                toon.Parameters["CelMap"].SetValue(cell);
                toon.Parameters["LightMap"].SetValue(rHelper[PrincipalConstants.lightRt]);
                toon.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
                if (useFloatingBuffer)
                    rHelper.RenderFullScreenQuadVertexPixel(toon, SamplerState.PointClamp);
                else
                    rHelper.RenderFullScreenQuadVertexPixel(toon, GraphicInfo.SamplerState);              

                ImageToProcess = rHelper.PopRenderTargetAsSingleRenderTarget2D();
            }

                line.Parameters["Thickness"].SetValue(1.0f);
                line.Parameters["Threshold"].SetValue(0.2f);                
                if (useFloatingBuffer)
                    rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, line, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                    rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, line, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);
            
        }

        #endregion
    }
}
#endif