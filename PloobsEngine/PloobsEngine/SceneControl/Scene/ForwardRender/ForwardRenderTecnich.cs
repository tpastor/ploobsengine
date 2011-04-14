using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.SceneControl
{    

    public struct ForwardRenderTecnichDescription
    {
        public ForwardRenderTecnichDescription(Color BackGroundColor)
        {
            this.BackGroundColor = BackGroundColor;
        }
        public Color BackGroundColor;
    }

    public class ForwardRenderTecnich : IRenderTechnic
    {
        public ForwardRenderTecnich(ForwardRenderTecnichDescription desc) : base(PostEffectType.Forward)
        {
            this.desc = desc;
        }

        ForwardRenderTecnichDescription desc;
        #region IRenderTechnic Members

        public override void AddPostEffect(IPostEffect postEffect)
        {
            ActiveLogger.LogMessage("PostEffects for Forward Render not supported yet, operation ignored", LogLevel.RecoverableError);
            //base.AddPostEffect(postEffect);
        }

        public override void RemovePostEffect(IPostEffect postEffect)
        {
            ActiveLogger.LogMessage("PostEffects for Forward Render not supported yet, operation ignored", LogLevel.RecoverableError);
            //base.RemovePostEffect(postEffect);
        }


        protected override void ExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world)
        {
            render.Clear(desc.BackGroundColor);

            world.Culler.StartFrame(world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, world.CameraManager.ActiveCamera.BoundingFrustum);
            foreach (var item in world.Culler.GetNotCulledObjectsList(Material.MaterialType.FORWARD))
            {
                ///critical code, no log
                System.Diagnostics.Debug.Assert(item.Material.MaterialType == Material.MaterialType.FORWARD, "This Technich is just for forward materials and shaders");
                item.Material.Drawn(gameTime,item, world.CameraManager.ActiveCamera, world.Lights, render);                
            }
        }

        
        public override string TechnicName
        {
            get { return "ForwardTechnich"; }
        }

        #endregion
    }
}
