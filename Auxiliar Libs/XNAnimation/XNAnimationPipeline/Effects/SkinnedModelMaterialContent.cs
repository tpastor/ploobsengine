/*
 * SkinnedModelMaterialContent.cs
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace XNAnimationPipeline.Effects
{
    public class SkinnedModelMaterialContent : MaterialContent
    {
        /*
        // Cached parameters
        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private bool lightEnabled;

        // Effect parameters - Matrices
        private EffectParameter worldParam;
        private EffectParameter viewInverseParam;
        private EffectParameter viewProjectionParam;
        private EffectParameter bonesParam;

        // Configurations
        private EffectParameter diffuseTextureEnabledParam;

        // Material
        private Material material;
        private EffectParameter diffuseTextureParam;

        // Lights
        private EffectParameter ambientLightColorParam;
        private EnabledLights enabledLights;
        private PointLightCollection pointLightCollection;
        
        private 
        */
        //private bool diffuseMapEnabled;
        //private MaterialContent material;

        private Vector3 emissiveColor;
        private Vector3 diffuseColor;
        private Vector3 specularColor;
        private float specularPower;

        private bool diffuseMapEnabled;
        private bool normalMapEnabled;
        private bool specularMapEnabled;

        private ExternalReference<TextureContent> diffuseMapContent;
        private ExternalReference<TextureContent> normalMapContent;
        private ExternalReference<TextureContent> specularMapContent;

        public Vector3 EmissiveColor
        {
            get { return emissiveColor; }
            set { emissiveColor = value; }
        }

        public Vector3 DiffuseColor
        {
            get { return diffuseColor; }
            set { diffuseColor = value; }
        }

        public Vector3 SpecularColor
        {
            get { return specularColor; }
            set { specularColor = value; }
        }

        public float SpecularPower
        {
            get { return specularPower; }
            set { specularPower = value; }
        }

        public bool DiffuseMapEnabled
        {
            get { return diffuseMapEnabled; }
            set { diffuseMapEnabled = value; }
        }

        public bool NormalMapEnabled
        {
            get { return normalMapEnabled; }
            set { normalMapEnabled = value; }
        }

        public bool SpecularMapEnabled
        {
            get { return specularMapEnabled; }
            set { specularMapEnabled = value; }
        }

        public ExternalReference<TextureContent> DiffuseMapContent
        {
            get { return diffuseMapContent; }
            set { diffuseMapContent = value; }
        }

        public ExternalReference<TextureContent> NormalMapContent
        {
            get { return normalMapContent; }
            set { normalMapContent = value; }
        }

        public ExternalReference<TextureContent> SpecularMapContent
        {
            get { return specularMapContent; }
            set { specularMapContent = value; }
        }

        internal SkinnedModelMaterialContent()
        {
        }
    }
}