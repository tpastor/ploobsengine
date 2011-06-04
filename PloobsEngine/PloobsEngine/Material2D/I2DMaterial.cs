using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl._2DScene;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Material2D
{
    public abstract class I2DMaterial
    {        
        public virtual void Initialization(GraphicInfo ginfo, GraphicFactory factory, I2DObject obj) { }
        public virtual void PreDrawnPhase(GameTime gt, I2DWorld mundo, I2DObject obj, RenderHelper render) { }
        public abstract void Draw(GameTime gt, I2DObject obj, RenderHelper render);
        public virtual void Update(GameTime gameTime, IObject obj) { }             
    }
}
