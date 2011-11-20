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
   public enum GammaType
    {
        Normal, Simple,InvertNormal, InvertSimple,InvertVerySimple,VerySimple
    }

    /// <summary>
    /// OLHAR http://mynameismjp.wordpress.com/2009/12/31/correcting-xnas-gamma-correction/
    /// SOLUCAO MAIS ELEGANTE PARA PCS --  mudar o render state pelo effect apos o ultimo efeito aplicado
    /// </summary>
    public class GammaCorrectionPostEffect : IPostEffect
    {
        public GammaCorrectionPostEffect(GammaType type) : base(PostEffectType.All)
        {
            this._gType  = type;
        }

#region IPostEffect Members

        Effect effect = null;

        private GammaType _gType;
        
        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            ///Draw a quad using the "effect", passing the CurrentImage as a Parameter            
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("gamma",true,true);            
            switch (_gType)
            {
                case GammaType.Normal:
                    effect.CurrentTechnique = effect.Techniques[0];
                    break;
                case GammaType.Simple:
                    effect.CurrentTechnique = effect.Techniques[1];
                    break;
                case GammaType.InvertNormal:
                    effect.CurrentTechnique = effect.Techniques[2];
                    break;
                case GammaType.InvertSimple:
                    effect.CurrentTechnique = effect.Techniques[3];
                    break;
                case GammaType.VerySimple:
                    effect.CurrentTechnique = effect.Techniques[4];
                    break;
                case GammaType.InvertVerySimple:
                    effect.CurrentTechnique = effect.Techniques[5];
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
#endif