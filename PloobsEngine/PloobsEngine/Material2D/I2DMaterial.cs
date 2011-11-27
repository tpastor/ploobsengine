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
using PloobsEngine.SceneControl._2DScene;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Material2D
{
    public abstract class I2DMaterial
    {        
        public virtual void Initialization(GraphicInfo ginfo, GraphicFactory factory, I2DObject obj) { }
        public virtual void PreDrawnPhase(GameTime gt, I2DWorld mundo, I2DObject obj, RenderHelper render) { }
        public abstract void Draw(GameTime gt, I2DObject obj, RenderHelper render);
        public abstract void LightDraw(GameTime gt, I2DObject obj, RenderHelper render, Color color,PloobsEngine.Light2D.Light2D light);
        public virtual void Update(GameTime gameTime, I2DObject obj) { }             
    }
}
