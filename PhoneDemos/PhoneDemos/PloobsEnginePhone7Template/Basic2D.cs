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
using PloobsEngine.Modelo2D;
using PloobsEngine.SceneControl;
using PloobsEngine.Particles;
using PloobsEngine.Light2D;
using PloobsEngine.Engine;

namespace EngineTestes._2DSamples
{
    public class Basic2D : I2DScene
    {

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
        }

        protected override void SetWorldAndRenderTechnich(out RenderTechnich2D renderTech, out I2DWorld world)
        {
            Basic2DRenderTechnich rt = new Basic2DRenderTechnich();
            renderTech = rt;
            world = new I2DWorld(new FarseerWorld(new Vector2(0, 9.8f)), new DPSFParticleManager());
        }


        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IContentManager contentManager)
        {

            //FarseerWorld fworld = this.World.PhysicWorld as FarseerWorld;

            /////from texture
            //{
            //    Texture2D tex = factory.GetTexture2D("Textures//goo");
            //    IModelo2D model = new SpriteFarseer(tex);
            //    Basic2DTextureMaterial mat = new Basic2DTextureMaterial();
            //    FarseerObject fs = new FarseerObject(fworld, tex);
            //    I2DObject o = new I2DObject(fs, mat, model);            
            //    this.World.AddObject(o);
            //}


            ///camera
            this.World.Camera2D = new Camera2D(GraphicInfo);

            base.LoadContent(GraphicInfo, factory, contentManager);
        }
    }
}
