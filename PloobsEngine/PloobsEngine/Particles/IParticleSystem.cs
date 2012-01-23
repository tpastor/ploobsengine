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
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;

namespace PloobsEngine.Particles
{
    /// <summary>
    /// Particle system specification
    /// </summary>
    public abstract class IParticleSystem
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="IParticleSystem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public IParticleSystem(String name, BoundingBox? BoundingBox = null)
        {
            this.BoundingBox = BoundingBox;
            if (String.IsNullOrEmpty(name))
            {
                ActiveLogger.LogMessage("ParticleSystem name cannot be null/empty", LogLevel.FatalError);
                throw new Exception("ParticleSystem name cannot be null/empty");
            }
            
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name
        {
            get;
            set;
        }

        public BoundingBox? BoundingBox
        {
            get;
            set;
        }
    }
}
