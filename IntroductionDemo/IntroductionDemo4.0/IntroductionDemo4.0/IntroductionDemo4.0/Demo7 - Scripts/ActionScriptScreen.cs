using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Features;
using PloobsEngine.Commands;
using PloobsScripts;
using System.IO;
using System.Reflection;
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;
using PloobsEngine.Input;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework.Graphics;

namespace IntroductionDemo4._0
{
    public class ActionScriptScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = false;            
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            engine.AddComponent(new PloobsEngine.MessageSystem.MessageDeliver());           
            base.InitScreen(GraphicInfo, engine);            
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent(MessageDeliver.MyName);
            base.CleanUp(engine);
        }

        SystemRecieverMessage SystemRecieverMessage;
        InterScriptTalking interteste;
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///script to build the scene
            {
                ScriptParsed ScriptParsed = Parser.ParseScriptFile("Content//Script//script.txt");
                ScriptParsed.References.AddRange(new String[] {"IntroductionDemo4.0.exe", "PloobsEngineDebug.dll"                
            });

                ScriptParsed.UsingStatements.AddRange(new String[] { "IntroductionDemo4._0" , "System" , "System.Collections.Generic" , "System.Text"
            , "PloobsEngine.Engine", "PloobsEngine.Modelo" , "PloobsEngine.Physics.Bepu", "PloobsEngine.Material", "PloobsEngine.SceneControl"
            , "Microsoft.Xna.Framework" , "PloobsEngine.Physics" , "PloobsEngine.Utils" , "PloobsEngine.Light"
            , "Microsoft.Xna.Framework.Graphics" , "PloobsEngine.Cameras" , "PloobsEngine.Features", "PloobsEngine.Commands"
            });
            

                Generator GenerateClassCode = new PloobsScripts.Generator(ScriptParsed, "TesteInter", true, true);
                GenerateClassCode.GenerateClass("teste", "ISceneBuilder");
                GenerateClassCode.GenerateMethod("BuildScene", ScriptParsed.MethodCode, typeof(void), System.CodeDom.MemberAttributes.Public | System.CodeDom.MemberAttributes.Override);
                String srt = GenerateClassCode.GetCode(ScriptParsed);

                String erro = null;
                Assembly Assembly = Compilers.GenerateAssembly(srt, ScriptParsed.References, out erro);
                if (erro != null)
                {
                    throw new Exception(erro);
                }

                ISceneBuilder interteste = Executor.BindTypeFromAssembly<ISceneBuilder>(Assembly, GenerateClassCode.TypeName);

                interteste.graphicFactory = GraphicFactory;
                interteste.graphicInfo = GraphicInfo;
                interteste.world = this.World;
                interteste.renderTechnic = RenderTechnic;

                interteste.BuildScene();
            }

            ///script to handle messages
            {
                ScriptParsed ScriptParsed = Parser.ParseScriptFile("Content//Script//talk1.txt");
                ScriptParsed.References.AddRange(new String[] {"IntroductionDemo4.0.exe", "PloobsEngineDebug.dll"});

                ScriptParsed.UsingStatements.AddRange(new String[] { "IntroductionDemo4._0" , "System" , "System.Collections.Generic" , "System.Text"
            , "PloobsEngine.Engine", "PloobsEngine.Modelo" , "PloobsEngine.Physics.Bepu", "PloobsEngine.Material", "PloobsEngine.SceneControl"
            , "Microsoft.Xna.Framework" , "PloobsEngine.Physics" , "PloobsEngine.Utils" , "PloobsEngine.Light"
            , "Microsoft.Xna.Framework.Graphics" , "PloobsEngine.Cameras" ,"PloobsEngine.MessageSystem", "PloobsEngine.Features", "PloobsEngine.Commands"
            });

                Generator GenerateClassCode = new PloobsScripts.Generator(ScriptParsed, "IntroductionDemo4._0", true, true);
                GenerateClassCode.GenerateClass("talk", "InterScriptTalking");
                GenerateClassCode.GenerateMethod("execute", ScriptParsed.MethodCode, typeof(void), System.CodeDom.MemberAttributes.Public | System.CodeDom.MemberAttributes.Override);
                String srt = GenerateClassCode.GetCode(ScriptParsed);

                String erro = null;
                Assembly Assembly = Compilers.GenerateAssembly(srt, ScriptParsed.References, out erro);
                if (erro != null)
                {
                    throw new Exception(erro);
                }

                interteste = Executor.BindTypeFromAssembly<InterScriptTalking>(Assembly, GenerateClassCode.TypeName);

                interteste.graphicFactory = GraphicFactory;
                interteste.graphicInfo = GraphicInfo;
                interteste.world = this.World;
                interteste.renderTechnic = RenderTechnic;

                interteste.execute();
                EntityMapper.getInstance().AddEntity(interteste);                

                SystemRecieverMessage = new SystemRecieverMessage();
                SystemRecieverMessage.OnMessage += new Action<Message>(SystemRecieverMessage_OnMessage);
                EntityMapper.getInstance().AddgrouptagRecieveEntity("teste", SystemRecieverMessage);

                this.BindInput(new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS,Keys.Space,                
                (a) => 
                    {                        
                        ///5000 is the id of the script
                        ///defined inside it =P look talk1.txt
                        MessageDeliver.SendMessage(new Message(-1,5000,null,Priority.LOW,0,SenderType.NORMAL,StaticRandom.PickRandomPoint(new Vector3(100,100,100),new Vector3(200,100,200),new Vector3(-100,100,-100)),null));
                    }
                ));
            }
        }

        int recievedMessage = 0;
        void SystemRecieverMessage_OnMessage(Message obj)
        {
            recievedMessage++;   
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            Texture2D logo = GraphicFactory.GetTexture2D("Textures\\engine_logo");
            int wd = 64;
            int hg = 48;
            render.RenderTextureComplete(logo, new Rectangle(this.GraphicInfo.BackBufferWidth - wd, this.GraphicInfo.BackBufferHeight - hg, wd, hg));

            render.RenderTextComplete("Demo Scripts 2 -> Scrips", new Vector2(GraphicInfo.Viewport.Width - 715, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Scripts Advanced Usage: Press Space to send a message to be handled by a script", new Vector2(GraphicInfo.Viewport.Width - 715, 35), Color.White, Matrix.Identity);
            render.RenderTextComplete("Messages recieved: " + recievedMessage, new Vector2(GraphicInfo.Viewport.Width - 715, 55), Color.White, Matrix.Identity);
        }
    }
}
