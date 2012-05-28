using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;

namespace EngineTestes.Post
{
    public class DownSamplePostEffect
        : IPostEffect
    {
        public DownSamplePostEffect()
            : base(PostEffectType.AllHidef)
        {
        }

        DownSampler DownSampler;
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            DownSampler = new DownSampler(factory, ginfo.BackBufferWidth / 4, ginfo.BackBufferHeight / 4);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            Texture2D Texture2D = DownSampler.DownShaderBilinearTexture(rHelper, ImageToProcess);
            rHelper.RenderTextureComplete(Texture2D, ImageToProcess.Bounds);
        }
    }
}
