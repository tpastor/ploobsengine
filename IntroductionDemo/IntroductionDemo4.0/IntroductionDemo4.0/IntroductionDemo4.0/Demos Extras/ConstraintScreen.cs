using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.Physics;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using PloobsEngine.Material;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Physic.Constraints.BepuConstraint;
using System.Collections.Generic;
using PloobsEngine.Input;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Basic Deferred Scene
    /// </summary>
    public class ConstraintScreen : IScene
    {                
        private PointPointConstraint constraint;
        LightThrowBepu lt;
        
        private bool constraintstate = true;
        
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the world using bepu as physic api and a simple culler implementation
            ///IT DOES NOT USE PARTICLE SYSTEMS (see the complete constructor, see the ParticleDemo to know how to add particle support)
            world = new IWorld(new BepuPhysicWorld(-0.098f), new SimpleCuller());

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
            
            List<IObject> balls = new List<IObject>();
            Vector3 pos1,pos2;
            pos1 = new Vector3(0, 20, 0);
            pos2 = new Vector3(0,-50,0);

            IObject ball1 = CreateSphere(pos1, Matrix.Identity, Color.Red);
            ball1.PhysicObject.isMotionLess = true; // Setting the Parent object as static

            World.AddObject(ball1);

            IObject ball2 = CreateSphere(pos2,Matrix.Identity,Color.White);
            World.AddObject(ball2);
            balls.Add(ball2);

            constraint = new PointPointConstraint((pos1 + pos2) / 2, ball1.PhysicObject, ball2.PhysicObject);


            World.PhysicWorld.AddConstraint(constraint);


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

            lt = new LightThrowBepu(this.World, factory);
            lt.force = 60;

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (constraintstate)
                {
                    World.PhysicWorld.RemoveConstraint(constraint);
                }
                else
                {
                    World.PhysicWorld.AddConstraint(constraint);
                }

                constraintstate = !constraintstate;
            }

            base.Update(gameTime);
        }


        private IObject CreateSphere(Vector3 pos, Matrix ori, Color Color)
        {
            ///Load a Model with a custom texture
            SimpleModel sm2 = new SimpleModel(GraphicFactory, "Model\\ball");
            sm2.SetTexture(GraphicFactory.CreateTexture2DColor(1, 1, Color, false), TextureType.DIFFUSE);
            DeferredNormalShader nd = new DeferredNormalShader();
            IMaterial m = new DeferredMaterial(nd);
            SphereObject pi2 = new SphereObject(pos, 1, 0.05f, 10, MaterialDescription.DefaultBepuMaterial());
            IObject o = new IObject(m, sm2, pi2);
            return o;
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

            Texture2D logo = GraphicFactory.GetTexture2D("Textures\\engine_logo");
            int wd = 64;
            int hg = 48;
            render.RenderTextureComplete(logo, new Rectangle(this.GraphicInfo.BackBufferWidth - wd, this.GraphicInfo.BackBufferHeight - hg, wd, hg));


            ///Draw some text to the screen
            render.RenderTextComplete("Demo: Constraint Screen", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Left click to throw a ball at the white ball =P", new Vector2(10, 35), Color.White, Matrix.Identity);
            
        }

        protected override void CleanUp(EngineStuff engine)
        {
            base.CleanUp(engine);
            lt.CleanUp();
        }
    }
}

