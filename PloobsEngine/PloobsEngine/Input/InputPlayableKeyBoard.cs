#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
#if !WINDOWS_PHONE || WINRT
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
  
}
#endif