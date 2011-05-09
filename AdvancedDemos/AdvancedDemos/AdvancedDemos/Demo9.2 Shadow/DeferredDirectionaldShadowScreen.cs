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
using PloobsEngine.Loader;
using PloobsEngine.Input;
using PloobsEngine.Utils;

namespace AdvancedDemo4._0
{
    public class DeferredDirectionaldShadowScreen : IScene
    {

        LightThrowBepu lt;
        Vec3Interpolator inter = new Vec3Interpolator();
        DirectionalLightPE ld1;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
            ShadowLightMap css = new ShadowLightMap(ShadowFilter.PCF7x7SOFT, 2048);
            desc.DeferredLightMap = css;
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

            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "shadow");
            WorldLoader wl = new WorldLoader();
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);
            wl.OnCreateILight += new CreateILight(wl_OnCreateILight);
            wl.LoadWorld(factory, GraphicInfo, World, data);
        
            lt = new LightThrowBepu(this.World, factory);

            #region NormalLight            
            ld1 = new DirectionalLightPE(new Vector3(0.2f, -1, 0.2f), Color.White);
            ld1.CastShadown = true;
            float li = 0.9f;
            ld1.LightIntensity = li;
            this.World.AddLight(ld1);            
            #endregion

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo.Viewport);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grasscube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

            ///Interpolator to change lightDirection
            inter.Start(new Vector3(0, -1, 0), new Vector3(1, -1, 1), 3, true);

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());

        }

        ILight wl_OnCreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li)
        {
            return null;
        }

        IObject wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ModelInformation mi)
        {

            mi.batchInformation.ModelLocalTransformation = mi.batchInformation.ModelLocalTransformation * Matrix.CreateScale(0.5f);
            
            return WorldLoader.CreateOBJ(world, factory, ginfo, mi);
        }

        protected override void Update(GameTime gameTime)
        {
            inter.Update(gameTime);
            ld1.LightDirection = inter.CurrentValue;

            base.Update(gameTime);
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
            render.RenderTextComplete("Directional Shadow Sample using cascade Shadow Mapping", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("This sample uses PCF7x7 filter with soft edges, it is heavy", new Vector2(10, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("The Shadow Map resolution is 2048X2048 --big ", new Vector2(10, 55), Color.White, Matrix.Identity);
        }

    }
}
