using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Material2D;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.SceneControl._2DScene
{
    public class Basic2DRenderTechnich : RenderTechnich2D
    {
        public Color BackGroundColor = Color.CornflowerBlue;
        public bool usePreDrawPhase = false;
        public Dictionary<Type, IMaterialProcessor> MaterialProcessors = new Dictionary<Type, IMaterialProcessor>();

#if !WINDOWS_PHONE
        RenderTarget2D renderTarget;
        RenderTarget2D postEffectTarget;
        bool usePostProcessing = true;
        Engine.GraphicInfo ginfo;

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
#if !WINDOWS_PHONE
            this.ginfo = ginfo;
            if (usePostProcessing)
            {
                renderTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
                postEffectTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
            }
#endif
            base.AfterLoadContent(manager, ginfo, factory);
        }

        protected override void ExecuteTechnic(Microsoft.Xna.Framework.GameTime gameTime, RenderHelper render, I2DWorld world)
        {
            if (usePreDrawPhase)
            {
                render.Clear(BackGroundColor);
                foreach (var item in world.MaterialSortedObjects.Keys)
                {
                    IMaterialProcessor MaterialProcessor = MaterialProcessors[item];

                    if (MaterialProcessor != null)
                    {
                        MaterialProcessor.ProcessPreDraw(gameTime, render, world.Camera2D,world, world.MaterialSortedObjects[item]);
                    }
                    else
                    {
                        foreach (var iobj in world.MaterialSortedObjects[item])
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
            if (usePostProcessing)
            {
                render.PushRenderTarget(renderTarget);
            }
#endif

            render.Clear(BackGroundColor);
            render.RenderPreComponents(gameTime, world.Camera2D.View, world.Camera2D.SimProjection);            
            foreach (var item in world.MaterialSortedObjects.Keys)
            {
                IMaterialProcessor MaterialProcessor = MaterialProcessors[item];

                if (MaterialProcessor != null)
                {
                    MaterialProcessor.ProcessDraw(gameTime, render, world.Camera2D, world.MaterialSortedObjects[item]);
                }
                else
                {
                    foreach (var iobj in world.MaterialSortedObjects[item])
                    {
                        if (iobj.PhysicObject.Enabled == true)
                        {
                            iobj.Material.Draw(gameTime, iobj, render);                             
                        }
                    }                    
                }
            }
            render.RenderPosWithDepthComponents(gameTime, world.Camera2D.View, world.Camera2D.SimProjection);            

#if !WINDOWS_PHONE
            if (usePostProcessing)
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

            render.RenderPosComponents(gameTime, world.Camera2D.View, world.Camera2D.SimProjection);            
        }

        public override string TechnicName
        {
            get { return "Basic2DRenderTechnich"; }
        }
    }
}
