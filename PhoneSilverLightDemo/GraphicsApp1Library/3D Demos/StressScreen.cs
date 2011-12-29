using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Physics;
using Microsoft.Xna.Framework;
using PloobsEngine.Features;
using PloobsEngine.Modelo;
using PloobsEngine.Utils;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using EngineTestes;
using PloobsEngine.Input;
using System.Collections.Generic;

public class StressScreen : IScene
{
    RotatingCamera cam;
    BallThrowBepu lt;
    float drawfps;
    float combinedfps;

    protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
    {
        BepuPhysicWorld BepuPhysicWorld = new BepuPhysicWorld(-9.7f);
        SimpleCuller SimpleCuller = new SimpleCuller();
        world = new IWorld(BepuPhysicWorld, SimpleCuller);

        ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
        desc.BackGroundColor = Color.CornflowerBlue;
        renderTech = new ForwardRenderTecnich(desc);
    }

    protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
    {
        base.InitScreen(GraphicInfo, engine);

        engine.AddComponent(new FPSCounter());

        ///IF the component already exist, it wont be added, the safest way is to get if from here
        FPSCounter fpsCounter = engine.GetComponent<FPSCounter>("FPSCounter");

       
        fpsCounter.DrawFps += new FpsEvent(fpsCounter_DrawFps);
    }

    void fpsCounter_DrawFps(float fps)
    {
        this.drawfps = fps;
    }
    

    public void CreateMOre()
    {
            createobjs( 2);        
    }

    private void createobjs( int minus = 0)
    {

        int numColumns = 7 - minus;
        int numRows = 7 - minus;
        int numHigh = 7 - minus;

        float separation = 3;
        for (int i = 0; i < numRows; i++)
            for (int j = 0; j < numColumns; j++)
                for (int k = 0; k < numHigh; k++)
                {
                    SimpleModel sm = new SimpleModel(GraphicFactory, "..\\Content\\Model\\cubo");
                    sm.SetTexture(GraphicFactory.CreateTexture2DColor(1, 1, StaticRandom.RandomColor()), TextureType.DIFFUSE);
                    MaterialDescription md = MaterialDescription.DefaultBepuMaterial();
                    md.Bounciness = 1;
                    BoxObject pi = new BoxObject(new Vector3(separation * i, k * separation, separation * j), 1, 1, 1, 1, new Vector3(1), Matrix.Identity, md);
                    pi.Entity.AngularDamping = 0f;
                    ForwardXNABasicShader shader = new ForwardXNABasicShader();
                    IMaterial mat = new ForwardMaterial(shader);
                    IObject obj5 = new IObject(mat, sm, pi);
                    this.World.AddObject(obj5);
                    shader.BasicEffect.EnableDefaultLighting();
                 
                }
    }
    


    protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
    {
        base.LoadContent(GraphicInfo, factory, contentManager);

        #region Models

        createobjs();

        ///Some Physic World Parameters
        ///No accuracy (speed up the simulation) and no gravity
        BepuPhysicWorld physicWorld = this.World.PhysicWorld as BepuPhysicWorld;        
        physicWorld.Space.Solver.IterationLimit = 1; //Essentially no sustained contacts, so don't need to worry about accuracy.            


        {
            SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\cubo");
            sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Blue), TextureType.DIFFUSE);
            MaterialDescription md = MaterialDescription.DefaultBepuMaterial();
            md.Bounciness = 1;
            BoxObject pi = new BoxObject(new Vector3(0,-30,0), 1, 1, 1, 1, new Vector3(50,1,50), Matrix.Identity, md);
            pi.isMotionLess = true;
            pi.Entity.AngularDamping = 0f;
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            IMaterial mat = new ForwardMaterial(shader);
            IObject obj5 = new IObject(mat, sm, pi);
            this.World.AddObject(obj5);
            shader.BasicEffect.EnableDefaultLighting();
        }

        #endregion

   

        cam = new RotatingCamera(this);        
        this.World.CameraManager.AddCamera(cam);
        
    }

    protected override void Draw(GameTime gameTime, RenderHelper render)
    {
        base.Draw(gameTime, render);
        render.RenderTextComplete("FPS Draw " + drawfps, new Vector2(40, 20), Color.White, Matrix.Identity);
        render.RenderTextComplete("Objects Rendered " + this.World.Objects.Count, new Vector2(40, 40), Color.White, Matrix.Identity);
        render.RenderTextComplete("Triangles " + this.World.Objects.Count * 12, new Vector2(40, 60), Color.White, Matrix.Identity);

        render.RenderTextComplete("Demo: Stress Test (BEPU)", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
       
    }


    protected override void CleanUp(EngineStuff engine)
    {
        ///if you wont want to remove the component itself, you should at least remove the events
        engine.RemoveComponent("FPSCounter");
    }
}
