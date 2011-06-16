using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace EngineTestes.Bolt
{
    public class BoltShader :IShader
    {
        public BoltShader(GraphicFactory factory)
        {
            effect = factory.GetEffect("effects/laser_shader"); // load effect before laserbolt

            effect_color = effect.Parameters["laser_bolt_color"];
            effect_center_to_viewer = effect.Parameters["center_to_viewer"];
            effect_technique = effect.Techniques["laserbolt_technique"];
            world = effect.Parameters["world"];
            wvp = effect.Parameters["wvp"];
            color = Color.Red;
        }

        Color color;
        Effect effect;
        EffectParameter effect_center_to_viewer;
        EffectParameter effect_color;
        EffectParameter world;
        EffectParameter wvp;


        EffectTechnique effect_technique;

        Matrix[] shader_matrices_combined = new Matrix[2];

        public BlendState laser_blends = BlendState.NonPremultiplied;
        public DepthStencilState laser_depth = DepthStencilState.None;

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.RenderHelper render, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights)
        {
               render.PushBlendState(laser_blends);
               effect.CurrentTechnique = effect_technique;                            
               world.SetValue(obj.WorldMatrix);
               wvp.SetValue(shader_matrices_combined[1] = obj.WorldMatrix * cam.View * cam.Projection);
               effect_color.SetValue(color.ToVector4());
               effect_center_to_viewer.SetValue(cam.Position);
               render.RenderBatch(obj.Modelo.GetBatchInformation(0)[0], effect);
               render.PopBlendState();
        }

        public override MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.FORWARD; }
        }
    }
}
