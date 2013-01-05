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

namespace EngineTestes
{
    public class Basic2DSpriteScreen : I2DScene
    {        
        Texture2D tile;
        I2DObject sheet;
        protected override void SetWorldAndRenderTechnich(out RenderTechnich2D renderTech, out I2DWorld world)
        {
            Basic2DRenderTechnich rt = new Basic2DRenderTechnich();            
            rt.RenderBackGround += new RenderBackGround(rt_RenderBackGround);
            renderTech = rt;            
            world = new I2DWorld(new FarseerWorld(new Vector2(0, 9.8f)));            
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

            ///Ground
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

            ///animated sprite
            {
                ///loading the texture
                Texture2D tex = factory.GetTexture2D("Textures//DudeSheet");
                ///scale the texture (this is not good specially with animated texture, cause the whole texture is being scalled)
                tex = factory.GetScaledTexture(tex, new Vector2(2));
                ///Loading the Sprite and extracting the frames 
                ///8 = Maximum number of frames in the horizontal
                ///2 = Number of animation
                ///See the texture DudeSheet to undertand
                SpriteAnimated sa = new SpriteAnimated(tex, 8, 2);
                ///Specify the first animation (First "line" of the texture) -- see the extra parameters in addAnimation
                sa.AddAnimation("ANIM1", 1, 8, 0);
                ///Specify the Second animation (Second "line" of the texture)
                sa.AddAnimation("ANIM2", 2, 4, 0);

                ///Create the Material
                Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
                ///To create the physic object, we extract one frame from the image and use it to be our physic body =P
                Texture2D frame = factory.GetTexturePart(tex, sa.GetFrameRectangle("ANIM1", 0));
                FarseerObject fs = new FarseerObject(fworld, frame);                
                sheet = new I2DObject(fs, mat, sa);
                sheet.PhysicObject.Position = new Vector2(0,0);
                this.World.AddObject(sheet);
            }

            ///camera
            this.World.Camera2D = new Camera2D(GraphicInfo);
            base.LoadContent(GraphicInfo, factory, contentManager);
            
            ///when double tap, perform the following action
            this.BindInput(new SimpleConcreteKeyboardInputPlayable(StateKey.DOWN,Keys.Space,
                (a) =>
                {
                    animationIndex = (animationIndex + 1) % 2;
                    (sheet.Modelo as SpriteAnimated).ChangeAnimation(animations[animationIndex]);
                    ///you can play, pause, change ..... using the SpriteAnimated object.
                }
                ));
        }
        int animationIndex = 0;
        String[] animations = new string[] { "ANIM1", "ANIM2" };
        
        protected override void Draw(GameTime gameTime, PloobsEngine.SceneControl.RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("PloobsEngine 2D Sprite Sample", new Vector2(20, 10), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Double tap to change the animation", new Vector2(20, 30), Color.Red, Matrix.Identity);
        }        

    }
}
