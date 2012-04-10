using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine;

namespace EngineTestes.Post
{
    class BackgroundTexture : IPostEffect
    {
        public BackgroundTexture(String BackTextureName)
            : base(PostEffectType.Deferred)
        {
            this.BackTextureName = BackTextureName;
        }
        String BackTextureName;
        Texture2D tex;

        public Texture2D BackGroundTexture
        {
            get { return tex; }
            set { tex = value; }
        }

        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/backgroundTexture");
            BackGroundTexture = factory.GetTexture2D(BackTextureName);
            
        }

        Effect effect;
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            effect.Parameters["extrart"].SetValue(rHelper[PrincipalConstants.extra1RT]);
            effect.Parameters["backtex"].SetValue(BackGroundTexture); 

            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);                         
        }
    }
}
