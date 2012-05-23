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

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());
            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc) ;
        }

        
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            #region Models
            ///Cenario
            {
                SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\cenario");                
                IPhysicObject pi = new TriangleMeshObject(sm,Vector3.Zero,Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader(0.05f, 50);                            
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }
            

            #endregion            

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo);            
            cam.FarPlane = 2000;

            ///Atirador de bolas classico
            lt = new LightThrowBepu(this.World,factory);            
            
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
            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());
        }

        protected override void  Draw(GameTime gameTime, RenderHelper render)
        {
 	        base.Draw(gameTime, render);

            Texture2D logo = GraphicFactory.GetTexture2D("Textures\\engine_logo");
            int wd = 64;
            int hg = 48;
            render.RenderTextureComplete(logo, new Rectangle(this.GraphicInfo.BackBufferWidth - wd, this.GraphicInfo.BackBufferHeight - hg, wd, hg));

            render.RenderTextComplete("Light Interpolation", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White,Matrix.Identity);
            
        }

        protected override void CleanUp(PloobsEngine.Engine.EngineStuff engine)
        {
            base.CleanUp(engine);        
            lt.CleanUp();
        }
    }
}

