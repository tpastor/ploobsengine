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
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{    

    public struct ForwardRenderTecnichDescription
    {
#if !WINDOWS_PHONE && !REACH
        public static ForwardRenderTecnichDescription Default()
        {
            return new ForwardRenderTecnichDescription(Color.Black, true);
        }
#else
        public static ForwardRenderTecnichDescription Default()
        {
            return new ForwardRenderTecnichDescription(Color.Black, false);
        }
        #endif

        internal ForwardRenderTecnichDescription(Color BackGroundColor,bool usePostEffect = true)
        {
            this.BackGroundColor = BackGroundColor;
            this.UsePostEffect = usePostEffect;
            UsePreDrawPhase = false;
            UsePostDrawPhase = false;
            OrderAllObjectsBeforeDraw = null;            
        }
        public bool UsePostEffect;
        public Color BackGroundColor;
        public bool UsePreDrawPhase ;
        public bool UsePostDrawPhase;
        /// <summary>
        /// Function called all frames to order all objects that are not culled
        /// Use this to sort objects by material and minimize gpu state changes
        /// </summary>
        public Func<List<IObject>, List<IObject>> OrderAllObjectsBeforeDraw ;
    }

    public class ForwardRenderTecnich : IRenderTechnic
    {
        public ForwardRenderTecnich(ForwardRenderTecnichDescription desc)
#if !WINDOWS_PHONE && !REACH
            : base(PostEffectType.Forward3D)
#else
         : base(PostEffectType.WindowsPhoneAndReach)
#endif

        {
            this.desc = desc;
        }

        ForwardRenderTecnichDescription desc;

        RenderTarget2D renderTarget;
        RenderTarget2D postEffectTarget;
        Engine.GraphicFactory factory;

        Engine.GraphicInfo ginfo;
        #region IRenderTechnic Members

        protected override void AfterLoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            this.ginfo = ginfo;
            this.factory = factory;
            this.ginfo.OnGraphicInfoChange+=new EventHandler(ginfo_OnGraphicInfoChange); ;

            if (desc.UsePostEffect)
            {
                renderTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth,ginfo.BackBufferHeight,SurfaceFormat.Color,ginfo.UseMipMap,DepthFormat.Depth24Stencil8,ginfo.MultiSample,RenderTargetUsage.DiscardContents);
                postEffectTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
            }            
            base.AfterLoadContent(manager, ginfo, factory);
        }

        void ginfo_OnGraphicInfoChange(object sender, EventArgs e)
        {
            GraphicInfo newGraphicInfo = (GraphicInfo)sender;
            if (renderTarget != null && postEffectTarget != null)
            {
                renderTarget.Dispose();
                postEffectTarget.Dispose();
                renderTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
                postEffectTarget = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
            }
        }
        
        protected override void ExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world)
        {
            world.Culler.StartFrame(world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, world.CameraManager.ActiveCamera.BoundingFrustum);
            List<IObject> objList = world.Culler.GetNotCulledObjectsList(Material.MaterialType.FORWARD);
            if (desc.OrderAllObjectsBeforeDraw != null)
                objList = desc.OrderAllObjectsBeforeDraw(objList);

            if (desc.UsePreDrawPhase)
            {
                foreach (var item in objList)
                {
                    item.Material.PreDrawnPhase(gameTime, world, item, world.CameraManager.ActiveCamera, world.Lights, render);
                }
            }


            if (desc.UsePostEffect)
            {
                render.PushRenderTarget(renderTarget);                
            }

            render.Clear(desc.BackGroundColor);
            render.RenderPreComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);                        
            foreach (var item in objList)
            {
                //critical code, no log
                System.Diagnostics.Debug.Assert(item.Material.MaterialType == Material.MaterialType.FORWARD, "This Technich is just for forward materials and shaders");
                if(item.Material.IsVisible)
                    item.Material.Drawn(gameTime,item, world.CameraManager.ActiveCamera, world.Lights, render);                
            }

            if (desc.UsePostDrawPhase)
            {
                foreach (var item in objList)
                {
                    item.Material.PosDrawnPhase(gameTime, item, world.CameraManager.ActiveCamera, world.Lights, render);
                }
            }

            if (world.ParticleManager != null)
                world.ParticleManager.iDraw(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, render);

            render.RenderPosWithDepthComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);


            if (desc.UsePostEffect)
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
                render.RenderTextureComplete(render[PrincipalConstants.CurrentImage], Color.White, ginfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, ginfo.SamplerState);                                             
            }           

            render.RenderPosComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);

        }

        
        public override string TechnicName
        {
            get { return "ForwardTechnich"; }
        }

        #endregion

        public override void CleanUp()
        {
            this.ginfo.OnGraphicInfoChange -= ginfo_OnGraphicInfoChange;
            if (renderTarget != null && postEffectTarget != null)
            {
                renderTarget.Dispose();
                postEffectTarget.Dispose();
            }

            for (int i = 0; i < PostEffects.Count; i++)
            {
                PostEffects[i].CleanUp();
            }
        }
    }
}

