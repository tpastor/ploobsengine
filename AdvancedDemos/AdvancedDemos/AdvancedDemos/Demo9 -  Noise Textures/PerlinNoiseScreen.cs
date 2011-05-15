using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework.Input;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Most basic sample about texture
    /// </summary>
    public class PerlinNoiseScreen : IScreen
    {
        Texture2D staticRandomTex;
        bool firsttime = true;
        protected override void  Draw(GameTime gameTime, RenderHelper render)
        {
            if (firsttime)
            {
                ///Generate the PerlinNoise in CPU side using the real equation
                staticRandomTex = GraphicFactory.CreateTexture2DPerlinNoise(800, 600, 0.015f, 1.2f, 0.55f, 8);
                firsttime = false;
            }
            render.RenderTextureComplete(staticRandomTex);            
            render.RenderTextComplete("Demo 17-22:Texture Generates On the fly ", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Perlin Noise Texture generated Precedurally ", new Vector2(10, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Hit Space to change the texture", new Vector2(10, 55), Color.White, Matrix.Identity);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new ProceduralTextureScreen());
            }
            base.Update(gameTime);
        }
    }
        
}

