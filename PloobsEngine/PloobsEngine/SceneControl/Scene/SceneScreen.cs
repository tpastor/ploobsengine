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
        
        private IRenderTechnic[] _renderTecnics = null;
        private IRenderHelper _render = null;
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
            internal get
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
        protected override void AfterLoadContent()
        {
            foreach (IRenderTechnic item in RenderTechnics)
            {
                item.AfterLoadContent();

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
        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
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
            
        }

        /// <summary>
        /// Load content for the screen.
        /// Here you create the Render Technic and the World
        /// </summary>
        /// <param name="es"></param>
        protected override void LoadContent()        {               
            
            base.LoadContent();
        }
      
    }
}
