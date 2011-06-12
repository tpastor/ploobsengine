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

        public Basic2DTextureMaterial()
        {
            CastShadow = true;

        }

        /// <summary>
        /// Gets or sets a value indicating whether [cast shadow].
        /// Default true
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cast shadow]; otherwise, <c>false</c>.
        /// </value>
        public bool CastShadow
        {
            set;
            get;
        }


        public override void Draw(GameTime gt, SceneControl._2DScene.I2DObject obj, SceneControl.RenderHelper render)
        {
            render.RenderTexture(obj.Modelo.Texture, obj.PhysicObject.Position, obj.Modelo.SourceRectangle, Color.White, obj.PhysicObject.Rotation + obj.Modelo.Rotation, obj.Modelo.Origin + obj.PhysicObject.Origin, 1, SpriteEffects.None, obj.Modelo.LayerDepth);
        }
        
        public override void LightDraw(GameTime gt, SceneControl._2DScene.I2DObject obj, SceneControl.RenderHelper render, Color color, PloobsEngine.Light2D.Light2D light)
        {
            if(CastShadow)
                render.RenderTexture(obj.Modelo.Texture, light.ToRelativePosition(obj.PhysicObject.Position), obj.Modelo.SourceRectangle, color, obj.PhysicObject.Rotation + obj.Modelo.Rotation, obj.Modelo.Origin + obj.PhysicObject.Origin, 1);
        }
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

        public void ProcessLightDraw(GameTime gameTime, SceneControl.RenderHelper render, SceneControl._2DScene.ICamera2D camera, List<SceneControl._2DScene.I2DObject> objects, Color color, PloobsEngine.Light2D.Light2D light)
        {
            render.RenderBegin(camera.View, null);
            foreach (var iobj in objects)
            {
                if (iobj.PhysicObject.Enabled == true)
                {
                    iobj.Material.LightDraw(gameTime, iobj, render, color, light);
                }
            }
            render.RenderEnd();
        }

        #endregion
    }
}
