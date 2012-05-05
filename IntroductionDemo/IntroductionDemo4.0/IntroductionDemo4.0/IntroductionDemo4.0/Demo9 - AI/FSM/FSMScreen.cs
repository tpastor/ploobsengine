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
using PloobsEngine.IA;
using Microsoft.Xna.Framework.Input;

namespace IntroductionDemo4._0
{
    public class FSMScreen : IScene
    {
        StateMachine fsm;
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

        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

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

                ///create the fsm
                fsm = new StateMachine();
                ///atach the fsm to the object using the attachment property (there are lots of ways of doing this like extending the iobject class)
                AttachmentUpdate atachment = new AttachmentUpdate(
                    (a, b) => fsm.UpdateFSM(b)
                        );
                obj.IObjectAttachment.Add(atachment);

                ////Sample usage
                ///Press Space to go to state2 and press enter in state 2 to get back to state1 =P
                ///create state 1
                {
                    StateSample state1 = new StateSample("state1",obj);
                    state1.NextStateFunc =
                      (a) =>
                      {
                          KeyboardState kstate = Keyboard.GetState();
                          if (kstate.IsKeyDown(Keys.Space))
                          {
                              return "state2";
                          }
                          return null;
                      };
                    fsm.AddState(state1);

                    ///SET THE CURRENT STATE
                    fsm.SetCurrentState(state1.Name);
                }

                

                ///create state 2
                {
                    StateSample state2 = new StateSample("state2",obj);
                    state2.NextStateFunc =
                      (a) =>
                      {
                          KeyboardState kstate = Keyboard.GetState();
                          if (kstate.IsKeyDown(Keys.Enter))
                          {
                              return "state1";
                          }
                          return null;
                      };
                    fsm.AddState(state2);
                }
            }

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>x
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            ///Draw some text on the screen
            render.RenderTextComplete("Demo: Finite State Machine Basic Sample", new Vector2(GraphicInfo.Viewport.Width - 715, 15), Color.Red, Matrix.Identity);            
            render.RenderTextComplete("Current State: " + fsm.GetCurrentState().Name, new Vector2(GraphicInfo.Viewport.Width - 715, 35), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Space to go to state 2 from state 1 ", new Vector2(GraphicInfo.Viewport.Width - 715, 55), Color.Red, Matrix.Identity);            
            render.RenderTextComplete("Enter to go to state 1 from state 2", new Vector2(GraphicInfo.Viewport.Width - 715, 75), Color.Red, Matrix.Identity);            
            
        }

    }
}
