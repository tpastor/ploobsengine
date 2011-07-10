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
using PloobsEngine.Commands;

namespace PloobsEngine.Input
{
    public class BindMouseCommand : ICommand
    {
        private InputPlaybleMouseBottom ip;
        private InputPlaybleMousePosition ipx;
        private BindAction ba;        
        int type;
        private readonly static int POSITION = 1;
        private readonly static int BOTTOM = 2;

        public BindMouseCommand(InputPlaybleMouseBottom ip, BindAction ba)
        {
            this.ip = ip;
            this.ba = ba;
            type = BOTTOM;
        }
        public BindMouseCommand(InputPlaybleMousePosition ipx, BindAction ba)
        {
            this.ipx = ipx;
            this.ba = ba;
            type = POSITION;
        }
        
        #region ICommand Members
                
        private InputAdvanced ia;

        protected override void execute()
        {
            if (type == POSITION)
            {
                ia.BindMousePosition(ipx, ba);
            }
            else if (type == BOTTOM)
            {
                ia.BindMouseBottom(ip, ba);
            }
        }

        protected override void setTarget(object obj)
        {
            ia = (InputAdvanced)obj;
        }
        #endregion

        #region ICommand Members


        public override string TargetName
        {
            get { return InputAdvanced.MyName; }
        }

        #endregion

        public BindAction BindAction
        {
            get { return ba; }
            set { ba = value; }
        }
    }
}
#endif