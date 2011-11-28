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

    public struct XNABasicShaderDescription
    {
        public XNABasicShaderDescription(Color AmbientColor, Color EmissiveColor, Color SpecularColor, float specularPower = 50, float alpha = 1, bool EnableLightning = true, bool EnableTexture = true)
        {
            this.AmbientColor = AmbientColor.ToVector3();
            this.EmissiveColor = EmissiveColor.ToVector3();
            this.SpecularColor = SpecularColor.ToVector3();
            this.SpecularPower = specularPower;
            this.alpha = alpha;
            this.EnableLightning = EnableLightning;
            this.EnableTexture = EnableTexture;
            DefaultLightning = false;
        }

        public static XNABasicShaderDescription Default()
        {
            XNABasicShaderDescription desc = new XNABasicShaderDescription(Color.White, Color.Black, Color.White, 0, 1, false, true);
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
             
    }

    /// <summary>
    /// XNA basic shader
    /// Wrapper to xna basic effect
    /// </summary>
    public class XNABasicShader : IShader
    {
        XNABasicShaderDescription desc;
        /// <summary>
        /// Initializes a new instance of the <see cref="XNABasicShader"/> class.
        /// </summary>
        /// <param name="desc">The desc.</param>
        public XNABasicShader(XNABasicShaderDescription desc)
        {
            this.desc = desc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XNABasicShader"/> class.
        /// </summary>
        public XNABasicShader()
            : this(XNABasicShaderDescription.Default())
        {            
        }

        private BasicEffect effect;

        public override void Initialize(GraphicInfo ginfo, GraphicFactory factory, IObject obj)
        {
            effect = factory.GetBasicEffect();            
            base.Initialize(ginfo,factory,obj);
            effect.PreferPerPixelLighting = true;
            SetDescription(desc);            
        }

        public XNABasicShaderDescription GetDescription()
        {
            return desc;
        }

        public void SetDescription(XNABasicShaderDescription desc)
        {
            this.desc = desc;
            if (desc.DefaultLightning)
            {
                effect.EnableDefaultLighting();
            }
            else
            {
                if (effect.LightingEnabled)
                {
                    effect.LightingEnabled = true;
                    effect.SpecularColor = desc.SpecularColor;
                    effect.SpecularPower = desc.SpecularPower;
                    effect.EmissiveColor = desc.EmissiveColor;
                    effect.AmbientLightColor = desc.AmbientColor;                    
                }
            }
            
            effect.TextureEnabled = desc.EnableTexture;
            effect.Alpha = desc.alpha;
            
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gt, SceneControl.IObject obj, RenderHelper render, Cameras.ICamera cam)
        {
            effect.Texture = obj.Modelo.getTexture(Modelo.TextureType.DIFFUSE);
            effect.View = cam.View;
            effect.Projection = cam.Projection;

            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {
                    effect.World = bi[j].ModelLocalTransformation;
                    effect.CurrentTechnique.Passes[0].Apply();
                    render.RenderBatch(ref bi[j]);
                }
            }
        }        

        public override MaterialType MaterialType
        {
            get { return Material.MaterialType.FORWARD; }
        }
    }
}
