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
using RVO3D;

namespace IntroductionDemo4._0
{
    public class RVO3DScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            desc.UsePostDrawPhase = true;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            engine.IsMouseVisible = true;
            base.InitScreen(GraphicInfo, engine);
        }

        Vector3 destiny = new Vector3(200);
        RVOSimulator Simulator = new RVOSimulator();

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);
                BoxObject tmesh = new BoxObject(new Vector3(0), 1, 1, 1, 10, new Vector3(1000, 1, 1000), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                tmesh.isMotionLess = true;
                ForwardTransparenteShader shader = new ForwardTransparenteShader();
                shader.TransparencyLevel = 0.2f;
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }
            
            
            Simulator.setTimeStep(0.10f);
            Simulator.setAgentDefaults(15.0f, 25, 5.0f, 5.0f, 2.0f,new Vector3());

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {

                    SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                    simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
                    GhostObject tmesh = new GhostObject(new Vector3(100 + j * 15, 5, i * 15), Matrix.Identity, new Vector3(1, 1, 1));
                    ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                    ForwardMaterial fmaterial = new ForwardMaterial(shader);
                    int id = Simulator.addAgent(tmesh.Position);
                    RVOObject obj = new RVOObject(id, fmaterial, simpleModel, tmesh);
                    obj.OnUpdate += new OnUpdate(obj_OnUpdate); /// dummy position update way =p
                    this.World.AddObject(obj);
                }
            }
            
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            Picking p = new Picking(this, 1000);
            p.OnPickedLeftButton += new OnPicked(p_OnPickedLeftButton);            
        }

        void obj_OnUpdate(IObject obj, GameTime gt, ICamera cam)
        {
            Vector3 p = Simulator.getAgentPosition((obj as RVOObject).RVOID);
            obj.PhysicObject.Position= p;
        }

        void p_OnPickedLeftButton(SegmentInterceptInfo SegmentInterceptInfo)
        {
            destiny = SegmentInterceptInfo.ImpactPosition;
            for (int i = 0; i < Simulator.getNumAgents(); i++)
            {
                Simulator.setAgentPrefVelocity(i, Vector3.Normalize( destiny - Simulator.getAgentPosition(i)));
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

            Texture2D logo = GraphicFactory.GetTexture2D("Textures\\engine_logo");
            int wd = 64;
            int hg = 48;
            render.RenderTextureComplete(logo, new Rectangle(this.GraphicInfo.BackBufferWidth - wd, this.GraphicInfo.BackBufferHeight - hg, wd, hg));

            render.RenderTextComplete("Demo: RVO2 3D Crowd Simulation", new Vector2(GraphicInfo.Viewport.Width - 515, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Only for PC Plataform. Native Implementation", new Vector2(GraphicInfo.Viewport.Width - 515, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Left Mouse bottom to set a destination", new Vector2(GraphicInfo.Viewport.Width - 515, 55), Color.White, Matrix.Identity);
        }

    }
}
