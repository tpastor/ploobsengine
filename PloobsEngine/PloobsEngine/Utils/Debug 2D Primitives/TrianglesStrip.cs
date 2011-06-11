using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Features.DebugDraw
{
    public class TrianglesStrip : I2DPrimitive
    {
        List<Vector2> point = new List<Vector2>();
        Color color = Color.White;
        bool fill = true;
        RasterizerState state;
        
        public TrianglesStrip()
        {            
            state = new RasterizerState();
            state.CullMode = CullMode.CullCounterClockwiseFace;
            state.FillMode = FillMode.WireFrame;
        }

        public void AddPoint(float x1, float y1)
        {
            point.Add(new Vector2(x1,y1));            
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetFillMode(bool fill)
        {
            this.fill = fill;
        }
                
        protected override void Draw(Vector2 transform, RenderHelper render, PrimitiveBatch batch)
        {
            if (fill == false)
            {
                render.PushRasterizerState(state);
            }

            if (point.Count < 3)
            {
                throw new InvalidOperationException("Need at least 3 points to make a triangle strip");
            }

            batch.Begin(PrimitiveType.TriangleStrip);
            foreach (var item in point)
            {
                batch.AddVertex(item + transform, color);       
            }
            batch.End();

            if (fill == false)
            {
                render.PopRasterizerState();
            }
        }
    }
}
