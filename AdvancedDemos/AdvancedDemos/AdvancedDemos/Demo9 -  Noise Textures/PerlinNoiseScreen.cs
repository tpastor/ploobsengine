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
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class PerlinNoiseScreen : IScreen
    {
        Texture2D staticRandomTex;
        bool firsttime = true;
        protected override void  Draw(GameTime gameTime, RenderHelper render)
        {
            if (firsttime)
            {

                ///Generate the PerlinNoise in CPU side using the real equation
                staticRandomTex = GraphicFactory.CreateTexture2DPerlinNoise(800, 600, frequency, amplitude, 0.55f, 8);
                firsttime = false;
            }
            render.RenderTextureComplete(staticRandomTex, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity);
            render.RenderTextComplete("Demo 17-22:Texture Generated On the fly ", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Perlin Noise Texture generated Precedurally ", new Vector2(10, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Hit Space to change the texture", new Vector2(10, 55), Color.White, Matrix.Identity);
        }
        float frequency = 0.015f;
        private float amplitude = 1.2f;
        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                frequency += 0.001f;
                firsttime = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                frequency -= 0.001f;
                firsttime = true;
                if (frequency<0)
                {
                    frequency = 0;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                amplitude += 0.001f;
                firsttime = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                amplitude -= 0.001f;
                firsttime = true;
                if (amplitude < 0)
                {
                    amplitude = 0;
                }
            }



            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new ProceduralTextureScreen());
            }
            base.Update(gameTime);
        }
    }
        
}

