using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Entity;
using PloobsEngine.SceneControl;

namespace PloobsEngine
{
    /// <summary>
    /// Updateable component
    /// Can be atached to a screen, and everytime the screens's update is called
    /// this class update is also called
    /// has an id
    /// </summary>
    public class IScreenUpdateable : IEntity
    {

        private int id;
        private IScreen owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="IScreenUpdateable"/> class.
        /// </summary>
        /// <param name="owner">The screen owner.</param>
        public IScreenUpdateable(IScreen owner)
        {
            this.owner = owner;
            EntityMapper.getInstance().AddEntity(this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="IScreenUpdateable"/> is reclaimed by garbage collection.
        /// </summary>
        ~IScreenUpdateable()
        {
            EntityMapper.getInstance().RemoveEntity(this);
        }

        /// <summary>
        /// Starts this instance.
        /// The update will be called after this call
        /// </summary>
        public void Start()
        {
            owner.AddScreenUpdateable(this);
        }
        /// <summary>
        /// Stops the updating method being called
        /// </summary>
        public void Stop()
        {
            owner.RemoveScreenUpdateable(this);
        }

        /// <summary>
        /// Updates this called
        /// Should be overloaded
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        #region IEntity Members

        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public int getId()
        {
            return this.id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void setId(int id)
        {
            this.id = id;
        }

        #endregion
    }
}
