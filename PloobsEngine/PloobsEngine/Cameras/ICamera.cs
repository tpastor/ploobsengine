using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;
using PloobsEngine.SceneControl;
using System.Runtime.Serialization;

namespace PloobsEngine.Cameras
{
    /// <summary>
    /// Specification of a camera
    /// </summary>
    public interface ICamera : IEntity, IRecieveMessageEntity, ISerializable
    {
        
        /// <summary>
        /// Gets a value indicating whether this <see cref="ICamera"/> has moved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hasmoved; otherwise, <c>false</c>.
        /// </value>
        bool Hasmoved { get; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        Vector3 Position { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        Vector3 Target { get; set; }
        /// <summary>
        /// Gets or sets up vector.
        /// </summary>
        /// <value>
        /// Up.
        /// </value>
        Vector3 Up { get; set; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        Quaternion Rotation { get; set; }
        /// <summary>
        /// Gets or sets the field of view.
        /// </summary>
        /// <value>
        /// The field of view.
        /// </value>
        float FieldOfView { get; set; }
        /// <summary>
        /// Gets or sets the aspect ratio.
        /// </summary>
        /// <value>
        /// The aspect ratio.
        /// </value>
        float AspectRatio { get; set; }
        /// <summary>
        /// Gets or sets the near plane.
        /// </summary>
        /// <value>
        /// The near plane.
        /// </value>
        float NearPlane { get; set; }
        /// <summary>
        /// Gets or sets the far plane.
        /// </summary>
        /// <value>
        /// The far plane.
        /// </value>
        float FarPlane { get; set; }
        /// <summary>
        /// Gets the view.
        /// </summary>
        Matrix View { get; }
        /// <summary>
        /// Gets the projection.
        /// </summary>
        Matrix Projection { get; }
        /// <summary>
        /// Gets the bounding frustum.
        /// </summary>
        BoundingFrustum BoundingFrustum { get; }
        /// <summary>
        /// Gets the bounding box.
        /// </summary>
        BoundingBox BoundingBox   { get; }
        /// <summary>
        /// Updates .
        /// </summary>
        /// <param name="gt">The gt.</param>
        void Update(GameTime gt);
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        String Name { get; set; }

   }

        
}

