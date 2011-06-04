using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physics2D.Farseer;
using FarseerPhysics.Common;
using PloobsEngine.Engine;

namespace PloobsEngine.Material2D
{
    public class Basic2DTextureMaterial : I2DMaterial
    {   
        #region IMaterial2D Members

        public void Initialization(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, SceneControl.IObject obj)
        {
            
        }

        public virtual void PreDrawnPhase(Microsoft.Xna.Framework.GameTime gt, SceneControl._2DScene.I2DWorld mundo, SceneControl._2DScene.I2DObject obj, SceneControl.RenderHelper render)
        {
            
        }

        /// <summary>
        /// Draws 
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        public void Draw(Microsoft.Xna.Framework.GameTime gt, SceneControl._2DScene.I2DObject obj, SceneControl.RenderHelper render)
        {
            render.RenderTexture(obj.Modelo.Texture, ConvertUnits.ToDisplayUnits(obj.PhysicObject.Position), Color.White, obj.PhysicObject.Rotation, obj.PhysicObject.Origin + obj.Modelo.Origin, 1);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime, SceneControl.IObject obj)
        {
            
        }

        #endregion
    }
}
