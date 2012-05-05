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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using EngineTestes;

namespace ProjectTemplate
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class PostEffectScreen : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the IWorld
            world = new IWorld(new BepuPhysicWorld(-9.8f), new SimpleCuller());

            ///Create the deferred technich
            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription();
            desc.BackGroundColor = Color.AliceBlue;
            desc.UsePostEffect = true;
            renderTech = new ForwardRenderTecnich(desc);
            
        }
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            
            base.InitScreen(GraphicInfo, engine);
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
            
            ///Uncoment to add your model
            SimpleModel simpleModel = new SimpleModel(factory, "Model/cenario");
            ///Physic info (position, rotation and scale are set here)
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            ///Forward Shader (look at this shader construction for more info)
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            ///Forward material
            ForwardMaterial fmaterial = new ForwardMaterial(shader);
            ///The object itself
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            ///Add to the world
            this.World.AddObject(obj);

            this.RenderTechnic.AddPostEffect(MotionBlurPostEffect);
            this.RenderTechnic.AddPostEffect(BloomPostEffect);

            BloomPostEffect.Enabled = false;
            MotionBlurPostEffect.Enabled = false;

            blurdefault = MotionBlurPostEffect.Amount;

            RotatingCamera cam = new RotatingCamera(this, new Vector3(0,-100,-400));
            
            this.World.CameraManager.AddCamera(cam);

            RasterizerState = new RasterizerState();
            RasterizerState.CullMode = CullMode.None;
        }

        RasterizerState RasterizerState;
        SimpleMotionBlurPostEffect MotionBlurPostEffect = new SimpleMotionBlurPostEffect();
        BloomPostEffect BloomPostEffect = new BloomPostEffect();
        private int blurdefault;


        public void ToggleBloom()
        {
            
            BloomPostEffect.Enabled = !BloomPostEffect.Enabled;

            if (BloomPostEffect.Enabled)
            {
                MotionBlurPostEffect.Enabled = false;
            }
            
        }
        public void ToggleBlur()
        {
            
            MotionBlurPostEffect.Enabled = !MotionBlurPostEffect.Enabled;

            if (MotionBlurPostEffect.Enabled)
            {
                BloomPostEffect.Enabled = false;
            }
        
        }

        public bool EnableBloom
        {
            get
            {
                return BloomPostEffect.Enabled;
            }
            set
            {
                this.BloomPostEffect.Enabled = value;
            }
        }

        public bool EnableMotionBlur
        {
            get
            {
                return MotionBlurPostEffect.Enabled;
            }
            set
            {
                this.MotionBlurPostEffect.Enabled = value;
            }
        }

        public void AmmountMotionBlurDelta(int delta)
        {
            MotionBlurPostEffect.Amount =blurdefault + delta;
        }

        public void AddAmountMotionBlur()
        {
            MotionBlurPostEffect.Amount = MotionBlurPostEffect.Amount + 1;
        }

        public void RemoveAmmountMotionBlur()
        {
            MotionBlurPostEffect.Amount = MotionBlurPostEffect.Amount - 1;
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {            

            render.PushRasterizerState(RasterizerState);
            base.Draw(gameTime, render);
            render.PopRasterizerState();

            ///Draw some text on the screen
            render.RenderTextComplete("Demo: PostProcess Demo", new Vector2(10, 15), Color.Red, Matrix.Identity);
            render.RenderTextComplete("MotionBlur Enabled : " + MotionBlurPostEffect.Enabled, new Vector2(10, 35), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Bloom Enabled : " + BloomPostEffect.Enabled, new Vector2(10, 55), Color.Red, Matrix.Identity);
            
        }

    }
}
