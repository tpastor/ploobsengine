#if !WINDOWS_PHONE
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
        
        #region IDeferredLightMap Members        
        protected void DrawDirectionalLight(ICamera camera, IList<ILight> lights, IDeferredGBuffer DeferredGBuffer,RenderHelper render)
        {
            directionalLightEffect.Parameters["colorMap"].SetValue(DeferredGBuffer[GBufferTypes.COLOR]);
            directionalLightEffect.Parameters["normalMap"].SetValue(DeferredGBuffer[GBufferTypes.NORMAL]);
            directionalLightEffect.Parameters["depthMap"].SetValue(DeferredGBuffer[GBufferTypes.DEPH]);
            directionalLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);
            directionalLightEffect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(camera.ViewProjection));
            directionalLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
                        
            foreach (ILight item in lights)
            {
                if (item.LightType == LightType.Deferred_Directional && item.Enabled == true)
                {
                    PloobsEngine.Light.DirectionalLightPE dl = item as PloobsEngine.Light.DirectionalLightPE;

                    directionalLightEffect.Parameters["lightDirection"].SetValue(dl.LightDirection);
                    directionalLightEffect.Parameters["Color"].SetValue(dl.Color.ToVector3());
                    directionalLightEffect.Parameters["lightIntensity"].SetValue(dl.LightIntensity);

                    render.RenderFullScreenQuadVertexPixel(directionalLightEffect);
                }
            }
        }

        protected void DrawPointLight(ICamera camera, IList<ILight> lights, IDeferredGBuffer DeferredGBuffer,RenderHelper render)
        {

            pointLightEffect.Parameters["colorMap"].SetValue(DeferredGBuffer[GBufferTypes.COLOR]);
            pointLightEffect.Parameters["normalMap"].SetValue(DeferredGBuffer[GBufferTypes.NORMAL]);
            pointLightEffect.Parameters["depthMap"].SetValue(DeferredGBuffer[GBufferTypes.DEPH]);
            pointLightEffect.Parameters["Projection"].SetValue(camera.Projection);
            pointLightEffect.Parameters["View"].SetValue(camera.View);
            pointLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);
            pointLightEffect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(camera.ViewProjection));            

            foreach (ILight item in lights)
            {
                if (item.LightType == LightType.Deferred_Point && item.Enabled == true)
                {

                    PointLightPE pl = item as PointLightPE;
                    Matrix sphereWorldMatrix = Matrix.CreateScale(pl.LightRadius) * Matrix.CreateTranslation(pl.LightPosition);
                    
                    ContainmentType ct = ContainmentType.Contains;
                    if(cullPointLight)
                        ct = camera.BoundingFrustum.Contains(new BoundingSphere(pl.LightPosition, pl.LightRadius * 1.5f));
                    if (ct == ContainmentType.Contains || ct == ContainmentType.Intersects)
                    {
                        pointLightEffect.Parameters["World"].SetValue(sphereWorldMatrix);
                        pointLightEffect.Parameters["lightPosition"].SetValue(pl.LightPosition);
                        pointLightEffect.Parameters["Color"].SetValue(pl.Color.ToVector3());
                        pointLightEffect.Parameters["lightRadius"].SetValue(pl.LightRadius);
                        pointLightEffect.Parameters["lightIntensity"].SetValue(pl.LightIntensity);
                        pointLightEffect.Parameters["quadratic"].SetValue(pl.UsePointLightQuadraticAttenuation);

                        float cameraToCenter = Vector3.Distance(camera.Position, pl.LightPosition);

                        if (cameraToCenter < pl.LightRadius)
                            render.PushRasterizerState(RasterizerState.CullClockwise);
                        else
                            render.PushRasterizerState(RasterizerState.CullCounterClockwise);
                            
                        render.RenderBatch(sphereModel.GetBatchInformation(0)[0],pointLightEffect);

                        render.PopRasterizerState();
                    }
                }
            }
        }

        protected void DrawnSpotLight(ICamera camera, IList<ILight> lights, IDeferredGBuffer DeferredGBuffer,RenderHelper render)
        {
            render.PushRasterizerState(RasterizerState.CullNone);
            render.PushDepthStencilState(DepthStencilState.None);            

            spotLightEffect.Parameters["colorMap"].SetValue(DeferredGBuffer[GBufferTypes.COLOR]);
            spotLightEffect.Parameters["normalMap"].SetValue(DeferredGBuffer[GBufferTypes.NORMAL]);
            spotLightEffect.Parameters["depthMap"].SetValue(DeferredGBuffer[GBufferTypes.DEPH]);
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

            render.SetSamplerState(samplerState, 0);
            render.SetSamplerState(samplerState, 1);

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
            
        }

        #endregion
    }
}

#endif