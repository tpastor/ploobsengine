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
using System.IO;
using System.Runtime.InteropServices;

namespace AdvancedDemo4._0
{
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class BoxCloth28Screen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            PhysxPhysicWorld PhysxPhysicWorld = new PhysxPhysicWorld(new Vector3(0, -10, 0), true);
            world = new IWorld(PhysxPhysicWorld, new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }


        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            PhysxPhysicWorld PhysxPhysicWorld = World.PhysicWorld as PhysxPhysicWorld;

            base.LoadContent(GraphicInfo, factory, contentManager);


            ///Cosntruct a grid of vertices to make a cloth
            int w = 50;
            int h = 50;

            float hw = w / 2.0f;
            float hh = h / 2.0f;

            Vector3 p = new Vector3(0, 70, 0);

            var grid = VertexGrid.CreateGrid(w, h);

            ///Cloth Model (Code inside the demo)
            ClothModel ClothModel = new PloobsEngine.Modelo.ClothModel(factory, PhysxPhysicWorld,
                new ClothMeshDescription(), grid.Points, grid.TextCoords, grid.Indices, "Textures//fabric");

            ///Cloth Description
            var clothDesc = new ClothDescription()
            {
                Friction = 0.5f,
                ClothMesh = ClothModel.ClothMesh,
                Flags = ClothFlag.Gravity | ClothFlag.Bending | ClothFlag.CollisionTwoway | ClothFlag.Visualization | ClothFlag.SelfCollision,
                Thickness = 0.5f,
            };

            ///Adding Cloth Vertices
            clothDesc.MeshData.AllocatePositions<Vector3>(grid.Points.Length);
            clothDesc.MeshData.AllocateIndices<int>(grid.Indices.Length);
            clothDesc.MeshData.AllocateNormals<Vector3>(grid.Points.Length);

            clothDesc.MeshData.MaximumVertices = grid.Points.Length;
            clothDesc.MeshData.MaximumIndices = grid.Indices.Length;

            clothDesc.MeshData.NumberOfVertices = grid.Points.Length;
            clothDesc.MeshData.NumberOfIndices = grid.Indices.Length;




            ///Cloth Physic Model
            PhysxClothObject PhysxClothObject = new PloobsEngine.Physics.PhysxClothObject(clothDesc,
                                                Matrix.CreateTranslation(-hw, 0, -hh) * Matrix.CreateTranslation(p));





            ForwardXNABasicShader ForwardXNABasicShader = new PloobsEngine.Material.ForwardXNABasicShader();
            ClothMaterial ClothMaterial = new ClothMaterial(ForwardXNABasicShader);
            IObject IObject = new PloobsEngine.SceneControl.IObject(ClothMaterial, ClothModel, PhysxClothObject);

            World.AddObject(IObject);


            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//table", "Textures//wood_dark");

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
                shader.BasicEffect.EnableDefaultLighting();
                this.World.AddObject(obj);
            }
            {

                StillDesign.PhysX.Material material2 = PhysxPhysicWorld.CreatePhysicMaterial(
                       new StillDesign.PhysX.MaterialDescription()
                       {

                           Restitution = PloobsEngine.Utils.StaticRandom.RandomBetween(0, 1),
                           DynamicFriction = PloobsEngine.Utils.StaticRandom.RandomBetween(0, 1),
                           StaticFriction = PloobsEngine.Utils.StaticRandom.RandomBetween(0, 1),
                           RestitutionCombineMode = CombineMode.Max,
                       }
                       );
                {
                    SimpleModel sm2 = new SimpleModel(factory, "Model\\ball");
                    sm2.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Green, false), TextureType.DIFFUSE);

                    ForwardXNABasicShader nd = new ForwardXNABasicShader();
                    IMaterial m = new ForwardMaterial(nd);

                    SphereShapeDescription SphereGeometry = new SphereShapeDescription(5);
                    SphereGeometry.Material = material2;

                    PhysxPhysicObject PhysxPhysicObject = new PhysxPhysicObject(SphereGeometry,
                        0.5f, Matrix.CreateTranslation(new Vector3(0, 50, 0)), Vector3.One * 5f);

                    IObject o = new IObject(m, sm2, PhysxPhysicObject);
                    nd.BasicEffect.EnableDefaultLighting();
                    this.World.AddObject(o);
                    PhysxPhysicObject.isMotionLess = true;
                }
            }



            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);
            BallThrowBullet.ballSize = 1;
            BallThrowBullet.Speed = 20;
            this.AttachCleanUpAble(BallThrowBullet);
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            ///must be called before
            base.Draw(gameTime, render);

            ///Draw some text to the screen
            render.RenderTextComplete("Physx Simple Cloth ", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("not Physically perfect, but good enough for Real Time ", new Vector2(20, 35), Color.White, Matrix.Identity);
        }


    }
}

