using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EngineTestes.Post
{
    public class DreamPostEffect : IPostEffect
    {
        public DreamPostEffect()
            : base(PostEffectType.All)
        {
        }
        
        public override void Init(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Effects/dream");
        }
        Effect effect;
        float m_Timer ;
        Vector4 vecSkill1 = new Vector4(2,7,0.1f,0.4f);

        public float Range
        {
            set
            {
                this.vecSkill1.W = value;
            }
            get
            {
                return this.vecSkill1.W;
            }
        }


        public float Sharpness
        {
            set
            {
                this.vecSkill1.Y = value;
            }
            get
            {
                return this.vecSkill1.Y;
            }
        }

        public float Speed
        {
            set
            {
                this.vecSkill1.Z = value;
            }
            get
            {
                return this.vecSkill1.Z;
            }
        }

        public float Blur
        {
            set
            {
                this.vecSkill1.X = value; 
            }
            get
            {
                return this.vecSkill1.X;
            }
        }
        
        public override void Draw(Microsoft.Xna.Framework.Graphics.Texture2D ImageToProcess, RenderHelper rHelper, Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatBuffer)
        {
            m_Timer += (float)gt.ElapsedGameTime.Milliseconds / 500;
            effect.Parameters["vecTime"].SetValue(m_Timer);
            effect.Parameters["vecSkill1"].SetValue(vecSkill1);


            if (useFloatBuffer)
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, SamplerState.PointClamp);
            else
                rHelper.RenderTextureToFullScreenSpriteBatch(ImageToProcess, effect, GraphicInfo.FullScreenRectangle, GraphicInfo.SamplerState);                
        }
    }
}
