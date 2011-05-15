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
    public class AntiAliasingPostEffectStalker : IPostEffect 
    {
        public AntiAliasingPostEffectStalker()
            : base(PostEffectType.Deferred)
        {
            
        }        
        
        private Vector2 barrier = new Vector2(0.8f, 0.00001f);

        public Vector2 Barrier
        {
            get { return barrier; }
            set { barrier = value; }
        }
        private Vector2 weights = new Vector2(1);

        public Vector2 Weights
        {
            get { return weights; }
            set { weights = value; }
        }
        private Vector2 kernel = new Vector2(1);

        public Vector2 Kernel
        {
            get { return kernel; }
            set { kernel = value; }
        }
                
        Effect effect = null;
        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {


            effect.Parameters["e_barrier"].SetValue(barrier);
            effect.Parameters["e_kernel"].SetValue(kernel);
            effect.Parameters["e_weights"].SetValue(weights);
            effect.Parameters["pixel_size"].SetValue(GraphicInfo.HalfPixel);            
            effect.Parameters["depthTex"].SetValue(rHelper[PrincipalConstants.DephRT]);
            effect.Parameters["normalTex"].SetValue(rHelper[PrincipalConstants.normalRt]);
            effect.Parameters["image"].SetValue(ImageToProcess);                                    
            rHelper.Clear(Color.Transparent);
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
            //{
            //    rHelper.RenderTextureComplete(ImageToProcess);
            //    return;
            //}
            if (useFloatingBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.AnisotropicClamp);
        }

        public override void Init(GraphicInfo ginfo, GraphicFactory factory)
        {
            effect = factory.GetEffect("AntiAliasingtabulastalker",false,true);
            effect.CurrentTechnique = effect.Techniques["AntiAliasingStalker"];            
        }        
    }
}
