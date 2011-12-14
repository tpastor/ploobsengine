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
        Texture2D tile;        
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
        
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IContentManager contentManager)
        {
            tile = factory.GetTexture2D("Textures/tile");
            FarseerWorld fworld = this.World.PhysicWorld as FarseerWorld;

            //ground
            {
                Vertices Vertices = new Vertices(3);
                Vertices.Add(new Vector2(-GraphicInfo.BackBufferWidth / 2, -GraphicInfo.BackBufferHeight / 2));
                Vertices.Add(new Vector2(-GraphicInfo.BackBufferWidth / 2, -GraphicInfo.BackBufferHeight / 4));
                Vertices.Add(new Vector2(GraphicInfo.BackBufferWidth / 2, -GraphicInfo.BackBufferHeight / 2));
                
                SpriteFarseer SpriteFarseer = new SpriteFarseer(factory, Vertices, Color.Black,true);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, SpriteFarseer, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, SpriteFarseer);
                this.World.AddObject(o);

            }

            ///rectangle
            Vertices verts = PolygonTools.CreateRectangle(50,50);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Red, false);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                this.World.AddObject(o);
            }

            ///rectangle
            ////cria em Display
             verts = PolygonTools.CreateRectangle(10, 10);

            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Green,false);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                                
                ///usa Simulation unit 
                //o.PhysicObject.Position = new Vector2(-75, -75);
                this.World.AddObject(o);
            }

            verts = PolygonTools.CreateRectangle(1, 1);

            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Yellow, false);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model.Texture, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                
                o.PhysicObject.Position = new Vector2(25, 0);
                this.World.AddObject(o);
            }

            ///rectangle
             verts = PolygonTools.CreateRectangle(5, 5);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Orange);
                model.LayerDepth = 0;
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model.Texture, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                o.PhysicObject.Position = new Vector2(-GraphicInfo.BackBufferWidth / 2, 0);
                this.World.AddObject(o);
            }

            ///circle
            CircleShape circle = new CircleShape(5, 1);
            {
                IModelo2D model = new SpriteFarseer(factory, circle, Color.Orange);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model);
                I2DObject o = new I2DObject(fs, mat, model);
                o.PhysicObject.Position = new Vector2(0, -500);
                this.World.AddObject(o);
            }


            ///from texture
            {
                Texture2D tex = factory.GetTexture2D("Textures//goo");
                IModelo2D model = new SpriteFarseer(tex);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model);
                I2DObject o = new I2DObject(fs, mat, model);
                this.World.AddObject(o);
            }


            ///animated sprite
            {
                Texture2D tex = factory.GetTexture2D("Textures//DudeSheet");                
                SpriteAnimated sa = new SpriteAnimated(tex, 8, 2);
                sa.AddAnimation("ANIM1", 1, 8, 0);
                sa.AddAnimation("ANIM2", 2, 4, MathHelper.PiOver2);

                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                Texture2D frame = factory.GetTexturePart(tex, sa.GetFrameRectangle("ANIM1", 0));
                FarseerObject fs = new FarseerObject(fworld, frame);
                
                I2DObject sheet = new I2DObject(fs, mat, sa);
                sheet.PhysicObject.Position = new Vector2(100, 100);
                this.World.AddObject(sheet);
            }

            ///camera
            this.World.Camera2D = new Camera2D(GraphicInfo);
            base.LoadContent(GraphicInfo, factory, contentManager);
        }



        
        protected override void Draw(GameTime gameTime, PloobsEngine.SceneControl.RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("PloobsEngine 2D Sample on Windows Phone7", new Vector2(20, 10), Color.Red, Matrix.Identity);

        }        

    }
}
