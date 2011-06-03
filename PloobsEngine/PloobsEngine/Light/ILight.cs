using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using System.Runtime.Serialization;

namespace PloobsEngine.Light
{
    /// <summary>
    /// Specification of a Light
    /// </summary>
    #if !WINDOWS_PHONE
    public interface ILight : ISerializable
    #else
    public interface ILight 
    #endif
    {
        /// <summary>
        /// Gets or sets a value indicating whether [cast shadown].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cast shadown]; otherwise, <c>false</c>.
        /// </value>
        bool CastShadown
        {
            set;
            get;
        }        
        /// <summary>
        /// Gets or sets the name of the light.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        String Name
        {
            set;
            get;
        }
        /// <summary>
        /// Gets the type of the light.
        /// </summary>
        /// <value>
        /// The type of the light.
        /// </value>
        LightType LightType
        {
            get;
        }
    }

    /// <summary>
    /// Light Types
    /// </summary>
    public enum LightType
    {
        /// <summary>
        /// Deferred Directional Light
        /// </summary>
        Deferred_Directional,
        /// <summary>
        /// Deferred Point Light 
        /// </summary>
        Deferred_Point,
        /// <summary>
        /// Deferred Spot Light
        /// </summary>
        Deferred_Spot,
        /// <summary>
        /// None of these
        /// </summary>
        OTHER
    }
}
