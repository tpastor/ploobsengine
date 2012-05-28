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
        bool CastShadowAndReflection;
        public BoltShader(GraphicFactory factory, Color color, bool CastShadowAndReflection = false)
        {
            effect = factory.GetEffect("effects/laser_shader"); // load effect before laserbolt

            effect_color = effect.Parameters["laser_bolt_color"];
            effect_center_to_viewer = effect.Parameters["center_to_viewer"];
            effect_technique = effect.Techniques["laserbolt_technique"];
            world = effect.Parameters["world"];
            wvp = effect.Parameters["wvp"];
            this.color = color;
            this.CastShadowAndReflection = CastShadowAndReflection;
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

        protected override void Draw(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.RenderHelper render, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights)
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
        public override void BasicDraw(GameTime gt, PloobsEngine.SceneControl.IObject obj, ref Matrix view, ref Matrix projection, IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render, Plane? clippingPlane, bool useAlphaBlending = false)
        {
            if(CastShadowAndReflection)
                base.BasicDraw(gt, obj, ref view, ref projection, lights, render, clippingPlane, useAlphaBlending);
        }
        public override void DepthExtractor(GameTime gt, PloobsEngine.SceneControl.IObject obj, ref Matrix View, ref Matrix projection, PloobsEngine.SceneControl.RenderHelper render)
        {
            if(CastShadowAndReflection)
                base.DepthExtractor(gt, obj, ref View, ref projection, render);
        }

        public override MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.FORWARD; }
        }
    }
}
