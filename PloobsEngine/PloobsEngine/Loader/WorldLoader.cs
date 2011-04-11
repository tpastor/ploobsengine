using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Light;
using PloobsEngine.Utils;
using PloobsEngine.Engine;
using PloobsEngine.Modelo;
using PloobsEngine.Engine.Logger;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;

namespace PloobsEngine.Loader
{
    public delegate IObject CreateIObject(IWorld world, GraphicFactory factory,GraphicInfo ginfo, ModelInformation mi);
    public delegate ILight CreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li);
    public delegate ICamera CreateICamera(IWorld world, GraphicFactory factory, GraphicInfo ginfo, CameraInfo cinfo);
    public delegate void ProcessDummies(IWorld world, DummyInfo dinfo);

    public class WorldLoader
    {
        public static IObject CreateOBJ(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ModelInformation mi)
        {
            IModelo model = new CustomModel(factory, mi.ModelName, mi.batchInformation, mi.difuse, mi.bump, mi.specular, mi.glow);
            IPhysicObject po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            IShader shader = new CustomDeferred(mi.HasTexture(TextureType.GLOW), mi.HasTexture(TextureType.BUMP), mi.HasTexture(TextureType.SPECULAR), mi.HasTexture(TextureType.PARALAX));
            DeferredMaterial dm = new DeferredMaterial(shader);
            return new IObject(dm,model,po);
        }

        public static ICamera CreateCamera(IWorld world, GraphicFactory factory, GraphicInfo ginfo, CameraInfo cinfo)
        {
            CameraStatic cm = new CameraStatic(cinfo.Position, cinfo.Target);
            cm.Name = cinfo.Name;
            return cm;
        }

        public event CreateIObject OnCreateIObject = null;
        public event CreateILight OnCreateILight = null;
        public event CreateICamera OnCreateICamera = null;
        public event ProcessDummies OnProcessDummies = null;

        public void LoadWorld(GraphicFactory factory, GraphicInfo ginfo, IWorld world, ModelLoaderData worldData)
        {
            foreach (var item in worldData.ModelMeshesInfo)
	        {
                if (OnCreateIObject != null)
                {
                    IObject obj = OnCreateIObject(world, factory, ginfo, item);
                    if (obj != null)
                    {
                        world.AddObject(obj);
                    }
                }
                else
                {
                    world.AddObject(WorldLoader.CreateOBJ(world, factory, ginfo, item));
                }

	        }

            foreach (var item in worldData.LightsInfo)
            {
                if (OnCreateILight != null)
                {
                    ILight l = OnCreateILight(world, factory, ginfo, item);
                    if (l != null)
                        world.AddLight(l);
                }
                else
                {
                    world.AddLight(item);
                }
            }

            foreach (var item in worldData.CameraInfo)
            {
                if (OnCreateICamera != null)
                {
                    ICamera cam = OnCreateICamera(world, factory, ginfo, item);
                    if (cam != null)
                        world.CameraManager.AddCamera(cam);
                }
                else
                {
                    world.CameraManager.AddCamera(WorldLoader.CreateCamera(world, factory, ginfo, item));
                }
            }

            foreach (var item in worldData.DummyInfo)
            {
                if (OnProcessDummies != null)
                    OnProcessDummies(world, item);
            }
        }
    }
        
}
