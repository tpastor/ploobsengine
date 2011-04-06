using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Material
{
    

    /// <summary>
    /// Deferred Material
    /// </summary>
    public class DeferredMaterial : IMaterial
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardMaterial"/> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        public DeferredMaterial(IShader shader)
        {
            System.Diagnostics.Debug.Assert(shader.MaterialType == Material.MaterialType.DEFERRED);
            this.Shadder = shader;
        }

        bool affectedByShadow = false;
        IShader shader = null;        

        #region IMaterial Members

        /// <summary>
        /// Initializations the specified Material.
        /// </summary>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="obj">The obj.</param>
        public void Initialization(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, SceneControl.IObject obj)
        {
            shader.Initialize(ginfo, factory, obj);
        }


        /// <summary>
        /// Pre drawn Function.
        /// Called before all the objects are draw
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="mundo">The mundo.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        public void PreDrawnPhase(Microsoft.Xna.Framework.GameTime gt, SceneControl.IWorld mundo, SceneControl.IObject obj, Cameras.ICamera cam, IList<Light.ILight> lights, SceneControl.RenderHelper render)
        {
            shader.PreDrawPhase(gt, mundo, obj, render, cam);
        }

        /// <summary>
        /// Pos drawn Function.
        /// Called after all objects are Draw
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        public void PosDrawnPhase(Microsoft.Xna.Framework.GameTime gt, SceneControl.IObject obj, Cameras.ICamera cam, IList<Light.ILight> lights, SceneControl.RenderHelper render)
        {
            shader.PosDrawPhase(gt, obj, render, cam, lights);      
        }

        /// <summary>
        /// Normal Drawn Function.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        public void Drawn(Microsoft.Xna.Framework.GameTime gt, SceneControl.IObject obj, Cameras.ICamera cam, IList<Light.ILight> lights, SceneControl.RenderHelper render)
        {
            shader.Draw(gt, obj, render, cam,lights);
        }



        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="lights">The lights.</param>
        public void Update(Microsoft.Xna.Framework.GameTime gametime, SceneControl.IObject obj, IList<Light.ILight> lights)
        {            
            shader.Update(gametime, obj, lights);
        }

        /// <summary>
        /// Gets or sets the shadder.
        /// </summary>
        /// <value>
        /// The shadder.
        /// </value>
        public IShader Shadder
        {
            get
            {
                return shader;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value.MaterialType == Material.MaterialType.DEFERRED);
                this.shader = value;
            }
        }

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public MaterialType MaterialType
        {
            get
            {
                return Material.MaterialType.DEFERRED;    
            }
            
        }

        /// <summary>
        /// Gets or sets a value indicating whether this material is [affected by shadow].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [affected by shadow]; otherwise, <c>false</c>.
        /// </value>
        public bool AffectedByShadow
        {
            get
            {
                return affectedByShadow;          
            }
            set
            {
                this.affectedByShadow = value;
            }
        }

        #endregion

        #region ISerializable Members

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {            
        }

        #endregion

        
    }
}
