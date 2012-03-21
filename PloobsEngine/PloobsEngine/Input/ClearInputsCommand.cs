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
#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Commands;


namespace PloobsEngine.Input
{
    public class ClearInputsCommand : ICommand
    {       

        public ClearInputsCommand()
        {            
        }

        #region ICommand Members

        private InputAdvanced ia;

        protected override void execute()
        {
            ia.Clear();
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


    }
}
#endif