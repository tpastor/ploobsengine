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
        public SSAOPost(float blurAmount = 1, BlurRadiusSize BlurRadiusSize= BlurRadiusSize.Fifteen)
            : base(PostEffectType.Deferred)
        {
            this.blurAmount = blurAmount;
            this.BlurRadiusSize = BlurRadiusSize;
        }
        BlurRadiusSize BlurRadiusSize;
        float blurAmount;
        SBlurPost sBlurPost;
        public SBlurPost SBlurPost
        {
            get { return sBlurPost; }
        }
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            Effect = factory.GetEffect("SSAO\\ssao");
            tex = factory.GetTexture2D("SSAO\\noise");
            sBlurPost = new SBlurPost(blurAmount, BlurRadiusSize);
            sBlurPost.Init(ginfo, factory);
            RenderTarget2D = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            RenderTarget2D2 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
        }

        Effect Effect;
        Texture2D tex;
        RenderTarget2D RenderTarget2D;
        RenderTarget2D RenderTarget2D2;
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {          
            ///ssao
            rHelper.PushRenderTarget(RenderTarget2D);
            rHelper.Textures[0] = rHelper[PrincipalConstants.normalRt];
            SamplerState s0 = rHelper.SetSamplerState(SamplerState.LinearClamp, 0);

            rHelper.Textures[1] = rHelper[PrincipalConstants.DephRT];
            SamplerState s1 = rHelper.SetSamplerState(SamplerState.PointClamp, 1);

            rHelper.Textures[2] = tex;
            SamplerState s2 = rHelper.SetSamplerState(SamplerState.LinearWrap, 2);            

            Effect.CurrentTechnique = Effect.Techniques[0];
            Effect.Parameters["random_size"].SetValue(64);
            Effect.Parameters["g_screen_size"].SetValue(new Vector2(GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight));    
            Effect.Parameters["invertViewProj"].SetValue(Matrix.Invert(world.CameraManager.ActiveCamera.Projection));
            Effect.Parameters["View"].SetValue(world.CameraManager.ActiveCamera.View);
            Effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);

            ///artist parameters
            Effect.Parameters["g_sample_rad"].SetValue(radius);
            Effect.Parameters["g_intensity"].SetValue(intensity);
            Effect.Parameters["g_scale"].SetValue(scale);
            Effect.Parameters["g_bias"].SetValue(bias);
            
            rHelper.RenderFullScreenQuadVertexPixel(Effect);         

            rHelper.PopRenderTarget();
            rHelper.SetSamplerState(s0, 0);
            rHelper.SetSamplerState(s1, 1);
            rHelper.SetSamplerState(s2, 2);

            ///blur
            rHelper.PushRenderTarget(RenderTarget2D2);
            sBlurPost.Draw(RenderTarget2D, rHelper, gt, GraphicInfo, world, useFloatBuffer);
            rHelper.PopRenderTarget();            
            
            rHelper[PrincipalConstants.SSAORT] = RenderTarget2D2;

            ///Mix
            Effect.CurrentTechnique = Effect.Techniques[1];
            Effect.Parameters["SSAOCombineIntensityity"].SetValue(SSAOCombineIntensityity);
            rHelper.Textures[0] = RenderTarget2D2;
            SamplerState s = useFloatBuffer == true ? SamplerState.PointClamp : SamplerState.LinearClamp;
            s1 = rHelper.SetSamplerState(s, 1);
            rHelper.Textures[1] = ImageToProcess;                        
            rHelper.RenderFullScreenQuadVertexPixel(Effect);
            rHelper.SetSamplerState(s1, 1);           

        }

        float radius = 15;

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }
        float intensity = 3;

        public float Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }

        float sSAOCombineIntensityity = 1;

        public float SSAOCombineIntensityity
        {
            get { return sSAOCombineIntensityity; }
            set { sSAOCombineIntensityity = value; }
        }


        float scale = 1;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        float bias = 0.005f;

        public float Bias
        {
            get { return bias; }
            set { bias = value; }
        }
    }
}
