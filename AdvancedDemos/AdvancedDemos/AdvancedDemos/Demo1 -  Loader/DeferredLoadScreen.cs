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
    public class DeferredLoadScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
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

            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "leonScene");
            WorldLoader wl = new WorldLoader();
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);
            wl.OnCreateILight += new CreateILight(wl_OnCreateILight);
            wl.OnCreateICamera += new CreateICamera(wl_OnCreateICamera);
            wl.LoadWorld(factory, GraphicInfo, World, data);

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

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo.Viewport);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            //SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grasscube");
            //CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);


            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());

        }

        ICamera wl_OnCreateICamera(IWorld world, GraphicFactory factory, GraphicInfo ginfo, CameraInfo cinfo)
        {
            return null;
        }

        ILight wl_OnCreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li)
        {
            return null;
        }

        IObject wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ModelInformation mi)
        {            
            return WorldLoader.CreateOBJ(world, factory, ginfo, mi);
        }

    }
}
