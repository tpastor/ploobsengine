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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Engine
{
    public class IntermediateRenderTarget
    {
        public RenderTarget2D RenderTarget;
        public bool InUse;
    }


    internal class RenderTargetPool
    {
        GraphicsDevice device;
        public RenderTargetPool(GraphicsDevice device)
        {
            this.device = device;
        }

        private static List<IntermediateRenderTarget> intermediateTextures = new List<IntermediateRenderTarget>();

        public IntermediateRenderTarget GetIntermediateTexture(int width, int height, bool mipmap, SurfaceFormat SurfaceFormat, DepthFormat DepthFormat, int preferedMultisampleCount, RenderTargetUsage RenderTargetUsage)
        {
            // Look for a matching rendertarget in the cache
            for (int i = 0; i < intermediateTextures.Count; i++)
            {
                if (intermediateTextures[i].InUse == false
                    && height == intermediateTextures[i].RenderTarget.Height
                    && width == intermediateTextures[i].RenderTarget.Width
                    && preferedMultisampleCount == intermediateTextures[i].RenderTarget.MultiSampleCount
                    && SurfaceFormat == intermediateTextures[i].RenderTarget.Format
                    && DepthFormat == intermediateTextures[i].RenderTarget.DepthStencilFormat
                    && RenderTargetUsage == intermediateTextures[i].RenderTarget.RenderTargetUsage
                    && (mipmap == true && intermediateTextures[i].RenderTarget.LevelCount > 0 || mipmap == false && intermediateTextures[i].RenderTarget.LevelCount == 0)

                    )
                {
                    intermediateTextures[i].InUse = true;
                    return intermediateTextures[i];
                }
            }

            // We didn't find one, let's make one
            IntermediateRenderTarget newTexture = new IntermediateRenderTarget();
            newTexture.RenderTarget = new RenderTarget2D(device,width, height, mipmap, SurfaceFormat,DepthFormat, preferedMultisampleCount, RenderTargetUsage );
            intermediateTextures.Add(newTexture);
            newTexture.InUse = true;
            return newTexture;
        }

    }

}

