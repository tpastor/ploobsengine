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
using PloobsEngine.Entity;

namespace PloobsEngine.Events
{
    /// <summary>
    /// Evento Simples com alvos simples
    /// </summary>
    public class SimpleEvent : IEvent<object>
    {
        public SimpleEvent(String code)
        {
            this.code = code;
        }
        protected string code;
        protected object _source;

        protected IList<IEntity> _targets = new List<IEntity>();

        #region IEvent Members

        public virtual void FireEvent(object source)
        {
            this._source = source;
            foreach (IEntity item in _targets)
            {
                Message me = new Message(PrincipalConstants.EventSenderId, item.GetId(), null, Priority.LOW, 0, SenderType.EVENT, this, this.Code);
                MessageDeliver.SendMessage(me);
            }
        }

        public void AddTarget(IRecieveMessageEntity entity)
        {
            _targets.Add(entity);
        }

        public void RemoveTarget(IRecieveMessageEntity entity)
        {
            _targets.Remove(entity);
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        #endregion

        #region IEvent Members


        public object GetEventSource()
        {
            return _source;
        }

        #endregion

    }
}
