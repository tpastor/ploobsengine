#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    public class HSVEffect : IPostEffect
    {
        public HSVEffect(Vector4 toAdd, Vector4 toMultiply) : base(PostEffectType.All)
        {
            this.toAdd = toAdd;
            this.toMultiply = toMultiply;
        }

        #region IPostEffect Members        
        private Effect hsv;
        Vector4 toAdd;

        public Vector4 ToAdd
        {
            get { return toAdd; }
            set { toAdd = value; }
        }
        Vector4 toMultiply;

        public Vector4 ToMultiply
        {
            get { return toMultiply; }
            set { toMultiply = value; }
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {            
            this.hsv = factory.GetEffect("HSV",false,true);            
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
           hsv.Parameters["cena"].SetValue(ImageToProcess);
           hsv.Parameters["toMultiply"].SetValue(toMultiply);
           hsv.Parameters["toAdd"].SetValue(toAdd);
           if (useFloatingBuffer)
               rHelper.RenderFullScreenQuadVertexPixel(hsv, SamplerState.PointClamp);
           else
               rHelper.RenderFullScreenQuadVertexPixel(hsv, GraphicInfo.SamplerState);        
        }

        #endregion
    }
}
#endif