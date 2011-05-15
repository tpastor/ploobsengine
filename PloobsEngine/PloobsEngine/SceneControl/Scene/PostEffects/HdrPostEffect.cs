using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{    

    public class HdrPostEffect : IPostEffect
    {
        public HdrPostEffect() : base(PostEffectType.All) { }

        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        long lastTime = 0;
        float dt = 0;                
        Effect effect ;
        Effect tone;
        Effect toScreen;
        Effect fULLGPUBlur;
        Effect luminance;
        Effect threshold;        
        RenderTarget2D[] luminanceChain;
        protected RenderTarget2D currentFrameLuminance;
        protected RenderTarget2D currentFrameAdaptedLuminance;
        protected RenderTarget2D lastFrameAdaptedLuminance;
        public float toneMapKey = 0.8f;
        public float maxLuminance = 16.0f;
        public float bloomThreshold = 0.05f;
        public float bloomMultiplier = 1.0f;
        public float blurSigma = 2.5f;
        public float fTau = 0.5f;
        GraphicFactory factory;
        bool firstTime = true;

        private Texture2D DownScale(RenderHelper rHelper, Texture2D input, RenderTarget2D downscaleTarget, String technichName, bool useFloatingBuffer)
        {            
            effect.CurrentTechnique = effect.Techniques[technichName];
            effect.Parameters["SourceTexture0"].SetValue(input);
            effect.Parameters["g_vSourceDimensions"].SetValue(new Vector2(input.Width, input.Height));
            effect.Parameters["g_vDestinationDimensions"].SetValue(new Vector2(downscaleTarget.Width, downscaleTarget.Height));
            rHelper.PushRenderTarget(downscaleTarget);            
            if (useFloatingBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.LinearClamp);
            return rHelper.PopRenderTargetAsSingleRenderTarget2D();            
        }

        private Texture2D DownScale(RenderHelper rHelper, Texture2D input, int factor, SurfaceFormat format, String technicName, bool useFloatingBuffer)
        {
            IntermediateRenderTarget downscaleTarget = factory.GetRenderTargetFromPool(input.Width / factor, input.Height / factor, format);
            Texture2D tex = this.DownScale(rHelper, input, downscaleTarget.RenderTarget, technicName, useFloatingBuffer);
            downscaleTarget.InUse = false;
            return tex;
        }

        private Texture2D Luminance(RenderHelper rHelper, Texture2D input,RenderTarget2D target,String techniqueName)
        {
            luminance.CurrentTechnique = luminance.Techniques[techniqueName];
            luminance.Parameters["SourceTexture0"].SetValue(input);
            luminance.Parameters["g_vSourceDimensions"].SetValue(new Vector2(input.Width, input.Height));
            luminance.Parameters["g_vDestinationDimensions"].SetValue(new Vector2(target.Width, target.Height));
            rHelper.PushRenderTarget(target);
            rHelper.RenderFullScreenQuadVertexPixel(luminance);
            return rHelper.PopRenderTargetAsSingleRenderTarget2D();            
            
        }
        private Texture2D BlurHV(RenderHelper rHelper, Texture2D input, RenderTarget2D target)
        {            
            String baseTechniqueName = "GaussianBlur";

            // Do horizontal pass first
            fULLGPUBlur.CurrentTechnique = fULLGPUBlur.Techniques[baseTechniqueName + "X"];
            fULLGPUBlur.Parameters["g_fSigma"].SetValue(blurSigma);
            fULLGPUBlur.Parameters["SourceTexture0"].SetValue(input);
            fULLGPUBlur.Parameters["g_vSourceDimensions"].SetValue(new Vector2(input.Width, input.Height));
            fULLGPUBlur.Parameters["g_vDestinationDimensions"].SetValue(new Vector2(target.Width, target.Height));
            rHelper.PushRenderTarget(target);
            rHelper.RenderFullScreenQuadVertexPixel(fULLGPUBlur);
            Texture2D pass1 = rHelper.PopRenderTargetAsSingleRenderTarget2D();

            // Now the vertical pass             
            IntermediateRenderTarget t2 = factory.GetRenderTargetFromPool(target.Width, target.Height, SurfaceFormat.Color);                        
            fULLGPUBlur.CurrentTechnique = fULLGPUBlur.Techniques[baseTechniqueName + "Y"];
            fULLGPUBlur.Parameters["SourceTexture0"].SetValue(pass1);
            rHelper.PushRenderTarget(t2.RenderTarget);
            rHelper.RenderFullScreenQuadVertexPixel(fULLGPUBlur);
            Texture2D tex =  rHelper.PopRenderTargetAsSingleRenderTarget2D();
            t2.InUse = false;
            return tex;

        }


        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            if (firstTime)
            {                
                rHelper.PushRenderTarget(lastFrameAdaptedLuminance);                
                rHelper.Clear(Color.White,ClearOptions.Target);
                rHelper.PopRenderTargetAsSingleRenderTarget2D();
                firstTime = false;
            }

                long time = stopwatch.ElapsedMilliseconds;
                dt = (time - lastTime) / 1000.0f;
                lastTime = time;

                Texture2D d4 = DownScale(rHelper, ImageToProcess, 4, SurfaceFormat.HalfVector4, "Downscale4", useFloatingBuffer);              
               
               Texture2D d16 = DownScale(rHelper, d4, 4, SurfaceFormat.HalfVector4, "Downscale4", useFloatingBuffer);
                      
               Texture2D tex = Luminance(rHelper, d16, luminanceChain[0], "Luminance");               
               
               
               for (int i = 1; i < luminanceChain.Length; i++)
                   tex = DownScale(rHelper, tex, luminanceChain[i], "Downscale4", useFloatingBuffer);   ///possivelmente mudar para o efeito Downscale4Luminance
               
               // Final downscale                           
               tex = DownScale(rHelper, tex, currentFrameLuminance, "Downscale4Luminance", useFloatingBuffer);
                        
               // Adapt the luminance, to simulate slowly adjust exposure
               luminance.Parameters["g_fDT"].SetValue(dt);
               luminance.Parameters["fTau"].SetValue(fTau);
               luminance.Parameters["SourceTexture1"].SetValue(lastFrameAdaptedLuminance);
               tex = Luminance(rHelper, tex, currentFrameAdaptedLuminance, "CalcAdaptedLuminance");

               ///Bloom               
               IntermediateRenderTarget thresholdTex = factory.GetRenderTargetFromPool(d16.Width, d16.Height, SurfaceFormat.HalfVector4);
               threshold.CurrentTechnique = threshold.Techniques["Threshold"];
               threshold.Parameters["g_fThreshold"].SetValue(bloomThreshold);
               threshold.Parameters["g_fMiddleGrey"].SetValue(toneMapKey);
               threshold.Parameters["g_fMaxLuminance"].SetValue(maxLuminance);               
               threshold.Parameters["SourceTexture0"].SetValue(d16);
               threshold.Parameters["SourceTexture1"].SetValue(tex);
               threshold.Parameters["g_vSourceDimensions"].SetValue(new Vector2(d16.Width, d16.Height));
               threshold.Parameters["g_vDestinationDimensions"].SetValue(new Vector2(thresholdTex.RenderTarget.Width, thresholdTex.RenderTarget.Height));
               rHelper.PushRenderTarget(thresholdTex.RenderTarget);
               rHelper.RenderFullScreenQuadVertexPixel(threshold);               
               Texture2D blom = rHelper.PopRenderTargetAsSingleRenderTarget2D();

               IntermediateRenderTarget postBlur = factory.GetRenderTargetFromPool(d16.Width, d16.Height, SurfaceFormat.Color);
               Texture2D blur = BlurHV(rHelper, blom, postBlur.RenderTarget);
               thresholdTex.InUse = false;               

               IntermediateRenderTarget upscale1 = factory.GetRenderTargetFromPool(GraphicInfo.BackBufferWidth / 8, GraphicInfo.BackBufferHeight / 8, SurfaceFormat.Color);
               DownScale(rHelper, postBlur.RenderTarget, upscale1.RenderTarget, "ScaleHW", useFloatingBuffer);
               postBlur.InUse = false;               

               IntermediateRenderTarget upscale2 = factory.GetRenderTargetFromPool(GraphicInfo.BackBufferWidth / 4, GraphicInfo.BackBufferHeight / 4, SurfaceFormat.Color);
               DownScale(rHelper, upscale1.RenderTarget, upscale2.RenderTarget, "ScaleHW", useFloatingBuffer);
               upscale1.InUse = false;               

               IntermediateRenderTarget bloom = factory.GetRenderTargetFromPool(GraphicInfo.BackBufferWidth / 2, GraphicInfo.BackBufferHeight / 2, SurfaceFormat.Color);
               Texture2D resp = DownScale(rHelper, upscale2.RenderTarget, bloom.RenderTarget, "ScaleHW",useFloatingBuffer);
               upscale2.InUse = false;               

               //toScreen.Parameters["SourceTexture0"].SetValue(resp);
               //toScreen.Parameters["g_vSourceDimensions"].SetValue(new Vector2(resp.Width, resp.Height));
               //toScreen.Parameters["g_vDestinationDimensions"].SetValue(new Vector2(GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight));
               //rHelper.RenderFullScreenQuadVertexPixel(toScreen);

               // Now do tone mapping on the main source image, and add in the bloom
               tone.CurrentTechnique = tone.Techniques["Tone"];
               tone.Parameters["g_fMiddleGrey"].SetValue(toneMapKey);
               tone.Parameters["g_fMaxLuminance"].SetValue(maxLuminance);
               tone.Parameters["g_fBloomMultiplier"].SetValue(bloomMultiplier);
               tone.Parameters["g_vDestinationDimensions"].SetValue(new Vector2(GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight));
               tone.Parameters["SourceTexture0"].SetValue(ImageToProcess);
               tone.Parameters["SourceTexture1"].SetValue(currentFrameAdaptedLuminance);
               tone.Parameters["SourceTexture2"].SetValue(resp);
               rHelper.RenderFullScreenQuadVertexPixel(tone);              
               
               //// Flip the luminance textures
               Swap(ref currentFrameAdaptedLuminance, ref lastFrameAdaptedLuminance);

               bloom.InUse = false;
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            this.factory = factory;
            effect = factory.GetEffect("DownScaling", false, true);             
            luminance = factory.GetEffect("Luminance", false, true);             
            threshold = factory.GetEffect("Threshold", false, true);             
            fULLGPUBlur = factory.GetEffect("FULLGPUBlur", false, true);             
            tone = factory.GetEffect("ToneHdr", false, true);             
                      

            int chainLength = 1;
            int startSize = (int)MathHelper.Min(ginfo.BackBufferWidth / 16, ginfo.BackBufferHeight / 16);
            int size = 16;
            for (size = 16; size < startSize; size *= 4)
                chainLength++;

            luminanceChain = new RenderTarget2D[chainLength];
            size /= 4;
            for (int i = 0; i < chainLength; i++)
            {
                luminanceChain[i] = factory.CreateRenderTarget(size, size, SurfaceFormat.Single, false, DepthFormat.None);
                size /= 4;
            }


            currentFrameLuminance = factory.CreateRenderTarget(1, 1, SurfaceFormat.Single,false,DepthFormat.None,0,RenderTargetUsage.PreserveContents);
            currentFrameAdaptedLuminance = factory.CreateRenderTarget(1, 1, SurfaceFormat.Single, false, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
            lastFrameAdaptedLuminance = factory.CreateRenderTarget(1, 1, SurfaceFormat.Single, false, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);            

        }

        protected void Swap(ref RenderTarget2D rt1, ref RenderTarget2D rt2)
        {
            RenderTarget2D temp = rt1;
            rt1 = rt2;
            rt2 = temp;
        }


    }
}
