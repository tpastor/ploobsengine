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

namespace PloobsEngine.Material
{
    /// <summary>
    /// Shader that uses branching to use Glow,Bump, Specular and Paralax
    /// Dont need to use all of them at once
    /// </summary>
    public class DeferredEnvironmentCustomShader : IShader
    {
        private string effect;
        private Effect _shader;
        private bool useGlow;        
        private bool useBump;
        private bool useSpecular;


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
        /// <param name="specularIntensity">The specular intensity.</param>
        /// <param name="specularPower">The specular power.</param>
        public DeferredEnvironmentCustomShader(bool useGlow, bool useBump, bool useSpecular, float specularIntensity = 0, float specularPower = 0)
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

            this.effect = "AllBufferEnvironment";
            this.specularIntensity = specularIntensity;
            this.specularPower = specularPower;
            this.useGlow = useGlow;
            this.useSpecular = useSpecular;
            this.useBump = useBump;            
            specularPowerMapScale = 1;
            specularIntensityMapScale = 1;            
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

        private float reflectionIndex = 1;
        public float ReflectionIndex
        {
            get
            {
                return reflectionIndex;
            }
            set
            {
                this.reflectionIndex = value;
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
               Pid.SetValue(shaderId);            
               PuseGlow.SetValue(useGlow);
               PuseBump.SetValue(useBump);
               PuseSpecular.SetValue(useSpecular);                                                   
               PView.SetValue(cam.View);
               PProjection.SetValue(cam.Projection);
               PCameraPos.SetValue(cam.Position);
               PreflectionIndex.SetValue(reflectionIndex);

                Matrix wld = obj.WorldMatrix;

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        //PDiffuse.SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE,i,j));
                        //PTexture.SetValue(obj.Modelo.GetTextureInformation(i)[j].getCubeTexture(TextureType.ENVIRONMENT));

                        render.device.Textures[0] = obj.Modelo.GetTextureInformation(i)[j].getTexture(TextureType.DIFFUSE);                        
                        render.device.Textures[4] = obj.Modelo.GetTextureInformation(i)[j].getCubeTexture(TextureType.ENVIRONMENT);
                        
                        if (useBump)
                        {
                            //PNormalMap.SetValue(obj.Modelo.getTexture(TextureType.BUMP, i, j));
                            render.device.Textures[1] = obj.Modelo.GetTextureInformation(i)[j].getTexture(TextureType.BUMP);
                        }
                        if (useSpecular)
                        {
                            //PSpecularMap.SetValue(obj.Modelo.getTexture(TextureType.SPECULAR, i, j));                            
                            render.device.Textures[2] = obj.Modelo.GetTextureInformation(i)[j].getTexture(TextureType.SPECULAR);
                            PspecularIntensityScale.SetValue(SpecularIntensityMapScale);
                            PspecularPowerScale.SetValue(SpecularPowerMapScale);                            
                        }
                        else
                        {
                            PspecularIntensity.SetValue(specularIntensity);
                            PspecularPower.SetValue(specularPower);                            
                            
                        }

                        if (useGlow)
                            //Pglow.SetValue(obj.Modelo.getTexture(TextureType.GLOW, i, j));
                            render.device.Textures[3] = obj.Modelo.GetTextureInformation(i)[j].getTexture(TextureType.GLOW);
                 

                        Matrix w1 = Matrix.Multiply(bi[j].ModelLocalTransformation,wld);                    
                        PWorld.SetValue(w1);
                        PWorldInverseTranspose.SetValue(Matrix.Transpose(Matrix.Invert(w1))); 

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

            PProjection = this._shader.Parameters["Projection"];
            PView = this._shader.Parameters["View"];
            PuseSpecular = this._shader.Parameters["useSpecular"];
            PuseBump = this._shader.Parameters["useBump"];
            PuseGlow = this._shader.Parameters["useGlow"];
            PCameraPos = this._shader.Parameters["CameraPos"];
            Pid = this._shader.Parameters["id"];
            //PTexture = this._shader.Parameters["Texture"];
            PspecularPowerScale = this._shader.Parameters["specularPowerScale"]; 
            PspecularIntensityScale = this._shader.Parameters["specularIntensityScale"];
            PspecularPower = this._shader.Parameters["specularPower"];
            PspecularIntensity = this._shader.Parameters["specularIntensity"];
            PWorld = this._shader.Parameters["World"];
            PWorldInverseTranspose = this._shader.Parameters["WorldInverseTranspose"];
            //PNormalMap = this._shader.Parameters["NormalMap"];
            //PSpecularMap = this._shader.Parameters["SpecularMap"];
            //Pglow = this._shader.Parameters["glow"];
            PreflectionIndex = this._shader.Parameters["reflectionIndex"];
            //PDiffuse = this._shader.Parameters["DifTex"];
        }
        //EffectParameter PDiffuse;
        EffectParameter PreflectionIndex; 
        //EffectParameter Pglow; 
        //EffectParameter PSpecularMap; 
        //EffectParameter  PNormalMap; 
        EffectParameter  PWorld; 
        EffectParameter  PWorldInverseTranspose; 
        
        EffectParameter  PspecularPowerScale; 
        EffectParameter  PspecularIntensityScale; 
        EffectParameter  PspecularPower; 
        EffectParameter  PspecularIntensity;
        EffectParameter  PCameraPos;

        //EffectParameter  PTexture; 
        EffectParameter PProjection;
        EffectParameter PView;
        EffectParameter PuseSpecular;
        EffectParameter PuseBump;
        EffectParameter PuseGlow;        
        EffectParameter Pid;
        
    }
}
#endif