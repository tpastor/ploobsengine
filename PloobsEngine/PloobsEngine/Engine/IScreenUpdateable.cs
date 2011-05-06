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
    public abstract class IScreenUpdateable : IEntity
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
            Enabled = false;
        }        

        /// <summary>
        /// Starts this instance.
        /// The update will be called after this call
        /// </summary>
        public void Start()
        {
            owner.AddScreenUpdateable(this);
            Enabled = true;
        }
        /// <summary>
        /// Stops the updating method being called
        /// </summary>
        public void Stop()
        {
            owner.RemoveScreenUpdateable(this);
            Enabled = false;
        }

        public bool Enabled
        {
            get;
            internal set;
        }

        /// <summary>
        /// Updates this called
        /// Should be overloaded
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected abstract void Update(GameTime gameTime);
        internal void iUpdate(GameTime gameTime)
        {
            Update(gameTime);
        }

        #region IEntity Members

        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public int GetId()
        {
            return this.id;
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        protected virtual void CleanUp() 
        {
            EntityMapper.getInstance().RemoveEntity(this);
        }
        internal void iCleanUp()
        {
            CleanUp();
        }
        

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(int id)
        {
            this.id = id;
        }

        #endregion
    }
}
