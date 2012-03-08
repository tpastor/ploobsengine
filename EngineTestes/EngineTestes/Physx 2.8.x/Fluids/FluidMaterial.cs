using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using Phyx = StillDesign.PhysX.MathPrimitives;

namespace PloobsEngine.Material
{
    
    public class FluidMaterial : IMaterial
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardMaterial"/> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        public FluidMaterial(IShader shader, int maxParticles, Microsoft.Xna.Framework.Vector2 scale)
        {
            this.Shader = shader;
            CanAppearOfReflectionRefraction = false;
            CanCreateShadow = false;
            IsVisible = true;
            BilboardInstance = new BilboardInstance[maxParticles];
            for (int i = 0; i < maxParticles; i++)
            {
                BilboardInstance[i] = new BilboardInstance();
                BilboardInstance[i].Scale = scale;
            }
        }

        BilboardInstance[] BilboardInstance;
                          
        
        public bool CanAppearOfReflectionRefraction
        {
            get;
            set;
        }

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

        private bool rasterStateFlag = false;
        /// <summary>
        /// Normal Draw Function.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        public void Drawn(Microsoft.Xna.Framework.GameTime gt, SceneControl.IObject obj, Cameras.ICamera cam, IList<Light.ILight> lights, SceneControl.RenderHelper render)
        {
            PhysxFluidObject PhysxFluidObject = obj.PhysicObject as PhysxFluidObject;
            InstancedBilboardModel InstancedBilboardModel = obj.Modelo as InstancedBilboardModel;

            if (!PhysxFluidObject.Fluid.ParticleWriteData.NumberOfParticles.HasValue)
                return;

            Phyx.Vector3[] pos = PhysxFluidObject.Fluid.ParticleWriteData.PositionBuffer.GetData<Phyx.Vector3>();            
            BilboardInstance[] inst = new Modelo.BilboardInstance[PhysxFluidObject.Fluid.ParticleWriteData.NumberOfParticles.Value];
            for (int i = 0; i < PhysxFluidObject.Fluid.ParticleWriteData.NumberOfParticles; i++)
			{
			    inst[i] = BilboardInstance[i];
                inst[i].Position = pos[i].AsXNA();
			}

            InstancedBilboardModel.SetBilboardInstances(inst);

            shader.Draw(gt, obj, render, cam, lights);

        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="lights">The lights.</param>
        public void Update(Microsoft.Xna.Framework.GameTime gametime, SceneControl.IObject obj, IWorld world)
        {            
            shader.Update(gametime, obj, world.Lights);
        }

        /// <summary>
        /// Gets or sets the shadder.
        /// </summary>
        /// <value>
        /// The shadder.
        /// </value>
        public IShader Shader
        {
            get
            {
                return shader;
            }
            set
            {             
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
                return shader.MaterialType;
            }

        }

        /// <summary>
        /// Gets or sets a value indicating whether this material is [affected by shadow].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [affected by shadow]; otherwise, <c>false</c>.
        /// </value>
        public bool CanCreateShadow
        {
            get;
            set;
        }

        #endregion

        #region ISerializable Members
#if WINDOWS
        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
        }
#endif
        #endregion

        #region IMaterial Members


        public bool IsVisible
        {
            get;
            set;
        }

        #endregion

        #region IMaterial Members

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="factory"></param>
        public void CleanUp(PloobsEngine.Engine.GraphicFactory factory)
        {
            shader.Cleanup(factory);
        }

        #endregion
    }
}
