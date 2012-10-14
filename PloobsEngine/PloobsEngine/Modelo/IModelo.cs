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
    /// <param name="tex">The tex.</param>
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

        protected IModelo() { }

        internal IModelo(bool isinternal, GraphicFactory factory, String modelName, bool callLoadContent = true)
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
        /// <summary>
        /// Model Name
        /// </summary>
        protected String  modelName = null;

        /// <summary>
        /// All BAtchInformations of the model
        /// </summary>
        protected BatchInformation[][] BatchInformations = null;
        /// <summary>
        /// All Texture Informations from the mode
        /// </summary>
        protected TextureInformation[][] TextureInformations = null;
        /// <summary>
        /// Graphic Factory
        /// </summary>
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

        internal void afterAdded(IObject obj)
        {
            this.AfterAdded(obj);
        }

        /// <summary>
        /// Called Afters the objets has been added to the world
        /// </summary>
        /// <param name="obj">The obj.</param>
        protected virtual void AfterAdded(IObject obj)
        {
        }

        /// <summary>
        /// Gets one texture of the model.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="meshIndex">Index of the mesh.</param>
        /// <param name="meshPartIndex">Index of the mesh part.</param>
        /// <returns></returns>
        public Texture2D getTexture(TextureType type = TextureType.DIFFUSE, int meshIndex = 0, int meshPartIndex = 0)
        {
            return TextureInformations[meshIndex][meshPartIndex].getTexture(type);
        }

        /// <summary>
        /// Sets the texture.
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="type">The type.</param>
        /// <param name="meshIndex">Index of the mesh.</param>
        /// <param name="meshPartIndex">Index of the mesh part.</param>
        public void SetTexture(String textureName, TextureType type = TextureType.DIFFUSE, int meshIndex = 0, int meshPartIndex = 0)
        {
            TextureInformation ti = TextureInformations[meshIndex][meshPartIndex];
            ti.SetTexture(textureName, type);            
        }

        /// <summary>
        /// Set Cube Texture of a mesh
        /// </summary>
        /// <param name="textureCube"></param>
        /// <param name="meshIndex"></param>
        /// <param name="meshPartIndex"></param>
        public void SetCubeTexture(TextureCube textureCube, TextureType TextureType = TextureType.ENVIRONMENT, int meshIndex = 0, int meshPartIndex = 0)
        {
            TextureInformation ti = TextureInformations[meshIndex][meshPartIndex];
            ti.SetCubeTexture(textureCube, TextureType);
        }


        /// <summary>
        /// Get texture cube 
        /// (Only Environment and AmbientCubeMap)
        /// </summary>
        /// <param name="TextureType"></param>
        /// <param name="meshIndex"></param>
        /// <param name="meshPartIndex"></param>
        /// <returns></returns>
        public TextureCube GetCubeTexture(TextureType TextureType = TextureType.ENVIRONMENT, int meshIndex = 0, int meshPartIndex = 0)
        {
            TextureInformation ti = TextureInformations[meshIndex][meshPartIndex];
            return ti.getCubeTexture(TextureType);
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
        public void SetTexture(Texture2D tex, TextureType type = TextureType.DIFFUSE, int meshIndex = 0, int meshPartIndex = 0)
        {
            TextureInformations[meshIndex][meshPartIndex].SetTexture(tex, type);
        }

        /// <summary>
        /// Sets the texture.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="type">The type.</param>
        /// <param name="meshIndex">Index of the mesh.</param>
        /// <param name="meshPartIndex">Index of the mesh part.</param>
        public void SetTexture(Color color, TextureType type = TextureType.DIFFUSE, int meshIndex = 0, int meshPartIndex = 0)
        {

            TextureInformations[meshIndex][meshPartIndex].SetTexture(factory.CreateTexture2DColor(color), type);
        }

        /// <summary>
        /// Gets the Total mesh number.
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
        public BatchInformation[] GetBatchInformation(int meshNumber = 0)
        {
            return BatchInformations[meshNumber];
        }

        /// <summary>
        /// Gets the texture information.
        /// </summary>
        /// <param name="meshNumber">The mesh number.</param>
        /// <returns></returns>
        public TextureInformation[] GetTextureInformation(int meshNumber = 0)
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

        public virtual void Update(GameTime gameTime, IWorld world)
        {
        }

        /// <summary>
        /// Cleans up the IModelo
        /// </summary>
        public virtual void CleanUp(GraphicFactory factory)
        {
            factory.ReleaseAsset(Name);
            for (int i = 0; i < MeshNumber; i++)
            {
                foreach (var item in TextureInformations[i])
                {
                    factory.ReleaseAsset(item.DiffuseMapName);
                    factory.ReleaseAsset(item.BumpMapName);
                    factory.ReleaseAsset(item.GlowName);
                    factory.ReleaseAsset(item.SpecularMapName);
                }

                ///Automatic handled
                foreach (var item in BatchInformations[i])
                {
                    if (item.VertexBuffer != null && item.VertexBuffer.IsDisposed == false)
                    {
                        item.VertexBuffer.Dispose();
                    }

                    if (item.IndexBuffer != null && item.IndexBuffer.IsDisposed == false)
                    {
                        item.IndexBuffer.Dispose();
                    }

                    if (item.InstancedVertexBuffer != null && item.InstancedVertexBuffer.IsDisposed == false)
                    {
                        item.InstancedVertexBuffer.Dispose();
                    }
                }
            }
        }

    }
    /// <summary>
    /// Textures Types avaliable
    /// </summary>
    public enum TextureType
    {
        /// <summary>
        /// EnvironmentMap used for environment lightining
        /// </summary>
        AMBIENT_CUBE_MAP,
        /// <summary>
        /// EnvironmentMap
        /// </summary>
        ENVIRONMENT,
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
