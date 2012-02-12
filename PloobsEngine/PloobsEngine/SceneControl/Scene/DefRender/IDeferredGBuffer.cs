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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// GBUffer generation specification
    /// </summary>
    public interface IDeferredGBuffer
    {
        /// <summary>
        /// Sets the G buffer.
        /// </summary>
        void SetGBuffer(RenderHelper render);
        /// <summary>
        /// Resolves the G buffer.
        /// </summary>
        void ResolveGBuffer(RenderHelper render);
        /// <summary>
        /// Clears the G buffer.
        /// </summary>
        void ClearGBuffer(RenderHelper render);

        /// <summary>
        /// Pre Draw the scene.
        /// Responsible to the PreDraw phase
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="world">The world.</param>
        /// <param name="render">The render.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="objectsToPreDraw">The objects to pre draw.</param>
        void PreDrawScene(GameTime gameTime, IWorld world, RenderHelper render, GraphicInfo ginfo, List<IObject> objectsToPreDraw);

        /// <summary>
        /// Draws.
        /// Generate the GBuffer
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="world">The world.</param>
        /// <param name="render">The render.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="objectsToPreDraw">The objects to pre draw.</param>
        void DrawScene(GameTime gameTime, IWorld world, RenderHelper render, GraphicInfo ginfo, List<IObject> objectsToPreDraw);
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="BackGroundColor">Color of the back ground.</param>
        void LoadContent(IContentManager manager, GraphicInfo ginfo, GraphicFactory factory,Color BackGroundColor);


        /// <summary>
        /// Return the generated Buffers
        /// </summary>
        Texture2D this[GBufferTypes type]
        {
            get;
        }
    }

        /// <summary>
        /// Buffer Types used
        /// </summary>
        public enum GBufferTypes
        {
            /// <summary>
            /// Depth
            /// </summary>
            DEPH,
            /// <summary>
            /// Color
            /// </summary>
            COLOR,
            /// <summary>
            /// Normal
            /// </summary>
            NORMAL,
            /// <summary>
            /// Glow
            /// </summary>
            Extra1,
            /// <summary>
            /// Final image
            /// </summary>
            FINALIMAGE            
        }
}
