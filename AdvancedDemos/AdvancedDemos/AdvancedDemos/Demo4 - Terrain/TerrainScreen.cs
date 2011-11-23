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

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Terrain Sample
    /// </summary>
    public class TerrainScreen : IScene
    {
        LightThrowBepu lt;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///with mult thread support
            world = new IWorld(new BepuPhysicWorld(-0.97f,true,1,true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;            
            renderTech = new DeferredRenderTechnic(desc);
        }
 
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);            
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            ///Create the Terrain Object
            ///Controls how the heigh map is loaded
            TerrainObject to = new TerrainObject(factory,"..\\Content\\Textures\\Untitled",Vector3.Zero,Matrix.Identity,MaterialDescription.DefaultBepuMaterial(),2,1);
            ///Create the Model using the Terrain Object. Here we pass the textures used, in our case we are using MultiTextured Terrain so we pass lots of textures
            TerrainModel stm = new TerrainModel(factory, to,"TerrainName","..\\Content\\Textures\\Terraingrass", "..\\Content\\Textures\\rock", "..\\Content\\Textures\\sand", "..\\Content\\Textures\\snow");
            ///Create the shader
            ///In this sample we passed lots of textures, each one describe a level in the terrain, the ground is the sand and grass. the hills are rocks and the "mountains" are snow
            ///They are interpolated in the shader, you can control how using the shader parameters exposed in the DeferredTerrainShader
            DeferredTerrainShader shader = new DeferredTerrainShader(TerrainType.MULTITEXTURE);
            ///the classic material
            DeferredMaterial mat = new DeferredMaterial(shader);
            IObject obj3 = new IObject(mat, stm, to);
            this.World.AddObject(obj3);

            lt = new LightThrowBepu(this.World, factory);
        
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

            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(30), MathHelper.ToRadians(-10), new Vector3(200, 150, 250), GraphicInfo.Viewport);
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());
        }

        protected override void CleanUp(EngineStuff engine)
        {
            lt.CleanUp();
            engine.RemoveComponent("SkyBox");

            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo 8-22:Terrain Sample", new Vector2(10, 15), Color.White, Matrix.Identity);         
        }


    }
}
