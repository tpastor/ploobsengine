using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class BlackWhitePostEffect : IPostEffect
    {
        public BlackWhitePostEffect()
            : base(PostEffectType.All)
        {
        }

        #region IPostEffect Members
               
        Effect effect = null;

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {        
            if(useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.LinearClamp);            
        }

        public override void  Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {         
            effect = factory.GetEffect("BlackWhite",false,true);            
        }
       
        #endregion
    }
}
