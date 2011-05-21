using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class WigglePostEffect : IPostEffect
    {
        public WigglePostEffect() : base(PostEffectType.All) { }
        private Effect wiggle;
        private float m_Timer = 0;        

        #region IPostEffect Members

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {

            m_Timer += (float)gt.ElapsedGameTime.Milliseconds / 500;

            wiggle.Parameters["fTimer"].SetValue(m_Timer);
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, wiggle, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, wiggle, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);
            
            
        }

        #endregion

        #region IPostEffect Members        
        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {            
            this.wiggle = factory.GetEffect("wiggle",false,true);         
        }

        #endregion
    }
}
