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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{

    public class ContrastPostEffect : IPostEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContrastPostEffect"/> class.
        /// </summary>
        /// <param name="contrast">-1 a 1</param>
        public ContrastPostEffect(float contrast) : base(PostEffectType.All)
        {
            this.contrast  = contrast;
        }

        #region IPostEffect Members

        Effect effect = null;
        float contrast = 0.5f;
        
        public float Contrast
        {
            get { return contrast; }
            set { contrast = value; }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {            
            effect.Parameters["Contrast"].SetValue(contrast);
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("contrast",false,true);            
        }
        
        #endregion

    }
}
#endif