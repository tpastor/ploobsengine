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
    /// <summary>
    /// When to restore the depth Buffer
    /// REMEMBER, the forward pass is performed when the Depth Buffer is restored
    /// </summary>
    public enum RestoreDepthOption
    {
        /// <summary>
        /// Dont restore
        /// </summary>
        NONE,
        /// <summary>
        /// Before applying the post effects
        /// The Forward will be affected by the Post Effects
        /// </summary>
        BEFORE_POSTEFFECT,
        /// <summary>
        /// After the Post effects
        /// The Forward pass wont be affected by the Post Effects
        /// </summary>
        AFTER_POSTEFFECT
    }

    /// <summary>
    /// Deferred Render Technic Init Description
    /// </summary>
    public struct DeferredRenderTechnicInitDescription
    {
        /// <summary>
        /// Defaults this instance.
        /// </summary>
        /// <returns></returns>
        public static DeferredRenderTechnicInitDescription Default()
        {
            return new DeferredRenderTechnicInitDescription(new GBuffer(),new LightMap(),new FinalCombination(Color.Black),new ForwardPass(ForwardPassDescription.Default()),Color.Black,false,false,true,true,true,new string[] { PrincipalConstants.CurrentImage,PrincipalConstants.colorRT,PrincipalConstants.normalRt,PrincipalConstants.lightRt  } ,RestoreDepthOption.BEFORE_POSTEFFECT);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredRenderTechnicInitDescription"/> struct.
        /// </summary>
        /// <param name="DeferredGBuffer">The deferred G buffer.</param>
        /// <param name="DeferredLightMap">The deferred light map.</param>
        /// <param name="DeferredFinalCombination">The deferred final combination.</param>
        /// <param name="ForwardPass">The forward pass.</param>
        /// <param name="BackGroundColor">Color of the back ground.</param>        
        /// <param name="LightDebug">if set to <c>true</c> [light debug].</param>
        /// <param name="DefferedDebug">if set to <c>true</c> [deffered debug].</param>
        /// <param name="UseFloatingBufferForLightMap">if set to <c>true</c> [use floating buffer for light map].</param>
        /// <param name="CullPointLight">if set to <c>true</c> [cull point light].</param>
        /// <param name="ExtraForwardPass">if set to <c>true</c> [extra forward pass].</param>
        /// <param name="RenderTargetsNameToDefferedDebug">The render targets name to deffered debug.</param>
        /// <param name="RestoreDepthOption">The restore depth option.</param>
        public DeferredRenderTechnicInitDescription(IDeferredGBuffer DeferredGBuffer, IDeferredLightMap DeferredLightMap, IDeferredFinalCombination DeferredFinalCombination,
            ForwardPass ForwardPass, Color BackGroundColor, bool LightDebug, bool DefferedDebug, bool UseFloatingBufferForLightMap,
            bool CullPointLight, bool ExtraForwardPass, String[] RenderTargetsNameToDefferedDebug, RestoreDepthOption RestoreDepthOption)
        {            
            this.DefferedDebug = DefferedDebug;
            this.UseFloatingBufferForLightMap = UseFloatingBufferForLightMap;
            this.LightDebug = LightDebug;
            this.CullPointLight = CullPointLight;
            this.ExtraForwardPass = ExtraForwardPass;
            this.DeferredGBuffer = DeferredGBuffer;
            this.DeferredLightMap = DeferredLightMap;
            this.DeferredFinalCombination = DeferredFinalCombination;        
            this.RestoreDepthOption = RestoreDepthOption;        
            this.ForwardPass = ForwardPass;
            this.RenderTargetsNameToDefferedDebug = RenderTargetsNameToDefferedDebug;
            this.BackGroundColor = BackGroundColor;
            OrderAllObjectsBeforeDraw = null;
            OrderDeferredObjectsBeforeDraw = null;
            OrderForwardObjectsBeforeDraw = null;
        }
        
        /// <summary>
        /// not yet avaliable
        /// </summary>
        public bool LightDebug ;
        /// <summary>
        /// Cant be change at runtime (only in the creation time)
        /// </summary>
        public bool DefferedDebug ;
        /// <summary>
        /// Cant be change at runtime (only in the creation time)
        /// </summary>
        public bool UseFloatingBufferForLightMap ;
        public bool CullPointLight;
        public bool ExtraForwardPass;
        public IDeferredGBuffer DeferredGBuffer;
        public IDeferredLightMap DeferredLightMap;
        public IDeferredFinalCombination DeferredFinalCombination;
        public RestoreDepthOption RestoreDepthOption;
        public ForwardPass ForwardPass;
        /// <summary>
        /// cant be changed at runtime
        /// </summary>
        public String[] RenderTargetsNameToDefferedDebug;
        public Color BackGroundColor;

        /// <summary>
        /// Function called all frames to order all objects that are not culled
        /// Use this to sort objects by material and minimize gpu state changes
        /// </summary>
        public Func<List<IObject>,IWorld, List<IObject>> OrderAllObjectsBeforeDraw;
        /// <summary>
        /// Function called all frames to order all Deferred objects that are not culled
        /// Use this to sort objects by material and minimize gpu state changes
        /// </summary>
        public Func<List<IObject>, IWorld, List<IObject>> OrderDeferredObjectsBeforeDraw;
        /// <summary>
        /// Function called all frames to order all Forward objects that are not culled
        /// Use this to sort objects by material and minimize gpu state changes
        /// </summary>
        public Func<List<IObject>, IWorld, List<IObject>> OrderForwardObjectsBeforeDraw;

    }

    /// <summary>
    /// Implementation of the Deferred Render Technic
    /// </summary>
    public class DeferredRenderTechnic : IRenderTechnic
    {
        public DeferredRenderTechnic(DeferredRenderTechnicInitDescription desc) : base(PostEffectType.Deferred)
        {
            this.desc = desc;        
            deferredGBuffer = desc.DeferredGBuffer;
            deferredLightMap = desc.DeferredLightMap;
            deferredFinalCombination  = desc.DeferredFinalCombination;
            forwardPass = desc.ForwardPass;
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

        DeferredRenderTechnicInitDescription desc;        
        private IDeferredGBuffer deferredGBuffer ;
        private IDeferredLightMap deferredLightMap ;
        private IDeferredFinalCombination deferredFinalCombination ;
        private IForwardPass forwardPass ;
        private RenderTarget2D target;
        private RenderTarget2D target2; ///for ping pong with target       
        private RenderTarget2D PostEffectTarget;                                       
        private RestoreDepth restoreDepth;
        private GraphicInfo ginfo;
        int halfWidth;
        int halfHeight;

        /// <summary>
        /// Not all confs can be changed at runtime (ex: cant enable deferred debug. You can only start with it enabled)
        /// </summary>
        public DeferredRenderTechnicInitDescription DeferredRenderTechnicInitDescription
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

            if (desc.UseFloatingBufferForLightMap)
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


            deferredGBuffer.LoadContent(manager, ginfo, factory, desc.BackGroundColor, desc.UseFloatingBufferForLightMap);
            deferredLightMap.LoadContent(manager, ginfo, factory, desc.CullPointLight,desc.UseFloatingBufferForLightMap);
            deferredFinalCombination.LoadContent(manager, ginfo, factory, desc.UseFloatingBufferForLightMap, desc.ExtraForwardPass);


            if (desc.UseFloatingBufferForLightMap)
            {
                target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample);
                target2 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample);
            }
            else
            {
                target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight,SurfaceFormat.Color,ginfo.UseMipMap,DepthFormat.Depth24Stencil8,ginfo.MultiSample);
                target2 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample);
            }

            PostEffectTarget = target;
            halfWidth = ginfo.Viewport.Width / 2;
            halfHeight = ginfo.Viewport.Height / 2;
            restoreDepth = new RestoreDepth(desc.UseFloatingBufferForLightMap, manager, factory, ginfo);
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
            List<IObject> DeferrednotCulledObjectsList = world.Culler.GetNotCulledObjectsList(MaterialType.DEFERRED, CullerComparer.ComparerFrontToBack,world.CameraManager.ActiveCamera.Position);
            List<IObject> ForwardnotCulledObjectsList = world.Culler.GetNotCulledObjectsList(MaterialType.FORWARD, CullerComparer.ComparerBackToFront, world.CameraManager.ActiveCamera.Position);

            if (desc.OrderAllObjectsBeforeDraw != null)
                AllnotCulledObjectsList = desc.OrderAllObjectsBeforeDraw(AllnotCulledObjectsList,world);

            if (desc.OrderDeferredObjectsBeforeDraw != null)
                DeferrednotCulledObjectsList = desc.OrderDeferredObjectsBeforeDraw(DeferrednotCulledObjectsList, world);

            if (desc.OrderForwardObjectsBeforeDraw != null)
                ForwardnotCulledObjectsList = desc.OrderForwardObjectsBeforeDraw(ForwardnotCulledObjectsList, world);

            render.SetSamplerStates(ginfo.SamplerState);
            render.DettachBindedTextures();

            deferredGBuffer.PreDrawScene(gameTime, world, render, ginfo, AllnotCulledObjectsList);

            render.SetSamplerStates(ginfo.SamplerState);
            render.DettachBindedTextures();

            deferredGBuffer.SetGBuffer(render);            
            deferredGBuffer.ClearGBuffer(render);
            deferredGBuffer.DrawScene(gameTime, world, render, ginfo, DeferrednotCulledObjectsList);
            deferredGBuffer.ResolveGBuffer(render);

            render.DettachBindedTextures();
            render.ValidateSamplerStates();            

            deferredLightMap.SetLightMap(render);
            deferredLightMap.DrawLights(gameTime, world, deferredGBuffer, render);
            deferredLightMap.ResolveLightMap(render);

            render.DettachBindedTextures(5);
            render.ValidateSamplerStates();

            deferredFinalCombination.SetFinalCombination(render);
            deferredFinalCombination.DrawScene(gameTime, world, deferredGBuffer, deferredLightMap,render);

            render.DettachBindedTextures(3);
            render.ValidateSamplerStates();

            if (desc.ExtraForwardPass)
            {
                render[PrincipalConstants.colorRT] = deferredGBuffer[GBufferTypes.COLOR];
                render[PrincipalConstants.normalRt] = deferredGBuffer[GBufferTypes.NORMAL];
                render[PrincipalConstants.lightRt] = deferredLightMap[DeferredLightMapType.LIGHTMAP];
                render[PrincipalConstants.DephRT] = deferredGBuffer[GBufferTypes.DEPH];
                render[PrincipalConstants.extra1RT] = deferredGBuffer[GBufferTypes.Extra1];
                render[PrincipalConstants.CombinedImage] = deferredFinalCombination[GBufferTypes.FINALIMAGE];
                render[PrincipalConstants.CurrentImage] = deferredFinalCombination[GBufferTypes.FINALIMAGE];

                if (desc.RestoreDepthOption == RestoreDepthOption.BEFORE_POSTEFFECT)
                {
                    restoreDepth.PerformForwardPass(render[PrincipalConstants.CombinedImage], render[PrincipalConstants.DephRT], render,ginfo);

                    render.DettachBindedTextures(2);
                    render.ValidateSamplerStates();

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

                    forwardPass.Draw(gameTime, world, render,DeferrednotCulledObjectsList,ForwardnotCulledObjectsList);

                    render.DettachBindedTextures(6);
                    render.ValidateSamplerStates();

                    render.RenderPosWithDepthComponents(gameTime, ref view, ref projection);

                    render.DettachBindedTextures(6);
                    render.ValidateSamplerStates();

                    render[PrincipalConstants.CurrentImage] = restoreDepth.EndForwardPass(render);
                    render[PrincipalConstants.CombinedImage] = render[PrincipalConstants.CurrentImage];

                    for (int i = 0; i < PostEffects.Count; i++)
                    {
                        if (PostEffects[i].Enabled)
                        {
                            render.PushRenderTarget(PostEffectTarget);
                            render.Clear(Color.Black);
                            PostEffects[i].Draw(render[PrincipalConstants.CurrentImage], render, gameTime, ginfo, world, desc.UseFloatingBufferForLightMap);
                            Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                            render[PrincipalConstants.CurrentImage] = tex;
                            SwapTargetBuffers();                            
                        }
                    }

                    render.SetSamplerStates(ginfo.SamplerState);
                    render.DettachBindedTextures(6);
                                        
                    if (desc.UseFloatingBufferForLightMap)
                    {                        
                        render.Clear(Color.Black);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp);                     
                    }
                    else
                    {                        
                        render.Clear(Color.Black);                        
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity,null,true,SpriteSortMode.Deferred,ginfo.SamplerState);                    
                    }                    
                }
                else if (desc.RestoreDepthOption == RestoreDepthOption.AFTER_POSTEFFECT)
                {                    
                    for (int i = 0; i < PostEffects.Count; i++)
                    {
                        if (PostEffects[i].Enabled)
                        {
                            render.PushRenderTarget(PostEffectTarget);
                            render.Clear(Color.Black);
                            PostEffects[i].Draw(render[PrincipalConstants.CurrentImage], render, gameTime, ginfo, world, desc.UseFloatingBufferForLightMap);
                            Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                            render[PrincipalConstants.CurrentImage] = tex;
                            SwapTargetBuffers();
                        }
                    }

                    render.SetSamplerStates(ginfo.SamplerState);
                    render.DettachBindedTextures(16);

                    restoreDepth.PerformForwardPass(render[PrincipalConstants.CurrentImage], render[PrincipalConstants.DephRT], render, ginfo);

                    render.DettachBindedTextures(2);

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

                    render.DettachBindedTextures(16);
                    forwardPass.Draw(gameTime, world, render,DeferrednotCulledObjectsList,ForwardnotCulledObjectsList);

                    render.DettachBindedTextures(16);
                    render.RenderPosWithDepthComponents(gameTime, ref view, ref projection);
                    render.DettachBindedTextures(16);

                    render[PrincipalConstants.CurrentImage] = restoreDepth.EndForwardPass(render);

                    if (desc.UseFloatingBufferForLightMap)
                    {                        
                        System.Diagnostics.Debug.Assert(render.PeekBlendState() == BlendState.Opaque);
                        render.Clear(Color.Black);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp);                     
                        System.Diagnostics.Debug.Assert(render.PeekBlendState() == BlendState.Opaque);
                    }
                    else
                    {
                        render.Clear(Color.Black);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, ginfo.SamplerState);                                             
                    }
                
                }
                else if (desc.RestoreDepthOption == RestoreDepthOption.NONE)
                {
                    for (int i = 0; i < PostEffects.Count ; i++)
                    {
                        if (PostEffects[i].Enabled)
                        {
                            render.PushRenderTarget(PostEffectTarget);
                            render.Clear(Color.Black);
                            PostEffects[i].Draw(render[PrincipalConstants.CurrentImage], render, gameTime, ginfo, world, desc.UseFloatingBufferForLightMap);
                            Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                            System.Diagnostics.Debug.Assert(tex != null);
                            render[PrincipalConstants.CurrentImage] = tex;
                            SwapTargetBuffers();
                        }
                    }
                    
                    if (desc.UseFloatingBufferForLightMap)
                    {
                        render.Clear(Color.Black);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp);                     
                    }
                    else
                    {
                        render.Clear(Color.Black);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, ginfo.SamplerState);                     
                    }
                }                                

                render.RenderPosComponents(gameTime, ref view, ref projection);
        
                if (desc.DefferedDebug)
                {
                    render.Clear(Color.Black);                    
                    if (desc.UseFloatingBufferForLightMap)
                        render.RenderBegin(Matrix.Identity,null,SpriteSortMode.Immediate,SamplerState.PointClamp);
                    else
                        render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Immediate, ginfo.SamplerState);
                    
                    render.RenderTexture(render[desc.RenderTargetsNameToDefferedDebug[0]], Color.White, new Rectangle(0, 0, halfWidth, halfHeight));
                    render.RenderTexture(render[desc.RenderTargetsNameToDefferedDebug[1]], Color.White, new Rectangle(0, halfHeight, halfWidth, halfHeight));
                    render.RenderTexture(render[desc.RenderTargetsNameToDefferedDebug[2]], Color.White, new Rectangle(halfWidth, 0, halfWidth, halfHeight));
                    render.RenderTexture(render[desc.RenderTargetsNameToDefferedDebug[3]], Color.White, new Rectangle(halfWidth, halfHeight, halfWidth, halfHeight));                    
                    
                    render.RenderEnd();
                }                
                
            }
            else
            {
                render.RenderPosComponents(gameTime, ref view, ref projection);
            }

            render.ValidateSamplerStates();
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