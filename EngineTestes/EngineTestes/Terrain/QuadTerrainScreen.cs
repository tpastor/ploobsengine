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

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
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
            QuadTerrain q = new PloobsEngine.Material.QuadTerrain(factory, tMap,65, 1025, 1, 1f);

            //Set various terrain stats.
            q.diffuseScale = q.flatScale / 4;
            q.detailScale = q.flatScale / 10;
            q.detailMapStrength = 2;
            q.textureBlend = factory.GetTexture2D("Textures//hmap_256blend");
            q.textureDetail = factory.GetTexture2D("Textures//coolgrass2DOT3");
            q.textureRed = factory.GetTexture2D("Textures//TexR");
            q.textureGreen = factory.GetTexture2D("Textures//TexG");
            q.textureBlue = factory.GetTexture2D("Textures//TexB");
            q.textureBlack = factory.GetTexture2D("Textures//TexBase");
            
            q.sunlightVector = Vector3.Normalize(new Vector3(.5f, .5f, .8f));
            q.sunlightColour = new Vector3(2.3f, 2f, 1.8f);

            ForwardTerrainMaterial mat = new ForwardTerrainMaterial(q);
            TerrainObject to = new TerrainObject(factory,Vector3.Zero, Matrix.Identity, q.getHeightMap() ,MaterialDescription.DefaultBepuMaterial());            
                        
            IObject obj3 = new IObject(mat, null, to);
            this.World.AddObject(obj3);

            LightThrowBepu lt = new LightThrowBepu(this.World, factory);

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


            this.World.CameraManager.AddCamera(CameraFirstPerson);
            

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

    }
}
