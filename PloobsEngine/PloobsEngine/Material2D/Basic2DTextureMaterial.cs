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

        /// <summary>
        /// Draws 
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gt, SceneControl._2DScene.I2DObject obj, SceneControl.RenderHelper render)
        {
            render.RenderTexture(obj.Modelo.Texture, ConvertUnits.ToDisplayUnits(obj.PhysicObject.Position), Color.White, obj.PhysicObject.Rotation, obj.Modelo.Origin + obj.PhysicObject.Origin, 1);
        }
        #endregion        
    }

    public class Basic2DTextureMaterialProcessor : IMaterialProcessor
    {
        #region IMaterialProcessor Members

        public void ProcessDraw(GameTime gameTime, SceneControl.RenderHelper render, SceneControl._2DScene.ICamera2D camera, List<SceneControl._2DScene.I2DObject> objects)
        {
            render.RenderBegin(camera.View, null);
            foreach (var iobj in objects)
            {
                if (iobj.PhysicObject.Enabled == true)
                {
                    iobj.Material.Draw(gameTime, iobj, render);
                }
            }
            render.RenderEnd();
        }

        public void ProcessPreDraw(GameTime gameTime, SceneControl.RenderHelper render, SceneControl._2DScene.ICamera2D camera, SceneControl._2DScene.I2DWorld world, List<SceneControl._2DScene.I2DObject> objects)
        {
            render.RenderBegin(camera.View, null);
            foreach (var iobj in objects)
            {
                if (iobj.PhysicObject.Enabled == true)
                {
                    iobj.Material.PreDrawnPhase(gameTime, world,iobj, render);
                }
            }
            render.RenderEnd();
        }

        #endregion
    }
}
