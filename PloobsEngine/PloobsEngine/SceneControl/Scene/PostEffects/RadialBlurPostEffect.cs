#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class RadialBlurPostEffect : IPostEffect
    {
        public RadialBlurPostEffect() : base(PostEffectType.All) {
            Center = new Vector2(0.5f, 0.5f);
            BlurStart = 1.0f;
            BlurWidth = -0.1f;        
        }
        Effect effect;
        public Vector2 Center
        {
            set;
            get;
        }
        public float BlurWidth
        {
            set;
            get;
        }
        public float BlurStart
        {
            set;
            get;
        }


        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {            
            effect = factory.GetEffect("RadialBlur", false, true);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            effect.Parameters["BlurStart"].SetValue(BlurStart);
            effect.Parameters["BlurWidth"].SetValue(BlurWidth);
            effect.Parameters["Center"].SetValue(Center);
            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);                
        }
    }
}
#endif