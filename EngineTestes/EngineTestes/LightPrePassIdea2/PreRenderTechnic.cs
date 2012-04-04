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
using PloobsEngine.DataStructure;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Material;

namespace PloobsEngine.SceneControl
{

    public struct PrePassRenderTechnicInitDescription
    {
        /// <summary>
        /// Defaults this instance.
        /// </summary>
        /// <returns></returns>
        public static PrePassRenderTechnicInitDescription Default()
        {
            return new PrePassRenderTechnicInitDescription(new PreGBuffer(), new PreLightMap(), Color.Black, false, false, true, true, new string[] { PrincipalConstants.CurrentImage, PrincipalConstants.colorRT, PrincipalConstants.normalRt, PrincipalConstants.lightRt }, RestoreDepthOption.BEFORE_POSTEFFECT);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredRenderTechnicInitDescription"/> struct.
        /// </summary>
        /// <param name="DeferredGBuffer">The deferred G buffer.</param>
        /// <param name="DeferredLightMap">The deferred light map.</param>
        /// <param name="BackGroundColor">Color of the back ground.</param>
        /// <param name="LightDebug">if set to <c>true</c> [light debug].</param>
        /// <param name="DefferedDebug">if set to <c>true</c> [deffered debug].</param>
        /// <param name="UseFloatingBufferForLightMap">if set to <c>true</c> [use floating buffer for light map].</param>
        /// <param name="CullPointLight">if set to <c>true</c> [cull point light].</param>
        /// <param name="ExtraForwardPass">if set to <c>true</c> [extra forward pass].</param>
        /// <param name="RenderTargetsNameToDefferedDebug">The render targets name to deffered debug.</param>
        /// <param name="RestoreDepthOption">The restore depth option.</param>
        public PrePassRenderTechnicInitDescription(IDeferredGBuffer DeferredGBuffer, IDeferredLightMap DeferredLightMap,Color BackGroundColor, bool LightDebug, bool DefferedDebug,
            bool CullPointLight, bool ExtraForwardPass, String[] RenderTargetsNameToDefferedDebug, RestoreDepthOption RestoreDepthOption)
        {
            this.DefferedDebug = DefferedDebug;
            this.LightDebug = LightDebug;
            this.CullPointLight = CullPointLight;
            this.ExtraForwardPass = ExtraForwardPass;
            this.DeferredGBuffer = DeferredGBuffer;
            this.DeferredLightMap = DeferredLightMap;
            this.RenderTargetsNameToDefferedDebug = RenderTargetsNameToDefferedDebug;
            this.BackGroundColor = BackGroundColor;
            OrderAllObjectsBeforeDraw = null;
            OrderDeferredObjectsBeforeDraw = null;
            OrderForwardObjectsBeforeDraw = null;
        }

        /// <summary>
        /// not yet avaliable
        /// </summary>
        public bool LightDebug;
        /// <summary>
        /// Cant be change at runtime (only in the creation time)
        /// </summary>
        public bool DefferedDebug;
        public bool CullPointLight;
        public bool ExtraForwardPass;
        public IDeferredGBuffer DeferredGBuffer;
        public IDeferredLightMap DeferredLightMap;
        /// <summary>
        /// cant be changed at runtime
        /// </summary>
        public String[] RenderTargetsNameToDefferedDebug;
        public Color BackGroundColor;

        /// <summary>
        /// Function called all frames to order all objects that are not culled
        /// Use this to sort objects by material and minimize gpu state changes
        /// </summary>
        public Func<List<IObject>, List<IObject>> OrderAllObjectsBeforeDraw;
        /// <summary>
        /// Function called all frames to order all Deferred objects that are not culled
        /// Use this to sort objects by material and minimize gpu state changes
        /// </summary>
        public Func<List<IObject>, List<IObject>> OrderDeferredObjectsBeforeDraw;
        /// <summary>
        /// Function called all frames to order all Forward objects that are not culled
        /// Use this to sort objects by material and minimize gpu state changes
        /// </summary>
        public Func<List<IObject>, List<IObject>> OrderForwardObjectsBeforeDraw;

    }

    /// <summary>
    /// Implementation of the Deferred Render Technic
    /// </summary>
    public class PreRenderTechnic : IRenderTechnic
    {
        public PreRenderTechnic(PrePassRenderTechnicInitDescription desc)
            : base(PostEffectType.Deferred)
        {
            this.desc = desc;
            deferredGBuffer = new PreGBuffer();
            deferredLightMap = new PreLightMap();
            if (desc.LightDebug)
                ActiveLogger.LogMessage("LighDebug is not implemented yet, will be disabled", LogLevel.Warning);

            if (desc.DefferedDebug)
            {
                if (desc.RenderTargetsNameToDefferedDebug != null)
                {
                    if (desc.RenderTargetsNameToDefferedDebug.Count() != 4)
                    {
                        ActiveLogger.LogMessage("RenderTargetsNameToDefferedDebug must be 4, Deferred Degug Disabled", LogLevel.RecoverableError);
                        desc.DefferedDebug = false;
                    }
                }
                else
                {
                    ActiveLogger.LogMessage("RenderTargetsNameToDefferedDebug must be 4, Deferred Degub Disabled", LogLevel.RecoverableError);
                    desc.DefferedDebug = false;
                }
            }
        }

        PrePassRenderTechnicInitDescription desc;        
        private IDeferredGBuffer deferredGBuffer ;
        private IDeferredLightMap deferredLightMap ;
        private RenderTarget2D scenerender;
        private RenderTarget2D target;
        private RenderTarget2D target2; ///for ping pong with target       
        private RenderTarget2D PostEffectTarget;                                               
        private GraphicInfo ginfo;
        int halfWidth;
        int halfHeight;

        /// <summary>
        /// Not all confs can be changed at runtime (ex: cant enable deferred debug. You can only start with it enabled)
        /// </summary>
        public PrePassRenderTechnicInitDescription DeferredRenderTechnicInitDescription
        {
            get { return desc; }            
        }

        private void SwapTargetBuffers()
        {
            if (PostEffectTarget == target)
                PostEffectTarget = target2;
            else
            PostEffectTarget = target;
        }        

        protected override void  AfterLoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            this.ginfo = ginfo;

            {
                if (ginfo.CheckIfRenderTargetFormatIsSupported(SurfaceFormat.HdrBlendable, DepthFormat.Depth24Stencil8, ginfo.UseMipMap, ginfo.MultiSample) == false)
                {
                    throw new NotSupportedException("The plataform does not support the specified Render target Combination for Deferred Rendering, check the logs for more info");
                }               
            }

            if (ginfo.CheckIfRenderTargetFormatIsSupported(SurfaceFormat.Color, DepthFormat.Depth24Stencil8, ginfo.UseMipMap, ginfo.MultiSample) == false)
            {
                throw new NotSupportedException("The plataform does not support the specified Render target Combination for Deferred Rendering, check the logs for more info");
            }

            if (ginfo.CheckIfRenderTargetFormatIsSupported(SurfaceFormat.Single, DepthFormat.Depth24Stencil8, false, 0) == false)
            {
                throw new NotSupportedException("The plataform does not support the specified Render target Combination for Deferred Rendering, check the logs for more info");
            }

            if (ginfo.CheckIfRenderTargetFormatIsSupported(SurfaceFormat.Single, DepthFormat.Depth24Stencil8, ginfo.UseMipMap, ginfo.MultiSample) == false)
            {
                ActiveLogger.LogMessage("Shadow can behave strange, you dont have the minimum requirements", LogLevel.Warning);
            }


            deferredGBuffer.LoadContent(manager, ginfo, factory, desc.BackGroundColor, true);
            deferredLightMap.LoadContent(manager, ginfo, factory, desc.CullPointLight,true);
            
            target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample);
            target2 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample);
            scenerender = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample);

            PostEffectTarget = target;
            halfWidth = ginfo.Viewport.Width / 2;
            halfHeight = ginfo.Viewport.Height / 2;        
        }

        /// <summary>
        /// Executes the technic.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render">The render.</param>
        /// <param name="world">The world.</param>
        protected override void ExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world)
        {
            Draw(gameTime, world, render);
        }       


        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="world">The world.</param>
        /// <param name="render">The render.</param>
        protected void Draw(GameTime gameTime, IWorld world, RenderHelper render)
        {
            Matrix view = world.CameraManager.ActiveCamera.View;
            Matrix projection = world.CameraManager.ActiveCamera.Projection;

            world.Culler.StartFrame(ref view, ref projection, world.CameraManager.ActiveCamera.BoundingFrustum);
            List<IObject> AllnotCulledObjectsList = world.Culler.GetNotCulledObjectsList(null);
            List<IObject> DeferrednotCulledObjectsList = world.Culler.GetNotCulledObjectsList(MaterialType.DEFERRED);
            List<IObject> ForwardnotCulledObjectsList = world.Culler.GetNotCulledObjectsList(MaterialType.FORWARD);

            if (desc.OrderAllObjectsBeforeDraw != null)
                AllnotCulledObjectsList = desc.OrderAllObjectsBeforeDraw(AllnotCulledObjectsList);

            if (desc.OrderDeferredObjectsBeforeDraw != null)
                DeferrednotCulledObjectsList = desc.OrderDeferredObjectsBeforeDraw(DeferrednotCulledObjectsList);

            if (desc.OrderForwardObjectsBeforeDraw != null)
                ForwardnotCulledObjectsList = desc.OrderForwardObjectsBeforeDraw(ForwardnotCulledObjectsList);

            render.SetSamplerStates(ginfo.SamplerState);
            render.DettachBindedTextures();

            deferredGBuffer.PreDrawScene(gameTime, world, render, ginfo, AllnotCulledObjectsList);

            render.SetSamplerStates(ginfo.SamplerState);
            render.DettachBindedTextures();

            deferredGBuffer.SetGBuffer(render);            
            deferredGBuffer.ClearGBuffer(render);
            deferredGBuffer.DrawScene(gameTime, world, render, ginfo, DeferrednotCulledObjectsList);
            deferredGBuffer.ResolveGBuffer(render);

            render[PrincipalConstants.DephRT] = deferredGBuffer[GBufferTypes.DEPH];
            render[PrincipalConstants.normalRt] = deferredGBuffer[GBufferTypes.NORMAL];

            render.DettachBindedTextures();
            render.ValidateSamplerStates();            

            deferredLightMap.SetLightMap(render);
            deferredLightMap.DrawLights(gameTime, world, deferredGBuffer, render);
            deferredLightMap.ResolveLightMap(render);
            render[PrincipalConstants.lightRt] = deferredLightMap[DeferredLightMapType.LIGHTMAP];

            render.DettachBindedTextures(5);
            render.ValidateSamplerStates();

            render.PushRenderTarget(scenerender);
            render.Clear(desc.BackGroundColor);
            foreach (IObject item in AllnotCulledObjectsList)
            {
                if (item.Material.IsVisible)
                    item.Material.PosDrawnPhase(gameTime, item, world.CameraManager.ActiveCamera, world.Lights, render);
            }

            render.DettachBindedTextures(3);
            render.ValidateSamplerStates();


            render[PrincipalConstants.colorRT] = deferredGBuffer[GBufferTypes.COLOR];
            render[PrincipalConstants.normalRt] = deferredGBuffer[GBufferTypes.NORMAL];
            render[PrincipalConstants.lightRt] = deferredLightMap[DeferredLightMapType.LIGHTMAP];
            render[PrincipalConstants.DephRT] = deferredGBuffer[GBufferTypes.DEPH];
            render[PrincipalConstants.extra1RT] = deferredGBuffer[GBufferTypes.Extra1];
            
                        
                if (world.PhysicWorld.isDebugDraw)
                {
                    world.PhysicWorld.iDebugDrawn(render, gameTime, world.CameraManager.ActiveCamera);
                }
                if (world.ParticleManager != null)
                {
                    world.ParticleManager.iDraw(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, render);
                    render.ResyncStates();
                    render.SetSamplerStates(ginfo.SamplerState);
                }

                render.DettachBindedTextures(6);
                render.ValidateSamplerStates();            
                
                render.RenderPosWithDepthComponents(gameTime, ref view, ref projection);

                render.DettachBindedTextures(6);
                render.ValidateSamplerStates();

                render[PrincipalConstants.CurrentImage] = render[PrincipalConstants.CombinedImage] = render.PopRenderTargetAsSingleRenderTarget2D();

                for (int i = 0; i < PostEffects.Count; i++)
                {
                    if (PostEffects[i].Enabled)
                    {
                        render.PushRenderTarget(PostEffectTarget);
                        render.Clear(Color.Black);
                        PostEffects[i].Draw(render[PrincipalConstants.CurrentImage], render, gameTime, ginfo, world, true);
                        Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                        render[PrincipalConstants.CurrentImage] = tex;
                        SwapTargetBuffers();
                    }
                }

                render.SetSamplerStates(ginfo.SamplerState);
                render.DettachBindedTextures(6);

                render.Clear(Color.Black);
                render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp);            

        }

#region IRenderTechnic Members

        /// <summary>
        /// Gets the name of the technic.
        /// </summary>
        /// <value>
        /// The name of the technic.
        /// </value>
        public override string  TechnicName
        {
	        get { return "DeferredTechnic"; }
        }
        
        
        #endregion


        public override void CleanUp()
        {
            if(target!= null)
                target.Dispose();
            if(PostEffectTarget!= null)
                PostEffectTarget.Dispose();
            if(target2!= null)
                target2.Dispose();

            for (int i = 0; i < PostEffects.Count; i++)
            {
                PostEffects[i].CleanUp();
            }
        }
    }    

}

#endif