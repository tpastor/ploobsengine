using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNAnimation;
using XNAnimation.Controllers;
using PloobsEngine.Utils;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Modelo.Animation
{
    /// <summary>
    /// Animated Controller concrete
    /// </summary>
    public class AnimatedController : IAnimatedController
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedController"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="StartAnimationName">Start name of the animation.</param>
        /// <param name="changeOnlyWhenDifferentAnimation">if set to <c>true</c> [change the current animationonly only when different animation is set].</param>
        public AnimatedController(IAnimatedModel model, String StartAnimationName = null,bool changeOnlyWhenDifferentAnimation = true)            
        {
            skinnedModel = model.GetAnimation() as SkinnedModel;
            animationController = new AnimationController(skinnedModel.SkeletonBones);            
            this.StartAnimationName = StartAnimationName;
            this.actualAnimation = StartAnimationName;
            if(StartAnimationName != null)
                animationController.StartClip(skinnedModel.AnimationClips[StartAnimationName]);
            this.changeOnlyWhenDifferentAnimation = changeOnlyWhenDifferentAnimation;

        }

        private bool changeOnlyWhenDifferentAnimation = true;
        private string actualAnimation;
        private SkinnedModel skinnedModel;        
        private AnimationController animationController;
        private String StartAnimationName = null;
        private IDictionary<String, String> _actionAnimation = new Dictionary<string, String>();
        private double transitionBetweenAnimationTimeInSeconds = 1;

        /// <summary>
        /// Gets or sets the transition between animation time in seconds.
        /// </summary>
        /// <value>
        /// The transition between animation time in seconds.
        /// </value>
        public double TransitionBetweenAnimationTimeInSeconds
        {
            get { return transitionBetweenAnimationTimeInSeconds; }
            set { transitionBetweenAnimationTimeInSeconds = value; }
        }

        /// <summary>
        /// Gets or sets the animation speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public float Speed
        {
            get
            {
                return animationController.Speed;
            }
            set
            {
                this.animationController.Speed = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in loop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loop; otherwise, <c>false</c>.
        /// </value>
        public bool isLoop
        {
            get
            {
                return animationController.LoopEnabled;
            }
            set
            {
                this.animationController.LoopEnabled = value;
            }
        }

        /// <summary>
        /// Mapps one action to animation.
        /// When using behaviors for example
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="animation">The animation.</param>
        public void MappActionAnimation(String actionName, String animation)
        {
            _actionAnimation.Add(actionName, animation);
        }


        #region IAnimatedController Members

        /// <summary>
        /// Transforms the bone.
        /// </summary>
        /// <param name="boneName">Name of the bone.</param>
        /// <param name="rot">The rot.</param>
        public void TransformBone(String boneName,Quaternion rot)
        {
            animationController.SetBoneController(boneName, rot);
        }

        /// <summary>
        /// Gets the bone absolute transform.
        /// </summary>
        /// <param name="boneName">Name of the bone.</param>
        /// <returns></returns>
        public Matrix GetBoneAbsoluteTransform(String boneName)
        {
            return animationController.GetBoneAbsoluteTransform(boneName);
        }

        /// <summary>
        /// Changes the animation.
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        /// <param name="mode">The interpolation mode.</param>
        public void ChangeAnimation(string animationName, AnimationChangeMode mode)
        {
            if (changeOnlyWhenDifferentAnimation)
            {
                if (animationName != actualAnimation)
                {
                    animationController.CrossFade(
                            skinnedModel.AnimationClips[animationName],
                            TimeSpan.FromSeconds(transitionBetweenAnimationTimeInSeconds));
                    actualAnimation = animationName;
                }
            }
            else
            {
                animationController.CrossFade(
                     skinnedModel.AnimationClips[animationName],
                     TimeSpan.FromSeconds(transitionBetweenAnimationTimeInSeconds));
                actualAnimation = animationName;

            }            
        }        

        #endregion

        

        #region IAnimatedController Members


        /// <summary>
        /// Updates the controller.
        /// CAlled by the API
        /// </summary>
        /// <param name="gt">The gt.</param>
        public void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            animationController.Update(gt.ElapsedGameTime, Matrix.Identity);
        }

        #endregion
 

        #region IAnimatedController Members

        /// <summary>
        /// Changes the interpolation mode.
        /// </summary>
        /// <param name="im">The im.</param>
        public void ChangeInterpolationMode(AnimationInterpolationMode im)
        {
            if (im == AnimationInterpolationMode.No_Interpolation)
            {

                animationController.TranslationInterpolation = InterpolationMode.None;
                animationController.OrientationInterpolation = InterpolationMode.None;
                animationController.ScaleInterpolation = InterpolationMode.None;
            }
            else if (im == AnimationInterpolationMode.Linear_Interpolation)
            {

                animationController.TranslationInterpolation = InterpolationMode.Linear;
                animationController.OrientationInterpolation = InterpolationMode.Linear;
                animationController.ScaleInterpolation = InterpolationMode.Linear;
            }
            else if (im == AnimationInterpolationMode.Cubic_Interpolation)
            {

                animationController.TranslationInterpolation = InterpolationMode.Cubic;
                animationController.OrientationInterpolation = InterpolationMode.Linear;
                animationController.ScaleInterpolation = InterpolationMode.Cubic;
            }
            else if (im == AnimationInterpolationMode.Spherical_Interpolation)
            {
                animationController.TranslationInterpolation = InterpolationMode.Linear;
                animationController.OrientationInterpolation = InterpolationMode.Spherical;
                animationController.ScaleInterpolation = InterpolationMode.Linear;

            }
        }

        #endregion

        #region IAnimatedController Members


        /// <summary>
        /// Gets the bone transformations.
        /// </summary>
        /// <returns></returns>
        public Matrix[] GetBoneTransformations()
        {
            return animationController.SkinnedBoneTransforms;
        }        

        #endregion

        #region ISerializable Members

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not yet implemented", LogLevel.Warning);
        }

        #endregion
    }
}
