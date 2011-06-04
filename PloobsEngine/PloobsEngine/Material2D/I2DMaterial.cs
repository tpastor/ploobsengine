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
    public interface I2DMaterial
    {
        void Initialization(GraphicInfo ginfo, GraphicFactory factory, IObject obj);
        void PreDrawnPhase(GameTime gt, I2DWorld mundo, I2DObject obj,RenderHelper render);
        void Draw(GameTime gt, I2DObject obj, RenderHelper render);
        void Update(GameTime gameTime, IObject obj);             
    }
}
