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
using PloobsEngine.MessageSystem;
using PloobsEngine.Events;

namespace PloobsEngine.Trigger
{
    /// <summary>
    /// Generic Event Handler
    /// </summary>
    public abstract class GenericEventHandler : IRecieveMessageEntity , IEventHandler
    {        
        #region IEntity Members

        private long _id;
        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public long GetId()
        {
            return _id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(long id)
        {
            this._id = id;
        }

        #endregion

        #region IRecieveMessageEntity Members

        public bool HandleThisMessageType(SenderType type)
        {
            return type == SenderType.EVENT;
        }

        public void HandleMessage(Message mes)
        {
            Process(mes);
        }

        #endregion

        #region IEventHandler Members

        /// <summary>
        /// Processes the specified mes.
        /// </summary>
        /// <param name="mes">The mes.</param>
        public void Process(Message mes)
        {

            HandleEvent(mes.Content as IEvent<Object>);
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="evt">The evt.</param>
        public abstract void HandleEvent(IEvent<Object> evt);
        #endregion
    }
}
