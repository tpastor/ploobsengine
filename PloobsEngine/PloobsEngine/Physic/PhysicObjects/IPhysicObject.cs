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
    /// Specification of a physic Object
    /// </summary>
    public interface IPhysicObject : ISerializable
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        Vector3 Position { set; get; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        Vector3 Scale { set;  get; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        Matrix Rotation { set; get; }
        /// <summary>
        /// Vector pointing to the front
        /// </summary>
        Vector3 FaceVector { get; }
        /// <summary>
        /// Gets the world matrix.
        /// </summary>
        Matrix WorldMatrix {get; }
        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        Vector3 Velocity { set; get; }        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is motion less.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is motion less; otherwise, <c>false</c>.
        /// </value>
        bool isMotionLess { set; get; }
        /// <summary>
        /// Gets or sets the IObject owner.
        /// </summary>
        /// <value>
        /// The IObject owner.
        /// </value>
        IObject ObjectOwner { set; get; }
        /// <summary>
        /// Gets the physic object type.
        /// </summary>
        PhysicObjectTypes PhysicObjectTypes { get; }
        /// <summary>
        /// Enables this instance.
        /// </summary>
        void Enable();
        /// <summary>
        /// Disables this instance.
        /// </summary>
        void Disable();
        /// <summary>
        /// Applies an impulse.
        /// </summary>
        /// <param name="force">The force.</param>
        void ApplyImpulse(Vector3 force);
        /// <summary>
        /// Applies the torque.
        /// </summary>
        /// <param name="force">The force.</param>
        void ApplyTorque(Vector3 force);
        /// <summary>
        /// Gets the bounding box IN WORLD COORDINATES
        /// </summary>
        BoundingBox BoundingBox
        {
            get;
        }

    }

    /// <summary>
    /// Physic Types allowed
    /// </summary>
    public enum PhysicObjectTypes 
    {
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
