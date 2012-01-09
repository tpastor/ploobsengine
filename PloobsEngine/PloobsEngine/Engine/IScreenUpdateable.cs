#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
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

        private long id;
        protected IScreen owner;

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
        public long GetId()
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
        public void SetId(long id)
        {
            this.id = id;
        }

        #endregion
    }
}
