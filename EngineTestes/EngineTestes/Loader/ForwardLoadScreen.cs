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
            
            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());

        }

        ILight wl_OnCreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li)
        {
            return null;
        }

        IObject[] wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation[] oi)
        {
            IObject[] objs = new IObject[oi.Count()];
            int i = 0;
            foreach (var mi in oi)
            {
                mi.batchInformation.ModelLocalTransformation = Matrix.Identity;
                IModelo model = new CustomModel(factory, mi.modelName, mi.batchInformation, mi.textureInformation, mi.meshIndex, mi.meshPartIndex);
                //IPhysicObject po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                GhostObject po = new GhostObject(mi.position, Matrix.CreateFromQuaternion(mi.rotation), mi.scale);
                IShader shader = new ForwardXNABasicShader();
                ForwardMaterial dm = new ForwardMaterial(shader);
                IObject obj = new IObject(dm, model, po);
                objs[i++] = obj;
            }

            return objs;
        }

    }
}
