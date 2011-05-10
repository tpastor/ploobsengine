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
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Features;
using PloobsEngine.Commands;
using PloobsEngine.Physic.PhysicObjects.BepuObject;
using PloobsEngine.Input;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Deferred Transparent Screen
    /// </summary>
    public class TransparentDeferredScreen : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.97f,true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
        }

        /// <summary>
        /// Init Screen
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="engine"></param>
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
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

            ///Classic Island
            SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            DeferredNormalShader shader = new DeferredNormalShader();
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            this.World.AddObject(obj);

            ///Transparent blocks
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    ///Procedural yellow diffuse texture
                    SimpleModel sm = new SimpleModel(factory, "Model\\block");
                    sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Yellow), TextureType.DIFFUSE);
                    ///physic Ghost object(no collision)
                    GhostObject pi = new GhostObject(new Vector3(i * 10, 50, j * 10), Matrix.Identity, new Vector3(5));
                    pi.isMotionLess = true;
                    ///Transparency shader                    
                    ///Transparent objects are NOT affected by lights
                    ///It is a forward shader in the deferred engine (applied after all deferreed processing)
                    ForwardTransparenteShader s = new ForwardTransparenteShader();
                    ///If the texture does not have Alpha, you can create an alpha for all the model
                    s.TransparencyLevel = 0.3f;
                    ///THIS MODEL is DRAW AFTER all the Deferred ones (remember:light wont afect it)
                    ///You can use all the forward material the same way we are using this
                    ForwardMaterial mat = new ForwardMaterial(s);
                    IObject obj4 = new IObject(mat, sm, pi);
                    this.World.AddObject(obj4);
                }

            }

            LightThrowBepu lt = new LightThrowBepu(this.World, factory);

            #region NormalLight
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.5f;
            ld1.LightIntensity = li;
            ld2.LightIntensity = li;
            ld3.LightIntensity = li;
            ld4.LightIntensity = li;
            ld5.LightIntensity = li;
            this.World.AddLight(ld1);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);
            this.World.AddLight(ld4);
            this.World.AddLight(ld5);
            #endregion

            this.World.CameraManager.AddCamera(new CameraFirstPerson(true, GraphicInfo.Viewport));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCUBE");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            ///must be called before
            base.Draw(gameTime, render);

            ///Draw some text to the screen
            render.RenderTextComplete("Demo: Deferred Shading with Transparency", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Transparent Objects are not affected by lights", new Vector2(20, 35), Color.White, Matrix.Identity);
        }

    }
}
