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
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.SceneControl
{
    public class DepthCueingPostEffect : IPostEffect
    {
        public DepthCueingPostEffect()
            : base(PostEffectType.Deferred)
        {
        }

        Effect effect = null;

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
             effect.Parameters["Z_Near"].SetValue(world.CameraManager.ActiveCamera.NearPlane);
             effect.Parameters["Z_Far"].SetValue(world.CameraManager.ActiveCamera.FarPlane);
             effect.Parameters["depth"].SetValue(rHelper[PrincipalConstants.DephRT]);
             effect.Parameters["color"].SetValue(ImageToProcess);
             effect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(world.CameraManager.ActiveCamera.View * world.CameraManager.ActiveCamera.Projection));
             effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
             effect.Parameters["cameraPosition"].SetValue(world.CameraManager.ActiveCamera.Position);

             if (useFloatingBuffer)
                 rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
             else
                 rHelper.RenderFullScreenQuadVertexPixel(effect, GraphicInfo.SamplerState);        
         
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("CuingDepth",false,true);            
        }

    }
}


#endif