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
    public abstract class ICamera : ISerializable
    {

        /// <summary>
        /// Gets a value indicating whether this <see cref="ICamera"/> has moved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hasmoved; otherwise, <c>false</c>.
        /// </value>
        public abstract bool Hasmoved { get; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public virtual Vector3 Position { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public virtual Vector3 Target { get; set; }
        /// <summary>
        /// Gets or sets up vector.
        /// </summary>
        /// <value>
        /// Up.
        /// </value>
        public virtual Vector3 Up { get; set; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public virtual Quaternion Rotation { get; set; }
        /// <summary>
        /// Gets or sets the field of view.
        /// </summary>
        /// <value>
        /// The field of view.
        /// </value>
        public virtual float FieldOfView { get; set; }
        /// <summary>
        /// Gets or sets the aspect ratio.
        /// </summary>
        /// <value>
        /// The aspect ratio.
        /// </value>
        public virtual float AspectRatio { get; set; }
        /// <summary>
        /// Gets or sets the near plane.
        /// </summary>
        /// <value>
        /// The near plane.
        /// </value>
        public virtual float NearPlane { get; set; }
        /// <summary>
        /// Gets or sets the far plane.
        /// </summary>
        /// <value>
        /// The far plane.
        /// </value>
        public virtual float FarPlane { get; set; }
        /// <summary>
        /// Gets the view.
        /// </summary>
        public abstract Matrix View { get; }
        /// <summary>
        /// Gets the projection.
        /// </summary>
        public abstract Matrix Projection { get; }
        /// <summary>
        /// Gets the view projection.
        /// </summary>
        public abstract Matrix ViewProjection { get; }
        /// <summary>
        /// Gets the bounding frustum.
        /// </summary>
        public abstract BoundingFrustum BoundingFrustum { get; }
        /// <summary>
        /// Updates .
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected abstract void Update(GameTime gt);
        internal void iUpdate(GameTime gt)
        {
            Update(gt);
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }


        #region ISerializable Members

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);

        #endregion

    }
        
}

