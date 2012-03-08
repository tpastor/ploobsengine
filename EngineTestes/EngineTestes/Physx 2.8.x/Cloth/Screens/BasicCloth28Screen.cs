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
    public class BasicCloth28Screen : IScene
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

            int w = 25;
            int h = 25;

            float hw = w / 2.0f;
            float hh = h / 2.0f;

            Vector3 p = new Vector3(0, 20, 0);
            

            var grid = VertexGrid.CreateGrid(w, h);
            
            ClothModel ClothModel = new PloobsEngine.Modelo.ClothModel(factory, PhysxPhysicWorld,
                new ClothMeshDescription(), grid.Points,grid.TextCoords,grid.Indices, "Textures//meiofio");

            var clothDesc = new ClothDescription()
            {
                Friction = 0.5f,
                ClothMesh = ClothModel.ClothMesh,
                Flags = ClothFlag.Gravity | ClothFlag.Bending | ClothFlag.CollisionTwoway | ClothFlag.Visualization ,
                Thickness = 0.2f,                
            };            
            
            clothDesc.MeshData.AllocatePositions<Vector3>(grid.Points.Length );
            clothDesc.MeshData.AllocateIndices<int>(grid.Indices.Length );
            clothDesc.MeshData.AllocateNormals<Vector3>(grid.Points.Length );

            clothDesc.MeshData.MaximumVertices = grid.Points.Length ;
            clothDesc.MeshData.MaximumIndices = grid.Indices.Length ;

            clothDesc.MeshData.NumberOfVertices = grid.Points.Length ;
            clothDesc.MeshData.NumberOfIndices = grid.Indices.Length ;            
            
            PhysxClothObject PhysxClothObject = new PloobsEngine.Physics.PhysxClothObject(clothDesc,
                                                Matrix.CreateTranslation(-hw, 0, -hh) * Matrix.CreateTranslation(p));
            ForwardXNABasicShader ForwardXNABasicShader = new PloobsEngine.Material.ForwardXNABasicShader();
            ClothMaterial ClothMaterial = new ClothMaterial(ForwardXNABasicShader);
            IObject IObject = new PloobsEngine.SceneControl.IObject(ClothMaterial, ClothModel, PhysxClothObject);
            
            World.AddObject(IObject);



            // Four corner boxes to hold it in place
            var positions = new[]
			{
				new StillDesign.PhysX.MathPrimitives.Vector3(0, 0, -hh), // Back
				new StillDesign.PhysX.MathPrimitives.Vector3(0, 0, hh), // Front
				new StillDesign.PhysX.MathPrimitives.Vector3(-hw, 0, 0), // Left
				new StillDesign.PhysX.MathPrimitives.Vector3(hw, 0, 0), // Right
			};

            var sizes = new[]
			{
				new StillDesign.PhysX.MathPrimitives.Vector3(w, 1, 1), // Back
				new StillDesign.PhysX.MathPrimitives.Vector3(w, 1, 1), // Front
				new StillDesign.PhysX.MathPrimitives.Vector3(1, 1, h), // Left
				new StillDesign.PhysX.MathPrimitives.Vector3(1, 1, h), //Right
			};

            ///pra preender
            for (int i = 0; i < 2; i++)
            {
                var actorDesc = new ActorDescription()
                {
                    GlobalPose = StillDesign.PhysX.MathPrimitives.Matrix.Translation(positions[i] + p.AsPhysX()),
                    Shapes = { new BoxShapeDescription(sizes[i]) }
                };

                var actor = PhysxPhysicWorld.Scene.CreateActor(actorDesc);


                PhysxClothObject.Cloth.AttachToShape(actor.Shapes.First(), (ClothAttachmentFlag)0);                
            }
                        
            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }


    }
}

