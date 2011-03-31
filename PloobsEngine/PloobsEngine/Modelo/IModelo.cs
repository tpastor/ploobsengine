using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PloobsEngine.Modelo
{
    /// <summary>
    /// Model Specification
    /// </summary>
    public interface IModelo 
    {
        /// <summary>
        /// load the model
        /// Must be Called
        /// The engine dont call this method
        /// </summary>
        void LoadModelo();
        /// <summary>
        /// Serializes the specified Model.
        /// </summary>
        /// <param name="textWriter">The text writer.</param>
        void Serialize(System.Xml.XmlTextWriter textWriter);
        /// <summary>
        /// Deserialize.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="engine">The engine.</param>
        void DeSerialize(System.Xml.XmlNode node);
        /// <summary>
        /// Gets the model tag.
        /// </summary>
        /// <returns></returns>
        object getModelTag();
        /// <summary>
        /// Gets the bones transformation.
        /// </summary>
        /// <returns></returns>
        Matrix[] getBonesTransformation();
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <param name="textureType">Type of the texture.</param>
        /// <returns></returns>
        Texture2D getTexture(TextureType textureType);
        /// <summary>
        /// Gets the bouding sphere.
        /// </summary>
        /// <returns></returns>
        BoundingSphere GetBoudingSphere();

        /// <summary>
        /// Gets the index of the parent bone.
        /// </summary>
        /// <param name="meshNumber">The mesh number.</param>
        /// <returns></returns>        
        int GetParentBoneIndex(int meshNumber);
        /// <summary>
        /// Gets the mesh number.
        /// </summary>
        int MeshNumber{get;}
        /// <summary>
        /// Gets the vertex buffer.
        /// </summary>
        /// <param name="meshNumber">The mesh number.</param>
        /// <returns></returns>
        VertexBuffer GetVertexBuffer(int meshNumber);
        /// <summary>
        /// Gets the index buffer.
        /// </summary>
        /// <param name="meshNumber">The mesh number.</param>
        /// <returns></returns>
        IndexBuffer GetIndexBuffer(int meshNumber);
        /// <summary>
        /// Gets the batch information.
        /// </summary>
        /// <param name="meshNumber">The mesh number.</param>
        /// <returns></returns>
        BatchInformation[] GetBatchInformation(int meshNumber);


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        String Name
        {
            get;
            set;
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
