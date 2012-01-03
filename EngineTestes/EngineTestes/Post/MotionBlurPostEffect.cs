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
    class MotionBlurPostEffect : IPostEffect
    {
        public MotionBlurPostEffect()
            : base(PostEffectType.Deferred)
        {
            NumSamples = 6;
        }
        
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/MotionBlur");
        }
        Effect effect;
        Matrix oldViewProjection;

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
            
            effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
            effect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(world.CameraManager.ActiveCamera.ViewProjection));
            effect.Parameters["oldViewProjection"].SetValue(oldViewProjection);            
            effect.Parameters["numSamples"].SetValue(NumSamples);
            effect.Parameters["depth"].SetValue(rHelper[PrincipalConstants.DephRT]);
            effect.Parameters["cena"].SetValue(ImageToProcess);

            oldViewProjection = world.CameraManager.ActiveCamera.ViewProjection;                        

            if (useFloatBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect,SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect,GraphicInfo.SamplerState);                
        }
    }
}
