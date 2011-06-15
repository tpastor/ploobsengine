using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine.Logger;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Called when the texture change
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="model">The model.</param>
    public delegate void OnTextureChange(TextureType type,TextureInformation tex);

    /// <summary>
    /// Model Specification
    /// </summary>
    public abstract class IModelo 
    {
        public IModelo(GraphicFactory factory, String modelName,bool callLoadContent = true)
            : this(false,factory,modelName,callLoadContent)
        {
                        
        }

        public IModelo(bool isinternal, GraphicFactory factory, String modelName, bool callLoadContent = true)
        {
            this.isInternal = isinternal;

            if (String.IsNullOrEmpty(modelName))
            {
                ActiveLogger.LogMessage("ModelName Cannot Be null", LogLevel.FatalError);                
                throw new Exception("ModelName Cannot Be null");
            }

            if (factory == null)
            {
                ActiveLogger.LogMessage("Factory Cannot Be null", LogLevel.FatalError);
                throw new Exception("Factory Cannot Be null");
            }
            
            this.modelName = modelName;            
            this.factory = factory;

            if(callLoadContent)
                LoadModelo(factory);
        }

        internal bool isInternal = false;                    
        String    modelName = null;
        
        protected BatchInformation[][] BatchInformations = null;
        protected TextureInformation[][] TextureInformations = null;
        protected GraphicFactory factory;

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


        /// <summary>
        /// Gets one texture of the model.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="meshIndex">Index of the mesh.</param>
        /// <param name="meshPartIndex">Index of the mesh part.</param>
        /// <returns></returns>
        public Texture2D getTexture(TextureType type, int meshIndex = 0, int meshPartIndex = 0)
        {
            return TextureInformations[meshIndex][meshPartIndex].getTexture(type);
        }

        /// <summary>
        /// Sets the texture.
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="type">The type.</param>
        public void SetTexture(String textureName, TextureType type, int meshIndex = 0, int meshPartIndex = 0)
        {
            TextureInformation ti = TextureInformations[meshIndex][meshPartIndex];
            ti.SetTexture(textureName, type);            
        }

        /// <summary>
        /// Load the model
        /// </summary>
        /// <param name="factory">The factory.</param>
        internal void LoadModelo(GraphicFactory factory)
        {   
            if(BatchInformations == null)
                LoadModel(factory, out BatchInformations,out TextureInformations);                                    
        }

        /// <summary>
        /// Loads the batch info, called by the constructor if callLoadContent is true
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="BatchInformations">The batch informations.</param>
        /// <param name="TextureInformation">The texture information.</param>
        protected abstract void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformation);


        /// <summary>
        /// Sets the texture.
        /// </summary>
        /// <param name="tex">The tex.</param>
        /// <param name="type">The type.</param>
        /// <param name="meshIndex">Index of the mesh.</param>S
        /// <param name="meshPartIndex">Index of the mesh part.</param>
        public void SetTexture(Texture2D tex, TextureType type, int meshIndex = 0, int meshPartIndex = 0)
        {
            TextureInformations[meshIndex][meshPartIndex].SetTexture(tex, type);
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
        public BatchInformation[] GetBatchInformation(int meshNumber)
        {
            return BatchInformations[meshNumber];
        }

        public TextureInformation[] GetTextureInformation(int meshNumber)
        {
            return TextureInformations[meshNumber];
        }

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
        MULTITEX4,
        /// <summary>
        /// HEIGHTMAP, can used anywhere
        /// </summary>
        HEIGHTMAP


    }

    
}
