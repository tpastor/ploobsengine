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
using Microsoft.Xna.Framework;
namespace PloobsEngine.SceneControl._2DScene
{
    /// <summary>
    /// 2D camera specification
    /// </summary>
    public interface ICamera2D
    {
        /// <summary>
        /// Converts the screen to world coordinates.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="useSimulatedProjection">if set to <c>true</c> [use simulated projection].</param>
        /// <param name="returnInDisplayUnits">if set to <c>true</c> [return in display units].</param>
        /// <returns></returns>
        Vector2 ConvertScreenToWorld(Vector2 location, bool useSimulatedProjection = true, bool returnInDisplayUnits = true);
        /// <summary>
        /// Converts the world to screen coordinates.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="useSimulatedProjection">if set to <c>true</c> [use simulated projection].</param>
        /// <param name="convertToSimUnits">if set to <c>true</c> [convert to sim units].</param>
        /// <returns></returns>
        Vector2 ConvertWorldToScreen(Vector2 location, bool useSimulatedProjection = true, bool convertToSimUnits = true);

        /// <summary>
        /// Moves the camera.
        /// </summary>
        /// <param name="amount">The amount.</param>
        void MoveCamera(Microsoft.Xna.Framework.Vector2 amount);
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        Microsoft.Xna.Framework.Vector2 Position { get; set; }
        /// <summary>
        /// Rotates the camera.
        /// </summary>
        /// <param name="amount">The amount.</param>
        void RotateCamera(float amount);
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        float Rotation { get; set; }
        /// <summary>
        /// Gets the sim projection.
        /// </summary>
        Microsoft.Xna.Framework.Matrix SimProjection { get; }
        /// <summary>
        /// Gets the projection.
        /// </summary>
        Microsoft.Xna.Framework.Matrix Projection { get; }
        /// <summary>
        /// Gets the sim view.
        /// </summary>
        Microsoft.Xna.Framework.Matrix SimView { get; }
        /// <summary>
        /// Updates
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Update(Microsoft.Xna.Framework.GameTime gameTime);
        /// <summary>
        /// Gets the view.
        /// </summary>
        Microsoft.Xna.Framework.Matrix View { get; }
        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>
        /// The zoom.
        /// </value>
        float Zoom { get; set; }

        /// <summary>
        /// Piece of the screen seen by the camera
        /// </summary>
        Rectangle ScreenPortion
        {
            get;
        }

        /// <summary>
        /// Gets the bounding frustrum.
        /// </summary>
        BoundingFrustum BoundingFrustrum
        {
            get;
        }

        /// <summary>
        /// Called when the attached screen is removed
        /// If this camera is not attached to a screen, this method is not called =P
        /// </summary>
        void CleanUp();
    }
}
