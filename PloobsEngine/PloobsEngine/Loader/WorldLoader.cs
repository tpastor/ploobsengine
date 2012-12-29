#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
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
using PloobsEngine.Particles;


namespace PloobsEngine.Loader
{
    public delegate IObject[] CreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation[] mi);
    public delegate ILight CreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li);
    public delegate ICamera CreateICamera(IWorld world, GraphicFactory factory, GraphicInfo ginfo, CameraInfo cinfo);
    public delegate IParticleSystem CreateIParticle(IWorld world, ParticleInfo pinfo);
    
    public delegate IPhysicConstraint CreateIConstraint(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ConstraintInfo cinfo, IObject o1, IObject o2);
    public delegate void ProcessDummies(IWorld world, DummyInfo dinfo);

    /// <summary>
    /// Used to build the world from a loaded source (like our 3ds exporter script )
    /// </summary>
    public class WorldLoader
    {
        /// <summary>
        /// Used to retrieve objects
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

                case "Ghost":

                   
                    po = new GhostObject(mi[0].position,Matrix.CreateFromQuaternion(mi[0].rotation), mi[0].scale);

                    break;
                case "Cylinder":

                    binf.ModelLocalTransformation = Matrix.Identity;
                    bb = ModelBuilderHelper.CreateBoundingBoxFromModel(binf);
                    Vector3 len = bb.Max - bb.Min;

                    po = new CylinderObject(mi[0].position, len.Y, len.X / 2,Vector3.Up ,mi[0].mass, Matrix.CreateFromQuaternion(mi[0].rotation), material);
                    
                    break;


                case "Sphere":
                    binf.ModelLocalTransformation = Matrix.Identity;
                    po = new SphereObject(mi[0].position, model.GetModelRadius(), mi[0].mass, mi[0].scale.X, material);
                    po.Rotation = Matrix.CreateFromQuaternion(mi[0].rotation);

                    break;


                case "Box":

                    bb = ModelBuilderHelper.CreateBoundingBoxFromModel(binf);

                    len = bb.Max - bb.Min;

                    po = new BoxObject(mi[0].position, len.X, len.Y, len.Z, mi[0].mass, mi[0].scale, Matrix.CreateFromQuaternion(mi[0].rotation), material);

                    break;

                case "Water":
                    po = new GhostObject(mi[0].position, Matrix.CreateFromQuaternion(mi[0].rotation), mi[0].scale);
                    break;
                case "TriangleMesh":
                default:
                    po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, new Vector3(1), material);
                    break;
            }

            po.isMotionLess = massflag;

            IShader shader = null;
#if !REACH && !WINDOWS_PHONE
            
            if (mi[0].HasTexture(TextureType.ENVIRONMENT))
            {
                shader = new DeferredEMReflectiveShader();
                (shader as DeferredEMReflectiveShader).TextureCube = mi[0].textureInformation.getCubeTexture(TextureType.ENVIRONMENT);
                
            }
            else if (mi[0].collisionType != null && mi[0].collisionType.Contains("Water"))
            {
                Vector3 position = (Vector3)(mi[0].extra["position"]);
                var width = (mi[0].extra["width"]);
                var height = (mi[0].extra["length"]);
                shader = new DeferredWaterCompleteShader((int)width,(int)height, new Plane(position.X, position.Y, position.Z, 1),10.0f);
            }
            
            else
            {
                shader = new DeferredCustomShader(mi[0].HasTexture(TextureType.GLOW), mi[0].HasTexture(TextureType.BUMP), mi[0].HasTexture(TextureType.SPECULAR), mi[0].HasTexture(TextureType.PARALAX)); 
            }
          
            DeferredMaterial dm = new DeferredMaterial(shader);
#else
            shader = new ForwardXNABasicShader();
            ForwardMaterial dm = new ForwardMaterial(shader);

#endif
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
        public event CreateIParticle OnCreateIParticle = null;
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

                IObject o1 = obb.First(delegate(IObject o) { return o.Name == item.bodyA; });
                IObject o2 = obb.First(delegate(IObject o) { return o.Name == item.bodyB; });

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

            foreach (var item in worldData.ParticleInfo)
            {
                if (OnCreateIParticle != null)
                {
                    IParticleSystem part = OnCreateIParticle(world, item);
                    if (part != null)
                    {
                        world.ParticleManager.AddAndInitializeParticleSystem(part);
                        (part as DPFSParticleSystem).IDPSFParticleSystem.Emitter.PositionData.Position = item.Position;
                        (part as DPFSParticleSystem).IDPSFParticleSystem.Emitter.OrientationData.Orientation = item.Orientation;

                    }
                }
                
            }
        }
    }

}
