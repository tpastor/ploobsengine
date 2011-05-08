using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Light;
using PloobsEngine.Modelo;
using PloobsEngine.Cameras;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Forward Material
    /// </summary>
    public class ForwardTransparenteShader : IShader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardTransparenteShader"/> class.
        /// Use Texture Alpha
        /// </summary>
        public ForwardTransparenteShader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardTransparenteShader"/> class.
        /// Use custom alpha
        /// </summary>
        /// <param name="transparencyLevel">The transparency level.</param>
        public ForwardTransparenteShader(float transparencyLevel)
        {
            this.TransparencyLevel = TransparencyLevel;
        }

        bool useTextureAlpha = true;
        float transparencyLevel = 0;
        Effect _shader;
        RasterizerState cullMode = RasterizerState.CullNone;

        /// <summary>
        /// Default None
        /// </summary>
        public RasterizerState CullModeToUse
        {
            get { return cullMode; }
            set { cullMode = value; }
        }

        /// <summary>
        /// Between 0 and 1 
        /// If not setted, the texture alpha will be used instead
        /// </summary>
        public float TransparencyLevel
        {
            get { return transparencyLevel; }
            set {

                System.Diagnostics.Debug.Assert(value >= 0 && value <= 1);
                transparencyLevel = value; 
                useTextureAlpha = false; 
            }
        }
             

        public override MaterialType MaterialType
        {
            get { return MaterialType.FORWARD; }
        }

        public override void Initialize(GraphicInfo ginfo, GraphicFactory factory, IObject obj)
        {
            _shader = factory.GetEffect("TransparentEffect",true,true);
        }

        public override void  PosDrawPhase(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
 	    {
                _shader.Parameters["colorMap"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE));                                
              render.PushRasterizerState(cullMode);
              render.PushBlendState(BlendState.NonPremultiplied);                                                                        

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    
                    for (int j = 0; j < bi.Count(); j++)
                    {                      
                        Matrix w1 = Matrix.Multiply(obj.WorldMatrix, bi[j].ModelLocalTransformation);
                        _shader.Parameters["wvp"].SetValue(w1 * cam.ViewProjection);                

                        _shader.Parameters["alpha"].SetValue(transparencyLevel);
                        _shader.Parameters["useTextureAlpha"].SetValue(useTextureAlpha);                       

                        render.RenderBatch(bi[j], _shader);
                    }
                }
                render.PopBlendState();
                render.PopRasterizerState();         
        }
    }
}
