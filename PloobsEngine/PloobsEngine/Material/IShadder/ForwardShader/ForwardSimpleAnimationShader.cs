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
    /// Animation shader with simple effects
    /// </summary>
    public class ForwardSimpleAnimationShader : IShader
    {

#if WINDOWS_PHONE || REACH        
                
        /// <summary>
        /// CAN BE ONLY CALLED AFTER Adding the IOBJECT TO THE WORLD
        /// </summary>
        public SkinnedEffect SkinnedEffect
        {
            get;
            set;
        }

        public bool EnableTexture
        {
            get;
            set;
        }
        
#endif

        /// <summary>
        /// Serialization
        /// </summary>
        internal ForwardSimpleAnimationShader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardSimpleAnimationShader"/> class.
        /// </summary>
        /// <param name="ac">The ac.</param>
        public ForwardSimpleAnimationShader(IAnimatedController ac)
        {
            this.ac = ac;
#if WINDOWS_PHONE || REACH
            EnableTexture = true;
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardSimpleAnimationShader"/> class.
        /// THIS IS A SPECIAL CONSTRUCTOR, it is used to ATACH this shader to a external BONE
        /// Can be used for example to put a gun in the hand of a character
        /// </summary>
        /// <param name="ac">The ac.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="Followed">The followed.</param>
        /// <param name="boneName">Name of the bone.</param>
        public ForwardSimpleAnimationShader(IAnimatedController ac, IObject obj, IAnimatedController Followed, String boneName)
        {
            this.ac = ac;
            this.Followed = Followed;
            followBone = true;
            this.boneName = boneName;
            this.Followobj = obj;
#if WINDOWS_PHONE || REACH
            EnableTexture = true;
#endif
        }

        public override void Initialize(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {
            base.Initialize(ginfo, factory, obj);

#if WINDOWS_PHONE || REACH
             SkinnedEffect = factory.GetSkinnedEffect();
             SkinnedEffect.EnableDefaultLighting();
#endif
        }

#if WINDOWS_PHONE || REACH
         IObject ent = null;
         public override void PreUpdate(IObject ent, IList<Light.ILight> lights)
         {
             this.ent = ent;
             base.PreUpdate(ent, lights);

             AnimatedModel modelo = ent.Modelo as AnimatedModel;

             for (int i = 0; i < modelo.GetAnimatedModel().Meshes.Count; i++)
             {
                 ModelMesh modelMesh = modelo.GetAnimatedModel().Meshes[i];
                 for (int j = 0; j < modelMesh.MeshParts.Count; j++)
                 {
                     modelMesh.MeshParts[j].Effect = SkinnedEffect;
                 }
             }
         }
#endif


        IObject Followobj;
        String boneName;
        IAnimatedController Followed;
        bool followBone = false;
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
            get { return MaterialType.FORWARD; }
        }

        /// <summary>
        /// Updates this shader
        /// Called every frame once
        /// </summary>
        /// <param name="gt"></param>
        /// <param name="ent">The ent.</param>
        /// <param name="lights">The lights.</param>
        public override void Update(GameTime gt, IObject ent, IList<Light.ILight> lights)
        {
            base.Update(gt, ent, lights);
            this.WorldMatrix = ent.WorldMatrix;
        }


#if WINDOWS_PHONE || REACH
        public override void  Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<Light.ILight> lights)        
 	    {
            AnimatedModel modelo = obj.Modelo as AnimatedModel;

            for (int i = 0; i < modelo.GetAnimatedModel().Meshes.Count; i++)
            {
                ModelMesh modelMesh = modelo.GetAnimatedModel().Meshes[i];
                for (int j = 0; j < modelMesh.MeshParts.Count; j++)
                {
                    SkinnedEffect basicEffect = (SkinnedEffect) modelMesh.MeshParts[j].Effect;

                    if (EnableTexture)
                        basicEffect.Texture = modelo.getTexture(TextureType.DIFFUSE, i, j);
                    
                    if (followBone)
                    {
                        basicEffect.World = Followed.GetBoneAbsoluteTransform(boneName) * Followobj.WorldMatrix;
                        basicEffect.SetBoneTransforms(modelo.getBonesTransformation());
                    }
                    else
                    {
                        basicEffect.World = WorldMatrix;
                        basicEffect.SetBoneTransforms(ac.GetBoneTransformations());
                    }
                    basicEffect.View = cam.View;
                    basicEffect.Projection = cam.Projection;
                }
                modelMesh.Draw();
            }
        }
#else
        public override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<Light.ILight> lights)
        {
            AnimatedModel modelo = obj.Modelo as AnimatedModel;

            for (int i = 0; i < modelo.GetAnimatedModel().Meshes.Count; i++)
            {
                ModelMesh modelMesh = modelo.GetAnimatedModel().Meshes[i];
                for (int j = 0; j < modelMesh.MeshParts.Count; j++)
                {
                    SkinnedModelBasicEffect basicEffect = (SkinnedModelBasicEffect)modelMesh.MeshParts[j].Effect;
                    basicEffect.CurrentTechnique = basicEffect.Techniques["FORWARD"];
                    basicEffect.Parameters["diffuseMap0"].SetValue(modelo.getTexture(TextureType.DIFFUSE, i, j));
                    basicEffect.Parameters["diffuseMapEnabled"].SetValue(true);
                    if (followBone)
                    {
                        basicEffect.World = Followed.GetBoneAbsoluteTransform(boneName) * Followobj.WorldMatrix;
                        basicEffect.Bones = ac.GetBoneTransformations();
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
#endif
    }
}


    

