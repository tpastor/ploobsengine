using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Modelo.Animation
{
    /// <summary>
    /// New Kind of Model specific for Animations
    /// </summary>
    public interface IAnimatedModel : IModelo
    {
        /// <summary>
        /// Gets the animation Information from Model
        /// It can be for example the Bone hierarchy of the model
        /// The result need to be casted (vary toooo much between animation APIs, better to keep as Object)
        /// </summary>
        /// <returns></returns>
        Object GetAnimation();        
    }
}
