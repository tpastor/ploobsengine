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
    public enum ReflectionType
    {
        PerfectMirror, ReflexiveSurface
    }

    public class EMReflective : IShader
    {        
        private Effect _shader;  
        private float specularIntensity = 0f;
        private TextureCube texCube;        
        private String texName = null;
        private ReflectionType rType = ReflectionType.ReflexiveSurface;       
 
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
        
        internal EMReflective()
        {        
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EMReflective"/> class.
        /// </summary>
        /// <param name="texName">Name of the tex.</param>
        /// <param name="type">The type.</param>
        public EMReflective(String texName , ReflectionType type  )
        {            
            this.rType = type;
            this.texName = texName;            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EMReflective"/> class.
        /// Reflexive Surface implicity
        /// </summary>
        /// <param name="texName">Name of the tex.</param>
        /// <param name="reflectionIndex">Index of the reflection.</param>
        public EMReflective(String texName,float reflectionIndex)
        {
            this.reflectionIndex = reflectionIndex;
            this.texName = texName;         
        }

        private float specularPower = 0f;

        public float SpecularIntensity
        {
            get { return specularIntensity; }
            set { specularIntensity = value; 
            if(isInitialized)
                this._shader.Parameters["specularIntensity"].SetValue(specularIntensity);
            }
        }        

        public float SpecularPower
        {
            get { return specularPower; }
            set { specularPower = value; 
            if(isInitialized)
                this._shader.Parameters["specularPower"].SetValue(specularPower);
            }
        }

        public override void PreUpdate(IObject ent, IList<ILight> lights)
        {
            if (rType == ReflectionType.PerfectMirror)
                this._shader.CurrentTechnique = this._shader.Techniques["PerfectMirror"];
            else if (rType == ReflectionType.ReflexiveSurface)
                this._shader.CurrentTechnique = this._shader.Techniques["ReflexiveSurface"];           

            this._shader.Parameters["reflectionIndex"].SetValue(reflectionIndex);
            if (rType == ReflectionType.ReflexiveSurface)
                this._shader.Parameters["Texture"].SetValue(ent.Modelo.getTexture(TextureType.DIFFUSE));

            this._shader.Parameters["specularIntensity"].SetValue(specularIntensity);
            this._shader.Parameters["specularPower"].SetValue(specularPower);

        }
       
        public override void  Draw(GameTime gt, IObject obj, RenderHelper render, ICamera camera, IList<ILight> lights)
        {                        
                this._shader.Parameters["View"].SetValue(camera.View);
                this._shader.Parameters["Projection"].SetValue(camera.Projection);
                this._shader.Parameters["Cubemap"].SetValue(texCube);
                this._shader.Parameters["id"].SetValue(shaderId);
                
                Matrix wld = obj.WorldMatrix;

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    for (int j = 0; j < bi.Count(); j++)
                    {                        
                        this._shader.Parameters["World"].SetValue(Matrix.Multiply(wld, bi[j].ModelLocalTransformation));
                        render.RenderBatch(ref bi[j], _shader);
                    }         
            }
        }

        public override MaterialType MaterialType
        {
            get { return MaterialType.DEFERRED; }
        }

        public override void  Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
 	    {
            this._shader = factory.GetEffect("EMSHADER",true,true);            
            texCube = factory.GetTextureCube(texName,false);
            obj.Modelo.OnTextureChange += new OnTextureChange(Modelo_OnTextureChange);
            base.Initialize(ginfo, factory, obj);
        }

        void Modelo_OnTextureChange(TextureType type, IModelo model)
        {
            if (rType == ReflectionType.ReflexiveSurface)
                this._shader.Parameters["Texture"].SetValue(model.getTexture(TextureType.DIFFUSE));
        }
      

    }
}
