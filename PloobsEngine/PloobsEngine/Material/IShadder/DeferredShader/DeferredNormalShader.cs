#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
#if !WINDOWS_PHONE && !REACH
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
    /// Most Basic Deferred Shader (uses only diffuse texture and specular power/intensity properties)
    /// </summary>
    public class DeferredNormalShader : IShader
    {
        private Effect _shader;        
        EffectParameter ViewProjectionParameter;          
        EffectParameter TextureParameter;  
        EffectParameter SpecularPowerParameter;  
        EffectParameter SpecularIntensityParameter;
        EffectParameter IdParameter;  
        EffectParameter WorldParameter;
        EffectParameter PAmbientCubeMapScale;
        EffectParameter PAmbientCubeTexture;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredNormalShader"/> class.
        /// </summary>
        public DeferredNormalShader(float specularIntensity = 0, float specularPower = 0, bool useAmbientCubeMap = false,  float AmbientCubeMapScale = 0.1f)
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

            if (AmbientCubeMapScale< 0)
            {
                ActiveLogger.LogMessage("ambientCubeMapScale cannot be negative, setting to 0.1f", LogLevel.RecoverableError);
                AmbientCubeMapScale = 0.1f;
            }

            this.specularIntensity = specularIntensity;
            this.specularPower = specularPower;
            this.AmbientCubeMapScale = AmbientCubeMapScale;
            this.UseAmbientCubeMap = useAmbientCubeMap;
        }

        public float AmbientCubeMapScale
        {
            get;
            set;
        }

        bool useAmbientCubeMap = false;

        public bool UseAmbientCubeMap
        {
            get
            {
                return useAmbientCubeMap;
            }
            set
            {
                this.useAmbientCubeMap = value;
                if (value == true)
                {
                    shaderId |= ShaderUtils.CreateSpecificBitField(false, false, false, true);
                }
                else
                {
                    shaderId &= ~ShaderUtils.CreateSpecificBitField(false, false, false, true);
                }
                if (value == true && this._shader != null)
                {
                    this._shader.CurrentTechnique = this._shader.Techniques["Technique2"];
                }
                else if (value == false && this._shader != null)
                {
                    this._shader.CurrentTechnique = this._shader.Techniques["Technique1"];
                }
            }
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
        /// <param name="lights">The lights.</param>
        protected override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera camera, IList<ILight> lights)
        {   
                IModelo modelo = obj.Modelo;           
                IdParameter.SetValue((float)shaderId);
                SpecularIntensityParameter.SetValue(specularIntensity);
                SpecularPowerParameter.SetValue(specularPower);                
                ViewProjectionParameter.SetValue(camera.ViewProjection);
            
                for (int i = 0; i < modelo.MeshNumber; i++)
                {                    
                    BatchInformation[] bi = modelo.GetBatchInformation(i);                                        
                    for (int j = 0; j < bi.Count(); j++)
                    {

                        if (useAmbientCubeMap)
                        {
                            PAmbientCubeTexture.SetValue(modelo.GetCubeTexture(TextureType.AMBIENT_CUBE_MAP, i, j));
                            PAmbientCubeMapScale.SetValue(AmbientCubeMapScale);
                        }
                        
                        //render.Textures[0] = modelo.getTexture(TextureType.DIFFUSE, i, j);
                        TextureParameter.SetValue(modelo.getTexture(TextureType.DIFFUSE,i,j));
                        WorldParameter.SetValue(bi[j].ModelLocalTransformation * obj.WorldMatrix);
                        render.RenderBatch(bi[j], _shader);                        
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
            base.Initialize(ginfo, factory, obj);      
            this._shader = factory.GetEffect("RenderGBuffer",true,false);            

            ViewProjectionParameter = this._shader.Parameters["ViewProjection"];              
            TextureParameter = this._shader.Parameters["diffuse"];
            IdParameter = this._shader.Parameters["id"];
            SpecularIntensityParameter = this._shader.Parameters["specularIntensity"];
            SpecularPowerParameter = this._shader.Parameters["specularPower"];  
            WorldParameter = this._shader.Parameters["World"];

            PAmbientCubeTexture = this._shader.Parameters["map_diffuse"];
            PAmbientCubeMapScale = this._shader.Parameters["ambientScale"];

            if (useAmbientCubeMap == true)
            {
                this._shader.CurrentTechnique = this._shader.Techniques["Technique2"];
            }
            else
            {
                this._shader.CurrentTechnique = this._shader.Techniques["Technique1"];
            }
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
#endif