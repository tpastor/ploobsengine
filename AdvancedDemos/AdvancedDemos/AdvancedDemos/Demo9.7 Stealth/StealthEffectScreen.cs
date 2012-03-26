using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.Physics;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using EngineTestes.TransparencyOnBorders;
using PloobsEngine.Commands;
using PloobsEngine.Features;
using PloobsEngine.Engine;
using PloobsEngine.Utils;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Basic Deferred Scene
    /// </summary>
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class StealthEffectScreen : IScene
    {
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
        }

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
            ///must be called before all
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                ///Create a simple object
                ///Geomtric Info and textures (this model automaticaly loads the texture)
                SimpleModel simpleModel = new SimpleModel(factory, "model/dude");
                //simpleModel.SetTexture(factory.CreateTexture2DColor(1,1,Color.Red), TextureType.DIFFUSE);
                ///Physic info (position, rotation and scale are set here)
                GhostObject pi = new GhostObject(new Vector3(200, 10, 0), Matrix.Identity, new Vector3(4));
                ///Shader info (must be a deferred type)
                DeferredNormalShader shader = new DeferredNormalShader();
                shader.ShaderId = ShaderUtils.CreateBitField(false,false,false,false,true);
                ///Material info (must be a deferred type also)
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                fmaterial.IsVisible = false;
                ///The object itself
                IObject obj = new IObject(fmaterial, simpleModel, pi);
                ///Add to the world
                this.World.AddObject(obj);
                StealthPostEffect StealthPostEffect = new StealthPostEffect();
                StealthPostEffect.objs.Add(obj);
                this.RenderTechnic.AddPostEffect(StealthPostEffect);
            }

            

            {
                ///Create a simple object
                ///Geomtric Info and textures (this model automaticaly loads the texture)
                SimpleModel simpleModel = new SimpleModel(factory, "model/cenario");
                ///Physic info (position, rotation and scale are set here)
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ///Shader info (must be a deferred type)
                DeferredNormalShader shader = new DeferredNormalShader();
                ///Material info (must be a deferred type also)
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                ///The object itself
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                ///Add to the world
                this.World.AddObject(obj);
            }
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

            ///Add a AA post effect
            //this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
            
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent(SkyBox.MyName);
            base.CleanUp(engine);
        }             

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo : Stealth Character with Custom Shader and Post Effect", new Vector2(20, 15), Color.White, Matrix.Identity);            
        }
    }
}

