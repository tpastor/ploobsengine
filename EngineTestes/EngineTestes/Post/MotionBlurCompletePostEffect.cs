using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine;

namespace EngineTestes.Post
{
    class MotionBlurCompletePostEffect : IPostEffect
    {
        public MotionBlurCompletePostEffect()
            : base(PostEffectType.AllHidef)
        {
            NumSamples = 6;
        }
        
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            eff = factory.GetEffect("Effects/MotionBlurComplete");
            effectvelocity = factory.GetEffect("Effects/VelocityTextureMotionBlur");
            rt = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
        }
        Effect effectvelocity;
        Effect eff;
        Matrix oldViewProjection;
        RenderTarget2D rt;

        public int NumSamples
        {
            set;
            get;
        }

        bool firstTime = true;


        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {            
            if (firstTime)
            {
                oldViewProjection = world.CameraManager.ActiveCamera.ViewProjection;
                firstTime = false;
            }

            rHelper.PushRenderTarget(rt);
            rHelper.Clear(Color.Black);
            rHelper.RenderSceneWithCustomMaterial(effectvelocity,
                (effect, obj, bi,ti,s,er,wvp) =>
                {                    
                    Matrix w1 = Matrix.Multiply(obj.WorldMatrix, bi.ModelLocalTransformation);
                    effect.Parameters["wvp"].SetValue(w1 * wvp);
                    effect.Parameters["oldwvp"].SetValue(w1 * oldViewProjection);
                }, world, gt, null, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, false, true);

            Texture2D tex = rHelper.PopRenderTargetAsSingleRenderTarget2D();

            eff.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
            eff.Parameters["numSamples"].SetValue(NumSamples);
            eff.Parameters["velocity"].SetValue(tex);
            eff.Parameters["cena"].SetValue(ImageToProcess);

            oldViewProjection = world.CameraManager.ActiveCamera.ViewProjection;                        

            if (useFloatBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(eff, SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(eff, GraphicInfo.SamplerState);                
        }
    }
}
