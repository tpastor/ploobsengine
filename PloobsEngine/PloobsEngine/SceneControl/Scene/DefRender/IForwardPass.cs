using System;

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
        void Draw(Microsoft.Xna.Framework.GameTime gt, IWorld world, RenderHelper render);
    }
}
