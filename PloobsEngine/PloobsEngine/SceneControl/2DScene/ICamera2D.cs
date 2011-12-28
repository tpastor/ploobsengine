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
    public interface ICamera2D
    {
        Vector2 ConvertScreenToWorld(Vector2 location, bool useSimulatedProjection = true, bool returnInDisplayUnits = true);
        Vector2 ConvertWorldToScreen(Vector2 location, bool useSimulatedProjection = true, bool convertToSimUnits = true);
        
        void MoveCamera(Microsoft.Xna.Framework.Vector2 amount);
        Microsoft.Xna.Framework.Vector2 Position { get; set; }
        void RotateCamera(float amount);
        float Rotation { get; set; }
        Microsoft.Xna.Framework.Matrix SimProjection { get; }
        Microsoft.Xna.Framework.Matrix Projection { get; }
        Microsoft.Xna.Framework.Matrix SimView { get; }
        void Update(Microsoft.Xna.Framework.GameTime gameTime);
        Microsoft.Xna.Framework.Matrix View { get; }
        float Zoom { get; set; }

        /// <summary>
        /// Piece of the screen seen by the camera
        /// </summary>
        Rectangle ScreenPortion
        {
            get;
        }

        BoundingFrustum BoundingFrustrum
        {
            get;
        }
    }
}
