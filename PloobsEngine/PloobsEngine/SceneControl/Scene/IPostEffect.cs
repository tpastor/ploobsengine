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
    public enum PostEffectType : ulong
    {
        /// <summary>
        /// Only on Deferred
        /// </summary>
        Deferred = 0x0001,
        /// <summary>
        /// only on Forward
        /// </summary>
        Forward3D = 0x0010,

        /// <summary>
        /// Works in 2D Also
        /// </summary>
        Forward2D = 0x0100,
        /// <summary>
        /// Works in both renders
        /// </summary>
        All = 0x1111
    }

}
#endif