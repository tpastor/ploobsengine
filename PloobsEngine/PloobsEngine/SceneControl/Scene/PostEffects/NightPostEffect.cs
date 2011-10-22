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
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.SceneControl
{
    public class NightPostEffect : IPostEffect
    {
        public NightPostEffect()
            : base(PostEffectType.All)
        {
        }

        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Night",false,true);
            tex = factory.GetTexture2D("noise_tex6",true);
        }
        Effect effect;
        Texture2D tex;
        private float m_Timer = 0;
        float luminanceThreshold = 0.1f;

        public float LuminanceThreshold
        {
            get { return luminanceThreshold; }
            set { luminanceThreshold = value; }
        }
        float colorAmplification = 4;

        public float ColorAmplification
        {
            get { return colorAmplification; }
            set { colorAmplification = value; }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {

            m_Timer += (float)gt.ElapsedGameTime.Milliseconds / 500;
            effect.Parameters["elapsedTime"].SetValue(m_Timer);
            effect.Parameters["tex"].SetValue(tex);
            effect.Parameters["colorAmplification"].SetValue(colorAmplification);
            effect.Parameters["luminanceThreshold"].SetValue(luminanceThreshold);           

            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);
        }
    }
}
#endif