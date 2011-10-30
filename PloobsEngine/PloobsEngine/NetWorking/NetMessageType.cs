using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.NetWorking
{
    public enum NetMessageType : short
    {
        PhysicInternalSync = 0x01,        
        UserDefined = 0x02,
        PhysicRedistribute = 0x04,
        CreateNetworkObjectOnServer = 0x05,
        CreateNetworkObjectOnClient = 0x06
    }

    public class NetWorkingConstants
    {
        public const int HeaderSizeinBytes = sizeof(short);
    }
    

}
