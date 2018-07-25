﻿#region License
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
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Helper To Restore Depth in deferred Pass
    /// In XNA its needed cause when we change Render Targets, the depth buffer is erased
    /// </summary>
    internal class RestoreDepth
    {
        private bool usefloatBuffer;
        EffectParameter Depth;
        EffectParameter Color;
        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreDepth"/> class.
        /// </summary>
        /// <param name="useFloatBuffer">if set to <c>true</c> [use float buffer].</param>
        /// <param name="manager">The manager.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="ginfo">The ginfo.</param>
        public RestoreDepth(bool useFloatBuffer,IContentManager manager,GraphicFactory factory, GraphicInfo ginfo)
        {
            this.usefloatBuffer = useFloatBuffer;
            this.restore = manager.GetAsset<Effect>("RestoreDepth",false);
            this.restore.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
            
            Depth = restore.Parameters["Depth"];
            Color = restore.Parameters["Color"];

            if (useFloatBuffer)
                target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
            else
                target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
        }        

        private RenderTarget2D target;
        private Effect restore;
        
        /// <summary>
        /// Performs the forward pass.
        /// </summary>
        /// <param name="combined">The combined.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="render">The render.</param>
        /// <param name="ginfo">The ginfo.</param>
        public void PerformForwardPass(Texture2D combined, Texture2D depth, RenderHelper render,GraphicInfo ginfo)
        {
            render.PushRenderTarget(target);
            SamplerState s1 = render.SetSamplerState(SamplerState.PointClamp, 1);

            Depth.SetValue(depth);                
            Color.SetValue(combined);
            SamplerState s0;
            if (usefloatBuffer)
            {
                s0 = render.SetSamplerState(SamplerState.PointClamp, 0);                
            }
            else
            {
                s0 = render.SetSamplerState(ginfo.SamplerState, 0);
            }
            render.RenderFullScreenQuadVertexPixel(restore);
            
            render.SetSamplerState(s1, 1);
            render.SetSamplerState(s0, 0);
        }

        /// <summary>
        /// Ends the forward pass.
        /// Clean Up
        /// </summary>
        /// <returns></returns>
        public Texture2D EndForwardPass(RenderHelper render)
        {
            render.PopRenderTarget();
            return target;
        }
    }
}
#endif