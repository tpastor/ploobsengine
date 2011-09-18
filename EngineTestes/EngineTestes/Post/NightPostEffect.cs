﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;

namespace EngineTestes.Post
{
    class NightPostEffect : IPostEffect
    {
        public NightPostEffect()
            : base(PostEffectType.All)
        {
        }

        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/night");
            tex = factory.GetTexture2D("Textures/noise_tex6");
        }
        Effect effect;
        Texture2D tex;
        private float m_Timer = 0;    

        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {

            m_Timer += (float)gt.ElapsedGameTime.Milliseconds / 500;
            effect.Parameters["elapsedTime"].SetValue(m_Timer);
            effect.Parameters["tex"].SetValue(tex);
            

            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);
        }
    }
}
