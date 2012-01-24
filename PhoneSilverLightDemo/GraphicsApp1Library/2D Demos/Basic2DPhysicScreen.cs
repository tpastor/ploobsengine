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
using PloobsEngine.Input;
using PloobsEngine.Physic2D;
using PloobsEngine.Features.DebugDraw;

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
    public class Basic2DPhysicScreen : I2DScene
    {        
        I2DObject ball;
        Texture2D tile;
        I2DObject goo;
        Primitive2DDraw Primitive2DDraw;
        String result = null;
        bool fired = false;
        bool mousepressed = false;
        Lines lines;

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
            ///enable draw components
            rt.UseDrawComponents = true;
            renderTech = rt;                  
            ///creating the world =P
            world = new I2DWorld(new FarseerWorld(new Vector2(0, 9.8f)),new DPSFParticleManager());            
        }


        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {            
            Primitive2DDraw = new PloobsEngine.Features.DebugDraw.Primitive2DDraw();
            engine.AddComponent(Primitive2DDraw);
            base.InitScreen(GraphicInfo, engine);
        }


        /// <summary>
        /// render the background
        /// Can be used to make paralax ... for example
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

            //add the 2d primitive draw component
            lines = new Lines();
            Primitive2DDraw.Add2DPrimitive(lines);

            ///recover the physic world reference
            FarseerWorld fworld = this.World.PhysicWorld as FarseerWorld;
            
            ///Ground 1
            Vertices verts = PolygonTools.CreateRectangle(GraphicInfo.BackBufferWidth, 100);
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

            ///Ground 2
            verts = PolygonTools.CreateRectangle(GraphicInfo.BackBufferWidth, 100);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Green);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                ///the origin of the created object will be in the center of it, this mean: if we draw it, the center of mass of it will be on the midle of the screen
                ///We need to translate it a bit down                
                o.PhysicObject.Position = new Vector2(GraphicInfo.BackBufferWidth, 250);
                this.World.AddObject(o);
            }

            ///Ground 3
            verts = PolygonTools.CreateRectangle(GraphicInfo.BackBufferWidth, 100);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Yellow);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                ///the origin of the created object will be in the center of it, this mean: if we draw it, the center of mass of it will be on the midle of the screen
                ///We need to translate it a bit down                
                o.PhysicObject.Position = new Vector2(GraphicInfo.BackBufferWidth * 2, 250);
                this.World.AddObject(o);
            }

            ///Support 
            verts = PolygonTools.CreateRectangle(50, 200);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Yellow);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Static);
                I2DObject o = new I2DObject(fs, mat, model);
                ///the origin of the created object will be in the center of it, this mean: if we draw it, the center of mass of it will be on the midle of the screen
                ///We need to translate it a bit down                
                o.PhysicObject.Position = new Vector2(0, 100);
                this.World.AddObject(o);
            }

            ///plataform  1
            verts = PolygonTools.CreateRectangle(50, 200);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Green);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Dynamic);
                I2DObject o = new I2DObject(fs, mat, model);
                ///the origin of the created object will be in the center of it, this mean: if we draw it, the center of mass of it will be on the midle of the screen
                ///We need to translate it a bit down                
                o.PhysicObject.Position = new Vector2(GraphicInfo.BackBufferWidth * 1.5f, 100);
                this.World.AddObject(o);
            }

            ///plataform  2
            verts = PolygonTools.CreateRectangle(50, 200);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Green);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Dynamic);
                I2DObject o = new I2DObject(fs, mat, model);
                ///the origin of the created object will be in the center of it, this mean: if we draw it, the center of mass of it will be on the midle of the screen
                ///We need to translate it a bit down                
                o.PhysicObject.Position = new Vector2(GraphicInfo.BackBufferWidth * 1.6f, 100);
                this.World.AddObject(o);
            }

            ///plataform  3
            verts = PolygonTools.CreateRectangle(200, 50);
            {
                IModelo2D model = new SpriteFarseer(factory, verts, Color.Green);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model, 1, BodyType.Dynamic);
                I2DObject o = new I2DObject(fs, mat, model);
                ///the origin of the created object will be in the center of it, this mean: if we draw it, the center of mass of it will be on the midle of the screen
                ///We need to translate it a bit down                
                o.PhysicObject.Position = new Vector2(GraphicInfo.BackBufferWidth * 1.55f, -100);
                this.World.AddObject(o);
            }

            ///objective            
            {
                Texture2D tex = factory.GetTexture2D("Textures//goo");
                tex = factory.GetScaledTexture(tex,new Vector2(2));
                IModelo2D model = new SpriteFarseer(tex);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model);                
                goo = new I2DObject(fs, mat, model);
                goo.PhysicObject.Position = new Vector2(GraphicInfo.BackBufferWidth * 1.55f, -175);
                this.World.AddObject(goo);
            }

            ///Ball
            CircleShape circle = new CircleShape(50, 1);
            {
                IModelo2D model = new SpriteFarseer(factory, circle, Color.Orange);
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                FarseerObject fs = new FarseerObject(fworld, model);
                ball = new I2DObject(fs, mat, model);
                ball.PhysicObject.Position = new Vector2(0, -25);
                ball.OnUpdate += new PloobsEngine.SceneControl._2DScene.OnUpdate(ball_OnUpdate);
                this.World.AddObject(ball);
            }            
            
            ///the basic ortographic 2D camera
            Camera2D Camera2D = new Camera2D(GraphicInfo);
            Camera2D.Position = new Vector2(GraphicInfo.BackBufferWidth * 1.6f, 0);
            ///teleport to the given position
            Camera2D.Jump2Target();

            this.World.Camera2D = Camera2D;

            ///go to the desired position but without teleport =P
            Camera2D.IntertiaController = 0.09f;
            Camera2D.Position = new Vector2(0, 0);
            Camera2D.EnablePositionTracking = true;
            Camera2D.ReachedTheTrackingPosition += new Action<PloobsEngine.SceneControl._2DScene.Camera2D>(Camera2D_ReachedTheTrackingPosition);

            base.LoadContent(GraphicInfo, factory, contentManager);

            
        }

        void Camera2D_ReachedTheTrackingPosition(Camera2D obj)
        {
            SimpleConcreteGestureInputPlayable SimpleConcreteMouseBottomInputPlayable1 = new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.FreeDrag,
                 (sample) =>
                 {
                     mousepressed = true;
                 }
             );
            this.BindInput(SimpleConcreteMouseBottomInputPlayable1);


            SimpleConcreteGestureInputPlayable SimpleConcreteMouseBottomInputPlayable = null;
            SimpleConcreteMouseBottomInputPlayable = new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.DragComplete,
                 (sample) =>
                 {
                     mousepressed = false;
                     lines.isEnabled = false;
                     fired = true;

                     Vector2 mpos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                     Vector2 wpos = this.World.Camera2D.ConvertScreenToWorld(mpos);
                     Vector2 force = (ball.PhysicObject.Position - wpos) * 30;
                     ball.PhysicObject.ApplyForce(force);
                     this.RemoveInputBinding(SimpleConcreteMouseBottomInputPlayable);
                     this.RemoveInputBinding(SimpleConcreteMouseBottomInputPlayable1);

                     (this.World.Camera2D as Camera2D).TrackingBody = ball;
                     (this.World.Camera2D as Camera2D).IntertiaController = 1;
                 }
             );
            this.BindInput(SimpleConcreteMouseBottomInputPlayable);            
        }

        void ball_OnUpdate(I2DObject obj, GameTime gt)
        {
            if (fired)
            {
                if (((obj.PhysicObject.Position.Y > 500 || (obj.PhysicObject as FarseerObject).LinearVelocity.Length() < 0.5f) && (goo.PhysicObject as FarseerObject).LinearVelocity.Length() < 0.5f) || goo.PhysicObject.Position.Y > 150)
                {
                    ///victory condition =P
                    if (goo.PhysicObject.Position.Y > 150)
                    {
                        ///VC GANHOU EBA !!!                        
                        result = "YOU HAVE WON !!!!!!!!!!!!!!!!";
                        fired = false;
                    }
                    else
                    {
                        ///you have lost playba =P                        
                        result = "YOU HAVE LOST !!!!!!!!!!!!!!";
                        fired = false;
                    }
                }                
            }
        }
                
        protected override void Update(GameTime gameTime)
        {
            if (mousepressed)
            {
                lines.isEnabled = true;
                lines.Clear();
                Vector2 mpos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Vector2 wpos = this.World.Camera2D.ConvertScreenToWorld(mpos);

                lines.AddLine(wpos, ball.PhysicObject.Position);
            }
            base.Update(gameTime);            
        }

        protected override void Draw(GameTime gameTime, PloobsEngine.SceneControl.RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("PloobsEngine 2D Angry Birds Physic Clone Sample", new Vector2(20, 10), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Hold gesture near the ball to fire", new Vector2(20, 30), Color.Red, Matrix.Identity);
            if(result != null)
                render.RenderTextComplete(result, new Vector2(20, 60), Color.Red, Matrix.CreateScale(2));            
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent(PloobsEngine.Features.DebugDraw.Primitive2DDraw.myName);
            base.CleanUp(engine);
        }

    }
}