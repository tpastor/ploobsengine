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
using PloobsEngine.Light;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using PloobsEngine.Physics;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Loader
{

    /// <summary>
    /// Struct that represents a Model    
    /// </summary>
    public struct ObjectInformation
    {
        public string modelName;
        public int meshIndex;
        public int meshPartIndex;
        public BatchInformation batchInformation;
        public TextureInformation textureInformation;
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation;
        public float mass;
        public float dinamicfriction;
        public float staticfriction;
        public float ellasticity;
        public String collisionType;
        
        //store extra information
        public Dictionary<string, object> extra;

        /// <summary>
        /// Determines whether this model has the specified texture type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type has texture; otherwise, <c>false</c>.
        /// </returns>
        public bool HasTexture(TextureType type)
        {

            if (type == TextureType.ENVIRONMENT)
            {
                return textureInformation.getCubeTexture(TextureType.ENVIRONMENT) != null;
            }
            if (type == TextureType.DIFFUSE)
            {
                return textureInformation.getTexture(TextureType.DIFFUSE) != null;
            }
            else if (type == TextureType.BUMP)
            {
                return textureInformation.getTexture(TextureType.BUMP) != null;
            }
            else if (type == TextureType.SPECULAR)
            {
                return textureInformation.getTexture(TextureType.SPECULAR) != null;
            }
            else if (type == TextureType.GLOW)
            {
                return textureInformation.getTexture(TextureType.GLOW) != null;
            }
            else if (type == TextureType.PARALAX)
            {
                return textureInformation.getTexture(TextureType.PARALAX) != null;
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

    public struct ParticleInfo
    {
        public string Name;
        public Vector3 Position;
        public Quaternion Orientation;
        public string Type;    
    }


    /// <summary>
    /// Dummy Info
    /// </summary>
    public struct DummyInfo
    {
        public String Name;
        public Vector3 Position;
    }

    /// <summary>
    /// Constraint info
    /// </summary>
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
        List<ParticleInfo> particleInfo = new List<ParticleInfo>();

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


        /// <summary>
        /// Gets or sets the particle info.
        /// </summary>
        /// <value>
        /// The particle info.
        /// </value>
        public List<ParticleInfo> ParticleInfo
        {
            get { return particleInfo; }
            set { particleInfo = value; }
        }

        /// <summary>
        /// Gets or sets the constraint info.
        /// </summary>
        /// <value>
        /// The constraint info.
        /// </value>
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
    public interface IModelLoader : ICleanupAble
    {
        /// <summary>
        /// Extract infos about models
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="info">The info.</param>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        ModelLoaderData Load(GraphicFactory factory, GraphicInfo info, String Name);
    }
}
