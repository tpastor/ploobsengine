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
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace PloobsEngine.Particles
{
    /// <summary>
    /// Abstract particle manager
    /// </summary>
    public abstract class IParticleManager
    {
        /// <summary>
        /// Gets the graphic factory.
        /// </summary>
        public GraphicFactory GraphicFactory
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the graphic info.
        /// </summary>
        public GraphicInfo GraphicInfo
        {
            get;
            internal set;
        }

        /// <summary>
        /// Adds the and initialize particle system.
        /// </summary>
        /// <param name="particleSystem">The particle system.</param>
        public abstract void AddAndInitializeParticleSystem(IParticleSystem particleSystem);
        /// <summary>
        /// Removes the particle system.
        /// </summary>
        /// <param name="particleSystem">The particle system.</param>
        public abstract void RemoveParticleSystem(IParticleSystem particleSystem);
        /// <summary>
        /// Gets the particle system.
        /// </summary>
        /// <param name="particleSystemName">Name of the particle system.</param>
        /// <returns></returns>
        public abstract IParticleSystem GetParticleSystem(String particleSystemName);

        protected abstract void Update3D(GameTime gt, Matrix view, Matrix Projection, Vector3 camPosition);
        internal void iUpdate3D(GameTime gt, Matrix view, Matrix Projection, Vector3 camPosition)
        {
            Update3D(gt,view,Projection,camPosition);
        }

        protected abstract void Update2D(GameTime gt, Matrix view, Matrix Projection);
        internal void iUpdate2D(GameTime gt, Matrix view, Matrix Projection)
        {
            Update2D(gt, view, Projection);
        }

        protected abstract void Draw(GameTime gt, Matrix view, Matrix Projection, RenderHelper render);
        internal void iDraw(GameTime gt, Matrix view, Matrix Projection, RenderHelper render)
        {
            Draw(gt,view,Projection,render);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IParticleManager"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public abstract bool Enabled
        {
            set;
            get;
        }

        internal void iCleanUp()
        {
            CleanUp();
        }
        protected virtual void CleanUp()
        {
        }


    }
}
