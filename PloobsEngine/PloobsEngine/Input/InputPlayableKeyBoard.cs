#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.Input
{
    /// <summary>
    /// Called when a key event is fired
    /// </summary>
    /// <param name="ipk">The ipk.</param>
    public delegate void KeyStateChange(InputPlayableKeyBoard ipk);

    /// <summary>
    /// keyboard Input Binding interface
    /// </summary>
    public abstract class InputPlayableKeyBoard : IInput
    {
        public abstract StateKey StateKey
        {         
            get;
        }
        public abstract Keys[] Keys
        {
            get;
        }

        public event KeyStateChange KeyStateChange = null;

        internal void FireEvent()
        {
            if (KeyStateChange != null)
                KeyStateChange(this);
        }
        
        public abstract EntityType EntityType
        {
            get;
        }

        public abstract InputMask InputMask
        {
            get;
        }

    }
        
    

  public enum StateKey
  {
      PRESS, UP, RELEASE, DOWN
  }
  public enum EntityType
  {
      IOBJECT , CAMERA , COMPONENT , TOOLS
  }
}
#endif