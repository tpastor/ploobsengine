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

namespace AdvancedDemo4._0
{

    public class DemosHomeScreen : IScreen
    {
        public DemosHomeScreen() : base(null) { }

        int index = 0;
        
        EngineStuff engine;

        private int[] screenList = new int[22];

        private IScreen GetScreen(int screenNumber)
        {
            switch (screenNumber)
            {
                case 0:
                    return new DeferredLoadScreen();
                case 1:
                    return new BumpSpecularDemo();
                case 2:
                    return new EnvMapScreen();
                case 3:
                    return new ParalaxScreen();
                case 4:
                    return new TransparentDeferredScreen();
                case 5:
                    return new SoundScreen();
                case 6:
                    return new FollowerObjectSoundScreen();
                case 7:
                    return new TerrainScreen();
                case 8:
                    return new ParticleScreen();
                case 9:
                    return new AnimatedBilboardScreen();
                case 10:
                    return new InstancedBilboardScreen();
                case 11:
                    return new NormalBilboardScreen();
                case 12:
                    return new ProceduralAnimatedBilboardScreen();
                case 13:
                    return new DeferredAnimatedScreen();
                case 14:
                    return new DGUIScreen();
                case 15:
                    return new FGUIScreen();
                case 16:
                    return new NoiseScreen();
                case 17:
                    return new PerlinNoiseScreen();
                case 18:
                    return new ProceduralTextureScreen();
                case 19:
                    return new OceanScreen();
                case 20:
                    return new WaterCompleteScreen();
                case 21:
                    return new DeferredDirectionaldShadowScreen();                
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

            InputAdvanced inp = new InputAdvanced();
            engine.AddComponent(inp);

        }

        protected override void  LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
 	        base.LoadContent(GraphicInfo, factory, contentManager);
             
            for (int i = 0; i < screenList.Length; i++)
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

            render.RenderTextComplete("Welcome to the Ploobs Game Engine Introduction Demos", new Vector2(40, 30), Color.White,Matrix.Identity);
            render.RenderTextComplete("(Press F1 to cycle through demos)", new Vector2(40, 55), Color.White, Matrix.Identity);
            render.RenderTextComplete("(Press Escape to exit)", new Vector2(40, 80), Color.White, Matrix.Identity);           

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