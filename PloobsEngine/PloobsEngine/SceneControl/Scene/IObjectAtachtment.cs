using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    /// <summary>    
    /// You can bind one IObjectAtachtment to one IObject
    /// The IObjectAtachtment method Update will be called everytime 
    /// the method Update of the object is called
    /// </summary>
    public abstract class IObjectAtachtment
    {
        /// <summary>
        /// Updates the atachment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="gt">The gt.</param>
        protected abstract void Update(IObject obj, GameTime gt);
        internal void IUpdate(IObject obj, GameTime gt)
        {
            Update(obj, gt);
        }
    }
}
