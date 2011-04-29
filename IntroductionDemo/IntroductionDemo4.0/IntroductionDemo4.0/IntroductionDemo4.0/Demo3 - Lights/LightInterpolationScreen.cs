using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Input;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Light Interpolation Screen
    /// Cena com cerca de 500 luzes pontuais (POINT)
    /// que ficam acendendo e apagando com velocidades aleatorias
    /// Mostra como adicionar luzes pontuais e como manipula-las
    /// </summary>
    public class LightInterpolationScreen : IScene
    {
        LightThrowBepu lt;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic[] renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());
            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = false;
            renderTech = new DeferredRenderTechnic[] { new DeferredRenderTechnic(desc) };
        }

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
            InputAdvanced inp = new InputAdvanced();
            engine.AddComponent(inp);
        }

        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            #region Models
            ///Cenario
            {
                SimpleModel sm = new SimpleModel(contentManager, "..\\Content\\Model\\cenario");                
                IPhysicObject pi = new TriangleMeshObject(sm,Vector3.Zero,Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
                NormalDeferred shader = new NormalDeferred(0.05f,50);                            
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }
            

            #endregion            

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo.Viewport);            
            cam.FarPlane = 2000;

            ///Atirador de bolas classico
            lt = new LightThrowBepu(this.World,factory,contentManager);            
            
            ///Interpolador que ira variar a cor das luzes
            UnitLightInterpolator li = new UnitLightInterpolator(this, true);
            ///Inicia o funcionamento do componente IScreenUpdateable (classe pai do UnitLightInterpolator)
            li.Start();            
            
            ///Adiciona DIVERSAS luzes pelo cenario
            for (int i = -10; i < 10; i++)
            {
                for (int j = -10; j < 10 ; j++)
                {
                    ///Luz Pontual
                    PointLightPE pl = new PointLightPE(new Vector3(i * 20, 10, j * 20), Color.White, 35, 2);
                    //Atenuacao quadratica (inverso do quadrado ao inves do padrao que eh inverso da linear)
                    ///custa mais, mas eh mais bonita
                    pl.UsePointLightQuadraticAttenuation = true;
                    ///adiciona ao mundo
                    this.World.AddLight(pl);
                    ///adiciona ao interpolador
                    li.AddLight(pl, StaticRandom.RandomColor(), StaticRandom.RandomColor(), StaticRandom.RandomBetween(0, 5));
                }
            }

            ///mais luzes, mais alto
            for (int i = -5; i < 5; i++)
            {
                for (int j = -5; j < 5; j++)
                {
                    PointLightPE pl = new PointLightPE(new Vector3(i * 50, 140, j * 50), Color.White, 70, 1);
                    pl.UsePointLightQuadraticAttenuation = true;
                    this.World.AddLight(pl);
                    li.AddLight(pl, StaticRandom.RandomColor(), StaticRandom.RandomColor(), StaticRandom.RandomBetween(0, 10));
                }
            }

            this.World.CameraManager.AddCamera(cam);            
        }

        protected override void  Draw(GameTime gameTime, RenderHelper render)
        {
 	        base.Draw(gameTime, render);
            render.RenderTextComplete("Demo: Light Interpolation", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White,Matrix.Identity);
            
        }

        protected override void CleanUp(PloobsEngine.Engine.EngineStuff engine)
        {
            base.CleanUp(engine);        
            lt.CleanUp();
        }
    }
}

