using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine;
using PloobsEngine.Engine.Logger;

namespace EngineTestes.Post
{
    public enum BlurRadiusSize : int
    {
        Fifteen =15,
        Seven = 7,
        Three = 3,
    }    

    public class SBlurPost : IPostEffect
    {
        private int BLUR_RADIUS = 15;
        BlurRadiusSize BlurRadiusSize;
        public SBlurPost(float BLUR_AMOUNT = 1, BlurRadiusSize BlurRadiusSize = BlurRadiusSize.Fifteen, SurfaceFormat SurfaceFormat = Microsoft.Xna.Framework.Graphics.SurfaceFormat.Color)
            : base(PostEffectType.Deferred)
        {
            this.BLUR_AMOUNT = BLUR_AMOUNT;
            this.SurfaceFormat = SurfaceFormat;
            ImageSamplerState = SamplerState.LinearClamp;
            this.BlurRadiusSize = BlurRadiusSize;
            BLUR_RADIUS = (int)BlurRadiusSize;
        }

        public SBlurPost(Vector2 OriginSize, Vector2 destinySize, SurfaceFormat SurfaceFormat,BlurRadiusSize BlurRadiusSize = BlurRadiusSize.Fifteen,float BLUR_AMOUNT = 1)
            : base(PostEffectType.Deferred)
        {
            this.BLUR_AMOUNT = BLUR_AMOUNT;
            this.OriginSize = OriginSize;
            this.destinySize = destinySize;
            this.SurfaceFormat = SurfaceFormat;
            ImageSamplerState = SamplerState.LinearClamp;
            this.BlurRadiusSize = BlurRadiusSize;
            BLUR_RADIUS = (int)BlurRadiusSize;
        }
                
        float BLUR_AMOUNT = 2f;
        Vector2? OriginSize;
        SurfaceFormat? SurfaceFormat;
        Vector2? destinySize;
        Effect effect;
        RenderTarget2D RenderTarget2D;
        float blurDepthFalloff = 1;

        public float BlurDepthFalloff
        {
            get { return blurDepthFalloff; }
            set { blurDepthFalloff = value; }
        }

        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            switch (BlurRadiusSize)
            {
                case BlurRadiusSize.Fifteen:
                    effect = factory.GetEffect("SBlurPost\\sblur", true); 
                    break;
                case BlurRadiusSize.Seven:
                    effect = factory.GetEffect("SBlurPost\\sblur2", true); 
                    break;
                case BlurRadiusSize.Three:
                    effect = factory.GetEffect("SBlurPost\\sblur3", true); 
                    break;
                default:
                    ActiveLogger.LogMessage("Wrong Blur Radius Size Specified", LogLevel.RecoverableError);
                    effect = factory.GetEffect("SBlurPost\\sblur", true); 
                    break;
            }

            if (!destinySize.HasValue)
            {
                destinySize = new Vector2(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            }

            if (SurfaceFormat.HasValue)
            {
                if (SurfaceFormat.Value == Microsoft.Xna.Framework.Graphics.SurfaceFormat.Single ||
                    SurfaceFormat.Value == Microsoft.Xna.Framework.Graphics.SurfaceFormat.HalfSingle)
                {
                    effect.CurrentTechnique = effect.Techniques["GAUSSSingle"];
                }
                else
                {
                    effect.CurrentTechnique = effect.Techniques["GAUSSTriple"];
                }
                RenderTarget2D = factory.CreateRenderTarget((int)destinySize.Value.X, (int)destinySize.Value.Y, SurfaceFormat.Value);
            }
            else
            {
                RenderTarget2D = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);                
            }

            ComputeKernel(BLUR_RADIUS, BLUR_AMOUNT);
            if (OriginSize.HasValue)
            {
                ComputeOffsets(OriginSize.Value.X, OriginSize.Value.Y);
            }
            else
            {
                ComputeOffsets(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            }
        }

        public SamplerState ImageSamplerState
        {
            get;
            set;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            rHelper.PushRenderTarget(RenderTarget2D);
            effect.Parameters["blurDepthFalloff"].SetValue(blurDepthFalloff);
            effect.Parameters["weights"].SetValue(kernel);
            effect.Parameters["offsets"].SetValue(offsetsHoriz);
            effect.Parameters["GBufferPixelSize"].SetValue(new Vector2(1f / ImageToProcess.Width, 1f / ImageToProcess.Height));
            effect.Parameters["TempBufferRes"].SetValue(destinySize.Value);
            rHelper.Textures[0] = rHelper[PrincipalConstants.DephRT];
            rHelper.Textures[1] = ImageToProcess;
            SamplerState s0 = rHelper.SetSamplerState(SamplerState.PointClamp, 0);
            SamplerState s1 = rHelper.SetSamplerState(ImageSamplerState, 1);
            rHelper.RenderFullScreenQuadVertexPixel(effect);

            rHelper.PopRenderTarget();
            
            effect.Parameters["offsets"].SetValue(offsetsVert);
            rHelper.Textures[1] = RenderTarget2D;
            rHelper.RenderFullScreenQuadVertexPixel(effect);

            rHelper.SetSamplerState(s0, 0);
            rHelper.SetSamplerState(s1, 1);

        }
        
        /////////////////////////////////////////////
        
        private float sigma;
        private float[] kernel;
        private Vector2[] offsetsHoriz;
        private Vector2[] offsetsVert;
        private int radius;
        private float amount;
        void ComputeKernel(int blurRadius, float blurAmount)
        {
            radius = blurRadius;
            amount = blurAmount;

            kernel = null;
            kernel = new float[radius * 2 + 1];
            sigma = radius / amount;

            float twoSigmaSquare = 2.0f * sigma * sigma;
            float sigmaRoot = (float)Math.Sqrt(twoSigmaSquare * Math.PI);
            float total = 0.0f;
            float distance = 0.0f;
            int index = 0;

            for (int i = -radius; i <= radius; ++i)
            {
                distance = i * i;
                index = i + radius;
                kernel[index] = (float)Math.Exp(-distance / twoSigmaSquare) / sigmaRoot;
                total += kernel[index];
            }

            for (int i = 0; i < kernel.Length; ++i)
                kernel[i] /= total;
        }
        void ComputeOffsets(float textureWidth, float textureHeight)
        {
            offsetsHoriz = null;
            offsetsHoriz = new Vector2[radius * 2 + 1];

            offsetsVert = null;
            offsetsVert = new Vector2[radius * 2 + 1];

            int index = 0;
            float xOffset = 1.0f / textureWidth;
            float yOffset = 1.0f / textureHeight;

            for (int i = -radius; i <= radius; ++i)
            {
                index = i + radius;
                offsetsHoriz[index] = new Vector2(i * xOffset, 0.0f);
                offsetsVert[index] = new Vector2(0.0f, i * yOffset);
            }
        }       

    }
}
