using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a Render Technic
    /// </summary>
    public abstract class IRenderTechnic
    {
        /// <summary>
        /// Befores the first execution.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="world">The world.</param>
        protected virtual void BeforeFirstExecution(RenderHelper render, IWorld world) { }
        internal void iBeforeFirstExecution(RenderHelper render, IWorld world)
        {
            BeforeFirstExecution(render, world);
        }

        /// <summary>
        /// Executes the technic.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="world">The world.</param>
        protected abstract void ExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world);
        internal void iExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world)
        {
            ExecuteTechnic(gameTime, render, world);
        }

        /// <summary>
        /// Called after the All the Engine stuffs are loaded
        /// </summary>
        protected virtual void AfterLoadContent() { }
        internal void iAfterLoadContent()
        {
            AfterLoadContent();
        }
        /// <summary>
        /// Gets the name of the technic.
        /// </summary>
        /// <value>
        /// The name of the technic.
        /// </value>
        public abstract String TechnicName
        {
            get;
        }
        
    }
}
