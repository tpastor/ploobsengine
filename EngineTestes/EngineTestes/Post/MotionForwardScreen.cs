using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace EngineTestes
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class MotionForwardScreen : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the IWorld
            world = new IWorld(new BepuPhysicWorld(-0.97f, true, 1), new SimpleCuller());

            ///Create the deferred technich
            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription();
            desc.BackGroundColor = Color.AliceBlue;
            desc.UsePostEffect = true;
            renderTech = new ForwardRenderTecnich(desc);
        }


        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///Uncoment to add your model
            SimpleModel simpleModel = new SimpleModel(factory, "Model/cenario");
            ///Physic info (position, rotation and scale are set here)
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            ///Forward Shader (look at this shader construction for more info)
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            ///Forward material
            ForwardMaterial fmaterial = new ForwardMaterial(shader);
            ///The object itself
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            ///Add to the world
            this.World.AddObject(obj);

            this.RenderTechnic.AddPostEffect(MotionBlurPostEffect);

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            RasterizerState = new RasterizerState();
            RasterizerState.CullMode = CullMode.None;
        }

        RasterizerState RasterizerState;
        SimpleMotionBlurPostEffect MotionBlurPostEffect = new SimpleMotionBlurPostEffect();
        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                MotionBlurPostEffect.Amount = MotionBlurPostEffect.Amount + 1; 
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                MotionBlurPostEffect.Amount = MotionBlurPostEffect.Amount - 1;
            }

            render.PushRasterizerState(RasterizerState);
            base.Draw(gameTime, render);
            render.PopRasterizerState();

            ///Draw some text on the screen
            render.RenderTextComplete("Demo: PostProcess Motion Blur", new Vector2(10, 15), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Amount (0 to 255): " + MotionBlurPostEffect.Amount, new Vector2(10, 35), Color.Red, Matrix.Identity);
        }

    }
}
