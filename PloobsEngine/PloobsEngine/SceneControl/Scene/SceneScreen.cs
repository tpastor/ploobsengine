using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.Audio;
using PloobsEngine.Entity;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Descreve a 3D Screen of the Game
    /// </summary>
    /// 
    public class SceneScreen : IScreen
    {
        #region properties

        private bool _isDebugDrawn = false;
        private IRenderTechnic[] _renderTecnics = null;
        private IRenderHelper _render;
        private bool _isFirstTimeTechnic = true;

        /// <summary>
        /// Gets or sets the render Helper used by this screen.
        /// </summary>
        /// <value>
        /// The render.
        /// </value>
        public IRenderHelper Render
        {
            set
            {
                this._render = value;
            }
            get
            {
                return this._render;
            }
        }

        /// <summary>
        /// Gets or sets the render technics that this screen will use.
        /// </summary>
        /// <value>
        /// The render technics.
        /// </value>
        public IRenderTechnic[] RenderTechnics
        {
            get
            {
                return _renderTecnics;
            }
            set
            {
                _isFirstTimeTechnic = true;
                this._renderTecnics = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in debug drawn mode.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug drawn; otherwise, <c>false</c>.
        /// </value>
        public bool isDebugDrawn
        {
            set
            {
                this._isDebugDrawn = value;
            }
            get
            {
                return this._isDebugDrawn;
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
            set
            {
                this._world = value;
            }
            get
            {
                return this._world;
            }
        }

        /// <summary>
        /// Function called After all the stuffs LoadContent is called
        /// </summary>
        public override void AfterLoadContent()
        {
            foreach (IRenderTechnic item in RenderTechnics)
            {
                item.AfterLoadContent();

            }
        }
       

        #endregion

        /// <summary>
        /// Allows the screen to run logic, such as updating the transition position.
        /// Unlike HandleInput, this method is called regardless of whether the screen
        /// is active, hidden, or in the middle of a transition.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="otherScreenHasFocus"></param>
        /// <param name="coveredByOtherScreen"></param>
        public override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
            World.UpdateWorld(gameTime);
        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_isFirstTimeTechnic == true)
            {                
                foreach (IRenderTechnic item in RenderTechnics)
                {
                    item.Initialization(Render,this.World);
                }
                _isFirstTimeTechnic = false;
            }

            foreach (IRenderTechnic item in RenderTechnics)
            {                
                item.ExecuteTechnic(Render, this.World);
            }                 
            
        
            base.Draw(gameTime);
            
        }

        /// <summary>
        /// Load content for the screen.
        /// Here you create the Render Technic and the World
        /// </summary>
        /// <param name="es"></param>
        public override void LoadContent()
        {               
            //this._render = new RenderHelper(es);            
            base.LoadContent();
        }
      
    }
}
