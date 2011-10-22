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
    public class NoisePostEffect : IPostEffect
    {
        public NoisePostEffect() : base(PostEffectType.All) { }

        #region IPostEffect Members        
        private Effect noise;
        private float m_Timer = 0;        

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {            
            this.noise = factory.GetEffect("noise",false,true);            
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            m_Timer += (float)gt.ElapsedGameTime.Milliseconds / 500;
            noise.Parameters["fTimer"].SetValue(m_Timer);
            noise.Parameters["iSeed"].SetValue(1337);
            noise.Parameters["fNoiseAmount"].SetValue(0.01f);
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, noise, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, noise, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
            
        }

        #endregion
    }
}
#endif