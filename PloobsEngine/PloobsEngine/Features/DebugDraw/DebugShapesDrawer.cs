using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.Features.DebugDraw
{
    public class DebugShapesDrawer
    {
        public DebugShapesDrawer(bool DrawAllShapesEachFrame = false)
        {
            this.drawAllShapesEachFrame = DrawAllShapesEachFrame;
        }

        bool drawAllShapesEachFrame = false;

        public bool DrawAllShapesEachFrame
        {
            get { return drawAllShapesEachFrame; }
            internal set { drawAllShapesEachFrame = value; }
        }

        List<IDebugDrawShape> shapes = new List<IDebugDrawShape>();
        bool initialized = false;

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
