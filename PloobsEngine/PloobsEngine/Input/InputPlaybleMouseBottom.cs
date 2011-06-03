#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.Input
{
    public delegate void MouseStateChangeComplete(MouseState ms);

    public abstract class InputPlaybleMouseBottom : IInput
    {
        public abstract StateKey StateKey
        {
            get;
        }
        public abstract MouseButtons MouseButtons
        {
            get;
        }
        public abstract EntityType EntityType
        {
            get;
        }

        public event MouseStateChangeComplete KeyStateChange = null;

        internal void FireEvent(MouseState ms)
        {
            if (KeyStateChange != null)
                KeyStateChange(ms);

        }
        
        public abstract InputMask InputMask
        {
            get;
        }
        

    }

    
}
#endif