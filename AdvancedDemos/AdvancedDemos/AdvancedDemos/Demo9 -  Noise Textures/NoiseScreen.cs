using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework.Input;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// How can you use the ordinary XNA draw tools with the engine
    /// "Also the Noise Effect that is nice" - Its a modified version of Riemers code
    /// </summary>
    public class NoiseScreen : IScreen
    {

        Effect effect;
        RenderTarget2D target;
        Texture2D staticRandomTex;
        Texture2D perlinNoise;        

        bool firstTime = true;

        protected override void  Draw(GameTime gameTime, RenderHelper render)
        {
            if (firstTime)
            {
                render.PushRenderTarget(target);
                float time = (float)gameTime.TotalGameTime.TotalMilliseconds / 100.0f;
                render.Clear(Color.Black);
                effect.CurrentTechnique = effect.Techniques["PerlinNoise"];
                effect.Parameters["xTexture"].SetValue(staticRandomTex);
                effect.Parameters["xOvercast"].SetValue(1.1f);
                effect.Parameters["xTime"].SetValue(time / 1000.0f);                
                render.RenderFullScreenQuadVertexPixel (effect);                
                perlinNoise = render.PopRenderTargetAsSingleRenderTarget2D();                
                //firstTime = false; ///uncomment to create only one
            }

            render.RenderTextureComplete(perlinNoise, Color.White, GraphicInfo.FullScreenRectangle, Matrix.Identity);
            render.RenderTextComplete("Demo 17-22:Texture Generates On the fly ", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Perlin Noise Generated On Shader", new Vector2(10, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Hit Space to change the texture", new Vector2(10, 55), Color.White, Matrix.Identity);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new PerlinNoiseScreen());
            }
            base.Update(gameTime);
        }

        protected override void  LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
 	        base.LoadContent(GraphicInfo, factory, contentManager);
            staticRandomTex = factory.CreateTexture2DRandom(32,32); ///tem q ser 32, pq o shader espera isso (nao vale a pena alterar o shader para deixar independente de resolucao)
            target = factory.CreateRenderTarget(GraphicInfo.BackBufferWidth,GraphicInfo.BackBufferHeight);
            effect = factory.GetEffect("Effects\\perlin");                        
        }
    }
        
}

