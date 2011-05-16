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

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Animated Bilboard Sample
    /// </summary>
    public class AnimatedBilboardScreen : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();        
            desc.UseFloatingBufferForLightMap = false;            
            renderTech = new DeferredRenderTechnic(desc);
        }
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);
         
            ///Classic island
            SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            DeferredNormalShader shader = new DeferredNormalShader();
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            this.World.AddObject(obj);


            ///The Animated Bilboard
            {
                ///A lits with all the bilboards positions
                List<Vector3> poss = new List<Vector3>();
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        float x, y;
                        x = i * 100;
                        y = j * 100;
                        poss.Add(new Vector3(x, 5, y));
                    }
                }

                ///Create the Bilboard Model
                ///the Textures\\coin Texture is a Horizontal animated texture
                StaticBilboardModel bm = new StaticBilboardModel(factory, "Bilbs", "Textures\\coin", poss);
                ///The shader that will render it, pass the number of frames, the time to wait between frames and the type of the bilboard
                ///Spherical ALWAYS face the camera
                ///Cilindric has a axis that the bilboard can rotate around, to face the camera
                DeferredAnimatedTextureShader cb = new DeferredAnimatedTextureShader(6, 100, BilboardType.Cilindric);
                ///We can animated the two up vertices of the bilboard quad (useful in grass for example)
                cb.Amplitude = 0f;
                ///Quad Scale 
                cb.Scale = new Vector2(50, 50);
                ///Color atenuation, this will multiply the Texture colors
                cb.Atenuation = new Vector4(0.4f, 0.4f, 0.4f, 1);
                DeferredMaterial matfor = new DeferredMaterial(cb);
                ///no physical representation (REMEMBER; all bilboards are the SAME OBJECT)
                GhostObject go = new GhostObject(new Vector3(70,0,0),Matrix.Identity,Vector3.One);                
                IObject obj2 = new IObject(matfor, bm, go);
                this.World.AddObject(obj2);
            }           


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


            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(-60), MathHelper.ToRadians(-10), new Vector3(-200, 150, 250), GraphicInfo.Viewport);
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grassCube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

        }


        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo 10-22:Animated Bilboard Sample, putting animated textures on Quads", new Vector2(10, 15), Color.White, Matrix.Identity);
        }

    }
}
