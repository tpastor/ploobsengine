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
using EngineTestes.TransparencyOnBorders;

namespace ProjectTemplate
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class StealthShaderScreen : IScene
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
            desc.UsePostDrawPhase = true;
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
            {
                ///Create a simple object
                ///Geomtric Info and textures (this model automaticaly loads the texture)
                SimpleModel simpleModel = new SimpleModel(factory, "model/dude");                
                for (int i = 0; i < simpleModel.GetTextureInformation(0).Count(); i++)
                {
                    simpleModel.GetTextureInformation(0)[i].SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
                }
                
                ///Physic info (position, rotation and scale are set here)
                GhostObject pi = new GhostObject(new Vector3(200, 10, 0), Matrix.Identity, new Vector3(4));
                ///Shader info (must be a deferred type)
                ForwardGradativeAlphaShader shader = new ForwardGradativeAlphaShader();
                shader.ShaderId = 0.4f;
                ///Material info (must be a deferred type also)
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                
                ///The object itself
                IObject obj = new IObject(fmaterial, simpleModel, pi);
                ///Add to the world
                this.World.AddObject(obj);                
            }

            {
                ///Create a simple object
                ///Geomtric Info and textures (this model automaticaly loads the texture)
                SimpleModel simpleModel = new SimpleModel(factory, "model/dude");
                //simpleModel.SetTexture(factory.CreateTexture2DColor(1,1,Color.Red), TextureType.DIFFUSE);
                ///Physic info (position, rotation and scale are set here)
                GhostObject pi = new GhostObject(new Vector3(200, 10, 0), Matrix.Identity, new Vector3(4));
                ///Shader info (must be a deferred type)
                ForwardEMTransparentShader shader = new ForwardEMTransparentShader("Textures/Cubemap");                
                ///Material info (must be a deferred type also)
                ForwardMaterial fmaterial = new ForwardMaterial(shader);                
                ///The object itself
                IObject obj = new IObject(fmaterial, simpleModel, pi);
                ///Add to the world
                this.World.AddObject(obj);                
            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "model/cenario");
                ///Physic info (position, rotation and scale are set here)
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ///Forward Shader (look at this shader construction for more info)
                ForwardXNABasicShader shader = new ForwardXNABasicShader();
                ///Deferred material
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                ///The object itself
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                ///Add to the world
                //this.World.AddObject(obj);
            }

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            ///Draw some text on the screen
            render.RenderTextComplete("Demo: Basic Screen Forward", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
        }

    }
}
