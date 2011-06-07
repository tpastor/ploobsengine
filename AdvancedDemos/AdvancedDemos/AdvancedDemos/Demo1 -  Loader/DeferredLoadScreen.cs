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
    /// <summary>
    /// Basic Load Screen
    /// </summary>
    public class DeferredLoadScreen : IScene
    {
        LightThrowBepu lt;
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the world
            world = new IWorld(new BepuPhysicWorld(-0.097f,true), new SimpleCuller());

            ///Create the render technich
            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
        }

        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///Create the xml file model extractor
            ///Loads a XML file that was export by our 3DS MAX plugin
            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ///Extract all the XML info (Model,Cameras, ...)
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "leonScene");
            ///Create the WOrld Loader
            ///Convert the ModelLoaderData in World Entities
            WorldLoader wl = new WorldLoader();
            ///Register some Custom HAnlder
            ///The Default arent good all the time
            ///Called when an object is created, the default creates a triangle mesh entity and a deferred Material (whith Custom Shader) and add it to the world
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);
            ///When a ligh is created, the default just add the light
            wl.OnCreateILight += new CreateILight(wl_OnCreateILight);
            ///when a camera is created, the default just add the camera
            wl.OnCreateICamera += new CreateICamera(wl_OnCreateICamera);
            wl.LoadWorld(factory, GraphicInfo, World, data);

            ///the classical light throw
            lt = new LightThrowBepu(this.World, factory);

            ///Classical lights
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

            ///Classic Camera =P
            
            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(30), MathHelper.ToRadians(-30), new Vector3(50,50,75), GraphicInfo.Viewport);
            cam.FarPlane = 500;
            cam.NearPlane = 1;
            cam.MoveSpeed *= 2;
   
            
            this.World.CameraManager.AddCamera(cam);            

            ///Some basic AA
            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());

        }

        /// <summary>
        /// called when a camera is found
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="cinfo">The cinfo.</param>
        /// <returns></returns>
        ICamera wl_OnCreateICamera(IWorld world, GraphicFactory factory, GraphicInfo ginfo, CameraInfo cinfo)
        {
            ///when null is returned, nothing is created
            return null;
        }

        /// <summary>
        /// called when a light is found
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="li">The li.</param>
        /// <returns></returns>
        ILight wl_OnCreateILight(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ILight li)
        {
            ///when null is returned nothing is created
            return null;
        }

        /// <summary>
        /// Called when an object is found
        ///Return the object, return null to not add this object
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="mi">The mi.</param>
        /// <returns></returns>
        IObject wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation mi)
        {            

            ///Do what default would do.
            IObject obj =  WorldLoader.CreateOBJ(world, factory, ginfo, mi);
            ///Change object property here !!!
            DeferredCustomShader cd = (obj.Material.Shadder as DeferredCustomShader); ///the world loader uses deferredCustomShader for all objects
            System.Diagnostics.Debug.Assert(cd != null);
            ///if the obj does not use specular map
            if (!cd.UseSpecular)
            {
                ///set a constant specular for all the object
                cd.SpecularIntensity = 0.3f;
                cd.SpecularPower = 150;
            }
            else
            {
                
            }
            
            ///If you want, create the object on your own, without using the World Loader,                          
            ///THIS IS WHAT WorldLoader.CreateOBJ DOES
            //IModelo model = new CustomModel(factory, mi.modelName, new BatchInformation[] { mi.batchInformation}, mi.difuse, mi.bump, mi.specular, mi.glow);
            //IPhysicObject po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            //IShader shader = new DeferredCustomShader(mi.HasTexture(TextureType.GLOW), mi.HasTexture(TextureType.BUMP), mi.HasTexture(TextureType.SPECULAR), mi.HasTexture(TextureType.PARALAX));
            //DeferredMaterial dm = new DeferredMaterial(shader);
            //return new IObject(dm, model, po);

            return obj;
        }

        /// <summary>
        /// Cleans up resources that arent exclusive of the screen
        /// </summary>
        /// <param name="engine"></param>
        protected override void CleanUp(EngineStuff engine)
        {
            lt.CleanUp();
            ///MUST CALL THE BASE FUNCTION !!!!!!!!!!!!!!!!!!!
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            ///must be called before
            base.Draw(gameTime, render);

            ///Draw some text to the screen
            render.RenderTextComplete("Demo 1-22: Scene Loaded from our XML 3DS Max Exporter Plugin", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Use mouse and ASDW to move the camera", new Vector2(20, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Use Left mouse buttom to throw a light ", new Vector2(20, 55), Color.White, Matrix.Identity);            
            render.RenderTextComplete("Use Right mouse buttom to put a light in the camera actual position", new Vector2(20, 75), Color.White, Matrix.Identity);            
            
        }

    }
}
