using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.Cameras;
using PloobsEngine.SceneControl;


namespace PloobsEngine.Light
{
    /// <summary>
    /// Deferred Directional Light
    /// </summary>
    public class DirectionalLightPE : DeferredLight
    {        
        /// <summary>
        /// experimentalllll
        /// </summary>
        private float nearClipOffset = 800.0f;

        /// <summary>
        /// Parameter to Tune the Shadow Mapping Generation
        /// Default 800
        /// </summary>
        public float NearClipOffset
        {
            get { return nearClipOffset; }
            set { nearClipOffset = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalLightPE"/> class.
        /// </summary>
        internal DirectionalLightPE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalLightPE"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="color">The color.</param>
        public DirectionalLightPE(Vector3 direction, Color color)
        {
            this.lightDirection = direction;
            this.color = color;
            target = lightDirection;
            position = Vector3.Zero;
            ///bom valor para o Bias, obtido experimentalmente para este tipo de luz
            _bias = 0.006f;
        }


        protected Vector3 target ;

        /// <summary>
        /// Used With the Position to generate a Direction
        /// </summary>
        public virtual Vector3 Target
        {
            set
            {
                this.target = value;
                this.lightDirection = (Vector3)target - (Vector3)position;
                lightDirection.Normalize();                
            }
            get
            {                
                return target;
            }
        }


        private float lightIntensity = 1;

        /// <summary>
        /// Gets or sets the light intensity.
        /// YOU MUST SET BOTH
        /// </summary>
        /// <value>
        /// The light intensity.
        /// </value>
        public float LightIntensity
        {
            get { return lightIntensity; }
            set { lightIntensity = value; }
        }

        
        protected Vector3 position;
        /// <summary>
        /// Used with the target to generate a Direction
        /// YOU MUST SET BOTH
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public virtual Vector3 Position
        {
            set
            {
                this.position= value;
                this.lightDirection = (Vector3)target - (Vector3)position;
                this.lightDirection.Normalize();             

            }
            get
            {                
                return position;
            }
        }




        private Vector3 lightDirection;

        /// <summary>
        /// Gets or sets the light direction.
        /// </summary>
        /// <value>
        /// The light direction.
        /// </value>
        public Vector3 LightDirection
        {
            set
            {
                this.lightDirection = value;
            }
            get
            {
                return lightDirection;
            }
        }


        /// <summary>
        /// Create a Fake view projection matrixes to use in Shadow.
        /// Calculate a Box that englobes the cam frustrum, put the eye near the center
        /// and calculates the view matrix, after use an ortograph projection to create
        /// the projection matrix
        /// Classic Algorith, dont make assumption on the scene orgnization, but also dont generate an optimal
        /// depth map (lots of none used pixels)
        /// </summary>
        /// <param name="cam">The cam.</param>
        public void FakeViewProjection(ICamera cam)   /////////directional light does not have position, need to create a fake one before the camera ()
        {
            // Find the centroid            
            Vector3 frustumCentroid = new Vector3(0, 0, 0);
            foreach (Vector3 item in cam.BoundingFrustum.GetCorners())
            {              
                frustumCentroid += item;
            }
            frustumCentroid /= 8;

            // Position the shadow-caster camera so that it's looking at the centroid,
            // and backed up in the direction of the sunlight

            float distFromCentroid = cam.FarPlane + nearClipOffset;
            viewMatrix = Matrix.CreateLookAt(frustumCentroid - (lightDirection * distFromCentroid), frustumCentroid, new Vector3(0, 1, 0));

            // Determine the position of the frustum corners in light space
            Vector3[] frustumCornersLS = new Vector3[8];
            Vector3.Transform(cam.BoundingFrustum.GetCorners(), ref viewMatrix, frustumCornersLS);

            // Calculate an orthographic projection by sizing a bounding box 
            // to the frustum coordinates in light space
            Vector3 mins = frustumCornersLS[0];
            Vector3 maxes = frustumCornersLS[0];
            for (int i = 1; i < 8; i++)  
            { 
                mins = Vector3.Min(mins, frustumCornersLS[i]) ;
                maxes = Vector3.Max(maxes, frustumCornersLS[i]); 
            } 

            // Create an orthographic camera for use as a shadow caster
            projMatrix =  Matrix.CreateOrthographicOffCenter(mins.X, maxes.X, mins.Y, maxes.Y, -maxes.Z - nearClipOffset, -mins.Z );            

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
            get { return LightType.Deferred_Directional; }
        }

        #endregion
    }
}
