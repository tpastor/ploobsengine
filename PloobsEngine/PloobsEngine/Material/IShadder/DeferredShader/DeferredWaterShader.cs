using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Ocean Water
    /// </summary>
    public class DeferredWaterShader : IShader
    {

        public DeferredWaterShader(String texCubeName)
        {
            TexureName = texCubeName;
        }

        public TextureCube EnvironmentTexture
        {
            get { return environmentTexture; }
            set { 
                environmentTexture = value;
                TexureName = null;
                if(isInitialized)
                    effect.Parameters["tEnvMap"].SetValue(value); 

            }
        }


        string TexureName;
        TextureCube environmentTexture = null;
        Effect effect;
        float bumpHeight = 0.5f;
        Vector2 textureScale = new Vector2(4, 4);
        Vector2 bumpSpeed = new Vector2(0, .05f);
        float fresnelBias = .025f;
        float fresnelPower = 1.0f;
        float hdrMultiplier = 1.0f;
        Color deepWaterColor = Color.Black;
        Color shallowWaterColor = Color.SkyBlue;
        Color reflectionColor = Color.White;
        float reflectionAmount = 0.5f;
        float waterAmount = 0f;
        float waveAmplitude = 0.5f;
        float waveFrequency = 0.1f;        

        /// <summary>
        /// Height of water bump texture.
        /// Min 0.0 Max 2.0 Default = .5
        /// </summary>
        /// <value>
        /// The height of the bump.
        /// </value>
        public float BumpHeight
        {
            get { return bumpHeight; }
            set { bumpHeight = value; }
        }
        /// <summary>
        /// Scale of bump texture.
        /// </summary>
        /// <value>
        /// The texture scale.
        /// </value>
        public Vector2 TextureScale
        {
            get { return textureScale; }
            set { textureScale = value; }
        }
        /// <summary>
        /// Velocity of water flow
        /// </summary>
        /// <value>
        /// The bump speed.
        /// </value>
        public Vector2 BumpSpeed
        {
            get { return bumpSpeed; }
            set { bumpSpeed = value; }
        }
        /// <summary>
        /// Min 0.0 Max 1.0 Default = .025
        /// </summary>
        /// <value>
        /// The fresnel bias.
        /// </value>
        public float FresnelBias
        {
            get { return fresnelBias; }
            set { fresnelBias = value; }
        }
        /// <summary>
        /// Min 0.0 Max 10.0 Default = 1.0;
        /// </summary>
        /// <value>
        /// The fresnel power.
        /// </value>
        public float FresnelPower
        {
            get { return FresnelPower; }
            set { fresnelPower = value; }
        }
        /// <summary>
        /// Min = 0.0 Max = 100 Default = 1.0
        /// </summary>
        /// <value>
        /// The HDR multiplier.
        /// </value>
        public float HDRMultiplier
        {
            get { return hdrMultiplier; }
            set { hdrMultiplier = value; }
        }
        /// <summary>
        /// Color of deep water Default = Black;
        /// </summary>
        /// <value>
        /// The color of the deep water.
        /// </value>
        public Color DeepWaterColor
        {
            get { return deepWaterColor; }
            set { deepWaterColor = value; }
        }
        /// <summary>
        /// Color of shallow water Default = SkyBlue
        /// </summary>
        /// <value>
        /// The color of the shallow water.
        /// </value>
        public Color ShallowWaterColor
        {
            get { return shallowWaterColor; }
            set { shallowWaterColor = value; }
        }
        /// <summary>
        /// Default = White
        /// </summary>
        /// <value>
        /// The color of the reflection.
        /// </value>
        public Color ReflectionColor
        {
            get { return reflectionColor; }
            set { reflectionColor = value; }
        }
        /// <summary>
        /// Min = 0.0 Max = 2.0 Default = .5
        /// </summary>
        /// <value>
        /// The reflection amount.
        /// </value>
        public float ReflectionAmount
        {
            get { return reflectionAmount; }
            set { reflectionAmount = value; }
        }
        /// <summary>
        /// Amount of water color to use.
        /// Min = 0 Max = 2 Default = 0;
        /// </summary>
        /// <value>
        /// The water amount.
        /// </value>
        public float WaterAmount
        {
            get { return waterAmount; }
            set { waterAmount = value; }
        }
        /// <summary>
        /// Min = 0.0 Max = 10 Defatult = 0.5
        /// </summary>
        /// <value>
        /// The wave amplitude.
        /// </value>
        public float WaveAmplitude
        {
            get { return waveAmplitude; }
            set { waveAmplitude = value; }
        }
        /// <summary>
        /// Min = 0 Max = 1 Default .1
        /// </summary>
        /// <value>
        /// The wave frequency.
        /// </value>
        public float WaveFrequency
        {
            get { return waveFrequency; }
            set { waveFrequency = value; }
        }


        /// <summary>
        /// Set Default Parameters
        /// </summary>
        public void SetDefault()
        {
            bumpHeight = 0.5f;
            textureScale = new Vector2(4, 4);
            bumpSpeed = new Vector2(0, .05f);
            fresnelBias = .025f;
            fresnelPower = 1.0f;
            hdrMultiplier = 1.0f;
            deepWaterColor = Color.Black;
            shallowWaterColor = Color.SkyBlue;
            reflectionColor = Color.White;
            reflectionAmount = 0.5f;
            waterAmount = 0f;
            waveAmplitude = 0.5f;
            waveFrequency = 0.1f;
        }



        public override void  Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<Light.ILight> lights)
        {
            
            Matrix World = obj.WorldMatrix;

            Matrix WVP = World * cam.ViewProjection;
            Matrix WV = World * cam.View;
            Matrix viewI = Matrix.Invert(cam.View);

            render.SetSamplerState(SamplerState.LinearWrap, 0);
            
            effect.Parameters["matWorldViewProj"].SetValue(WVP);
            effect.Parameters["matWorld"].SetValue(World);
            effect.Parameters["matWorldView"].SetValue(WV);
            effect.Parameters["matViewI"].SetValue(viewI);
            effect.Parameters["id"].SetValue(shaderId);
            effect.Parameters["fBumpHeight"].SetValue(bumpHeight);
            effect.Parameters["vTextureScale"].SetValue(textureScale);
            effect.Parameters["vBumpSpeed"].SetValue(bumpSpeed);
            effect.Parameters["fFresnelBias"].SetValue(fresnelBias);
            effect.Parameters["fFresnelPower"].SetValue(fresnelPower);
            effect.Parameters["fHDRMultiplier"].SetValue(hdrMultiplier);
            effect.Parameters["vDeepColor"].SetValue(deepWaterColor.ToVector4());
            effect.Parameters["vShallowColor"].SetValue(shallowWaterColor.ToVector4());
            effect.Parameters["vReflectionColor"].SetValue(reflectionColor.ToVector4());
            effect.Parameters["fReflectionAmount"].SetValue(reflectionAmount);
            effect.Parameters["fWaterAmount"].SetValue(waterAmount);
            effect.Parameters["fWaveAmp"].SetValue(waveAmplitude);
            effect.Parameters["fWaveFreq"].SetValue(waveFrequency);
            effect.Parameters["fTime"].SetValue((float)gt.TotalGameTime.TotalSeconds);

            
            BatchInformation[] bi = obj.Modelo.GetBatchInformation(0);                
            render.RenderBatch(bi[0], effect);                            

        }        
        
        public override void  Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
        {
            base.Initialize(ginfo, factory, obj);
            effect = factory.GetEffect("Water",true,true);        
            effect.Parameters["tNormalMap"].SetValue(factory.GetTexture2D("waves2",true));
            if (String.IsNullOrEmpty(TexureName) && environmentTexture == null)
            {
                ActiveLogger.LogMessage("WaterModel: TextCubeName cannot be null/empty", LogLevel.FatalError);
                throw new Exception("WaterModel: TextCubeName cannot be null/empty");
            }
            if (environmentTexture != null)
            {
                effect.Parameters["tEnvMap"].SetValue(environmentTexture);
            }
            else
            {
                environmentTexture = factory.GetTextureCube(TexureName);
                effect.Parameters["tEnvMap"].SetValue(environmentTexture);
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
            get { return MaterialType.DEFERRED; }
        }

    }

    

}
