#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
#if !WINDOWS_PHONE && !REACH
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Material
{
    public class DeferredCilindricGPUBilboardShader : IShader
    {        
        private Effect _shader;
        private Vector3 allowRotationDirection = new Vector3(0, 1, 0);

        /// <summary>
        /// Default Vector3(0, 1, 0)
        /// </summary>
        public Vector3 AllowRotationDirection
        {
            get { return allowRotationDirection; }
            set { allowRotationDirection = value; }
        }
        private Vector2 scale = new Vector2(100, 100);

        /// <summary>
        /// Default Vector2(100, 100)
        /// </summary>
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private Vector4 atenuation = Vector4.One;

        /// <summary>
        /// Default Vector4.One
        /// </summary>
        public Vector4 Atenuation
        {
            get { return atenuation; }
            set { atenuation = value; }
        }

        public override MaterialType MaterialType
        {
            get { return MaterialType.DEFERRED; }
        }

        public override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
            _shader.Parameters["scaleX"].SetValue(scale.X);
            _shader.Parameters["scaleY"].SetValue(scale.Y);
            _shader.Parameters["xWorld"].SetValue(obj.PhysicObject.WorldMatrix);
            _shader.Parameters["xView"].SetValue(cam.View);
            _shader.Parameters["xProjection"].SetValue(cam.Projection);
            _shader.Parameters["xCamPos"].SetValue(cam.Position);
            _shader.Parameters["xAllowedRotDir"].SetValue(allowRotationDirection);
            _shader.Parameters["xBillboardTexture"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE,0,0));
            _shader.Parameters["atenuation"].SetValue(atenuation);
            
            BatchInformation batchInfo = obj.Modelo.GetBatchInformation(0)[0];            
            {                
                _shader.Parameters["alphaTest"].SetValue(alphaTestLimit);
                render.RenderBatch(batchInfo, _shader);                
            }
            
        }

        public override void BasicDraw(GameTime gt, IObject obj, Matrix view, Matrix projection, IList<ILight> lights, RenderHelper render, Plane? clippingPlane, bool useAlphaBlending = false)
        {
            ///no reflection refraction
        }

        public override void DepthExtractor(GameTime gt, IObject obj, Matrix View, Matrix projection, RenderHelper render)
        {
            ///no shadow =P
        }

        private float alphaTestLimit = 200.0f / 256.0f;

        public float AlphaTestLimit
        {
            get { return alphaTestLimit; }
            set { alphaTestLimit = value; }
        }

        public override void Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
        {
            this._shader = factory.GetEffect("cilBilboard",false,true);
            base.Initialize(ginfo, factory, obj);
        }





    }
}
#endif