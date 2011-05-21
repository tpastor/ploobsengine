using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{

    public class ContrastPostEffect : IPostEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContrastPostEffect"/> class.
        /// </summary>
        /// <param name="contrast">-1 a 1</param>
        public ContrastPostEffect(float contrast) : base(PostEffectType.All)
        {
            this.contrast  = contrast;
        }

        #region IPostEffect Members

        Effect effect = null;
        float contrast = 0.5f;
        
        public float Contrast
        {
            get { return contrast; }
            set { contrast = value; }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {            
            effect.Parameters["Contrast"].SetValue(contrast);
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("contrast",false,true);            
        }

        #endregion

    }
}
