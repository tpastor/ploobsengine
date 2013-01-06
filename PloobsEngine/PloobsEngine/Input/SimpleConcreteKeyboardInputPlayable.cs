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
    public class SimpleConcreteKeyboardInputPlayable : InputPlayableKeyBoard
    {
        private StateKey state;
        private Keys[] key;        
        private EntityType type;
        private InputMask mask;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConcreteKeyboardInputPlayable"/> class.
        /// 
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="key">The key.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="mask">The mask.</param>
        /// <param name="type">The type.</param>
        public SimpleConcreteKeyboardInputPlayable(StateKey state, Keys key, KeyStateChange callback = null, InputMask mask = InputMask.GSYSTEM, EntityType type = Input.EntityType.TOOLS)
        {
            this.state = state;
            this.key = new Keys[1];
            this.key[0] = key;
            if (callback != null)
                this.KeyStateChange += callback;
            this.type = type;
            this.mask = mask;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConcreteKeyboardInputPlayable"/> class.
        /// For Combo
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="key">The key.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="mask">The mask.</param>
        /// <param name="type">The type.</param>
        public SimpleConcreteKeyboardInputPlayable(StateKey state, Keys[] key, KeyStateChange callback = null, InputMask mask = InputMask.GSYSTEM, EntityType type = Input.EntityType.TOOLS)
        {
            this.state = state;
            this.key = key;
            if(callback != null)
                this.KeyStateChange += callback;
            this.type = type;
            this.mask = mask;
        }
        

        #region InputPlayable Members

        public override StateKey StateKey
        {
            get
            {
                return state;
            }
            
        }

        public override Microsoft.Xna.Framework.Input.Keys[] Keys
        {
            get { return key; }
        }

        #endregion

        #region  InputPlayable Members


        public override EntityType EntityType
        {
            get { return type; }
        }

        #endregion

        #region InputPlayableKeyBoard Members


        public override InputMask InputMask
        {
            get { return mask; }
        }



        #endregion
    }
}
#endif