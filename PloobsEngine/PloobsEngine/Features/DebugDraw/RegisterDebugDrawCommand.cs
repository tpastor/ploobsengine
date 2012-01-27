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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Commands;

namespace PloobsEngine.Features.DebugDraw
{
    /// <summary>
    /// Command responsible for registering a Shape Drawer
    /// </summary>
    public class RegisterDebugDrawCommand : ICommand
    {
        public RegisterDebugDrawCommand(DebugShapesDrawer DebugDrawer)
        {
            this.shape = DebugDrawer;
        }

        DebugDraw ddc;
        DebugShapesDrawer shape;
        protected override void execute()
        {
            ddc.RegisterDebugDrawer(shape);
        }

        protected override void setTarget(object obj)
        {
            ddc = obj as DebugDraw;
        }

        public override string TargetName
        {
            get { return "DebugDraw"; }
        }
    }
}














