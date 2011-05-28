using PloobsEngine.Physics;
using PloobsEngine.SceneControl;
using PloobsEngine;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;
using PloobsEngine.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.Light;
using PloobsEngine.Input;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// A simple Screen
    /// Implements a SceneScreen, a special case of IScreen specific for building 3d worlds
    /// </summary>
    public class PointLightScreen : IScene
    {                                  
        LightThrowBepu lt;        

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());
            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc) ;   
        }


        protected override void  LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
 	        base.LoadContent(GraphicInfo, factory, contentManager);     

            ///Create a Simple Model
            IModelo sm = new SimpleModel(factory, "..\\Content\\Model\\cenario");            
            ///Create a Physic Object
            IPhysicObject pi = new TriangleMeshObject(sm, Vector3.Zero, Matrix.Identity, Vector3.One,MaterialDescription.DefaultBepuMaterial());
            pi.isMotionLess = true;
            ///Create a shader 
            IShader shader = new DeferredNormalShader();            
            ///Create a Material
            IMaterial mat = new DeferredMaterial(shader);
            ///Create a an Object that englobs everything and add it to the world
            IObject obj4 = new IObject(mat, sm,pi);
            this.World.AddObject(obj4);

            lt = new LightThrowBepu(this.World, factory);  

            ///Create a FirstPerson Camera
            ///This is a special camera, used in the development
            ///You can move around using wasd / qz / and the mouse
            ICamera cam = new CameraFirstPerson(GraphicInfo.Viewport);
            this.World.CameraManager.AddCamera(cam);

            PointLightCircularUpdater lcu = new PointLightCircularUpdater(this);

            ///Create 100 moving point lights and add to the world
            ///IF the radius of the point light is TOO big like 250, it will make the game slower
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    PointLightPE pl0 = new PointLightPE(new Vector3(i * 5, 25, j * 5), StaticRandom.RandomColor(), 100, 2);
                    pl0.UsePointLightQuadraticAttenuation = true;
                    lcu.AddLight(pl0, 0.01f, 50 * i, j / StaticRandom.Random());                    
                    this.World.AddLight(pl0);
                }
            }

            lcu.Start();
            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo: Point Lights",new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White,Matrix.Identity);
        }
    }
}
