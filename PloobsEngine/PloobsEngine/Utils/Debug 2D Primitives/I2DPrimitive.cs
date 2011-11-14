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
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Features.DebugDraw
{
    public abstract class I2DPrimitive
    {   protected GraphicInfo graphicInfo;
        protected GraphicFactory graphicFactory;

        protected virtual void Init(GraphicInfo ginfo, GraphicFactory factory) { }
        internal void iInit(GraphicInfo ginfo, GraphicFactory factory)
        {        
            this.graphicFactory = factory;
            this.graphicInfo = ginfo;
            isEnabled = true;
            Init(ginfo,factory);
        }

        protected abstract void Draw(Vector2 transform, RenderHelper render, PrimitiveBatch batch);
        
        internal void iDraw(Vector2 transform, RenderHelper render, PrimitiveBatch batch)
        {
            if (isEnabled)
                Draw(transform,render,batch);
        }

        public bool isEnabled
        {
            get;
            set;
        }
    }
}
