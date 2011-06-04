#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl.Scene;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// IPost Effect Specification
    /// </summary>
    public abstract class IPostEffect
    {
        public IPostEffect(PostEffectType PostEffectType)
        {
            this.PostEffectType = PostEffectType;
            Priority = 0;
            Enabled = true;
        }

        /// <summary>
        /// Gets or sets the type of the post effect.
        /// </summary>
        /// <value>
        /// The type of the post effect.
        /// </value>
        public PostEffectType PostEffectType
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the priority in relation with others PostEffects        
        /// Lower the number, first the effect will be applied
        /// 0 By Default
        /// </summary>
        public float Priority
        {
            get { return priority; }
            set
            {
                this.priority = value;
                if (tech != null)
                {
                    tech.RemovePostEffect(this);
                    tech.AddPostEffect(this);
                }
            }
        }
        protected float priority;
        internal IIRenderTechnic tech = null;



        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPostEffect"/> is enabled.
        /// Enabled by default
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get;
            set;
        }
        
        /// <summary>
        /// Initiates the specified Post Effect.
        /// Called by the engine
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        public abstract void Init(GraphicInfo ginfo, GraphicFactory factory);

        /// <summary>
        /// Apply the post effect
        /// </summary>
        /// <param name="ImageToProcess">The image to process.</param>
        /// <param name="rHelper">The r helper.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="world">The world.</param>
        /// <param name="useFloatBuffer">if set to <c>true</c> [use float buffer].</param>
        public abstract void Draw(Texture2D ImageToProcess,RenderHelper rHelper, GameTime gt, GraphicInfo GraphicInfo, IWorld world,bool useFloatBuffer);
    }

    /// <summary>
    /// Defines in what render the post effect works
    /// </summary>
    public enum PostEffectType
    {
        /// <summary>
        /// Only on Deferred
        /// </summary>
        Deferred,
        /// <summary>
        /// only on Forward
        /// </summary>
        Forward,
        /// <summary>
        /// Works in both renders
        /// </summary>
        All
    }

}
#endif