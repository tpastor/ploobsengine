using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;

namespace PloobsEngine.Features.DebugDraw
{
    public class DebugDraw : IComponent
    {
        private List<DebugShapesDrawer> debugDrawers = new List<DebugShapesDrawer>();

        Engine.GraphicInfo GraphicInfo;
        Engine.GraphicFactory factory;
        
        protected override void LoadContent(Engine.GraphicInfo GraphicInfo, Engine.GraphicFactory factory)
        {
            this.factory = factory;
            this.GraphicInfo = GraphicInfo;
            base.LoadContent(GraphicInfo, factory);
        }

        internal void RegisterDebugDrawer(DebugShapesDrawer debugDrawer)
        {
            debugDrawer.Initialize(factory, GraphicInfo);
            debugDrawers.Add(debugDrawer);
        }
        
        public override ComponentType ComponentType
        {
            get { return Components.ComponentType.POS_WITHDEPTH_DRAWABLE; }
        }

        public override string getMyName()
        {
            return "DebugDraw";
        }

        protected override void PosWithDepthDraw(SceneControl.RenderHelper render, Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Matrix activeView, Microsoft.Xna.Framework.Matrix activeProjection)
        {
            base.PosWithDepthDraw(render, gt, activeView, activeProjection);

            foreach (var item in debugDrawers)
            {
                foreach (var shape in item.GetShapes())
                {
                    shape.Draw(render, activeView, activeProjection);    
                }

                item.EndFrame();
            }

        }

    }
}
