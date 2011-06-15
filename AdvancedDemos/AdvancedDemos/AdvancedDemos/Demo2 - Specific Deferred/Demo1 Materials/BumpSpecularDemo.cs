using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.SceneControl;
using PloobsEngine.Loader;
using PloobsEngine.Engine;
using System.Collections.Generic;
using PloobsEngine.Input;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Input;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// BumpSpecular Deferred Scene Demo
    /// </summary>
    public class BumpSpecularDemo : IScene
    {

        List<IObject> withBump = new List<IObject>();
        List<IObject> withSpecular = new List<IObject>();
        bool bump = true;
        bool specular = true;
        LightThrowBepu lt;

        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the world using bepu as physic api and a simple culler implementation
            ///IT DOES NOT USE PARTICLE SYSTEMS (see the complete constructor, see the ParticleDemo to know how to add particle support)
            world = new IWorld(new BepuPhysicWorld(-0.097f,true), new SimpleCuller());

            ///Create the deferred description
            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            ///Some custom parameter, this one allow light saturation. (and also is a pre requisite to use hdr)
            desc.UseFloatingBufferForLightMap = true;
            ///set background color, default is black
            desc.BackGroundColor = Color.CornflowerBlue;
            ///create the deferred technich
            renderTech = new DeferredRenderTechnic(desc);
        }

        /// <summary>
        /// Cleans up resources that arent exclusive of the screen
        /// </summary>
        /// <param name="engine"></param>
        protected override void CleanUp(EngineStuff engine)
        {
            lt.CleanUp();        
            base.CleanUp(engine);
        }

        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            ///must be called before all
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///Create the xml file model extractor
            ///Loads a XML file that was export by our 3DS MAX plugin
            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//", "Textures//");
            ///Extract all the XML info (Model,Cameras, ...)
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "leonScene");
            ///Create the WOrld Loader
            ///Convert the ModelLoaderData in World Entities
            WorldLoader wl = new WorldLoader();
            ///Register some Custom Handler
            ///The Default arent good all the time
            ///Called when an object is created, the default creates a triangle mesh entity and a deferred Material (whith Custom Shader) and add it to the world
            wl.OnCreateIObject += new CreateIObject(wl_OnCreateIObject);
            ///When a ligh is created, the default just add the light
            wl.OnCreateILight += new CreateILight(wl_OnCreateILight);
            ///when a camera is created, the default just add the camera
            wl.OnCreateICamera += new CreateICamera(wl_OnCreateICamera);
            wl.LoadWorld(factory, GraphicInfo, World, data);

            ///Add some directional lights
            #region Lights
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.3f;
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

            ///Add a post effect
            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());

            ///add a camera
            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(30), MathHelper.ToRadians(-30), new Vector3(50,50,75), GraphicInfo.Viewport);
            cam.FarPlane = 500;
            cam.NearPlane = 1;

            this.World.CameraManager.AddCamera(cam);

            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.B, bumpChange);
                this.BindInput(ik);
            }
            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.V, specularChange);
                this.BindInput(ik);
            }

            lt = new LightThrowBepu(this.World, factory);

        }

        public void bumpChange(InputPlayableKeyBoard ipk)
        {
            foreach (var item in withBump)
            {
                DeferredCustomShader shader = item.Material.Shadder as DeferredCustomShader;
                System.Diagnostics.Debug.Assert(shader != null);
                shader.UseBump = !shader.UseBump;
                bump = shader.UseBump;
            }
        }

        public void specularChange(InputPlayableKeyBoard ipk)
        {
            foreach (var item in withSpecular)
            {
                DeferredCustomShader shader = item.Material.Shadder as DeferredCustomShader;
                System.Diagnostics.Debug.Assert(shader != null);
                shader.UseSpecular = !shader.UseSpecular;
                specular = shader.UseSpecular;
            }
        }
        

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            ///must be called before
            base.Draw(gameTime, render);

            ///Draw some text to the screen
            render.RenderTextComplete("Demo 2-22: Bump And Specular", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Use V and B to Enable/Disable Bump and Specular", new Vector2(20, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Bump Enabled: " + bump, new Vector2(20, 55), Color.White, Matrix.Identity);
            render.RenderTextComplete("Specular Enabled: " + specular, new Vector2(20, 75), Color.White, Matrix.Identity);
            render.RenderTextComplete("Use Left mouse buttom to throw a light ", new Vector2(20, 95), Color.White, Matrix.Identity);
            render.RenderTextComplete("Use Right mouse buttom to put a light in the camera actual position", new Vector2(20, 115), Color.White, Matrix.Identity);            
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
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="mi">The mi.</param>
        /// <returns></returns>
        IObject[] wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ObjectInformation[] mi)
        {
            IObject[] objs = new IObject[mi.Length];
            for (int i = 0; i < mi.Length; i++)
            {
                IModelo model = new CustomModel(factory, mi[i].modelName, mi[i].batchInformation, mi[i].textureInformation);
                IPhysicObject po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                DeferredCustomShader shader = new DeferredCustomShader(mi[i].HasTexture(TextureType.GLOW), mi[i].HasTexture(TextureType.BUMP), mi[i].HasTexture(TextureType.SPECULAR), mi[i].HasTexture(TextureType.PARALAX));
                DeferredMaterial dm = new DeferredMaterial(shader);
                IObject obj = new IObject(dm, model, po);

                if (mi[i].HasTexture(TextureType.BUMP))
                {
                    withBump.Add(obj);
                }

                if (mi[i].HasTexture(TextureType.SPECULAR))
                {
                    shader.SpecularPowerMapScale = 2f;
                    shader.SpecularIntensityMapScale = 0.1f;
                    withSpecular.Add(obj);
                }
                objs[i] = obj;
            }

            return objs;

        }

    }
}

