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
using DPSF.ParticleSystems;
using PloobsEngine.Light2D;
using PloobsEngine.Engine;
using PloobsEngine.Input;
using PloobsEngine.SceneControl._2DScene.Culler;

namespace EngineTestes._2DSamples
{
    public  class Basic2D : I2DScene
    {
        Texture2D tile;
        I2DObject sheet;

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            engine.IsMouseVisible = true;
            base.InitScreen(GraphicInfo, engine);
        }

        Border border;
        protected override void SetWorldAndRenderTechnich(out RenderTechnich2D renderTech, out I2DWorld world)
        {
            Basic2DRenderTechnich rt = new Basic2DRenderTechnich();
            rt.UsePostProcessing = false;
            rt.RenderBackGround += new RenderBackGround(rt_RenderBackGround);
            renderTech = rt;

            world = new I2DWorld(new FarseerWorld(new Vector2(0, 9.8f)),new DPSFParticleManager(), new Physic2DCuller());            
        }

        void rt_RenderBackGround(GraphicInfo ginfo,RenderHelper render)
        {
            Rectangle source = new Rectangle(0, 0, ginfo.Viewport.Width, ginfo.Viewport.Height);
            render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Deferred, SamplerState.LinearWrap, BlendState.Opaque, RasterizerState.CullNone,DepthStencilState.Default);
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
                IModelo2D model = new SpriteFarseer(tex);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, tex);
                I2DObject o = new I2DObject(fs, mat, model);                
                this.World.AddObject(o);
            }

            ///from texture, scale usage sample
            {
                Texture2D tex = factory.GetTexture2D("Textures//goo");
                tex = factory.GetScaledTexture(tex, new Vector2(2));
                IModelo2D model = new SpriteFarseer(tex);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, tex);
                I2DObject o = new I2DObject(fs, mat, model);
                o.OnHasMoved += new PloobsEngine.SceneControl._2DScene.OnHasMoved(o_OnHasMoved);
                this.World.AddObject(o);
            }

            ///rectangle
            Vertices verts = PolygonTools.CreateRectangle(50, 50);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Orange);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, verts);
                I2DObject o = new I2DObject(fs, mat, model);
                this.World.AddObject(o);
            }

            ///circle
            CircleShape circle = new CircleShape(5, 1);
            {
                IModelo2D model = new SpriteFarseer(factory, circle , Color.Orange);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, circle);
                I2DObject o = new I2DObject(fs, mat, model);
                this.World.AddObject(o);
            }

            ///animated sprite
            {
                Texture2D tex = factory.GetTexture2D("Textures//DudeSheet");
                SpriteAnimated sa = new SpriteAnimated(tex, 8, 2);                
                sa.AddAnimation("ANIM1", 1, 8,0);
                sa.AddAnimation("ANIM2", 2, 4, MathHelper.PiOver2);
                
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                Texture2D frame = factory.GetTexturePart(tex,sa.GetFrameRectangle("ANIM1",0));
                FarseerObject fs = new FarseerObject(fworld, frame);                

                //GhostObject fs = new GhostObject(Vector2.Zero);
                sheet = new I2DObject(fs, mat, sa);
                this.World.AddObject(sheet);
            }

            {
                PointLight2D l = new PointLight2D(new Vector2(-GraphicInfo.BackBufferWidth / 4, -GraphicInfo.BackBufferWidth / 4), Color.Red, 1);
                this.World.AddLight(l);
            }

            {
                SpotLight2D l = new SpotLight2D(new Vector2(+GraphicInfo.BackBufferWidth / 4, -GraphicInfo.BackBufferWidth / 4), Color.Blue, new Vector2(0, 1), MathHelper.ToRadians(45));
                this.World.AddLight(l);
            }
            
            {
            SimpleConcreteKeyboardInputPlayable sc = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS,Keys.Space);
            sc.KeyStateChange+=new KeyStateChange(sc_KeyStateChange);
            this.BindInput(sc);
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

        void sc_KeyStateChange(InputPlayableKeyBoard ipk)
        {
            SpriteAnimated sa = sheet.Modelo as SpriteAnimated;
            if (sa.Animation == "ANIM1")
                sa.Animation = "ANIM2";
            else
                sa.Animation = "ANIM1";
        }

        void o_OnHasMoved(I2DObject Reciever)
        {
            DPSFParticleManager DPSFParticleManager = this.World.ParticleManager as DPSFParticleManager;
            DPFSParticleSystem ParticleSystem = DPSFParticleManager.GetParticleSystem("TESTE") as DPFSParticleSystem;
            SpriteParticleSystem SpriteParticleSystem = ParticleSystem.IDPSFParticleSystem as SpriteParticleSystem;
            Vector2 v = Reciever.PhysicObject.Position; ///simulation position            
            SpriteParticleSystem.AttractorPosition = new Vector3(v,0);
        }

        protected override void Update(GameTime gameTime)
        {
            HandleCamera(gameTime);

            base.Update(gameTime);
        }        

        private void HandleCamera(GameTime gameTime)
        {
            Vector2 camMove = Vector2.Zero;
            KeyboardState KeyboardState =  Keyboard.GetState();
            if (KeyboardState.IsKeyDown(Keys.Up))
            {
                camMove.Y -= 10f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (KeyboardState.IsKeyDown(Keys.Down))
            {
                camMove.Y += 10f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (KeyboardState.IsKeyDown(Keys.Left))
            {
                camMove.X -= 10f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (KeyboardState.IsKeyDown(Keys.Right))
            {
                camMove.X += 10f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (KeyboardState.IsKeyDown(Keys.PageUp))
            {
                this.World.Camera2D.Zoom += 5f * (float)gameTime.ElapsedGameTime.TotalSeconds * this.World.Camera2D.Zoom / 20f;
            }
            if (KeyboardState.IsKeyDown(Keys.PageDown))
            {
                this.World.Camera2D.Zoom -= 5f * (float)gameTime.ElapsedGameTime.TotalSeconds * this.World.Camera2D.Zoom / 20f;
            }
            if (camMove != Vector2.Zero)
            {
                this.World.Camera2D.MoveCamera(camMove);
            }            
        }

        protected override void Draw(GameTime gameTime, PloobsEngine.SceneControl.RenderHelper render)
        {
            base.Draw(gameTime, render);

            border.Draw(render, this.World.Camera2D);

            render.RenderTextComplete("Objects rendered: " + World.Culler.RenderedObjectThisFrame, new Vector2(10, 10), Color.White, Matrix.Identity);

        }

    }
}
