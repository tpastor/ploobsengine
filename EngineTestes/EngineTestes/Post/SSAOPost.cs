using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using Microsoft.Xna.Framework;

namespace EngineTestes.Post
{
    public class SSAOPost : IPostEffect
    {
        public SSAOPost()
            : base(PostEffectType.Deferred)
        {
        }

        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            Effect = factory.GetEffect("SSAO\\ssao");
            tex = factory.GetTexture2D("SSAO\\noise");
        }
        Effect Effect;
        Texture2D tex;
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {

            Effect.Parameters["g_buffer_normtex"].SetValue(rHelper[PrincipalConstants.normalRt]); ;
            Effect.Parameters["depthSamplertex"].SetValue(rHelper[PrincipalConstants.DephRT]);
            Effect.Parameters["g_randomtex"].SetValue(tex);
            Effect.Parameters["random_size"].SetValue(64);
            Effect.Parameters["g_screen_size"].SetValue(new Vector2(GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight));
            Effect.Parameters["invertViewProj"].SetValue(Matrix.Invert(world.CameraManager.ActiveCamera.Projection));
            Effect.Parameters["View"].SetValue(world.CameraManager.ActiveCamera.View);
            Effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);

            ///artist parameters
            Effect.Parameters["g_sample_rad"].SetValue(15f);
            Effect.Parameters["g_intensity"].SetValue(3);
            Effect.Parameters["g_scale"].SetValue(1);
            Effect.Parameters["g_bias"].SetValue(0.005f);

            rHelper.SetSamplerStates(SamplerState.PointWrap);
            rHelper.DettachBindedTextures(16);
            rHelper.RenderFullScreenQuadVertexPixel(Effect);
         
        }
    }
}
