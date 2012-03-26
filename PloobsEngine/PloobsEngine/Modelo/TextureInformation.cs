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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Handles the textures of a IModelo.
    /// Similar to BatchInformation, but for textures only
    /// </summary>
    public class TextureInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureInformation"/> class.
        /// </summary>
        /// <param name="isinternal">if set to <c>true</c> [isinternal].</param>
        /// <param name="factory">The factory.</param>
        /// <param name="diffuseTextureName">Name of the diffuse texture.</param>
        /// <param name="bumpTextureName">Name of the bump texture.</param>
        /// <param name="specularTextureName">Name of the specular texture.</param>
        /// <param name="glowTextureName">Name of the glow texture.</param>
        public TextureInformation(bool isinternal, GraphicFactory factory, String diffuseTextureName = null, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null)
        {
            isDisposed = false;
            this.isInternal = isinternal;            

            if (factory == null)
            {
                ActiveLogger.LogMessage("Factory Cannot Be null", LogLevel.FatalError);
                throw new Exception("Factory Cannot Be null");
            }
            
            this._diffuseName = diffuseTextureName;
            this._specularName = specularTextureName;
            this._bumpName = bumpTextureName;
            this._glowName = glowTextureName;
            this.factory = factory;            
        }

        internal bool isInternal = false;            
        protected const string CUSTOM = "CUSTOM";        
        protected string _paralaxName = null;
        protected string _glowName = null;
        protected string _diffuseName = null;
        protected string _bumpName = null;
        protected string _specularName = null;
        private string _reflectionName = null;
        private string _ambientName = null;
        protected string _mult1 = null;
        protected string _mult2 = null;
        protected string _mult3 = null;
        protected string _mult4 = null;
        protected string _heightMap = null;
        protected Texture2D diffuse = null;
        protected TextureCube reflection = null;
        protected TextureCube ambient = null;

        protected Texture2D bump = null;
        protected Texture2D specular = null;
        protected Texture2D glow = null;
        protected Texture2D paralax = null;
        protected Texture2D multitexture1 = null;
        protected Texture2D multitexture2 = null;
        protected Texture2D multitexture3 = null;
        protected Texture2D multitexture4 = null;
        protected Texture2D heightMap = null;        
        protected GraphicFactory factory;

        public bool isDisposed
        {
            private set;
            get;
        }


        /// <summary>
        /// Disposes all.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public void CleanUp(GraphicFactory factory)
        {
            if (bump != null)
                factory.ReleaseAsset(BumpMapName);

            if (specular != null)
                factory.ReleaseAsset(_specularName);

            if (diffuse != null)
                factory.ReleaseAsset(DiffuseMapName);

            if (glow != null)
                factory.ReleaseAsset(_glowName);

            if (heightMap != null)
                factory.ReleaseAsset(HeightMapName);

            if (multitexture1 != null)
                factory.ReleaseAsset(MultiTexture1MapName);

            if (multitexture2 != null)
                factory.ReleaseAsset(MultiTexture2MapName);

            if (multitexture3 != null)
                factory.ReleaseAsset(MultiTexture3MapName);

            if (multitexture4 != null)
                factory.ReleaseAsset(MultiTexture4MapName);


            isDisposed = true;
        }

        /// <summary>
        /// Occurs when [on texture change].
        /// </summary>
        public event OnTextureChange OnTextureChange = null;


        /// <summary>
        /// Gets the cube texture.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public TextureCube getCubeTexture(TextureType type)
        {
            if (type == TextureType.ENVIRONMENT)
            {
                return this.reflection;
            }
            else if(type == TextureType.AMBIENT_CUBE_MAP)
            {
                return this.ambient;
            }
            else
            {
#if WINDOWS
                System.Diagnostics.Debug.Fail("Invalid Parameter, Can only pass ENVIRONMENT and AMBIENT_CUBE_MAP to ths function");
#endif
                return null;
            }
        }

        /// <summary>
        /// Gets one texture of the model.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Texture2D getTexture(TextureType type)
        {
           

            if (type == TextureType.DIFFUSE)
            {
                return diffuse;                
            }
            else if (type == TextureType.BUMP)
            {
                return this.bump;
            }
            else if (type == TextureType.SPECULAR)
            {
                return this.specular;
            }
            else if (type == TextureType.GLOW)
            {
                return this.glow;
            }
            else if (type == TextureType.PARALAX)
            {
                return this.paralax;
            }
            else if (type == TextureType.HEIGHTMAP)
            {
                return this.heightMap;
            }
            else if (type == TextureType.MULTITEX1)
            {
                return this.multitexture1;
            }
            else if (type == TextureType.MULTITEX2)
            {
                return this.multitexture2;
            }
            else if (type == TextureType.MULTITEX3)
            {
                return this.multitexture3;
            }
            else if (type == TextureType.MULTITEX4)
            {
                return this.multitexture4;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the passed texture type to null.
        /// </summary>
        /// <param name="type">The type.</param>
        public void SetNullTexture(TextureType type)
        {
            switch (type)
            {
                case TextureType.AMBIENT_CUBE_MAP:
                    this._ambientName = "CUSTOM";
                    this.ambient = null;
                    break;
                case TextureType.ENVIRONMENT:
                     this._reflectionName = "CUSTOM";
                    this.reflection = null;
                    break;
                case TextureType.DIFFUSE:
                    this._diffuseName = "CUSTOM";
                    this.diffuse = null;
                    break;
                case TextureType.SPECULAR:
                    this._specularName = "CUSTOM";
                    this.specular = null;
                    break;
                case TextureType.BUMP:
                    this._bumpName = "CUSTOM";
                    this.bump = null;
                    break;
                case TextureType.GLOW:
                    this._glowName = "CUSTOM";
                    this.glow = null;
                    break;
                case TextureType.PARALAX:
                    this._paralaxName = "CUSTOM";
                    this.paralax = null;
                    break;
                case TextureType.MULTITEX1:
                    this._mult1 = "CUSTOM";
                    this.multitexture1 = null;
                    break;
                case TextureType.MULTITEX2:
                    this._mult2 = "CUSTOM";
                    this.multitexture2 = null;
                    break;
                case TextureType.MULTITEX3:
                    this._mult3 = "CUSTOM";
                    this.multitexture3 = null;
                    break;
                case TextureType.MULTITEX4:
                    this._mult4 = "CUSTOM";
                    this.multitexture4 = null;
                    break;
                case TextureType.HEIGHTMAP:
                    this._heightMap = "CUSTOM";
                    this.heightMap = null;
                    break;
                default:
                    ActiveLogger.LogMessage("Setting Invalid Type of Texture", LogLevel.RecoverableError);
                    return;
            }
            if (OnTextureChange != null)
                OnTextureChange(type, this);
        }

        /// <summary>
        /// Sets the texture.
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="type">The type.</param>
        public void SetTexture(String textureName, TextureType type)
        {
            switch (type)
            {

                case TextureType.AMBIENT_CUBE_MAP:
                    this._ambientName = textureName;
                    break;
                case TextureType.ENVIRONMENT:
                    this._reflectionName = textureName;
                    break;
                case TextureType.DIFFUSE:
                    this._diffuseName = textureName;
                    break;
                case TextureType.SPECULAR:
                    this._specularName = textureName;
                    break;
                case TextureType.BUMP:
                    this._bumpName = textureName;
                    break;
                case TextureType.GLOW:
                    this._specularName = textureName;
                    break;
                case TextureType.PARALAX:
                    this._paralaxName = textureName;
                    break;
                case TextureType.MULTITEX1:
                    this._mult1 = textureName;
                    break;
                case TextureType.MULTITEX2:
                    this._mult2 = textureName;
                    break;
                case TextureType.MULTITEX3:
                    this._mult3 = textureName;
                    break;
                case TextureType.MULTITEX4:
                    this._mult4 = textureName;
                    break;
                case TextureType.HEIGHTMAP:
                    this._heightMap = textureName;
                    break;
                default:
                    ActiveLogger.LogMessage("Setting Invalid Type of Texture: " + textureName, LogLevel.RecoverableError);
                    return;
            }

            LoadTexture();
            if (OnTextureChange != null)
                OnTextureChange(type,this);
        }

        public  void LoadTexture()
        {
            if (!String.IsNullOrEmpty(_reflectionName) && _reflectionName != CUSTOM)
            {
                this.reflection = factory.GetTextureCube(_reflectionName, isInternal);
            }

            if (!String.IsNullOrEmpty(_ambientName) && _ambientName != CUSTOM)
            {
                this.ambient = factory.GetTextureCube(_ambientName, isInternal);
            }

            if (!String.IsNullOrEmpty(_diffuseName) && _diffuseName != CUSTOM)
            {
                this.diffuse = factory.GetTexture2D(_diffuseName, isInternal);
            }
            if (!String.IsNullOrEmpty(_bumpName) && _bumpName != CUSTOM)
            {
                this.bump = factory.GetTexture2D(_bumpName, isInternal);
            }
            if (!String.IsNullOrEmpty(_specularName) && _specularName != CUSTOM)
            {
                this.specular = factory.GetTexture2D(_specularName, isInternal);
            }
            if (!String.IsNullOrEmpty(_glowName) && _glowName != CUSTOM)
            {
                this.glow = factory.GetTexture2D(_glowName, isInternal);
            }
            if (!String.IsNullOrEmpty(_paralaxName) && _paralaxName != CUSTOM)
            {
                this.paralax = factory.GetTexture2D(_paralaxName, isInternal);
            }
            if (!String.IsNullOrEmpty(_mult1) && _mult1 != CUSTOM)
            {
                this.multitexture1 = factory.GetTexture2D(_mult1, isInternal);
            }
            if (!String.IsNullOrEmpty(_mult2) && _mult2 != CUSTOM)
            {
                this.multitexture2 = factory.GetTexture2D(_mult2, isInternal);
            }
            if (!String.IsNullOrEmpty(_mult3) && _mult3 != CUSTOM)
            {
                this.multitexture3 = factory.GetTexture2D(_mult3, isInternal);
            }
            if (!String.IsNullOrEmpty(_mult4) && _mult4 != CUSTOM)
            {
                this.multitexture4 = factory.GetTexture2D(_mult4, isInternal);
            }
            if (!String.IsNullOrEmpty(_heightMap) && _heightMap != CUSTOM)
            {
                this.heightMap = factory.GetTexture2D(_heightMap, isInternal);
            }                        
        }

        /// <summary>
        /// Sets the cube texture.
        /// </summary>
        /// <param name="tex">The tex.</param>
        /// <param name="type">The type.</param>
        public void SetCubeTexture(TextureCube tex, TextureType type)
        {
            switch (type)
            {

                case TextureType.AMBIENT_CUBE_MAP:
                    this._ambientName = "CUSTOM";
                    this.ambient = tex;
                    break;
                case TextureType.ENVIRONMENT:
                    this._reflectionName = "CUSTOM";
                    this.reflection = tex;
                    break;
                default:
                    ActiveLogger.LogMessage("Setting Invalid Type of Texture", LogLevel.RecoverableError);
                    break;
            }
        
        }

                /// <summary>
        /// Sets the texture.
        /// </summary>
        /// <param name="tex">The tex.</param>
        /// <param name="type">The type.</param>
        public void SetTexture(Texture2D tex, TextureType type)
        {
            switch (type)
            {

                                  

                case TextureType.DIFFUSE:
                    this._diffuseName = "CUSTOM";
                    this.diffuse = tex;
                    break;
                case TextureType.SPECULAR:
                    this._specularName = "CUSTOM";
                    this.specular = tex;
                    break;
                case TextureType.BUMP:
                    this._bumpName = "CUSTOM";
                    this.bump = tex;
                    break;
                case TextureType.GLOW:
                    this._glowName = "CUSTOM";
                    this.glow = tex;
                    break;
                case TextureType.PARALAX:
                    this._paralaxName = "CUSTOM";
                    this.paralax = tex;
                    break;
                case TextureType.MULTITEX1:
                    this._mult1 = "CUSTOM";
                    this.multitexture1 = tex;
                    break;
                case TextureType.MULTITEX2:
                    this._mult2 = "CUSTOM";
                    this.multitexture2 = tex;
                    break;
                case TextureType.MULTITEX3:
                    this._mult3 = "CUSTOM";
                    this.multitexture3 = tex;
                    break;
                case TextureType.MULTITEX4:
                    this._mult4 = "CUSTOM";
                    this.multitexture4 = tex;
                    break;
                case TextureType.HEIGHTMAP:
                    this._heightMap = "CUSTOM";
                    this.heightMap = tex;
                    break;
                default:
                    ActiveLogger.LogMessage("Setting Invalid Type of Texture", LogLevel.RecoverableError);
                    return;
            }
            if (OnTextureChange != null)
                OnTextureChange(type, this);
        }

        /// <summary>
        /// Gets or sets the name of the paralax texture.
        /// </summary>
        /// <value>
        /// The name of the paralax.
        /// </value>
        public string ParalaxName
        {
            get { return _paralaxName; }
            set { _paralaxName = value; }
        }              

        /// <summary>
        /// Gets or sets the name of the glow texture.
        /// </summary>
        /// <value>
        /// The name of the glow.
        /// </value>
        public string GlowName
        {
            get { return _glowName; }
            set { _glowName = value; }
        }
        
        /// <summary>
        /// Gets or sets the name of the bump map.
        /// </summary>
        /// <value>
        /// The name of the bump map.
        /// </value>
        public string BumpMapName
        {
            get { return _bumpName; }
            set { _bumpName = value; }
        }
        

        /// <summary>
        /// Gets or sets the name of the specular map.
        /// </summary>
        /// <value>
        /// The name of the specular map.
        /// </value>
        public string SpecularMapName
        {
            get { return _specularName; }
            set { _specularName = value; }
        }

        /// <summary>
        /// Gets or sets the name of the diffuse map.
        /// </summary>
        /// <value>
        /// The name of the diffuse map.
        /// </value>
        public String DiffuseMapName
        {
            get { return _diffuseName; }
            set { _diffuseName = value; }
        }


        /// <summary>
        /// Gets or sets the name of the multi texture1 map.
        /// </summary>
        /// <value>
        /// The name of the multi texture1 map.
        /// </value>
        public String MultiTexture1MapName
        {
            get
            {
                return _mult1;
            }
            set
            {
                this._mult1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the multi texture2 map.
        /// </summary>
        /// <value>
        /// The name of the multi texture2 map.
        /// </value>
        public String MultiTexture2MapName
        {
            get
            {
                return _mult2;
            }
            set
            {
                this._mult2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the multi texture3 map.
        /// </summary>
        /// <value>
        /// The name of the multi texture3 map.
        /// </value>
        public String MultiTexture3MapName
        {
            get
            {
                return _mult3;
            }
            set
            {
                this._mult3 = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the multi texture4 map.
        /// </summary>
        /// <value>
        /// The name of the multi texture4 map.
        /// </value>
        public String MultiTexture4MapName
        {
            get
            {
                return _mult4;
            }
            set
            {
                this._mult4 = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the height map.
        /// </summary>
        /// <value>
        /// The name of the height map.
        /// </value>
        public String HeightMapName
        {
            get
            {
                return _heightMap;
            }
            set
            {
                this._heightMap = value;
            }
        }


    }
}
