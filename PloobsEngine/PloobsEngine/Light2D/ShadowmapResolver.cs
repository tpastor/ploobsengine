using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace PloobsEngine.Light2D
{
    public enum ShadowmapSize
    {
        Size128 = 6,
        Size256 = 7,
        Size512 = 8,
        Size1024 = 9,
    }
    class ShadowmapResolver
    {
        private GraphicsDevice graphicsDevice;

        private int reductionChainCount;
        private int baseSize;
        private int depthBufferSize;

        Effect resolveShadowsEffect;
        Effect reductionEffect;

        RenderTarget2D distortRT;
        RenderTarget2D shadowMap;
        RenderTarget2D shadowsRT;
        RenderTarget2D processedShadowsRT;

        QuadRender quadRender;
        RenderTarget2D distancesRT;
        RenderTarget2D[] reductionRT;


        /// <summary>
        /// Creates a new shadowmap resolver
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="quadRender">The quad render.</param>
        /// <param name="maxShadowmapSize">Size of the max shadowmap.</param>
        /// <param name="maxDepthBufferSize">Size of the max depth buffer.</param>
        public ShadowmapResolver(GraphicFactory factory, QuadRender quadRender, ShadowmapSize maxShadowmapSize, ShadowmapSize maxDepthBufferSize)
        {
            this.graphicsDevice = factory.device;
            this.quadRender = quadRender;

            reductionChainCount = (int)maxShadowmapSize;
            baseSize = 2 << reductionChainCount;
            depthBufferSize = 2 << (int)maxDepthBufferSize;
            LoadContent(factory);
        }

        private void LoadContent(GraphicFactory factory)
        {
            reductionEffect = factory.GetEffect("reductionEffect",false,true);
            resolveShadowsEffect = factory.GetEffect("resolveShadowsEffect", false, true);

            distortRT = new RenderTarget2D(graphicsDevice, baseSize, baseSize, false, SurfaceFormat.HalfVector2,DepthFormat.None);
            distancesRT = new RenderTarget2D(graphicsDevice, baseSize, baseSize, false, SurfaceFormat.HalfVector2,DepthFormat.None);
            shadowMap = new RenderTarget2D(graphicsDevice, 2, baseSize, false, SurfaceFormat.HalfVector2,DepthFormat.None);
            reductionRT = new RenderTarget2D[reductionChainCount];
            for (int i = 0; i < reductionChainCount; i++)
            {
                reductionRT[i] = new RenderTarget2D(graphicsDevice, 2 << i, baseSize, false, SurfaceFormat.HalfVector2,DepthFormat.None);
            }

            shadowsRT = new RenderTarget2D(graphicsDevice, baseSize, baseSize);
            processedShadowsRT = new RenderTarget2D(graphicsDevice, baseSize, baseSize);
        }

        public void ResolveShadows(Light2D light)
        {
            graphicsDevice.BlendState = BlendState.Opaque;
            ExecuteTechnique(light.RenderTarget, distancesRT, "ComputeDistances");
            ExecuteTechnique(distancesRT, distortRT, "Distort");
            ApplyHorizontalReduction(distortRT, shadowMap);

            resolveShadowsEffect.Parameters["intensity"].SetValue(light.Intensity);
            if (light is PointLight2D)
            {
                resolveShadowsEffect.CurrentTechnique = resolveShadowsEffect.Techniques["DrawShadowsPoint"];
            }
            else if (light is SpotLight2D)
            {
                SpotLight2D sl = (SpotLight2D)light;
                resolveShadowsEffect.CurrentTechnique = resolveShadowsEffect.Techniques["DrawShadowsSpot"];
                resolveShadowsEffect.Parameters["Direction"].SetValue(sl.Direction);
                resolveShadowsEffect.Parameters["anglecossine"].SetValue((float)Math.Cos(sl.Angle));
            }
            else
            {
                throw new Exception("Unsupported Light type =P");
            }

            ExecuteTechnique(null, shadowsRT, null, shadowMap);
            ExecuteTechnique(shadowsRT, processedShadowsRT, "BlurHorizontally");
            ExecuteTechnique(processedShadowsRT, light.RenderTarget, "BlurVerticallyAndAttenuate");
        }

        private void ExecuteTechnique(Texture2D source, RenderTarget2D destination, string techniqueName)
        {
            ExecuteTechnique(source, destination, techniqueName, null);
        }

        private void ExecuteTechnique(Texture2D source, RenderTarget2D destination, string techniqueName, Texture2D shadowMap)
        {
            Vector2 renderTargetSize;
            renderTargetSize = new Vector2((float)baseSize, (float)baseSize);
            graphicsDevice.SetRenderTarget(destination);
            graphicsDevice.Clear(Color.White);
            resolveShadowsEffect.Parameters["renderTargetSize"].SetValue(renderTargetSize);

            if (source != null)
                resolveShadowsEffect.Parameters["InputTexture"].SetValue(source);
            if (shadowMap !=null)
                resolveShadowsEffect.Parameters["ShadowMapTexture"].SetValue(shadowMap);

            if(techniqueName!= null)
                resolveShadowsEffect.CurrentTechnique = resolveShadowsEffect.Techniques[techniqueName];

            foreach (EffectPass pass in resolveShadowsEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                quadRender.Render(Vector2.One * -1, Vector2.One);
            }
            graphicsDevice.SetRenderTarget(null);
        }


        private void ApplyHorizontalReduction(RenderTarget2D source, RenderTarget2D destination)
        {
            int step = reductionChainCount-1;
            RenderTarget2D s = source;
            RenderTarget2D d = reductionRT[step];
            reductionEffect.CurrentTechnique = reductionEffect.Techniques["HorizontalReduction"];

            while (step >= 0)
            {
                d = reductionRT[step];

                graphicsDevice.SetRenderTarget(d);
                graphicsDevice.Clear(Color.White);

                reductionEffect.Parameters["SourceTexture"].SetValue(s);
                Vector2 textureDim = new Vector2(1.0f / (float)s.Width, 1.0f / (float)s.Height);
                reductionEffect.Parameters["TextureDimensions"].SetValue(textureDim);

                foreach (EffectPass pass in reductionEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    quadRender.Render(Vector2.One * -1, new Vector2(1, 1));
                }

                graphicsDevice.SetRenderTarget(null);

                s = d;
                step--;
            }

            //copy to destination
            graphicsDevice.SetRenderTarget(destination);
            reductionEffect.CurrentTechnique = reductionEffect.Techniques["Copy"];
            reductionEffect.Parameters["SourceTexture"].SetValue(d);

            foreach (EffectPass pass in reductionEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                quadRender.Render(Vector2.One * -1, new Vector2(1, 1));
            }

            reductionEffect.Parameters["SourceTexture"].SetValue(reductionRT[reductionChainCount - 1]);
            graphicsDevice.SetRenderTarget(null);
        }

        
    }
}
