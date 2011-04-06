using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Material;
using PloobsEngine.Cameras;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Culler Specification
    /// </summary>
    public abstract class ICuller
    {
        internal IWorld world = null;

        /// <summary>
        /// Get all the objects that pass the culling phase
        /// </summary>
        /// <param name="world"></param>
        /// <param name="Filter">Or Filter</param>
        /// <returns></returns>
        public abstract IEnumerable<IObject> GetNotCulledObjectsList(ICamera cam, MaterialType Filter);

        /// <summary>
        /// Called by the engine Once in the start of each rendering pass
        /// </summary>
        /// <param name="world"></param>
        public abstract void StartFrame(Matrix view, Matrix projection,BoundingFrustum frustrum);

        /// <summary>
        /// Called when an object is added to the world
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void onObjectAdded(IObject obj);
        /// <summary>
        /// Called when an object s removed from the world
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void onObjectRemoved(IObject obj);

        /// <summary>
        /// Gets the number of objects rendered in this frame.
        /// </summary>
        public abstract int RenderedObjectThisFrame
        {
            get;
        }
        
    }
}
