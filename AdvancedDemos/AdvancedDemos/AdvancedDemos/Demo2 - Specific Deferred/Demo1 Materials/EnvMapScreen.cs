using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Entity;
using PloobsEngine.Input;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.SceneControl;
using PloobsEngine.Trigger;
using PloobsEngine.MessageSystem;
using PloobsEngine.Utils;
using PloobsEngine.Engine;
using PloobsEngine.Features;
using PloobsEngine.Commands;

namespace AdvancedDemo4._0
{   

    /// <summary>
    /// Env Map Screen    
    /// </summary>
    public class EnvMapScreen : IScene
    {
        LightThrowBepu lt;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.97f,true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
        }

        /// <summary>
        /// Inits the screen.
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="engine">The engine.</param>
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox sb = new SkyBox();
            engine.AddComponent(sb);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            lt.CleanUp();
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        private IObject tea;

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            #region Models
            {
                SimpleModel simpleModel = new SimpleModel(factory, "..\\Content\\Model\\teapot");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1,1, Color.Red), TextureType.DIFFUSE);
                //TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());

                GhostObject tmesh = new GhostObject(Vector3.Zero, Matrix.Identity, Vector3.One);
                ///Environment Map Shader, there are 2 options, the first is a fully reflective surface (dont use the object texture) and the second
                ///is a mix of the object texture and the environment texture
                ///Used to fake ambient reflection, give metal appearence to an object ....
                DeferredEMReflectiveShader shader = new DeferredEMReflectiveShader("Textures\\grassCUBE", 0.9f);
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                tea = new IObject(fmaterial, simpleModel, tmesh);

                tea.OnUpdate += new OnUpdate(tea_OnUpdate);
                this.World.AddObject(tea);                
                
            }

            //{
            //    SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
            //    simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
            //    BoxObject tmesh = new BoxObject(new Vector3(0,-20,0), 1, 1, 1, 10, new Vector3(200, 1, 200), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
            //    tmesh.isMotionLess = true;
            //    DeferredNormalShader shader = new DeferredNormalShader();
            //    DeferredMaterial fmaterial = new DeferredMaterial(shader);
            //    IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            //    this.World.AddObject(obj);
            //}


            #endregion

            lt = new LightThrowBepu(this.World, factory);

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

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCUBE");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(30), MathHelper.ToRadians(-30), new Vector3(30, 30, 50), GraphicInfo.Viewport);
            cam.FarPlane = 500;
            cam.NearPlane = 1;
            this.World.CameraManager.AddCamera(cam);

            AntiAliasingPostEffectTabula aa = new AntiAliasingPostEffectTabula();
            aa.Weights = 2;
            this.RenderTechnic.AddPostEffect(aa);
        }

        void tea_OnUpdate(IObject obj, GameTime gt, ICamera cam)
        {
            tea.PhysicObject.Rotation *= Matrix.CreateRotationY(0.02f);
            tea.PhysicObject.Rotation *= Matrix.CreateRotationZ(0.02f);

        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            ///must be called before
            base.Draw(gameTime, render);

            ///Draw some text to the screen
            render.RenderTextComplete("Demo 3-22: Environment Mapping", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Used to fake ambient reflection, give metal appearence to an object ....", new Vector2(20, 35), Color.White, Matrix.Identity);            
        }


    }

    
}

