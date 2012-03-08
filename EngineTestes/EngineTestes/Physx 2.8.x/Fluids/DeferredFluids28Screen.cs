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
    public class DeferredFluids28Screen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            PhysxPhysicWorld PhysxPhysicWorld = new PhysxPhysicWorld(new Vector3(0,-10,0));            
            world = new IWorld(PhysxPhysicWorld, new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }
        
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            PhysxPhysicWorld PhysxPhysicWorld = World.PhysicWorld as PhysxPhysicWorld;
            
            base.LoadContent(GraphicInfo, factory, contentManager);

            const int maximumParticles = 1000;
            var fluidEmitterDesc = new FluidEmitterDescription()
            {
                DimensionX = 0.5f,
                DimensionY = 0.5f,
                Rate = 15,
                RelativePose =
                    Phyx.Matrix.RotationAxis(new Phyx.Vector3(0, 1, 0), (float)Math.PI) *
                    Phyx.Matrix.Translation(-40, 10, -50),
                Shape = EmitterShape.Rectangular,
                Type = EmitterType.ConstantFlowRate,
                RandomAngle = 0.5f
            };
            fluidEmitterDesc.Flags |= (FluidEmitterFlag.Enabled | FluidEmitterFlag.Visualization);

            var fluidDesc = new FluidDescription()
            {
                Emitters = { fluidEmitterDesc },
                Flags = FluidFlag.Enabled | FluidFlag.Visualization | FluidFlag.Enabled,
                MaximumParticles = maximumParticles,
                
            };
            fluidDesc.ParticleWriteData.AllocatePositionBuffer<Vector3>(maximumParticles);
            fluidDesc.ParticleWriteData.NumberOfParticles = maximumParticles;

            InstancedBilboardModel InstancedBilboardModel = new InstancedBilboardModel(factory, "teste", "Textures/Smoke", new BilboardInstance[] {new BilboardInstance() }, maximumParticles);
            PhysxFluidObject PhysxFluidObject = new PloobsEngine.Physics.PhysxFluidObject(fluidDesc);
            DeferredInstancedBilboardShader DeferredInstancedBilboardShader = new PloobsEngine.Material.DeferredInstancedBilboardShader(BilboardType.Spherical);
            DeferredInstancedBilboardShader.AlphaTestLimit = 0.2f;
            FluidMaterial DeferredMaterial = new FluidMaterial(DeferredInstancedBilboardShader,maximumParticles,new Vector2(0.2f));
            IObject IObject = new IObject(DeferredMaterial, InstancedBilboardModel, PhysxFluidObject);
            this.World.AddObject(IObject);


            // Ledge
            {
                var boxShapeDesc = new BoxShapeDescription(5, 0.1f, 5);
                SimpleModel SimpleModel = new PloobsEngine.Modelo.SimpleModel(factory, "Model/block");
                SimpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);

                PhysxPhysicObject PhysxPhysicObject = new PloobsEngine.Physics.PhysxPhysicObject(boxShapeDesc,
                    (Phyx.Matrix.RotationX(-0.5f) * Phyx.Matrix.Translation(-40, 5, -52)).AsXNA(),new Vector3(5,0.1f,5));
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, SimpleModel, PhysxPhysicObject);
                this.World.AddObject(obj);
            }

            // Drain
            {
                var boxShapeDesc = new BoxShapeDescription(5, 0.1f, 5);
                boxShapeDesc.Flags |= ShapeFlag.FluidDrain;

                var drainActorDesc = new ActorDescription()
                {
                    GlobalPose = Phyx.Matrix.Translation(-40, 0, -55),
                    Shapes = { boxShapeDesc }
                };

                var drianActor = PhysxPhysicWorld.Scene.CreateActor(drainActorDesc);
            }

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


            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory,false);

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }


    }
}
