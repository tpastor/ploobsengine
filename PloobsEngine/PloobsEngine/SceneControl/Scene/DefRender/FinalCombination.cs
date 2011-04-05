using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    public class FinalCombination : IDeferredFinalCombination
    {                      
        private Effect finalCombineEffect;                 
        private RenderTarget2D target;
        GraphicInfo ginfo;
        private Color ambientColor = Color.Black;
        private bool saveToTexture;

        public Color AmbientColor
        {
            get { return ambientColor; }
            set { ambientColor = value; }
        }

        public FinalCombination(Color AmbientColor)
        {
            this.ambientColor = AmbientColor;
        }       
        

        #region IDeferredFinalCombination Members


        public Texture2D this[GBufferTypes type]
        {
            get
            {
                if (type == GBufferTypes.FINALIMAGE)
                {
                    return this.target;
                }
                else
                {
                    ActiveLogger.LogMessage("Wrong Image Type requested", LogLevel.FatalError);
                    throw new Exception("wrong type");
                }
            }
        }

        #endregion

        #region IDeferredFinalCombination Members

        public void SetFinalCombination(RenderHelper render)
        {
            if (saveToTexture)
            {
                render.PushRenderTarget(target);                
            }            
        }

        public void DrawScene(GameTime gameTime, IWorld world, IDeferredGBuffer gbuffer, IDeferredLightMap lightmap, RenderHelper render)
        {
            render.PushDepthState(DepthStencilState.None);
            
            finalCombineEffect.Parameters["EXTRA1"].SetValue(gbuffer[GBufferTypes.Extra1]);
            finalCombineEffect.Parameters["ambientColor"].SetValue(ambientColor.ToVector3());
            finalCombineEffect.Parameters["colorMap"].SetValue(gbuffer[GBufferTypes.COLOR]);
            finalCombineEffect.Parameters["lightMap"].SetValue(lightmap[DeferredLightMapType.LIGHTMAP]);
            finalCombineEffect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);

            render.RenderFullScreenQuadVertexPixel(finalCombineEffect);

            if (saveToTexture)
            {
                render.PopRenderTarget();                
            }
            render.PopDepthStencilState();
            
        }


        #endregion

        #region IDeferredFinalCombination Members


        public void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, bool useFloatBuffer, bool saveToTexture )
        {
            this.ginfo = ginfo;
            this.saveToTexture = saveToTexture;
            finalCombineEffect = manager.GetAsset<Effect>("CombineFinal",true);
            if (saveToTexture)
            {
                if (useFloatBuffer)
                    target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, false, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
                else
                    target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, false, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            }                        
        }

        #endregion
    }
}
