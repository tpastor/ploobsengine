using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a Content Loader
    /// </summary>
    public interface IContentManager
    {
        /// <summary>
        /// Gets the asset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        T GetAsset<T>(String fileName, bool isInternal = false);                
    }
    
}
