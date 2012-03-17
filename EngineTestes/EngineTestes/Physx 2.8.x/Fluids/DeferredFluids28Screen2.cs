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
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class DeferredFluids28Screen2 : IScene
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

            // Create a simple fluid description with fluids and visualization enabled
            FluidDescription fluidDesc = new FluidDescription()
            {
                Flags = FluidFlag.Enabled | FluidFlag.Visualization,
            };

            // Store our particle positions somewhere (as our particle generation function below generates and unknown number of particles at runtime we need a list instead of an array)
            var particlePositions = new List<Phyx.Vector3>();

            // Move all the particles by this offset
            Phyx.Vector3 position = new Phyx.Vector3(0, 20, 0);

            // Number of particles in the x, y and z directions
            int sideNum = 10;
            float distance = 1f;

            float radius = sideNum * distance * 0.5f;

            for (int i = 0; i < sideNum; i++)
            {
                for (int j = 0; j < sideNum; j++)
                {
                    for (int k = 0; k < sideNum; k++)
                    {
                        Phyx.Vector3 p = new Phyx.Vector3(i * distance, j * distance, k * distance);

                        if ((p - new Phyx.Vector3(radius, radius, radius)).Length() < radius)
                        {
                            p += position - new Phyx.Vector3(radius, radius, radius);

                            particlePositions.Add(p);
                        }
                    }
                }
            }

            // Allocate memory for the initial particle positions to be stored in
            // And then set the position buffer
            fluidDesc.InitialParticleData.AllocatePositionBuffer<Phyx.Vector3>(particlePositions.Count);
            fluidDesc.InitialParticleData.NumberOfParticles = particlePositions.Count;
            fluidDesc.InitialParticleData.PositionBuffer.SetData(particlePositions.ToArray());

            // Allocate memory for PhysX to store the position of each particle
            fluidDesc.ParticleWriteData.AllocatePositionBuffer<Phyx.Vector3>(particlePositions.Count);
            fluidDesc.ParticleWriteData.NumberOfParticles = particlePositions.Count;

            InstancedBilboardModel InstancedBilboardModel = new InstancedBilboardModel(factory, "teste", "Textures/Smoke", new BilboardInstance[] { new BilboardInstance() }, particlePositions.Count);
            PhysxFluidObject PhysxFluidObject = new PloobsEngine.Physics.PhysxFluidObject(fluidDesc);
            DeferredInstancedBilboardShader DeferredInstancedBilboardShader = new PloobsEngine.Material.DeferredInstancedBilboardShader(BilboardType.Spherical);
            DeferredInstancedBilboardShader.AlphaTestLimit = 0.2f;
            FluidMaterial DeferredMaterial = new FluidMaterial(DeferredInstancedBilboardShader, particlePositions.Count, new Vector2(0.2f));
            IObject IObject = new IObject(DeferredMaterial, InstancedBilboardModel, PhysxFluidObject);
            this.World.AddObject(IObject);


            // Ledge
            {
                var boxShapeDesc = new BoxShapeDescription(10, 0.2f, 10);
                SimpleModel SimpleModel = new PloobsEngine.Modelo.SimpleModel(factory, "Model/block");
                SimpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);

                PhysxPhysicObject PhysxPhysicObject = new PloobsEngine.Physics.PhysxPhysicObject(boxShapeDesc,
                    (Phyx.Matrix.RotationZ(0.3f) *
                        Phyx.Matrix.Translation(0, 5, 0)).AsXNA(), new Vector3(10, 0.2f, 10));
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, SimpleModel, PhysxPhysicObject);
                this.World.AddObject(obj);                        
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
            this.AttachCleanUpAble(BallThrowBullet);

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }


    }
}
