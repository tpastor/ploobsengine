using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// IPost Effect Specification
    /// </summary>
    public interface IPostEffect
    {

        /// <summary>
        /// Gets the priority in relation with others PostEffects        
        /// </summary>
        int Priority
        {
            get;            
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPostEffect"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        bool Enabled
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
        void init(IContentManager manager, GraphicInfo ginfo, GraphicFactory factory);

        /// <summary>
        /// Apply the post effect
        /// </summary>
        /// <param name="rHelper">The r helper.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="world">The world.</param>
        void Draw(RenderHelper rHelper, GameTime gt, GraphicInfo GraphicInfo, IWorld world);
    }



}
