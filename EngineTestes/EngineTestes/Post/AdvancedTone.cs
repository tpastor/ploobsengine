using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EngineTestes.Post
{
    class AdvancedTone : IPostEffect
    {
        public AdvancedTone() : base(PostEffectType.All)
        {
        }

        float Desat = 0.5f;
        float Toned = 0.5f;
        Vector3 LightColor = new Vector3 (1, 1.0f, 1.0f) ;
        Vector3 DarkColor = new Vector3(0.0f, 0.0f, 0);  

        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/AdvancedColorTone");
        }
        Effect effect;
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            effect.Parameters["Desat"].SetValue(Desat);
            effect.Parameters["Toned"].SetValue(Toned);
            effect.Parameters["DarkColor"].SetValue(DarkColor);
            effect.Parameters["LightColor"].SetValue(LightColor);            
            
            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);                
        }
    }
}
