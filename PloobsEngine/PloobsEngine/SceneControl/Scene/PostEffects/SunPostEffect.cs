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

namespace PloobsEngine.SceneControl
{
    public class SunPostEffect : IPostEffect
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SunPostEffect"/> class.
        /// </summary>
        /// <param name="flareTexture">The flare texture, null to use default.</param>
        public SunPostEffect(String flareTexture = null) : base(PostEffectType.Deferred) 
        {
            this.flareTexture = flareTexture;
        }

#region IPostEffect Members
        private string flareTexture = null;        
               
        Effect effect = null;
        /// <summary>
        /// default = Color.White
        /// </summary>
        private Color sunColor = Color.White;

        public Color SunColor
        {
            get { return sunColor; }
            set { sunColor = value; }
        }
        /// <summary>
        /// default = 1
        /// </summary>
        private float sunIntensity = 1f;

        public float SunIntensity
        {
            get { return sunIntensity; }
            set { sunIntensity = value; }
        }
        /// <summary>
        /// default = 1500
        /// </summary>
        private float sunSunSize = 1500;

        public float SunSunSize
        {
            get { return sunSunSize; }
            set { sunSunSize = value; }
        }
        /// <summary>
        /// default = Vector3(1000,1000,0)
        /// </summary>
        private Vector3 sunPosition = new Vector3(1000,1000,0);

        public Vector3 SunPosition
        {
            get { return sunPosition; }
            set { sunPosition = value; }
        }

        Texture2D tex;


        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
         
            effect.Parameters["Color"].SetValue(sunColor.ToVector3());
            effect.Parameters["lightIntensity"].SetValue(sunIntensity);
            effect.Parameters["SunSize"].SetValue(sunSunSize);
            effect.Parameters["lightPosition"].SetValue(sunPosition);
            effect.Parameters["flare"].SetValue(tex);

            effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
            effect.Parameters["VP"].SetValue(world.CameraManager.ActiveCamera.ViewProjection);
            effect.Parameters["cameraPosition"].SetValue(world.CameraManager.ActiveCamera.Position);

            effect.Parameters["BackBufferTex"].SetValue(ImageToProcess);
            effect.Parameters["Extra1"].SetValue(rHelper[PrincipalConstants.extra1RT]);

            if (useFloatingBuffer)
                rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
            else
                rHelper.RenderFullScreenQuadVertexPixel(effect, GraphicInfo.SamplerState);              
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            
            effect = factory.GetEffect("Sun",false,true);
            if (flareTexture==null)
                tex = factory.GetTexture2D("flare",true);
            else
                tex = factory.GetTexture2D(flareTexture);          
        }
       
        #endregion
    }
}
#endif