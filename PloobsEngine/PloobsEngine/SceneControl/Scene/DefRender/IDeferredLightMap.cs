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
        void DrawLights(GameTime gameTime,IWorld world, IDeferredGBuffer deferredGBuffer,RenderHelper render);
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="useFloatBuffer">if set to <c>true</c> [use float buffer].</param>
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
