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
using PloobsEngine.Loader;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Particle System Screen
    /// We use http://www.xnaparticles.com/ for the particles
    /// I STRONGLY suggets that everyone download their package of samples and see what this gooooood APi can Do
    /// The integration is VERY simple, just copy and past those samples here and it is working    
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
            BepuPhysicWorld BepuPhysicWorld = new BepuPhysicWorld(-0.97f);
            SimpleCuller SimpleCuller = new SimpleCuller();
            world = new IWorld(BepuPhysicWorld, SimpleCuller, new DPSFParticleManager());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);
            {
                ///Uncoment to add your model
                SimpleModel simpleModel = new SimpleModel(factory, "Model/cenario");
                ///Physic info (position, rotation and scale are set here)
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ///Forward Shader (look at this shader construction for more info)
                ForwardXNABasicShader shader = new ForwardXNABasicShader();
                ///Deferred material
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                ///The object itself
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                ///Add to the world
                this.World.AddObject(obj);
            }

            {
                SnowParticleSystem snow = new SnowParticleSystem();
                DPFSParticleSystem ps = new DPFSParticleSystem("snow", snow);
                this.World.ParticleManager.AddAndInitializeParticleSystem(ps);

                ///cant set emiter position before adding the particle
                ///IF YOU DO SO, IT WILL NOT WORK
                snow.Emitter.PositionData.Position = new Vector3(500, 0, 0);
            }


            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(-50), MathHelper.ToRadians(-10), new Vector3(-200, 150, 250), GraphicInfo.Viewport);
            this.World.CameraManager.AddCamera(cam);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo Particle System Integration; Smoke -> check DPFS SAMPLES for more", new Vector2(10, 15), Color.White, Matrix.Identity);
        }

    }
}
