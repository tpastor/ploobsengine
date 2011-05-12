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
using PloobsEngine.Particles;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Particle System Screen
    /// We use http://www.xnaparticles.com/ for the particles
    /// I STRONGLY suggets that everyone download their package of samples and see what this gooooood APi can Do
    /// The integration is VERY simple, just copy and past those samples here and it is working
    /// We showed how to do it for 2 samples, the Smoke and the Snow =P
    /// </summary>
    public class ParticleScreen : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.097f,true), new SimpleCuller(),new DPSFParticleManager());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
        }   

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SnowParticleSystem snow = new SnowParticleSystem();
                DPFSParticleSystem ps = new DPFSParticleSystem("snow", snow);
                this.World.ParticleManager.AddAndInitializeParticleSystem(ps);

                ///cant set emiter position before adding the particle
                ///IF YOU DO SO, IT WILL NOT WORK
                snow.Emitter.PositionData.Position = new Vector3(1000, 0, 0);
            }            

            ObjectThrowSmokeParticle ot = new ObjectThrowSmokeParticle(this);
            this.AddScreenUpdateable(ot);


            ///OUR Classic Model
            SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            DeferredNormalShader shader = new DeferredNormalShader();
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            this.World.AddObject(obj);

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

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo.Viewport));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());

        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Particle System Integration; Snow and Smoke, check DPFS SAMPLES", new Vector2(10, 15), Color.White, Matrix.Identity);
        }

    }
}
