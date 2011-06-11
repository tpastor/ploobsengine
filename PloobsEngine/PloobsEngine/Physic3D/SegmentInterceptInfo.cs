using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Physics
{
    /// <summary>
    /// Interception Info
    /// RayCast using PhysicEngine directly
    /// </summary>
    public sealed class SegmentInterceptInfo 
    {
        private IPhysicObject physicObject;

        /// <summary>
        /// Gets or sets the physic object itercepted.
        /// </summary>
        /// <value>
        /// The physic object.
        /// </value>
        public IPhysicObject PhysicObject
        {
            get { return physicObject; }
            set { physicObject = value; }
        }
        private float distance;

        /// <summary>
        /// Gets or sets the distance from the object to the ray source.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        private Vector3 impactPosition;

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
        private Vector3 impactNormal;

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
