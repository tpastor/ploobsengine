using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Light;
using PloobsEngine.Utils;
using System;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Commands;
using PloobsEngine.DataStructure;
using PloobsEngine.Engine;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Screen Animacao
    /// </summary>
    public class MovementScreen : IScene
    {        
        LightThrowBepu lt;
        IObject player;
        CameraFollowObject cam0;
        CameraFirstPerson cam1;        

        private CircularList<String> camerasNames = new CircularList<string>(2);

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
            ///Cria um modelo Controlavel pelo teclado
            {
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cubo");
                sm.SetTexture(factory.CreateTexture2DColor(1,1,Color.Blue),TextureType.DIFFUSE);

                DeferredNormalShader shader = new DeferredNormalShader();                
                
                IMaterial mat = new DeferredMaterial(shader);
                CharacterControllerInput character = new CharacterControllerInput(this, new Vector3(100, 150, 1), 1, 1, 50, Vector3.One * 10, 0.5f);
                
                character.AheadKey = Keys.G;
                character.BackKey = Keys.T;
                character.LeftKey = Keys.F;
                character.RightKey = Keys.H;
                character.JumpKey = Keys.R;                
                character.Characterobj.CharacterController.MaxSpeed = 35f;
                character.Characterobj.CharacterController.JumpSpeed = 15f;
                player = new IObject(mat, sm, character.Characterobj);
                this.World.AddObject(player);
            }            

            ///Cria o cenario padrao de sempre ;)
            {
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cenario");
                
                IPhysicObject pi = new TriangleMeshObject(sm,Vector3.Zero, Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();                                
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }
            
            #endregion

            #region Cameras

            cam0 = new CameraFollowObject(player); 
            ///Dando um nome a ela (para poder recupera-la depois)
            cam0.Name = "follow";
            cam0.FarPlane = 3000;
            ///Adiciona ao Manager (NAO ESTA SENDO ATIVADA, APENAS ADICIONADA)
            this.World.CameraManager.AddCamera(cam0, cam0.Name);
            ///Ativa a camera atual (AO ADICIONAR UMA CAMERA AO WORLD USANDO mundo.AddCamera(cam) A CAMERA EH AUTOMATICAMENTE ATIVADA  )
            this.World.CameraManager.SetActiveCamera(cam0.Name);
            ///Adiciona na lista circular
            camerasNames.Value = cam0.Name;
            camerasNames.Next();

            cam1 = new CameraFirstPerson(GraphicInfo.Viewport);
            ///Dando um nome a ela (para poder recupera-la depois)
            cam1.Name = "first person";
            cam1.FarPlane = 3000;
            ///Adiciona ao Manager (NAO ESTA SENDO ATIVADA, APENAS ADICIONADA)
            this.World.CameraManager.AddCamera(cam1, cam1.Name);
            ///Ativa a camera atual (AO ADICIONAR UMA CAMERA AO WORLD USANDO mundo.AddCamera(cam) A CAMERA EH AUTOMATICAMENTE ATIVADA  )
            this.World.CameraManager.SetActiveCamera(cam1.Name);
            ///Adiciona na lista circular
            camerasNames.Value = cam1.Name;
            //camerasNames.Next();

            #endregion

            ///Objeto que permite atirar bolas de luzes na cena
            lt = new LightThrowBepu(this.World, factory);

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
            
            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Space, ChangeCamera);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo: Character Movement", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White,Matrix.Identity);
            render.RenderTextComplete("Space = switch to character camera", new Vector2(GraphicInfo.Viewport.Width  - 315, 40), Color.White,Matrix.Identity);
            
            if(shouldDraw)
            {
            render.RenderTextComplete("T = forward, G = back, F = left, ", new Vector2(20, 15), Color.White,Matrix.Identity);
            render.RenderTextComplete("H = right, R = jump", new Vector2(20, 40), Color.White,Matrix.Identity);
            }
            
            
        }

        bool shouldDraw;
        public void ChangeCamera(InputPlayableKeyBoard ipk)
        {
            if (shouldDraw) shouldDraw = false;
            else shouldDraw = true;
            camerasNames.Next();
            this.World.CameraManager.SetActiveCamera(camerasNames.Value, InterpolationType.BYTIME, 3);    
        }

        protected override void CleanUp(EngineStuff engine)
        {
            lt.CleanUp();
        }
    }
}

