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
using PloobsEngine.SceneControl;
using PloobsEngine;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.SceneControl
{
    public class AntiAliasingPostEffectTabula : IPostEffect 
    {
        public AntiAliasingPostEffectTabula()
            : base(PostEffectType.Deferred)
        {
            this.Priority = float.MaxValue;
        }        
        
        private float weights = 1;

        public float Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        float depthSensibility = 1000;

        public float DepthSensibility
        {
            get { return depthSensibility; }
            set { depthSensibility = value; }
        }
        float normalSensibility = 700;

        public float NormalSensibility
        {
            get { return normalSensibility; }
            set { normalSensibility = value; }
        }
                

        Effect effect = null;
        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            effect.Parameters["normalSensibility"].SetValue(normalSensibility);
            effect.Parameters["depthSensibility"].SetValue(depthSensibility);
            effect.Parameters["weight"].SetValue(weights);
            effect.Parameters["pixel_size"].SetValue(GraphicInfo.HalfPixel);            
            effect.Parameters["depthTex"].SetValue(rHelper[PrincipalConstants.DephRT]);
            effect.Parameters["normalTex"].SetValue(rHelper[PrincipalConstants.normalRt]);
            effect.Parameters["image"].SetValue(ImageToProcess);                                    

            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    rHelper.RenderTextureComplete(ImageToProcess);
            //    return;
            //}
            if(useFloatingBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect,SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect, GraphicInfo.SamplerState);
        }

        public override void Init(GraphicInfo ginfo, GraphicFactory factory)
        {
            effect = factory.GetEffect("AntiAliasingtabulastalker",false,true);
            effect.CurrentTechnique = effect.Techniques["AntiAliasingTabula"];            
        }        
    }
}
#endif