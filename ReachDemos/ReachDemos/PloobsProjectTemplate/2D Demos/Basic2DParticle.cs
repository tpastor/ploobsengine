using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl._2DScene;
using PloobsEngine.Physic2D.Farseer;
using Microsoft.Xna.Framework;
using PloobsEngine.Material2D;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using PloobsEngine.Modelo2D;
using PloobsEngine.SceneControl;
using PloobsEngine.Particles;
using PloobsEngine.Light2D;
using PloobsEngine.Engine;

namespace PloobsEnginePhone7Template
{
    public class Basic2DParticle : I2DScene
    {

        Texture2D tile;

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            engine.IsMouseVisible = true;
            base.InitScreen(GraphicInfo, engine);
        }

        Border border;
        protected override void SetWorldAndRenderTechnich(out RenderTechnich2D renderTech, out I2DWorld world)
        {
            Basic2DRenderTechnich rt = new Basic2DRenderTechnich();
            rt.RenderBackGround += new RenderBackGround(rt_RenderBackGround);
            renderTech = rt;

            world = new I2DWorld(new FarseerWorld(new Vector2(0, 9.8f)), new DPSFParticleManager());
        }

        void rt_RenderBackGround(GraphicInfo ginfo, RenderHelper render)
        {
            Rectangle source = new Rectangle(0, 0, ginfo.Viewport.Width, ginfo.Viewport.Height);
            render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Deferred, SamplerState.LinearWrap, BlendState.Opaque, RasterizerState.CullNone, DepthStencilState.Default);
            render.RenderTexture(tile, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            render.RenderEnd();
        }

        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IContentManager contentManager)
        {
            tile = factory.GetTexture2D("Textures/tile");


            FarseerWorld fworld = this.World.PhysicWorld as FarseerWorld;

            ///border
            border = new Border(fworld, factory, GraphicInfo, factory.CreateTexture2DColor(1, 1, Color.Red));

            ///from texture
            {
                Texture2D tex = factory.GetTexture2D("Textures//goo");
                tex = factory.GetScaledTexture(tex, new Vector2(4));
                IModelo2D model = new SpriteFarseer(tex);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model);
                I2DObject o = new I2DObject(fs, mat, model);
                o.OnHasMoved += new PloobsEngine.SceneControl._2DScene.OnHasMoved(o_OnHasMoved);
                this.World.AddObject(o);
            }
            
            ///camera
            this.World.Camera2D = new Camera2D(GraphicInfo);

            DPFSParticleSystem ps = new DPFSParticleSystem("TESTE", new SpriteParticleSystem(null));
            this.World.ParticleManager.AddAndInitializeParticleSystem(ps);

            ///add a post effect =P
            //this.RenderTechnic.AddPostEffect(new WigglePostEffect());

            ///updateable
            JointUpdateable ju = new JointUpdateable(this, fworld, this.World.Camera2D);

            base.LoadContent(GraphicInfo, factory, contentManager);
        }

        void o_OnHasMoved(I2DObject Reciever)
        {
            DPSFParticleManager DPSFParticleManager = this.World.ParticleManager as DPSFParticleManager;
            DPFSParticleSystem ParticleSystem = DPSFParticleManager.GetParticleSystem("TESTE") as DPFSParticleSystem;
            SpriteParticleSystem SpriteParticleSystem = ParticleSystem.IDPSFParticleSystem as SpriteParticleSystem;
            Vector2 v = Reciever.PhysicObject.Position; ///simulation position            
            SpriteParticleSystem.AttractorPosition = new Vector3(v, 0);
        }

        protected override void Draw(GameTime gameTime, PloobsEngine.SceneControl.RenderHelper render)
        {
            base.Draw(gameTime, render);

            border.Draw(render, this.World.Camera2D);

            render.RenderTextComplete("PloobsEngine 2D Sample on Windows Phone7", new Vector2(20, 10), Color.Red, Matrix.Identity);

        }

    }
}
