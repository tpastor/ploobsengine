using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.Features.DebugDraw
{
    public interface IDebugDrawShape
    {
        bool Visible
        {
            set;
            get;
        }
        void Initialize(GraphicFactory factory, GraphicInfo ginfo);
        void Draw(RenderHelper render, Matrix view, Matrix projection);
    }
}
