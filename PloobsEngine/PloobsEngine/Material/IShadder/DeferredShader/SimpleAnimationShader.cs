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
    public class SimpleAnimationShader : IShader
    {
        /// <summary>
        /// Serialization
        /// </summary>
        internal SimpleAnimationShader()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAnimationShader"/> class.
        /// </summary>
        /// <param name="ac">The ac.</param>
        public SimpleAnimationShader(IAnimatedController ac)
        {
            this.ac = ac; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAnimationShader"/> class.
        /// THIS IS A SPECIAL CONSTRUCTOR, it is used to ATACH this shader to a external BONE
        /// Can be used for example to put a gun in the hand of a character
        /// </summary>
        /// <param name="ac">The ac.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="Followed">The followed.</param>
        /// <param name="boneName">Name of the bone.</param>
        public SimpleAnimationShader(IAnimatedController ac,IObject obj, IAnimatedController Followed,String boneName)
        {
            this.ac = ac;            
            this.Followed = Followed;
            followBone = true;
            this.boneName = boneName;
            this.Followobj = obj;       

        }       

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
            get { return MaterialType.DEFERRED; }
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

        public override void  Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<Light.ILight> lights)        
 	    {
            AnimatedModel modelo = obj.Modelo as AnimatedModel;
            foreach (ModelMesh modelMesh in modelo.GetAnimatedModel().Meshes)
            {
                foreach (ModelMeshPart meshPart in modelMesh.MeshParts)
                {
                    SkinnedModelBasicEffect basicEffect = (SkinnedModelBasicEffect)meshPart.Effect;
                    basicEffect.DiffuseMapEnabled = true;
                    basicEffect.CurrentTechnique = basicEffect.Techniques["DEFERRED"];
                    basicEffect.Parameters["diffuseMap0"].SetValue(modelo.getTexture(TextureType.DIFFUSE));
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
                    basicEffect.View = cam.View;
                    basicEffect.Projection = cam.Projection;
                }

                modelMesh.Draw();
            }
        }
    }

    
}
