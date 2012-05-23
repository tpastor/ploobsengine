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
            world = new IWorld(new BepuPhysicWorld(-9.7f, true), new SimpleCuller(), null, true);
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

            cam = new CameraFirstPerson(GraphicInfo);
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
            

            ///Bind a Key event (combination of Key + state(pressed, Released ...) + inputMask ) to a function
            SimpleConcreteKeyboardInputPlayable ik1 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.T,g1, InputMask.G1);
            ///When you use the method Bind of a IScreen, The key event will be sent by the engine while this screen remains added in the ScreenManager.
            ///TO create a Gloal Input (Keep working even if the screen goes away), see the DemosHomeScreen.cs
            this.BindInput(ik1);            
           
            SimpleConcreteKeyboardInputPlayable ik2 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Y, g2,InputMask.G2);
            this.BindInput(ik2);
            
            ///The SYSTEM Mask is Always On (cant be turned off)
            SimpleConcreteKeyboardInputPlayable ik3 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Space, ChangeGroup,InputMask.GSYSTEM);
            this.BindInput(ik3);            
            
            ///StateKey.DOWN mean when the key is down the event will be fired --looooots of times(samae as UP)
            ///StateKey.PRESS is fired ONCE when the key is pressed (same as RELEASE)
            ///WHEN USING COMBOS, use DOWN AND UP (better for precision)
            ///The parameter EntityType is not used internaly 
            SimpleConcreteKeyboardInputPlayable ik4 = new SimpleConcreteKeyboardInputPlayable(StateKey.DOWN, new Keys[] { Keys.LeftControl, Keys.U }, Multiple);
            this.BindInput(ik4);

            ///Send a command (design pattern) to the InputSystem to change the InputMask            
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
            render.RenderText("Keyboard Input Masks", new Vector2(GraphicInfo.Viewport.Width - 515, 15),Vector2.One ,Color.White);
            render.RenderText("Press Space to change the Active Input Mask (G1 or G2)", new Vector2(GraphicInfo.Viewport.Width - 515, 40), Vector2.One, Color.White);
            render.RenderText("Press T when G1 or ALL InputMask Input is active", new Vector2(GraphicInfo.Viewport.Width - 515, 60), Vector2.One, Color.White);
            render.RenderText("Press Y when G2 or ALL InputMask Input is active", new Vector2(GraphicInfo.Viewport.Width - 515, 80), Vector2.One, Color.White);
            render.RenderText("Press Ctrl + U TO use a Combo (Registered in all Masks)", new Vector2(GraphicInfo.Viewport.Width - 515, 100), Vector2.One, Color.White);
            if(isAllActive)
                render.RenderText("Group Mask ALL Active", new Vector2(20, 40),Vector2.One, Color.White);
            if (isGroup1)
                render.RenderText("T is Pressed", new Vector2(20, 20), Vector2.One, Color.White);
            if(isGroup2)
                render.RenderText("Y is Pressed", new Vector2(100, 20),Vector2.One, Color.White);
            if(isChangeGroup)
                render.RenderText("Active InputMask " + Groups[index], new Vector2(20, 40),Vector2.One, Color.White);
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

            ///Turn Off all Masks (we cant turn the GSYSTEM MASK, even if we try ....)
            TurnOffInputMaskCommand tof = new TurnOffInputMaskCommand(InputMask.GALL);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(tof);

            ///Turn only the right mask
            ///Masks are Bit Field, You can turn more than one using InputMask.GALL | InputMask.G1 for example
            ///The TurnOnInputMaskCommand Just combine its actual mask with the mask provided, It does not TURN OFFthe active masks (this is the reason why i sent a turn off mask before)
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

