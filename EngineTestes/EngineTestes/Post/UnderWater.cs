using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EngineTestes.Post
{
    class UnderWater : IPostEffect
    {
        public UnderWater()
            : base(PostEffectType.All)
        {
        }
        
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/UnderWater");
        }
        Effect effect;
        float m_Timer ;        
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            m_Timer += (float)gt.ElapsedGameTime.Milliseconds / 500;
            effect.Parameters["timervalue"].SetValue(m_Timer);

            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);                
        }
    }
}
