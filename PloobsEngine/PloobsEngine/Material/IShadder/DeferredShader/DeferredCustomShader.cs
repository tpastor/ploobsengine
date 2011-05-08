using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Shader that uses branching to use Glow,Bump, Specular and Paralax
    /// Dont need to use all of them at once
    /// </summary>
    public class DeferredCustomShader : IShader
    {
        private string effect;
        private Effect _shader;
        private bool useGlow;
        private bool useParalax;
        private bool useBump;
        private bool useSpecular;

        /// <summary>
        /// Gets or sets a value indicating whether [use paralax].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use paralax]; otherwise, <c>false</c>.
        /// </value>
        public bool UseParalax
        {
            get { return useParalax; }
            set { useParalax = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use glow].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use glow]; otherwise, <c>false</c>.
        /// </value>
        public bool UseGlow
        {
            get { return useGlow; }
            set { useGlow = value; }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [use bump].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use bump]; otherwise, <c>false</c>.
        /// </value>
        public bool UseBump
        {
            get { return useBump; }
            set { useBump = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use specular].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use specular]; otherwise, <c>false</c>.
        /// </value>
        public bool UseSpecular
        {
            get { return useSpecular; }
            set { useSpecular = value; }
        }

        private float specularIntensity = 0;
        /// <summary>
        /// Gets or sets the specular intensity. Default 0
        /// </summary>
        /// <value>
        /// The specular intensity.
        /// </value>
        public float SpecularIntensity
        {
            get { return specularIntensity; }
            set { specularIntensity = value; }
        }
        private float specularPower = 0;

        /// <summary>
        /// Gets or sets the specular power. Default 0
        /// </summary>
        /// <value>
        /// The specular power.
        /// </value>
        public float SpecularPower
        {
            get { return specularPower; }
            set { specularPower = value; }
        }

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public override MaterialType MaterialType
        {
            get { return MaterialType.DEFERRED; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredCustomShader"/> class.
        /// </summary>
        /// <param name="useGlow">if set to <c>true</c> [use glow].</param>
        /// <param name="useBump">if set to <c>true</c> [use bump].</param>
        /// <param name="useSpecular">if set to <c>true</c> [use specular].</param>
        /// <param name="useParalax">if set to <c>true</c> [use paralax].</param>
        /// <param name="specularIntensity">The specular intensity.</param>
        /// <param name="specularPower">The specular power.</param>
        public DeferredCustomShader(bool useGlow,bool useBump,bool useSpecular,bool useParalax, float specularIntensity = 0, float specularPower = 0)
        {
            if (specularPower < 0)
            {
                ActiveLogger.LogMessage("specularPower cannot be negative, setting to 0", LogLevel.RecoverableError);
                specularPower = 0;
            }
            if (specularIntensity < 0)
            {
                ActiveLogger.LogMessage("specularIntensity cannot be negative, setting to 0", LogLevel.RecoverableError);
                specularIntensity = 0;
            }
            this.effect = "AllBuffer";
            this.specularIntensity = specularIntensity;
            this.specularPower = specularPower;
            this.useGlow = useGlow;
            this.useSpecular = useSpecular;
            this.useBump = useBump;
            this.useParalax = useParalax;
            if (useParalax == true && useBump == false)
            {
                ActiveLogger.LogMessage("Are you sure you will use only Paralax without BUMP, the paralax expects bump", LogLevel.Warning);                
            }
        }

        private Vector2 scaleBias = new Vector2(0.04f, -0.03f);

        /// <summary>
        /// Default  Vector2(0.04f, -0.03f);
        /// used for paralax
        /// </summary>
        /// <value>
        /// The scale bias.
        /// </value>
        public Vector2 ScaleBias
        {
            get { return scaleBias; }
            set { scaleBias = value; }
        }


        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gt"></param>
        /// <param name="obj"></param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights"></param>
        public override void  Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<Light.ILight> lights)
        {                 
                this._shader.Parameters["id"].SetValue(shaderId);
                this._shader.Parameters["useParalax"].SetValue(useParalax);
                this._shader.Parameters["useGlow"].SetValue(useGlow);
                this._shader.Parameters["useBump"].SetValue(useBump);
                this._shader.Parameters["useSpecular"].SetValue(useSpecular);            
                this._shader.Parameters["Texture"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE));
                if (useBump)
                {
                    if (useParalax)
                    {
                        this._shader.Parameters["HeightMap"].SetValue(obj.Modelo.getTexture(TextureType.PARALAX));
                        this._shader.Parameters["scaleBias"].SetValue(scaleBias);                        
                    }

                    this._shader.Parameters["NormalMap"].SetValue(obj.Modelo.getTexture(TextureType.BUMP));

                }
                if (useSpecular)
                {
                    this._shader.Parameters["SpecularMap"].SetValue(obj.Modelo.getTexture(TextureType.SPECULAR));
                }
                else
                {
                    this._shader.Parameters["specularIntensity"].SetValue(specularIntensity);
                    this._shader.Parameters["specularPower"].SetValue(specularPower);
                }

                if (useGlow)
                    this._shader.Parameters["glow"].SetValue(obj.Modelo.getTexture(TextureType.GLOW));
                                        
               this._shader.Parameters["View"].SetValue(cam.View);
               this._shader.Parameters["Projection"].SetValue(cam.Projection);

                if(useParalax)
                    this._shader.Parameters["CameraPos"].SetValue(cam.Position);                               
                
                Matrix wld = obj.WorldMatrix;

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        Matrix w1 = Matrix.Multiply(wld, bi[j].ModelLocalTransformation);                    
                        this._shader.Parameters["World"].SetValue(w1);
                        this._shader.Parameters["WorldInverseTranspose"].SetValue(Matrix.Transpose(Matrix.Invert(w1))); 

                        render.RenderBatch(bi[j],_shader);
                    }
                }               
            }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="ginfo"></param>
        /// <param name="factory"></param>
        /// <param name="obj"></param>
        public override void Initialize(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {
            this._shader = factory.GetEffect(effect,false,true);            
            base.Initialize(ginfo, factory, obj);
        }

        
    }
}
