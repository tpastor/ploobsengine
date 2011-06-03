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
    public delegate IObject CreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation mi);
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

        public static IObject CreateOBJ(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation mi)
        {
            BatchInformation[] bi = { mi.batchInformation};
            IModelo model = new CustomModel(factory, mi.modelName, bi , mi.difuse, mi.bump, mi.specular, mi.glow);
            //IModelo model = new SimpleModel(factory, mi.modelName, mi.difuse.Name, mi.bump.Name, mi.specular.Name, mi.glow.Name,true);
            

            MaterialDescription material = new MaterialDescription(mi.staticfriction, mi.dinamicfriction, mi.ellasticity);

            IPhysicObject po;


            bool flag = false;
            if (mi.mass == 0)
            {
                flag = true;
                mi.mass = 0.5f;
            }


            Vector3 translation, scale;
            Quaternion rotation;
            BatchInformation binf = model.GetBatchInformation(0)[0];

            binf.ModelLocalTransformation.Decompose(out scale, out rotation, out translation);

            BoundingBox bb;

            switch (mi.collisionType)
            {


                case "Cylinder":

                    binf.ModelLocalTransformation = Matrix.Identity;
                    bb = ModelBuilderHelper.CreateBoundingBoxFromModel(binf, model);
                    Vector3 len = bb.Max - bb.Min;

                    po = new CylinderObject(translation, len.Y, len.X / 2, mi.mass,Matrix.CreateFromQuaternion(rotation), material);
                    
                    break;


                case "Sphere":
                    binf.ModelLocalTransformation = Matrix.Identity;
                    po = new SphereObject(translation, model.GetModelRadius(), mi.mass, 1 /*scale.X*/, material);
                    po.Rotation = Matrix.CreateFromQuaternion(rotation);



                    break;


                case "Box":

                    bb = ModelBuilderHelper.CreateBoundingBoxFromModel(binf, model);

                    len = bb.Max - bb.Min;

                    po = new BoxObject(translation, len.X, len.Y, len.Z, mi.mass, scale, Matrix.CreateFromQuaternion(rotation), material);

                    break;
                default:
                    po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, new Vector3(1), MaterialDescription.DefaultBepuMaterial());
                    break;
            }

            po.isMotionLess = flag;

            IShader shader = new DeferredCustomShader(mi.HasTexture(TextureType.GLOW), mi.HasTexture(TextureType.BUMP), mi.HasTexture(TextureType.SPECULAR), mi.HasTexture(TextureType.PARALAX));
            DeferredMaterial dm = new DeferredMaterial(shader);
            IObject ob = new IObject(dm, model, po);

            ob.Name = mi.modelName;

            return ob;
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
                    IObject ob1 = WorldLoader.CreateOBJ(world, factory, ginfo, item);

                    world.AddObject(ob1);



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