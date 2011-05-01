using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Commands;
using PloobsEngine.Features;
using PloobsEngine.Input;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using PloobsEngine.Engine;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Picking Screen    
    /// </summary>
    public class PickingScreen : IScene
    {        
        ICamera cam;




        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9.8f, true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            ///Add the Input Component
            ///InputAdvanced is responsible for abstracting the xna input layer.            
            InputAdvanced inp = new InputAdvanced();
            engine.AddComponent(inp);

            engine.IsMouseVisible = true;
        }


        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            Picking picking = new Picking(this);
            this.AddScreenUpdateable(picking);

            #region Models            
            {
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cenario");
                IPhysicObject pi = new TriangleMeshObject(sm, Vector3.Zero,Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();                                
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                obj3.Name = "cenario";
                this.World.AddObject(obj3);
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cubo");
                    sm.SetTexture(factory.CreateTexture2DColor(1,1, Color.White), TextureType.DIFFUSE);                    
                    BoxObject pi = new BoxObject(new Vector3(i * 10, 100, j * 10),1,1,1,1,new Vector3(5),Matrix.Identity,MaterialDescription.DefaultBepuMaterial());
                    pi.isMotionLess = true;
                    DeferredNormalShader shader = new DeferredNormalShader();
                    IMaterial mat = new DeferredMaterial(shader);
                    IObject obj4 = new IObject(mat, sm, pi);
                    obj4.Name = "Block " + i + " : " + j;
                    this.World.AddObject(obj4);
                }
            }            

            #endregion            

            cam = new CameraFirstPerson(GraphicInfo.Viewport);            
            cam.FarPlane = 3000;

            #region NormalLight
            ///Conjunto de luzes direcionais
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.4f;
            ld1.LightIntensity = li;
            ld2.LightIntensity = li;
            ld3.LightIntensity = li;
            ld4.LightIntensity = li;
            ld5.LightIntensity = li;
            this.World.AddLight(ld1);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);
            this.World.AddLight(ld4);
            this.World.AddLight(ld5);
            #endregion

            
            this.World.CameraManager.AddCamera(cam);
            

            ///O PICKING FUNCIONA APENAS COM OBJETOS QUE TENHAM CORPO FISICO REAL !!!
            ///OS GHOST E OS DUMMY NUNCA SERAO SELECIONADOS
            ///Para ser informado a todo frame sobre as colisoes do raio, basta utilizar o outro construtor
            picking.OnPickedLeftButton += new OnPicked(prc_redirectPicking);            
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo: Picking", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White,Matrix.Identity);
            render.RenderTextComplete("Left click to pick an object", new Vector2(GraphicInfo.Viewport.Width - 315, 40), Color.White, Matrix.Identity);

            if (shouldDraw)
            {
                render.RenderTextComplete("Selecionado -> " + objName, new Vector2(20, 20), Color.White, Matrix.Identity);
                render.RenderTextComplete("Position " + ri.ImpactPosition, new Vector2(20, 40), Color.White, Matrix.Identity);
                render.RenderTextComplete("Normal " + ri.ImpactNormal, new Vector2(20, 60), Color.White, Matrix.Identity);
            }            
        }

        SegmentInterceptInfo ri = null;
        string objName;
        bool shouldDraw;

        void prc_redirectPicking(SegmentInterceptInfo SegmentInterceptInfo)
        {
            
            IObject obj = SegmentInterceptInfo.PhysicObject.ObjectOwner;
            if(obj != null)
            {                
                objName = obj.Name;
                shouldDraw = true;
                this.ri = SegmentInterceptInfo;
            }
        }

        protected override void CleanUp(EngineStuff engine)
        {            
        }
    }
}

