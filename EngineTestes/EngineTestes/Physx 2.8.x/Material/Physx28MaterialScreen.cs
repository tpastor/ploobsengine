
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

namespace EngineTestes
{
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class Physx28MaterialScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            PhysxPhysicWorld PhysxPhysicWorld = new PhysxPhysicWorld(new Vector3(0,-10,0));            
            world = new IWorld(PhysxPhysicWorld, new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            PhysxPhysicWorld PhysxPhysicWorld = World.PhysicWorld as PhysxPhysicWorld;

            base.LoadContent(GraphicInfo, factory, contentManager);
            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");

                StillDesign.PhysX.Material material1 = PhysxPhysicWorld.CreatePhysicMaterial(
                    new StillDesign.PhysX.MaterialDescription()
                    {
                        Restitution = 0.3f,
                        DynamicFriction = 0.5f,
                        StaticFriction = 1,
                    }
                    );
                PhysxTriangleMesh tmesh = new PhysxTriangleMesh(PhysxPhysicWorld, simpleModel,
                    Matrix.Identity, Vector3.One, 1, material1);

                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);
            this.AttachCleanUpAble(BallThrowBullet);

            for (int i = 0; i < 10; i++)
            {

                StillDesign.PhysX.Material material2 = PhysxPhysicWorld.CreatePhysicMaterial(
                        new StillDesign.PhysX.MaterialDescription()
                        {
                            Restitution = PloobsEngine.Utils.StaticRandom.RandomBetween(0,1),
                            DynamicFriction = PloobsEngine.Utils.StaticRandom.RandomBetween(0, 1),
                            StaticFriction = PloobsEngine.Utils.StaticRandom.RandomBetween(0, 1),                            
                            RestitutionCombineMode = CombineMode.Max,
                        }
                        );

                ///Load a Model with a custom texture
                SimpleModel sm2 = new SimpleModel(factory, "Model\\ball");
                sm2.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Green, false), TextureType.DIFFUSE);
                
                ForwardXNABasicShader nd = new ForwardXNABasicShader();
                IMaterial m = new ForwardMaterial(nd);
                
                SphereShapeDescription SphereGeometry = new SphereShapeDescription(5f);
                SphereGeometry.Material = material2;

                PhysxPhysicObject PhysxPhysicObject = new PhysxPhysicObject(SphereGeometry,
                    0.5f, Matrix.CreateTranslation(new Vector3(100,100,50 * i)), Vector3.One * 5f);

                IObject o = new IObject(m, sm2, PhysxPhysicObject);
                this.World.AddObject(o);
            }
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

    }
}
