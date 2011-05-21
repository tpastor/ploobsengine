using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class NoisePostEffect : IPostEffect
    {
        public NoisePostEffect() : base(PostEffectType.All) { }

        #region IPostEffect Members        
        private Effect noise;
        private float m_Timer = 0;        

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {            
            this.noise = factory.GetEffect("noise",false,true);            
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            m_Timer += (float)gt.ElapsedGameTime.Milliseconds / 500;
            noise.Parameters["fTimer"].SetValue(m_Timer);
            noise.Parameters["iSeed"].SetValue(1337);
            noise.Parameters["fNoiseAmount"].SetValue(0.01f);
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, noise, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, noise, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
            
        }

        #endregion
    }
}
