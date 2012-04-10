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
#if !WINDOWS_PHONE && !REACH && !XBOX360
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Material;
using PloobsEngine.Light;
using PloobsEngine.Cameras;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    public enum ShadowFilter
    {
        NONE, PCF3x3,PCF7x7SOFT
    }


    public class ShadowLightMap : IDeferredLightMap
    {
        public ShadowLightMap(ShadowFilter SpotShadowFilter = ShadowFilter.PCF7x7SOFT, int SpotShadowBufferSize = 1024, DirectionalShadowFilteringType DirectionalFilteringType = DirectionalShadowFilteringType.PCF7x7, int DirectionalShadowMapSize = 512, float CascadeSplitConstant = 0.95f)
        {
            this.shadowFilterSpot = SpotShadowFilter;
            this.shadownBufferSize = SpotShadowBufferSize;
            this.filteringType = DirectionalFilteringType;
            this.shadowMapSize = DirectionalShadowMapSize;
            this.splitConstant = CascadeSplitConstant;
        }

        DirectionalShadowFilteringType filteringType = DirectionalShadowFilteringType.PCF7x7;
        int shadowMapSize = 512;
        float splitConstant = 0.95f;
        GraphicInfo ginfo;
        private RenderTarget2D lightRT;
        private RenderTarget2D shadowRT;
        private RenderTarget2D deferredRT;
        private Effect directionalLightEffect;
        private Effect pointLightEffect;
        private Effect spotLightEffect;
        private Effect Depth;
        private SimpleModel sphereModel;        
        private Texture2D shadowMap;                
        private Texture2D content;
        private Texture2D blank;     
        private ShadowFilter shadowFilterSpot = ShadowFilter.NONE;                        
        RenderTarget2D rt;
        DirectionalShadowRenderer shadow;
        private int shadownBufferSize;
        
#region IDeferredLightMap Members

        private void DrawDirectionalLight(RenderHelper render, GraphicInfo ginfo, ICamera camera, DirectionalLightPE dl, IDeferredGBuffer DeferredGBuffer)
        {    
            directionalLightEffect.Parameters["shadowBufferSize"].SetValue(shadownBufferSize);
            if(dl.CastShadown)
                //directionalLightEffect.Parameters["xShadowMap"].SetValue(shadowMap);
                render.device.Textures[3] = shadowMap;
            else
                //directionalLightEffect.Parameters["xShadowMap"].SetValue(blank);
                render.device.Textures[3] = blank;

            //directionalLightEffect.Parameters["colorMap"].SetValue(DeferredGBuffer[GBufferTypes.COLOR]);
            //directionalLightEffect.Parameters["normalMap"].SetValue(DeferredGBuffer[GBufferTypes.NORMAL]);
            //directionalLightEffect.Parameters["depthMap"].SetValue(DeferredGBuffer[GBufferTypes.DEPH]);            

            render.device.Textures[0] = DeferredGBuffer[GBufferTypes.COLOR];
            render.device.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
            render.device.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
            SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);
            SamplerState s3 = render.SetSamplerState(SamplerState.PointClamp, 3);


            directionalLightEffect.Parameters["View"].SetValue(camera.View);
            directionalLightEffect.Parameters["Projection"].SetValue(camera.Projection);
            directionalLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);
            directionalLightEffect.Parameters["shadown"].SetValue(dl.CastShadown);
            directionalLightEffect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(camera.ViewProjection));
            directionalLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
            directionalLightEffect.Parameters["lightDirection"].SetValue(dl.LightDirection);
            directionalLightEffect.Parameters["Color"].SetValue(dl.Color.ToVector3());
            directionalLightEffect.Parameters["lightIntensity"].SetValue(dl.LightIntensity);            
            render.PushDepthStencilState(DepthStencilState.None);
            render.RenderFullScreenQuadVertexPixel(directionalLightEffect);
            render.PopDepthStencilState();

            render.SetSamplerState(s2, 2);
            render.SetSamplerState(s3, 3);
           }

        private void DrawPointLight(RenderHelper render, GraphicInfo ginfo, ICamera camera, PointLightPE pl, IDeferredGBuffer DeferredGBuffer,bool cullPointLight)
        {

                    pointLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
                    render.device.Textures[0] = DeferredGBuffer[GBufferTypes.COLOR];
                    render.device.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
                    render.device.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
                    SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);

                    pointLightEffect.Parameters["Projection"].SetValue(camera.Projection);
                    pointLightEffect.Parameters["View"].SetValue(camera.View);
                    pointLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);
                    pointLightEffect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(camera.ViewProjection));

                    
                    Matrix sphereWorldMatrix = Matrix.CreateScale(pl.LightRadius) * Matrix.CreateTranslation(pl.LightPosition);

                    ContainmentType ct = ContainmentType.Contains;
                    if (cullPointLight)
                        ct = camera.BoundingFrustum.Contains(new BoundingSphere(pl.LightPosition, pl.LightRadius));
                    if (ct == ContainmentType.Contains || ct == ContainmentType.Intersects)
                    {
                        pointLightEffect.Parameters["World"].SetValue(sphereWorldMatrix);
                        pointLightEffect.Parameters["lightPosition"].SetValue(pl.LightPosition);
                        pointLightEffect.Parameters["Color"].SetValue(pl.Color.ToVector3());
                        pointLightEffect.Parameters["lightRadius"].SetValue(pl.LightRadius);
                        pointLightEffect.Parameters["lightIntensity"].SetValue(pl.LightIntensity);
                        pointLightEffect.Parameters["quadratic"].SetValue(pl.UsePointLightQuadraticAttenuation);

                        float cameraToCenter = Vector3.Distance(camera.Position, pl.LightPosition);

                        if (cameraToCenter < pl.LightRadius + camera.NearPlane)
                            render.PushRasterizerState(RasterizerState.CullClockwise);
                        else
                            render.PushRasterizerState(RasterizerState.CullCounterClockwise);

                        render.RenderBatch(sphereModel.GetBatchInformation(0)[0], pointLightEffect);

                        render.PopRasterizerState();
                    }

                    render.SetSamplerState(s2, 2);
          }                    

        private void DrawnSpotLight(RenderHelper render, GraphicInfo ginfo, ICamera camera, SpotLightPE sl, IDeferredGBuffer DeferredGBuffer)
        {
                    //if(sl.CastShadown)
                    //    spotLightEffect.Parameters["xShadowMap"].SetValue(shadowMap);
                    //else
                    //    spotLightEffect.Parameters["xShadowMap"].SetValue(blank);

                    if (sl.CastShadown)
                        render.device.Textures[3] = shadowMap;
                    else
                        render.device.Textures[3] = blank;

                    spotLightEffect.Parameters["shadowBufferSize"].SetValue(shadownBufferSize);
                    spotLightEffect.Parameters["BIAS"].SetValue(sl.SHADOWBIAS);                    

                    //spotLightEffect.Parameters["colorMap"].SetValue(DeferredGBuffer[GBufferTypes.COLOR]);
                    //spotLightEffect.Parameters["normalMap"].SetValue(DeferredGBuffer[GBufferTypes.NORMAL]);
                    //spotLightEffect.Parameters["depthMap"].SetValue(DeferredGBuffer[GBufferTypes.DEPH]);            
                    render.device.Textures[0] = DeferredGBuffer[GBufferTypes.COLOR];
                    render.device.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
                    render.device.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
                    SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);
                    SamplerState s3 = render.SetSamplerState(SamplerState.PointClamp, 3);

                    spotLightEffect.Parameters["xLightViewProjection"].SetValue(sl.ViewMatrix * sl.ProjMatrix);
                    spotLightEffect.Parameters["View"].SetValue(camera.View);
                    spotLightEffect.Parameters["Projection"].SetValue(camera.Projection);
                    spotLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);
                    spotLightEffect.Parameters["shadown"].SetValue(sl.CastShadown);
                    spotLightEffect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(camera.ViewProjection));
                    spotLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
                    spotLightEffect.Parameters["lightPosition"].SetValue(sl.Position);
                    spotLightEffect.Parameters["lightDirection"].SetValue(sl.Direction);
                    spotLightEffect.Parameters["lightRadius"].SetValue(sl.LightRadius);
                    spotLightEffect.Parameters["lightDecayExponent"].SetValue(sl.ConeDecay);
                    spotLightEffect.Parameters["Color"].SetValue(sl.Color.ToVector3());
                    spotLightEffect.Parameters["lightAngleCosine"].SetValue(sl.LightAngleCosine);
                    spotLightEffect.Parameters["lightIntensity"].SetValue(sl.LightIntensity);
                    render.PushDepthStencilState(DepthStencilState.None);     
                    render.RenderFullScreenQuadVertexPixel(spotLightEffect);
                    render.PopDepthStencilState();
                    render.SetSamplerState(s2, 2);
                    render.SetSamplerState(s3, 3);
        }

        private void RenderShadowMap(GameTime gt, RenderHelper render, ref Matrix view, ref Matrix proj, IWorld world, IDeferredGBuffer deferredGBuffer)
        {
            render.PushRenderTarget(shadowRT);             
            render.Clear(Color.Transparent,ClearOptions.Target | ClearOptions.DepthBuffer,1,0);
            render.RenderSceneDepth(world, gt, ref view, ref  proj, true);
            shadowMap = render.PopRenderTargetAsSingleRenderTarget2D();
        }

        public void DrawLights(GameTime gameTime, IWorld world, IDeferredGBuffer deferredGBuffer, RenderHelper render)
        {
            render.Clear(Color.Transparent, ClearOptions.Target);

            foreach (ILight light in world.Lights.Where((a) => a.CastShadown == true && a.Enabled == true))
            {
                switch (light.LightType)
                {
                    case LightType.Deferred_Directional:
                        DirectionalLightPE dl = light as DirectionalLightPE;
                        shadowMap = shadow.Render(gameTime, render, ginfo, dl, world.CameraManager.ActiveCamera, world, deferredGBuffer);

                        render.PushBlendState(BlendState.AlphaBlend);
                        DrawDirectionalLight(render, ginfo, world.CameraManager.ActiveCamera, dl, deferredGBuffer);
                        render.PopBlendState();

                        break;
                    case LightType.Deferred_Point:
#if WINDOWS
                        System.Diagnostics.Debug.Fail("Point Light Shadow not supported, in production no error will be created, the light just wont cast any shadow");
#endif
                        render.PushBlendState(BlendState.AlphaBlend);
                        DrawPointLight(render, ginfo, world.CameraManager.ActiveCamera, light as PointLightPE, deferredGBuffer, true);
                        render.PopBlendState();
                        break;
                    case LightType.Deferred_Spot:
                        SpotLightPE sl = light as SpotLightPE;
                        Matrix v = sl.ViewMatrix;
                        Matrix p =sl.ProjMatrix;
                        RenderShadowMap(gameTime, render, ref v, ref p, world, deferredGBuffer);
                        render.PushBlendState(BlendState.AlphaBlend);
                        DrawnSpotLight(render, ginfo, world.CameraManager.ActiveCamera, sl, deferredGBuffer);
                        render.PopBlendState();
                        break;
                    default:
                        throw new Exception("Light type Unexpected");
                }
            }

            render.DettachBindedTextures();
            render.SetSamplerStates(ginfo.SamplerState);

            render.PushBlendState(BlendState.AlphaBlend);

            foreach (ILight light in world.Lights.Where((a) => a.CastShadown != true && a.Enabled == true))
            {
                switch (light.LightType)
                {
                    case LightType.Deferred_Directional:
                        DirectionalLightPE dl = light as DirectionalLightPE;                        
                        DrawDirectionalLight(render,ginfo, world.CameraManager.ActiveCamera, dl, deferredGBuffer);
                        break;
                    case LightType.Deferred_Point:                        
                        DrawPointLight(render,ginfo, world.CameraManager.ActiveCamera, light as PointLightPE, deferredGBuffer,true);
                        break;
                    case LightType.Deferred_Spot:
                        SpotLightPE sl = light as SpotLightPE;                        
                        DrawnSpotLight(render,ginfo, world.CameraManager.ActiveCamera, sl, deferredGBuffer);
                        break;
                    default:
                        throw new Exception("Light type Unexpected");
                }
            }      
                  
            render.PopBlendState();            
        }

        public void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, bool cullPointLight, bool useFloatingBufferForLightning)
        {
            this.ginfo = ginfo;
            shadow = new DirectionalShadowRenderer();
            shadow.ShadowFilteringType = filteringType;
            shadow.ShadowMapSize = shadowMapSize;
            shadow.SplitConstant = splitConstant;
            
            shadownBufferSize = ginfo.BackBufferWidth;
            shadowRT = factory.CreateRenderTarget(shadownBufferSize, shadownBufferSize, SurfaceFormat.Single, ginfo.UseMipMap,DepthFormat.Depth24Stencil8,ginfo.MultiSample);

            if (useFloatingBufferForLightning)
                lightRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample, RenderTargetUsage.PreserveContents);
            else
                lightRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample, RenderTargetUsage.PreserveContents);

            Depth = factory.GetEffect("ShadowDepth",false,true);
            pointLightEffect = factory.GetEffect("PointLight",false,true);
            directionalLightEffect = factory.GetEffect("ShadowDirectionalCascade",false,true);
            spotLightEffect = factory.GetEffect("ShadowSpot",false,true);
            sphereModel = new SimpleModel(factory, "Dsphere", true);
            blank = factory.CreateTexture2DColor(1, 1, Color.White);

            rt = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight,SurfaceFormat.Color,ginfo.UseMipMap,DepthFormat.Depth24Stencil8,ginfo.MultiSample);
            deferredRT = factory.CreateRenderTarget(shadownBufferSize, shadownBufferSize, SurfaceFormat.Single,ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample);
            shadow.Load(factory,ginfo);

            switch (shadowFilterSpot)
            {
                case ShadowFilter.NONE:
                    spotLightEffect.CurrentTechnique = spotLightEffect.Techniques["Technique1"];
                    break;                
                case ShadowFilter.PCF3x3:
                    spotLightEffect.CurrentTechnique = spotLightEffect.Techniques["Technique2"];
                    break;             
                case ShadowFilter.PCF7x7SOFT:
                    spotLightEffect.CurrentTechnique = spotLightEffect.Techniques["Technique3"];
                    break;
                default:
                    break;
            }

        }

#endregion

#region IDeferredLightMap Members


        public Texture2D this[DeferredLightMapType type]
        {
            get
            {
                switch (type)
                {
                    case DeferredLightMapType.LIGHTMAP:
                        return content;
                    default:
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
            content = render.PopRenderTargetAsSingleRenderTarget2D();
        }

#endregion

        
    }
}


#endif