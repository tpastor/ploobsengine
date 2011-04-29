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
        void PreDrawScene(GameTime gameTime, IWorld world, RenderHelper render);

        /// <summary>
        /// Draws.
        /// Generate the GBuffer
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="world">The world.</param>
        void DrawScene(GameTime gameTime, IWorld world, RenderHelper render);
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
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
