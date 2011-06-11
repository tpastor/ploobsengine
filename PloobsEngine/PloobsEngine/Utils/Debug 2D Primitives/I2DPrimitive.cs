using System;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Features.DebugDraw
{
    public abstract class I2DPrimitive
    {   protected GraphicInfo graphicInfo;
        protected GraphicFactory graphicFactory;

        protected virtual void Init(GraphicInfo ginfo, GraphicFactory factory) { }
        internal void iInit(GraphicInfo ginfo, GraphicFactory factory)
        {        
            this.graphicFactory = factory;
            this.graphicInfo = ginfo;
            isEnabled = true;
            Init(ginfo,factory);
        }

        protected abstract void Draw(Vector2 transform, RenderHelper render, PrimitiveBatch batch);
        
        internal void iDraw(Vector2 transform, RenderHelper render, PrimitiveBatch batch)
        {
            if (isEnabled)
                Draw(transform,render,batch);
        }

        public bool isEnabled
        {
            get;
            set;
        }
    }
}
