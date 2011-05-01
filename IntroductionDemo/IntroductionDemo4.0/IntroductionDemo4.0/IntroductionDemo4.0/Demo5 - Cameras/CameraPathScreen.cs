using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PloobsEngine;
using PloobsEngine.Cameras;
using PloobsEngine.Commands;
using PloobsEngine.Input;
using PloobsEngine.Light;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Physics;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Engine;

namespace IntroductionDemo4._0
{    
    /// <summary>
    /// Camera Path Screen
    /// Mostra como usar varias Cameras em uma cena
    /// Ao apertar R, o caminho percorrido pela camera atual eh gravado (na memoria)
    /// Ao apertar T, este caminho eh salvo em disco
    /// Ao apertar Y, o caminho salvo em disco eh carregado e mostrado    
    /// </summary>
    public class CameraPathScreen : IScene
    {        
        CameraRecordPath record;        

        BindKeyCommand bkk0 ;
        BindKeyCommand bkk1 ;
        BindKeyCommand bkk2 ;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9.8f, true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            ///Add the Input Component
            ///InputAdvanced is responsible for abstracting the xna input layer.            
            InputAdvanced inp = new InputAdvanced();
            engine.AddComponent(inp);
        }


        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);                        
        
            ///Cenario 
            SimpleModel sm = new SimpleModel(factory,"..\\Content\\Model\\cenario" );
                  
            IShader normal = new DeferredNormalShader();
            
            IMaterial mat = new DeferredMaterial(normal);
            IPhysicObject pi = new TriangleMeshObject(sm, new Vector3(0, 10, 100), Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());            
            pi.isMotionLess = true;
            IObject obj4 = new IObject(mat, sm,pi);
            this.World.AddObject(obj4);                        

            ///Luzes
            DirectionalLightPE ld = new DirectionalLightPE(new Vector3(4, -2, 7), Color.White);            
            this.World.AddLight(ld);                                             
           
            ///Camera basica
            ICamera cam = new CameraFirstPerson(GraphicInfo.Viewport);
            cam.Name = "stdCam";
            
            this.World.CameraManager.AddCamera(cam);                                                        
            

            ///Associando callbacks com eventos do teclado
            {
                InputPlayableKeyBoard ipp = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.R, Start, EntityType.TOOLS);
                bkk0 = new BindKeyCommand(ipp, BindAction.ADD);                
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkk0);

                InputPlayableKeyBoard ipp2 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.T, Stop, EntityType.TOOLS);
                bkk1 = new BindKeyCommand(ipp2, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkk1);

                InputPlayableKeyBoard ipp3 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Y, Load, EntityType.TOOLS);
                bkk2 = new BindKeyCommand(ipp3, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkk2);
            }

            ///Camera Recorder (objeto q gravara e carregara os caminhos)            
            record = new CameraRecordPath(this, cam);
            record.FinishInTheStartPosition = true; 
        }

        bool isLoading, isRecording, isFinished, isStopped;


        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            render.RenderTextComplete("Demo: Recording Camera Path", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("R = Record, T = Stop, Y = Load", new Vector2(GraphicInfo.Viewport.Width - 315, 40), Color.White, Matrix.Identity);

            if(isRecording)
                render.RenderTextComplete("Recording", new Vector2(20, 20), Color.White, Matrix.Identity);
            if(isLoading)
                render.RenderTextComplete("Loading - Following", new Vector2(20, 20), Color.White, Matrix.Identity);
            if(isFinished)
                render.RenderTextComplete("Finished", new Vector2(20, 20), Color.White, Matrix.Identity);
            if(isStopped)
                render.RenderTextComplete("Stopped - Saved", new Vector2(20, 20), Color.White, Matrix.Identity);
            
        }

        /// <summary>
        /// Ao PRESSIONAR R 
        /// </summary>
        /// <param name="ipk"></param>
        private void Start(InputPlayableKeyBoard ipk)
        {
            if (isRecording || this.World.CameraManager.ActiveCamera is CameraFollowPath)
                return;

            ///Comeca a gravar
            record.StartRecord();

            isFinished = false;
            isRecording = true;
        }


        /// <summary>
        /// y
        /// </summary>
        /// <param name="ipk"></param>
        private void Load(InputPlayableKeyBoard ipk)
        {
            if (this.World.CameraManager.ActiveCamera is CameraFollowPath)
            {
                if (!(this.World.CameraManager.ActiveCamera as CameraFollowPath).Ended)
                    return;
            }
            else if (isRecording == true)
            {
                return;
            }

            ///Carrega um caminho de um arquivo e seta uma camera para segui-lo
            CameraPathData pd = record.LoadCurveFile("teste.bin");
            CameraFollowPath fcp = new CameraFollowPath(pd, this.World, "stdCam");
            fcp.OnLoop = false;
            this.World.CameraManager.AddCamera(fcp,"follow");
            this.World.CameraManager.SetActiveCamera("follow");
            fcp.OnPathEnded += new OnPathEnded(fcp_OnPathEnded);

            isStopped = false;
            isLoading = true;
        }

        void fcp_OnPathEnded(CameraFollowPath cam)
        {
            this.World.CameraManager.RemoveCamera(cam.Name);

            isLoading = false;
            isFinished = true;
        }

        /// <summary>
        /// t
        /// </summary>
        /// <param name="ipk"></param>
        private void Stop(InputPlayableKeyBoard ipk)
        {
            if ((this.World.CameraManager.ActiveCamera is CameraFollowPath) || isRecording == false)
            {
                return;
            }

            record.StopRecord();
            record.SaveCurveToFile("teste.bin");

            this.World.CameraManager.SetActiveCamera("stdCam");

            isRecording = false;
            isStopped = true;
        }

        protected override void CleanUp(EngineStuff engine)
        {
            bkk0.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkk0);

            bkk1.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkk1);

            bkk2.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bkk2);
        }
    }
}
