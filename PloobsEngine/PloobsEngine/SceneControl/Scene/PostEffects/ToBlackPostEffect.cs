using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace PloobsEngine.SceneControl
{
    public class ToBlackPostEffect : IPostEffect
    {
        public ToBlackPostEffect() : base(PostEffectType.Deferred) { }

        Effect effect = null;                
        RenderTarget2D tr2d;
        public Texture2D t;

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {             
             effect.Parameters["EXTRA"].SetValue(rHelper[PrincipalConstants.extra1RT]);
             effect.Parameters["color"].SetValue(ImageToProcess);             
             effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
             if (useFloatingBuffer)
                 rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
             else
                 rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.LinearClamp);              
         
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("ToBlack",false,true);
        }

    }
}


