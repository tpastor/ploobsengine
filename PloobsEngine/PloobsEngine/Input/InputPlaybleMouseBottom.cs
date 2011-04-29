using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.Input
{
    public delegate void MouseStateChangeComplete(MouseState ms);

    public interface InputPlaybleMouseBottom : IInput
    {
        StateKey StateKey
        {
            get;
        }
        MouseButtons MouseButtons
        {
            get;
        }
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
