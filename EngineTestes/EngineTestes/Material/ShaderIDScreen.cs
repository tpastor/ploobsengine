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

namespace EngineTestes
{
    /// <summary>
    /// Paralax Effect and Normal Mapping Screen
    /// </summary>
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
                DeferredEnvironmentCustomShader DeferredEnvironmentCustomShader = new DeferredEnvironmentCustomShader(false, true, false);
                DeferredEnvironmentCustomShader.SpecularIntensity = 0.2f;
                DeferredEnvironmentCustomShader.SpecularPower = 30;
                DeferredEnvironmentCustomShader.ShaderId = ShaderUtils.CreateSpecificBitField(true);
                IMaterial mat = new DeferredMaterial(DeferredEnvironmentCustomShader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
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

            new LightThrowBepu(this.World, GraphicFactory);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }
    

    }
}

