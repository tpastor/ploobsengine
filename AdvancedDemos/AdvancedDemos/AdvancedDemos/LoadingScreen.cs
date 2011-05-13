using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;

namespace AdvancedDemos
{
    public class LoadingScreen : IScreen
    {
        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime, RenderHelper render)
        {
            render.Clear(Color.Black);
            render.RenderTextComplete("LOADING SCREEN", new Vector2(50, 50), Color.White, Matrix.Identity);
        }
    }
}
