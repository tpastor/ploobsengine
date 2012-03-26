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
    public class FlagCloth28Screen : IScene
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

            ///grid
            var grid = VertexGrid.CreateGrid(100, 100, 0.3f, 100, 100);
            
            ///model
            ClothModel ClothModel = new PloobsEngine.Modelo.ClothModel(factory, PhysxPhysicWorld,
                new ClothMeshDescription(), grid.Points,grid.TextCoords,grid.Indices, "Textures//meiofio");

            ///description
            ///NOW WITH WIND =P
            var clothDesc = new ClothDescription()
            {
                Friction = 0.5f,
                ClothMesh = ClothModel.ClothMesh,
                Flags = ClothFlag.Bending | ClothFlag.CollisionTwoway | ClothFlag.Visualization ,
                Thickness = 0.2f,                
                WindAcceleration = new StillDesign.PhysX.MathPrimitives.Vector3(10,0,10)
            };            
                        clothDesc.MeshData.AllocatePositions<Vector3>(grid.Points.Length );
            clothDesc.MeshData.AllocateIndices<int>(grid.Indices.Length );
            clothDesc.MeshData.AllocateNormals<Vector3>(grid.Points.Length );

            clothDesc.MeshData.MaximumVertices = grid.Points.Length ;
            clothDesc.MeshData.MaximumIndices = grid.Indices.Length ;

            clothDesc.MeshData.NumberOfVertices = grid.Points.Length ;
            clothDesc.MeshData.NumberOfIndices = grid.Indices.Length ;            
            
            PhysxClothObject PhysxClothObject = new PloobsEngine.Physics.PhysxClothObject(clothDesc,
                                                Matrix.CreateRotationX((float)Math.PI / 2f) * Matrix.CreateTranslation(0,10,0));

            
            ForwardXNABasicShader ForwardXNABasicShader = new PloobsEngine.Material.ForwardXNABasicShader();
            ClothMaterial ClothMaterial = new ClothMaterial(ForwardXNABasicShader);
            IObject IObject = new PloobsEngine.SceneControl.IObject(ClothMaterial, ClothModel, PhysxClothObject);
            
            World.AddObject(IObject);

            ForwardXNABasicShader.BasicEffect.EnableDefaultLighting();
            
                ///TO HOLD !!!            
                CapsuleShapeDescription CapsuleShapeDescription = new StillDesign.PhysX.CapsuleShapeDescription();
                CapsuleShapeDescription.Height = 100;
                CapsuleShapeDescription.Radius = 0.15f;
                CapsuleShapeDescription.LocalPosition = new StillDesign.PhysX.MathPrimitives.Vector3(0, 0.15f + 0.5f * 10, 0);
                
                var actorDesc = new ActorDescription()
                {
                    GlobalPose = StillDesign.PhysX.MathPrimitives.Matrix.Translation(0,-0.2f,0),
                    Shapes = { CapsuleShapeDescription }
                };

                var actor = PhysxPhysicWorld.Scene.CreateActor(actorDesc);

                PhysxClothObject.Cloth.AttachToShape(actor.Shapes.First(), (ClothAttachmentFlag)0);                
            
                        
            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);
            this.AttachCleanUpAble(BallThrowBullet);
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            ///must be called before
            base.Draw(gameTime, render);

            ///Draw some text to the screen
            render.RenderTextComplete("Demo: Physx Simple Flag", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Just a Cloth with Wind =P", new Vector2(20, 35), Color.White, Matrix.Identity);
        }



    }
}

