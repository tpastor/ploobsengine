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

namespace EngineTestes
{
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class Fluids28Screen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            PhysxPhysicWorld PhysxPhysicWorld = new PhysxPhysicWorld(new Vector3(0,-10,0));            
            world = new IWorld(PhysxPhysicWorld, new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            CPUSphericalBillboardComponent = new CPUSphericalBillboardComponent();
            engine.AddComponent(CPUSphericalBillboardComponent);
        }

        CPUSphericalBillboardComponent CPUSphericalBillboardComponent;
        Fluid fluid;
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
            
            fluid = PhysxPhysicWorld.Scene.CreateFluid(fluidDesc);

                       
            Texture2D tex = factory.GetTexture2D("Textures/Smoke");
            for (int i = 0; i < maximumParticles; i++)
            {
                Billboard3D Billboard3D = new Billboard3D(tex,Vector3.Zero,new Vector2(0.002f));
                Billboard3D.Enabled = false;
                CPUSphericalBillboardComponent.Billboards.Add(Billboard3D);
            }


            // Ledge
            {
                var boxShapeDesc = new BoxShapeDescription(5, 0.1f, 5);
                SimpleModel SimpleModel = new PloobsEngine.Modelo.SimpleModel(factory, "Model/block");
                SimpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);
                PhysxPhysicObject PhysxPhysicObject = new PloobsEngine.Physics.PhysxPhysicObject(boxShapeDesc,
                    (Phyx.Matrix.RotationX(-0.5f) * Phyx.Matrix.Translation(-40, 5, -52)).AsXNA(),new Vector3(5,0.1f,5));
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
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

            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);
            this.AttachCleanUpAble(BallThrowBullet);

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            Phyx.Vector3[] pos = fluid.ParticleWriteData.PositionBuffer.GetData<Phyx.Vector3>();            
            
            for (int i = 0; i < CPUSphericalBillboardComponent.Billboards.Count; i++)
			{
                if (i < fluid.ParticleWriteData.NumberOfParticles)
                {
                    CPUSphericalBillboardComponent.Billboards[i].Enabled = true;
                    CPUSphericalBillboardComponent.Billboards[i].Position = pos[i].AsXNA();
                }
                else
                {
                    CPUSphericalBillboardComponent.Billboards[i].Enabled = false;
                }
			}            

            base.Draw(gameTime, render);
        }

    }
}
