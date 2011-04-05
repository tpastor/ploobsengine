using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine.Logger;
using PloobsEngine.SceneControl;


namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Called when the texture change
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="model">The model.</param>
    public delegate void OnTextureChange(TextureType type,IModelo model);

    /// <summary>
    /// Model Specification
    /// </summary>
    public abstract class IModelo 
    {
        public IModelo(IContentManager contentManager, String modelName, String diffuseTextureName = null, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null)
            : this(false,contentManager,modelName,diffuseTextureName,bumpTextureName,specularTextureName,glowTextureName)
        {
                        
        }

        public IModelo(bool isinternal, IContentManager contentManager, String modelName, String diffuseTextureName = null, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null)
        {
            this.isInternal = isinternal;

            if (String.IsNullOrEmpty(modelName))
            {
                ActiveLogger.LogMessage("ModelName Cannot Be null", LogLevel.FatalError);
                System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(modelName));
                throw new Exception("ModelName Cannot Be null");
            }

            if (_diffuseName == null)
            {
                ActiveLogger.LogMessage("Diffuse Texture Name is null", LogLevel.Warning);
            }

            this.modelName = modelName;
            this._diffuseName = diffuseTextureName;
            this._specularName = specularTextureName;
            this._bumpName = bumpTextureName;
            this._glowName = glowTextureName;
            this.contentManager = contentManager;

            LoadModelo(contentManager);
        }

        internal bool isInternal = false;            
        protected const string CUSTOM = "CUSTOM";
        String    modelName = null;
        protected string _paralaxName = null;
        protected string _glowName = null;
        protected string _diffuseName = null;
        protected string _bumpName = null;
        protected string _specularName = null;
        protected Texture2D diffuse = null;
        protected Texture2D bump = null;
        protected Texture2D specular = null;
        protected Texture2D glow = null;
        protected Texture2D paralax = null;
        protected BatchInformation[][] BatchInformations = null;
        protected IContentManager contentManager;        

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        /// <returns></returns>
        object Tag
        {
            set;
            get;
        }

        public event OnTextureChange OnTextureChange = null;
            
        
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <param name="textureType">Type of the texture.</param>
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
            else
            {
                return null;
            }
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
                default:
                    ActiveLogger.LogMessage("Setting Invalid Type of Texture: " + textureName, LogLevel.RecoverableError);
                    return;
            }

            LoadModelo(contentManager);
            if (OnTextureChange != null)
                OnTextureChange(type, this);
        }

        /// <summary>
        /// Load the model
        /// </summary>
        internal void LoadModelo(IContentManager cmanager)
        {
            this.contentManager = cmanager;
            if (!String.IsNullOrEmpty(_diffuseName) && _diffuseName != CUSTOM)
            {
                this.diffuse = cmanager.GetAsset<Texture2D>(_diffuseName, isInternal);
            }
            if (!String.IsNullOrEmpty(_bumpName) && _bumpName != CUSTOM)
            {
                this.bump = cmanager.GetAsset<Texture2D>(_bumpName, isInternal);
            }
            if (!String.IsNullOrEmpty(_specularName) && _specularName != CUSTOM)
            {
                this.specular = cmanager.GetAsset<Texture2D>(_specularName, isInternal);
            }
            if (!String.IsNullOrEmpty(_glowName) && _glowName != CUSTOM)
            {
                this.glow = cmanager.GetAsset<Texture2D>(_glowName, isInternal);
            }
            if (!String.IsNullOrEmpty(_paralaxName) && _paralaxName != CUSTOM)
            {
                this.paralax = cmanager.GetAsset<Texture2D>(_paralaxName, isInternal);
            }

            if(BatchInformations == null)
                LoadBatchInfo(cmanager, out BatchInformations);
            
        }

        protected abstract void LoadBatchInfo(SceneControl.IContentManager cmanager, out BatchInformation[][] BatchInformations);
        

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
                default:
                    ActiveLogger.LogMessage("Setting Invalid Type of Texture", LogLevel.RecoverableError);
                    return;
            }
            if (OnTextureChange != null)
                OnTextureChange(type, this);
        }

        /// <summary>
        /// Gets the mesh number.
        /// </summary>
        public abstract int MeshNumber{get;}

        /// <summary>
        /// Gets the model radius.
        /// </summary>
        /// <returns></returns>
        public abstract float GetModelRadius();
        
        /// <summary>
        /// Gets the batch information.
        /// </summary>
        /// <param name="meshNumber">The mesh number.</param>
        /// <returns></returns>
        public abstract BatchInformation[] GetBatchInformation(int meshNumber);

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name
        {
            get
            {
                return modelName;
            }
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
    }
    /// <summary>
    /// Textures Types avaliable
    /// </summary>
    public enum TextureType
    {
        /// <summary>
        /// Diffuse
        /// </summary>
        DIFFUSE,
        /// <summary>
        /// Specular
        /// </summary>
        SPECULAR,
        /// <summary>
        /// Bump
        /// </summary>
        BUMP,
        /// <summary>
        /// GLow
        /// </summary>
        GLOW,
        /// <summary>
        /// Paralax
        /// </summary>
        PARALAX,
        /// <summary>
        /// Reliefe Mapping (Not used yet)
        /// </summary>
        RELIEF,
        /// <summary>
        /// Multitex used in Terrain
        /// can be used anywhere
        /// </summary>
        MULTITEX1,
        /// <summary>
        /// Multitex used in terrain
        /// can be used anywhere
        /// </summary>
        MULTITEX2,
        /// <summary>
        /// Multitex used in terrain
        /// can be used anywhere
        /// </summary>
        MULTITEX3,
        /// <summary>
        /// Multitex used in terrain
        /// can be used anywhere
        /// </summary>
        MULTITEX4
    }

    
}
