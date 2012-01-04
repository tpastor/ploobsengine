#if !WINDOWS_PHONE && !REACH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Motion BLur Post Effect For Deferred Shading
    /// </summary>
    public class MotionBlurPostEffect : IPostEffect
    {
        /// <summary>
        /// Create an instance of MotionBlur Post Effect
        /// </summary>
        public MotionBlurPostEffect()
            : base(PostEffectType.Deferred)
        {
            NumSamples = 10;
            Attenuation = 0.2f;
        }

        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("MotionBlur",false,true);
        }
        Effect effect;
        Matrix oldViewProjection;

        /// <summary>
        /// Gets or sets the num samples.
        /// Default 10
        /// </summary>
        /// <value>
        /// The num samples.
        /// </value>
        public int NumSamples
        {
            set;
            get;
        }

        bool firstTime = true;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MotionBlurPostEffect"/> is attenuation.
        /// Default 0.2f
        /// </summary>
        /// <value>
        ///   <c>true</c> if attenuation; otherwise, <c>false</c>.
        /// </value>
        public float Attenuation
        {
            set;
            get;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {

            if (firstTime)
            {
                oldViewProjection = world.CameraManager.ActiveCamera.ViewProjection;
                firstTime = false;
            }

            effect.Parameters["attenuation"].SetValue(Attenuation);
            effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
            effect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(world.CameraManager.ActiveCamera.ViewProjection));
            effect.Parameters["oldViewProjection"].SetValue(oldViewProjection);
            effect.Parameters["numSamples"].SetValue(NumSamples);
            effect.Parameters["depth"].SetValue(rHelper[PrincipalConstants.DephRT]);
            effect.Parameters["extra"].SetValue(rHelper[PrincipalConstants.extra1RT]);
            effect.Parameters["cena"].SetValue(ImageToProcess);

            oldViewProjection = world.CameraManager.ActiveCamera.ViewProjection;

            if (useFloatBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect, GraphicInfo.SamplerState);
        }
    }
}

#endif