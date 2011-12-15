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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Features.DebugDraw
{
    public class Lines : I2DPrimitive
    {        
        List<Vector2> lines = new List<Vector2>();
        Color color = Color.White;                        

        public void AddLine(float x1, float y1, float x2, float y2)
        {
            lines.Add(new Vector2(x1,y1));
            lines.Add(new Vector2(x2, y2));            
        }

        public void AddLine(Vector2 v1, Vector2 v2)
        {
            lines.Add(v1);
            lines.Add(v2);
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }


        protected override void Draw(Matrix view, Matrix Projection, RenderHelper render, PrimitiveBatch batch)
        {
            batch.Begin(PrimitiveType.LineList,view,Projection);
            foreach (var item in lines)
            {
                batch.AddVertex(item, color);
            }
            batch.End();
        }
    }
}
