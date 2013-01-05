/*
 * SkinnedModelBasicEffect.cs
 * Author: Bruno Evangelista
 * Copyright (c) 2008 Bruno Evangelista. All rights reserved.
 *
 * THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace XNAnimation.Effects
{
    
    /// <summary>
    /// Represents an effect that supports skinning, normal, specular and diffuse textures, 
    /// and phong lighting with multiple point light sources. This effect can use shader model 2.0
    /// or 3.0, according to the number of lights enabled.
    /// </summary>
    public class SkinnedModelBasicEffect : Effect
    {
        public static int MaxSupportedBones = 80;        

        // Cached parameters
        private Matrix viewMatrix;
        private Matrix projectionMatrix;

        // Effect parameters - Matrices
        private EffectParameter bonesParam;
        private EffectParameter worldParam;
        private EffectParameter viewInverseParam;
        private EffectParameter viewProjectionParam;

        // Configurations
        private EffectParameter diffuseMapEnabledParam;
        private EffectParameter specularMapEnabledParam;
        private EffectParameter glowMapEnabledParam;
        private EffectParameter normalMapEnabled;       

        private EffectParameter diffuseMapParam;
        private EffectParameter normalMapParam;
        private EffectParameter specularMapParam;
        private EffectParameter glowMapParam;

        #region Properties

        public Matrix World
        {
            get { return worldParam.GetValueMatrix(); }
            set { worldParam.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the bone matrices of the skeleton.
        /// </summary>
        public Matrix[] Bones
        {
            get { return bonesParam.GetValueMatrixArray(MaxSupportedBones); }
            set { bonesParam.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets the camera view matrix.
        /// </summary>
        public Matrix View
        {
            get { return viewMatrix; }
            set
            {
                viewMatrix = value;
                viewInverseParam.SetValue(Matrix.Invert(value));

                viewProjectionParam.SetValue(viewMatrix * projectionMatrix);
            }
        }

        /// <summary>
        /// Gets or sets the camera projection matrix.
        /// </summary>
        public Matrix Projection
        {
            get { return projectionMatrix; }
            set
            {
                projectionMatrix = value;
                viewProjectionParam.SetValue(viewMatrix * projectionMatrix);
            }
        }

        /// <summary>
        /// Enables diffuse texture.
        /// </summary>
        public bool DiffuseMapEnabled
        {
            get { return diffuseMapEnabledParam.GetValueBoolean(); }
            set { diffuseMapEnabledParam.SetValue(value); }
        }

        /// <summary>
        /// Enables diffuse texture.
        /// </summary>
        public bool GlowMapEnabled
        {
            get { return glowMapEnabledParam.GetValueBoolean(); }
            set { glowMapEnabledParam.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets diffuse texture.
        /// </summary>
        public Texture2D DiffuseMap
        {
            get { return diffuseMapParam.GetValueTexture2D(); }
            set { diffuseMapParam.SetValue(value); }
        }
        
        public Texture2D GlowMap
        {
            get { return glowMapParam.GetValueTexture2D(); }
            set { glowMapParam.SetValue(value); }
        }

        public bool SpecularMapEnabled
        {
            get { return specularMapEnabledParam.GetValueBoolean(); }
            set { specularMapEnabledParam.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets normal map texture.
        /// </summary>
        public Texture2D SpecularMap
        {
            get { return specularMapParam.GetValueTexture2D(); }
            set { specularMapParam.SetValue(value); }
        }

        /// <summary>
        /// Enables normal map texture.
        /// </summary>
        public bool NormalMapEnabled
        {
            get { return normalMapEnabled.GetValueBoolean(); }
            set { normalMapEnabled.SetValue(value); }
        }

        /// <summary>
        /// Gets or sets normal map texture.
        /// </summary>
        public Texture2D NormalMap
        {
            get { return normalMapParam.GetValueTexture2D(); }
            set { normalMapParam.SetValue(value); }
        }        

        #endregion

        /// <summary>Initializes a new instance of the 
        /// <see cref="T:XNAnimation.Effects.SkinnedModelBasicEffect" />
        /// class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device that will create the effect.</param>
        /// <param name="clone">The effect to clone.</param>
        protected SkinnedModelBasicEffect(GraphicsDevice graphicsDevice, SkinnedModelBasicEffect clone)
            : base(clone)
        {
            CacheEffectParams();
        }

        public SkinnedModelBasicEffect(GraphicsDevice graphicsDevice,byte[] effectCode)
            : base(graphicsDevice, effectCode)
        {
            CacheEffectParams();
        }
        

        private void CacheEffectParams()
        {
            // Matrices
            worldParam = Parameters["matW"];
            viewInverseParam = Parameters["matVI"];
            viewProjectionParam = Parameters["matVP"];
            bonesParam = Parameters["matBones"];

            // Configurations
            diffuseMapEnabledParam = Parameters["diffuseMapEnabled"];
            specularMapEnabledParam = Parameters["specularMapEnabled"];
            normalMapEnabled = Parameters["normalMapEnabled"];
            glowMapEnabledParam = Parameters["glowMapEnabled"];
            
            diffuseMapParam = Parameters["diffuseMap0"];
            normalMapParam = Parameters["normalMap0"];
            specularMapParam = Parameters["specularMap0"];
            glowMapParam = Parameters["glowMap0"];

        }

        private void InitializeEffectParams()
        {
            World = Matrix.Identity;
            View = Matrix.Identity;
            Projection = Matrix.Identity;
            for (int i = 0; i < MaxSupportedBones; i++)
                bonesParam.Elements[i].SetValue(Matrix.Identity);            
        }
        
        #if !WINDOWS_PHONE
        internal static SkinnedModelBasicEffect Read(ContentReader input)
        {
            IGraphicsDeviceService graphicsDeviceService = (IGraphicsDeviceService)
                    input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService));

            GraphicsDevice graphicsDevice = graphicsDeviceService.GraphicsDevice;

            //ResourceContentManager cnt = new ResourceContentManager(input.ContentManager.ServiceProvider, Resource1.ResourceManager);            z
#if WINDOWS           
            SkinnedModelBasicEffect basicEffect = new SkinnedModelBasicEffect(graphicsDevice, Resource1.SkinnedModelEffect);
#else
            SkinnedModelBasicEffect basicEffect = new SkinnedModelBasicEffect(graphicsDevice, Resource1.SkinnedModelEffect2);
#endif
            

            input.ReadVector3();
            input.ReadVector3();
            input.ReadVector3();
            input.ReadSingle();

            basicEffect.DiffuseMapEnabled = input.ReadBoolean();
            basicEffect.NormalMapEnabled = input.ReadBoolean();
            basicEffect.SpecularMapEnabled = input.ReadBoolean();
            basicEffect.DiffuseMap = input.ReadObject<Texture2D>(); ;
            basicEffect.NormalMap = input.ReadObject<Texture2D>(); ;
            basicEffect.SpecularMap = input.ReadObject<Texture2D>(); ;            

            return basicEffect;
        }
#else
        internal static SkinnedEffect Read(ContentReader input)
        {
            IGraphicsDeviceService graphicsDeviceService = (IGraphicsDeviceService)
                    input.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService));

            GraphicsDevice graphicsDevice = graphicsDeviceService.GraphicsDevice;

            SkinnedEffect basicEffect = new SkinnedEffect(graphicsDevice);


            input.ReadVector3();
            input.ReadVector3();
            input.ReadVector3();
            input.ReadSingle();

            input.ReadBoolean();
            input.ReadBoolean();
            input.ReadBoolean();
            input.ReadObject<Texture2D>(); 
            input.ReadObject<Texture2D>(); 
            input.ReadObject<Texture2D>(); 

            return basicEffect;
        }
#endif
    }
}