#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class RadialBlurPostEffect : IPostEffect
    {
        public RadialBlurPostEffect() : base(PostEffectType.All) { }

        #region IPostEffect Members
               
        private Effect effect = null;
        private Vector2 center = new Vector2(0.5f);
        private float globalAlpha = 1;
        private float pixelDistance = 0.01f;

        public float PixelDistance
        {
            get { return pixelDistance; }
            set { pixelDistance = value; }
        }

        public float GlobalAlpha
        {
            get { return globalAlpha; }
            set { globalAlpha = value; }
        }

        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            effect.Parameters["PixelDistance"].SetValue(pixelDistance);
            effect.Parameters["GlobalAlpha"].SetValue(globalAlpha);
            effect.Parameters["Center"].SetValue(center);

            ///Draw a quad using the "effect", passing the CurrentImage as a Parameter
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("RadialBlur",false,true);            
        }
       
        #endregion
    }
}
#endif