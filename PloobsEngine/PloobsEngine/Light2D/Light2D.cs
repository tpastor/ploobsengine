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
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Light2D
{
    public abstract class Light2D
    {
        internal RenderTarget2D RenderTarget { get; set; }
        public Vector2 LightPosition 
        {
            get
            {
                return Vector2.Transform(pos, view);
            }
            set
            {
                pos = value;
            }
        }

        Vector2 pos;
        internal int baseSize;
        public Vector2 LightAreaSize { get; set; }
        public Color Color;
        public float Intensity;

        public Light2D(Vector2 LightPosition, Color color,float intensity, ShadowmapSize size = ShadowmapSize.Size1024)
        {
            this.Intensity = intensity;
            this.Color = color;
            this.LightPosition = LightPosition;
            baseSize = 2 << (int)size;            
            LightAreaSize = new Vector2(baseSize);
            
        }
        Matrix view;
        internal void UpdateLight(Matrix view)
        {
            this.view = view;
        }

        internal Vector2 ToRelativePosition(Vector2 worldPosition)
        {
            return worldPosition - (LightPosition - LightAreaSize * 0.5f);
        }

        internal void BeginDrawingShadowCasters(RenderHelper render)
        {
            render.PushRenderTarget(RenderTarget);
            render.Clear(Color.Transparent,ClearOptions.Target);
        }

        internal void EndDrawingShadowCasters(RenderHelper render)
        {
            render.PopRenderTarget();
        }
    }
}
