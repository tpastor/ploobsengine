using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Input
{
    public interface InputPlaybleMousePosition : IInput
    {
        
        EntityType EntityType
        {
            get;
        }
        MouseStateChangeComplete KeyStateChange
        {
            get;
        }
        InputMask InputMask
        {
            get;
        }
    }
}
