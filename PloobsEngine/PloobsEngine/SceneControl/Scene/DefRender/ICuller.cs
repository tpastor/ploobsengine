using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Material;
using PloobsEngine.Cameras;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Culler Specification
    /// </summary>
    public interface ICuller
    {
        /// <summary>
        /// Get all the objects that pass the culling phase
        /// </summary>
        /// <param name="world"></param>
        /// <param name="Filter">Or Filter</param>
        /// <returns></returns>
        IEnumerable<IObject> GetNotCulledObjectsList(IWorld world, ICamera cam,MaterialType Filter);

        /// <summary>
        /// Called by the engine Once in the start of each rendering pass
        /// </summary>
        /// <param name="world"></param>
        void StartFrame(IWorld world, ICamera cam);

        /// <summary>
        /// Called when an object is added to the world
        /// </summary>
        /// <param name="obj">The obj.</param>
        void onObjectAdded(IObject obj);
        /// <summary>
        /// Called when an object s removed from the world
        /// </summary>
        /// <param name="obj">The obj.</param>
        void onObjectRemoved(IObject obj);

        /// <summary>
        /// Gets the number of objects rendered in this frame.
        /// </summary>
        int RenderedObjectThisFrame
        {
            get;
        }
        
    }
}
