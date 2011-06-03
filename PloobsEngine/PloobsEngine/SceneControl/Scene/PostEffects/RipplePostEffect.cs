#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class RipplePostEffect : IPostEffect
    {
        public RipplePostEffect() : base(PostEffectType.All) { }
        Effect effect = null;
        private Vector2 center = new Vector2(0.5f);

        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }
        private float rippleSize = 0.25f;

        public float RippleSize
        {
            get { return rippleSize; }
            set { rippleSize = value; }
        }
        private float intensity = 0.25f;

        public float Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            effect.Parameters["Intensity"].SetValue(intensity);
            effect.Parameters["RippleSize"].SetValue(rippleSize);
            effect.Parameters["Point"].SetValue(center);
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }
        
        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Ripple",false,true);            
        }

    }
}
#endif