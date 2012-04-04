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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Deferred implementation of final combination pass
    /// </summary>
    public class PreFinalCombination : IDeferredFinalCombination
    {                      
        private Effect finalCombineEffect;                 
        private RenderTarget2D target;
        GraphicInfo ginfo;
        private Color ambientColor = Color.Black;
        private bool saveToTexture;
        private bool useFloatBuffer;

        /// <summary>
        /// Gets or sets the ambient color factor.
        /// Uniform for all the scene
        /// </summary>
        /// <value>
        /// The color of the ambient.
        /// </value>
        public Color AmbientColor
        {
            get { return ambientColor; }
            set { ambientColor = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FinalCombination"/> class.
        /// </summary>
        /// <param name="AmbientColor">Color of the ambient.</param>
        public PreFinalCombination(Color AmbientColor)
        {
            this.ambientColor = AmbientColor;
        }       
        

#region IDeferredFinalCombination Members


        public Texture2D this[GBufferTypes type]
        {
            get
            {
                if (type == GBufferTypes.FINALIMAGE)
                {
                    return this.target;
                }
                else
                {
                    ActiveLogger.LogMessage("Wrong Image Type requested", LogLevel.FatalError);
                    throw new Exception("wrong type");
                }
            }
        }

        #endregion

#region IDeferredFinalCombination Members

        /// <summary>
        /// Sets the final combination.
        /// </summary>
        /// <param name="render">The render.</param>
        public void SetFinalCombination(RenderHelper render)
        {
            if (saveToTexture)
            {
                render.PushRenderTarget(target);                
            }            
        }

        public void DrawScene(GameTime gameTime, IWorld world, IDeferredGBuffer gbuffer, IDeferredLightMap lightmap, RenderHelper render)
        {
            render.PushDepthStencilState(DepthStencilState.None);

            PambientColor.SetValue(ambientColor.ToVector3());
            render.Textures[0] = lightmap[DeferredLightMapType.LIGHTMAP];
            SamplerState s0 = render.SetSamplerState(SamplerState.PointClamp, 0);

            render.Textures[1] = gbuffer[GBufferTypes.Extra1];
            render.Textures[2] = gbuffer[GBufferTypes.COLOR];
            //PEXTRA1.SetValue(gbuffer[GBufferTypes.Extra1]);            
            //PcolorMap.SetValue(gbuffer[GBufferTypes.COLOR]);
            //PlightMap.SetValue(lightmap[DeferredLightMapType.LIGHTMAP]);            

            render.RenderFullScreenQuadVertexPixel(finalCombineEffect);

            render.SetSamplerState(s0, 0);            

            if (saveToTexture)
            {
                render.PopRenderTarget();                
            }
            render.PopDepthStencilState();
            
        }

        EffectParameter PhalfPixel;
        //EffectParameter PEXTRA1;
        EffectParameter PambientColor;
        //EffectParameter PcolorMap;
        //EffectParameter PlightMap;

        #endregion

#region IDeferredFinalCombination Members


        public void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, bool useFloatBuffer, bool saveToTexture )
        {
            this.useFloatBuffer = useFloatBuffer;
            this.ginfo = ginfo;
            this.saveToTexture = saveToTexture;
            finalCombineEffect = manager.GetAsset<Effect>("CombineFinal",true);
            PhalfPixel = finalCombineEffect.Parameters["halfPixel"];            
            PambientColor = finalCombineEffect.Parameters["ambientColor"];
            //PEXTRA1 = finalCombineEffect.Parameters["EXTRA1"];
            //PcolorMap = finalCombineEffect.Parameters["colorMap"];
            //PlightMap = finalCombineEffect.Parameters["lightMap"];
            

            PhalfPixel.SetValue(ginfo.HalfPixel);
            if (saveToTexture)
            {
                if (useFloatBuffer)
                    target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
                else
                    target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
            }                        
        }

        #endregion
    }
}
#endif