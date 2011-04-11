using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Modelo.Animation;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Light;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Most Basic Deferred Shader
    /// </summary>
    public class NormalDeferred : IShader
    {
        private Effect _shader;        
        EffectParameter ViewProjectionParameter;          
        EffectParameter TextureParameter;  
        EffectParameter SpecularPowerParameter;  
        EffectParameter SpecularIntensityParameter;
        EffectParameter IdParameter;  
        EffectParameter WorldParameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalDeferred"/> class.
        /// </summary>
        public NormalDeferred(float specularIntensity = 0, float specularPower = 0)
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
            this.specularIntensity = specularIntensity;
            this.specularPower = specularPower;
        }

        private float specularIntensity = 0f;
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
        private float specularPower = 0f;

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
        /// Draw.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        /// <param name="camera">The camera.</param>
        public override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera camera, IList<ILight> lights)
        {            
                IModelo modelo = obj.Modelo;           
                IdParameter.SetValue(shaderId);
                SpecularIntensityParameter.SetValue(specularIntensity);
                SpecularPowerParameter.SetValue(specularPower);
                TextureParameter.SetValue(modelo.getTexture(TextureType.DIFFUSE));
                ViewProjectionParameter.SetValue(camera.ViewProjection);

                for (int i = 0; i < modelo.MeshNumber; i++)
                {                    
                    BatchInformation[] bi = modelo.GetBatchInformation(i);                                        
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        WorldParameter.SetValue(bi[j].ModelLocalTransformation * obj.WorldMatrix);
                        render.RenderBatch(ref bi[j], _shader);                        
                    }
                }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="ginfo"></param>
        /// <param name="factory"></param>
        /// <param name="obj"></param>
        public override void Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)
        {                     
            this._shader = factory.GetEffect("RenderGBuffer",false,true);            
            ViewProjectionParameter = this._shader.Parameters["ViewProjection"];              
            TextureParameter = this._shader.Parameters["Texture"];
            IdParameter = this._shader.Parameters["id"];
            SpecularIntensityParameter = this._shader.Parameters["specularIntensity"];
            SpecularPowerParameter = this._shader.Parameters["specularPower"];  
            WorldParameter = this._shader.Parameters["World"];
            base.Initialize(ginfo, factory, obj);
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
    }
}
