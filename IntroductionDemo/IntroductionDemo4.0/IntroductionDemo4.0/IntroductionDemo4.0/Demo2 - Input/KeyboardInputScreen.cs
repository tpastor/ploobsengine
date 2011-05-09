using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Commands;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Input;

namespace IntroductionDemo4._0
{    
    /// <summary>
    /// InputScreen    
    /// </summary>
    public class KeyboardInputScreen: IScene
    {        
        ICamera cam;        
        LightThrowBepu lt;        

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.097f,true), new SimpleCuller());
            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = false;                                    
            renderTech = new DeferredRenderTechnic(desc) ;
        }
        protected override void  LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);
            
            #region Models

            {
                SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cenario");                
                IPhysicObject pi = new TriangleMeshObject(sm,Vector3.Zero, Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();
                shader.SpecularIntensity = 0;
                shader.SpecularPower = 0;                
                IMaterial mat = new DeferredMaterial(shader);
                IObject obj3 = new IObject(mat, sm, pi);
                this.World.AddObject(obj3);
            }


            #endregion

            cam = new CameraFirstPerson(GraphicInfo.Viewport);
            cam.FarPlane = 3000;

            lt = new LightThrowBepu(this.World,factory);

            #region NormalLight
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
            
            SimpleConcreteKeyboardInputPlayable ik1 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.T, g1, InputMask.G1);
            ///Ao usar o metodo Bind da Screen, o evento sera de tecla sera enviado enquanto a screen estiver Ativa (adicionada ao Screen manager)
            ///Para criar um metodo global (mesmo q a screen tenha sido removida, ele continua chamando os eventos), veja DemosHomeScreen.cs
            this.BindInput(ik1);            
           
            SimpleConcreteKeyboardInputPlayable ik2 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Y, g2,InputMask.G2);
            this.BindInput(ik2);
            
            ///MASCARA SYSTEM ESTA SEMPRE LIGADA, nao importa quantos TurnOffs forem usados
            SimpleConcreteKeyboardInputPlayable ik3 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Space, ChangeGroup,InputMask.GSYSTEM);
            this.BindInput(ik3);
            
            ///AO nao especificar a mascara
            ///A mascara GSYSTEM sera aplicada (sempre ativada, NAO HA COMO DESATIVA-LA USANDO O TURNOFF)
            
            ///StateKey.DOWN eh a todo frame enquanto a tecla estiver apertada (vale o mesmo para o UP)
            ///StateKey.PRESS eh disparado uma unica vez ao pressionar uma tecla (vale o mesmo para o RELEASE)
            ///Com combo, melhor utilizar o DOWN para capturar o evento
            ///O CAMPO EntityType eh utilizado apenas para fins estatisticos            
            SimpleConcreteKeyboardInputPlayable ik4 = new SimpleConcreteKeyboardInputPlayable(StateKey.DOWN, new Keys[] { Keys.LeftControl, Keys.U }, Multiple);
            this.BindInput(ik4);

            TurnOnInputMaskCommand tom = new TurnOnInputMaskCommand(InputMask.GALL);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(tom);

            isAllActive = true;

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());
        }

        bool isAllActive;
        bool isGroup1;
        bool isGroup2;
        bool isChangeGroup;
        bool isComboPressed;

        protected override void  Draw(GameTime gameTime, RenderHelper render)
        {
 	        base.Draw(gameTime, render);

            render.RenderBegin(Matrix.Identity);
            render.RenderText("Demo: Keyboard Input", new Vector2(GraphicInfo.Viewport.Width - 315, 15),Vector2.One ,Color.White);
            render.RenderText("T, Y, Space, Ctrl+U = Activate masks", new Vector2(GraphicInfo.Viewport.Width - 315, 40), Vector2.One, Color.White);
            if(isAllActive)
                render.RenderText("Group ALL Active", new Vector2(20, 40),Vector2.One, Color.White);
            if (isGroup1)
                render.RenderText("Group1", new Vector2(20, 20), Vector2.One, Color.White);
            if(isGroup2)
                render.RenderText("Group2", new Vector2(100, 20),Vector2.One, Color.White);
            if(isChangeGroup)
                render.RenderText("ChangeGroup " + Groups[index], new Vector2(20, 40),Vector2.One, Color.White);
            if(isComboPressed)
                render.RenderText("Combo Pressed", new Vector2(20, 60),Vector2.One, Color.White);
            render.RenderEnd();
            
        }

        public void g1(InputPlayableKeyBoard ipk)
        {
            isGroup2 = false;
            isComboPressed = false;
            isGroup1 = true;
        }
        public void g2(InputPlayableKeyBoard ipk)
        {
            isGroup1 = false;
            isComboPressed = false;
            isGroup2 = true;
        }

        InputMask[] Im = new InputMask[] { InputMask.G1, InputMask.G2, InputMask.GALL };
        string[] Groups = new string[] { "G1", "G2", "GALL" };
        int index = 2;
        public void ChangeGroup(InputPlayableKeyBoard ipk)
        {
            index = (index + 1) % 3;

            ///Inicialmente desligar TODAS as mascaras
            TurnOffInputMaskCommand tof = new TurnOffInputMaskCommand(InputMask.GALL);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(tof);

            ///Ligar apenas a mascara correta
            ///As mascaras sao campos Bit Field, sendo possivel realizar operacoes como InputMask.GALL | InputMask.G1
            ///O TurnOnInputMaskCommand apenas combina a mascara atual com a mandada no parametro, ELE NAO DESATIVA AS QUE JA ESTIVEREM ATIVAS (porisso q foi mandado um turnoff antes)
            TurnOnInputMaskCommand tom = new TurnOnInputMaskCommand(Im[index] | InputMask.GNONE);            
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(tom);

            isAllActive = false;
            isGroup1 = false;
            isGroup2 = false;
            isComboPressed = false;            
            isChangeGroup = true;
        }

        public void Multiple(InputPlayableKeyBoard ipk)
        {
            isGroup1 = false;
            isGroup2 = false;
            isComboPressed = true;
        }

        protected override void CleanUp(PloobsEngine.Engine.EngineStuff engine)
        {
            base.CleanUp(engine);
            lt.CleanUp();            
        }        
    }
}

