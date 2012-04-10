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
using PloobsEngine.Utils;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Paralax Effect and Normal Mapping Screen
    /// </summary>
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class ShaderIDScreen : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.97f, true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;            
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

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
        }
        DeferredEnvironmentCustomShader DeferredEnvironmentCustomShader;
        
        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            #region Models

            {
                ///Need to load the height, the normal texture and the difuse texture
                SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\block", "..\\Content\\Textures\\color_map");
                sm.SetTexture("Textures\\normal_map", TextureType.BUMP);                
                sm.SetCubeTexture(factory.GetTextureCube("Textures//cubemap"),TextureType.ENVIRONMENT);

                BoxObject pi = new BoxObject(new Vector3(200, 110, 0), 1, 1, 1, 5, new Vector3(100, 100, 100), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                DeferredEnvironmentCustomShader = new DeferredEnvironmentCustomShader(false, true, false);
                DeferredEnvironmentCustomShader.SpecularIntensity = 0.2f;
                DeferredEnvironmentCustomShader.SpecularPower = 30;
                DeferredEnvironmentCustomShader.ReflectionIndex = 0.5f;

                ///Setting Shader iD
                ///This make the object be not affect by the light
                DeferredEnvironmentCustomShader.ShaderId = ShaderUtils.CreateSpecificBitField(true);
                IMaterial mat = new DeferredMaterial(DeferredEnvironmentCustomShader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);

                {
                    SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Space,
                        (a) =>
                        {
                            if (DeferredEnvironmentCustomShader.ShaderId == ShaderUtils.CreateSpecificBitField(true))
                            {
                                DeferredEnvironmentCustomShader.ShaderId = ShaderUtils.CreateSpecificBitField(false);
                            }
                            else
                            {
                                DeferredEnvironmentCustomShader.ShaderId = ShaderUtils.CreateSpecificBitField(true);
                            }                            
                        }
                        );
                    this.BindInput(ik);
                }

            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                DeferredCustomShader shader = new DeferredCustomShader(false,false,false,false);
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            #endregion            

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

            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(10), MathHelper.ToRadians(-10), new Vector3(200, 150, 250), GraphicInfo);
            this.World.CameraManager.AddCamera(cam);

            LightThrowBepu = new LightThrowBepu(this.World, GraphicFactory);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }

        LightThrowBepu LightThrowBepu;

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent(SkyBox.MyName);
            LightThrowBepu.CleanUp();
            base.CleanUp(engine);
        }


        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            ///must be called before
            base.Draw(gameTime, render);

            ///Draw some text to the screen
            render.RenderTextComplete("Demo Shader ID. The Big Box Object is not affected by the Light", new Vector2(20, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("This can be used to create lots of cool effects, like custom lightining", new Vector2(20, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Press Space to toggle, left mouse buttom to throw a ball with a light", new Vector2(20, 55), Color.White, Matrix.Identity);
            render.RenderTextComplete("Box is Affected By Light: " + (DeferredEnvironmentCustomShader.ShaderId != ShaderUtils.CreateSpecificBitField(true)), new Vector2(20, 75), Color.White, Matrix.Identity);
             
        }

    }
}

