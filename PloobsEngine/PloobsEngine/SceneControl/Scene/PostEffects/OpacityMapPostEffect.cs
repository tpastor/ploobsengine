#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class OpacityMapPostEffect : IPostEffect
    {
        public OpacityMapPostEffect(String opacityTexture) : base(PostEffectType.All)
        {
            textureName = opacityTexture;
        }

        #region IPostEffect Members

        private string textureName;
        Effect effect = null;
        Texture2D tex;
        GraphicFactory factory;

        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; 
            if(effect != null)
                tex = factory.GetTexture2D(textureName);
            }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            ///Draw a quad using the "effect", passing the CurrentImage as a Parameter
            effect.Parameters["opacityMap"].SetValue(tex);
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("opacity",false,true);
            tex = factory.GetTexture2D(textureName);
            this.factory = factory;
        }

        #endregion
    }
}
#endif
