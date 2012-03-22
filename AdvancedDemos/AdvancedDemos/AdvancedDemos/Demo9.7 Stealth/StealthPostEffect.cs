using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;

namespace EngineTestes.TransparencyOnBorders
{
    public class StealthPostEffect : IPostEffect
    {
        public StealthPostEffect()
            : base(PostEffectType.Deferred)
        {
        }

        private List<IObject> invisibleMaskObjects = new List<IObject>();

        public List<IObject> InvisibleMaskObjects
        {
            get { return invisibleMaskObjects; }
            set { invisibleMaskObjects = value; }
        }

        Effect effect;
        Effect effectdistorcion;
        RenderTarget2D target;
        Texture2D x;
        private const float blurAmount = 2f;


        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("effects/TransparentOnBorder");
            effectdistorcion = factory.GetEffect("effects/effect2");
            target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            SetBlurEffectParameters(1f / (float)ginfo.BackBufferWidth, 1f / (float)ginfo.BackBufferHeight);
        }
        public List<IObject> objs = new List<IObject>();

        /// <summary>
        /// ULTRA SENSIBLE
        /// EACH MODEL NEED A DIFERENT VALUE        
        /// </summary>
        float Distortion = 0.01f;

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            rHelper.PushRenderTarget(target);
            rHelper.Clear(Color.Black);
            effectdistorcion.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
            effectdistorcion.Parameters["DEPTH"].SetValue(rHelper[PrincipalConstants.DephRT]);
            effectdistorcion.Parameters["DistortionScale"].SetValue(Distortion);
            rHelper.PushDepthStencilState(DepthStencilState.None);

            foreach (var obj in objs)
            {
                IModelo modelo = obj.Modelo;

                for (int i = 0; i < modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = modelo.GetBatchInformation(i);
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        Matrix w = bi[j].ModelLocalTransformation * obj.WorldMatrix;
                        effectdistorcion.Parameters["WorldViewProjection"].SetValue(w * world.CameraManager.ActiveCamera.View * world.CameraManager.ActiveCamera.Projection);
                        effectdistorcion.Parameters["WorldView"].SetValue(w * world.CameraManager.ActiveCamera.View);                        
                        rHelper.RenderBatch(bi[j], effectdistorcion);
                    }
                }
            }

           rHelper.PopDepthStencilState();

           x = rHelper.PopRenderTargetAsSingleRenderTarget2D();

           rHelper.Clear(Color.Black);
           effect.Parameters["SceneTexture"].SetValue(ImageToProcess);
           effect.Parameters["DistortionMap"].SetValue(x);
           //effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);           
           rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess,effect,GraphicInfo.FullScreenRectangle);          
         
        }
        
        /// <summary>
        /// Computes sample weightings and texture coordinate offsets
        /// for one pass of a separable gaussian blur filter.
        /// </summary>
        /// <remarks>
        /// This function was originally provided in the BloomComponent class in the 
        /// Bloom Postprocess sample.
        /// </remarks>
        void SetBlurEffectParameters(float dx, float dy)
        {
            // Look up the sample weight and offset effect parameters.
            EffectParameter weightsParameter, offsetsParameter;

            weightsParameter = effect.Parameters["SampleWeights"];
            offsetsParameter = effect.Parameters["SampleOffsets"];

            // Look up how many samples our gaussian blur effect supports.
            int sampleCount = weightsParameter.Elements.Count;

            // Create temporary arrays for computing our filter settings.
            float[] sampleWeights = new float[sampleCount];
            Vector2[] sampleOffsets = new Vector2[sampleCount];

            // The first sample always has a zero offset.
            sampleWeights[0] = ComputeGaussian(0);
            sampleOffsets[0] = new Vector2(0);

            // Maintain a sum of all the weighting values.
            float totalWeights = sampleWeights[0];

            // Add pairs of additional sample taps, positioned
            // along a line in both directions from the center.
            for (int i = 0; i < sampleCount / 2; i++)
            {
                // Store weights for the positive and negative taps.
                float weight = ComputeGaussian(i + 1);

                sampleWeights[i * 2 + 1] = weight;
                sampleWeights[i * 2 + 2] = weight;

                totalWeights += weight * 2;

                // To get the maximum amount of blurring from a limited number of
                // pixel shader samples, we take advantage of the bilinear filtering
                // hardware inside the texture fetch unit. If we position our texture
                // coordinates exactly halfway between two texels, the filtering unit
                // will average them for us, giving two samples for the price of one.
                // This allows us to step in units of two texels per sample, rather
                // than just one at a time. The 1.5 offset kicks things off by
                // positioning us nicely in between two texels.
                float sampleOffset = i * 2 + 1.5f;

                Vector2 delta = new Vector2(dx, dy) * sampleOffset;

                // Store texture coordinate offsets for the positive and negative taps.
                sampleOffsets[i * 2 + 1] = delta;
                sampleOffsets[i * 2 + 2] = -delta;
            }

            // Normalize the list of sample weightings, so they will always sum to one.
            for (int i = 0; i < sampleWeights.Length; i++)
            {
                sampleWeights[i] /= totalWeights;
            }

            // Tell the effect about our new filter settings.
            weightsParameter.SetValue(sampleWeights);
            offsetsParameter.SetValue(sampleOffsets);
        }

        /// <summary>
        /// Evaluates a single point on the gaussian falloff curve.
        /// Used for setting up the blur filter weightings.
        /// </summary>
        /// <remarks>
        /// This function was originally provided in the BloomComponent class in the 
        /// Bloom Postprocess sample.
        /// </remarks>
        float ComputeGaussian(float n)
        {
            return (float)((1.0 / Math.Sqrt(2 * Math.PI * blurAmount)) *
                           Math.Exp(-(n * n) / (2 * blurAmount * blurAmount)));
        }

    }
}
