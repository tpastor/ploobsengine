using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Commands;
using PloobsEngine.DataStructure;
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
    /// Camera Screen 
    /// Mostra o funcionamento basico do CameraManager, com exemplos de criacao, adicao e interpolacao entre cameras     
    /// </summary>
    public class CameraScreens : IScene
    {        
        BindKeyCommand bk;
        
        /// <summary>
        /// Lista circular que armazenara os nomes das cameras
        /// </summary>
        private CircularList<String> camerasNames = new CircularList<string>(3);

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9.8f, true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }
                
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);                        
        

            #region Models

            ///Cria uma textura 1x1 com a cor branca
            Texture2D white = factory.CreateTexture2DColor(1,1, Color.White);
            {
                SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\cubo");
                sm.SetTexture(white, TextureType.DIFFUSE);
                
                BoxObject pi = new BoxObject(new Vector3(100, 20, 0), 1,1,1, 5,new Vector3(100, 5, 100),Matrix.Identity,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();                
                ///Setando alguns parametros do material
                shader.SpecularIntensity = 0.01f;
                shader.SpecularPower = 50;                
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }

            {
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cubo");
                sm.SetTexture(white, TextureType.DIFFUSE);
                
                BoxObject pi = new BoxObject(new Vector3(90, 30, 0), 1,1,1, 10,new Vector3(1),Matrix.Identity,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();                
                
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);                
            }


            {
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cenario");                
                IPhysicObject pi = new TriangleMeshObject(sm,Vector3.Zero,Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }
            

            #endregion                                    
           
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

            
            ///Criando uma camera estatica
            CameraStatic camx = new CameraStatic(new Vector3(130, 100, 700), Vector3.Zero);
            ///Dando um nome a ela (para poder recupera-la depois)
            camx.Name = "default";
            camx.FarPlane = 3000;
            ///Adiciona ao Manager (NAO ESTA SENDO ATIVADA, APENAS ADICIONADA)
            this.World.CameraManager.AddCamera(camx, camx.Name);
            ///Ativa a camera atual (AO ADICIONAR UMA CAMERA AO WORLD USANDO mundo.AddCamera(cam) A CAMERA EH AUTOMATICAMENTE ATIVADA  )
            this.World.CameraManager.SetActiveCamera(camx.Name);
            ///Adiciona na lista circular
            camerasNames.Value = camx.Name;
            camerasNames.Next();          

            ///Idem para uma segunda camera (Porem esta nao eh ativada)
            CameraStatic cam2 = new CameraStatic(new Vector3(100, 100, 100), Vector3.Zero);
            cam2.Name = "StaticCamera";
            cam2.FarPlane = 3000;
            this.World.CameraManager.AddCamera(cam2, cam2.Name);
            camerasNames.Value = cam2.Name;
            camerasNames.Next();

            ///Idem para a terceira
            CameraStatic cam3 = new CameraStatic(new Vector3(500, 300, 300), Vector3.Zero);
            cam3.Name = "StaticCamera3";
            cam3.FarPlane = 3000;
            this.World.CameraManager.AddCamera(cam3, cam3.Name);
            camerasNames.Value = cam3.Name;
            camerasNames.Next();


            SimpleConcreteKeyboardInputPlayable ikp = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Space, KeyStateChange);
            bk = new BindKeyCommand(ikp, BindAction.ADD);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);            
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Preset Cameras", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White,Matrix.Identity);
            render.RenderTextComplete("Space = Move to next camera", new Vector2(GraphicInfo.Viewport.Width - 315, 40), Color.White, Matrix.Identity);            
        }

        /// <summary>
        /// Chamada qd Espaco for pressionado
        /// </summary>
        /// <param name="ipk"></param>
        void KeyStateChange(InputPlayableKeyBoard ipk)
        {
            ///TO CHANGE ONLY IN THE END OF THE INTERPOLATION
            //if (mundo.CameraManager.ActiveCameraType != State.INTERPOLATING)
            //{ 
                //camerasNames.Next();
                //mundo.CameraManager.SetActiveCamera(camerasNames.Value,InterpolationType.BYSTEP, 0.005f);
                //mundo.CameraManager.SetActiveCamera(camerasNames.Value, InterpolationType.BYTIME, 3);
            //}

            ///Avanca o ponteiro da lista circular            
            camerasNames.Next();
            ///Ativa a camera correspondente
            ///Existem dois interpoladores, Por tempo (demora um tempo fixo independententemente da distancia)
            ///e um Por Etapa, que tem uma velocidade fixa de movimentacao (independente de quanto tempo for levar)
            this.World.CameraManager.SetActiveCamera(camerasNames.Value, InterpolationType.BYTIME, 3);            
        }

        protected override void CleanUp(EngineStuff engine )
        {
            bk.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
        }

    }
}

