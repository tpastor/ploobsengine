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
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

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
        /// Init Screen
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="engine"></param>
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            InputAdvanced ia = new InputAdvanced();
            engine.AddComponent(ia);

        }

        /// <summary>
        /// Cleans up resources that arent exclusive of the screen
        /// </summary>
        /// <param name="engine"></param>
        protected override void CleanUp(EngineStuff engine)
        {
            lt.CleanUp();
            engine.RemoveComponent("InputAdvanced");
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
            float li = 0.1f;
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
            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffectStalker());

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo.Viewport));

            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.B, bumpChange, EntityType.TOOLS);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.V, specularChange, EntityType.TOOLS);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
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
            render.RenderTextComplete("Demo: Bump And Specular", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Use V and B to Enable/Disable Bump and Specular", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Bump Enabled: " + bump, new Vector2(GraphicInfo.Viewport.Width - 315, 55), Color.White, Matrix.Identity);
            render.RenderTextComplete("Specular Enabled: " + specular, new Vector2(GraphicInfo.Viewport.Width - 315, 75), Color.White, Matrix.Identity);
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
        IObject wl_OnCreateIObject(IWorld world, GraphicFactory factory, GraphicInfo ginfo, ModelInformation mi)
        {            
            IModelo model = new CustomModel(factory, mi.modelName, new BatchInformation[] { mi.batchInformation }, mi.difuse, mi.bump, mi.specular, mi.glow);
            IPhysicObject po = new TriangleMeshObject(model, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            DeferredCustomShader shader = new DeferredCustomShader(mi.HasTexture(TextureType.GLOW), mi.HasTexture(TextureType.BUMP), mi.HasTexture(TextureType.SPECULAR), mi.HasTexture(TextureType.PARALAX));
            DeferredMaterial dm = new DeferredMaterial(shader);
            IObject obj = new  IObject(dm, model, po);
            if (mi.HasTexture(TextureType.BUMP))
            {
                withBump.Add(obj);
            }
            if (mi.HasTexture(TextureType.SPECULAR))
            {
                shader.SpecularPowerMapScale = 2f;
                shader.SpecularIntensityMapScale = 0.2f;            
                withSpecular.Add(obj);
            }

            return obj;

        }

    }
}

