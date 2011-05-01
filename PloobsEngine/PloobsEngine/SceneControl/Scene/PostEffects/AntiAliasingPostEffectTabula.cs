using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.SceneControl
{
    public class AntiAliasingPostEffectTabula : IPostEffect 
    {
        public AntiAliasingPostEffectTabula()
            : base(PostEffectType.Deferred)
        {
            this.priority = float.MaxValue;
        }        
        
        private float weights = 1;

        public float Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        float depthSensibility = 1000;

        public float DepthSensibility
        {
            get { return depthSensibility; }
            set { depthSensibility = value; }
        }
        float normalSensibility = 700;

        public float NormalSensibility
        {
            get { return normalSensibility; }
            set { normalSensibility = value; }
        }
                

        Effect effect = null;
        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
            effect.Parameters["normalSensibility"].SetValue(normalSensibility);
            effect.Parameters["depthSensibility"].SetValue(depthSensibility);
            effect.Parameters["weight"].SetValue(weights);
            effect.Parameters["pixel_size"].SetValue(GraphicInfo.HalfPixel);            
            effect.Parameters["depthTex"].SetValue(rHelper[PrincipalConstants.DephRT]);
            effect.Parameters["normalTex"].SetValue(rHelper[PrincipalConstants.normalRt]);                        
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    rHelper.RenderTextureComplete(ImageToProcess);
            //    return;
            //}
            rHelper.RenderFullScreenQuadVertexPixel(effect);
        }

        public override void Init(GraphicInfo ginfo, GraphicFactory factory)
        {
            effect = factory.GetEffect("AntiAliasingtabulastalker",false,true);
            effect.CurrentTechnique = effect.Techniques["AntiAliasingTabula"];            
        }        
    }
}
