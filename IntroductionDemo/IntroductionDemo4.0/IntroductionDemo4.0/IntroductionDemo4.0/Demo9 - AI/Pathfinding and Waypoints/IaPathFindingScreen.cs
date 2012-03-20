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
using PloobsEngine.Input;
using PloobsEngine.IA;
using PloobsEngine.Features.DebugDraw;
using PloobsEngine.Commands;
using EngineTestes.AI;

namespace IntroductionDemo4._0
{
    public class IaPathFindingScreen : IScene
    {
        DebugShapesDrawer ddrawer = new DebugShapesDrawer(true);
        DebugLines dlines;
        WaypointHandler wh;
        Waypoint start;
        LinkedList<Waypoint> path = null;
        Vector3? destiny = null;


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

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent(DebugDraw.MyName);
            base.CleanUp(engine);
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
            
            Picking pick = new Picking(this,2000);
            pick.OnPickedLeftButton += new OnPicked(pick_OnPickedLeftButton);
                        
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

            wh = new WaypointHandler();
            wh.LoadConnectedWaypoints("waypoints.xml");
            start = wh.CurrentWaypointsCollection.GetWaypointsList()[0];

            {
                ///Procedural yellow diffuse texture
                SimpleModel sm = new SimpleModel(factory, "Model\\block");
                sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Yellow), TextureType.DIFFUSE);
                ///physic Ghost object(no collision)
                GhostObject pi = new GhostObject(start.WorldPos, Matrix.Identity, new Vector3(5));
                ForwardXNABasicShader s = new ForwardXNABasicShader();                
                ForwardMaterial mat = new ForwardMaterial(s);
                IObject obj4 = new IObject(mat, sm, pi);
                this.World.AddObject(obj4);
            }

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            {
                SimpleModel sm = new SimpleModel(factory, "Model\\block");
                sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Blue), TextureType.DIFFUSE);                
                GhostObject pi = new GhostObject(start.WorldPos, Matrix.Identity, new Vector3(5));
                ForwardXNABasicShader s = new ForwardXNABasicShader();                
                ForwardMaterial mat = new ForwardMaterial(s);
                IObject obj4 = new IObject(mat, sm, pi);
                obj4.OnUpdate += new OnUpdate(obj4_OnUpdate);
                this.World.AddObject(obj4);
            }

        }

        void obj4_OnUpdate(IObject obj, GameTime gt, ICamera cam)
        {
            if (pathchanged)
            {
                obj.IObjectAttachment.Clear();
                obj.IObjectAttachment.Add(new MoverAtachment(path));
                pathchanged = false;
            }
        }

        bool pathchanged= false;

        void pick_OnPickedLeftButton(SegmentInterceptInfo SegmentInterceptInfo)
        {
            SimpleMap sm = new SimpleMap(wh.CurrentWaypointsCollection);
            AStar astar = new AStar(sm);
            path = astar.GetPath(start.WorldPos, SegmentInterceptInfo.ImpactPosition);
            ///the first will always be the start and the last should be the clicked position, not the last waypoint =P            
            path.AddLast(new Waypoint() { WorldPos = SegmentInterceptInfo.ImpactPosition });
            pathchanged = true;
            destiny = SegmentInterceptInfo.ImpactPosition;
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
            if(destiny != null)
                dlines.AddLine(destiny.Value, destiny.Value+ Vector3.Up * 500, Color.Violet);
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
            render.RenderTextComplete("Demo: PathFinding", new Vector2(GraphicInfo.Viewport.Width - 715, 15), Color.Red, Matrix.Identity);
            render.RenderTextComplete("It uses the Waypoints generated in the last Screen", new Vector2(GraphicInfo.Viewport.Width - 715, 35), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Left Mouse Bottom to place an endpoint for the BlockObject", new Vector2(GraphicInfo.Viewport.Width - 715, 55), Color.Red, Matrix.Identity);
            
        }

    }
}
