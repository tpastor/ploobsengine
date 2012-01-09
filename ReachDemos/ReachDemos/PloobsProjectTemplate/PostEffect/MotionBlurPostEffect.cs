using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsProjectTemplate.PostEffect
{
    public class MotionBlurPostEffect : IPostEffect
    {
        public MotionBlurPostEffect() : base(PostEffectType.All) { }
                
        Texture2D end = null;
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {            
            //mix with last frame downsampled
            if (tex != null)
            {
                rHelper.PushRenderTarget(rtend);
                rHelper.RenderTextureComplete(tex, Color.FromNonPremultiplied(255, 255, 255, 255), GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.LinearClamp);
                rHelper.RenderTextureComplete(ImageToProcess, Color.FromNonPremultiplied(255, 255, 255, Amount), GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.LinearClamp, BlendState.AlphaBlend);                
                end = rHelper.PopRenderTargetAsSingleRenderTarget2D();
            }            

            //DownSample
            rHelper.PushRenderTarget(rt);
            rHelper.Clear(Color.Black);
            if (end == null)
            {
                rHelper.RenderTextureComplete(ImageToProcess, Color.White, rt.Bounds, Matrix.Identity, ImageToProcess.Bounds, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp);
            }
            else
            {
                rHelper.RenderTextureComplete(end, Color.White, rt.Bounds, Matrix.Identity, ImageToProcess.Bounds, true, SpriteSortMode.Deferred, SamplerState.AnisotropicClamp);
            }
            tex = rHelper.PopRenderTargetAsSingleRenderTarget2D();                        

            if(end!=null)
                rHelper.RenderTextureComplete(end, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.LinearClamp);
            else
                rHelper.RenderTextureComplete(ImageToProcess, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity, null, true, SpriteSortMode.Deferred, SamplerState.LinearClamp);
             
        }

        public int Amount
        {
            get;
            set;

        }

        Texture2D tex;
        RenderTarget2D rt;
        RenderTarget2D rtend;
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            rt = factory.CreateRenderTarget(ginfo.BackBufferWidth , ginfo.BackBufferHeight );
            rtend = factory.CreateRenderTarget(ginfo.BackBufferWidth , ginfo.BackBufferHeight ); 
            Amount = 100;
        }
    }
}
