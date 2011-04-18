using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class ColorCorrectionPostEffect : IPostEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorCorrectionPostEffect"/> class.
        /// </summary>
        /// <param name="colorAdd">The color add.</param>
        /// <param name="colorCorrect">The color correct.</param>
        public ColorCorrectionPostEffect(Color colorAdd ,float colorCorrect) : base(PostEffectType.All)
        {
            this.colorAdd = colorAdd.ToVector3(); ;
            this.colorCorrect = colorCorrect;

        }

        #region IPostEffect Members

        Effect effect = null;

        Vector3 colorAdd;

        public Vector3 ColorAdd
        {
            get { return colorAdd; }
            set { colorAdd = value; }
        }
        float colorCorrect;

        public float ColorCorrect
        {
            get { return colorCorrect; }
            set { colorCorrect = value; }
        }

        float colorToAddIntensity = 1;

        public float ColorToAddIntensity
        {
            get { return colorToAddIntensity; }
            set { colorToAddIntensity = value; }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            ///Draw a quad using the "effect", passing the CurrentImage as a Parameter            
            effect.Parameters["ColorCorrect"].SetValue(colorCorrect);
            effect.Parameters["ColorAdd"].SetValue(colorAdd);
            effect.Parameters["ColorToAddIntensity"].SetValue(colorToAddIntensity);

            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.LinearClamp);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("colorCorrection",false,true);            
        }

        #endregion

    }
}
