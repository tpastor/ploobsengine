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
using PloobsEngine.Light;
using PloobsEngine.Modelo;
using PloobsEngine.Cameras;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Forward Material
    /// </summary>
    public class ForwardTransparenteShader : IShader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardTransparenteShader"/> class.
        /// Use Texture Alpha
        /// </summary>
        public ForwardTransparenteShader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardTransparenteShader"/> class.
        /// Use custom alpha
        /// </summary>
        /// <param name="transparencyLevel">The transparency level.</param>
        public ForwardTransparenteShader(float transparencyLevel)
        {
            useTextureAlpha = false;
            this.TransparencyLevel = TransparencyLevel;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use texture alpha]. as transparency level
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use texture alpha]; otherwise, <c>false</c>.
        /// </value>
        public bool UseTextureAlpha
        {
            get { return useTextureAlpha; }
            set { useTextureAlpha = value; }
        }

        bool useTextureAlpha = true;        
        float transparencyLevel = 0;
        Effect _shader;
        RasterizerState cullMode = RasterizerState.CullNone;

        /// <summary>
        /// Default None
        /// </summary>
        public RasterizerState CullModeToUse
        {
            get { return cullMode; }
            set { cullMode = value; }
        }

        /// <summary>
        /// Between 0 and 1 
        /// If not setted, the texture alpha will be used instead
        /// </summary>
        public float TransparencyLevel
        {
            get { return transparencyLevel; }
            set {

                System.Diagnostics.Debug.Assert(value >= 0 && value <= 1);
                transparencyLevel = value; 
                useTextureAlpha = false; 
            }
        }

        
             

        public override MaterialType MaterialType
        {
            get { return MaterialType.FORWARD; }
        }

        public override void Initialize(GraphicInfo ginfo, GraphicFactory factory, IObject obj)
        {
            _shader = factory.GetEffect("TransparentEffect",true,true);
            base.Initialize(ginfo, factory, obj);
        }

        public override void  PosDrawPhase(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
 	    {              
              render.PushRasterizerState(cullMode);
              render.PushBlendState(BlendState.NonPremultiplied);                                                                        

                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        _shader.Parameters["colorMap"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE,i,j));                                
                        Matrix w1 = Matrix.Multiply(obj.WorldMatrix, bi[j].ModelLocalTransformation);
                        _shader.Parameters["wvp"].SetValue(w1 * cam.ViewProjection);                

                        _shader.Parameters["alpha"].SetValue(transparencyLevel);
                        _shader.Parameters["useTextureAlpha"].SetValue(useTextureAlpha);                       

                        render.RenderBatch(bi[j], _shader);
                    }
                }
                render.PopBlendState();
                render.PopRasterizerState();         
        }
    }
}
#endif