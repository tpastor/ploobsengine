#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
   public enum GammaType
    {
        Normal, Simple,InvertNormal, InvertSimple,InvertVerySimple,VerySimple
    }

    /// <summary>
    /// OLHAR http://mynameismjp.wordpress.com/2009/12/31/correcting-xnas-gamma-correction/
    /// SOLUCAO MAIS ELEGANTE PARA PCS --  mudar o render state pelo effect apos o ultimo efeito aplicado
    /// </summary>
    public class GammaCorrectionPostEffect : IPostEffect
    {
        public GammaCorrectionPostEffect(GammaType type) : base(PostEffectType.All)
        {
            this._gType  = type;
        }

        #region IPostEffect Members

        Effect effect = null;

        private GammaType _gType;

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            ///Draw a quad using the "effect", passing the CurrentImage as a Parameter            
            if (useFloatingBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);            
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            ///Load the asset
            effect = factory.GetEffect("gamma",false,true);            
            switch (_gType)
            {
                case GammaType.Normal:
                    effect.CurrentTechnique = effect.Techniques[0];
                    break;
                case GammaType.Simple:
                    effect.CurrentTechnique = effect.Techniques[1];
                    break;
                case GammaType.InvertNormal:
                    effect.CurrentTechnique = effect.Techniques[2];
                    break;
                case GammaType.InvertSimple:
                    effect.CurrentTechnique = effect.Techniques[3];
                    break;
                case GammaType.VerySimple:
                    effect.CurrentTechnique = effect.Techniques[4];
                    break;
                case GammaType.InvertVerySimple:
                    effect.CurrentTechnique = effect.Techniques[5];
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
#endif