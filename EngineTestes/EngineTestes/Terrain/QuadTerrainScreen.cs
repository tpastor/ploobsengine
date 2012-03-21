using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Features;
using PloobsEngine.Commands;
using PloobsEngine.Physic.PhysicObjects.BepuObject;
using PloobsEngine.Input;
using Microsoft.Xna.Framework.Graphics;

namespace EngineTestes
{
    public class QuadTerrainScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);

            InputAdvanced ia = new InputAdvanced();
            engine.AddComponent(ia);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            CameraFirstPerson CameraFirstPerson = new CameraFirstPerson(true, GraphicInfo);

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
            TerrainObject to = new TerrainObject(factory,Vector3.Zero, Matrix.Identity, q.getHeightMap() ,MaterialDescription.DefaultBepuMaterial());            
                        
            IObject obj3 = new IObject(mat, null, to);
            this.World.AddObject(obj3);

            LightThrowBepu lt = new LightThrowBepu(this.World, factory);
                        
            this.World.CameraManager.AddCamera(CameraFirstPerson);
            CameraFirstPerson.FarPlane = 20000;

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

    }
}
