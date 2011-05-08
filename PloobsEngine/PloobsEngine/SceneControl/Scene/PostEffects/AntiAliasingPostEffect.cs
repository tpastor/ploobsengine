using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class AntiAliasingPostEffect : IPostEffect 
    {
        public AntiAliasingPostEffect() : base(PostEffectType.Deferred) 
        {
            this.priority = float.MaxValue;
        }

        private float weight = 1;

        /// <summary>
        /// Default 1
        /// Bigger value, bigger the effect
        /// </summary>
        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private float pixelSizeMultiplier = 1;

        /// <summary>
        /// Default 1
        /// Change only if you Know what are you doing
        /// </summary>
        public float PixelSizeMultiplier
        {
            get { return pixelSizeMultiplier; }
            set { pixelSizeMultiplier = value; }
        }

        Effect effect = null;
        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
                effect.Parameters["weight"].SetValue(weight);
                effect.Parameters["pixel_size"].SetValue(GraphicInfo.HalfPixel * 2 * pixelSizeMultiplier);
                effect.Parameters["normal"].SetValue(rHelper[PrincipalConstants.normalRt]);
                
                if (useFloatingBuffer)
                    rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle,SamplerState.PointClamp,BlendState.Opaque);
                else
                    rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.LinearClamp, BlendState.Opaque);
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("AntiAliasing",false,true);        
        }

    }
}
