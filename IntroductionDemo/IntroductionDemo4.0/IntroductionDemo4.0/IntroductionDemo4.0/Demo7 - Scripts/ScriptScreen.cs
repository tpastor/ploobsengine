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

namespace IntroductionDemo4._0
{
    public class ScriptScreen : IScene
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
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo,factory, contentManager);

            ScriptParsed ScriptParsed = Parser.ParseScriptFile("Content//Script//script.txt");                        
            ScriptParsed.References.AddRange( new String[] {"EngineTestes.exe", "PloobsEngine.dll"                
            });            

            ScriptParsed.UsingStatements.AddRange(new String[] { "EngineTestes.Scripts" , "System" , "System.Collections.Generic" , "System.Text"
            , "PloobsEngine.Engine", "PloobsEngine.Modelo" , "PloobsEngine.Physics.Bepu", "PloobsEngine.Material", "PloobsEngine.SceneControl"
            , "Microsoft.Xna.Framework" , "PloobsEngine.Physics" , "PloobsEngine.Utils" , "PloobsEngine.Light"
            , "Microsoft.Xna.Framework.Graphics" , "PloobsEngine.Cameras" , "PloobsEngine.Features", "PloobsEngine.Commands"
            });

            Generator GenerateClassCode = new PloobsScripts.Generator(ScriptParsed, "TesteInter",true,true);
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

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo Scripts 1 -> Scene generated on script file", new Vector2(GraphicInfo.Viewport.Width - 515, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Check the Code", new Vector2(GraphicInfo.Viewport.Width - 515, 25), Color.White, Matrix.Identity);            
        }

    }
}
