using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a Render Technic
    /// </summary>
    public interface IRenderTechnic
    {
        /// <summary>
        /// Initializations the specified render.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="world">The world.</param>
        /// <param name="engine">The engine.</param>
        void Initialization(IRenderHelper render,IWorld world);
        /// <summary>
        /// Executes the technic.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="world">The world.</param>
        void ExecuteTechnic(IRenderHelper render, IWorld world);

        /// <summary>
        /// Called after the All the Engine stuffs are loaded
        /// </summary>
        void AfterLoadContent();
        /// <summary>
        /// Gets the name of the technic.
        /// </summary>
        /// <value>
        /// The name of the technic.
        /// </value>
        String TechnicName
        {
            get;
        }
    }
}
