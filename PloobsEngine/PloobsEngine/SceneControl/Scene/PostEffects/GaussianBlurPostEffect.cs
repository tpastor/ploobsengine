#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
#if !WINDOWS_PHONE && !REACH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.SceneControl
{
    public class GaussianBlurPostEffect : IPostEffect
    {
        public GaussianBlurPostEffect() : base(PostEffectType.All) { }

#region IPostEffect Members

        private Effect gblur;        
        private RenderTarget2D target;
        private Vector2[] sampleOffsetsH = new Vector2[15];
        private float[] sampleWeightsH = new float[15];                

        private Vector2[] sampleOffsetsV = new Vector2[15];
        private float[] sampleWeightsV = new float[15];

        private Texture2D intermediateTex;

        void SetBlurParameters(float dx, float dy, ref Vector2[] vSampleOffsets, ref float[] fSampleWeights)
        {
            // The first sample always has a zero offset.
            fSampleWeights[0] = ComputeGaussian(0);
            vSampleOffsets[0] = new Vector2(0);

            // Maintain a sum of all the weighting values.
            float totalWeights = fSampleWeights[0];

            // Add pairs of additional sample taps, positioned
            // along a line in both directions from the center.
            for (int i = 0; i < 15 / 2; i++)
            {
                // Store weights for the positive and negative taps.
                float weight = ComputeGaussian(i + 1);

                fSampleWeights[i * 2 + 1] = weight;
                fSampleWeights[i * 2 + 2] = weight;

                totalWeights += weight * 2;

                // The 1.5 offset kicks things off by
                // positioning us nicely in between two texels.
                float sampleOffset = i * 2 + 1.5f;

                Vector2 delta = new Vector2(dx, dy) * sampleOffset;

                // Store texture coordinate offsets for the positive and negative taps.
                vSampleOffsets[i * 2 + 1] = delta;
                vSampleOffsets[i * 2 + 2] = -delta;
            }

            // Normalize the list of sample weightings, so they will always sum to one.
            for (int i = 0; i < fSampleWeights.Length; i++)
                fSampleWeights[i] /= totalWeights;
        }
        private float ComputeGaussian(float n)
        {
            float theta = 2.0f + float.Epsilon;
            return theta = (float)((1.0 / Math.Sqrt(2 * Math.PI * theta)) * Math.Exp(-(n * n) / (2 * theta * theta)));
        }
        

        // Set blur parameters to effect
        void SetParameters(GaussianBlurDirection Direction)
        {
            if (Direction == GaussianBlurDirection.Horizontal)
            {
                gblur.Parameters["sampleWeights"].SetValue(sampleWeightsH);
                gblur.Parameters["sampleOffsets"].SetValue(sampleOffsetsH);
            }
            else
            {
                gblur.Parameters["sampleWeights"].SetValue(sampleWeightsV);
                gblur.Parameters["sampleOffsets"].SetValue(sampleOffsetsV);
            }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {   
            rHelper.PushRenderTarget(target);
            rHelper.Clear(Color.Black,ClearOptions.Target);

            SetParameters(GaussianBlurDirection.Horizontal);
            if (useFloatingBuffer)

                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, gblur, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, gblur, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);




            intermediateTex  = rHelper.PopRenderTargetAsSingleRenderTarget2D();
            
             SetParameters(GaussianBlurDirection.Vertical); // Set vertical parameters
             rHelper.RenderTextureToFullScreenSpriteBatch(intermediateTex, gblur, GraphicInfo.FullScreenRectangle);             
        }
        

        #endregion

#region IPostEffect Members

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            gblur = factory.GetEffect("gblur",true,true);            
            target = factory.CreateRenderTarget(ginfo.BackBufferWidth,ginfo.BackBufferHeight,SurfaceFormat.Color,ginfo.UseMipMap,DepthFormat.None,ginfo.MultiSample);

            Vector2 texelSize = new Vector2(1f / ginfo.BackBufferWidth, 1f / ginfo.BackBufferHeight);
            SetBlurParameters(texelSize.X, 0, ref sampleOffsetsH, ref sampleWeightsH);
            SetBlurParameters(0, texelSize.Y, ref sampleOffsetsV, ref sampleWeightsV);
        }

        #endregion

    }
    internal enum GaussianBlurDirection { Horizontal, Vertical };
}
#endif