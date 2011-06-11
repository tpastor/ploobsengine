using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a Content Loader
    /// </summary>
    public abstract class IContentManager
    {
        internal ContentManager externalContentManager;

        /// <summary>
        /// Gets the asset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public abstract T GetAsset<T>(String fileName, bool isInternal = false);                
    }
    
}
