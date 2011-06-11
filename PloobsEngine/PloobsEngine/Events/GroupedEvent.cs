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
