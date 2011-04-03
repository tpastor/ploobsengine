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
        private RenderHelper _render = null;
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
        protected override void AfterLoadContent()
        {
            foreach (IRenderTechnic item in RenderTechnics)
            {
                item.iAfterLoadContent();
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
        /// Here you create the Render Technic and the World
        /// </summary>
        /// <param name="es"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory,contentManager);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, IContentManager contentManager)
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
            base.InitScreen(GraphicInfo, contentManager);
        }

        protected abstract void SetWorldAndRenderTechnich(out IRenderTechnic[] renderTech, out IWorld world );
        
      
    }
}
