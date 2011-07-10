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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Commands;
using PloobsEngine.Cameras;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Features
{
    public class SkyBoxEnableDisableCommand : ICommand
    {
        bool enable;
        private SkyBox sb;


        public SkyBoxEnableDisableCommand(bool enable)
        {
            this.enable = enable;
        }

        #region ICommand Members        

        protected override void execute()
        {
            sb.setParameters(enable);         
        }

        protected override void setTarget(object obj)
        {
            this.sb = obj as SkyBox;            
        }

        #endregion

        #region ICommand Members


        public override string TargetName
        {
            get { return SkyBox.MyName; }
        }

        #endregion
    }
    
}
#endif