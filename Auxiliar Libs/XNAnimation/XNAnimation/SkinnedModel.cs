/*
 * SkinnedModel.cs
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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNAnimation
{
    public class SkinnedModel
    {
        private Model model;
        private SkinnedModelBoneCollection skeleton;
        private AnimationClipDictionary animationClips;

        // TODO Add an Tag object

        #region Properties

        public Model Model
        {
            get { return model; }
        }

        public SkinnedModelBoneCollection SkeletonBones
        {
            get { return skeleton; }
        }

        public AnimationClipDictionary AnimationClips
        {
            get { return animationClips; }
        }

        #endregion

        internal SkinnedModel()
        {
        }

        /*
        public void Update(TimeSpan elapsedTime, Matrix parentBone)
        {
            // TODO Add parent bone

            rootBone.UpdateHierarchy(ref skinnedBones);
            for (int i = 0; i < skinnedBones.Length; i++)
            {
                skinnedBones[i] = inverseBindPose[i] * skinnedBones[i];
            }

            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in modelMesh.MeshParts)
                {
                    SkinnedModelBasicEffect effect = (SkinnedModelBasicEffect) meshPart.Effect;
                    effect.SkeletonBones = skinnedBones;
                }
            }
        }
        */

        public void Draw(TimeSpan elapsedTime)
        {
            /*
            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in modelMesh.MeshParts)
                {

                }
            }
             */
        }

        internal static SkinnedModel Read(ContentReader input)
        {
            SkinnedModel skinnedModel = new SkinnedModel();

            skinnedModel.model = input.ReadObject<Model>();
            skinnedModel.ReadBones(input);
            skinnedModel.ReadAnimations(input);
            return skinnedModel;
        }

        private void ReadBones(ContentReader input)
        {
            int numSkeletonBones = input.ReadInt32();
            List<SkinnedModelBone> skinnedModelBoneList = new List<SkinnedModelBone>(numSkeletonBones);

            // Read all bones
            for (int i = 0; i < numSkeletonBones; i++)
            {
                input.ReadSharedResource<SkinnedModelBone>(
                    delegate(SkinnedModelBone skinnedBone) { skinnedModelBoneList.Add(skinnedBone); });
            }

            // Create the skeleton
            skeleton = new SkinnedModelBoneCollection(skinnedModelBoneList);

            //foreach (SkinnedModelBoneContent bone in skeleton)
            //output.WriteSharedResource(bone);

            /*
            ///
            /// TODO I can optimize this using write shared resource for the skeleton
            /// 
            string rootBoneName = input.ReadString();
            int numBones = input.ReadInt32();

            SkinnedModelBone[] tempBonesArray = new SkinnedModelBone[numBones];
            Dictionary<string, SkinnedModelBone> boneDictionary =
                new Dictionary<string, SkinnedModelBone>();

            // Read all boneDictionary
            for (int i = 0; i < numBones; i++)
            {
                string boneName = input.ReadString();
                Matrix boneTransform = input.ReadMatrix();
                Matrix boneAbsoluteTransform = input.ReadMatrix();
                Matrix boneBindPoseTransform = input.ReadMatrix();

                SkinnedModelBone bone =
                    new SkinnedModelBone(boneName, boneTransform, boneAbsoluteTransform,
                        boneBindPoseTransform);

                tempBonesArray[i] = bone;
                boneDictionary.Add(boneName, bone);
            }

            // Read the boneDictionary parent and children
            for (int i = 0; i < numBones; i++)
            {
                // Find parent bone
                SkinnedModelBone parentBone = null;
                string parentBoneName = input.ReadObject<string>();
                if (parentBoneName != null)
                    parentBone = boneDictionary[parentBoneName];

                tempBonesArray[i].Parent = parentBone;

                // Find children
                int numChildren = input.ReadInt32();
                List<SkinnedModelBone> childrenList = new List<SkinnedModelBone>(numChildren);
                for (int j = 0; j < numChildren; j++)
                {
                    string childName = input.ReadString();
                    childrenList.Add(boneDictionary[childName]);
                }

                tempBonesArray[i].Children = new SkinnedModelBoneCollection(childrenList);
            }

            // Store everything
            tempBonesArray = null;

            rootBone = boneDictionary[rootBoneName];
            this.boneDictionary = new SkinnedModelBoneDictionary(boneDictionary);
            skinnedBones = new Matrix[numBones];

            inverseBindPose = new Matrix[numBones];
            rootBone.GetHierarchyInverseBindPoseTransfom(ref inverseBindPose);
             */
        }

        private void ReadAnimations(ContentReader input)
        {
            int numAnimationClips = input.ReadInt32();
            Dictionary<string, AnimationClip> animationClipDictionary =
                new Dictionary<string, AnimationClip>();

            // Read all animation clips
            for (int i = 0; i < numAnimationClips; i++)
            {
                input.ReadSharedResource<AnimationClip>(
                    delegate(AnimationClip animationClip) { animationClipDictionary.Add(animationClip.Name, animationClip); });
            }

            animationClips = new AnimationClipDictionary(animationClipDictionary);

            /*
            Dictionary<string, AnimationClip> trackDictionary =
                new Dictionary<string, AnimationClip>();

            int numAnimations = input.ReadInt32();
            for (int i = 0; i < numAnimations; i++)
            {
                Dictionary<string, AnimationChannel> channelDictionary =
                    new Dictionary<string, AnimationChannel>();

                string trackName = input.ReadString();
                TimeSpan trackDuration = input.ReadObject<TimeSpan>();

                int numChannels = input.ReadInt32();
                for (int j = 0; j < numChannels; j++)
                {
                    string channelName = input.ReadString();

                    int numKeyframes = input.ReadInt32();
                    List<AnimationChannelKeyframe> keyframeList = new List<AnimationChannelKeyframe>(numKeyframes);
                    for (int k = 0; k < numKeyframes; k++)
                    {
                        TimeSpan keyframeTime = input.ReadObject<TimeSpan>();
                        Matrix keyframeTransform = input.ReadMatrix();

                        keyframeList.Add(new AnimationChannelKeyframe(keyframeTime, keyframeTransform));
                    }

                    channelDictionary.Add(channelName, new AnimationChannel(keyframeList));
                }

                AnimationClip animationClip =
                    new AnimationClip(trackName, new AnimationChannelDictionary(channelDictionary),
                        trackDuration);
                trackDictionary.Add(trackName, animationClip);
            }

            animationClips = new AnimationClipDictionary(trackDictionary);
             */
        }
    }
}