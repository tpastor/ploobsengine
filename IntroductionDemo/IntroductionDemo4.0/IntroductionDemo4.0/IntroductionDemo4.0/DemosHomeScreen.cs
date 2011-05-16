using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Entity;
using PloobsEngine.Input;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.MessageSystem;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Components;
using PloobsEngine.Commands;
using PloobsEngine.DataStructure;
using System;
using PloobsEngine.Engine;

namespace IntroductionDemo4._0
{

    public class DemosHomeScreen : IScreen
    {        
        public DemosHomeScreen() : base(null)
        {
        }

        int index = 0;

        static int totalDemos = 15;
        EngineStuff engine;

        private int[] screenList = new int[totalDemos];

        private IScreen GetScreen(int screenNumber)
        {
            switch (screenNumber)
            {
                case 0:
                    return new BasicScreenDeferredDemo();
                case 1:
                    return new BasicScreenForwardDemo();
                case 2:
                    return new KeyboardInputScreen();
                case 3:
                    return new LightInterpolationScreen();
                case 4:
                    return new PointLightScreen();
                case 5:
                    return new SpotLightScreen();
                case 6:
                    return new CollisionTypesBepuScreen();
                case 7:
                    return new GravitationalBepuScreen();
                case 8:
                    return new MovementScreen();
                case 9:
                    return new StressBepuScreen();
                case 10:
                    return new CameraPathScreen();
                case 11:
                    return new CameraScreens();                
                case 12:
                    return new TriggerBepuScreen();                    
                case 13:
                    return new PickingScreen();                    
                case 14:                    
                    return new ChangingMessagesScreen();
                default:
                    break;
            }
            return null;
        }

        IScreen active = null;

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
            this.engine = engine;
        }

        protected override void  LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
 	        base.LoadContent(GraphicInfo, factory, contentManager);
             
            for (int i = 0; i < totalDemos; i++)
            {
                screenList.SetValue(i, i);
            }

            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.F1, ChangeDemo);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
            {
                SimpleConcreteKeyboardInputPlayable ik = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Escape, LeaveGame);
                BindKeyCommand bk = new BindKeyCommand(ik, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bk);
            }
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {            
            render.Clear(Color.Black);

            render.RenderTextComplete("Welcome to the Ploobs Game Engine Funcionality Introduction Demos", new Vector2(40, 30), Color.White, Matrix.Identity);
            render.RenderTextComplete("The focus here is not in the visual, it is in the funcionalities (Check the Source Code =P)", new Vector2(40, 50), Color.Red, Matrix.Identity);            
            render.RenderTextComplete("(Press F1 to cycle through demos)", new Vector2(40, 75), Color.White, Matrix.Identity);
            render.RenderTextComplete("(Press Escape to exit)", new Vector2(40, 95), Color.White, Matrix.Identity);           

        }

        public void ChangeDemo(InputPlayableKeyBoard ipk)
        {
            if(this.ScreenState == PloobsEngine.SceneControl.ScreenState.Active)
                this.ScreenState = ScreenState.Hidden;
            if (active != null)
                ScreenManager.RemoveScreen(active);
            active = GetScreen(screenList[index % screenList.GetLength(0)]);
            ScreenManager.AddScreen(active);
            index++;
        }

        public void LeaveGame(InputPlayableKeyBoard ipk)
        {
            engine.Exit();
        }
    }
}