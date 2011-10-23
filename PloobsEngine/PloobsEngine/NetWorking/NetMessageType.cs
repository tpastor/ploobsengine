using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.NetWorking
{
    public enum NetMessageType : short
    {
        PhysicInternalSync = 0x01,        
        PhysicCreate = 0x03,
        UserDefined = 0x02
    }
}
