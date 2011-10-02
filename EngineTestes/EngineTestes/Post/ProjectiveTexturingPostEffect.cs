//TODO
///DEcal com post effect !!!
///ideia no projective.fx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EngineTestes.Post
{
    class ProjectiveTexturingPostEffect : IPostEffect
    {
        public ProjectiveTexturingPostEffect()
            : base(PostEffectType.Deferred)
        {
        }
        
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/Projective");
        }
        Effect effect;        
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            //effect.Parameters["timervalue"].SetValue(m_Timer);

            if (useFloatBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect,SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect,SamplerState.LinearClamp);
        }
    }
}
