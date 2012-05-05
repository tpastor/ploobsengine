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
    /// Deferred LightMap generation specification
    /// </summary>
    public interface IDeferredLightMap 
    {
        /// <summary>
        /// Sets the light map.
        /// </summary>
        void SetLightMap(RenderHelper render);
        /// <summary>
        /// Resolves the light map.
        /// </summary>
        void ResolveLightMap(RenderHelper render);
        /// <summary>
        /// Draws the lights.
        /// Perform the light pass
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="world">The world.</param>
        /// <param name="deferredGBuffer">The deferred G buffer.</param>
        /// <param name="render">The render.</param>
        void DrawLights(GameTime gameTime,IWorld world, IDeferredGBuffer deferredGBuffer,RenderHelper render);
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="cullPointLight">if set to <c>true</c> [cull point light].</param>
        /// <param name="useFloatingBufferForLightning">if set to <c>true</c> [use floating buffer for lightning].</param>
        void LoadContent(IContentManager manager, GraphicInfo ginfo, GraphicFactory factory,bool cullPointLight, bool useFloatingBufferForLightning );
        /// <summary>
        /// Get the generated Images by this pass
        /// </summary>
        Texture2D this[DeferredLightMapType type]
        {
            get;            
        }

        
    }

    /// <summary>
    /// Generated Buffers by the lightmap pass
    /// </summary>
    public enum DeferredLightMapType
    {
        /// <summary>
        /// LightMap
        /// </summary>
        LIGHTMAP
    }
}
