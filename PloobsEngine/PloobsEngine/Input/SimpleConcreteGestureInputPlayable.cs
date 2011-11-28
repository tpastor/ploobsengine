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
#if WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;

namespace PloobsEngine.Input
{
    /// <summary>
    /// InputPlaybleMousePosition Implementation for mouse position
    /// </summary>
    public class SimpleConcreteGestureInputPlayable : InputPlaybleGesture
    {
        private InputMask mask;


        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConcreteMousePositionInputPlayable"/> class.
        /// </summary>
        /// <param name="mst">The MST.</param>
        /// <param name="mask">The mask.</param>
        /// <param name="et">The et.</param>
        public SimpleConcreteGestureInputPlayable(GestureType gestureType, Action<GestureSample> mst = null, InputMask mask = InputMask.GSYSTEM, EntityType et = Input.EntityType.TOOLS)
        {
            this.GestureType = gestureType;
            this.GestureFired += mst;
            this.EntityType = et;
            this.mask = mask;
        }

        

      
    }
}
#endif