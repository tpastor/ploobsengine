using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using PloobsEngine.Physics;
using SkinnedModel;
using Microsoft.Xna.Framework;

namespace PloobsProjectTemplate
{
    public class SkinnedIObject : IObject
    {
        public SkinnedIObject(GraphicFactory factory,String modelName, IPhysicObject phyobj)         
        {
            this.Modelo = SkinnedModel = new SkinnedModel(factory,modelName);
            ForwardSkinnedShader = new ForwardSkinnedShader();
            this.Material = new ForwardMaterial(ForwardSkinnedShader);
            this.PhysicObject = phyobj;
            IObjectAttachment = new List<IObjectAttachment>();
            Name = modelName;            
        }


        public ForwardSkinnedShader ForwardSkinnedShader
        {
            get;
            private set;
        }
        public SkinnedModel SkinnedModel
        {
            get;
            private set;
        }

        float headRotation = 0;
        float step = 0.01f;
        protected override void UpdateObject(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Cameras.ICamera cam, IWorld world)
        {
            // Tell the animation player to compute the latest bone transform matrices.
            SkinnedModel.AnimationPlayer.UpdateBoneTransforms(gt.ElapsedGameTime, true);

            Matrix[] boneTransforms = new Matrix[SkinnedModel.SkinningData.BindPose.Count];
            // Copy the transforms into our own array, so we can safely modify the values.
            SkinnedModel.AnimationPlayer.GetBoneTransforms().CopyTo(boneTransforms, 0);

            // Modify the transform matrices for the head and upper-left arm bones.
            int headIndex = SkinnedModel.SkinningData.BoneIndices["Head"];

            ///some bone manipulation            
            headRotation += step;
            if (headRotation > 0.7f || headRotation < -0.7f)
                step *= -1;            

            Matrix headTransform = Matrix.CreateRotationX(headRotation);            
            boneTransforms[headIndex] = headTransform * boneTransforms[headIndex];

            // Tell the animation player to recompute the world and skin matrices.
            SkinnedModel.AnimationPlayer.UpdateWorldTransforms(Matrix.Identity, boneTransforms);
            SkinnedModel.AnimationPlayer.UpdateSkinTransforms();

            ForwardSkinnedShader.SetBones(SkinnedModel.AnimationPlayer.GetSkinTransforms());
            base.UpdateObject(gt, cam, world);
        }
    }
}
