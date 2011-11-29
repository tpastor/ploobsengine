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
using EngineTestes;
using Microsoft.Xna.Framework.Input.Touch;

namespace PloobsEnginePhone7Template
{

    public class DemosHomeScreen : IScreen
    {        
        public DemosHomeScreen() : base()
        {
        }

        int index = 0;

        static int totalDemos = 8;
        EngineStuff engine;

        private int[] screenList = new int[totalDemos];

        private IScreen GetScreen(int screenNumber)
        {   
            switch (screenNumber)
            {
                case 0:
                    return new FirstScreen();
                case 1:
                    return new Basic2D();
                case 2:
                    return new CamScreen();
                case 3:
                    return new InputGestureScreen();
                case 4:
                    return new Picking2D();
                case 5:
                    return new Picking3D();
                case 6:
                    return new AnimationScreen();    
                case 7:
                    return new RotateCameraScreen();
                
            }
            return null;
        }

        IScreen active = null;

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
            this.engine = engine;
        }

        float delta = 0;
        protected override void  LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            for (int i = 0; i < totalDemos; i++)
            {
                screenList.SetValue(i, i);
            }

            {
                SimpleConcreteGestureInputPlayable SimpleConcreteGestureInputPlayable = new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.HorizontalDrag,
                    (sample) =>
                    {
                        delta += sample.Delta.X;
                    }
                );
                BindGestureCommand BindGestureCommand = new BindGestureCommand(SimpleConcreteGestureInputPlayable, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(BindGestureCommand);
            }
            {
                SimpleConcreteGestureInputPlayable SimpleConcreteGestureInputPlayable = new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.DragComplete,
                    (sample) =>
                    {
                        ///big horizontal drag only =P
                        if (delta > 400)
                        {
                            ChangeDemo();                         
                        }
                        delta = 0;
                    }
                );
                BindGestureCommand BindGestureCommand = new BindGestureCommand(SimpleConcreteGestureInputPlayable, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(BindGestureCommand);
            }
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {            
            render.Clear(Color.Black);

            render.RenderTextComplete("Welcome to the Ploobs Game Engine Funcionality Introduction Demos for Phone 7", new Vector2(40, 30), Color.White, Matrix.Identity);
            render.RenderTextComplete("The focus here is not in the visual, it is in the funcionalities (Check the Source Code =P)", new Vector2(40, 50), Color.Red, Matrix.Identity);
            render.RenderTextComplete("(Make a BIG HorizontalDrag gesture to cycle through demos)", new Vector2(40, 75), Color.White, Matrix.Identity);            

        }

        public void ChangeDemo()
        {
            if(this.ScreenState == PloobsEngine.SceneControl.ScreenState.Active)
                this.ScreenState = ScreenState.Hidden;
            if (active != null)
                ScreenManager.RemoveScreen(active);
            active = GetScreen(screenList[index % screenList.GetLength(0)]);
            ScreenManager.AddScreen(active);
            index++;
        }
        
    }
}