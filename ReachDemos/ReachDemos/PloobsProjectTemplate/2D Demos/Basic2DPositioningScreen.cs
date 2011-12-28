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
    /// <summary>
    /// IMPORTANT
    /// 2D COORDINATE SYSTEM !!!
    /// THE 0,0 IS IN THE MIDDLE OF THE SCREEN
    ///The Y grows to DOWN
    ///The X grows to RIGHT
    ///THE LEFT-DOWN CORNER IS: (-GraphicInfo.BackBufferWidth/2,GraphicInfo.BackBufferHeight/2)
    /// </summary>
    public class Basic2DPositioningScreen : I2DScene
    {        
        Texture2D tile;
        I2DObject goo;
        /// <summary>
        /// Called once on screen load
        /// </summary>
        /// <param name="renderTech"></param>
        /// <param name="world"></param>
        protected override void SetWorldAndRenderTechnich(out RenderTechnich2D renderTech, out I2DWorld world)
        {
            ////creating the rendering technic
            Basic2DRenderTechnich rt = new Basic2DRenderTechnich();            
            rt.RenderBackGround += new RenderBackGround(rt_RenderBackGround);
            renderTech = rt;            
            ///creating the world =P
            world = new I2DWorld(new FarseerWorld(new Vector2(0, 9.8f)),new DPSFParticleManager());            
        }


        /// <summary>
        /// render the background
        /// Can be use to make paralax ... for example
        /// </summary>
        /// <param name="ginfo"></param>
        /// <param name="render"></param>
        void rt_RenderBackGround(GraphicInfo ginfo,RenderHelper render)
        {
            Rectangle source = new Rectangle(0, 0, ginfo.Viewport.Width, ginfo.Viewport.Height);
            render.RenderBegin(Matrix.Identity, null, SpriteSortMode.Deferred, SamplerState.LinearWrap, BlendState.Opaque, RasterizerState.CullNone,DepthStencilState.Default);
            render.RenderTexture(tile, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            render.RenderEnd();            
        }
        
        /// <summary>
        /// Called once to load content
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IContentManager contentManager)
        {
            ///load background texture
            tile = factory.GetTexture2D("Textures/tile");

            ///recover the physic world reference
            FarseerWorld fworld = this.World.PhysicWorld as FarseerWorld;

            ///from vertices
            {
                ////creating objects from vertices
                Vertices Vertices = new Vertices(3);                
                Vertices.Add(new Vector2(0,0));
                Vertices.Add(new Vector2(100,0));
                Vertices.Add(new Vector2(0, -100));

                
                ///creating the IModelo (graphic representation)
                SpriteFarseer SpriteFarseer = new SpriteFarseer(factory, Vertices, Color.Green);
                ///The material (how to draw)
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                ///the physic object (physic representation)
                FarseerObject fs = new FarseerObject(fworld, SpriteFarseer, 1, BodyType.Static);
                ///the iobject (that comprises all)
                I2DObject o = new I2DObject(fs, mat, SpriteFarseer);
                ///adding to the world
                this.World.AddObject(o);

            }
            
            ///Creating from factory helper            
            Vertices verts = PolygonTools.CreateRectangle(GraphicInfo.BackBufferWidth, 100);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Red);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                ///the origin of the created object will be in the center of it, this mean: if we draw it, the center of mass of it will be on the midle of the screen
                ///We need to translate it a bit down                
                o.PhysicObject.Position = new Vector2(100,450);
                this.World.AddObject(o);
            }

            ///Creating from factory helper            
            verts = PolygonTools.CreateRectangle(GraphicInfo.BackBufferWidth, 100);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Red);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                ///the origin of the created object will be in the center of it, this mean: if we draw it, the center of mass of it will be on the midle of the screen
                ///We need to translate it a bit down                
                o.PhysicObject.Position = new Vector2(0, 250);
                this.World.AddObject(o);
            }
                       
            ///creating a circle =P
            CircleShape circle = new CircleShape(50, 1);
            {
                IModelo2D model = new SpriteFarseer(factory, circle, Color.Orange);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model);
                goo = new I2DObject(fs, mat, model);
                goo.PhysicObject.Position = new Vector2(0, -150); /// a middle of the screen + 150 pixels up
                this.World.AddObject(goo);
            }

            ///the basic ortographic 2D camera
            this.World.Camera2D = new Camera2D(GraphicInfo);
            base.LoadContent(GraphicInfo, factory, contentManager);
        }

        
        protected override void Draw(GameTime gameTime, PloobsEngine.SceneControl.RenderHelper render)
        {
            base.Draw(gameTime, render);

            ///to draw some text =P
            render.RenderTextComplete("PloobsEngine 2D Basic Objects Sample", new Vector2(20, 10), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Goo Object position: " + goo.PhysicObject.Position, new Vector2(20, 30), Color.Red, Matrix.Identity);
            
        }        

    }
}
