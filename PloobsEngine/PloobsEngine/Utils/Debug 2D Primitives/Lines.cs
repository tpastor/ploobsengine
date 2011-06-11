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

        public void SetColor(Color color)
        {
            this.color = color;
        }        
        

        protected override void Draw(Vector2 transform, RenderHelper render, PrimitiveBatch batch)
        {
            batch.Begin(PrimitiveType.LineList);
            foreach (var item in lines)
            {
                batch.AddVertex(item + transform, color);
            }
            batch.End();
        }
    }
}
