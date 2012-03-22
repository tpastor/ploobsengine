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
using EngineTestes.AI.Behavior_Tree;
using PloobsEngine.Utils;
using RVO2D;

namespace EngineTestes
{
    public class RVO2DScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            engine.IsMouseVisible = true;
            base.InitScreen(GraphicInfo, engine);
        }

        Vector2 destiny = new Vector2(200);
        RVOSimulator Simulator = new RVOSimulator();

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
            Simulator.setAgentDefaults(5.0f, 25, 10.0f, 25.0f, 2.0f, 4.0f,new Vector2(0));

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {

                    SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                    simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
                    BoxObject tmesh = new BoxObject(new Vector3(100 + j*5, 5, i * 5), 1, 1, 1, 10, new Vector3(1, 1, 1), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                    ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                    ForwardMaterial fmaterial = new ForwardMaterial(shader);
                    int id = Simulator.addAgent(VectorUtils.ToVector2(tmesh.Position));
                    RVOObject obj = new RVOObject(id, fmaterial, simpleModel, tmesh);
                    obj.OnUpdate += new OnUpdate(obj_OnUpdate); /// dummy position update way =p
                    this.World.AddObject(obj);
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
                GhostObject tmesh = new GhostObject(new Vector3(30,0,30),Matrix.Identity,new Vector3(10));                
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            Picking p = new Picking(this, 1000);
            p.OnPickedLeftButton += new OnPicked(p_OnPickedLeftButton);            
        }

        void obj_OnUpdate(IObject obj, GameTime gt, ICamera cam)
        {
            Vector2 p = Simulator.getAgentPosition((obj as RVOObject).RVOID);
            Vector3 p3 = new Vector3(p.X,obj.PhysicObject.Position.Y,p.Y);            
            obj.PhysicObject.Position= p3;
        }

        void p_OnPickedLeftButton(SegmentInterceptInfo SegmentInterceptInfo)
        {
            destiny = VectorUtils.ToVector2(SegmentInterceptInfo.ImpactPosition);
            for (int i = 0; i < Simulator.getNumAgents(); i++)
            {
                Simulator.setAgentPrefVelocity(i, Vector2.Normalize( destiny - Simulator.getAgentPosition(i)));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            Simulator.doStep();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {   
            base.Draw(gameTime, render);         
        }

    }
}
