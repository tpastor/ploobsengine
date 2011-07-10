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
    public class SimpleConcreteMouseBottomInputPlayable : InputPlaybleMouseBottom
    {
        private InputMask mask;

        public SimpleConcreteMouseBottomInputPlayable(StateKey sk, EntityType et, MouseButtons mb, MouseStateChangeComplete mst = null, InputMask mask = Input.InputMask.GSYSTEM)
        {
            this.sk = sk;
            this.mb = mb;
            this.KeyStateChange += mst;
            this.et = et;
            this.mask = mask;
        }
        public  override InputMask InputMask
        {
            get { return mask; }
        }


        #region InputPlaybleMouseBottom Members

        private StateKey sk;
        private EntityType et;
        private MouseButtons mb;        

        public override StateKey StateKey
        {
            get { return sk; }
        }

        public override MouseButtons MouseButtons
        {
            get { return mb; }
        }

        public override EntityType EntityType
        {
            get { return et; }
        }

        
        #endregion
    }
}
#endif