using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Implemented by those objects that wants to be cleaned when the screen where it is attached is gone
    /// Call Screen.AttachCleanUpAble to attach this to a screen
    /// </summary>
    public interface ICleanupAble
    {
        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="factory">The factory.</param>
        void CleanUp(GraphicFactory factory);
    }
}
