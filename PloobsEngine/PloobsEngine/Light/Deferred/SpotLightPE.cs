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
    /// Deferred Spot Light
    /// </summary>
    public class SpotLightPE : DeferredLight
    {   
        /// <summary>
        /// Serialization 
        /// </summary>
        internal SpotLightPE()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotLightPE"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="coneDecay">The cone decay.</param>
        /// <param name="lightRadius">The light radius.</param>
        /// <param name="Color">The color.</param>
        /// <param name="lightAngleCosine">The light angle cosine.</param>
        /// <param name="lightIntensity">The light intensity.</param>
        public SpotLightPE(Vector3 position, Vector3 direction, float coneDecay, float lightRadius, Color Color, float lightAngleCosine, float lightIntensity)
        {
            this.position = position;
            this.direction = direction;
            //this.strength = strength;
            this.lightRadius = lightRadius;
            this.coneDecay = coneDecay;
            this.lightAngleCosine = lightAngleCosine;
            this.lightIntensity = lightIntensity;
            this.color = Color;
            target = direction;            
            init();            
         }

        private void init()
        {
            this.ViewMatrix = Matrix.CreateLookAt(this.position, this.direction, Vector3.Up);
            float angle = (float)Math.Acos(this.lightAngleCosine);
            this.projMatrix = Matrix.CreatePerspectiveFieldOfView(angle * 2, 1, 1f, 1000f);
            _bias = 1.0f / 10000.0f;
        }

        protected float lightIntensity;

        /// <summary>
        /// Gets or sets the proj matrix.
        /// </summary>
        /// <value>
        /// The proj matrix.
        /// </value>
        public override Matrix ProjMatrix
        {
            get
            {
                return this.projMatrix;
            }
            set
            {
                this.projMatrix= value;
            }
        }

        /// <summary>
        /// Gets or sets the view matrix.
        /// </summary>
        /// <value>
        /// The view matrix.
        /// </value>
        public override Matrix ViewMatrix
        {
            get
            {
                this.viewMatrix = Matrix.CreateLookAt(this.position, this.direction, Vector3.Up);
                return this.viewMatrix;
            }
            set
            {
                this.viewMatrix = value;
            }
        }

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

        protected float lightAngleCosine;

        /// <summary>
        /// Gets or sets the light angle cosine.
        /// </summary>
        /// <value>
        /// The light angle cosine.
        /// </value>
        public float LightAngleCosine
        {
            get { return lightAngleCosine; }
            set { lightAngleCosine = value; }
        }
        
        protected Vector3 position;

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public virtual Vector3 Position
        {
            get { return position; }
            set { 
                position = value;                
            }
        }
        //private float strength;

        //public float Strength
        //{
        //    get { return strength; }
        //    set { strength = value; }
        //}
        protected Vector3 direction;

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public Vector3 Direction
        {
            get { return direction; }
            set { direction = value; }
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
        protected float coneDecay;

        /// <summary>
        /// Gets or sets the cone decay.
        /// </summary>
        /// <value>
        /// The cone decay.
        /// </value>
        public float ConeDecay
        {
            get { return coneDecay; }
            set { coneDecay = value; }
        }


        protected Vector3 target ;
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public virtual Vector3 Target
        {
            set
            {
                this.target = value;
                this.direction = (Vector3)target - (Vector3)position;
                this.direction.Normalize();
                

            }
            get
            {                
                return target;
            }
        }

        /// <summary>
        /// Gets the type of the light.
        /// </summary>
        /// <value>
        /// The type of the light.
        /// </value>
        public override LightType LightType
        {
            get { return LightType.Deferred_Spot; }
        }
    }
}
