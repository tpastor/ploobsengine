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
using PloobsEngine.SceneControl;
using PloobsEngine.SceneControl._2DScene;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Material2D
{
    /// <summary>
    /// Interface for material processor
    /// Each material must have a processor
    /// Processors coordinate how the materials should be draw
    /// </summary>
    public interface IMaterialProcessor
    {
        /// <summary>
        /// Processes the light draw.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="render">The render.</param>
        /// <param name="camera">The camera.</param>
        /// <param name="objects">The objects.</param>
        /// <param name="color">The color.</param>
        /// <param name="light">The light.</param>
        void ProcessLightDraw(GameTime gameTime, RenderHelper render, ICamera2D camera, List<I2DObject> objects,Color color,PloobsEngine.Light2D.Light2D light);
        /// <summary>
        /// Processes the draw.
        /// </summary>
        /// <param name="uselayer">if set to <c>true</c> [uselayer].</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="render">The render.</param>
        /// <param name="camera">The camera.</param>
        /// <param name="objects">The objects.</param>
        void ProcessDraw(bool uselayer, GameTime gameTime, RenderHelper render, ICamera2D camera, List<I2DObject> objects);
        /// <summary>
        /// Processes the pre draw.
        /// </summary>
        /// <param name="uselayer">if set to <c>true</c> [uselayer].</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="render">The render.</param>
        /// <param name="camera">The camera.</param>
        /// <param name="world">The world.</param>
        /// <param name="objects">The objects.</param>
        void ProcessPreDraw(bool uselayer, GameTime gameTime, RenderHelper render, ICamera2D camera, I2DWorld world, List<I2DObject> objects);
    }
}
