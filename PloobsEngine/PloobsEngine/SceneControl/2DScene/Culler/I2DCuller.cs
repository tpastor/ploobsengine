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

namespace PloobsEngine.SceneControl._2DScene.Culler
{
    public abstract class I2DCuller
    {
        internal I2DWorld world = null;

        /// <summary>
        /// Get all the objects that pass the culling phase
        /// </summary>
        /// <param name="Filter">Or Filter, null for all</param>
        /// <returns></returns>
        public abstract Dictionary<Type, List<I2DObject>> GetNotCulledObjectsList();

        /// <summary>
        /// Called by the engine Once in the start of each rendering pass
        /// </summary>
        /// <param name="world"></param>
        public abstract void StartFrame(Matrix view, Matrix projection, BoundingFrustum frustrum);

        /// <summary>
        /// Called when an object is added to the world
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void onObjectAdded(I2DObject obj);
        /// <summary>
        /// Called when an object s removed from the world
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void onObjectRemoved(I2DObject obj);

        /// <summary>
        /// Gets the number of objects rendered in this frame.
        /// </summary>
        public abstract int RenderedObjectThisFrame
        {
            get;
        }
    

    }
}
