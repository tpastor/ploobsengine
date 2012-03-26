using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.Physics;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;
using System.Collections.Generic;
using PloobsEngine.Utils;

namespace ProjectTemplate
{
    /// <summary>
    /// Basic Deferred Scene
    /// </summary>
    public class MixReflection : IScene
    {
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
                IModelo sm = new SimpleModel(factory, "Model\\block");
                Matrix trans = Matrix.CreateTranslation(new Vector3(100, 50, 0));
                Plane plano = Plane.Transform(new Plane(0, 1, 0, 0), trans);
                IPhysicObject pi = new BoxObject(Vector3.Zero, 1, 1, 1, 5, new Vector3(1300, 0.1f, 1300), trans, MaterialDescription.DefaultBepuMaterial());
                pi.isMotionLess = true;
                ///Water shader, will refract and reflect according to the plano passed in the parameter
                ///Using default Parameters, there are lots of things that can be changed. See WaterCompleteShader 
                DeferredWaterCompleteShader shader = new DeferredWaterCompleteShader(800, 600, plano, 0);
                shader.SpecularIntensity = 0.01f;
                shader.SpecularPower = 50;
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj4 = new IObject(mat, sm, pi);
                this.World.AddObject(obj4);
            }


            {
                ///Need to load the height, the normal texture and the difuse texture
                SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\block", "..\\Content\\Textures\\color_map");
                sm.SetTexture("Textures\\normal_map", TextureType.BUMP);
                sm.SetCubeTexture(factory.GetTextureCube("Textures//cubemap"), TextureType.ENVIRONMENT);

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
                List<Vector3> poss = new List<Vector3>();
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        float x, y;
                        x = i * 100;
                        y = j * 100;
                        poss.Add(new Vector3(x, 100, y));
                    }
                }
                StaticBilboardModel bm = new StaticBilboardModel(factory, "Bilbs", "..\\Content\\Textures\\tree", poss);
                DeferredCilindricGPUBilboardShader cb = new DeferredCilindricGPUBilboardShader();
                DeferredMaterial matfor = new DeferredMaterial(cb);
                GhostObject go = new GhostObject();
                IObject obj2 = new IObject(matfor, bm, go);
                this.World.AddObject(obj2);
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
            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
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
            render.RenderTextComplete("Demo: Basic Screen Deferred", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
        }
    }
}

