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

namespace PloobsEngine.Features.DebugDraw
{
    /// <summary>
    /// Responsible for a collection of Debug primitives Draw
    /// </summary>
    public class DebugShapesDrawer
    {
        public DebugShapesDrawer(bool DrawAllShapesEachFrame = false)
        {
            this.drawAllShapesEachFrame = DrawAllShapesEachFrame;
        }

        bool drawAllShapesEachFrame = false;

        /// <summary>
        /// Gets a value indicating whether [draw all shapes each frame].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [draw all shapes each frame]; otherwise, <c>false</c>.
        /// </value>
        public bool DrawAllShapesEachFrame
        {
            get { return drawAllShapesEachFrame; }
            internal set { drawAllShapesEachFrame = value; }
        }

        List<IDebugDrawShape> shapes = new List<IDebugDrawShape>();
        bool initialized = false;

        /// <summary>
        /// Adds a shape to this drawer.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public void AddShape(IDebugDrawShape shape) 
        {
            if (initialized)
            {
                shape.Initialize(factory, ginfo);
            }
            shapes.Add(shape);
        }

        public void RemoveShape(IDebugDrawShape shape)
        {            
            shapes.Remove(shape);
        }

        internal List<IDebugDrawShape> GetShapes()
        {
            return shapes;
        }
        GraphicFactory factory;
        GraphicInfo ginfo;
        internal void Initialize(GraphicFactory factory, GraphicInfo ginfo)
        {            
            this.factory = factory;
            this.ginfo = ginfo;
            initialized = true;

            foreach (var item in shapes)
            {
                item.Initialize(factory, ginfo);
            }
        }

        internal void EndFrame()
        {
            if (!DrawAllShapesEachFrame)
            {
                shapes.Clear();
            }
        }
        
    }
}
