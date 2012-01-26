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
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PloobsEngine.Light;
using PloobsEngine.Cameras;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Directional Shadow Filter Options
    /// </summary>
    public enum DirectionalShadowFilteringType
    {
        PCF2x2 = 0,
        PCF3x3 = 1,
        PCF5x5 = 2,
        PCF7x7 = 3
    }

	internal class DirectionalShadowRenderer
	{
        int shadowMapSize = 512;        
        const int NumSplits = 4;
        private float splitConstant = 0.95f;        
		
        RenderTarget2D shadowMap;
		Effect shadowMapEffect;
        RenderTarget2D shadowOcclusion;       
                

		Vector3[] frustumCornersVS = new Vector3[8];
        Vector3[] frustumCornersWS = new Vector3[8];
        Vector3[] frustumCornersLS = new Vector3[8];
        Vector3[] farFrustumCornersVS = new Vector3[4];
        Vector3[] splitFrustumCornersVS = new Vector3[8];
        Matrix[] lightViewProjectionMatrices = new Matrix[NumSplits];
        Matrix[] lightProjectionMatrices = new Matrix[NumSplits];
        Matrix[] lightViewMatrices = new Matrix[NumSplits];
        Vector2[] lightClipPlanes = new Vector2[NumSplits];
        float[] splitDepths = new float[NumSplits + 1];        
        bool showCascadeSplits = false;
        DirectionalShadowFilteringType filteringType = DirectionalShadowFilteringType.PCF3x3;
        EffectTechnique[] shadowOcclusionTechniques = new EffectTechnique[4];
        Viewport vp;

        /// <summary>
        /// Default 512
        /// </summary>
        public int ShadowMapSize
        {
            get { return shadowMapSize; }
            set { shadowMapSize = value; }
        }

        /// <summary>
        /// Default 0.95
        /// Tune the sizes of the splits (exponecial constant)
        /// </summary>
        public float SplitConstant
        {
            get { return splitConstant; }
            set { splitConstant = value; }
        }


        public DirectionalShadowFilteringType ShadowFilteringType
        {
            get { return filteringType; }
            set { filteringType = value; }
        }

        public bool ShowCascadeSplits
        {
            get { return showCascadeSplits; }
            set { showCascadeSplits = value; }
        }

        public DirectionalShadowRenderer() 
        {
        }

		/// <summary>
		/// Creates the renderer
		/// </summary>		
		public DirectionalShadowRenderer(int ShadowMapSize )
		{
            this.ShadowMapSize = ShadowMapSize;         
		}

        internal void Load(GraphicFactory factory,GraphicInfo ginfo)
        {
            // Load the effect we need
            shadowMapEffect = factory.GetEffect("ShadowMap",false,true);

            // Create the shadow map, using a 32-bit floating-point surface format
            shadowMap = factory.CreateRenderTarget(ShadowMapSize * NumSplits, ShadowMapSize, SurfaceFormat.Single,true,DepthFormat.Depth24Stencil8,ginfo.MultiSample);
            
            // Create the shadow occlusion texture using the same dimensions as the backbuffer
            shadowOcclusion = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);

            // We'll keep an array of EffectTechniques that will let us map a
            // ShadowFilteringType to a technique for calculating shadow occlusion
            shadowOcclusionTechniques[0] = shadowMapEffect.Techniques["CreateShadowTerm2x2PCF"];
            shadowOcclusionTechniques[1] = shadowMapEffect.Techniques["CreateShadowTerm3x3PCF"];
            shadowOcclusionTechniques[2] = shadowMapEffect.Techniques["CreateShadowTerm5x5PCF"];
            shadowOcclusionTechniques[3] = shadowMapEffect.Techniques["CreateShadowTerm7x7PCF"];
        }



        /// <summary>
        /// Renders a list of models to the shadow map, and returns a surface
        /// containing the shadow occlusion factor
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="render">The render.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="light">The light for which the shadow is being calculated</param>
        /// <param name="mainCamera">The camera viewing the scene containing the light</param>
        /// <param name="world">The world.</param>
        /// <param name="deferredGBuffer">The deferred G buffer.</param>
        /// <returns>
        /// The shadow occlusion texture
        /// </returns>
		internal RenderTarget2D Render(GameTime gameTime, RenderHelper render,GraphicInfo ginfo, DirectionalLightPE light,
                                    ICamera mainCamera, IWorld world, IDeferredGBuffer deferredGBuffer)
									
		{
                vp = render.GetViewPort();
				// Set our targets								
				render.PushRenderTarget(shadowMap);
                render.Clear(Color.White,ClearOptions.Target, 1.0f, 0);
                render.Clear(Color.Black, ClearOptions.DepthBuffer, 1.0f, 0);                

				// Get corners of the main camera's bounding frustum
				Matrix cameraTransform, viewMatrix;                				                
                viewMatrix = mainCamera.View;
                cameraTransform = Matrix.Invert(viewMatrix);
				mainCamera.BoundingFrustum.GetCorners(frustumCornersWS);
				Vector3.Transform(frustumCornersWS, ref viewMatrix, frustumCornersVS);
				for (int i = 0; i < 4; i++)
					farFrustumCornersVS[i] = frustumCornersVS[i + 4];

                // Calculate the cascade splits.  We calculate these so that each successive
                // split is larger than the previous, giving the closest split the most amount
                // of shadow detail.  
                float N = NumSplits;
                float near = mainCamera.NearPlane, far = mainCamera.FarPlane;
                splitDepths[0] = near;
                splitDepths[NumSplits] = far;                
                for (int i = 1; i < splitDepths.Length - 1; i++)
                    splitDepths[i] = splitConstant * near * (float)Math.Pow(far / near, i / N) + (1.0f - splitConstant) * ((near + (i / N)) * (far - near));

                // Render our scene geometry to each split of the cascade
                for (int i = 0; i < NumSplits; i++)
                {
                    float minZ = splitDepths[i];
                    float maxZ = splitDepths[i + 1];

                    CalculateFrustum(light, mainCamera, minZ, maxZ, out lightProjectionMatrices[i], out lightViewMatrices[i],out lightViewProjectionMatrices[i]);                    

                    RenderShadowMap(gameTime,render, i,world);
                }

                render.PopRenderTargetAsSingleRenderTarget2D();

                render.SetViewPort(ginfo.Viewport);
				RenderShadowOcclusion(render,mainCamera,light,deferredGBuffer);                
				return shadowOcclusion;						
			
		}

        /// <summary>
        /// Determines the size of the frustum needed to cover the viewable area,
        /// then creates an appropriate orthographic projection.
        /// </summary>
        /// <param name="light">The directional light to use</param>
        /// <param name="mainCamera">The camera viewing the scene</param>
        /// <param name="minZ">The min Z.</param>
        /// <param name="maxZ">The max Z.</param>
        /// <param name="Projection">The projection.</param>
        /// <param name="view">The view.</param>
        /// <param name="ViewProjection">The view projection.</param>
        protected void CalculateFrustum(DirectionalLightPE light, ICamera mainCamera, float minZ, float maxZ, out Matrix Projection, out Matrix view, out Matrix ViewProjection)
		{
            // Shorten the view frustum according to the shadow view distance
            Matrix cameraMatrix = Matrix.Invert(mainCamera.View);            

            for (int i = 0; i < 4; i++)
                splitFrustumCornersVS[i] = frustumCornersVS[i + 4] * (minZ / mainCamera.FarPlane);

            for (int i = 4; i < 8; i++)
                splitFrustumCornersVS[i] = frustumCornersVS[i] * (maxZ / mainCamera.FarPlane);

            Vector3.Transform(splitFrustumCornersVS, ref cameraMatrix, frustumCornersWS);

			// Find the centroid
			Vector3 frustumCentroid = new Vector3(0,0,0);
			for (int i = 0; i < 8; i++)
				frustumCentroid += frustumCornersWS[i];
			frustumCentroid /= 8;

			// Position the shadow-caster camera so that it's looking at the centroid,
			// and backed up in the direction of the sunlight
            float distFromCentroid = MathHelper.Max((maxZ - minZ), Vector3.Distance(splitFrustumCornersVS[4], splitFrustumCornersVS[5])) + 50;
			view = Matrix.CreateLookAt(frustumCentroid - (light.LightDirection * distFromCentroid), frustumCentroid, new Vector3(0,1,0));

			// Determine the position of the frustum corners in light space
            Vector3.Transform(frustumCornersWS, ref view, frustumCornersLS);

			// Calculate an orthographic projection by sizing a bounding box 
			// to the frustum coordinates in light space
			Vector3 mins = frustumCornersLS[0];
			Vector3 maxes = frustumCornersLS[0];
			for (int i = 0; i < 8; i++)
			{
				if (frustumCornersLS[i].X > maxes.X)
					maxes.X = frustumCornersLS[i].X;
				else if (frustumCornersLS[i].X < mins.X)
					mins.X = frustumCornersLS[i].X;
				if (frustumCornersLS[i].Y > maxes.Y)
					maxes.Y = frustumCornersLS[i].Y;
				else if (frustumCornersLS[i].Y < mins.Y)
					mins.Y = frustumCornersLS[i].Y;
				if (frustumCornersLS[i].Z > maxes.Z)
					maxes.Z = frustumCornersLS[i].Z;
				else if (frustumCornersLS[i].Z < mins.Z)
					mins.Z = frustumCornersLS[i].Z;
			}     

            // Create an orthographic camera for use as a shadow caster            
            Projection = Matrix.CreateOrthographicOffCenter(mins.X, maxes.X, mins.Y, maxes.Y, -maxes.Z - light.NearClipOffset, -mins.Z);
            ViewProjection = view * Projection;
            
		}

        /// <summary>
        /// Renders the shadow map using the orthographic camera created in
        /// CalculateFrustum.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="render">The render.</param>
        /// <param name="splitIndex">Index of the split.</param>
        /// <param name="world">The world.</param>
        protected void RenderShadowMap(GameTime gameTime, RenderHelper render, int splitIndex, IWorld world)
		{
            // Set the viewport for the current split            
            Viewport splitViewport = new Viewport();
            splitViewport.MinDepth = 0;
            splitViewport.MaxDepth = 1;
            splitViewport.Width = ShadowMapSize;
            splitViewport.Height = ShadowMapSize;
            splitViewport.X = splitIndex * ShadowMapSize;
            splitViewport.Y = 0;
            render.SetViewPort(splitViewport);

            // Set up the effect
            //shadowMapEffect.CurrentTechnique = shadowMapEffect.Techniques["GenerateShadowMap"];            
            //shadowMapEffect.Parameters["g_matViewProj"].SetValue(lightViewProjectionMatrices[splitIndex]);
                        
            // Draw the models            
            render.RenderSceneDepth(world, gameTime, lightViewMatrices[splitIndex], lightProjectionMatrices[splitIndex], true);
		}

		/// <summary>
		/// Renders a texture containing the final shadow occlusion
		/// </summary>
        protected void RenderShadowOcclusion(RenderHelper render, ICamera mainCamera,DirectionalLightPE dl,  IDeferredGBuffer deferredGBuffer)
		{
			// Set the device to render to our shadow occlusion texture, 
			render.PushRenderTarget(shadowOcclusion);			

            Matrix cameraTransform = Matrix.Invert(mainCamera.View);

            // We'll use these clip planes to determine which split a pixel belongs to
            for (int i = 0; i < NumSplits; i++)
            {
                lightClipPlanes[i].X = -splitDepths[i];
                lightClipPlanes[i].Y = -splitDepths[i + 1];
                                
                //lightCameras[i].GetViewProjMatrix(out lightViewProjectionMatrices[i]);
            }                         

			// Setup the Effect
			shadowMapEffect.CurrentTechnique = shadowOcclusionTechniques[(int)filteringType];
			shadowMapEffect.Parameters["g_matInvView"].SetValue(cameraTransform);
            shadowMapEffect.Parameters["g_matLightViewProj"].SetValue(lightViewProjectionMatrices);
			shadowMapEffect.Parameters["g_vFrustumCornersVS"].SetValue(farFrustumCornersVS);
            shadowMapEffect.Parameters["g_vClipPlanes"].SetValue(lightClipPlanes);
			shadowMapEffect.Parameters["ShadowMap"].SetValue(shadowMap);
			shadowMapEffect.Parameters["DepthTexture"].SetValue(deferredGBuffer[GBufferTypes.DEPH]);
            shadowMapEffect.Parameters["g_matInvProj"].SetValue(Matrix.Invert(mainCamera.Projection));
			shadowMapEffect.Parameters["g_vOcclusionTextureSize"].SetValue(new Vector2(shadowOcclusion.Width, shadowOcclusion.Height));
			shadowMapEffect.Parameters["g_vShadowMapSize"].SetValue(new Vector2(shadowMap.Width, shadowMap.Height));
            shadowMapEffect.Parameters["g_bShowSplitColors"].SetValue(showCascadeSplits);
            shadowMapEffect.Parameters["BIAS"].SetValue(dl.SHADOWBIAS);
            
			// Draw the full screen quad		
            render.RenderFullScreenQuadVertexPixel(shadowMapEffect);

            render.PopRenderTargetAsSingleRenderTarget2D();
            //DeferredDebugImageRender dir2 = new DeferredDebugImageRender(new Rectangle(250, 0, 250, 250), shadowOcclusion.GetTexture());
            //DeferredRenderTechnic.DebugImages.Add(dir2);

		}
	}

}
#endif