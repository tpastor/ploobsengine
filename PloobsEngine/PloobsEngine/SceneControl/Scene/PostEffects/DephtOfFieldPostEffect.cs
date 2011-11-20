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
    public class DephtOfFieldPostEffect : IPostEffect
    {
        public DephtOfFieldPostEffect() : base(PostEffectType.Deferred) { }

#region IPostEffect Members
        GaussianBlurPostEffect be;        
        Effect depht;        
        RenderTarget2D target;
        float range = 50f;
        float distance = 70f;

        public float Range
        {
            set
            {
                this.range = value;
            }
            get
            {
                return this.range;
            }
        }

        public float Distance
        {
            set
            {
                this.distance = value;
            }
            get
            {
                return this.distance;
            }
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {           
            depht = factory.GetEffect("depth",false,true);
            target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight,SurfaceFormat.Color,
                ginfo.UseMipMap,DepthFormat.None,ginfo.MultiSample);
            be= new GaussianBlurPostEffect();
            be.Init(ginfo, factory); 
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {            
            rHelper.PushRenderTarget(target);
            be.Draw(ImageToProcess, rHelper,  gt, GraphicInfo, world,useFloatingBuffer);                                    
            depht.Parameters["BlurScene"].SetValue(rHelper.PopRenderTargetAsSingleRenderTarget2D());
            depht.Parameters["FarPlane"].SetValue(world.CameraManager.ActiveCamera.FarPlane);
            depht.Parameters["D1M"].SetValue(rHelper[PrincipalConstants.DephRT]);

            rHelper.Clear(Color.Black,ClearOptions.Target);
            SetShaderParameters(distance, range, world.CameraManager.ActiveCamera.NearPlane, world.CameraManager.ActiveCamera.FarPlane);
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, depht, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, depht, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
            
        }

        void SetShaderParameters(float fD, float fR, float nC, float fC)
        {
            float focusDistance = fD;
            float focusRange = fR;
            float nearClip = nC;
            float farClip = fC;
            farClip = farClip / (farClip - nearClip);

            depht.Parameters["Distance"].SetValue(focusDistance);
            depht.Parameters["Range"].SetValue(focusRange);
            depht.Parameters["Near"].SetValue(nearClip);
            depht.Parameters["Far"].SetValue(farClip);
        }

        #endregion
    }
}
#endif