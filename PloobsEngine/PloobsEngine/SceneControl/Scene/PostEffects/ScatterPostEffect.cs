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
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.SceneControl
{
    public class ScatterPostEffect : IPostEffect
    {
        public ScatterPostEffect() : base(PostEffectType.Deferred) { }
        
        Effect effect = null;             
        float density = 0.1f;
        Vector3 lightPosition = new Vector3(2000.0f, 2800.0f, 2000.0f);
        Effect effect2 = null;        
        RenderTarget2D tr2d;
        ToBlackPostEffect to;

        public Vector3 LightPosition
        {
            get { return lightPosition; }
            set { lightPosition = value; }
        }

        /// <summary>
        /// Default 0.1
        /// </summary>
        public float Density
        {
            get { return density; }
            set { density = value; }
        }
        float weight = 0.1f;

        /// <summary>
        /// Defautl 0.1
        /// </summary>
        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        float decay = 1.0f;

        /// <summary>
        /// Default 1;
        /// </summary>
        public float Decay
        {
            get { return decay; }
            set { decay = value; }
        }
        float exposition = 0.5f;

        /// <summary>
        /// Default 0.5f
        /// </summary>
        public float Exposition
        {
            get { return exposition; }
            set { exposition = value; }
        }
        

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {

            rHelper.PushRenderTarget(tr2d);
            to.Draw(ImageToProcess, rHelper, gt, GraphicInfo, world, useFloatingBuffer);
            Texture2D tex =  rHelper.PopRenderTargetAsSingleRenderTarget2D();
            
             effect.Parameters["View"].SetValue(world.CameraManager.ActiveCamera.View);             
             effect.Parameters["WorldViewProjection"].SetValue(world.CameraManager.ActiveCamera.ViewProjection);
             effect.Parameters["LightPosition"].SetValue(lightPosition);
             effect.Parameters["CameraPos"].SetValue(world.CameraManager.ActiveCamera.Position);
             //effect.Parameters["numSamples"].SetValue(numSamples);
             effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
             effect.Parameters["pb"].SetValue(tex);
             effect.Parameters["normal"].SetValue(ImageToProcess);
             effect.Parameters["Density"].SetValue(density);
             effect.Parameters["Weight"].SetValue(weight);
             effect.Parameters["Decay"].SetValue(decay);
             effect.Parameters["Exposition"].SetValue(exposition);
             if (useFloatingBuffer)
                 rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
             else
                 rHelper.RenderFullScreenQuadVertexPixel(effect, GraphicInfo.SamplerState);              
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Scatter",false,true);            
            to = new ToBlackPostEffect();
            to.Init(ginfo, factory);
            tr2d = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight,SurfaceFormat.Color,ginfo.UseMipMap,DepthFormat.None,ginfo.MultiSample);
            

        }

    }
}


#endif