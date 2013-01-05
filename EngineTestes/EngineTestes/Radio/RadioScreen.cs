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
using EngineTestes.AI.FSM;
using PloobsEngine.IA;
using Microsoft.Xna.Framework.Input;
using EngineTestes.Radio;
using PloobsEngine.MessageSystem;

namespace ProjectTemplate
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class RadioScreen : IScene
    {

        String messagesArrived = "";
        Radio radio = new Radio();

        Radio reciever= new Radio();

        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the IWorld
            world = new IWorld(new BepuPhysicWorld(-0.97f, true, 1), new SimpleCuller());

            ///Create the deferred technich
            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription();
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            engine.AddComponent(new MessageDeliver());
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
            radio.SendMessage("SYSTEM", "BEGIN LOAD CONTENT " + DateTime.Now);
            
            base.LoadContent(GraphicInfo, factory, contentManager);

            reciever.MessageHandler += new Action<PloobsEngine.MessageSystem.Message>(reciever_MessageHandler);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
                ///Physic info (position, rotation and scale are set here)
                BoxObject tmesh = new BoxObject(Vector3.Zero, 1, 1, 1, 10, new Vector3(1000, 1, 1000), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                tmesh.isMotionLess = true;
                ///Forward Shader (look at this shader construction for more info)
                ForwardXNABasicShader shader = new ForwardXNABasicShader();
                ///Deferred material
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                ///The object itself
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                ///Add to the world
                this.World.AddObject(obj);
                
            }

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            radio.SendMessage("SYSTEM", "END LOAD CONTENT " + DateTime.Now);
        }

        void reciever_MessageHandler(PloobsEngine.MessageSystem.Message obj)
        {
            messagesArrived += obj.Content as String;
            messagesArrived += "\n";
        }

        protected override void Update(GameTime gameTime)
        {
            radio.SendMessage("SYSTEM", "BEGIN UPDATE " + DateTime.Now);
            base.Update(gameTime);
            radio.SendMessage("SYSTEM", "END UPDATE " + DateTime.Now);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            radio.SendMessage("SYSTEM", "BEGIN DRAW " + DateTime.Now);
            base.Draw(gameTime, render);
            radio.SendMessage("SYSTEM", "END DRAW " + DateTime.Now);

            render.RenderTextComplete(messagesArrived, new Vector2(10, 10), Color.White);
            messagesArrived = "";
        }


    }
}
