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
    public class Triangle : I2DPrimitive
    {        
        List<Vector2> point = new List<Vector2>();
        Color color = Color.White;
        bool fill = true;
        RasterizerState state;

        public Triangle()
        {            
            state = new RasterizerState();
            state.CullMode = CullMode.CullCounterClockwiseFace;
            state.FillMode = FillMode.WireFrame;
        }

        public void AddTriangle(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            point.Add(new Vector2(x1,y1));
            point.Add(new Vector2(x2, y2));
            point.Add(new Vector2(x3, y3));
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetFillMode(bool fill)
        {
            this.fill = fill;
        }

        protected override void Draw(Matrix view, Matrix Projection, RenderHelper render, PrimitiveBatch batch)
        {
            if (fill == false)
            {
                render.PushRasterizerState(state);
            }

            batch.Begin(PrimitiveType.TriangleList,view,Projection);
            foreach (var item in point)
            {
                batch.AddVertex(item, color);
            }
            batch.End();

            render.PopRasterizerState();
        }

    }
}
