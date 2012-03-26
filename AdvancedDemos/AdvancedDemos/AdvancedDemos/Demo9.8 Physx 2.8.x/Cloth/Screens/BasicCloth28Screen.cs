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


            ///Cosntruct a grid of vertices to make a cloth
            int w = 25;
            int h = 25;

            float hw = w / 2.0f;
            float hh = h / 2.0f;

            Vector3 p = new Vector3(0, 20, 0);            

            var grid = VertexGrid.CreateGrid(w, h);
            
            ///Cloth Model (Code inside the demo)
            ClothModel ClothModel = new PloobsEngine.Modelo.ClothModel(factory, PhysxPhysicWorld,
                new ClothMeshDescription(), grid.Points,grid.TextCoords,grid.Indices, "Textures//meiofio");

            ///Cloth Description
            var clothDesc = new ClothDescription()
            {
                Friction = 0.5f,
                ClothMesh = ClothModel.ClothMesh,
                Flags = ClothFlag.Gravity | ClothFlag.Bending | ClothFlag.CollisionTwoway | ClothFlag.Visualization ,
                Thickness = 0.2f,                
            };            
            
            ///Adding Cloth Vertices
            clothDesc.MeshData.AllocatePositions<Vector3>(grid.Points.Length );
            clothDesc.MeshData.AllocateIndices<int>(grid.Indices.Length );
            clothDesc.MeshData.AllocateNormals<Vector3>(grid.Points.Length );

            clothDesc.MeshData.MaximumVertices = grid.Points.Length ;
            clothDesc.MeshData.MaximumIndices = grid.Indices.Length ;

            clothDesc.MeshData.NumberOfVertices = grid.Points.Length ;
            clothDesc.MeshData.NumberOfIndices = grid.Indices.Length ;            
            

            ///Cloth Physic Model
            PhysxClothObject PhysxClothObject = new PloobsEngine.Physics.PhysxClothObject(clothDesc,
                                                Matrix.CreateTranslation(-hw, 0, -hh) * Matrix.CreateTranslation(p));

            ForwardXNABasicShader ForwardXNABasicShader = new PloobsEngine.Material.ForwardXNABasicShader();
            ClothMaterial ClothMaterial = new ClothMaterial(ForwardXNABasicShader);
            IObject IObject = new PloobsEngine.SceneControl.IObject(ClothMaterial, ClothModel, PhysxClothObject);
            
            World.AddObject(IObject);


            ///Fixing the Cloth to some fixed Points
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

            ///To Hold
            for (int i = 0; i < 2; i++)
            {
                ///Create e Actor DIRECTELY without a PLOOBS Object.
                ///Ploobs does not know it "exists"
                var actorDesc = new ActorDescription()
                {
                    GlobalPose = StillDesign.PhysX.MathPrimitives.Matrix.Translation(positions[i] + p.AsPhysX()),
                    Shapes = { new BoxShapeDescription(sizes[i]) }
                };

                ///When you create the actor, you are automagically adding it to the world
                var actor = PhysxPhysicWorld.Scene.CreateActor(actorDesc);

                ///Attach one to another
                PhysxClothObject.Cloth.AttachToShape(actor.Shapes.First(), (ClothAttachmentFlag)0);                
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
            render.RenderTextComplete("Demo: Physx Simple Cloth ", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("not Physically perfect, but good enough for Real Time ", new Vector2(20, 35), Color.White, Matrix.Identity);
        }


    }
}



#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

namespace PloobsEngine.Modelox
{
    /// <summary>
    /// Implementation of the most simple Model in the engine
    /// </summary>
    public class SimpleModelx : IModelo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleModel"/> class.
        /// </summary>
        /// <param name="factory">The graphic factory.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="diffuseTextureName">Name of the diffuse texture.</param>
        /// <param name="bumpTextureName">Name of the bump texture.</param>
        /// <param name="specularTextureName">Name of the specular texture.</param>
        /// <param name="glowTextureName">Name of the glow texture.</param>
        /// <param name="forceFromDisk">if set to <c>true</c> [force from disk].</param>
       public SimpleModelx(GraphicFactory factory,String modelName, String diffuseTextureName = null, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null, bool forceFromDisk = false)
            : base(factory, modelName, false)
        {
            this.forceFromDisk = forceFromDisk;            
            this._diffuseName = diffuseTextureName;
            this._bumpName = bumpTextureName;
            this._glowName = glowTextureName;
            this._specularName = specularTextureName;
            LoadModel(factory, out BatchInformations, out TextureInformations);
        }
       
        string _glowName = null;
        string _diffuseName = null;
        string _bumpName = null;
        string _specularName = null;
        private Model model;        
        private float modelRadius;
        bool forceFromDisk ;

        protected override void LoadModel(GraphicFactory factory, out BatchInformation[][] BatchInformations, out TextureInformation[][] TextureInformation)
        {
            model = factory.GetModel(this.Name, false, false);
            ModelBuilderHelper.Extract(factory,out BatchInformations, out TextureInformation,model,_diffuseName,_bumpName,_specularName,_glowName,false);            
            
            BoundingSphere sphere = new BoundingSphere();
            foreach (var item in model.Meshes)
            {
                sphere = BoundingSphere.CreateMerged(sphere, item.BoundingSphere);
            }
            modelRadius = sphere.Radius;         
        }

        public override int MeshNumber
        {
            get { return model.Meshes.Count; }
        }

        public override float GetModelRadius()
        {
            return modelRadius;
        }

        public override void CleanUp(GraphicFactory factory)
        {
            for (int i = 0; i < MeshNumber; i++)
            {
                foreach (var item in TextureInformations[i])
                {
                    factory.ReleaseAsset(item.DiffuseMapName);
                    factory.ReleaseAsset(item.BumpMapName);
                    factory.ReleaseAsset(item.GlowName);
                    factory.ReleaseAsset(item.SpecularMapName);
                }
            }
            factory.ReleaseAsset(Name);            
        }
    }
}
