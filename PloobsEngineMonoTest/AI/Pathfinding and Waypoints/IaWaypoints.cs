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
using PloobsEngine.Input;
using EngineTestes.AI;
using PloobsEngine.Features.DebugDraw;
using PloobsEngine.Commands;

namespace ProjectTemplate
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class IaWaypoints : IScene
    {
        DebugShapesDrawer ddrawer = new DebugShapesDrawer(true);
        DebugLines dlines;
        WaypointHandler wh;
        bool saved = false;

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

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
            engine.AddComponent(new DebugDraw());
            engine.IsMouseVisible = true;
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

            RegisterDebugDrawCommand rc = new RegisterDebugDrawCommand(ddrawer);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(rc);
            dlines = new DebugLines();
            ddrawer.AddShape(dlines);

            Picking pick = new Picking(this);
            pick.OnPickedLeftButton += new OnPicked(pick_OnPickedLeftButton);

            wh = new WaypointHandler();

            ///Uncoment to add your model
            SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
            simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
            ///Physic info (position, rotation and scale are set here)
            BoxObject tmesh = new BoxObject(Vector3.Zero, 1, 1, 1, 10, new Vector3(1000, 1, 1000), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
            ///Forward Shader (look at this shader construction for more info)
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            ///Deferred material
            ForwardMaterial fmaterial = new ForwardMaterial(shader);
            ///The object itself
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            ///Add to the world
            this.World.AddObject(obj);

            {
                InputPlayableKeyBoard ipk = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS,Microsoft.Xna.Framework.Input.Keys.Space);
                ipk.KeyStateChange += new KeyStateChange(ipk_KeyStateChange);
                this.BindInput(ipk);
            }

            {
                InputPlayableKeyBoard ipk = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Microsoft.Xna.Framework.Input.Keys.Enter);
                ipk.KeyStateChange += new KeyStateChange(ipk_KeyStateChange2);
                this.BindInput(ipk);
            }
            
            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        void pick_OnPickedLeftButton(SegmentInterceptInfo SegmentInterceptInfo)
        {
            Waypoint cw = new Waypoint()
            {
                AditionalInfo = null,
                WayType = WAYPOINTTYPE.NORMAL,
                WorldPos =  SegmentInterceptInfo.ImpactPosition,               
              
            };
            wh.AddWaypointUnconnected(cw);
        }

        void ipk_KeyStateChange(InputPlayableKeyBoard ipk)
        {
            if (wh.CurrentWaypointsCollection.State == WaypointsState.UnConnected)
            {
                List<Waypoint> waypointsList = wh.CurrentWaypointsCollection.GetWaypointsList();
                if (waypointsList.Count > 0)
                {
                    VisibilityWaypointConnector VisibilityWaypointConnector = new VisibilityWaypointConnector(this.World);
                    wh.ConnectWaypoints(VisibilityWaypointConnector);
                }
            }
        }

        void ipk_KeyStateChange2(InputPlayableKeyBoard ipk)
        {
            if (wh.CurrentWaypointsCollection.State == WaypointsState.Connected)
            {
                wh.SaveConnectedWaypoints("waypoints.xml");
                saved = true;
            }
            
        }


        protected override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);

            dlines.ClearLines();
            foreach (var item in wh.CurrentWaypointsCollection.GetWaypointsList())
            {
                dlines.AddLine(item.WorldPos, item.WorldPos + Vector3.Up * 500, Color.Red);
                if (item.NeightBorWaypointsId != null)
                {
                    foreach (int viz in item.NeightBorWaypointsId)
                    {
                        dlines.AddLine(item.WorldPos + Vector3.Up * 20, wh.CurrentWaypointsCollection.IdWaypoint[viz].WorldPos + Vector3.Up * 20, Color.Yellow);                        
                    }
                }            
            }
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            ///Draw some text on the screen
            render.RenderTextComplete("IA: Placing Waypoints", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("WaypointsState: " + wh.CurrentWaypointsCollection.State, new Vector2(GraphicInfo.Viewport.Width - 315, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Quantity: " + wh.CurrentWaypointsCollection.GetWaypointsList().Count , new Vector2(GraphicInfo.Viewport.Width - 315, 55), Color.White, Matrix.Identity);
            render.RenderTextComplete("Saved: " + saved, new Vector2(GraphicInfo.Viewport.Width - 315, 75), Color.White, Matrix.Identity);
            
            
        }

    }
}
