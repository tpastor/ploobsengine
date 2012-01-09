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
using PloobsEngine.Engine;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Material
{

    /// <summary>
    /// Description that helps building the XNABasicShader
    /// </summary>
    public struct ForwardXNABasicShaderDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardXNABasicShaderDescription"/> struct.
        /// </summary>
        /// <param name="AmbientColor">Color of the ambient.</param>
        /// <param name="EmissiveColor">Color of the emissive.</param>
        /// <param name="SpecularColor">Color of the specular.</param>
        /// <param name="specularPower">The specular power.</param>
        /// <param name="alpha">The alpha.</param>
        /// <param name="EnableLightning">if set to <c>true</c> [enable lightning].</param>
        /// <param name="DefaultLightning">if set to <c>true</c> [default lightning].</param>
        /// <param name="EnableTexture">if set to <c>true</c> [enable texture].</param>
        public ForwardXNABasicShaderDescription(Color AmbientColor, Color EmissiveColor, Color SpecularColor, float specularPower = 50, float alpha = 1, bool EnableLightning = true, bool DefaultLightning = true, bool EnableTexture = true)
        {
            this.AmbientColor = AmbientColor.ToVector3();
            this.EmissiveColor = EmissiveColor.ToVector3();
            this.SpecularColor = SpecularColor.ToVector3();
            this.SpecularPower = specularPower;
            this.alpha = alpha;
            this.EnableLightning = EnableLightning;
            this.EnableTexture = EnableTexture;
            this.DefaultLightning = DefaultLightning;
            ObjectColor = Vector3.Zero;
        }

        /// <summary>
        /// Defaults this instance.
        /// </summary>
        /// <returns></returns>
        public static ForwardXNABasicShaderDescription Default()
        {
            ForwardXNABasicShaderDescription desc = new ForwardXNABasicShaderDescription(Color.White, Color.Black, Color.White, 0, 1, false, true);
            return desc;
        }
        
        public bool DefaultLightning;
        public Vector3 AmbientColor;
        public bool EnableLightning;
        public bool EnableTexture;
        public Vector3 EmissiveColor;
        public Vector3 SpecularColor;
        public float SpecularPower;
        public float alpha;
        public Vector3 ObjectColor;

             
    }

    /// <summary>
    /// XNA basic shader
    /// Wrapper to xna basic effect
    /// </summary>
    public class ForwardXNABasicShader : IShader
    {
        ForwardXNABasicShaderDescription desc;
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardXNABasicShader"/> class.
        /// </summary>
        /// <param name="desc">The desc.</param>
        public ForwardXNABasicShader(ForwardXNABasicShaderDescription desc)
        {
            this.desc = desc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardXNABasicShader"/> class.
        /// </summary>
        public ForwardXNABasicShader()
            : this(ForwardXNABasicShaderDescription.Default())
        {            
        }

        private BasicEffect effect;

        public BasicEffect BasicEffect
        {
            get { return effect; }            
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="ginfo"></param>
        /// <param name="factory"></param>
        /// <param name="obj"></param>
        public override void Initialize(GraphicInfo ginfo, GraphicFactory factory, IObject obj)
        {
            effect = factory.GetBasicEffect();            
            base.Initialize(ginfo,factory,obj);
            effect.PreferPerPixelLighting = true;
            SetDescription(desc);        
        }

        public ForwardXNABasicShaderDescription GetDescription()
        {
            return desc;
        }

        /// <summary>
        /// Sets the description.
        /// </summary>
        /// <param name="desc">The desc.</param>
        public void SetDescription(ForwardXNABasicShaderDescription desc)
        {
            this.desc = desc;
            if (desc.EnableLightning)
            {
                if (desc.DefaultLightning)
                {
                    effect.EnableDefaultLighting();
                }
                else
                {
                    effect.LightingEnabled = true;
                    effect.SpecularColor = desc.SpecularColor;
                    effect.SpecularPower = desc.SpecularPower;
                    effect.EmissiveColor = desc.EmissiveColor;
                    effect.AmbientLightColor = desc.AmbientColor;
                }
            }

            if (desc.EnableTexture)
                effect.TextureEnabled = desc.EnableTexture;
            else
                effect.DiffuseColor = desc.ObjectColor;

            effect.Alpha = desc.alpha;            
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gt">gametime</param>
        /// <param name="obj">the obj</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights"></param>
        public override void Draw(GameTime gt, IObject obj, RenderHelper render, Cameras.ICamera cam, IList<Light.ILight> lights)
        {
            base.Draw(gt, obj, render, cam, lights);
            
            effect.View = cam.View;
            effect.Projection = cam.Projection;

            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {
                    effect.Texture = obj.Modelo.getTexture(Modelo.TextureType.DIFFUSE,i,j);
                    effect.World = bi[j].ModelLocalTransformation * obj.WorldMatrix;
                    render.RenderBatch(bi[j],effect);
                }
            }
        }

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public override MaterialType MaterialType
        {
            get { return Material.MaterialType.FORWARD; }
        }
    }
}
