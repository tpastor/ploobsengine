#if !WINDOWS_PHONE
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
using PloobsEngine.Physic.Constraints;
using PloobsEngine.Physic.Constraints.BepuConstraint;


namespace PloobsEngine.Loader
{
    public delegate IObject[] CreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation[] mi);
    public delegate ILight CreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li);
    public delegate ICamera CreateICamera(IWorld world, GraphicFactory factory, GraphicInfo ginfo, CameraInfo cinfo);
    public delegate IPhysicConstraint CreateIConstraint(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ConstraintInfo cinfo, IObject o1, IObject o2);
    public delegate void ProcessDummies(IWorld world, DummyInfo dinfo);

    public class WorldLoader
    {
        /// <summary>
        /// Used to retrieve objects to
        /// </summary>
        private Dictionary<String, IObject> objects = new Dictionary<string, IObject>();

        public static IObject[] CreateOBJ(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation[] mi)
        {            

            IModelo model = new CustomModel(factory, mi);            

            MaterialDescription material;
            if (mi[0].staticfriction == -1 || mi[0].dinamicfriction == -1 || mi[0].ellasticity == -1)
            {
                material = MaterialDescription.DefaultBepuMaterial();
            }
            else
            {
                material = new MaterialDescription(mi[0].staticfriction, mi[0].dinamicfriction, mi[0].ellasticity);
            }

            IPhysicObject po;

            bool massflag = false;
            if (mi[0].mass == 0)
            {
                massflag = true;
                mi[0].mass = 0.5f;
            }

            BatchInformation binf = model.GetBatchInformation(0)[0];            

            BoundingBox bb;

            switch (mi[0].collisionType)
            {
                case "Cylinder":

                    binf.ModelLocalTransformation = Matrix.Identity;
                    bb = ModelBuilderHelper.CreateBoundingBoxFromModel(binf, model);
                    Vector3 len = bb.Max - bb.Min;

                    po = new CylinderObject(mi[0].position, len.Y, len.X / 2, mi[0].mass, Matrix.CreateFromQuaternion(mi[0].rotation), material);
                    
                    break;


                case "Sphere":
                    binf.ModelLocalTransformation = Matrix.Identity;
                    po = new SphereObject(mi[0].position, model.GetModelRadius(), mi[0].mass, mi[0].scale.X, material);
                    po.Rotation = Matrix.CreateFromQuaternion(mi[0].rotation);

                    break;


                case "Box":

                    bb = ModelBuilderHelper.CreateBoundingBoxFromModel(binf, model);

                    len = bb.Max - bb.Min;

                    po = new BoxObject(mi[0].position, len.X, len.Y, len.Z, mi[0].mass, mi[0].scale, Matrix.CreateFromQuaternion(mi[0].rotation), material);

                    break;
                default:
                    po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, new Vector3(1), material);
                    break;
            }

            po.isMotionLess = massflag;

            IShader shader = new DeferredCustomShader(mi[0].HasTexture(TextureType.GLOW), mi[0].HasTexture(TextureType.BUMP), mi[0].HasTexture(TextureType.SPECULAR), mi[0].HasTexture(TextureType.PARALAX));
            DeferredMaterial dm = new DeferredMaterial(shader);
            IObject ob = new IObject(dm, model, po);

            ob.Name = mi[0].modelName;

            return new IObject[] { ob };
        }


        public static IPhysicConstraint CreateConstraint(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ConstraintInfo cinfo, IObject o1, IObject o2)
        {
            PointPointConstraint con = new PointPointConstraint(cinfo.Position, o1.PhysicObject, o2.PhysicObject);
            return con;
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
        public event CreateIConstraint OnCreateIConstraint = null;
        public event ProcessDummies OnProcessDummies = null;

        Dictionary<String, List<ObjectInformation>> objinfos = new Dictionary<string, List<ObjectInformation>>();

        public void LoadWorld(GraphicFactory factory, GraphicInfo ginfo, IWorld world, ModelLoaderData worldData)
        {
            objinfos.Clear();
            foreach (var item in worldData.ModelMeshesInfo)
            {
                if(objinfos.ContainsKey(item.modelName + item.meshIndex))
                {
                    objinfos[item.modelName + item.meshIndex].Add(item);
                }
                else
                {
                    objinfos[item.modelName + item.meshIndex] = new List<ObjectInformation>();
                    objinfos[item.modelName + item.meshIndex].Add(item);
                }
            }

            foreach (var item in objinfos.Keys)
            {
                if (OnCreateIObject != null)
                {
                    IObject[] obj = OnCreateIObject(world, factory, ginfo, objinfos[item].ToArray());
                    if (obj != null)
                    {
                        foreach (var ob in obj)
	                    {
		                    world.AddObject(ob);
	                    }                        
                    }
                }
                else
                {
                    foreach (var obj in WorldLoader.CreateOBJ(world, factory, ginfo, objinfos[item].ToArray()))
                    {
                        world.AddObject(obj);    
                    }
                    
                }
            }

            foreach (var item in worldData.ConstraintInfo)
            {

                List<IObject> obb = world.Objects.ToList();                

                IObject o1 = obb.Find(delegate(IObject o) { return o.Name == item.bodyA; });
                IObject o2 = obb.Find(delegate(IObject o) { return o.Name == item.bodyB; });

                if (o1.PhysicObject.PhysicObjectTypes != PhysicObjectTypes.TRIANGLEMESHOBJECT && o2.PhysicObject.PhysicObjectTypes != PhysicObjectTypes.TRIANGLEMESHOBJECT)
                {
                    if (OnCreateIConstraint != null)
                    {
                        IPhysicConstraint constr = OnCreateIConstraint(world, factory, ginfo, item, o1, o2);
                        if (constr != null)
                            world.PhysicWorld.AddConstraint(constr);

                    }
                    else
                    {
                        IPhysicConstraint constr = WorldLoader.CreateConstraint(world, factory, ginfo, item, o1, o2);
                        world.PhysicWorld.AddConstraint(constr);
                    }
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
#endif