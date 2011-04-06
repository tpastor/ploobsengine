using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.SceneControl;


namespace PloobsEngine.Light
{
    /// <summary>
    /// Deferred Point Light
    /// </summary>
    public class PointLightPE : DeferredLight
    {
        /// <summary>
        /// Serialization 
        /// </summary>
        internal PointLightPE()
        {
        }

        /// <summary>
        /// Point Light
        /// </summary>
        /// <param name="lightPosition">Position in World</param>
        /// <param name="color">Color</param>
        /// <param name="lightRadius">Radius</param>
        /// <param name="lightIntensity">Intensity</param>
        public PointLightPE(Vector3 lightPosition, Color color, float lightRadius, float lightIntensity)
        {
            this.lightPosition = lightPosition;
            this.color = color;
            this.lightRadius = lightRadius;
            this.lightIntensity = lightIntensity;            
        }

        protected Vector3 lightPosition;

        /// <summary>
        /// Gets or sets the light position.
        /// </summary>
        /// <value>
        /// The light position.
        /// </value>
        public virtual Vector3 LightPosition
        {
            get { return lightPosition; }
            set { lightPosition = value; }
        }        
        protected float lightRadius;

        /// <summary>
        /// Gets or sets the light radius.
        /// </summary>
        /// <value>
        /// The light radius.
        /// </value>
        public float LightRadius
        {
            get { return lightRadius; }
            set { lightRadius = value; }
        }
        protected float lightIntensity;

        /// <summary>
        /// Gets or sets the light intensity.
        /// </summary>
        /// <value>
        /// The light intensity.
        /// </value>
        public float LightIntensity
        {
            get { return lightIntensity; }
            set { lightIntensity = value; }
        }

        protected bool usePointLightQuadraticAttenuation = false;

        /// <summary>
        /// Gets or sets a value indicating whether [use point light quadratic attenuation].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [use point light quadratic attenuation]; otherwise, <c>false</c>.
        /// </value>
        public bool UsePointLightQuadraticAttenuation
        {
            get { return usePointLightQuadraticAttenuation; }
            set { usePointLightQuadraticAttenuation = value; }
        }

        #region ILight Members

        /// <summary>
        /// Gets the type of the light.
        /// </summary>
        /// <value>
        /// The type of the light.
        /// </value>
        public override LightType LightType
        {
            get { return LightType.Deferred_Point; }
        }

        #endregion
    }
}
