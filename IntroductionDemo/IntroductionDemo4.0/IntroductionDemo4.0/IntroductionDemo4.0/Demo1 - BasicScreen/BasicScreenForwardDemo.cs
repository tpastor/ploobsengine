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

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class BasicScreenForwardDemo : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller(),null,true);

            ///Create a Forward Render Technich
            ///Most of our technich like illumination, shadow ... are NOT implemented in FORWARD. Use this for simple stuffs and
            ///for W7
            ///You cant use light is this Technich (We will release the Forward Lights implementation soon showing how to expand the PloobsEngine)
            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);            

            ///Create the model
            SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            ///create the physic object
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One,MaterialDescription.DefaultBepuMaterial());            
            /// BECAUSE OF THE RENDER TECHNICH, the Shader must be forward
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            /// BECAUSE OF THE RENDER TECHNICH, the Material must be forward
            ForwardMaterial fmaterial = new ForwardMaterial(shader);            
            ///create the object
            IObject obj = new IObject(fmaterial,simpleModel,tmesh);
            ///add to the world
            this.World.AddObject(obj); 


            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo.Viewport));                              
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo: Basic Screen Forward", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
        }

    }
}
