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
using System.Xml;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using System.Runtime.Serialization;

namespace PloobsEngine.Physics
{
    /// <summary>
    /// 3D Physic Material Description
    /// </summary>
    public struct MaterialDescription
    {
        public MaterialDescription(float StaticFriction, float DinamicFriction, float Bounciness)
        {
            this.StaticFriction = StaticFriction;
            this.DinamicFriction = DinamicFriction;
            this.Bounciness = Bounciness;
        }

        /// <summary>
        /// Defaults the bepu material.
        /// </summary>
        /// <returns></returns>
        public static MaterialDescription DefaultBepuMaterial()
        {
            MaterialDescription md = new MaterialDescription(BEPUphysics.Materials.MaterialManager.DefaultStaticFriction, BEPUphysics.Materials.MaterialManager.DefaultKineticFriction, BEPUphysics.Materials.MaterialManager.DefaultBounciness);
            return md;           
        }

        /// <summary>
        /// Static Friction
        /// </summary>
        public float StaticFriction;
        /// <summary>
        /// Dynamic Friction
        /// </summary>
        public float DinamicFriction;
        /// <summary>
        /// Bounciness
        /// </summary>
        public float Bounciness;
    };

    /// <summary>
    /// Specification of a physic Object
    /// </summary>
    #if WINDOWS
    public abstract class IPhysicObject : ISerializable
#else
    public abstract class IPhysicObject 
#endif
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public abstract Vector3 Position { set; get; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public abstract Vector3 Scale { set;  get; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public abstract Matrix Rotation { set; get; }
        /// <summary>
        /// Vector pointing to the front
        /// </summary>
        public abstract Vector3 FaceVector { get; }
        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        public abstract Matrix WorldMatrix { get; }
        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public abstract Vector3 Velocity { set; get; }        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is motion less.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is motion less; otherwise, <c>false</c>.
        /// </value>
        public abstract bool isMotionLess { set; get; }
        /// <summary>
        /// Gets or sets the IObject owner.
        /// </summary>
        /// <value>
        /// The IObject owner.
        /// </value>
        public abstract IObject ObjectOwner { set; get; }
        /// <summary>
        /// Gets the physic object type.
        /// </summary>
        public abstract PhysicObjectTypes PhysicObjectTypes { get; }
        /// <summary>
        /// Applies an impulse.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="force">The force.</param>
        public abstract void ApplyImpulse(Vector3 position, Vector3 force);        
        /// <summary>
        /// Gets the bounding box IN WORLD COORDINATES
        /// </summary>
        public abstract BoundingBox? BoundingBox
        {
            get;
        }

        /// <summary>
        /// Gets or sets the angular velocity.
        /// </summary>
        /// <value>
        /// The angular velocity.
        /// </value>
        public abstract Vector3 AngularVelocity { get; set; }

        #region ISerializable Members

#if WINDOWS

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
#endif


        #endregion
    }

    /// <summary>
    /// Physic Types allowed
    /// </summary>
    public enum PhysicObjectTypes 
    {
        /// <summary>
        /// Mobile Physic Obj
        /// </summary>
        MobilePhysicObject,

        /// <summary>
        /// InstancedTriangleMeshObject
        /// </summary>
        InstancedTriangleMeshObject,
        /// <summary>
        /// Terrain
        /// </summary>
        TERRAIN,
        /// <summary>
        /// Detector object like a trigger, dont has physic part
        /// </summary>
        DETECTOROBJECT,
        /// <summary>
        /// Compound (lots of shapes)
        /// </summary>
        COMPOUNDOBJECT,
        /// <summary>
        /// Cilinder
        /// </summary>
        CYLINDEROBJECT,
        /// <summary>
        /// Box
        /// </summary>
        BOXOBJECT,
        /// <summary>
        /// Car
        /// </summary>
        CAROBJECT,
        /// <summary>
        /// Capsule
        /// </summary>
        CAPSULEOBJECT,
        /// <summary>
        /// Character
        /// </summary>
        CHARACTEROBJECT,
        /// <summary>
        /// Plane
        /// </summary>
        PLANEOBJECT,
        /// <summary>
        /// Sphere
        /// </summary>
        SPHEREOBJECT,
        /// <summary>
        /// Triangle Mesh
        /// </summary>
        TRIANGLEMESHOBJECT,
        /// <summary>
        /// HeightMap
        /// </summary>
        HEIGHMAPOBJECT,
        /// <summary>
        /// Not added to physicWorld,
        /// cant colide, cant be detected by raycast or triggers
        /// </summary>
        GHOST,
        /// <summary>
        /// Object that moves kinematicaly
        /// </summary>
        SPECIALIZEDMOVER,
        /// <summary>
        /// None of those before
        /// </summary>
        OTHER,
        /// <summary>
        /// Should not be used, here for compatibility
        /// </summary>
        NONE
    }
    
}
