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

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of the ForwardPass pass
    /// </summary>
    public interface IForwardPass
    {
        /// <summary>
        /// Draws all the forward Objects 
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="world">The world.</param>
        /// <param name="render">The render.</param>
        void Draw(Microsoft.Xna.Framework.GameTime gt, IWorld world, RenderHelper render, List<IObject> deferred, List<IObject> forward);
    }
}
