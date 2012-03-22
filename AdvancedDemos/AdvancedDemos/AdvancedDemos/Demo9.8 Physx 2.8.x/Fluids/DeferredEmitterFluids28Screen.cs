using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physics;
using StillDesign.PhysX;
using Phyx = StillDesign.PhysX.MathPrimitives;
using PloobsEngine.Features.Billboard;
using PloobsEngine.Light;

namespace AdvancedDemo4._0
{
    public class DeferredEmitterFluids28Screen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            PhysxPhysicWorld PhysxPhysicWorld = new PhysxPhysicWorld(new Vector3(0, -10, 0));
            world = new IWorld(PhysxPhysicWorld, new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            PhysxPhysicWorld PhysxPhysicWorld = World.PhysicWorld as PhysxPhysicWorld;
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");

                PhysxTriangleMesh tmesh = new PhysxTriangleMesh(PhysxPhysicWorld, simpleModel,
                    Matrix.Identity, Vector3.One);

                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            PhysxPhysicObject frame;
            {
                var boxShapeDesc = new BoxShapeDescription(3, 10, 3);
                SimpleModel SimpleModel = new PloobsEngine.Modelo.SimpleModel(factory, "Model/block");
                SimpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);

                frame = new PloobsEngine.Physics.PhysxPhysicObject(boxShapeDesc, Matrix.CreateTranslation(100, 100, 100), new Vector3(3, 10, 3));
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, SimpleModel, frame);
                this.World.AddObject(obj);
            }

            //Create an attached emitter
            FluidEmitterDescription emitterDesc = new FluidEmitterDescription();
            emitterDesc.MaximumParticles = 0;
            emitterDesc.DimensionX = 0.4f;
            emitterDesc.DimensionY = 0.4f;
            emitterDesc.Type = EmitterType.ConstantFlowRate;
            emitterDesc.Rate = 300.0f;
            emitterDesc.FluidVelocityMagnitude = 50.0f;
            emitterDesc.ParticleLifetime = 10.0f;
            emitterDesc.Shape = EmitterShape.Rectangular;

            //attach to actor
            emitterDesc.Flags = FluidEmitterFlag.AddBodyVelocity | FluidEmitterFlag.Enabled | FluidEmitterFlag.Visualization;
            emitterDesc.RepulsionCoefficient = 0.02f;

            emitterDesc.RelativePose = Phyx.Matrix.RotationX((float)Math.PI / 2);
            emitterDesc.RelativePose *= Phyx.Matrix.Translation(0, 20f, 0);

            emitterDesc.FrameShape = frame.Actor.Shapes[0];

            FluidDescription fluidDesc = new FluidDescription();
            fluidDesc.MaximumParticles = 20000;
            fluidDesc.KernelRadiusMultiplier = 2.0f;
            fluidDesc.RestParticlesPerMeter = 7.0f;
            fluidDesc.MotionLimitMultiplier = 3.0f;
            fluidDesc.PacketSizeMultiplier = 8;
            fluidDesc.CollisionDistanceMultiplier = 0.1f;
            fluidDesc.Stiffness = 50.0f;
            fluidDesc.Viscosity = 40.0f;
            fluidDesc.RestDensity = 1000.0f;
            fluidDesc.Damping = 0.0f;
            fluidDesc.RestitutionForStaticShapes = 0.2f;
            fluidDesc.DynamicFrictionForStaticShapes = 0.05f;
            fluidDesc.Flags = FluidFlag.Enabled | FluidFlag.Visualization;
            fluidDesc.SimulationMethod = FluidSimulationMethod.SmoothedParticleHydrodynamics;

            fluidDesc.ParticleWriteData.AllocatePositionBuffer<Phyx.Vector3>(30000);
            fluidDesc.ParticleWriteData.NumberOfParticles = 30000;

            ///Use instanced Billboard to render the particles !
            InstancedBilboardModel InstancedBilboardModel = new InstancedBilboardModel(factory, "teste", "Textures/Smoke", new BilboardInstance[] { new BilboardInstance() }, 20000);
            PhysxFluidObject PhysxFluidObject = new PloobsEngine.Physics.PhysxFluidObject(fluidDesc);
            DeferredInstancedBilboardShader DeferredInstancedBilboardShader = new PloobsEngine.Material.DeferredInstancedBilboardShader(BilboardType.Spherical);
            DeferredInstancedBilboardShader.AlphaTestLimit = 0.2f;
            FluidMaterial DeferredMaterial = new FluidMaterial(DeferredInstancedBilboardShader, 20000, new Vector2(0.2f));
            IObject IObject = new IObject(DeferredMaterial, InstancedBilboardModel, PhysxFluidObject);
            this.World.AddObject(IObject);

            PhysxFluidObject.Fluid.CreateFluidEmitter(emitterDesc);

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


            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory, false);
            this.AttachCleanUpAble(BallThrowBullet);

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            ///must be called before
            base.Draw(gameTime, render);

            ///Draw some text to the screen
            render.RenderTextComplete("Demo: Physx Emiter Fluid", new Vector2(20, 15), Color.White, Matrix.Identity);
        }



    }
}
