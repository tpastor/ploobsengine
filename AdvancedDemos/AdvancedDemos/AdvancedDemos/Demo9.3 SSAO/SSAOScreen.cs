using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.Physics;
using PloobsEngine.SceneControl;
using PloobsEngine.Loader;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Input;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Input;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Basic Deferred Scene
    /// </summary>
    public class SSAOScreen : IScene
    {                 
        SSAOPostEffect ssao;
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
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///Create the xml file model extractor
            ///Loads a XML file that was export by our 3DS MAX plugin
            ExtractXmlModelLoader ext = new ExtractXmlModelLoader("Content//ModelInfos//", "Model//","Textures//");
            ///Extract all the XML info (Model,Cameras, ...)
            ModelLoaderData data = ext.Load(factory, GraphicInfo, "SSAO");
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
            ///Add some directional lights to completely iluminate the world
            #region Lights
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.4f;
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
                        
            {
                ///har way
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Enter,Active);
                this.BindInput(ik);
            }

            {
                ///easy way
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Space);
                ik.KeyStateChange += new KeyStateChange(ik_KeyStateChange);
                this.BindInput(ik);
            }

            ssao = new SSAOPostEffect();
            //ssao.OutputONLYSSAOMAP = true;
            ssao.WhiteCorrection = 0.7f;
            ssao.Intensity = 5;
            ssao.Diffscale = 0.5f;            

            ///Add a AA post effect
            this.RenderTechnic.AddPostEffect(ssao);


            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(0), MathHelper.ToRadians(-10), new Vector3(0,100, 150), GraphicInfo.Viewport);
            ///add a camera
            this.World.CameraManager.AddCamera(cam);
        }

        void ik_KeyStateChange(InputPlayableKeyBoard ipk)
        {
            if(ssao.Enabled)
                ssao.OutputONLYSSAOMAP = !ssao.OutputONLYSSAOMAP;
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
            IObject obj = WorldLoader.CreateOBJ(world, factory, ginfo, mi);
            ///Change object property here !!!
            DeferredCustomShader cd = (obj.Material.Shadder as DeferredCustomShader); ///the world loader uses deferredCustomShader for all objects
            System.Diagnostics.Debug.Assert(cd != null);
            ///if the obj does not use specular map
            if (!cd.UseSpecular)
            {
                ///No Specular this time
                cd.SpecularIntensity = 0f;                
            }          

            return obj;
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
            render.RenderTextComplete("Demo 21-22: SSAO", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Hit Enter to enable/disable SSAO: " + ssao.Enabled, new Vector2(20, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Hit Space to see the SSAO Map: " + ssao.OutputONLYSSAOMAP, new Vector2(20, 55), Color.White, Matrix.Identity);
        }

        public void Active(InputPlayableKeyBoard ipk)
        {
            ssao.Enabled = !ssao.Enabled;
        }
    }
}

