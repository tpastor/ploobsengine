using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Light;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Modelo;
using System.Runtime.Serialization;
using PloobsEngine.Engine;


namespace PloobsEngine.Material
{
    /// <summary>
    /// Material Specification
    /// </summary>
    #if !WINDOWS_PHONE
    public interface IMaterial : ISerializable
#else
    public interface IMaterial 
#endif
    {

        /// <summary>
        /// Initializations the specified Material.
        /// </summary>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="obj">The obj.</param>
        void Initialization(GraphicInfo ginfo, GraphicFactory factory, IObject obj);
        

        /// <summary>
        /// Pre drawn Function.
        /// Called before all the objects are draw
        /// </summary>
        /// <param name="mundo">The mundo.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        void PreDrawnPhase(GameTime gt, IWorld mundo, IObject obj, ICamera cam, IList<ILight> lights, RenderHelper render);
        
        /// <summary>
        /// Pos drawn Function.        
        /// Called after all objects are Draw
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        void PosDrawnPhase(GameTime gt, IObject obj, ICamera cam, IList<ILight> lights, RenderHelper render);
        
        /// <summary>
        /// Normal Drawn Function.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        void Drawn(GameTime gt  ,IObject obj, ICamera cam , IList<ILight> lights,RenderHelper render);
        
        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="lights">The lights.</param>
        void Update(GameTime gametime, IObject obj, IList<ILight> lights);
        /// <summary>
        /// Gets or sets the shadder.
        /// </summary>
        /// <value>
        /// The shadder.
        /// </value>
        IShader Shadder { set; get; }
        /// <summary>
        /// Gets or sets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        MaterialType MaterialType { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this material is [Create shadow on others objects].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [affected by shadow]; otherwise, <c>false</c>.
        /// </value>
        bool CanCreateShadow { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can appear of reflection and refraction.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can appear of reflection/refraction; otherwise, <c>false</c>.
        /// </value>
        bool CanAppearOfReflectionRefraction { set; get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        bool IsVisible { set; get; }                
    }

    /// <summary>
    /// Material Type
    /// </summary>
    public enum MaterialType 
    {
        /// <summary>
        /// When this type is set AND the render is Deferred,
        /// Only the Pre Draw and the Draw call are called
        /// </summary>
        DEFERRED,
        /// <summary>
        /// When this type is set AND the render is Deferred,
        /// Only the Pos Draw call is called.
        /// </summary>
        FORWARD
    }
}
