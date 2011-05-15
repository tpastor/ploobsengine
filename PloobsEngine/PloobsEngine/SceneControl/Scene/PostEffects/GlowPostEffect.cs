using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.SceneControl
{
    public class GlowPostEffect : IPostEffect
    {
        public GlowPostEffect() : base(PostEffectType.Deferred) { }

        Effect effect = null;                      
        RenderTarget2D target;
        RenderTarget2D target2;  
        GaussianBlurPostEffect gbp;
        Texture2D x;
        private bool doubleBlur = false;

        public bool DoubleBlur
        {
            get { return doubleBlur; }
            set { doubleBlur = value; }
        }

        private float intensity = 1;

        public float Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }        

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {         
             rHelper.PushRenderTarget(target);
             gbp.Draw(rHelper[PrincipalConstants.extra1RT], rHelper,  gt, GraphicInfo, world,false);
             x = rHelper.PopRenderTargetAsSingleRenderTarget2D();

             if (doubleBlur)
             {
                 rHelper.PushRenderTarget(target2);
                 gbp.Draw(x, rHelper, gt, GraphicInfo, world, false);
                 x = rHelper.PopRenderTargetAsSingleRenderTarget2D();
             }

             effect.Parameters["intensity"].SetValue(intensity);
             effect.Parameters["glowMapBlurried"].SetValue(x);
             effect.Parameters["colorMap"].SetValue(ImageToProcess);
             effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);

             if (useFloatingBuffer)
                 rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
             else
                 rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.LinearClamp);
         
        }
         
        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            target2 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);            
            effect = factory.GetEffect("glowPost",false,true);

            gbp = new GaussianBlurPostEffect();
            gbp.Init(ginfo, factory); 


        }

    }
}


