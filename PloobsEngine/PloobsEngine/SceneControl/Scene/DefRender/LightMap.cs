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
using PloobsEngine.Light;
using PloobsEngine.Cameras;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.SceneControl
{
    public class LightMap : IDeferredLightMap
    {   
        private RenderTarget2D lightRT;
        private Effect directionalLightEffect;
        private Effect pointLightEffect;
        private Effect spotLightEffect;
        private SimpleModel sphereModel;        
        private bool cullPointLight = true;
        GraphicInfo ginfo;
        SamplerState samplerState;


        //EffectParameter DirectionalcolorMap;
        //EffectParameter DirectionalnormalMap;
        //EffectParameter DirectionaldepthMap;
        EffectParameter DirectionalInvertViewProjection;
        EffectParameter DirectionalhalfPixel;
        EffectParameter DirectionalcameraPosition;        

        EffectParameter DirectionallightDirection;
        EffectParameter DirectionalColor;
        EffectParameter DirectionallightIntensity;
        
#region IDeferredLightMap Members        
        protected void DrawDirectionalLight(ICamera camera, IList<ILight> lights, IDeferredGBuffer DeferredGBuffer,RenderHelper render)
        {
            render.device.Textures[0] = DeferredGBuffer[GBufferTypes.COLOR];
            render.device.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
            render.device.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
            SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);

            DirectionalcameraPosition.SetValue(camera.Position);
            DirectionalInvertViewProjection.SetValue(Matrix.Invert(camera.ViewProjection));
            DirectionalhalfPixel.SetValue(ginfo.HalfPixel);
                        
            foreach (ILight item in lights)
            {
                if (item.LightType == LightType.Deferred_Directional && item.Enabled == true)
                {
                    PloobsEngine.Light.DirectionalLightPE dl = item as PloobsEngine.Light.DirectionalLightPE;

                    DirectionallightDirection.SetValue(dl.LightDirection);
                    DirectionalColor.SetValue(dl.Color.ToVector3());
                    DirectionallightIntensity.SetValue(dl.LightIntensity);

                    render.RenderFullScreenQuadVertexPixel(directionalLightEffect);
                }
            }
            render.SetSamplerState(s2, 2);
        }

        //EffectParameter PointcolorMap;
        //EffectParameter PointnormalMap;
        //EffectParameter PointdepthMap;
        EffectParameter PointProjection;
        EffectParameter PointView;
        EffectParameter PointInvertViewProjection;
        EffectParameter PointcameraPosition;

        EffectParameter PointWorld;
        EffectParameter PointlightPosition;
        EffectParameter PointColor;
        EffectParameter PointlightRadius;
        EffectParameter PointlightIntensity;
        EffectParameter Pointquadratic;

        

        protected void DrawPointLight(ICamera camera, IList<ILight> lights, IDeferredGBuffer DeferredGBuffer,RenderHelper render)
        {
            render.device.Textures[0] = DeferredGBuffer[GBufferTypes.COLOR];
            render.device.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
            render.device.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
            SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);

            PointProjection.SetValue(camera.Projection);
            PointView.SetValue(camera.View);
            PointcameraPosition.SetValue(camera.Position);
            PointInvertViewProjection.SetValue(Matrix.Invert(camera.ViewProjection));            

            foreach (ILight item in lights)
            {
                if (item.LightType == LightType.Deferred_Point && item.Enabled == true)
                {

                    PointLightPE pl = item as PointLightPE;
                    Matrix sphereWorldMatrix = Matrix.CreateScale(pl.LightRadius) * Matrix.CreateTranslation(pl.LightPosition);
                    
                    ContainmentType ct = ContainmentType.Contains;
                    if(cullPointLight)
                        ct = camera.BoundingFrustum.Contains(new BoundingSphere(pl.LightPosition, pl.LightRadius ));
                    if (ct == ContainmentType.Contains || ct == ContainmentType.Intersects)
                    {
                        PointWorld.SetValue(sphereWorldMatrix);
                        PointlightPosition.SetValue(pl.LightPosition);
                        PointColor.SetValue(pl.Color.ToVector3());
                        PointlightRadius.SetValue(pl.LightRadius);
                        PointlightIntensity.SetValue(pl.LightIntensity);
                        Pointquadratic.SetValue(pl.UsePointLightQuadraticAttenuation);

                        float cameraToCenter = Vector3.Distance(camera.Position, pl.LightPosition);

                        if (cameraToCenter < pl.LightRadius + camera.NearPlane)
                            render.PushRasterizerState(RasterizerState.CullClockwise);
                        else
                            render.PushRasterizerState(RasterizerState.CullCounterClockwise);

                        render.RenderBatch(sphereModel.GetBatchInformation(0)[0], pointLightEffect);

                        render.PopRasterizerState();
                    }
                  
                }
            }
            render.SetSamplerState(s2, 2);
        }

        protected void DrawnSpotLight(ICamera camera, IList<ILight> lights, IDeferredGBuffer DeferredGBuffer,RenderHelper render)
        {
            render.PushRasterizerState(RasterizerState.CullNone);
            render.PushDepthStencilState(DepthStencilState.None);            

            render.device.Textures[0] = DeferredGBuffer[GBufferTypes.COLOR];
            render.device.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
            render.device.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
            SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);

            spotLightEffect.Parameters["View"].SetValue(camera.View);
            spotLightEffect.Parameters["Projection"].SetValue(camera.Projection);
            spotLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);
            spotLightEffect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(camera.View * camera.Projection));
            
            
            foreach (ILight item in lights)
            {
                if (item.LightType == LightType.Deferred_Spot && item.Enabled == true)
                {
                    SpotLightPE sl = item as SpotLightPE;
                    spotLightEffect.Parameters["lightPosition"].SetValue(sl.Position);
                    spotLightEffect.Parameters["lightDirection"].SetValue(sl.Direction);
                    spotLightEffect.Parameters["lightRadius"].SetValue(sl.LightRadius);
                    spotLightEffect.Parameters["lightDecayExponent"].SetValue(sl.ConeDecay);
                    spotLightEffect.Parameters["Color"].SetValue(sl.Color.ToVector3());
                    spotLightEffect.Parameters["lightAngleCosine"].SetValue(sl.LightAngleCosine);
                    spotLightEffect.Parameters["lightIntensity"].SetValue(sl.LightIntensity);                    

                    render.RenderFullScreenQuadVertexPixel(spotLightEffect);                    
                }
            }

            render.PopDepthStencilState();
            render.PopRasterizerState();
            render.SetSamplerState(s2, 2);
        }       

        #endregion

#region IDeferredLightMap Members


        public Texture2D this[DeferredLightMapType type]
        {
            get {
                switch (type)
                {
                    case DeferredLightMapType.LIGHTMAP:
                        return lightRT;
                    default:
                        ActiveLogger.LogMessage("DeferredLightMapTypetype not present", LogLevel.FatalError);
                        throw new Exception("Type not present in this implementation");
                }
                
            
            }            
        }

        #endregion

#region IDeferredLightMap Members

        public void SetLightMap(RenderHelper render)
        {            
            render.PushRenderTarget(lightRT);
        }

        public void ResolveLightMap(RenderHelper render)
        {            
            render.PopRenderTarget();
        }

        #endregion

#region IDeferredLightMap Members


        public void DrawLights(GameTime gameTime, IWorld world, IDeferredGBuffer deferredGBuffer, RenderHelper render)
        {
            render.Clear(Color.Transparent,ClearOptions.Target);
            render.PushBlendState(BlendState.AlphaBlend);
            render.PushDepthStencilState(DepthStencilState.None);                        

            DrawDirectionalLight(world.CameraManager.ActiveCamera, world.Lights, deferredGBuffer,render);
            DrawPointLight(world.CameraManager.ActiveCamera, world.Lights, deferredGBuffer,render);
            DrawnSpotLight(world.CameraManager.ActiveCamera, world.Lights, deferredGBuffer,render);

            render.PopBlendState();
            render.PopDepthStencilState();
        }

        public void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, bool cullPointLight, bool useFloatingBufferForLightning)
        {
            this.ginfo = ginfo;
            this.cullPointLight = cullPointLight;
            if (useFloatingBufferForLightning)
            {
                lightRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
                samplerState = SamplerState.PointClamp;
            }
            else
            {
                lightRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
                samplerState = ginfo.SamplerState;
            }

            directionalLightEffect = manager.GetAsset<Effect>("DirectionalLight",true);
            pointLightEffect = manager.GetAsset<Effect>("PointLight",true);
            sphereModel = new SimpleModel(factory, "Dsphere", true); 
            spotLightEffect = manager.GetAsset<Effect>("SpotLight",true);
            
            spotLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
            pointLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);

            //DirectionalcolorMap = directionalLightEffect.Parameters["colorMap"];
            //DirectionalnormalMap = directionalLightEffect.Parameters["normalMap"];
            //DirectionaldepthMap = directionalLightEffect.Parameters["depthMap"];
            DirectionalInvertViewProjection = directionalLightEffect.Parameters["InvertViewProjection"];
            DirectionalhalfPixel = directionalLightEffect.Parameters["halfPixel"];
            DirectionalcameraPosition = directionalLightEffect.Parameters["cameraPosition"];
            

            DirectionallightDirection = directionalLightEffect.Parameters["lightDirection"];
            DirectionalColor = directionalLightEffect.Parameters["Color"];
            DirectionallightIntensity = directionalLightEffect.Parameters["lightIntensity"];


            //PointcolorMap = pointLightEffect.Parameters["colorMap"];
            //PointnormalMap = pointLightEffect.Parameters["normalMap"];
            //PointdepthMap = pointLightEffect.Parameters["depthMap"];
            PointProjection = pointLightEffect.Parameters["Projection"];
            PointView = pointLightEffect.Parameters["View"];
            PointInvertViewProjection = pointLightEffect.Parameters["InvertViewProjection"];

            PointWorld = pointLightEffect.Parameters["World"];
            PointlightPosition = pointLightEffect.Parameters["lightPosition"];
            PointColor = pointLightEffect.Parameters["Color"];
            PointlightRadius = pointLightEffect.Parameters["lightRadius"];
            PointlightIntensity = pointLightEffect.Parameters["lightIntensity"];
            Pointquadratic = pointLightEffect.Parameters["quadratic"];
            PointcameraPosition = pointLightEffect.Parameters["cameraPosition"];
            
        }

        #endregion
    }
}

#endif