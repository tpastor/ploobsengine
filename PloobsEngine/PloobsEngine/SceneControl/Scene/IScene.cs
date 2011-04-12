using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.Audio;
using PloobsEngine.Entity;
using PloobsEngine.Engine;
using PloobsEngine.Engine.Logger;
using System.Diagnostics;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Describe a 3D World
    /// </summary>
    /// 
    public abstract class IScene : IScreen
    {
        #region properties
        
        private IRenderTechnic[] _renderTecnics = null;        
        private bool _isFirstTimeTechnic = true;       


        /// <summary>
        /// Gets the render technics.
        /// </summary>
        public IRenderTechnic[] RenderTechnics
        {
            get
            {
                return _renderTecnics;
            }            
        }
        
        private IWorld _world = null;

        /// <summary>
        /// Gets or sets the world instance.
        /// </summary>
        /// <value>
        /// The world.
        /// </value>
        public IWorld World
        {            
            get
            {
                return this._world;
            }
        }

        /// <summary>
        /// Function called After all the stuffs LoadContent is called
        /// </summary>
        protected override void AfterLoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            foreach (IRenderTechnic item in RenderTechnics)
            {
                item.iAfterLoadContent(manager,ginfo,factory);
            }
        }
       

        #endregion

        /// <summary>
        /// Update the Screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
            World.iUpdateWorld(gameTime);
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime,RenderHelper render)
        {
            if (_isFirstTimeTechnic == true)
            {                
                foreach (IRenderTechnic item in RenderTechnics)
                {
                    item.iBeforeFirstExecution(render, this.World);
                }
                _isFirstTimeTechnic = false;
            }

            foreach (IRenderTechnic item in RenderTechnics)
            {
                item.iExecuteTechnic(gameTime, render, this.World);
            }                 
            
        }

        /// <summary>
        /// Load content for the screen.        
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory,contentManager);
        }

        /// <summary>
        /// Init Screen
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="engine"></param>
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            SetWorldAndRenderTechnich(out _renderTecnics, out _world);
            if (_renderTecnics == null ||  _renderTecnics.Count() == 0)
            {
                    ActiveLogger.LogMessage("IScene must have a renderTechnic", LogLevel.FatalError);
                    Debug.Fail("IScene must have a renderTechnic");
                    throw new Exception("IScene must have a renderTechnic");
            }
            if (_world == null)
            {
                    ActiveLogger.LogMessage("World cannot be null", LogLevel.FatalError);
                    Debug.Fail("World cannot be null");
                    throw new Exception("World cannot be null");
            }
            
            this._world.GraphicsFactory = GraphicFactory;
            this._world.GraphicsInfo = GraphicInfo;
            this._world.ContentManager = screenManager.contentManager;
            this._world.iInitWorld();
            base.InitScreen(GraphicInfo, engine);
        }

        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected abstract void SetWorldAndRenderTechnich(out IRenderTechnic[] renderTech, out IWorld world );
        
      
    }
}
