using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Commands;

namespace PloobsEngine.Features.DebugDraw
{
    public class RegisterDebugDrawCommand : ICommand
    {
        public RegisterDebugDrawCommand(DebugShapesDrawer DebugDrawer)
        {
            this.shape = DebugDrawer;
        }

        DebugDraw ddc;
        DebugShapesDrawer shape;
        protected override void execute()
        {
            ddc.RegisterDebugDrawer(shape);
        }

        protected override void setTarget(object obj)
        {
            ddc = obj as DebugDraw;
        }

        public override string TargetName
        {
            get { return "DebugDraw"; }
        }
    }
}
