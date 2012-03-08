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
using PloobsEngine.Physics.Bepu;

namespace EngineTestes
{
    public class PhysxTerrain28Screen : IScene
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

            Texture2D tMap = factory.GetTexture2D("Textures//hmap_10243");
            QuadTerrain q = new PloobsEngine.Material.QuadTerrain(factory, tMap, 33, 513, 10, 3f);

            ForwardTerrainMaterial mat = new ForwardTerrainMaterial(q);

            //Set various terrain stats.
            mat.diffuseScale = q.flatScale / 4;
            mat.detailScale = q.flatScale / 100;
            mat.detailMapStrength = 2;
            mat.textureBlend = factory.GetTexture2D("Textures//hmap_256blend");
            mat.textureDetail = factory.GetTexture2D("Textures//coolgrass2DOT3");
            mat.textureRed = factory.GetTexture2D("Textures//TexR");
            mat.textureGreen = factory.GetTexture2D("Textures//TexG");
            mat.textureBlue = factory.GetTexture2D("Textures//TexB");
            mat.textureBlack = factory.GetTexture2D("Textures//TexBase");

            mat.sunlightVector = Vector3.Normalize(new Vector3(.5f, .5f, .8f));
            mat.sunlightColour = new Vector3(2.3f, 2f, 1.8f);
            float[,] f = q.getHeightMap();
            int v = 513;
            var samples = new HeightFieldSample[v * v];

            for (int r = 0; r < v; r++)
            {
                for (int c = 0; c < v ; c++)
                {
                    var sample = new HeightFieldSample()
                    {
                        Height = (short)f[r, c],                        
                        //Height = 5,                        
                        TessellationFlag = 1,
                        MaterialIndex0 = 1,
                        MaterialIndex1 = 0
                    };

                    samples[r * v + c] = sample;
                }
            }

            var heightFieldDesc = new HeightFieldDescription()
            {
                NumberOfRows = v,
                NumberOfColumns = v,                                
                ConvexEdgeThreshold = 0f,

            };
            heightFieldDesc.SetSamples(samples);
            
            var heightField = PhysxPhysicWorld.Core.CreateHeightField(heightFieldDesc);
            
            heightFieldDesc.Dispose();

            var heightFieldShapeDesc = new HeightFieldShapeDescription()
            {
                HeightField = heightField,
                HeightScale = 1.0f,
                RowScale = 1,
                ColumnScale = 1,
            };
            heightFieldShapeDesc.LocalPose = StillDesign.PhysX.MathPrimitives.Matrix.Identity;

            var actorDesc = new ActorDescription()
            {
                GlobalPose = Matrix.Identity.AsPhysX(),
                Shapes = { heightFieldShapeDesc },                
            };

            var actor = PhysxPhysicWorld.Scene.CreateActor(actorDesc);

            IObject obj3 = new IObject(mat, null, new PhysxGhostObject());
            this.World.AddObject(obj3);            
            
            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);

            CameraFirstPerson CameraFirstPerson = new CameraFirstPerson(GraphicInfo);
            CameraFirstPerson.FarPlane = 20000;
            this.World.CameraManager.AddCamera(CameraFirstPerson);
        }

    }
}
