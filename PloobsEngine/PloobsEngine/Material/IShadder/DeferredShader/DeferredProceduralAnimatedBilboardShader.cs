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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Light;
using PloobsEngine.Cameras;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Simple Procedural Animated Bilboard.     
    /// </summary>
    public class DeferredProceduralAnimatedcilindricBilboardShader : IShader
    {
        
        private Effect _shader;
        private Vector2 scale = new Vector2(10, 10);

        /// <summary>
        /// Default Vector2(10, 10);
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

        private Vector3 allowRotDir = new Vector3(0,1,0);

        /// <summary>
        /// Default Vector3(0,1,0)
        /// </summary>
        public Vector3 AllowRotDir
        {
            get { return allowRotDir; }
            set { allowRotDir = value; }
        }

        public override MaterialType MaterialType
        {
            get { return MaterialType.DEFERRED; }
        }

        private float amplitude = 1;

        /// <summary>
        /// Default 1
        /// </summary>
        public float Amplitude
        {
            get { return amplitude; }
            set { amplitude = value; }
        }

        private float movimentSpeedControl = 5;

        /// <summary>
        /// Default 5
        /// </summary>
        public float MovimentSpeedControl
        {
            get { return movimentSpeedControl; }
            set { movimentSpeedControl = value; }
        }

        private float alphaTestLimit = 200.0f / 256.0f;

        public float AlphaTestLimit
        {
            get { return alphaTestLimit; }
            set { alphaTestLimit = value; }
        }

        public override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
            _shader.Parameters["amplitude"].SetValue(amplitude);
            _shader.Parameters["gTime"].SetValue((float)gt.TotalGameTime.TotalMilliseconds);
            _shader.Parameters["timeScale"].SetValue(movimentSpeedControl);
            _shader.Parameters["xCamPos"].SetValue(cam.Position);
 	        _shader.Parameters["scaleX"].SetValue(scale.X);
            _shader.Parameters["scaleY"].SetValue(scale.Y);
            _shader.Parameters["xWorld"].SetValue(obj.PhysicObject.WorldMatrix);
            _shader.Parameters["xView"].SetValue(cam.View);
            _shader.Parameters["xProjection"].SetValue(cam.Projection);                        
            _shader.Parameters["xBillboardTexture"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE,0,0));
            _shader.Parameters["atenuation"].SetValue(atenuation);
            _shader.Parameters["xAllowedRotDir"].SetValue(allowRotDir);

            render.PushRasterizerState(RasterizerState.CullNone);

            BatchInformation batchInfo = obj.Modelo.GetBatchInformation(0)[0];
            {
                _shader.Parameters["alphaTest"].SetValue(alphaTestLimit);
                render.RenderBatch(batchInfo, _shader);
            }

            render.PopRasterizerState();
        }


        public override void Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
        {
            this._shader = factory.GetEffect("GrassBillboard",true,true);            
        }


        public override void BasicDraw(GameTime gt, IObject obj, Matrix view, Matrix projection, IList<ILight> lights, RenderHelper render, Plane? clippingPlane, bool useAlphaBlending = false)
        {
            ///no reflection refraction
        }

        public override void DepthExtractor(GameTime gt, IObject obj, Matrix View, Matrix projection, RenderHelper render)
        {
            ///no shadow
        }

    }
}
#endif