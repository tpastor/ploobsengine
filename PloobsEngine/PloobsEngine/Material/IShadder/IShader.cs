#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using PloobsEngine.Modelo.Animation;
using System.Runtime.Serialization;

#endregion

namespace PloobsEngine.Material
{

    /// <summary>
    /// Base Class For all Shaders
    /// </summary>
    public abstract class IShader : ISerializable
    {

        /// <summary>
        /// List of the lights
        /// </summary>
        protected IList<ILight> lights;


        /// <summary>
        /// Object owned by this shader
        /// </summary>
        protected IObject obj;


        /// <summary>
        /// is fist time that this shader is updated
        /// </summary>
        protected bool firstTime = true;
        
        
        /// <summary>
        /// Shader ID that the object rendered by this shader will have        
        /// </summary>
        /// <remarks>
        /// This parameter lets the object not be affect by light, or be recovered in a post proccess phase
        /// </remarks>
        protected float shaderId = 0;
        /// <summary>
        /// instance of class that implements IDepthExtractor 
        /// </summary>
        /// <remarks> used By the shadow technic for example </remarks>
        protected IDepthExtractor depthExtractor;

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public abstract MaterialType MaterialType
        {
            get;
        }

        /// <summary>
        /// Used to Recover the object in PostProcces
        /// between 0 and 0.9f it will be illuminated, 
        /// bigger than 0.9 it wont be
        /// </summary>
        public float ShaderId
        {
            get { return shaderId; }
            set { shaderId = value; }
        }        

        /// <summary>
        /// Initializes a new instance of the <see cref="IShader"/> class.
        /// </summary>
        public IShader()
        {            
        }

        /// <summary>
        /// Gets or sets the depth extractor.
        /// </summary>
        /// <value>
        /// The depth extractor.
        /// </value>
        public IDepthExtractor DepthExtractor
        {
            get
            {
                return depthExtractor;
            }
            set
            {
                this.depthExtractor = value;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {            
            
        }

        /// <summary>
        /// Called Once only, before all draws
        /// </summary>
        /// <param name="ent">The current entity.</param>
        /// <param name="lights">All The lights </param>
        public virtual void PreUpdate(IObject ent, IList<ILight> lights) { }


        /// <summary>
        /// Updates this shader
        /// Called every frame once
        /// This function MUST be called by all the subclasses        
        /// </summary>
        /// <param name="ent">The ent.</param>
        /// <param name="lights">The lights.</param>
        public virtual void Update(IObject ent, IList<ILight> lights)
        {
            if (firstTime)
            {             
                PreUpdate(ent, lights);
                firstTime = false;
            }

            this.obj = ent;
            this.lights = lights;
            
        }

        
        /// <summary>
        /// Called every frame before the draw phase. 
        /// In deferred it is called before the GBUFFER is setted
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="modelo">The modelo.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The camera</param>
        public virtual void PreDrawPhase(IWorld world,IModelo modelo, IRenderHelper render, ICamera cam)
        {            
        }

        /// <summary>
        /// Called after the draw phase.
        /// In deferred its responsible for the Forward Pass
        /// IF THE SHADER IS NOT SETTED TO BE FORWARD THIS WONT BE CALLED
        /// </summary>
        /// <param name="modelo">The modelo.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The camera.</param>
        /// <param name="lights">The lights.</param>
        public virtual void PosDrawPhase(IModelo modelo, IRenderHelper render, ICamera cam , IList<ILight> lights)
        {
            
        }

        /// <summary>
        /// Draws the specified modelo.
        /// IF THE SHADER IS NOT DEFERRED THIS WONT BE CALLED
        /// </summary>
        /// <param name="modelo">The modelo.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        public virtual void Draw(IModelo modelo, IRenderHelper render, ICamera cam)
        {            
        }



        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }

        #endregion
    }
}
