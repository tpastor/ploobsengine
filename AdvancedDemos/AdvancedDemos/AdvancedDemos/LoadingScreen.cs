using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AdvancedDemo4._0
{
    public class LoadingScreen : IScreen
    {
        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime, RenderHelper render)
        {
            render.Clear(Color.Black);
            render.RenderTextureComplete(tex,Color.White,GraphicInfo.FullScreenRectangle,Matrix.Identity);
        }
        Texture2D tex;
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            tex = factory.GetTexture2D("Textures//loading_screen");
            base.LoadContent(GraphicInfo, factory, contentManager);
        }
    }
}
