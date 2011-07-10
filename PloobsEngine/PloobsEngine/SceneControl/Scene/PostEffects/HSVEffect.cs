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
    public class HSVEffect : IPostEffect
    {
        public HSVEffect(Vector4 toAdd, Vector4 toMultiply) : base(PostEffectType.All)
        {
            this.toAdd = toAdd;
            this.toMultiply = toMultiply;
        }

        #region IPostEffect Members        
        private Effect hsv;
        Vector4 toAdd;

        public Vector4 ToAdd
        {
            get { return toAdd; }
            set { toAdd = value; }
        }
        Vector4 toMultiply;

        public Vector4 ToMultiply
        {
            get { return toMultiply; }
            set { toMultiply = value; }
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {            
            this.hsv = factory.GetEffect("HSV",false,true);            
        }

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
           hsv.Parameters["cena"].SetValue(ImageToProcess);
           hsv.Parameters["toMultiply"].SetValue(toMultiply);
           hsv.Parameters["toAdd"].SetValue(toAdd);
           if (useFloatingBuffer)
               rHelper.RenderFullScreenQuadVertexPixel(hsv, SamplerState.PointClamp);
           else
               rHelper.RenderFullScreenQuadVertexPixel(hsv, GraphicInfo.SamplerState);        
        }

        #endregion
    }
}
#endif