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
using PloobsEngine.Cameras;
using PloobsEngine.Light;

namespace PloobsEngine.Material
{    
    /// <summary>
    /// Spherical Bilboard
    /// </summary>
    public class DeferredSphericalBilboardShader : IShader
    {
        
        private Effect _shader;
        private Vector2 scale = new Vector2(100, 100);

        /// <summary>
        /// Default Vector2(100, 100);
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


        protected override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
            Vector3 dir = cam.Target - cam.Position;
            dir.Normalize();
            _shader.Parameters["forward"].SetValue(dir);
            _shader.Parameters["camUp"].SetValue(cam.Up);
            _shader.Parameters["scaleX"].SetValue(scale.X);
            _shader.Parameters["scaleY"].SetValue(scale.Y);
            _shader.Parameters["xWorld"].SetValue(obj.PhysicObject.WorldMatrix);
            _shader.Parameters["xView"].SetValue(cam.View);
            _shader.Parameters["xProjection"].SetValue(cam.Projection);                        
            //_shader.Parameters["xBillboardTexture"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE,0,0));
            render.Textures[0] = obj.Modelo.getTexture(TextureType.DIFFUSE, 0, 0);
            _shader.Parameters["atenuation"].SetValue(atenuation);

            render.PushRasterizerState(RasterizerState.CullNone);

            BatchInformation batchInfo = obj.Modelo.GetBatchInformation(0)[0];            
            {
                _shader.Parameters["alphaTest"].SetValue(alphaTestLimit);
                render.RenderBatch(batchInfo, _shader);
            }

            render.PopRasterizerState();
        }

        private float alphaTestLimit = 200.0f / 256.0f;

        public float AlphaTestLimit
        {
            get { return alphaTestLimit; }
            set { alphaTestLimit = value; }
        }



        public override void Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
        {
            this._shader = factory.GetEffect("SphericalBillboard",true,true);            
        }


        public override void BasicDraw(GameTime gt, IObject obj, ref Matrix view, ref Matrix projection, IList<ILight> lights, RenderHelper render, Plane? clippingPlane, bool useAlphaBlending = false)
        {
            //no reflection refraction
        }

        public override void DepthExtractor(GameTime gt, IObject obj, ref Matrix View, ref Matrix projection, RenderHelper render)
        {
            //no shadow
        }
    }
}
#endif