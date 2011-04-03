﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
        public ForwardRenderTecnich(ref ForwardRenderTecnichDescription desc)
        {
            this.desc = desc;
        }

        ForwardRenderTecnichDescription desc;
        #region IRenderTechnic Members


        protected override void ExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world)
        {
            render.Clear(desc.BackGroundColor);

            world.Culler.StartFrame(world.CameraManager.ActiveCamera);
            foreach (var item in world.Culler.GetNotCulledObjectsList(world.CameraManager.ActiveCamera, Material.MaterialType.FORWARD))
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
