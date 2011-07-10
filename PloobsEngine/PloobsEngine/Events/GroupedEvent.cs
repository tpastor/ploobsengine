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

namespace PloobsEngine.Events
{
    /// <summary>
    /// Evento que tem como alvo um grupo , nao precisa adicionar targets
    /// </summary>
    public class GroupedEvent<T> : IEvent<T>
    {
        public GroupedEvent(String code , String group)
        {
            this.group = group;
            this.Code = code;
        }

        private string group;        

        #region IEvent<T> Members

        public void FireEvent(T data)
        {
            Message me = new Message(PrincipalConstants.EventSenderId, PrincipalConstants.InvalidId, group, Priority.LOW, 0, SenderType.EVENT, data, this.Code);
            MessageDeliver.SendMessage(me);
        }

        public string Code
        {
            get;
            set;
        }

        #endregion
    }
}
