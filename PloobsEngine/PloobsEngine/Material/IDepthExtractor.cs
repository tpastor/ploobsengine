using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Depth Extractor specification
    /// Used to Create a Depth Map from the object    
    /// Should be implemented by some kind of model like
    /// Tree, Animation ...
    /// Atach this to the correct IShader.
    /// </summary>
    public interface IDepthExtractor
    {
        /// <summary>
        /// Extracts the depth from object.
        /// The correct RenderTarget is already setted before this function is called
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="view">The view.</param>
        /// <param name="proj">The proj.</param>
        void ExtractDepthFromObject(IObject obj, Matrix view, Matrix proj); 
    }
}
