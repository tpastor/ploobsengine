using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine;
using PloobsEngine.Engine.Logger;

namespace EngineTestes.Post
{
    class MotionBlurCompletePostEffect : IPostEffect
    {
        public MotionBlurCompletePostEffect()
            : base(PostEffectType.AllHidef)
        {
            NumSamples = 4;
            Attenuation = 0.2f;
        }
        
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            eff = factory.GetEffect("Effects/MotionBlurComplete");
            effectvelocity = factory.GetEffect("Effects/VelocityTextureMotionBlur");
            if (ginfo.CheckIfRenderTargetFormatIsSupported(SurfaceFormat.Vector2, DepthFormat.None, false, 0))
            {
                rt = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Vector2);
            }
            else
            {
                throw new Exception("required Buffer precision not found (Vector 2)");
            }
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

        public float Attenuation
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
            eff.Parameters["Attenuation"].SetValue(Attenuation);
            

            oldViewProjection = world.CameraManager.ActiveCamera.ViewProjection;                        

            if (useFloatBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(eff, SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(eff, GraphicInfo.SamplerState);                
        }
    }
}
