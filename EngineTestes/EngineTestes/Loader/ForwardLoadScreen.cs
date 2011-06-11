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

namespace EngineTestes
{
    public class ForwardLoadScreen : IScene
    {
                
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            InputAdvanced ia = new InputAdvanced();
            engine.AddComponent(ia);

        }        

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "leonScene");
            WorldLoader wl = new WorldLoader();
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);
            wl.OnCreateILight += new CreateILight(wl_OnCreateILight);
            wl.LoadWorld(factory, GraphicInfo, World, data);

            //IModelo model = new SimpleModel(factory, "Model/leonScene");
            //IPhysicObject po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            //GhostObject po = new GhostObject();
            //IShader shader = new ForwardXNABasicShader();
            //ForwardMaterial dm = new ForwardMaterial(shader);
            //this.World.AddObject(new IObject(dm, model, po));
        
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

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());

        }

        ILight wl_OnCreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li)
        {
            return null;
        }

        IObject wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation mi)
        {
            mi.batchInformation.ModelLocalTransformation = Matrix.Identity;
            IModelo model = new CustomModel(factory, mi.modelName, new BatchInformation[] { mi.batchInformation}, mi.difuse, mi.bump, mi.specular, mi.glow);
            //IPhysicObject po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            GhostObject po = new GhostObject(mi.position, Matrix.CreateFromQuaternion(mi.rotation), mi.scale);
            IShader shader = new ForwardXNABasicShader();
            ForwardMaterial dm = new ForwardMaterial(shader);
            return new IObject(dm, model, po);
        }

    }
}
