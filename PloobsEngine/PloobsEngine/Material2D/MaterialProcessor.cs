using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.SceneControl._2DScene;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Material2D
{
    public interface IMaterialProcessor
    {
        void ProcessDraw(GameTime gameTime,RenderHelper render, ICamera2D camera, List<I2DObject> objects);
        void ProcessPreDraw(GameTime gameTime, RenderHelper render, ICamera2D camera,I2DWorld world, List<I2DObject> objects);
    }
}
