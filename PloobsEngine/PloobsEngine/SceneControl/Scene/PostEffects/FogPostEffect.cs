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
#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Fog Attenuation types
    /// </summary>
    public enum FogType
    {
        /// <summary>
        /// Linear decaiment
        /// </summary>
        LINEAR,
        /// <summary>
        /// Exponencial decaiment
        /// </summary>
        EXPONENCIAL,
        /// <summary>
        /// Squared Exponencial decaiment
        /// </summary>
        EXPONENCIALSQUARED
    }

    /// <summary>
    /// Fog Post Effect
    /// </summary>
    public class FogPostEffect : IPostEffect
    {
        /// <summary>
        /// Create the Post Effect using Linear Fog
        /// </summary>
        /// <param name="nearPlane"></param>
        /// <param name="farPlane"></param>
        public FogPostEffect(float nearPlane,float farPlane) : base(PostEffectType.Deferred)
        {
            this.farPlane = farPlane;
            this.nearPlane = nearPlane;
            fogType = FogType.LINEAR;
        }

        /// <summary>
        /// Create the post effect for using Exponencial or Exponencial Squared
        /// </summary>
        /// <param name="density"></param>
        /// <param name="type"></param>
        public FogPostEffect(float density, FogType type) : base(PostEffectType.Deferred)
        {
            System.Diagnostics.Debug.Assert(type != SceneControl.FogType.LINEAR, "For linear fog, use the other constructor");
            this.density = density;
            fogType = type;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FogPostEffect"/> class.
        /// Set the density for Exponencial and Exponencial Squared and near/far plane for Linear
        /// </summary>
        /// <param name="type">The type.</param>
        public FogPostEffect(FogType type) : base(PostEffectType.Deferred)
        {            
            fogType = type;
        }

        #region IPostEffect Members
               
        Effect effect = null;
        private Color fogColor = new Color(0.5f,0.5f,0.5f);
        private FogType fogType;
        private float density = 0.001f;

        /// <summary>
        /// Default 0.001f;
        /// </summary>
        public float Density
        {
            get { return density; }
            set { density = value; }
        }

        public FogType FogType
        {
            get { return fogType; }
            set { fogType = value; }
        }

        public Color FogColor
        {
            get { return fogColor; }
            set { fogColor = value; }
        }

        private float nearPlane = 1;

        /// <summary>
        /// Gets or sets the near plane.
        /// Default 1
        /// </summary>
        /// <value>
        /// The near plane.
        /// </value>
        public float NearPlane
        {
            get { return nearPlane; }
            set { nearPlane = value; }
        }
        private float farPlane = 1000;

        /// <summary>
        /// Gets or sets the far plane.
        /// Default 1000
        /// </summary>
        /// <value>
        /// The far plane.
        /// </value>
        public float FarPlane
        {
            get { return farPlane; }
            set { farPlane = value; }
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {                                
                effect.Parameters["dz"].SetValue(density);
                effect.Parameters["depth"].SetValue(rHelper[PrincipalConstants.DephRT]);
                effect.Parameters["cena"].SetValue(ImageToProcess);
                effect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(world.CameraManager.ActiveCamera.ViewProjection));
                effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
                effect.Parameters["cameraPosition"].SetValue(world.CameraManager.ActiveCamera.Position);                
                effect.Parameters["near"].SetValue(nearPlane);
                effect.Parameters["far"].SetValue(farPlane);
                effect.Parameters["fcolor"].SetValue(fogColor.ToVector3());

            if(useFloatingBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect,SamplerState.PointClamp);              
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect, GraphicInfo.SamplerState);              
                    
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("Fog",false,true);                        

            switch (fogType)
            {
                case FogType.LINEAR:
                    effect.CurrentTechnique = effect.Techniques["FogShader"];
                    break;
                case FogType.EXPONENCIAL:
                    effect.CurrentTechnique = effect.Techniques["FogExponencialShader"];
                    break;
                case FogType.EXPONENCIALSQUARED:
                    effect.CurrentTechnique = effect.Techniques["FogExponencialSquaredShader"];
                    break;
                default:
                    break;
            }
        }        
        #endregion
    }
}
#endif