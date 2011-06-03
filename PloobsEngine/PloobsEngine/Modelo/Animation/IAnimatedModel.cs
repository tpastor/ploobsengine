using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace PloobsEngine.Modelo.Animation
{
    /// <summary>
    /// New Kind of Model specific for Animations
    /// </summary>
    public abstract class IAnimatedModel : IModelo
    {
        public IAnimatedModel(GraphicFactory factory, String modelName, String diffuseTextureName, String bumpTextureName = null, String specularTextureName = null, String glowTextureName = null)
            : base(factory, modelName, diffuseTextureName, bumpTextureName, specularTextureName, glowTextureName)
        {

        }

        /// <summary>
        /// Gets the animation Information from Model
        /// It can be for example the Bone hierarchy of the model
        /// The result need to be casted (vary toooo much between animation APIs, better to keep as Object)
        /// </summary>
        /// <returns></returns>
        public abstract Object GetAnimation();        
    }
}
