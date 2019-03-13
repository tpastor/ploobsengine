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
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Utils;

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
        private bool useAmbientCubeMap;
        private bool useParalax;
        private bool useBump;
        private bool useSpecular;

        public float AmbientCubeMapScale
        {
            get;
            set;
        }

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
        /// /// <param name="useAmbientCubeMap">if set to <c>true</c> [use AmbientCubeMap].</param>
        /// <param name="specularIntensity">The specular intensity.</param>
        /// <param name="specularPower">The specular power.</param>
        /// <param name="ambientCubeMapScale ">The ambientCubeMap Scale.</param>
        public DeferredCustomShader(bool useGlow, bool useBump, bool useSpecular, bool useParalax, bool useAmbientCubeMap = false, float specularIntensity = 0, float specularPower = 0, float ambientCubeMapScale = 0.1f)
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

            if (ambientCubeMapScale< 0)
            {
                ActiveLogger.LogMessage("ambientCubeMapScale cannot be negative, setting to 0.1f", LogLevel.RecoverableError);
                ambientCubeMapScale = 0.1f;
            }

            this.AmbientCubeMapScale = ambientCubeMapScale;
            this.effect = "AllBuffer";
            this.specularIntensity = specularIntensity;
            this.specularPower = specularPower;
            this.useGlow = useGlow;
            this.useSpecular = useSpecular;
            this.useBump = useBump;
            this.useParalax = useParalax;
            specularPowerMapScale = 1;
            specularIntensityMapScale = 1;            

            if (useAmbientCubeMap == true && useGlow == true)
            {
                ActiveLogger.LogMessage("Cant enable GLow and AmbientCubeMap together, AmbientCubeMap being disabled", LogLevel.RecoverableError);
                useAmbientCubeMap = false;
            }

            this.UseAmbientCubeMap = useAmbientCubeMap;

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

        private float specularPowerMapScale;
        public float SpecularPowerMapScale
        {
            get
            {
                return specularPowerMapScale;
            }
            set
            {
                this.specularPowerMapScale = value;
            }
        }

        private float specularIntensityMapScale;
        public float SpecularIntensityMapScale
        {
            get
            {
                return specularIntensityMapScale;
            }
            set
            {
                this.specularIntensityMapScale = value;
            }
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gt"></param>
        /// <param name="obj"></param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights"></param>
        protected override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<Light.ILight> lights)
        {                 
               Pid.SetValue((float)shaderId);
               PuseParalax.SetValue(useParalax);
               PuseGlow.SetValue(useGlow);
               PuseBump.SetValue(useBump);
               PuseSpecular.SetValue(useSpecular);                                                   
               PView.SetValue(cam.View);
               PProjection.SetValue(cam.Projection);

                if(useParalax)
                    this._shader.Parameters["CameraPos"].SetValue(cam.Position);                               
                
                Matrix wld = obj.WorldMatrix;

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        Matrix w1 = Matrix.Multiply(bi[j].ModelLocalTransformation, wld);
                        PWorld.SetValue(w1);

                        //render.device.Textures[0] = obj.Modelo.getTexture(TextureType.DIFFUSE, i, j);
                        PTexture.SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE, i, j));
                        if (useBump)
                        {
                            if (useParalax)
                            {
                                this._shader.Parameters["HeightMap"].SetValue(obj.Modelo.getTexture(TextureType.PARALAX,i,j));
                                //render.device.Textures[5] = obj.Modelo.getTexture(TextureType.PARALAX, i, j);
                                this._shader.Parameters["scaleBias"].SetValue(scaleBias);
                            }

                            PWorldInverseTranspose.SetValue(Matrix.Transpose(Matrix.Invert(w1))); 
                            PNormalMap.SetValue(obj.Modelo.getTexture(TextureType.BUMP, i, j));
                            //render.device.Textures[1] = obj.Modelo.getTexture(TextureType.BUMP, i, j);

                        }
                        if (useSpecular)
                        {
                            PSpecularMap.SetValue(obj.Modelo.getTexture(TextureType.SPECULAR, i, j));
                            //render.device.Textures[2] = obj.Modelo.getTexture(TextureType.SPECULAR, i, j);
                            PspecularIntensityScale.SetValue(SpecularIntensityMapScale);
                            PspecularPowerScale.SetValue(SpecularPowerMapScale);                            
                        }
                        else
                        {
                            PspecularIntensity.SetValue(specularIntensity);
                            PspecularPower.SetValue(specularPower);                            
                        }

                        if (useGlow)
                            Pglow.SetValue(obj.Modelo.getTexture(TextureType.GLOW, i, j));
                            //render.device.Textures[3] = obj.Modelo.getTexture(TextureType.GLOW, i, j);

                        if (useAmbientCubeMap)
                        {
                            PAmbientCube.SetValue(obj.Modelo.GetCubeTexture(TextureType.AMBIENT_CUBE_MAP, i, j));
                            //render.device.Textures[4] = obj.Modelo.GetCubeTexture(TextureType.AMBIENT_CUBE_MAP, i, j);
                            PAmbientCubeMapScale.SetValue(AmbientCubeMapScale);
                        }
                        
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
            this._shader = factory.GetEffect(effect,true,false);            
            base.Initialize(ginfo, factory, obj);

            PProjection = this._shader.Parameters["Projection"];
            PView = this._shader.Parameters["View"];
            PuseSpecular = this._shader.Parameters["useSpecular"];
            PuseBump = this._shader.Parameters["useBump"];
            PuseGlow = this._shader.Parameters["useGlow"];
            PuseParalax = this._shader.Parameters["useParalax"];
            Pid = this._shader.Parameters["id"];
            PTexture = this._shader.Parameters["Texture"];
            PspecularPowerScale = this._shader.Parameters["specularPowerScale"]; 
            PspecularIntensityScale = this._shader.Parameters["specularIntensityScale"];
            PspecularPower = this._shader.Parameters["specularPower"];
            PspecularIntensity = this._shader.Parameters["specularIntensity"];
            PWorld = this._shader.Parameters["World"];
            PWorldInverseTranspose = this._shader.Parameters["WorldInverseTranspose"];
            //PNormalMap = this._shader.Parameters["NormalMap"];
            //PSpecularMap = this._shader.Parameters["SpecularMap"];
            //Pglow = this._shader.Parameters["glow"];
            //PAmbientCube = this._shader.Parameters["ambientcube"];
            PAmbientCubeMapScale = this._shader.Parameters["ambientScale"];

            if (useAmbientCubeMap == true )
            {
                this._shader.CurrentTechnique = this._shader.Techniques["Technique2"];
            }
            else
            {
                this._shader.CurrentTechnique = this._shader.Techniques["Technique1"];
            }

        }
        EffectParameter Pglow; 
        EffectParameter PSpecularMap; 
        EffectParameter  PNormalMap; 
        EffectParameter  PWorld; 
        EffectParameter  PWorldInverseTranspose; 
        EffectParameter  PAmbientCube;
        EffectParameter  PAmbientCubeMapScale;        
        
        EffectParameter  PspecularPowerScale; 
        EffectParameter  PspecularIntensityScale; 
        EffectParameter  PspecularPower; 
        EffectParameter  PspecularIntensity; 

        EffectParameter  PTexture; 
        EffectParameter PProjection;
        EffectParameter PView;
        EffectParameter PuseSpecular;
        EffectParameter PuseBump;
        EffectParameter PuseGlow;
        EffectParameter PuseParalax;
        EffectParameter Pid;
        
    }
}
#endif