using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.SceneControl
{    

    public struct ForwardRenderTecnichDescription
    {
        public static ForwardRenderTecnichDescription Default()
        {
            return new ForwardRenderTecnichDescription(Color.Black, true);
        }
        public ForwardRenderTecnichDescription(Color BackGroundColor,bool usePostEffect = true)
        {
            this.BackGroundColor = BackGroundColor;
            this.usePostEffect = usePostEffect;
            
        }
        public bool usePostEffect;
        public Color BackGroundColor;
    }

    public class ForwardRenderTecnich : IRenderTechnic
    {
        public ForwardRenderTecnich(ForwardRenderTecnichDescription desc) : base(PostEffectType.Forward)
        {
            this.desc = desc;
        }

        ForwardRenderTecnichDescription desc;
        RenderTarget2D renderTarget;
        RenderTarget2D postEffectTarget;
        Engine.GraphicInfo ginfo;
        #region IRenderTechnic Members

        protected override void AfterLoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            this.ginfo = ginfo;
            if (desc.usePostEffect)
            {
                renderTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth,ginfo.BackBufferHeight,SurfaceFormat.Color,true,DepthFormat.Depth24Stencil8,ginfo.MultiSample,RenderTargetUsage.DiscardContents);
                postEffectTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, true, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
            }            
            base.AfterLoadContent(manager, ginfo, factory);
        }

        protected override void ExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world)
        {   

            foreach (var item in world.Objects)
            {
                item.Material.PreDrawnPhase(gameTime, world, item, world.CameraManager.ActiveCamera, world.Lights, render);
            }

            if (desc.usePostEffect)
            {
                render.PushRenderTarget(renderTarget);                
            }

            render.Clear(desc.BackGroundColor);
            render.RenderPreComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);            
            world.Culler.StartFrame(world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, world.CameraManager.ActiveCamera.BoundingFrustum);
            IEnumerable<IObject> objList = world.Culler.GetNotCulledObjectsList(Material.MaterialType.FORWARD);
            foreach (var item in objList)
            {
                ///critical code, no log
                System.Diagnostics.Debug.Assert(item.Material.MaterialType == Material.MaterialType.FORWARD, "This Technich is just for forward materials and shaders");
                item.Material.Drawn(gameTime,item, world.CameraManager.ActiveCamera, world.Lights, render);                
            }

            foreach (var item in objList)
            {             
                item.Material.PosDrawnPhase(gameTime,item, world.CameraManager.ActiveCamera, world.Lights, render);                
            }

            if (world.ParticleManager != null)
                world.ParticleManager.iDraw(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, render);

            if (desc.usePostEffect)
            {
                render[PrincipalConstants.CurrentImage] = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                render[PrincipalConstants.CombinedImage] = render[PrincipalConstants.CurrentImage];
                for (int i = 0; i < PostEffects.Count; i++)
                {                    
                    if (PostEffects[i].Enabled)
                    {
                        render.PushRenderTarget(postEffectTarget);
                        PostEffects[i].Draw(render[PrincipalConstants.CurrentImage], render, gameTime, ginfo, world, false);
                        Texture2D tex = render.PopRenderTarget()[0].RenderTarget as Texture2D;
                        render[PrincipalConstants.CurrentImage] = tex;
                    }
                }
                render.Clear(Color.Black);
                render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp, BlendState.AlphaBlend);                                             
            }

            render.RenderPosComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);

        }

        
        public override string TechnicName
        {
            get { return "ForwardTechnich"; }
        }

        #endregion
    }
}
