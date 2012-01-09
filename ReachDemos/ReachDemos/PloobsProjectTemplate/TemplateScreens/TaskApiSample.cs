using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Features;
using System.Threading;
using PloobsEngine.Cameras;
using PloobsEngine.Material;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using PloobsEngine.Physics;
using PloobsEngine.Commands;

namespace PloobsProjectTemplate.TemplateScreens
{
    public class taskSample : ITask
    {        
        #region ITask Members

        public override void Process()
        {
            Thread.Sleep(5000);
        }
     
        public override TaskEndType TaskEndType
        {
            get { return PloobsEngine.Features.TaskEndType.ON_NEXT_UPDATE; }
        }

        #endregion
    }

    public class TaskSampleScreen : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the IWorld
            world = new IWorld(new BepuPhysicWorld(-0.97f, true, 1), new SimpleCuller());

            ///Create the deferred technich
            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription();
            desc.BackGroundColor = Color.AliceBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///Uncoment to add your model
            SimpleModel simpleModel = new SimpleModel(factory, "Model/cenario");
            ///Physic info (position, rotation and scale are set here)
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            ///Forward Shader (look at this shader construction for more info)
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            ///Deferred material
            ForwardMaterial fmaterial = new ForwardMaterial(shader);
            ///The object itself
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            ///Add to the world
            this.World.AddObject(obj);

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            ///task sample
            taskSample taskSample = new taskSample();
            ///when ended, call this (syncronous -- specified in the itask implementation) function
            taskSample.Ended += new Action<ITask, IAsyncResult>(taskSample_Ended);
            ///create and send the task to the processor
            TaskCommand TaskCommand = new TaskCommand(taskSample);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(TaskCommand);
        }

        void taskSample_Ended(ITask arg1, IAsyncResult arg2)
        {
            ended = true;
        }


        bool ended = false;
        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            ///Draw some text on the screen
            render.RenderTextComplete("Demo: Task API usage sample", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("TaskEnded: " + ended, new Vector2(GraphicInfo.Viewport.Width - 315, 35), Color.Red, Matrix.Identity);
            
        }

    }
}