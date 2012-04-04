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
    public class PreLightMap : IDeferredLightMap
    {   
        private RenderTarget2D lightRT;
        private Effect directionalLightEffect;
        private Effect pointLightEffect;
        private Effect spotLightEffect;
        private SimpleModel sphereModel;        
        private bool cullPointLight = true;
        GraphicInfo ginfo;
        SamplerState samplerState;


        EffectParameter DirectionalhalfPixel;
        EffectParameter DirectionallightDirection;
        EffectParameter DirectionalColor;
        EffectParameter DirectionallightIntensity;
        BlendState _lightAddBlendState;
        
#region IDeferredLightMap Members  
      

        private Vector3[] _cornersWorldSpace = new Vector3[8];
        private Vector3[] _cornersViewSpace = new Vector3[8];
        private Vector3[] _currentFrustumCorners = new Vector3[4];
        private Vector3[] _localFrustumCorners = new Vector3[4];

        /// <summary>
        /// Compute the frustum corners for a camera.
        /// Its used to reconstruct the pixel position using only the depth value.
        /// Read here for more information
        /// http://mynameismjp.wordpress.com/2009/03/10/reconstructing-position-from-depth/
        /// </summary>
        /// <param name="camera"> Current rendering camera </param>
        private void ComputeFrustumCorners(ICamera camera)
        {
            camera.BoundingFrustum.GetCorners(_cornersWorldSpace);
            Matrix matView = camera.View; //this is the inverse of our camera transform
            Vector3.Transform(_cornersWorldSpace, ref matView, _cornersViewSpace); //put the frustum into view space
            for (int i = 0; i < 4; i++) //take only the 4 farthest points
            {
                _currentFrustumCorners[i] = _cornersViewSpace[i + 4];
            }
            Vector3 temp = _currentFrustumCorners[3];
            _currentFrustumCorners[3] = _currentFrustumCorners[2];
            _currentFrustumCorners[2] = temp;
        }

        public void ApplyFrustumCorners(EffectParameter frustumCorners, Vector2 topLeftVertex, Vector2 bottomRightVertex)
        {
            float dx = _currentFrustumCorners[1].X - _currentFrustumCorners[0].X;
            float dy = _currentFrustumCorners[0].Y - _currentFrustumCorners[2].Y;

            _localFrustumCorners[0] = _currentFrustumCorners[2];
            _localFrustumCorners[0].X += dx * (topLeftVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[0].Y += dy * (bottomRightVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[1] = _currentFrustumCorners[2];
            _localFrustumCorners[1].X += dx * (bottomRightVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[1].Y += dy * (bottomRightVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[2] = _currentFrustumCorners[2];
            _localFrustumCorners[2].X += dx * (topLeftVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[2].Y += dy * (topLeftVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[3] = _currentFrustumCorners[2];
            _localFrustumCorners[3].X += dx * (bottomRightVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[3].Y += dy * (topLeftVertex.Y * 0.5f + 0.5f);

            frustumCorners.SetValue(_localFrustumCorners);
        }
        

        protected void DrawDirectionalLight(ICamera camera, IList<ILight> lights, IDeferredGBuffer DeferredGBuffer,RenderHelper render)
        {
            ComputeFrustumCorners(camera);
            ApplyFrustumCorners(directionalLightEffect.Parameters["FrustumCorners"], -Vector2.One, Vector2.One);

            render.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
            render.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
            SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);

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

        EffectParameter PointWordViewProjection;
        EffectParameter PointcameraPosition;
        EffectParameter PointlightPosition;
        EffectParameter PointColor;
        EffectParameter PointlightRadius;
        EffectParameter PointlightIntensity;
        EffectParameter Pointquadratic;

        

        protected void DrawPointLight(ICamera camera, IList<ILight> lights, IDeferredGBuffer DeferredGBuffer,RenderHelper render)
        {            
            render.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
            render.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
            SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);
            PointcameraPosition.SetValue(camera.Position);
            
            float _tanFovy = (float)Math.Tan(camera.FieldOfView /2);
            pointLightEffect.Parameters["TanAspect"].SetValue(new Vector2(_tanFovy * camera.AspectRatio, -_tanFovy));
            pointLightEffect.Parameters["farPlane"].SetValue(camera.FarPlane);
            
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

                        //convert light position into viewspace
                        Vector3 viewSpaceLPos = Vector3.Transform(pl.LightPosition,camera.View);

                        PointWordViewProjection.SetValue(sphereWorldMatrix * camera.ViewProjection);
                        PointlightPosition.SetValue(viewSpaceLPos);
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
                        
            render.Textures[1] = DeferredGBuffer[GBufferTypes.NORMAL];
            render.Textures[2] = DeferredGBuffer[GBufferTypes.DEPH];
            SamplerState s2 = render.SetSamplerState(SamplerState.PointClamp, 2);

            spotLightEffect.Parameters["View"].SetValue(camera.View);
            spotLightEffect.Parameters["Projection"].SetValue(camera.Projection);
            spotLightEffect.Parameters["cameraPosition"].SetValue(camera.Position);

            ApplyFrustumCorners(directionalLightEffect.Parameters["FrustumCorners"], -Vector2.One, Vector2.One);            
            
            foreach (ILight item in lights)
            {
                if (item.LightType == LightType.Deferred_Spot && item.Enabled == true)
                {
                    SpotLightPE sl = item as SpotLightPE;
                    Vector3 viewSpaceLPos = Vector3.Transform(sl.Position, camera.View);
                    Vector3 viewSpaceLDir = Vector3.Transform(sl.Direction, camera.View);

                    spotLightEffect.Parameters["lightPosition"].SetValue(viewSpaceLPos);
                    spotLightEffect.Parameters["lightDirection"].SetValue(viewSpaceLDir);
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
            render.Clear(Color.Black,ClearOptions.Target);
            render.PushBlendState(_lightAddBlendState);
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

            directionalLightEffect = manager.GetAsset<Effect>("PrePass2/DirectionalLight");
            pointLightEffect = manager.GetAsset<Effect>("PrePass2/PointLight");
            sphereModel = new SimpleModel(factory, "Models/Dsphere");
            spotLightEffect = manager.GetAsset<Effect>("PrePass2/SpotLight");
            
            spotLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
            pointLightEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);

            DirectionalhalfPixel = directionalLightEffect.Parameters["halfPixel"];
            

            DirectionallightDirection = directionalLightEffect.Parameters["lightDirection"];
            DirectionalColor = directionalLightEffect.Parameters["Color"];
            DirectionallightIntensity = directionalLightEffect.Parameters["lightIntensity"];

            PointWordViewProjection = pointLightEffect.Parameters["wvp"];
            PointlightPosition = pointLightEffect.Parameters["lightPosition"];
            PointColor = pointLightEffect.Parameters["Color"];
            PointlightRadius = pointLightEffect.Parameters["lightRadius"];
            PointlightIntensity = pointLightEffect.Parameters["lightIntensity"];
            Pointquadratic = pointLightEffect.Parameters["quadratic"];
            PointcameraPosition = pointLightEffect.Parameters["cameraPosition"];

            _lightAddBlendState = new BlendState()
            {
                AlphaSourceBlend = Blend.One,
                ColorSourceBlend = Blend.One,
                AlphaDestinationBlend = Blend.One,
                ColorDestinationBlend = Blend.One,
            };
        }

        #endregion
    }
}

#endif