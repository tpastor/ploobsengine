using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Deferred final combination specification
    /// This is the stage number 3, where the light map and the gbuffer are combined
    /// </summary>
    public interface IDeferredFinalCombination
    {
        /// <summary>
        /// Sets the final combination.
        /// </summary>
        /// <param name="render">The render.</param>
        void SetFinalCombination(RenderHelper render);
        /// <summary>
        /// Draws the pass.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="world">The world.</param>
        /// <param name="gbuffer">The gbuffer.</param>
        /// <param name="lightmap">The lightmap.</param>
        /// <param name="render">The render.</param>
        void DrawScene(GameTime gameTime,IWorld world, IDeferredGBuffer gbuffer, IDeferredLightMap lightmap,RenderHelper render);
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, bool useFloatBuffer, bool saveToTexture);
        
        /// <summary>
        /// Gets or sets the ambient color factor.
        /// Uniform for all the scene
        /// </summary>
        /// <value>
        /// The color of the ambient.
        /// </value>
        Color AmbientColor { get; set; }

        /// <summary>
        /// Gets the <see cref="Microsoft.Xna.Framework.Graphics.Texture2D"/> with the specified type.
        /// </summary>
        Texture2D this[GBufferTypes type]
        {
            get;
        }        
        
    }
    
}
