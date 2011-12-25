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

namespace EngineTestes
{
    public class Basic2DScreen : I2DScene
    {
        JointUpdateable ju;
        Texture2D tile;
        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {            
            base.InitScreen(GraphicInfo, engine);
        }

        Border border;
        protected override void SetWorldAndRenderTechnich(out RenderTechnich2D renderTech, out I2DWorld world)
        {
            Basic2DRenderTechnich rt = new Basic2DRenderTechnich();            
            rt.RenderBackGround += new RenderBackGround(rt_RenderBackGround);
            renderTech = rt;

            world = new I2DWorld(new FarseerWorld(new Vector2(0, 9.8f)),new DPSFParticleManager());            
        }

        void rt_RenderBackGround(GraphicInfo ginfo,RenderHelper render)
        {
            Rectangle source = new Rectangle(0, 0, ginfo.Viewport.Width, ginfo.Viewport.Height);
            render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Deferred, SamplerState.LinearWrap, BlendState.Opaque, RasterizerState.CullNone,DepthStencilState.Default);
            render.RenderTexture(tile, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            render.RenderEnd();            
        }

        public void Move(int mult)
        {
            this.World.Camera2D.MoveCamera(Vector2.UnitX * mult);
        }

        public void Trace(bool enable = true)
        {
            (this.World.Camera2D as Camera2D).EnableTracking= enable;
            if (enable)
            {
                (this.World.Camera2D as Camera2D).TrackingBody = partobj;
            }
            else
            {
                ///ok ok ok ... i know ..
                this.World.Camera2D = new Camera2D(GraphicInfo);
                ju.Camera = this.World.Camera2D; 
            }
        }

        I2DObject partobj;
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
                tex = factory.GetScaledTexture(tex, new Vector2(3));
                IModelo2D model = new SpriteFarseer(tex);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, tex);
                partobj = new I2DObject(fs, mat, model);
                partobj.OnHasMoved += new PloobsEngine.SceneControl._2DScene.OnHasMoved(o_OnHasMoved);
                this.World.AddObject(partobj);
            }

            ///rectangle
            Vertices verts = PolygonTools.CreateRectangle(5, 5);
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
                IModelo2D model = new SpriteFarseer(factory, circle, Color.Orange);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();                
                FarseerObject fs = new FarseerObject(fworld, circle);
                I2DObject o = new I2DObject(fs, mat, model);
                this.World.AddObject(o);
            }

            ///camera
            this.World.Camera2D = new Camera2D(GraphicInfo);

            DPFSParticleSystem ps = new DPFSParticleSystem("TESTE", new SpriteParticleSystem(null));
            this.World.ParticleManager.AddAndInitializeParticleSystem(ps);

            ///updateable
             ju = new JointUpdateable(this, fworld, this.World.Camera2D);
           
            base.LoadContent(GraphicInfo, factory, contentManager);
        }

        public void AtractorStrengh(float amount)
        {
            DPSFParticleManager DPSFParticleManager = this.World.ParticleManager as DPSFParticleManager;
            DPFSParticleSystem ParticleSystem = DPSFParticleManager.GetParticleSystem("TESTE") as DPFSParticleSystem;
            SpriteParticleSystem SpriteParticleSystem = ParticleSystem.IDPSFParticleSystem as SpriteParticleSystem;
            SpriteParticleSystem.AttractorStrength += amount;        
        }


        int i = 0;
        public void ChangeMode()
        {
            i = (i + 1) % 3;
            DPSFParticleManager DPSFParticleManager = this.World.ParticleManager as DPSFParticleManager;
            DPFSParticleSystem ParticleSystem = DPSFParticleManager.GetParticleSystem("TESTE") as DPFSParticleSystem;
            SpriteParticleSystem SpriteParticleSystem = ParticleSystem.IDPSFParticleSystem as SpriteParticleSystem;
            SpriteParticleSystem.AttractorMode = (EngineTestes.SpriteParticleSystem.EAttractorModes) (i + 1 );        

        }

        void o_OnHasMoved(I2DObject Reciever)
        {
            DPSFParticleManager DPSFParticleManager = this.World.ParticleManager as DPSFParticleManager;
            DPFSParticleSystem ParticleSystem = DPSFParticleManager.GetParticleSystem("TESTE") as DPFSParticleSystem;
            SpriteParticleSystem SpriteParticleSystem = ParticleSystem.IDPSFParticleSystem as SpriteParticleSystem;
            Vector2 v = Reciever.PhysicObject.Position; ///simulation position            
            SpriteParticleSystem.AttractorPosition = new Vector3(v,0);
         
        }
        
        protected override void Draw(GameTime gameTime, PloobsEngine.SceneControl.RenderHelper render)
        {
            base.Draw(gameTime, render);
            border.Draw(render, this.World.Camera2D);
            render.RenderTextComplete("PloobsEngine 2D Sample on Windows Phone7", new Vector2(20, 10), Color.Red, Matrix.Identity);

        }        

    }
}
