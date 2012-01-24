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
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Modelo.Animation;
using PloobsEngine.Light;
using XNAnimation.Effects;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Animation Shader that supports advanced effects
    /// </summary>
    public class DeferredCustomAnimationShader : IShader
    {
        /// <summary>
        /// Serialization
        /// </summary>
        internal DeferredCustomAnimationShader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredCustomAnimationShader"/> class.
        /// </summary>
        /// <param name="ac">The ac.</param>
        /// <param name="useBump">if set to <c>true</c> [use bump].</param>
        /// <param name="useSpecular">if set to <c>true</c> [use specular].</param>
        /// <param name="useGlow">if set to <c>true</c> [use glow].</param>
        public DeferredCustomAnimationShader(IAnimatedController ac , bool useBump, bool useSpecular, bool useGlow)
        {
            this.ac = ac;
            this.useBump = useBump;
            this.useSpecular = useSpecular;
            this.useGlow = useGlow;
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredCustomAnimationShader"/> class.
        /// </summary>
        /// <param name="ac">The ac.</param>
        /// <param name="useBump">if set to <c>true</c> [use bump].</param>
        /// <param name="useSpecular">if set to <c>true</c> [use specular].</param>
        /// <param name="useGlow">if set to <c>true</c> [use glow].</param>
        /// <param name="obj">The obj.</param>
        /// <param name="Followed">The followed.</param>
        /// <param name="boneName">Name of the bone.</param>
        public DeferredCustomAnimationShader(IAnimatedController ac, bool useBump, bool useSpecular, bool useGlow, IObject obj, IAnimatedController Followed,String boneName)
        {
            this.ac = ac;
            this.useBump = useBump;
            this.useSpecular = useSpecular;
            this.useGlow = useGlow;
            this.Followed = Followed;
            followBone = true;
            this.boneName = boneName;
            this.Followobj = obj;            
        }

        IObject Followobj;
        String boneName;
        IAnimatedController Followed;
        bool followBone = false;
        private bool useGlow;        
        private bool useBump;
        private bool useSpecular;

        private IAnimatedController ac;
        private Matrix WorldMatrix;

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

        public override void  Update(GameTime gt, IObject ent, IList<Light.ILight> lights)
        {
 	        base.Update(gt, ent, lights);
            this.WorldMatrix = ent.WorldMatrix;         
        }

        public override void DepthExtractor(GameTime gt, IObject obj, Matrix View, Matrix projection, RenderHelper render)
        {
            AnimatedModel modelo = obj.Modelo as AnimatedModel;
            foreach (ModelMesh modelMesh in modelo.GetAnimatedModel().Meshes)
            {
                foreach (ModelMeshPart meshPart in modelMesh.MeshParts)
                {
                    SkinnedModelBasicEffect basicEffect = (SkinnedModelBasicEffect)meshPart.Effect;                 
                    basicEffect.CurrentTechnique = basicEffect.Techniques["DEPTH"];
                    if (followBone)
                    {
                        basicEffect.World = Followed.GetBoneAbsoluteTransform(boneName) * Followobj.WorldMatrix;
                        basicEffect.Bones = modelo.getBonesTransformation();
                    }
                    else
                    {
                        basicEffect.World = WorldMatrix;
                        basicEffect.Bones = ac.GetBoneTransformations();
                    }
                    basicEffect.View = View;
                    basicEffect.Projection = projection;
                }

                modelMesh.Draw();
            }

        }

        public override void BasicDraw(GameTime gt, IObject obj, Matrix view, Matrix projection, IList<Light.ILight> lights, RenderHelper render, Plane? clippingPlane, bool useAlphaBlending = false)
        {
            AnimatedModel modelo = obj.Modelo as AnimatedModel;

            for (int i = 0; i < modelo.GetAnimatedModel().Meshes.Count; i++)			
            {
                ModelMesh modelMesh = modelo.GetAnimatedModel().Meshes[i];
                for (int j = 0; j < modelMesh.MeshParts.Count; j++)
			    {
                    SkinnedModelBasicEffect basicEffect = (SkinnedModelBasicEffect)modelMesh.MeshParts[j].Effect;
                    if (clippingPlane != null)
                    {
                        basicEffect.Parameters["clipenabled"].SetValue(true);
                        basicEffect.Parameters["plane"].SetValue(new Vector4(clippingPlane.Value.Normal, clippingPlane.Value.D));
                    }
                    else
                    {
                        basicEffect.Parameters["clipenabled"].SetValue(false);
                    }
                    basicEffect.DiffuseMapEnabled = true;
                    basicEffect.CurrentTechnique = basicEffect.Techniques["FORWARDCLIP"];
                    basicEffect.Parameters["diffuseMap0"].SetValue(modelo.getTexture(TextureType.DIFFUSE,i,j));
                    basicEffect.Parameters["diffuseMapEnabled"].SetValue(true);
                    if (followBone)
                    {
                        basicEffect.World = Followed.GetBoneAbsoluteTransform(boneName) * Followobj.WorldMatrix;
                        basicEffect.Bones = modelo.getBonesTransformation();
                    }
                    else
                    {
                        basicEffect.World = WorldMatrix;
                        basicEffect.Bones = ac.GetBoneTransformations();
                    }
                    basicEffect.View = view;
                    basicEffect.Projection = projection;
                }

                modelMesh.Draw();
            }
        }


        public override void  Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<Light.ILight> lights)
 	    {
            AnimatedModel modelo = obj.Modelo as AnimatedModel;
            for (int i = 0; i < modelo.GetAnimatedModel().Meshes.Count; i++)			
            {
                ModelMesh modelMesh = modelo.GetAnimatedModel().Meshes[i];
                for (int j = 0; j < modelMesh.MeshParts.Count; j++)
                {
                    SkinnedModelBasicEffect basicEffect = (SkinnedModelBasicEffect)modelMesh.MeshParts[j].Effect;                    
                    basicEffect.CurrentTechnique = basicEffect.Techniques["DEFERREDCUSTOM"];

                    basicEffect.Parameters["diffuseMap0"].SetValue(modelo.getTexture(TextureType.DIFFUSE,i,j));
                    basicEffect.Parameters["diffuseMapEnabled"].SetValue(true);
                    basicEffect.Parameters["normalMapEnabled"].SetValue(useBump);
                    basicEffect.Parameters["glowMapEnabled"].SetValue(useGlow);
                    basicEffect.Parameters["specularMapEnabled"].SetValue(useSpecular);

                    if (useGlow)
                    {
                        basicEffect.Parameters["glowMap0"].SetValue(modelo.getTexture(TextureType.GLOW, i, j));
                    }
                    if (useBump)
                    {
                        basicEffect.Parameters["normalMap0"].SetValue(modelo.getTexture(TextureType.BUMP, i, j));
                    }
                    if (useSpecular)
                    {
                        basicEffect.Parameters["specularMap0"].SetValue(modelo.getTexture(TextureType.SPECULAR, i, j));
                    }

                    if (followBone)
                    {
                        basicEffect.World = Followed.GetBoneAbsoluteTransform(boneName) * Followobj.WorldMatrix;
                        basicEffect.Bones = modelo.getBonesTransformation() ;
                    }
                    else
                    {
                        basicEffect.World = WorldMatrix;
                        basicEffect.Bones = ac.GetBoneTransformations();
                    }                    
                    
                    basicEffect.View = cam.View;
                    basicEffect.Projection = cam.Projection;
                }

                modelMesh.Draw();
            }


        }
    }
}
#endif