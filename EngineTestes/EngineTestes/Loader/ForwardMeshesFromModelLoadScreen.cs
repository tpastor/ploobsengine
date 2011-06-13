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
    public class ForwardMeshesFromModelLoadScreen : IScene
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
        }        

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            ExtractMeshesFromMoldelLoader ext = new ExtractMeshesFromMoldelLoader();
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "Model//leonScene");
            WorldLoader wl = new WorldLoader();
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);            
            wl.LoadWorld(factory, GraphicInfo, World, data);
                        
            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo.Viewport);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());
        }

        IObject wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation mi)
        {            
            IModelo model = new CustomModel(factory, mi.modelName, new BatchInformation[] { mi.batchInformation}, mi.difuse, mi.bump, mi.specular, mi.glow);
            IPhysicObject po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());            
            IShader shader = new ForwardXNABasicShader();
            ForwardMaterial dm = new ForwardMaterial(shader);
            return new IObject(dm, model, po);
        }

    }
}
