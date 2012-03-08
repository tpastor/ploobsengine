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
    /// Dynamic Bilboard
    /// </summary>
    public class DeferredInstancedBilboardShader : IShader
    {
        /// <summary>
        /// Bilboard animada
        /// </summary>
        /// <param name="bilboardType">Type of the bilboard.</param>
        public DeferredInstancedBilboardShader(BilboardType bilboardType)
        {
            this.bilboardType = bilboardType;            
        }

        BilboardType bilboardType;       
        
        
        private Effect _shader;
        private Vector2 scale = new Vector2(100, 100);

        /// <summary>
        /// Bilboard tem tamanho 1,1 -> deve-se ser multiplicada por um fator de escala
        /// Default Vector2(100, 100);
        /// </summary>
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private Vector4 atenuation = Vector4.One;

        /// <summary>
        /// Atenuacao na cor da textura utilizada
        /// Default Vector4.One
        /// </summary>
        public Vector4 Atenuation
        {
            get { return atenuation; }
            set { atenuation = value; }
        }

        private Vector3 allowRotDir = new Vector3(0,1,0);

        /// <summary>
        /// Constraint da bilboard IF CILINDRIC
        /// SPHERICAL DOES NOT HAVE CONSTRAINT DIRECTION
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
        /// Amplitude do movimento (os dois pontos de cima da billboard podem se movimentar)
        /// Default 1
        /// </summary>
        public float Amplitude
        {
            get { return amplitude; }
            set { amplitude = value; }
        }

        private float movimentSpeedControl = 5;

        /// <summary>
        /// Velocidade de moviemnto
        /// Default 5
        /// </summary>
        public float MovimentSpeedControl
        {
            get { return movimentSpeedControl; }
            set { movimentSpeedControl = value; }
        }

        public override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
            System.Diagnostics.Debug.Assert(obj.Modelo is InstancedBilboardModel, "This shader expects a InstancedBilboardModel");
            _shader.Parameters["xWorld"].SetValue(obj.PhysicObject.WorldMatrix);
            _shader.Parameters["xBillboardTexture"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE,0,0));
            _shader.Parameters["atenuation"].SetValue(atenuation);
            _shader.Parameters["forward"].SetValue(Vector3.Normalize(cam.Target - cam.Position));

            if (bilboardType == BilboardType.Cilindric)
            {
                _shader.Parameters["xAllowedRotDir"].SetValue(allowRotDir);
                _shader.Parameters["cilindric"].SetValue(true);
                
            }
            else
            {
                _shader.Parameters["cilindric"].SetValue(false);
                _shader.Parameters["xAllowedRotDir"].SetValue(cam.Up);                
            }

            _shader.Parameters["xProjection"].SetValue(cam.Projection);                                   
 	        _shader.Parameters["xView"].SetValue(cam.View);
            _shader.Parameters["xCamPos"].SetValue(cam.Position);          


            render.PushRasterizerState(RasterizerState.CullNone);
            //render.PushDepthStencilState(DepthStencilState.None);
            
            BatchInformation batchInfo = obj.Modelo.GetBatchInformation(0)[0];
            {
                _shader.Parameters["alphaTest"].SetValue(alphaTestLimit);
                render.RenderBatch(batchInfo, _shader);
            }
            //render.PopDepthStencilState();
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
            this._shader = factory.GetEffect("InstancedBilboard", true, true);            
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