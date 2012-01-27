using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;

namespace PloobsEngine.NetWorking
{
    public class ServerIObject : IObject
    {
        public ServerIObject(IPhysicObject PhysicObject)
        {        
            System.Diagnostics.Debug.Assert(PhysicObject != null);            
            this.PhysicObject = PhysicObject;            
            IObjectAttachment = new List<IObjectAttachment>();
            Name = null;
        }       
    }
}
