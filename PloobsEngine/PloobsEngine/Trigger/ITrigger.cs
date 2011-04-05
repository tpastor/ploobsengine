using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using PloobsEngine.Events;


namespace PloobsEngine.Trigger
{
    /// <summary>
    /// Trigger Specification
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// Gets or sets the event fired when the trigger is activated.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        IEvent<ITrigger> Event
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the name of the trigger.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        String Name
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets the ghost object that gives the SHAPE of the trigger.
        /// </summary>
        /// <value>
        /// The ghost object.
        /// </value>
        IPhysicObject GhostObject
        {
            set;
            get;
        }


    }
}
