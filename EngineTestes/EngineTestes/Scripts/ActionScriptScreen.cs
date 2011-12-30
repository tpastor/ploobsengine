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
using EngineTestes.Scripts;
using PloobsEngine.Entity;
using PloobsEngine.MessageSystem;
using PloobsEngine.Input;
using PloobsEngine.Utils;

namespace EngineTestes
{
    public class ActionScriptScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = false;            
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);

            engine.AddComponent(new PloobsEngine.MessageSystem.MessageDeliver());           

        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///script to build the scene
            {
                ScriptParsed ScriptParsed = Parser.ParseScriptFile("Content//Script//script.txt");
                ScriptParsed.References.AddRange(new String[] { "EngineTestes.exe", "PloobsEngine.dll" });
            

                ScriptParsed.UsingStatements.AddRange(new String[] { "EngineTestes.Scripts" , "System" , "System.Collections.Generic" , "System.Text"
            , "PloobsEngine.Engine", "PloobsEngine.Modelo" , "PloobsEngine.Physics.Bepu", "PloobsEngine.Material", "PloobsEngine.SceneControl"
            , "Microsoft.Xna.Framework" , "PloobsEngine.Physics" , "PloobsEngine.Utils" , "PloobsEngine.Light"
            , "Microsoft.Xna.Framework.Graphics" , "PloobsEngine.Cameras" , "PloobsEngine.Features", "PloobsEngine.Commands"});
            

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
                ScriptParsed.References.AddRange(new String[] {"EngineTestes.exe", "PloobsEngine.dll"});

                ScriptParsed.UsingStatements.AddRange(new String[] { "EngineTestes.Scripts" , "System" , "System.Collections.Generic" , "System.Text"
            , "PloobsEngine.Engine", "PloobsEngine.Modelo" , "PloobsEngine.Physics.Bepu", "PloobsEngine.Material", "PloobsEngine.SceneControl"
            , "Microsoft.Xna.Framework" , "PloobsEngine.Physics" , "PloobsEngine.Utils" , "PloobsEngine.Light"
            , "Microsoft.Xna.Framework.Graphics" , "PloobsEngine.Cameras" , "PloobsEngine.Features", "PloobsEngine.Commands", "PloobsEngine.MessageSystem"
            , "EngineTestes.Scripts" });

                Generator GenerateClassCode = new PloobsScripts.Generator(ScriptParsed, "talking", true, true);
                GenerateClassCode.GenerateClass("talk", "InterScriptTalking");
                GenerateClassCode.GenerateMethod("execute", ScriptParsed.MethodCode, typeof(void), System.CodeDom.MemberAttributes.Public | System.CodeDom.MemberAttributes.Override);
                String srt = GenerateClassCode.GetCode(ScriptParsed);

                String erro = null;
                Assembly Assembly = Compilers.GenerateAssembly(srt, ScriptParsed.References, out erro);
                if (erro != null)
                {
                    throw new Exception(erro);
                }

                InterScriptTalking interteste = Executor.BindTypeFromAssembly<InterScriptTalking>(Assembly, GenerateClassCode.TypeName);

                interteste.graphicFactory = GraphicFactory;
                interteste.graphicInfo = GraphicInfo;
                interteste.world = this.World;
                interteste.renderTechnic = RenderTechnic;

                interteste.execute();
                EntityMapper.getInstance().AddEntity(interteste);                

                SystemRecieverMessage SystemRecieverMessage = new SystemRecieverMessage();
                SystemRecieverMessage.OnMessage += new Action<Message>(SystemRecieverMessage_OnMessage);
                EntityMapper.getInstance().AddgrouptagRecieveEntity("teste", SystemRecieverMessage);

                this.BindInput(new SimpleConcreteKeyboardInputPlayable(StateKey.DOWN,Keys.Space,                
                (a) => 
                    {                        
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
            render.RenderTextComplete("Messages recieved: " + recievedMessage, new Vector2(10, 10), Color.White, Matrix.Identity);
        }
    }
}
