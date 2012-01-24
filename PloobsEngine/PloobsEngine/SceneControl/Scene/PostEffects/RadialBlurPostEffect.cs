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
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class RadialBlurPostEffect : IPostEffect
    {
        public RadialBlurPostEffect() : base(PostEffectType.All) {
            Center = new Vector2(0.5f, 0.5f);
            BlurStart = 1.0f;
            BlurWidth = -0.1f;        
        }
        Effect effect;
        public Vector2 Center
        {
            set;
            get;
        }
        public float BlurWidth
        {
            set;
            get;
        }
        public float BlurStart
        {
            set;
            get;
        }


        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {            
            effect = factory.GetEffect("RadialBlur", false, true);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            effect.Parameters["BlurStart"].SetValue(BlurStart);
            effect.Parameters["BlurWidth"].SetValue(BlurWidth);
            effect.Parameters["Center"].SetValue(Center);
            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);                
        }
    }
}
#endif