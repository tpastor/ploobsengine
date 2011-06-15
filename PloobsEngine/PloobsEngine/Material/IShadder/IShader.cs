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
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace PloobsEngine.Material
{

    /// <summary>
    /// Base Class For all Shaders
    /// </summary>
    #if !WINDOWS_PHONE
    public abstract class IShader : ISerializable
#else
    public abstract class IShader 
#endif
    {
        /// <summary>
        /// is fist time that this shader is updated
        /// </summary>
        protected bool firstTime = true;
        protected bool isInitialized = false;
        protected Effect basicDraw = null;
        protected Effect getDepth = null;
        
        /// <summary>
        /// Shader ID that the object rendered by this shader will have        
        /// </summary>
        /// <remarks>
        /// This parameter lets the object not be affect by light, or be recovered in a post proccess phase
        /// </remarks>
        protected float shaderId = 0;

        
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
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize(GraphicInfo ginfo, GraphicFactory factory, IObject obj)
        {
            #if !WINDOWS_PHONE
            basicDraw = factory.GetEffect("clippingPlane", false, true);
            getDepth = factory.GetEffect("ShadowDepth",false,true);
            #endif
            isInitialized = true;    
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
        /// </summary>
        /// <param name="ent">The ent.</param>
        /// <param name="lights">The lights.</param>
        public virtual void Update(GameTime gt, IObject ent, IList<ILight> lights)
        {
            if (firstTime)
            {             
                PreUpdate(ent, lights);
                firstTime = false;
            }            
        }


        /// <summary>
        /// Called every frame before the draw phase.
        /// In deferred it is called before the GBUFFER is setted
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="world">The world.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The camera</param>
        public virtual void PreDrawPhase(GameTime gt, IWorld world, IObject obj, RenderHelper render, ICamera cam)
        {            
        }

        /// <summary>
        /// Called after the draw phase.
        /// In deferred its responsible for the Forward Pass, in forward its not called        
        /// </summary>
        /// <param name="modelo">The modelo.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The camera.</param>
        /// <param name="lights">The lights.</param>
        public virtual void PosDrawPhase(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
            
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="modelo">The modelo.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        public virtual void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {            
        }

        #if !WINDOWS_PHONE
        /// <summary>
        /// Exctract the depth from an object
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        public virtual void DepthExtractor(GameTime gt, IObject obj, Matrix View, Matrix projection, RenderHelper render)
        {
            Matrix wld = obj.WorldMatrix;            
            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {
                    Matrix w1 = Matrix.Multiply(wld, bi[j].ModelLocalTransformation);
                    this.getDepth.Parameters["WVP"].SetValue(w1 * View * projection);

                    render.RenderBatch(bi[j], getDepth);
                }
            }
        }


        /// <summary>
        /// Draw the object in a simple way (WITH MINIMUM EFFECTS,....)
        /// USED IN RELECTIONS, REFRACTION .....
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        /// <param name="clippingPlane">The clipping plane.</param>
        public virtual void BasicDraw(GameTime gt, IObject obj, Matrix view, Matrix projection, IList<ILight> lights, RenderHelper render,Plane? clippingPlane, bool useAlphaBlending = false)
        {
            Matrix wld = obj.WorldMatrix;
            if (clippingPlane != null)
            {
                basicDraw.Parameters["clippingPlane"].SetValue(new Vector4(clippingPlane.Value.Normal, clippingPlane.Value.D));
                basicDraw.Parameters["isClip"].SetValue(true);
            }
            else
            {
                basicDraw.Parameters["isClip"].SetValue(false);
            }            

            if(useAlphaBlending)
                render.PushBlendState(BlendState.AlphaBlend);

            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {
                    basicDraw.Parameters["diffuse"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE,i,j));
                    Matrix w1 = Matrix.Multiply(wld, bi[j].ModelLocalTransformation);
                    this.basicDraw.Parameters["WVP"].SetValue(w1 * view * projection);

                    render.RenderBatch(bi[j], basicDraw);
                }
            }

            if(useAlphaBlending)
                render.PopBlendState();
        }
        
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
        #endif

    }
}
