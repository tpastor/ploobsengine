using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Lidgren.Network;
using System.Diagnostics;

namespace PloobsEngine.NetWorking
{
    public class NetWorkServerObject
    {
        public NetWorkServerObject(String Identifier, Func<NetIncomingMessage, ServerIObject> CreateLoadObjectAndRedistributeOrder, Func<ServerIObject, NetIncomingMessage, NetOutgoingMessage, NetOutgoingMessage> CreateRedistributeOrder)
        {
            this.identifier = Identifier;
            this.CreateLoadObjectOrder = CreateLoadObjectAndRedistributeOrder;
            this.CreateRedistributeOrder = CreateRedistributeOrder;
        }

        String identifier;

        public String Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        /// <summary>
        /// Create a Remote Create Object Message
        /// </summary>
        public Func<NetIncomingMessage, ServerIObject> CreateLoadObjectOrder;

        /// <summary>
        /// Create the create object redistribution message
        /// </summary>
        public Func<ServerIObject,NetIncomingMessage,NetOutgoingMessage, NetOutgoingMessage> CreateRedistributeOrder;
        
    }
}
