using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Entity;

namespace PloobsEngine.MessageSystem
{
    /// <summary>
    /// Entity that can recieve a message
    /// </summary>
    public interface IRecieveMessageEntity : IEntity
    {
        /// <summary>
        /// Handles a message from determined sender type.
        /// </summary>
        /// <param name="type">Sender type.</param>
        /// <returns></returns>
        bool HandleThisMessageType(SenderType type);

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="mes">The mes.</param>
        void HandleMessage(Message mes);       
        
    }
}
