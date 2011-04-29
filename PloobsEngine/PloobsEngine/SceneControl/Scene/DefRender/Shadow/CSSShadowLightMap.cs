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
        NONE, PCF2x2, PCF3x3, PCF2x2Variance, PCF3x3BLUR, PCF5x5BLUR
    }

    public class CSSShadowLightMap : IDeferredLightMap
    {
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
        RenderHelper rh;
        RenderTarget2D rt;
        ShadowRenderer shadow = new ShadowRenderer();        


        public ShadowRenderer DirectionalShadowRender
        {
            get { return shadow; }
            set { shadow = value; }
        }
        
        public ShadowFilter SpotShadowFilter
        {
            get { return shadowFilterSpot; }
            set { shadowFilterSpot = value; }
        }        
        
        private int shadownBufferSize ;
        
        public int SpotShadowBufferSize
        {
            get { return shadownBufferSize; }
            set { shadownBufferSize = value; }
        }

        #region IDeferredLightMap Members

        private void DrawDirectionalLight(RenderHelper render, GraphicInfo ginfo, ICamera camera, DirectionalLightPE dl, IDeferredGBuffer DeferredGBuffer)
        {    
            directionalLightEffect.Parameters["shadowBufferSize"].SetValue(shadownBufferSize);
            if(dl.CastShadown)
                directionalLightEffect.Parameters["xShadowMap"].SetValue(shadowMap);
            else
                directionalLightEffect.Parameters["xShadowMap"].SetValue(blank);

            directionalLightEffect.Parameters["colorMap"].SetValue(DeferredGBuffer[GBufferTypes.COLOR]);
            directionalLightEffect.Parameters["normalMap"].SetValue(DeferredGBuffer[GBufferTypes.NORMAL]);
            directionalLightEffect.Parameters["depthMap"].SetValue(DeferredGBuffer[GBufferTypes.DEPH]);            
            directionalLightEffect.Parameters["View"].SetValue(camera.View);
            directionalLightEffect.Parameters["Projection"].SetValue(camera.Projection);
            directionalLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);
            directionalLightEffect.Parameters["shadown"].SetValue(dl.CastShadown);
            directionalLightEffect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(camera.ViewProjection));
            directionalLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
            directionalLightEffect.Parameters["lightDirection"].SetValue(dl.LightDirection);
            directionalLightEffect.Parameters["Color"].SetValue(dl.Color.ToVector3());
            directionalLightEffect.Parameters["lightIntensity"].SetValue(dl.LightIntensity);            
            render.PushDepthState(DepthStencilState.None);
            render.RenderFullScreenQuadVertexPixel(directionalLightEffect);
            render.PopDepthStencilState();
           }

        private void DrawPointLight(RenderHelper render, GraphicInfo ginfo, ICamera camera, PointLightPE pl, IDeferredGBuffer DeferredGBuffer,bool cullPointLight)
        {
            pointLightEffect.Parameters["colorMap"].SetValue(DeferredGBuffer[GBufferTypes.COLOR]);
            pointLightEffect.Parameters["normalMap"].SetValue(DeferredGBuffer[GBufferTypes.NORMAL]);
            pointLightEffect.Parameters["depthMap"].SetValue(DeferredGBuffer[GBufferTypes.DEPH]);
            pointLightEffect.Parameters["Projection"].SetValue(camera.Projection);
            pointLightEffect.Parameters["View"].SetValue(camera.View);
            pointLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);
            pointLightEffect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(camera.ViewProjection));

                    
                    Matrix sphereWorldMatrix = Matrix.CreateScale(pl.LightRadius) * Matrix.CreateTranslation(pl.LightPosition);

                    ContainmentType ct = ContainmentType.Contains;
                    if (cullPointLight)
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

                        render.RenderBatch(ref sphereModel.GetBatchInformation(0)[0], pointLightEffect);

                        render.PopRasterizerState();
                    }
          }                    


        private void DrawnSpotLight(RenderHelper render, GraphicInfo ginfo, ICamera camera, SpotLightPE sl, IDeferredGBuffer DeferredGBuffer)
        {
                    spotLightEffect.Parameters["shadowBufferSize"].SetValue(shadownBufferSize);
                    spotLightEffect.Parameters["BIAS"].SetValue(sl.SHADOWBIAS);
                    spotLightEffect.Parameters["xShadowMap"].SetValue(shadowMap);
                    spotLightEffect.Parameters["colorMap"].SetValue(DeferredGBuffer[GBufferTypes.COLOR]);
                    spotLightEffect.Parameters["normalMap"].SetValue(DeferredGBuffer[GBufferTypes.NORMAL]);
                    spotLightEffect.Parameters["depthMap"].SetValue(DeferredGBuffer[GBufferTypes.DEPH]);            
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
                    render.PushDepthState(DepthStencilState.None);     
                    render.RenderFullScreenQuadVertexPixel(spotLightEffect);
                    render.PopDepthStencilState();
                }


        protected void DrawScene(GameTime gameTime, IWorld world, Matrix View, Matrix proj, RenderHelper render)
        {
            world.Culler.StartFrame(View, proj, new BoundingFrustum(View * proj));

            foreach (IObject item in world.Culler.GetNotCulledObjectsList(null))
            {
                item.Material.Shadder.DepthExtractor(gameTime, item, View, proj, render);
            }
        }
        private void RenderShadowMap(GameTime gt, RenderHelper render, Matrix view, Matrix proj, IWorld world, IDeferredGBuffer deferredGBuffer)
        {
            render.PushRenderTarget(shadowRT);             
            render.Clear(Color.Transparent,ClearOptions.Target | ClearOptions.DepthBuffer,1,0);            
            DrawScene(gt,world,view,proj,render);
            shadowMap = render.PopRenderTargetAsSingleRenderTarget2D();
        }

        public void DrawLights(GameTime gameTime, IWorld world, IDeferredGBuffer deferredGBuffer, RenderHelper render)
        {
            render.Clear(Color.Transparent, ClearOptions.Target);

            foreach (ILight light in world.Lights.Where((a) => a.CastShadown == true))
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
                        render.PushBlendState(BlendState.AlphaBlend);
                        DrawPointLight(render, ginfo, world.CameraManager.ActiveCamera, light as PointLightPE, deferredGBuffer, true);
                        render.PopBlendState();
                        break;
                    case LightType.Deferred_Spot:
                        SpotLightPE sl = light as SpotLightPE;
                        RenderShadowMap(gameTime, render, sl.ViewMatrix, sl.ProjMatrix, world, deferredGBuffer);
                        render.PushBlendState(BlendState.AlphaBlend);
                        DrawnSpotLight(render, ginfo, world.CameraManager.ActiveCamera, sl, deferredGBuffer);
                        render.PopBlendState();
                        break;
                    default:
                        throw new Exception("Light type Unexpected");
                }
            }

            render.PushBlendState(BlendState.AlphaBlend);       

            foreach (ILight light in world.Lights.Where((a) => a.CastShadown != true))
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
            shadownBufferSize = ginfo.BackBufferWidth;
            shadowRT = factory.CreateRenderTarget(shadownBufferSize, shadownBufferSize, SurfaceFormat.Single,true);

            if (useFloatingBufferForLightning)
                lightRT = factory.CreateRenderTarget(ginfo.BackBufferWidth,ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable,false,DepthFormat.None,0,RenderTargetUsage.PreserveContents);
            else
                lightRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, false, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);

            Depth = factory.GetEffect("ShadowDepth",false,true);
            pointLightEffect = factory.GetEffect("PointLight",false,true);
            directionalLightEffect = factory.GetEffect("ShadowDirectionalCascade",false,true);
            spotLightEffect = factory.GetEffect("ShadowSpot",false,true);
            sphereModel = new SimpleModel(factory, "Dsphere", true);
            blank = factory.CreateTexture2DColor(1, 1, Color.White);

            rt = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            deferredRT = factory.CreateRenderTarget(shadownBufferSize, shadownBufferSize,SurfaceFormat.Single);
            shadow.Load(factory,ginfo);

            switch (shadowFilterSpot)
            {
                case ShadowFilter.NONE:
                    spotLightEffect.CurrentTechnique = spotLightEffect.Techniques["Technique1"];
                    break;
                case ShadowFilter.PCF2x2:
                    spotLightEffect.CurrentTechnique = spotLightEffect.Techniques["Technique1"];
                    break;
                case ShadowFilter.PCF3x3:
                    spotLightEffect.CurrentTechnique = spotLightEffect.Techniques["Technique2"];
                    break;
                case ShadowFilter.PCF2x2Variance:
                    spotLightEffect.CurrentTechnique = spotLightEffect.Techniques["Technique1"];
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


