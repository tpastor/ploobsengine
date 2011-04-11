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

namespace EngineTestes
{
    public class DeferredLoadScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic[] renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic[] { new DeferredRenderTechnic(desc) };
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

            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ModelLoaderData data= ext.Load(factory, GraphicInfo, "shadow");
            WorldLoader wl = new WorldLoader();
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);
            wl.LoadWorld(factory, GraphicInfo, World, data);

            ///Create the water
            ///Water is just a Shader that Creats a Water like texture and binds it to a model
            {
                IModelo sm = new SimpleModel(factory, "Model\\block");
                Matrix trans = Matrix.CreateTranslation(new Vector3(100, 50, 0));
                Plane plano = Plane.Transform(new Plane(0, 1, 0, 0), trans);
                IPhysicObject pi = new BoxObject(Vector3.Zero, 1, 1, 1, 5, new Vector3(1300, 0.1f, 1300), trans, MaterialDescription.DefaultBepuMaterial());
                pi.isMotionLess = true;
                ///Water shader, will refract and reflect according to the plano passed in the parameter
                ///Using default Parameters, there are lots of things that can be changed. See WaterCompleteShader 
                WaterCompleteShader shader = new WaterCompleteShader(800, 600, plano, 0,false);             
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj4 = new IObject(mat, sm, pi);
                this.World.AddObject(obj4);
            }

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

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grasscube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }

        IObject wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ModelInformation mi)
        {
            foreach (var item in mi.batchInformation)
            {
                item.ModelLocalTransformation = item.ModelLocalTransformation * Matrix.CreateScale(0.5f);
            }
            return WorldLoader.CreateOBJ(world, factory, ginfo, mi);
        }

    }
}
