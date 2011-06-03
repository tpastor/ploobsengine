using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{

    public class SaturationPostEffect : IPostEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaturationPostEffect"/> class.
        /// </summary>
        /// <param name="saturation">The saturation.</param>
        public SaturationPostEffect(float saturation = 0.5f)
            : base(PostEffectType.All)
        {
            this.saturation = saturation;
        }

        #region IPostEffect Members

        Effect effect = null;

        float saturation = 0.5f;
        public float Saturation
        {
            get { return saturation; }
            set { saturation = value; }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {         
            ///Draw a quad using the "effect", passing the CurrentImage as a Parameter            
            effect.Parameters["saturation"].SetValue(saturation);

            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("saturation",false,true);            
        }

        #endregion

    }
}
