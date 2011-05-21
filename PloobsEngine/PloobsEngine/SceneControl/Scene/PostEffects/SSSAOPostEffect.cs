using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// gaussian Blur Modes
    /// </summary>
    public enum BlurMode
    {
        SINGLE, DOUBLE, NONE
    }

    public class SSAOPostEffect : IPostEffect
    {
        public SSAOPostEffect() : base(PostEffectType.Deferred) { }

        Effect effect = null;
        Effect ssaofinal = null;        
        Texture2D RandomTexture;
        RenderTarget2D target;
        RenderTarget2D target2;
        GaussianBlurPostEffect gbp;
        BlurMode blurMode = BlurMode.SINGLE;

        public BlurMode BlurMode
        {
            get { return blurMode; }
            set { blurMode = value; }
        }
        bool outputONLYSSAOMAP = false;

        public bool OutputONLYSSAOMAP
        {
            get { return outputONLYSSAOMAP; }
            set { outputONLYSSAOMAP = value; }
        }
        //Vector3[] rd;
        //Texture2D t;

        float jitter = 0.0013f;

        public float Jitter
        {
            get { return jitter; }
            set { jitter = value; }
        }
        float intensity = 1;

        public float Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }
        float diffscale = 1;

        public float Diffscale
        {
            get { return diffscale; }
            set { diffscale = value; }
        }

        float whiteCorrection = 0.5f;

        public float WhiteCorrection
        {
            get { return whiteCorrection; }
            set { whiteCorrection = value; }
        }


        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {

             effect.Parameters["Params"].SetValue(new Vector4(GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight, world.CameraManager.ActiveCamera.FarPlane, intensity));
             effect.Parameters["DepthBuffer"].SetValue(rHelper[PrincipalConstants.DephRT]);
             effect.Parameters["NormalBuffer"].SetValue(rHelper[PrincipalConstants.normalRt]);
             effect.Parameters["RandomTexture"].SetValue(RandomTexture);
             effect.Parameters["InvProj"].SetValue(Matrix.Transpose( Matrix.Invert(world.CameraManager.ActiveCamera.Projection)));
             effect.Parameters["View"].SetValue(world.CameraManager.ActiveCamera.View);
             effect.Parameters["jitter"].SetValue(jitter);
             effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
             effect.Parameters["diffScale"].SetValue(diffscale);
             effect.Parameters["whiteCorrection"].SetValue(whiteCorrection);

             if (outputONLYSSAOMAP)
             {                 
                 rHelper.RenderFullScreenQuadVertexPixel(effect);
                 return;
             }

             rHelper.PushRenderTarget(target);
             rHelper.RenderFullScreenQuadVertexPixel(effect);
             Texture2D r = rHelper.PopRenderTargetAsSingleRenderTarget2D();

             if (blurMode == BlurMode.SINGLE || blurMode == BlurMode.DOUBLE)
             {
                 rHelper.PushRenderTarget(target2);
                 gbp.Draw(r, rHelper, gt, GraphicInfo, world, useFloatingBuffer);
                 Texture2D x = rHelper.PopRenderTargetAsSingleRenderTarget2D();                

                 if (blurMode == BlurMode.DOUBLE)
                 {
                     rHelper.PushRenderTarget(target2);
                     gbp.Draw(x, rHelper, gt,GraphicInfo, world,useFloatingBuffer);
                     x = rHelper.PopRenderTargetAsSingleRenderTarget2D();                     
                 }
                 ssaofinal.Parameters["SSAOTex"].SetValue(x);
             }
             else if(blurMode == BlurMode.NONE)
             {
                 ssaofinal.Parameters["SSAOTex"].SetValue(r);
             }
             
             ssaofinal.Parameters["SceneTexture"].SetValue(ImageToProcess);
             ssaofinal.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
             ssaofinal.Parameters["weight"].SetValue(1);
             if (useFloatingBuffer)
                 rHelper.RenderFullScreenQuadVertexPixel(ssaofinal, SamplerState.PointClamp);
             else
                 rHelper.RenderFullScreenQuadVertexPixel(ssaofinal, GraphicInfo.SamplerState);              
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color,ginfo.UseMipMap,DepthFormat.None,ginfo.MultiSample);

            target2 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample);
            
            effect = factory.GetEffect("SSAOPOST",false,true);
            ssaofinal = factory.GetEffect("ssaofinal",false,true);

            RandomTexture = factory.GetTexture2D("random",true);
            gbp = new GaussianBlurPostEffect();
            gbp.Init(ginfo,factory);            
        }

    }
}


