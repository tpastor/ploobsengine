using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Physics2D
{
    public abstract class I2DPhysicWorld
    {

        /// <summary>
        /// Updates 
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected abstract void Update(GameTime gt);
        internal void iUpdate(GameTime gt)
        {
            Update(gt);
        }

        /// <summary>
        /// Adds the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void AddObject(I2DPhysicObject obj);

        /// <summary>
        /// Removes the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public abstract void RemoveObject(I2DPhysicObject obj);

    }
}
