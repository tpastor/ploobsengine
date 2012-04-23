using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;

namespace PloobsEngine.Material
{
    
    public class ClothMaterial : IMaterial
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardMaterial"/> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        public ClothMaterial(IShader shader)
        {
            this.Shader = shader;
            CanAppearOfReflectionRefraction = true;
            CanCreateShadow = true;
            IsVisible = true;
            rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
        }

        RasterizerState rasterizerState = null;

        public RasterizerState RasterizerState
        {
            get { return rasterizerState; }
            set { rasterizerState = value; }
        }

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
            PhysxClothObject PhysxClothObject = obj.PhysicObject as PhysxClothObject;
            ClothModel ClothModel = obj.Modelo as ClothModel;

            StillDesign.PhysX.MathPrimitives.Vector3[] pos = PhysxClothObject.Cloth.GetMeshData().PositionsStream.GetData<StillDesign.PhysX.MathPrimitives.Vector3>();
            StillDesign.PhysX.MathPrimitives.Vector3[] norm = PhysxClothObject.Cloth.GetMeshData().NormalsStream.GetData<StillDesign.PhysX.MathPrimitives.Vector3>();

            for (int i = 0; i < ClothModel.BatchInformation.NumVertices; i++)
            {
                ClothModel.vertexPositionNormalTexture[i].Position = pos[i].AsXNA();
                ClothModel.vertexPositionNormalTexture[i].Normal = norm[i].AsXNA();
            }

            ClothModel.BatchInformation.VertexBuffer.SetData<VertexPositionNormalTexture>(ClothModel.vertexPositionNormalTexture);

            if (rasterizerState != null && rasterizerState != render.PeekRasterizerState())
            {
                rasterStateFlag = true;
                render.PushRasterizerState(rasterizerState);
            }

            shader.iDraw(gt, obj, render, cam, lights);

            if (rasterStateFlag)
            {
                render.PopRasterizerState();
                rasterStateFlag = false;
            }
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

        public void AfterAdded(SceneControl.IObject obj)
        {
        }

        #endregion
    }
}
