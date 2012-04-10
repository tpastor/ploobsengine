using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    public class LightBloomPostEffect : IPostEffect
    {
        // Adjust these values to change visual appearance.
        public  float BloomThreshold = 0.25f;
        public  float BloomIntensity = 1.5f;
        public  int BlurPasses = 4; 


        // result = source - destination
        static BlendState extractBrightColors = new BlendState
        {
            ColorSourceBlend = Blend.One,
            AlphaSourceBlend = Blend.One,

            ColorDestinationBlend = Blend.One,
            AlphaDestinationBlend = Blend.One,

            ColorBlendFunction = BlendFunction.Subtract,
            AlphaBlendFunction = BlendFunction.Subtract,
        };


        // result = source + destination
        static BlendState additiveBlur = new BlendState
        {
            ColorSourceBlend = Blend.One,
            AlphaSourceBlend = Blend.One,

            ColorDestinationBlend = Blend.One,
            AlphaDestinationBlend = Blend.One,
        };


        // result = source + (destination * (1 - source))
        static BlendState combineFinalResult = new BlendState
        {
            ColorSourceBlend = Blend.One,
            AlphaSourceBlend = Blend.One,

            ColorDestinationBlend = Blend.InverseSourceColor,
            AlphaDestinationBlend = Blend.InverseSourceColor,
        };


        SpriteBatch spriteBatch;

        RenderTarget2D scene;
        RenderTarget2D halfSize;
        RenderTarget2D quarterSize;
        RenderTarget2D quarterSize2;


        public LightBloomPostEffect()
            : base(PostEffectType.All)
        { }

        public override void  Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)       
        {
            spriteBatch = factory.GetSpriteBatch();

            GraphicsDevice GraphicsDevice = factory.device;
            PresentationParameters pp = GraphicsDevice.PresentationParameters;

            int w = pp.BackBufferWidth;
            int h = pp.BackBufferHeight;

            scene = new RenderTarget2D(GraphicsDevice, w, h, false, pp.BackBufferFormat, pp.DepthStencilFormat, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);
            halfSize = new RenderTarget2D(GraphicsDevice, w / 2, h / 2, false, pp.BackBufferFormat, DepthFormat.None);
            quarterSize = new RenderTarget2D(GraphicsDevice, w / 4, h / 4, false, pp.BackBufferFormat, DepthFormat.None);
            quarterSize2 = new RenderTarget2D(GraphicsDevice, w / 4, h / 4, false, pp.BackBufferFormat, DepthFormat.None);
        }


        public override void  Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            // Shrink to half size.
            rHelper.PushRenderTarget(halfSize);
            DrawSprite(scene, BlendState.Opaque, GraphicInfo);

            // Shrink again to quarter size, at the same time applying the threshold subtraction.
            rHelper.PopRenderTarget();
            rHelper.PushRenderTarget(quarterSize);
            rHelper.Clear(new Color(BloomThreshold, BloomThreshold, BloomThreshold));
            DrawSprite(halfSize, extractBrightColors, GraphicInfo);

            // Kawase blur filter (see http://developer.amd.com/media/gpu_assets/Oat-ScenePostprocessing.pdf)
            for (int i = 0; i < BlurPasses; i++)
            {
                rHelper.PushRenderTarget(quarterSize2);
                rHelper.Clear(Color.Black);

                int w = quarterSize.Width;
                int h = quarterSize.Height;

                float brightness = 0.25f;

                // On the first pass, scale brightness to restore full range after the threshold subtraction.
                if (i == 0)
                    brightness /= (1 - BloomThreshold);

                // On the final pass, apply tweakable intensity adjustment.
                if (i == BlurPasses - 1)
                    brightness *= BloomIntensity;

                Color tint = new Color(brightness, brightness, brightness);

                spriteBatch.Begin(0, additiveBlur);

                spriteBatch.Draw(quarterSize, new Vector2(0.5f, 0.5f), new Rectangle(i + 1, i + 1, w, h), tint);
                spriteBatch.Draw(quarterSize, new Vector2(0.5f, 0.5f), new Rectangle(-i, i + 1, w, h), tint);
                spriteBatch.Draw(quarterSize, new Vector2(0.5f, 0.5f), new Rectangle(i + 1, -i, w, h), tint);
                spriteBatch.Draw(quarterSize, new Vector2(0.5f, 0.5f), new Rectangle(-i, -i, w, h), tint);

                spriteBatch.End();

                rHelper.PopRenderTarget();
                Swap(ref quarterSize, ref quarterSize2);
            }

            rHelper.Clear(Color.Black);
            DrawSprite(scene, BlendState.Opaque,GraphicInfo);
            DrawSprite(quarterSize, combineFinalResult, GraphicInfo);
            rHelper.ResyncStates();
        }


        void DrawSprite(Texture2D source, BlendState blendState, GraphicInfo ginfo)
        {
            spriteBatch.Begin(0, blendState);
            spriteBatch.Draw(source, ginfo.FullScreenRectangle, Color.White);
            spriteBatch.End();
        }


        static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}