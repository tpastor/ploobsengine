using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl.GUI
{
    public abstract class IGui
    {
        protected abstract void Initialize(EngineStuff engine, GraphicFactory factory, GraphicInfo ginfo);
        internal void iInitialize(EngineStuff engine, GraphicFactory factory, GraphicInfo ginfo)
        {
            Initialize(engine, factory, ginfo);
        }

        protected abstract void Dispose();
        internal void iDispose()
        {
            Dispose();
        }

        protected abstract void Update(GameTime gt);
        internal void iUpdate(GameTime gt)
        {
            Update(gt);
        }
        protected abstract void EndDraw(GameTime gt, RenderHelper render, GraphicInfo ginfo);
        internal void iEndDraw(GameTime gt, RenderHelper render, GraphicInfo ginfo)
        {
            EndDraw(gt, render, ginfo);
        }


        protected abstract void BeginDraw(GameTime gt, RenderHelper render, GraphicInfo ginfo);
        internal void iBeginDraw(GameTime gt, RenderHelper render, GraphicInfo ginfo)
        {
            BeginDraw(gt, render, ginfo);
        }

    }
}
