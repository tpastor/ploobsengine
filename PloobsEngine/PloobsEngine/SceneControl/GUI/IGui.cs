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
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl.GUI
{
    /// <summary>
    /// Specification for a GUI system
    /// </summary>
    public abstract class IGui
    {
        protected abstract void Initialize(EngineStuff engine, GraphicFactory factory, GraphicInfo ginfo);
        internal void iInitialize(EngineStuff engine, GraphicFactory factory, GraphicInfo ginfo)
        {
            Initialize(engine, factory, ginfo);
        }

        protected abstract void Dispose();
        internal void iDispose()
        {
            Dispose();
        }

        protected abstract void Update(GameTime gt);
        internal void iUpdate(GameTime gt)
        {
            Update(gt);
        }
        protected abstract void EndDraw(GameTime gt, RenderHelper render, GraphicInfo ginfo);
        internal void iEndDraw(GameTime gt, RenderHelper render, GraphicInfo ginfo)
        {
            EndDraw(gt, render, ginfo);
        }


        protected abstract void BeginDraw(GameTime gt, RenderHelper render, GraphicInfo ginfo);
        internal void iBeginDraw(GameTime gt, RenderHelper render, GraphicInfo ginfo)
        {
            BeginDraw(gt, render, ginfo);
        }

    }
}
