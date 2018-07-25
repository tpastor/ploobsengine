#if !MONO && !MONODX
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
using DPSF;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;

namespace PloobsEngine.Particles
{
    /// <summary>
    /// DPSF particle system implementation
    /// </summary>
    public class DPFSParticleSystem : IParticleSystem
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="DPFSParticleSystem"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="IDPSFParticleSystem">The IDPSF particle system.</param>
        public DPFSParticleSystem(String name, IDPSFParticleSystem IDPSFParticleSystem)
            : base(name)
        {
            if (IDPSFParticleSystem == null)
            {
                ActiveLogger.LogMessage("IDPSFParticleSystem cannot be null", LogLevel.FatalError);
                throw new Exception("IDPSFParticleSystem cannot be null");
            }
            this.IDPSFParticleSystem = IDPSFParticleSystem;
        }

        /// <summary>
        /// Gets or sets the IDPSF particle system.
        /// </summary>
        /// <value>
        /// The IDPSF particle system.
        /// </value>
        public IDPSFParticleSystem IDPSFParticleSystem
        {
            get;
            set;
        }        
    }
}
#endif