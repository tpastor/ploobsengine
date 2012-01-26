/*
 * SkinnedModelMaterialProcessor.cs
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
using System.ComponentModel;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using XNAnimationPipeline.Effects;

namespace XNAnimationPipeline.Pipeline
{
    [ContentProcessor(DisplayName = "Model Material - XNAnimation"), DesignTimeVisible(false)]
    internal class SkinnedModelMaterialProcessor : MaterialProcessor
    {
        #region Content Properties

        private string texturePath = "";
        [DisplayName("Texture Path"),
        Description("If set, changes the path of all texture resources. " +
            "The new path can be an absolute or relative path."),
        DefaultValue(typeof(string), "")]
        public virtual string TexturePath
        {
            get { return texturePath; }
            set { texturePath = value; }
        }

        private PathType texturePathType = PathType.Relative;
        [DisplayName("Texture Path Type"),
        Description("Indicates whether the \"Texture Path\" is an absolute or relative path." +
            "If the path is relative, it is relative to the source asset path."),
        DefaultValue(typeof(PathType), "Relative")]
        public virtual PathType TexturePathType
        {
            get { return texturePathType; }
            set { texturePathType = value; }
        }
        
        #endregion

        private static readonly char[] DirectorySeparatorChars = new char[] { '/', '\\' };
        
        public static readonly string DiffuseMapKey = "Texture";
        public static readonly string NormalMapKey = "Bump0";
        public static readonly string SpecularMapKey = "Specular0";

        public override MaterialContent Process(MaterialContent input, ContentProcessorContext context)
        {
            SkinnedModelMaterialContent skinnedModelMaterialContent = new SkinnedModelMaterialContent();

            // Processes all textures
            ProcessTextures(input, skinnedModelMaterialContent, context);
            
            // Processes surface material
            ProcessMaterial(input, skinnedModelMaterialContent, context);
            
            return skinnedModelMaterialContent;
        }

        private void ProcessTextures(MaterialContent input, SkinnedModelMaterialContent skinnedModelMaterial,
            ContentProcessorContext context)
        {
            foreach (string key in input.Textures.Keys)
            {
                ExternalReference<TextureContent> texture = input.Textures[key];

                if (!String.IsNullOrEmpty(texturePath))
                {
                    string fullFilePath;

                    if (texturePathType == PathType.Relative)
                    {
                        // If relative path
                        string sourceAssetPath = Path.GetDirectoryName(input.Identity.SourceFilename);
                        fullFilePath = Path.GetFullPath(
                            Path.Combine(sourceAssetPath, texturePath));
                    }
                    else
                    {
                        fullFilePath = texturePath;
                    }

                    texture.Filename = Path.Combine(fullFilePath,
                        Path.GetFileName(texture.Filename));
                }

                ProcessTexture(key, texture, skinnedModelMaterial, context);
            }
        }

        protected virtual void ProcessTexture(string key, ExternalReference<TextureContent> texture,
            SkinnedModelMaterialContent skinnedModelMaterial, ContentProcessorContext context)
        {
            if (key.Equals(DiffuseMapKey))
            {
                //skinnedModelMaterial.DiffuseMapEnabled = true;
                //skinnedModelMaterial.DiffuseMapContent = base.BuildTexture(key, texture, context);
            }
            else if (key.Equals(NormalMapKey))
            {
                //skinnedModelMaterial.NormalMapEnabled = true;
                //skinnedModelMaterial.NormalMapContent = base.BuildTexture(key, texture, context);
            }
            else if (key.Equals(SpecularMapKey))
            {
                //skinnedModelMaterial.SpecularMapEnabled = true;
                //skinnedModelMaterial.SpecularMapContent = base.BuildTexture(key, texture, context);
            }
        }

        protected virtual void ProcessMaterial(MaterialContent input, 
            SkinnedModelMaterialContent skinnedModelMaterial, ContentProcessorContext context)
        {
            BasicMaterialContent basicMaterial = input as BasicMaterialContent;
            //if (basicMaterial != null)
            //{
            //    skinnedModelMaterial.EmissiveColor = basicMaterial.EmissiveColor.GetValueOrDefault(
            //        Vector3.Zero);
            //    skinnedModelMaterial.DiffuseColor = basicMaterial.DiffuseColor.GetValueOrDefault(
            //        Vector3.One);
            //    skinnedModelMaterial.SpecularColor = basicMaterial.SpecularColor.GetValueOrDefault(
            //        Vector3.One);
            //    skinnedModelMaterial.SpecularPower = basicMaterial.SpecularPower.GetValueOrDefault(
            //        16);
            //}

        }
    }
}