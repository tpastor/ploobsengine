using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace EngineTestes.Post
{
    public class DownSampler
    {
        Effect effect;
        Rectangle Rectangle;
        RenderTarget2D RenderTarget2D;
        public DownSampler(GraphicFactory factory,float width, float height,
            SurfaceFormat SurfaceFormat = SurfaceFormat.Color,bool mipmap = false)
        {
            this.Rectangle = new Rectangle(0,0,(int)width,(int)height);
            RenderTarget2D = factory.CreateRenderTarget(Rectangle.Width,
                Rectangle.Height, SurfaceFormat, mipmap);
            effect = factory.GetEffect("SBlurPost/DownsampleDepth");
        }

        /// <summary>
        /// DownSample a Color Texture (using hardware Linear filtering, cant be used on Single/float Textures)
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="Texture2D">The texture2 D.</param>
        /// <param name="SamplerState">State of the sampler.</param>
        /// <returns></returns>
        public Texture2D DownColorTexture(RenderHelper render, Texture2D Texture2D, SamplerState SamplerState)
        {
            render.PushRenderTarget(RenderTarget2D);
            render.RenderTextureComplete(Texture2D, Color.White,
                Rectangle, Matrix.Identity, Texture2D.Bounds,
                true, SpriteSortMode.Deferred, SamplerState);
            return render.PopRenderTargetAsSingleRenderTarget2D();
        }

        /// <summary>
        /// DownSample a Single Texture using a Custom Filtering
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="Texture2D">The texture2 D.</param>
        /// <returns></returns>
        public Texture2D DownSingleTexture(RenderHelper render, Texture2D Texture2D)
        {
            effect.CurrentTechnique= effect.Techniques[0];
            effect.Parameters["PixelSize"].SetValue(new Vector2( 1f/Rectangle.Width , 1f/Rectangle.Height));
            effect.Parameters["HalfPixel"].SetValue(new Vector2(0.5f / RenderTarget2D.Width, 0.5f / RenderTarget2D.Height));
            render.Textures[0] = Texture2D;
            SamplerState s0 =render.SetSamplerState(SamplerState.PointClamp, 0);

            render.PushRenderTarget(RenderTarget2D);
            render.RenderFullScreenQuadVertexPixel(effect);
            render.SetSamplerState(s0, 0);
            return render.PopRenderTargetAsSingleRenderTarget2D();
        }

        /// <summary>
        /// DownSample a texture using bilinear filtering implemented in Shader.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="Texture2D">The texture2 D.</param>
        /// <returns></returns>
        public Texture2D DownShaderBilinearTexture(RenderHelper render, Texture2D Texture2D)
        {
            effect.CurrentTechnique = effect.Techniques[1];
            effect.Parameters["PixelSize"].SetValue(new Vector2(1f / Texture2D.Width, 1f / Texture2D.Height));
            effect.Parameters["HalfPixel"].SetValue(new Vector2(0.5f / Rectangle.Width, 0.5f / Rectangle.Height));
            render.Textures[0] = Texture2D;
            SamplerState s0 = render.SetSamplerState(SamplerState.PointClamp, 0);

            render.PushRenderTarget(RenderTarget2D);
            render.RenderFullScreenQuadVertexPixel(effect);
            render.SetSamplerState(s0, 0);
            return render.PopRenderTargetAsSingleRenderTarget2D();
        }
    }
}
