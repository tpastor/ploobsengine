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
    public class ColorCorrectionPostEffect : IPostEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorCorrectionPostEffect"/> class.
        /// </summary>
        /// <param name="colorAdd">The color add.</param>
        /// <param name="colorCorrect">The color correct.</param>
        public ColorCorrectionPostEffect(Color colorAdd ,float colorCorrect) : base(PostEffectType.All)
        {
            this.colorAdd = colorAdd.ToVector3(); ;
            this.colorCorrect = colorCorrect;

        }

#region IPostEffect Members

        Effect effect = null;

        Vector3 colorAdd;

        public Vector3 ColorAdd
        {
            get { return colorAdd; }
            set { colorAdd = value; }
        }
        float colorCorrect;

        public float ColorCorrect
        {
            get { return colorCorrect; }
            set { colorCorrect = value; }
        }

        float colorToAddIntensity = 1;

        public float ColorToAddIntensity
        {
            get { return colorToAddIntensity; }
            set { colorToAddIntensity = value; }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            ///Draw a quad using the "effect", passing the CurrentImage as a Parameter            
            effect.Parameters["ColorCorrect"].SetValue(colorCorrect);
            effect.Parameters["ColorAdd"].SetValue(colorAdd);
            effect.Parameters["ColorToAddIntensity"].SetValue(colorToAddIntensity);

            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("colorCorrection",false,true);            
        }

        #endregion

    }
}
#endif