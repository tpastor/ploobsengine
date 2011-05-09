using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Most basic sample about texture
    /// </summary>
    public class PerlinNoiseScreen : IScreen
    {
        Texture2D staticRandomTex;        
        protected override void  Draw(GameTime gameTime, RenderHelper render)
        {
            ///Generate the PerlinNoise in CPU side using the real equation
            staticRandomTex = GraphicFactory.CreateTexture2DPerlinNoise(800, 600, 0.015f, 1.2f, 0.55f, 8);            
            render.RenderTextureComplete(staticRandomTex);
            render.RenderTextComplete("Perlin Noise Texture generated Precedurally -- Real Equations", new Vector2(10, 15), Color.White, Matrix.Identity);
        }        
    }
        
}

