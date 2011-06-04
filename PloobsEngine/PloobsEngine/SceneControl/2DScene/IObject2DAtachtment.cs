using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl._2DScene
{
    public abstract class IObject2DAtachtment
    {
        /// <summary>
        /// Updates the atachment.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="gt">The gt.</param>
        protected abstract void Update(I2DObject obj, GameTime gt);
        internal void IUpdate(I2DObject obj, GameTime gt)
        {
            Update(obj, gt);
        }
    }
}
