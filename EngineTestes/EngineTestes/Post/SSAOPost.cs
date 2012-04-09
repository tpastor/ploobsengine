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
        public SSAOPost(SurfaceFormat SurfaceFormat = SurfaceFormat.Color)
            : base(PostEffectType.Deferred)
        {
            this.SurfaceFormat = SurfaceFormat;
        }

        SurfaceFormat SurfaceFormat;
        SBlurPost sBlurPost;

        public SBlurPost SBlurPost
        {
            get { return sBlurPost; }
        }
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            Effect = factory.GetEffect("SSAO\\ssao");
            tex = factory.GetTexture2D("SSAO\\noise");
            sBlurPost = new SBlurPost(1, SurfaceFormat);
            sBlurPost.Init(ginfo, factory);
            RenderTarget2D = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat);
        }
        Effect Effect;
        Texture2D tex;
        RenderTarget2D RenderTarget2D;
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            //rHelper.SetSamplerStates(SamplerState.PointWrap, 6);
            //rHelper.DettachBindedTextures(6);


            rHelper.PushRenderTarget(RenderTarget2D);
            rHelper.Textures[0] = rHelper[PrincipalConstants.normalRt];
            SamplerState s0 = rHelper.SetSamplerState(SamplerState.LinearClamp, 0);

            rHelper.Textures[1] = rHelper[PrincipalConstants.DephRT];
            SamplerState s1 = rHelper.SetSamplerState(SamplerState.PointClamp, 1);

            rHelper.Textures[2] = tex;
            SamplerState s2 = rHelper.SetSamplerState(SamplerState.LinearWrap, 2);
            

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

            
            rHelper.RenderFullScreenQuadVertexPixel(Effect);         

            rHelper.PopRenderTarget();

            sBlurPost.Draw(RenderTarget2D, rHelper, gt, GraphicInfo, world, useFloatBuffer);

            rHelper.SetSamplerState(s0, 0);
            rHelper.SetSamplerState(s1, 1);
            rHelper.SetSamplerState(s2, 2);
        }
    }
}
