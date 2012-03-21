using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using Microsoft.Xna.Framework;

namespace EngineTestes.TransparencyOnBorders
{
    class ForwardGradativeAlphaShader : IShader
    {
        Effect alphagradative;

        public override void Initialize(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {
            alphagradative = factory.GetEffect("effects/alphagradative");
            base.Initialize(ginfo, factory, obj);
        }

        public override MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.FORWARD; }
        }

        RasterizerState cullMode = RasterizerState.CullCounterClockwise;

        public override void PosDrawPhase(GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.RenderHelper render, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights)
        {
            render.PushRasterizerState(cullMode);
            render.PushBlendState(BlendState.NonPremultiplied);
            alphagradative.Parameters["camCenter"].SetValue(cam.Position);

            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);

                for (int j = 0; j < bi.Count(); j++)
                {
                    alphagradative.Parameters["colorMap"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE, i, j));
                    Matrix w1 = Matrix.Multiply(obj.WorldMatrix, bi[j].ModelLocalTransformation);
                    alphagradative.Parameters["wvp"].SetValue(w1 * cam.ViewProjection);
                    alphagradative.Parameters["w"].SetValue(w1);

                    render.RenderBatch(bi[j], alphagradative);
                }
            }
            render.PopBlendState();
            render.PopRasterizerState();         
        }
        
    }
}
