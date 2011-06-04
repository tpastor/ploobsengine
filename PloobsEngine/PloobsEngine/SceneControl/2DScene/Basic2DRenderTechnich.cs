using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl._2DScene
{
    public class Basic2DRenderTechnich : RenderTechnich2D
    {
        public Color BackGroundColor = Color.CornflowerBlue;


        public Basic2DRenderTechnich() : base(PostEffectType.Forward)
        { }

        protected override void ExecuteTechnic(Microsoft.Xna.Framework.GameTime gameTime, RenderHelper render, I2DWorld world)
        {
            render.Clear(BackGroundColor);
            foreach (var item in world.Objects)
            {
                if (item.PhysicObject.Enabled == true)
                    item.Material.PreDrawnPhase(gameTime, world, item, render);
            }

            render.Clear(BackGroundColor);
            render.RenderBegin(world.Camera2D.View, null);
            foreach (var item in world.Objects)
            {
                if(item.PhysicObject.Enabled == true)
                    item.Material.Draw(gameTime, item, render);
            }
            render.RenderEnd();
        }

        public override string TechnicName
        {
            get { return "Basic2DRenderTechnich"; }
        }
    }
}
