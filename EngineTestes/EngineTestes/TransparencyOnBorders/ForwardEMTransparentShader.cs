#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Light;

namespace PloobsEngine.Material
{

    public class ForwardEMTransparentShader : IShader
    {        
        private Effect _shader;          
        private TextureCube texCube;        
        private String texName = null;
 
        /// <summary>
        /// from 0 to 1 
        /// default 0.5f
        /// 1 = perfect mirror
        /// </summary>
        private float reflectionIndex = 0.5f;

        public float ReflectionIndex
        {
            get { return reflectionIndex; }
            set { reflectionIndex = value; 
            if(isInitialized)
                this._shader.Parameters["reflectionIndex"].SetValue(reflectionIndex);
            }
        }  
        

        public TextureCube TextureCube
        {
            get { return texCube; }
            set { texCube = value; }
        }        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredEMReflectiveShader"/> class.
        /// Reflexive Surface implicity
        /// </summary>
        /// <param name="texName">Name of the tex.</param>
        /// <param name="reflectionIndex">Index of the reflection.</param>
        public ForwardEMTransparentShader(String texName)
        {            
            this.texName = texName;         
        }

        RasterizerState cullMode = RasterizerState.CullCounterClockwise;
       
        public override void  PosDrawPhase(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
 	    {
            render.PushRasterizerState(cullMode);
            render.PushBlendState(BlendState.NonPremultiplied);
                this._shader.Parameters["View"].SetValue(cam.View);
                this._shader.Parameters["Projection"].SetValue(cam.Projection);
                this._shader.Parameters["Cubemap"].SetValue(texCube);
                this._shader.Parameters["id"].SetValue(shaderId);
                
                Matrix wld = obj.WorldMatrix;

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        
                        this._shader.Parameters["Texture"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE, i, j));
                        this._shader.Parameters["World"].SetValue(Matrix.Multiply(wld, bi[j].ModelLocalTransformation));
                        render.RenderBatch(bi[j], _shader);
                    }         
                }
                render.PopBlendState();
                render.PopRasterizerState();         
        }

        public override MaterialType MaterialType
        {
            get { return MaterialType.FORWARD; }
        }

        public override void  Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
 	    {
            this._shader = factory.GetEffect("effects/EMTransparencySHADER");            
            texCube = factory.GetTextureCube(texName,false);            
            base.Initialize(ginfo, factory, obj);
        }

        public override void BasicDraw(GameTime gt, IObject obj, Matrix view, Matrix projection, IList<ILight> lights, RenderHelper render, Plane? clippingPlane, bool useAlphaBlending = false)
        {
            ///no reflection refraction
        }

        public override void DepthExtractor(GameTime gt, IObject obj, Matrix View, Matrix projection, RenderHelper render)
        {
            ///no shadow
        }
      

    }
}
#endif