using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Engine.Input
{
    public delegate void KeyStateChange();

    public interface InputPlayable
    {
        StateKey StateKey
        {
            get;
        }
        Keys Keys
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
