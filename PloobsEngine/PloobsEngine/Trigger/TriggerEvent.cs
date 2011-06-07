using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.MessageSystem;
using PloobsEngine.Events;

namespace PloobsEngine.Trigger
{
    /// <summary>
    /// Special Event used by triggers
    /// </summary>
    public class TriggerEvent : IEvent<ITrigger>
    {
        String groupName;
        String triggerName;

        /// <summary>
        /// Gets or sets the name of the trigger.
        /// </summary>
        /// <value>
        /// The name of the trigger.
        /// </value>
        public String TriggerName
        {
            get { return TriggerName; }
            set { TriggerName = value; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerEvent"/> class.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="TriggerName">Name of the trigger.</param>
        public TriggerEvent(String groupName,String TriggerName)
        {
            this.groupName = groupName;
            this.triggerName = TriggerName;
        }

        #region IEvent Members

        /// <summary>
        /// Fires the event.
        /// </summary>
        /// <param name="trigger">The trigger.</param>
        public void FireEvent(ITrigger trigger)
        {
            Message me = new Message(PrincipalConstants.EventSenderId, -1, groupName, Priority.LOW, 0, SenderType.EVENT, trigger, this.Code);
            MessageDeliver.SendMessage(me);           
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code
        {
            get;
            set;
        }

        #endregion        
    }
}
