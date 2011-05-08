using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Light;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using PloobsEngine.Physics;

namespace PloobsEngine.Loader
{

    /// <summary>
    /// Struct that represents a Model
    /// By Now, It creates just pieces of a model, 
    /// IN THE NEXT RELEASE IT WILL CREATE THE FULL MODEL, RETURN AN IMODELO AND A TRANSFORMATION
    /// </summary>
    public struct ObjectInformation
    {
        public string modelName;
        public int modelPart;
        public BatchInformation batchInformation;                
        public Texture2D difuse;
        public Texture2D bump;
        public Texture2D glow;
        public Texture2D specular;
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation;
        public float mass;
        public float dinamicfriction;
        public float staticfriction;
        public float ellasticity;

        public String collisionType;

        /// <summary>
        /// Determines whether this model has the specified texture type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type has texture; otherwise, <c>false</c>.
        /// </returns>
        public bool HasTexture(TextureType type)
        {
            if (type == TextureType.DIFFUSE)
            {
                return difuse != null;
            }
            else if (type == TextureType.BUMP)
            {
                return bump != null;
            }
            else if (type == TextureType.SPECULAR)
            {
                return specular != null;
            }
            else if (type == TextureType.GLOW)
            {
                return glow != null;
            }
            else if (type == TextureType.PARALAX)
            {
                return false;
            }
            return false;
        }
    }

    /// <summary>
    /// Camera Info
    /// </summary>
    public struct CameraInfo
    {
        public Vector3 Position;
        public Vector3 Target;
        public String Name;
    }


    /// <summary>
    /// Dummy Info
    /// </summary>
    public struct DummyInfo
    {
        public String Name;
        public Vector3 Position;
    }

    public struct ConstraintInfo
    {
        public String Name;
        public String type;
        public String bodyA;
        public String bodyB;
        public bool breakable;
        public Vector3 Position;
    
    }

    /// <summary>
    /// Data that represents a Model Loaded
    /// </summary>
    public class ModelLoaderData
    {
        List<ObjectInformation> modelMeshesInfo = new List<ObjectInformation>();
        List<ILight> lightInfo = new List<ILight>();
        List<CameraInfo> cameraInfo = new List<CameraInfo>();
        List<DummyInfo> dummyInfo = new List<DummyInfo>();
        List<ConstraintInfo> constraintInfo = new List<ConstraintInfo>();

        /// <summary>
        /// Gets or sets the dummyinfo list.
        /// </summary>
        /// <value>
        /// The dummy info.
        /// </value>
        public List<DummyInfo> DummyInfo
        {
            get { return dummyInfo; }
            set { dummyInfo = value; }
        }



        public List<ConstraintInfo> ConstraintInfo
        {
            get { return constraintInfo; }
            set { constraintInfo = value; }

        
        }


        /// <summary>
        /// Gets or sets the camera info list.
        /// </summary>
        /// <value>
        /// The camera info.
        /// </value>
        public List<CameraInfo> CameraInfo
        {
            get { return cameraInfo; }
            set { cameraInfo = value; }
        }

        /// <summary>
        /// Gets or sets the lights info list.
        /// </summary>
        /// <value>
        /// The lights info.
        /// </value>
        public List<ILight> LightsInfo
        {
            get { return lightInfo; }
            set { lightInfo = value; }
        }

        /// <summary>
        /// Gets or sets the model meshes info list.
        /// </summary>
        /// <value>
        /// The model meshes info.
        /// </value>
        public List<ObjectInformation> ModelMeshesInfo
        {
            get { return modelMeshesInfo; }
            set { modelMeshesInfo = value; }
        }
    }

    /// <summary>
    /// Specification for Classes that can Load a Model from a file/stream ...
    /// </summary>
    public interface IModelLoader
    {                
        /// <summary>
        /// Extract infos about models
        /// </summary>
        /// <param name="name">The name of the File - assume that models is in /Models, textures in /Textures and ModelInfos (when needed) in /ModelInfos</param>
        /// <returns></returns>
        ModelLoaderData Load(GraphicFactory factory, GraphicInfo info, String Name);
    }
}
