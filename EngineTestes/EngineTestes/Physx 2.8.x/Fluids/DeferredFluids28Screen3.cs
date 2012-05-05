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


namespace EngineTestes
{
    public class DeferredEmitterFluids28Screen3 : IScene
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

                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            PhysxPhysicObject frame;
            {
                var boxShapeDesc = new BoxShapeDescription(3, 10, 3);
                SimpleModel SimpleModel = new PloobsEngine.Modelo.SimpleModel(factory, "Model/block");
                SimpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);

                frame = new PloobsEngine.Physics.PhysxPhysicObject(boxShapeDesc, Matrix.CreateTranslation(50, 5, 50), new Vector3(3, 10, 3));
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, SimpleModel, frame);
                this.World.AddObject(obj);
            }

            //Create an attached emitter
            FluidEmitterDescription emitterDesc = new FluidEmitterDescription();
            emitterDesc.MaximumParticles = 0;
            emitterDesc.DimensionX = 8f;
            emitterDesc.DimensionY = 8f;
            emitterDesc.Type = EmitterType.ConstantFlowRate;
            emitterDesc.Rate = 150.0f;
            emitterDesc.FluidVelocityMagnitude = 60.0f;
            emitterDesc.ParticleLifetime = 8.0f;
            emitterDesc.Shape = EmitterShape.Rectangular;

            //attach to actor
            emitterDesc.Flags = FluidEmitterFlag.AddBodyVelocity | FluidEmitterFlag.Enabled | FluidEmitterFlag.Visualization;
            emitterDesc.RepulsionCoefficient = 0.02f;

            //emitterDesc.RelativePose = Phyx.Matrix.RotationX((float)Math.PI / 2);
            emitterDesc.RelativePose *= Phyx.Matrix.Translation(0, 20f, 0);

            emitterDesc.FrameShape = frame.Actor.Shapes[0];

            FluidDescription fluidDesc = new FluidDescription();
            fluidDesc.MaximumParticles = 2500;
            fluidDesc.KernelRadiusMultiplier = 5.0f;
            fluidDesc.RestParticlesPerMeter = 5.0f;
            fluidDesc.MotionLimitMultiplier = 0.9f;
            fluidDesc.PacketSizeMultiplier = 16;
            fluidDesc.CollisionDistanceMultiplier = 0.12f;
            fluidDesc.Stiffness = 50.0f;
            fluidDesc.Viscosity = 40.0f;
            fluidDesc.RestDensity = 1000.0f;
            fluidDesc.Damping = 0.0f;
            fluidDesc.RestitutionForStaticShapes = 0.2f;
            fluidDesc.DynamicFrictionForStaticShapes = 0.05f;
            fluidDesc.Flags = FluidFlag.Enabled | FluidFlag.Visualization;
            fluidDesc.SimulationMethod = FluidSimulationMethod.SmoothedParticleHydrodynamics;

            fluidDesc.ParticleWriteData.AllocatePositionBuffer<Phyx.Vector3>(fluidDesc.MaximumParticles);
            fluidDesc.ParticleWriteData.NumberOfParticles = fluidDesc.MaximumParticles;

            FluidMOdel FluidMOdel = new PloobsEngine.Modelo.FluidMOdel(factory, "teste", null, fluidDesc.MaximumParticles);
            PhysxFluidObject PhysxFluidObject = new PloobsEngine.Physics.PhysxFluidObject(fluidDesc);
            FluidShader FluidShader = new FluidShader();
            DeferredMaterial ForwardMaterial = new DeferredMaterial(FluidShader);
            IObject IObject = new IObject(ForwardMaterial, FluidMOdel, PhysxFluidObject);
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


    }
}
