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
using PloobsEngine.SceneControl;
using PloobsEngine;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.SceneControl
{
    public class AntiAliasingPostEffectStalker : IPostEffect 
    {
        public AntiAliasingPostEffectStalker()
            : base(PostEffectType.Deferred)
        {
            
        }        
        
        private Vector2 barrier = new Vector2(0.8f, 0.00001f);

        public Vector2 Barrier
        {
            get { return barrier; }
            set { barrier = value; }
        }
        private Vector2 weights = new Vector2(1);

        public Vector2 Weights
        {
            get { return weights; }
            set { weights = value; }
        }
        private Vector2 kernel = new Vector2(1);

        public Vector2 Kernel
        {
            get { return kernel; }
            set { kernel = value; }
        }
                
        Effect effect = null;
        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {


            effect.Parameters["e_barrier"].SetValue(barrier);
            effect.Parameters["e_kernel"].SetValue(kernel);
            effect.Parameters["e_weights"].SetValue(weights);
            effect.Parameters["pixel_size"].SetValue(GraphicInfo.HalfPixel);            
            effect.Parameters["depthTex"].SetValue(rHelper[PrincipalConstants.DephRT]);
            effect.Parameters["normalTex"].SetValue(rHelper[PrincipalConstants.normalRt]);
            effect.Parameters["image"].SetValue(ImageToProcess);                                    
            rHelper.Clear(Color.Transparent);
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    rHelper.RenderTextureComplete(ImageToProcess);
            //    return;
            //}
            if (useFloatingBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect, GraphicInfo.SamplerState);
        }

        public override void Init(GraphicInfo ginfo, GraphicFactory factory)
        {
            effect = factory.GetEffect("AntiAliasingtabulastalker",false,true);
            effect.CurrentTechnique = effect.Techniques["AntiAliasingStalker"];            
        }        
    }
}
#endif