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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using EngineTestes;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Can run in PC/Windows and Phone 7
    /// Implemented in C#
    /// </summary>
    public class RVOScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9.8f), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            
            base.InitScreen(GraphicInfo, engine);
        }

        Microsoft.Xna.Framework.Vector2 destiny = new Microsoft.Xna.Framework.Vector2(200);
        RVO.Simulator Simulator = RVO.Simulator.Instance;

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);
                BoxObject tmesh = new BoxObject(new Vector3(0), 1, 1, 1, 10, new Vector3(1000, 1, 1000), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                tmesh.isMotionLess = true;
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            Simulator.setTimeStep(0.25f);
            Simulator.setAgentDefaults(5.0f, 25, 10.0f, 25.0f, 2.0f, 4.0f, new Vector2());

            {
                ///reuse parts of the object that are constant for all instances
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {                        
                        BoxObject tmesh = new BoxObject(new Vector3(100 + j * 5, 5, i * 5), 1, 1, 1, 10, new Vector3(1, 1, 1), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                        int id = Simulator.addAgent( new Vector2(tmesh.Position.X,tmesh.Position.Z));
                        RVOObject obj = new RVOObject(id, fmaterial, simpleModel, tmesh);
                        obj.OnUpdate += new OnUpdate(obj_OnUpdate); /// dummy position update way =p
                        this.World.AddObject(obj);
                    }
                }
            }

            ///counterclockwise vertices
            Simulator.addObstacle(
            new List<Vector2>()
            {
                new Vector2(20,20),                
                new Vector2(40,20),
                new Vector2(40,40),
                new Vector2(40,40),
                new Vector2(20,40),
                new Vector2(20,20),
                                
            }
            );

            Simulator.processObstacles();

            ///obstacle
            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Yellow), TextureType.DIFFUSE);
                GhostObject tmesh = new GhostObject(new Vector3(30, 0, 30), Matrix.Identity, new Vector3(10));
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            ///add a camera
            RotatingCamera cam = new RotatingCamera(this, new Vector3(-50,-30,-150));
            this.World.CameraManager.AddCamera(cam);

            Picking p = new Picking(this,Microsoft.Xna.Framework.Input.Touch.GestureType.DoubleTap, 10000);
            p.OnPickedGesture += new OnPicked(p_OnPickedGesture);
        }

        void p_OnPickedGesture(SegmentInterceptInfo SegmentInterceptInfo, Microsoft.Xna.Framework.Input.Touch.TouchLocation TouchLocation)
        {
            destiny = VectorUtils.ToVector2(SegmentInterceptInfo.ImpactPosition);
            for (int i = 0; i < Simulator.getNumAgents(); i++)
            {
                Vector2 rvopos = Simulator.getAgentPosition(i);
                Vector2 agentpos = new Vector2(rvopos.X, rvopos.Y);
                Vector2 vv = Vector2.Normalize(destiny - agentpos);
                Vector2 v = new Vector2(vv.X, vv.Y);
                Simulator.setAgentPrefVelocity(i, v);
            }
        }

        void obj_OnUpdate(IObject obj, GameTime gt, ICamera cam)
        {
            Vector2 p = Simulator.getAgentPosition((obj as RVOObject).RVOID);
            
            Vector3 p3 = new Vector3(p.X,obj.PhysicObject.Position.Y,p.Y);            
            obj.PhysicObject.Position= p3;
        }     

        protected override void Update(GameTime gameTime)
        {
            Simulator.doStep();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {   
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo: Crowed System, Use DoubleTap to controls the angents", new Vector2(15, 15), Color.White, Matrix.Identity);
        }

    }
}
