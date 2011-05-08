using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Features;
using PloobsEngine.Commands;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Engine;

namespace AdvancedDemo4._0
{    
    /// <summary>
    /// Paralax Effect and Normal Mapping Screen
    /// </summary>
    public class ParalaxScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = true;
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);

            InputAdvanced ia = new InputAdvanced();
            engine.AddComponent(ia);
        }

        DeferredCustomShader paralax;

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            #region Models

            ///Model with Paralax Shader enabled
            {
                ///Need to load the height, the normal texture and the difuse texture
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cubo", "..\\Content\\Textures\\color_map");
                sm.SetTexture("Textures\\normal_map", TextureType.BUMP);
                sm.SetTexture("Textures\\height_map", TextureType.PARALAX);                
                BoxObject pi = new BoxObject(new Vector3(200, 110, 0),1,1 ,1, 5, new Vector3(100, 100, 100),Matrix.Identity,MaterialDescription.DefaultBepuMaterial());
                ///Enable paralax and Normal;
                paralax = new DeferredCustomShader(false, true, false, true);
                paralax.SpecularIntensity = 0.2f;
                paralax.SpecularPower = 30;                
                IMaterial mat = new DeferredMaterial(paralax);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }


            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }


            #endregion

            LightThrowBepu lt = new LightThrowBepu(this.World, factory);

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

            this.World.CameraManager.AddCamera(new CameraFirstPerson(true, GraphicInfo.Viewport));


            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.DOWN, Keys.H, aumentaScale, EntityType.TOOLS);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.DOWN , Keys.G, diminuiScale, EntityType.TOOLS);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.DOWN, Keys.Y, aumentaBias, EntityType.TOOLS);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.DOWN, Keys.T, diminuiBias, EntityType.TOOLS);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }

            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.J, EnableDisableParalax, EntityType.TOOLS);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.K, EnableDisableBump, EntityType.TOOLS);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
        }

        public void aumentaScale(InputPlayableKeyBoard ipk)
        {
           paralax.ScaleBias = new Vector2(paralax.ScaleBias.X + 0.001f, paralax.ScaleBias.Y);         
        }
        public void diminuiScale(InputPlayableKeyBoard ipk)
        {
            paralax.ScaleBias = new Vector2(paralax.ScaleBias.X - 0.001f, paralax.ScaleBias.Y);                    
        }
        public void aumentaBias(InputPlayableKeyBoard ipk)
        {
            paralax.ScaleBias = new Vector2(paralax.ScaleBias.X, paralax.ScaleBias.Y + 0.001f);                    
        }
        public void diminuiBias(InputPlayableKeyBoard ipk)
        {
            paralax.ScaleBias = new Vector2(paralax.ScaleBias.X, paralax.ScaleBias.Y - 0.001f);                    
        }
        public void EnableDisableParalax(InputPlayableKeyBoard ipk)
        {
            paralax.UseParalax = !paralax.UseParalax;
        }
        public void EnableDisableBump(InputPlayableKeyBoard ipk)
        {
            paralax.UseBump = !paralax.UseBump;
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
            render.RenderTextComplete("Paralax " + paralax.UseParalax, new Vector2(20, 20), Color.White, Matrix.Identity);
            render.RenderTextComplete("Paralax ScaleBias " + paralax.ScaleBias, new Vector2(20, 40), Color.White, Matrix.Identity);
            render.RenderTextComplete("Bump " + paralax.UseBump, new Vector2(20, 60), Color.White, Matrix.Identity);            
        }

    }
}

