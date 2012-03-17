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

namespace EngineTestes
{
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class PressureCloth28Screen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            PhysxPhysicWorld PhysxPhysicWorld = new PhysxPhysicWorld(new Vector3(0,-10,0),true);            
            world = new IWorld(PhysxPhysicWorld, new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            PhysxPhysicWorld PhysxPhysicWorld = World.PhysicWorld as PhysxPhysicWorld;

            base.LoadContent(GraphicInfo, factory, contentManager);

            for (int i = 0; i < 3; i++)
            {
                ClothMeshDescription ClothMeshDescription = new ClothMeshDescription();
                ClothMeshDescription.WeldingDistance = 0.0001f;
                ClothMeshDescription.Flags = (MeshFlag)(int)ClothMeshFlag.WeldVertices;


                ClothModel ClothModel = new PloobsEngine.Modelo.ClothModel(factory, PhysxPhysicWorld,
                    ClothMeshDescription, "Model//ball", Vector3.One * 25, "Textures//meiofio");

                var clothDesc = new ClothDescription()
                {
                    Friction = 0.5f,
                    ClothMesh = ClothModel.ClothMesh,
                    Pressure = 0.9f,
                    Flags = ClothFlag.Bending | ClothFlag.CollisionTwoway | ClothFlag.Visualization | ClothFlag.Pressure | ClothFlag.Gravity | ClothFlag.SelfCollision | ClothFlag.TriangleCollision,
                    Thickness = 0.4f,
                };


                clothDesc.MeshData.AllocatePositions<Vector3>(ClothModel.VerticesNum);
                clothDesc.MeshData.AllocateIndices<int>(ClothModel.IndicesNum);
                clothDesc.MeshData.AllocateNormals<Vector3>(ClothModel.VerticesNum);

                clothDesc.MeshData.MaximumVertices = ClothModel.VerticesNum;
                clothDesc.MeshData.MaximumIndices = ClothModel.IndicesNum;

                clothDesc.MeshData.NumberOfVertices = ClothModel.VerticesNum;
                clothDesc.MeshData.NumberOfIndices = ClothModel.IndicesNum;

                PhysxClothObject PhysxClothObject = new PloobsEngine.Physics.PhysxClothObject(clothDesc,
                                                    Matrix.CreateTranslation(100, 50, i * 100));


                ForwardXNABasicShader ForwardXNABasicShader = new PloobsEngine.Material.ForwardXNABasicShader();
                ClothMaterial ClothMaterial = new ClothMaterial(ForwardXNABasicShader);
                //ClothMaterial.RasterizerState.FillMode = FillMode.WireFrame;
                IObject IObject = new PloobsEngine.SceneControl.IObject(ClothMaterial, ClothModel, PhysxClothObject);
                World.AddObject(IObject);

                ForwardXNABasicShader.BasicEffect.EnableDefaultLighting();
            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Blue), TextureType.DIFFUSE);

                BoxShapeDescription SphereGeometry = new BoxShapeDescription(1000, 5, 1000);
                PhysxPhysicObject PhysxPhysicObject = new PhysxPhysicObject(SphereGeometry,
                    Matrix.Identity, new Vector3(1000, 5, 1000));

                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, PhysxPhysicObject);
                this.World.AddObject(obj);

                shader.BasicEffect.EnableDefaultLighting();
            }


            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);
            this.AttachCleanUpAble(BallThrowBullet);
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }


    }
}

