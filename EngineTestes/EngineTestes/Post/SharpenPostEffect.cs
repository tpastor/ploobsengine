using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EngineTestes.Post
{
    class SharpenPostEffect : IPostEffect
    {
        public SharpenPostEffect()
            : base(PostEffectType.All)
        {
        }
        
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/Sharpen");
        }
        Effect effect;
        float sharpAmount = 15;

        public float SharpAmount
        {
            get { return sharpAmount; }
            set { sharpAmount = value; }
        }

        float amount = 0.0006f;

        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            effect.Parameters["sharpAmount"].SetValue(sharpAmount);
            effect.Parameters["amount"].SetValue(amount);           

            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);                
        }
    }
}
