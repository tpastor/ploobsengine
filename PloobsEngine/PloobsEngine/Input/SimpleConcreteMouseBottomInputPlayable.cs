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
#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Input
{
    /// <summary>
    /// InputPlaybleMousePosition Implementation for mouse position
    /// </summary>
    public class SimpleConcreteMousePositionInputPlayable : InputPlaybleMousePosition
    {
        private InputMask mask;


        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConcreteMousePositionInputPlayable"/> class.
        /// </summary>
        /// <param name="mst">The MST.</param>
        /// <param name="mask">The mask.</param>
        /// <param name="et">The et.</param>
        public SimpleConcreteMousePositionInputPlayable(MouseStateChangeComplete mst = null, InputMask mask = InputMask.GSYSTEM, EntityType et = Input.EntityType.TOOLS)
        {
            this.mst = mst;
            this.et = et;
            this.mask = mask;
        }

        #region InputPlaybleMouseBottom Members

        
        private EntityType et;
        private MouseStateChangeComplete mst;

        public override EntityType EntityType
        {
            get { return et; }
        }


        public override InputMask InputMask
        {
            get { return mask; }
        }

        #endregion
    }
}
#endif