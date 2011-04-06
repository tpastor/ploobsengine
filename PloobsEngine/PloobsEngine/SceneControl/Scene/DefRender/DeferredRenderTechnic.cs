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
            return new DeferredRenderTechnicInitDescription(new GBuffer(),new LightMap(),new FinalCombination(Color.Transparent),new ForwardPass(false),Color.Black,false,false,false,true,true,true,new string[] { PrincipalConstants.CurrentImage,PrincipalConstants.colorRT,PrincipalConstants.normalRt,PrincipalConstants.lightRt  } ,RestoreDepthOption.BEFORE_POSTEFFECT);
        }

        public DeferredRenderTechnicInitDescription(IDeferredGBuffer DeferredGBuffer, IDeferredLightMap DeferredLightMap, IDeferredFinalCombination DeferredFinalCombination,
            ForwardPass ForwardPass, Color BackGroundColor, bool PhysicDebug, bool LightDebug, bool DefferedDebug, bool UseFloatingBufferForLightMap,
            bool CullPointLight, bool ExtraForwardPass, String[] RenderTargetsNameToDefferedDebug, RestoreDepthOption RestoreDepthOption)
        {
            this.PhysicDebug = PhysicDebug;            
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
        }

        public bool PhysicDebug ;
        public bool LightDebug ;
        public bool DefferedDebug ;
        public bool UseFloatingBufferForLightMap ;
        public bool CullPointLight;
        public bool ExtraForwardPass;
        public IDeferredGBuffer DeferredGBuffer;
        public IDeferredLightMap DeferredLightMap;
        public IDeferredFinalCombination DeferredFinalCombination;
        public RestoreDepthOption RestoreDepthOption;
        public ForwardPass ForwardPass;
        public String[] RenderTargetsNameToDefferedDebug;
        public Color BackGroundColor;

    }

    /// <summary>
    /// Implementation of the Deferred Render Technic
    /// </summary>
    public class DeferredRenderTechnic : IRenderTechnic
    {

        public DeferredRenderTechnic(DeferredRenderTechnicInitDescription desc)
        {
            this.desc = desc;        
            deferredGBuffer = desc.DeferredGBuffer;
            deferredLightMap = desc.DeferredLightMap;
            deferredFinalCombination  = desc.DeferredFinalCombination;
            forwardPass = desc.ForwardPass;
            if (desc.LightDebug)
                ActiveLogger.LogMessage("LighDebug is not implemented yet", LogLevel.Warning);

            if (desc.DefferedDebug)
            {
                if (desc.RenderTargetsNameToDefferedDebug != null)
                {
                    if (desc.RenderTargetsNameToDefferedDebug.Count() != 4)
                    {
                        ActiveLogger.LogMessage("RenderTargetsNameToDefferedDebug must be 4, Deferred Degub Disabled", LogLevel.RecoverableError);
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

        private PriorityQueueB<IPostEffect> PostEffects = new PriorityQueueB<IPostEffect>(new PostEffectComparer());                       
        
        private RenderTarget2D target;        
        private RestoreDepth restoreDepth;
        private GraphicInfo ginfo;
        int halfWidth;
        int halfHeight;

        protected override void  AfterLoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            this.ginfo = ginfo;
            deferredGBuffer.LoadContent(manager,ginfo,factory,desc.BackGroundColor);
            deferredLightMap.LoadContent(manager, ginfo, factory, desc.CullPointLight,desc.UseFloatingBufferForLightMap);
            deferredFinalCombination.LoadContent(manager, ginfo, factory, desc.UseFloatingBufferForLightMap, desc.ExtraForwardPass);
            target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            halfWidth = ginfo.Viewport.Width / 2;
            halfHeight = ginfo.Viewport.Height / 2;
            restoreDepth = new RestoreDepth(desc.UseFloatingBufferForLightMap, manager, factory, ginfo);
        }

        /// <summary>
        /// Executes the technic.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="world">The world.</param>
        protected override void ExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world)
        {
            Draw(gameTime, world, render);
        }

        /// <summary>
        /// Adds the post effect.
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        public void AddPostEffect(IPostEffect postEffect)
        {
            PostEffects.Push(postEffect);
        }

        /// <summary>
        /// Removes the post effect.
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        public void RemovePostEffect(IPostEffect postEffect)
        {
            PostEffects.RemoveLocation(postEffect);
        }

        /// <summary>
        /// Determines whether [contains post effect] [the specified post effect].
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        /// <returns>
        ///   <c>true</c> if [contains post effect] [the specified post effect]; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsPostEffect(IPostEffect postEffect)
        {
            for (int i = 0; i < PostEffects.Count; i++)
            {
                if (PostEffects[i] == postEffect)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="world">The world.</param>
        /// <param name="render">The render.</param>
        protected void Draw(GameTime gameTime, IWorld world, RenderHelper render)
        {         
            deferredGBuffer.PreDrawScene(gameTime, world, render);
            world.Culler.StartFrame(world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, world.CameraManager.ActiveCamera.BoundingFrustum);
            deferredGBuffer.SetGBuffer(render);            
            deferredGBuffer.ClearGBuffer(render);                                    
            deferredGBuffer.DrawScene(gameTime, world,render);
            deferredGBuffer.ResolveGBuffer(render);
            
            deferredLightMap.SetLightMap(render);
            deferredLightMap.DrawLights(gameTime, world, deferredGBuffer, render);
            deferredLightMap.ResolveLightMap(render);

            deferredFinalCombination.SetFinalCombination(render);
            deferredFinalCombination.DrawScene(gameTime, world, deferredGBuffer, deferredLightMap,render);

            ///se nao for pra salvar em textura, nao tera debug nem post processing ... 
            if (desc.ExtraForwardPass)
            {
                render[PrincipalConstants.colorRT] = deferredGBuffer[GBufferTypes.COLOR];
                render[PrincipalConstants.normalRt] = deferredGBuffer[GBufferTypes.NORMAL];
                render[PrincipalConstants.lightRt] = deferredLightMap[DeferredLightMapType.LIGHTMAP];
                render[PrincipalConstants.dephRT] = deferredGBuffer[GBufferTypes.DEPH];
                render[PrincipalConstants.extra1RT] = deferredGBuffer[GBufferTypes.Extra1];
                render[PrincipalConstants.CombinedImage] = deferredFinalCombination[GBufferTypes.FINALIMAGE];
                render[PrincipalConstants.CurrentImage] = deferredFinalCombination[GBufferTypes.FINALIMAGE];

                if (desc.RestoreDepthOption == RestoreDepthOption.BEFORE_POSTEFFECT)
                {                    
                    restoreDepth.PerformForwardPass(render[PrincipalConstants.CombinedImage], render[PrincipalConstants.dephRT], render);
                    if (desc.PhysicDebug)
                    {
                        world.PhysicWorld.iDebugDrawn(gameTime, world.CameraManager.ActiveCamera);
                    }
                    if(world.ParticleManager!= null)
                        world.ParticleManager.Draw(render, gameTime, world.CameraManager.ActiveCamera);
                    forwardPass.Draw(gameTime, world, render);
                    render.RenderPosComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);
                    render[PrincipalConstants.CurrentImage] = restoreDepth .EndForwardPass(render);
                    render[PrincipalConstants.CombinedImage] = render[PrincipalConstants.CurrentImage];

                    for (int i = 0; i < PostEffects.Count; i++)
                    {                        
                        render.PushRenderTarget(target);
                        PostEffects[i].Draw(render, gameTime, ginfo, world);
                        Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                        System.Diagnostics.Debug.Assert(tex != null);
                        render[PrincipalConstants.CurrentImage] = tex;
                    }
                                        
                    if (desc.UseFloatingBufferForLightMap)
                    {
                        render.Clear(Color.Transparent);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage],Color.White,ginfo.FullScreenRectangle,Matrix.Identity,null,true,SpriteSortMode.Deferred,SamplerState.PointClamp);                                                
                    }
                    else
                    {
                        render.Clear(Color.Transparent);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true,SpriteSortMode.Immediate,null,BlendState.Opaque);                            
                    }
                }
                else if (desc.RestoreDepthOption == RestoreDepthOption.AFTER_POSTEFFECT)
                {
                    for (int i = 0; i < PostEffects.Count; i++)
                    {
                        render.PushRenderTarget(target);
                        PostEffects[i].Draw(render, gameTime, ginfo, world);
                        Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                        System.Diagnostics.Debug.Assert(tex != null);
                        render[PrincipalConstants.CurrentImage] = tex;
                    }

                    restoreDepth.PerformForwardPass(render[PrincipalConstants.CurrentImage], render[PrincipalConstants.dephRT], render);
                    if (desc.PhysicDebug)
                    {
                        world.PhysicWorld.iDebugDrawn(gameTime, world.CameraManager.ActiveCamera);
                    }
                    if (world.ParticleManager != null)
                        world.ParticleManager.Draw(render, gameTime, world.CameraManager.ActiveCamera);
                    forwardPass.Draw(gameTime, world, render);
                    render[PrincipalConstants.CurrentImage] = restoreDepth.EndForwardPass(render);

                    if (desc.UseFloatingBufferForLightMap)
                    {
                        render.Clear(Color.Transparent);
                        System.Diagnostics.Debug.Assert(render.PeekBlendState() == BlendState.Opaque);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Immediate);
                        System.Diagnostics.Debug.Assert(render.PeekBlendState() == BlendState.Opaque);
                    }
                    else
                    {
                        render.Clear(Color.Transparent);
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true,SpriteSortMode.Immediate);                            
                        
                    }
                
                }
                else if (desc.RestoreDepthOption == RestoreDepthOption.NONE)
                {
                    for (int i = 0; i < PostEffects.Count ; i++)
                    {
                        render.PushRenderTarget(target);
                        PostEffects[i].Draw(render, gameTime, ginfo, world);
                        Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                        System.Diagnostics.Debug.Assert(tex != null);
                        render[PrincipalConstants.CurrentImage] = tex;
                    }
                    if (desc.UseFloatingBufferForLightMap)
                    {
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.PointClamp, BlendState.Opaque);
                    }
                    else
                    {
                        render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.LinearClamp, BlendState.Opaque);
                    }
                }                                

                render.RenderPosComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);
        
                if (desc.DefferedDebug)
                {
                    if (desc.UseFloatingBufferForLightMap)
                        render.RenderBegin(Matrix.Identity,null,SpriteSortMode.Immediate,SamplerState.PointClamp,BlendState.Opaque);
                    else
                        render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Immediate, SamplerState.AnisotropicClamp, BlendState.Opaque);
                    
                    render.RenderTexture(render[desc.RenderTargetsNameToDefferedDebug[0]] ,Color.White, new Rectangle(0, 0, halfWidth, halfHeight));
                    render.RenderTexture(render[desc.RenderTargetsNameToDefferedDebug[1]], Color.White, new Rectangle(0, halfHeight, halfWidth, halfHeight));
                    render.RenderTexture(render[desc.RenderTargetsNameToDefferedDebug[2]], Color.White, new Rectangle(halfWidth, 0, halfWidth, halfHeight));
                    render.RenderTexture(render[desc.RenderTargetsNameToDefferedDebug[3]], Color.White, new Rectangle(halfWidth, halfHeight, halfWidth, halfHeight));
                    render.RenderEnd();
                }                
                
            }
            else
            {
                render.RenderPosComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);
            }


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

    }

    internal class PostEffectComparer : IComparer<IPostEffect>
    {        
    
        #region IComparer<IPostEffect> Members

        public int  Compare(IPostEffect x, IPostEffect y)
        {
 	        if(x.Priority > y.Priority)
                return 1;
            if(x.Priority  == y.Priority )
                return 0;
            return -1;
        }

        #endregion
}

}

