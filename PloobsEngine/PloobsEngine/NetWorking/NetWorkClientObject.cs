using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Lidgren.Network;
using System.Diagnostics;

namespace PloobsEngine.NetWorking
{
    public class NetWorkClientObject
    {
        public NetWorkClientObject(String Identifier, Func<NetOutgoingMessage, NetOutgoingMessage> CreateRemoteObjectOrder, Func<NetIncomingMessage, int, IObject> HandleRemoteObjectOrder)
        {
            this.identifier = Identifier;
            this.CreateRemoteObjectOrder = CreateRemoteObjectOrder;
            this.HandleRemoteObjectOrder = HandleRemoteObjectOrder;
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
        public Func<NetOutgoingMessage, NetOutgoingMessage> CreateRemoteObjectOrder;

        /// <summary>
        /// Handle Incomming Create Object message 
        /// </summary>
        public Func<NetIncomingMessage, int, IObject> HandleRemoteObjectOrder;                
    }
}
