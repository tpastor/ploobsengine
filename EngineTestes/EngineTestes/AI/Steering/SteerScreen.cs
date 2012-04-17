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
using Bnoerj.AI.Steering;
using Bnoerj.AI.Steering.Pedestrian;
using EngineTestes.AI;
using PloobsEngine.Features.DebugDraw;
using PloobsEngine.Commands;

namespace ProjectTemplate
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class SteerScreen : IScene
    {
        IPlugIn PlugIn;
        Path path = new Path(new List<Vector3>() { new Vector3(50, 50, 50), new Vector3(50, 50, 0), new Vector3(0, 50, 0) }, 5);
        DebugShapesDrawer ddrawer = new DebugShapesDrawer(true);        
        
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

            path.AddCircularObstacle(Vector3.Zero, 10);

            path.AddCircularObstacle(new Vector3(0,20,0), 15);

            path.AddCircularObstacle(new Vector3(200), 10);

            foreach (var item in path.Obstacles)
            {
                SphericalObstacle SphericalObstacle = item as SphericalObstacle;
                DebugSphere s = new DebugSphere(SphericalObstacle.Center, SphericalObstacle.Radius,Color.Blue);
                ddrawer.AddShape(s);                
            }
            //DebugSphere.WireFrameEnabled = true;

            DebugLines dls = new DebugLines();
            ddrawer.AddShape(dls);
            for (int i = 0; i < path.PolyPath.pointCount - 1; i++)
			{
                dls.AddLine(path.PolyPath.points[i], path.PolyPath.points[i ] + Vector3.Up * 200, Color.Brown);
                dls.AddLine(path.PolyPath.points[i], path.PolyPath.points[i + 1], Color.Red);
			}
            dls.AddLine(path.PolyPath.points[path.PolyPath.pointCount - 1], path.PolyPath.points[path.PolyPath.pointCount - 1] + Vector3.Up * 200, Color.Brown);

            PlugIn = new PedestrianPlugIn(this.World, path,
                (pd) =>
                {
                    SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                    simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Green), TextureType.DIFFUSE);
                    ///Physic info (position, rotation and scale are set here)
                    GhostObject tmesh = new GhostObject();
                    ///Forward Shader (look at this shader construction for more info)
                    ForwardXNABasicShader shader = new ForwardXNABasicShader();
                    ///Deferred material
                    ForwardMaterial fmaterial = new ForwardMaterial(shader);
                    ///The object itself
                    IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                    obj.IObjectAttachment.Add(new SteerAtachment(pd));
                    return obj;
                });

            PlugIn.Init();

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
                this.World.AddObject(new IObject(fmaterial, simpleModel, tmesh));
            }

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }        
        protected override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
            PlugIn.Update();                
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
            render.RenderTextComplete("Demo: Basic Screen Forward", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
        }

    }
}
