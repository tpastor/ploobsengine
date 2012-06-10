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
using PloobsEngine.Light;

namespace PloobsEngine.Material
{
    /// <summary>
    /// ReflectionType
    /// </summary>
    public enum ReflectionType
    {
        /// <summary>
        /// PerfectMirror
        /// </summary>
        PerfectMirror,
        /// <summary>
        /// ReflexiveSurface
        /// </summary>
        ReflexiveSurface
    }

    /// <summary>
    /// Deferred rnvironment Mapping Reflective Shader
    /// </summary>
    public class DeferredEMReflectiveShader : IShader
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

        /// <summary>
        /// Gets or sets the index of the reflection.
        /// </summary>
        /// <value>
        /// The index of the reflection.
        /// </value>
        public float ReflectionIndex
        {
            get { return reflectionIndex; }
            set { reflectionIndex = value; 
            if(isInitialized)
                this._shader.Parameters["reflectionIndex"].SetValue(reflectionIndex);
            }
        }


        /// <summary>
        /// Gets or sets the texture cube.
        /// </summary>
        /// <value>
        /// The texture cube.
        /// </value>
        public TextureCube TextureCube
        {
            get { return texCube; }
            set { texCube = value; }
        }
        
        internal DeferredEMReflectiveShader()
        {        
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredEMReflectiveShader"/> class.
        /// </summary>
        /// <param name="texName">Name of the tex.</param>
        /// <param name="type">The type.</param>
        public DeferredEMReflectiveShader(String texName , ReflectionType type  )
        {            
            this.rType = type;
            this.texName = texName;            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredEMReflectiveShader"/> class.
        /// Reflexive Surface implicity
        /// </summary>
        /// <param name="texName">Name of the tex.</param>
        /// <param name="reflectionIndex">Index of the reflection.</param>
        public DeferredEMReflectiveShader(String texName,float reflectionIndex)
        {
            this.reflectionIndex = reflectionIndex;
            this.texName = texName;         
        }

        private float specularPower = 0f;

        /// <summary>
        /// Gets or sets the specular intensity.
        /// </summary>
        /// <value>
        /// The specular intensity.
        /// </value>
        public float SpecularIntensity
        {
            get { return specularIntensity; }
            set { specularIntensity = value; 
            if(isInitialized)
                this._shader.Parameters["specularIntensity"].SetValue(specularIntensity);
            }
        }

        /// <summary>
        /// Gets or sets the specular power.
        /// </summary>
        /// <value>
        /// The specular power.
        /// </value>
        public float SpecularPower
        {
            get { return specularPower; }
            set { specularPower = value; 
            if(isInitialized)
                this._shader.Parameters["specularPower"].SetValue(specularPower);
            }
        }

        /// <summary>
        /// Called Once only, before all draws
        /// </summary>
        /// <param name="ent">The current entity.</param>
        /// <param name="lights">All The lights</param>
        public override void PreUpdate(IObject ent, IList<ILight> lights)
        {
            if (rType == ReflectionType.PerfectMirror)
                this._shader.CurrentTechnique = this._shader.Techniques["PerfectMirror"];
            else if (rType == ReflectionType.ReflexiveSurface)
                this._shader.CurrentTechnique = this._shader.Techniques["ReflexiveSurface"];           

            this._shader.Parameters["reflectionIndex"].SetValue(reflectionIndex);            
            this._shader.Parameters["specularIntensity"].SetValue(specularIntensity);
            this._shader.Parameters["specularPower"].SetValue(specularPower);

        }

        /// <summary>
        /// Draws the specified gt.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        /// <param name="camera">The camera.</param>
        /// <param name="lights">The lights.</param>
        protected override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera camera, IList<ILight> lights)
        {                
                this._shader.Parameters["View"].SetValue(camera.View);
                this._shader.Parameters["Projection"].SetValue(camera.Projection);

                //this._shader.Parameters["Cubemap"].SetValue(texCube);
                render.device.Textures[4] = texCube;

                this._shader.Parameters["id"].SetValue(shaderId);
                
                Matrix wld = obj.WorldMatrix;

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        if (rType == ReflectionType.ReflexiveSurface)
                            //this._shader.Parameters["Texture"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE, i, j));
                            render.device.Textures[0] = obj.Modelo.getTexture(TextureType.DIFFUSE, i, j);

                        this._shader.Parameters["World"].SetValue(Matrix.Multiply(wld, bi[j].ModelLocalTransformation));
                        render.RenderBatch(bi[j], _shader);
                    }         
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

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="ginfo"></param>
        /// <param name="factory"></param>
        /// <param name="obj"></param>
        public override void  Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
 	    {
            this._shader = factory.GetEffect("EMSHADER",true,true);

            if (texCube == null)
            {
                texCube = factory.GetTextureCube(texName, false);             
            }
            
            
            base.Initialize(ginfo, factory, obj);
        }

        /// <summary>
        /// Draw the object in a simple way (WITH MINIMUM EFFECTS,....)
        /// USED IN RELECTIONS, REFRACTION .....
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        /// <param name="clippingPlane">The clipping plane.</param>
        /// <param name="useAlphaBlending">if set to <c>true</c> [use alpha blending].</param>
        public override void BasicDraw(GameTime gt, IObject obj, ref Matrix view, ref Matrix projection, IList<ILight> lights, RenderHelper render, Plane? clippingPlane, bool useAlphaBlending = false)
        {
            //no reflection refraction
        }

        /// <summary>
        /// Exctract the depth from an object
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="View">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="render">The render.</param>
        public override void DepthExtractor(GameTime gt, IObject obj, ref Matrix View, ref Matrix projection, RenderHelper render)
        {
            //no shadow
        }
      

    }
}
#endif