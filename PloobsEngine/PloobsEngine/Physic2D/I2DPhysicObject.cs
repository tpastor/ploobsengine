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
using PloobsEngine.SceneControl._2DScene;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Physic2D
{
    /// <summary>
    /// Physic object type
    /// </summary>
    public enum Physic2DType
    {
        /// <summary>
        /// Do not exists in physic world (no collision detection, no ray intersections ......)
        /// </summary>
        Ghost,
        /// <summary>
        /// Real physic object
        /// </summary>
        Physic
    }

    /// <summary>
    /// 2D physic object specification
    /// </summary>
    public abstract class I2DPhysicObject
    {       
        Vector2 origin;

        /// <summary>
        /// Gets the type of the physic2D.
        /// </summary>
        /// <value>
        /// The type of the physic2 D.
        /// </value>
        public abstract Physic2DType Physic2DType
        {
            get;            
        }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }


        /// <summary>
        /// Gets the owner.
        /// </summary>
        public I2DObject Owner
        {
            internal set;
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is dynamic.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is dynamic; otherwise, <c>false</c>.
        /// </value>
        public virtual bool isDynamic
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public virtual Vector2 Position
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets the linear velocity.
        /// </summary>
        /// <value>
        /// The linear velocity.
        /// </value>
        public virtual Vector2 LinearVelocity
        {
            set;
            get;
        }
        /// <summary>
        /// Gets or sets the angular velocity.
        /// </summary>
        /// <value>
        /// The angular velocity.
        /// </value>
        public virtual float AngularVelocity
        {
            set;
            get;

        }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public virtual float Rotation
        {
            set;
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="I2DPhysicObject"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Enabled
        {
            internal set;
            get;
        }
        
        private Vector2 pos;
        private float rot;
        internal bool HasMoved()
        {
            if (!isDynamic)
                return false;

            if (Position != pos || Rotation != rot)
            {
                pos = Position;
                rot = Rotation;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Applies the force.
        /// </summary>
        /// <param name="force">The force.</param>
        /// <param name="point">The point.</param>
        public abstract void ApplyForce(Vector2 force, Vector2? point = null);
        
    
    }
}
