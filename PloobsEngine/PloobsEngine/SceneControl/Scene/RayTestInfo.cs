using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Object return after a RayCast is performed
    /// </summary>
    public class RayTestInfo
    {
        private IObject _object = null;

        /// <summary>
        /// Gets or sets the object hitted.
        /// </summary>
        /// <value>
        /// The object.
        /// </value>
        public IObject Object
        {
            get { return _object; }
            set { _object = value; }
        }
        private float distance = 0;

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        private Vector3 impactPosition = Vector3.Zero;

        /// <summary>
        /// Gets or sets the impact position.
        /// </summary>
        /// <value>
        /// The impact position.
        /// </value>
        public Vector3 ImpactPosition
        {
            get { return impactPosition; }
            set { impactPosition = value; }
        }
        private Vector3 impactNormal = Vector3.Zero;

        /// <summary>
        /// Gets or sets the impact normal.
        /// </summary>
        /// <value>
        /// The impact normal.
        /// </value>
        public Vector3 ImpactNormal
        {
            get { return impactNormal; }
            set { impactNormal = value; }
        }
    }
}
