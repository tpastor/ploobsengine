using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;
using PloobsEngine.DataStructure;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a Render Technic
    /// </summary>
    public abstract class IRenderTechnic : PloobsEngine.SceneControl.Scene.IIRenderTechnic
    {
        #if !WINDOWS_PHONE
        public IRenderTechnic(PostEffectType PostEffectType)
        {
            this.PostEffectType = PostEffectType;
        }        

        protected PostEffectType PostEffectType;

        /// <summary>
        /// List off all PostEffects
        /// </summary>
        public readonly PriorityQueueB<IPostEffect> PostEffects = new PriorityQueueB<IPostEffect>(new PostEffectComparer());

        /// <summary>
        /// Adds one post effect.
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        public virtual void AddPostEffect(IPostEffect postEffect)
        {
            System.Diagnostics.Debug.Assert(postEffect != null);
            if ((postEffect.PostEffectType & PostEffectType) != PostEffectType)            
            {
                ActiveLogger.LogMessage("Trying to add a wrong post effect for this Render Technich, pls check if the PostEffectType of the IPostEffect is All or " + PostEffectType + ", The engine is ignoring this operation", LogLevel.RecoverableError);
            }
            else
            {                
                PostEffects.Push(postEffect);
                postEffect.tech = this;

            }
        }
        /// <summary>
        /// Removes one post effect.
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        public virtual void RemovePostEffect(IPostEffect postEffect)
        {
            System.Diagnostics.Debug.Assert(postEffect != null);
            if ((postEffect.PostEffectType & PostEffectType) != PostEffectType)            
            {
                ActiveLogger.LogMessage("Trying to remove a wrong post effect type for this Render Technich, pls check if the PostEffectType of the IPostEffect is All or " + PostEffectType + ", The engine is ignoring this operation", LogLevel.RecoverableError);
            }
            PostEffects.RemoveLocation(postEffect);
            postEffect.tech = null;
        }

        /// <summary>
        /// Determines whether [contains post effect] [the specified post effect].
        /// </summary>
        /// <param name="postEffect">The post effect.</param>
        /// <returns>
        ///   <c>true</c> if [contains post effect] [the specified post effect]; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsPostEffect(IPostEffect postEffect)
        {
            System.Diagnostics.Debug.Assert(postEffect != null);
            for (int i = 0; i < PostEffects.Count; i++)
            {
                if (PostEffects[i] == postEffect)
                    return true;
            }
            return false;
        }
        #endif

        /// <summary>
        /// Befores the first execution.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="world">The world.</param>
        protected virtual void BeforeFirstExecution(RenderHelper render, IWorld world) { }
        internal void iBeforeFirstExecution(RenderHelper render, IWorld world)
        {
            BeforeFirstExecution(render, world);
        }

        /// <summary>
        /// Executes the technic.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="world">The world.</param>
        protected abstract void ExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world);
        internal void iExecuteTechnic(GameTime gameTime, RenderHelper render, IWorld world)
        {
            ExecuteTechnic(gameTime, render, world);
        }

        /// <summary>
        /// Called after the All the Engine stuffs are loaded
        /// </summary>
        protected virtual void AfterLoadContent(IContentManager manager, GraphicInfo ginfo, GraphicFactory factory) { }
        internal void iAfterLoadContent(IContentManager manager, GraphicInfo ginfo, GraphicFactory factory)
        {
            #if !WINDOWS_PHONE
            for (int i = 0; i < PostEffects.Count; i++)			
            {
                PostEffects[i].Init(ginfo,factory);
            }
            #endif
            AfterLoadContent(manager,ginfo,factory);
            
        }
        /// <summary>
        /// Gets the name of the technic.
        /// </summary>
        /// <value>
        /// The name of the technic.
        /// </value>
        public abstract String TechnicName
        {
            get;
        }
        
    }
}
