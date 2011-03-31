using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.Input
{
    public delegate void KeyStateChange(InputPlayableKeyBoard ipk);
    
    public interface InputPlayableKeyBoard : IInput
    {
        StateKey StateKey
        {
         
            get;
        }
        Keys[] Keys
        {
            get;
        }
        KeyStateChange KeyStateChange
        {
            get;
            
        }
        EntityType EntityType
        {
            get;
        }
        InputMask InputMask
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
