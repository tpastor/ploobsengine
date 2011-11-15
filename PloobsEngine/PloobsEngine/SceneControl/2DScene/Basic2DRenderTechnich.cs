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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Material2D;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;
using PloobsEngine.Physic2D;

namespace PloobsEngine.SceneControl._2DScene
{
    public delegate void RenderBackGround(GraphicInfo ginfo, RenderHelper render);
    public delegate void BeforeDraw(GraphicInfo ginfo, RenderHelper render);
    public delegate void AfterDrawBeforePostEffects(GraphicInfo ginfo, RenderHelper render);     

    public class Basic2DRenderTechnich : RenderTechnich2D
    {
        public event RenderBackGround RenderBackGround = null;
        public event BeforeDraw BeforeDraw = null;
        public event AfterDrawBeforePostEffects AfterDrawBeforePostEffects = null;
        
        /// <summary>
        /// Default false
        /// </summary>
        public bool  UsePreDrawPhase = false;
        /// <summary>
        /// Default false
        /// </summary>
        public bool  UseDrawComponents = false;
        public Dictionary<Type, IMaterialProcessor> MaterialProcessors = new Dictionary<Type, IMaterialProcessor>();

        /// <summary>
        /// Default
        /// Color.FromNonPremultiplied(10, 10, 10, 255)
        /// WHEN NOT USING LIGHTS, this is the background color
        /// </summary>
        public Color AmbientColor = Color.FromNonPremultiplied(10, 10, 10, 255);

        Engine.GraphicInfo ginfo;
#if !WINDOWS_PHONE        
        /// <summary>
        /// Default
        /// Color.Gray
        /// </summary>
        public Color LightMaskAttenuation = Color.Gray;
        /// <summary>
        /// Default true
        /// </summary>
        public bool UseLights = true;
        BlendState blendState;
        RenderTarget2D screenShadows;
        RenderTarget2D renderTarget;
        RenderTarget2D postEffectTarget;
        /// <summary>
        /// Default true
        /// </summary>
        public bool UsePostProcessing = true;        
        PloobsEngine.Light2D.ShadowmapResolver shadowmapResolver;

        public Basic2DRenderTechnich() : base(PostEffectType.Forward2D)
        {
            MaterialProcessors.Add(typeof(Basic2DTextureMaterial), new Basic2DTextureMaterialProcessor());
        }
#else
        public Basic2DRenderTechnich()         
        {
            MaterialProcessors.Add(typeof(Basic2DTextureMaterial), new Basic2DTextureMaterialProcessor());
        }
#endif

        protected override void AfterLoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            this.ginfo = ginfo;
#if !WINDOWS_PHONE
            blendState = new BlendState();
            blendState.ColorSourceBlend = Blend.DestinationColor;
            blendState.ColorDestinationBlend = Blend.SourceColor;            
            shadowmapResolver = new PloobsEngine.Light2D.ShadowmapResolver(factory,new QuadRender(factory.device), PloobsEngine.Light2D.ShadowmapSize.Size512, PloobsEngine.Light2D.ShadowmapSize.Size512);
            screenShadows = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);            
            if (UsePostProcessing)
            {
                renderTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
                postEffectTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
            }
#endif
            base.AfterLoadContent(manager, ginfo, factory);
        }

        protected override void ExecuteTechnic(Microsoft.Xna.Framework.GameTime gameTime, RenderHelper render, I2DWorld world)
        {
            world.Culler.StartFrame(world.Camera2D.SimView, world.Camera2D.SimProjection, world.Camera2D.BoundingFrustrum);
            Dictionary<Type, List<I2DObject>> objs = world.Culler.GetNotCulledObjectsList();

            if (UsePreDrawPhase)
            {
                foreach (var item in objs.Keys)
                {
                    IMaterialProcessor MaterialProcessor = MaterialProcessors[item];

                    if (MaterialProcessor != null)
                    {
                        MaterialProcessor.ProcessPreDraw(gameTime, render, world.Camera2D, world, objs[item]);
                    }
                    else
                    {
                        foreach (var iobj in objs[item])
                        {
                            if (iobj.PhysicObject.Enabled == true)
                            {
                                iobj.Material.Draw(gameTime, iobj, render);
                            }
                        }
                    }
                }
            }


#if !WINDOWS_PHONE
            if (UsePostProcessing)
            {
                render.PushRenderTarget(renderTarget);
            }

            if (UseLights)
            {
                BoundingFrustum bf = world.Camera2D.BoundingFrustrum;
                foreach (var item in world.Lights2D)
                {                    
                    item.BeginDrawingShadowCasters(render);
                    item.UpdateLight(world.Camera2D.View);

                    foreach (var item2 in objs.Keys)
                    {
                        IMaterialProcessor MaterialProcessor = MaterialProcessors[item2];

                        if (MaterialProcessor != null)
                        {
                            MaterialProcessor.ProcessLightDraw(gameTime, render, world.Camera2D, objs[item2], Color.Black, item);
                        }
                        else
                        {
                            foreach (var iobj in objs[item2])
                            {
                                if (iobj.PhysicObject.Enabled == true)
                                {
                                    iobj.Material.LightDraw(gameTime, iobj, render, Color.Black, item);
                                }
                            }
                        }
                    }

                    item.EndDrawingShadowCasters(render);   
                    shadowmapResolver.ResolveShadows(item);

                }

                render.PushRenderTarget(screenShadows);
                render.Clear(AmbientColor);

                if (RenderBackGround != null)
                    RenderBackGround(ginfo, render);

                render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Deferred, SamplerState.LinearClamp, BlendState.Additive);

                foreach (var item in world.Lights2D)
                {
                    render.RenderTexture(item.RenderTarget, item.LightPosition - item.LightAreaSize * 0.5f, item.Color, 0, Vector2.Zero, 1);
                }
                render.RenderEnd();                                                

                render.PopRenderTarget();

                render.Clear(LightMaskAttenuation);

                render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Immediate, SamplerState.LinearClamp, blendState);
                render.RenderTexture(screenShadows, Vector2.Zero, Color.White, 0, Vector2.Zero, 1);
                render.RenderEnd();
            }
            else
            {
                render.Clear(AmbientColor);
                if (RenderBackGround != null)
                    RenderBackGround(ginfo, render);
            }
            
#else
            render.Clear(AmbientColor);
            if(RenderBackGround!=null)
                RenderBackGround(ginfo,render);
#endif
            if (UseDrawComponents)
                render.RenderPreComponents(gameTime, world.Camera2D.View, world.Camera2D.SimProjection);

            if (BeforeDraw != null)
                BeforeDraw(ginfo, render);

            foreach (var item in objs.Keys)
            {
                IMaterialProcessor MaterialProcessor = MaterialProcessors[item];

                if (MaterialProcessor != null)
                {
                    MaterialProcessor.ProcessDraw(gameTime, render, world.Camera2D, objs[item]);
                }
                else
                {
                    foreach (var iobj in objs[item])
                    {
                        if (iobj.PhysicObject.Enabled == true)
                        {
                            iobj.Material.Draw(gameTime, iobj, render);                             
                        }
                    }                    
                }
            }

            if (UseDrawComponents)
                render.RenderPosWithDepthComponents(gameTime, world.Camera2D.View, world.Camera2D.SimProjection);

            if (world.ParticleManager != null)
                world.ParticleManager.iDraw(gameTime, world.Camera2D.View, world.Camera2D.SimProjection, render);

            if (AfterDrawBeforePostEffects != null)
                AfterDrawBeforePostEffects(ginfo, render);

#if !WINDOWS_PHONE
            if (UsePostProcessing)
            {
                render[PrincipalConstants.CurrentImage] = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                render[PrincipalConstants.CombinedImage] = render[PrincipalConstants.CurrentImage];
                for (int i = 0; i < PostEffects.Count; i++)
                {
                    if (PostEffects[i].Enabled)
                    {
                        render.PushRenderTarget(postEffectTarget);
                        PostEffects[i].Draw(render[PrincipalConstants.CurrentImage], render, gameTime, ginfo, null, false);
                        Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                        render[PrincipalConstants.CurrentImage] = tex;
                    }
                }
                render.Clear(Color.Black);
                render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp, BlendState.AlphaBlend);
            }
#endif
            if (UseDrawComponents)
                render.RenderPosComponents(gameTime, world.Camera2D.View, world.Camera2D.SimProjection);            
        }

        public override string TechnicName
        {
            get { return "Basic2DRenderTechnich"; }
        }
    }
}
