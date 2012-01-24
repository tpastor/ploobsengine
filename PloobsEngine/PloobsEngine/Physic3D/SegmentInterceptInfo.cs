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
