using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using Osiris.Graphics.Terrain;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Terrain Material
    /// </summary>
    public class TerrainMaterial : IMaterial
	{
		#region Variables
        		
		private TerrainModel _terrainModel;

		#endregion

		#region Properties

		public TerrainModel Model
		{
			get { return _terrainModel; }
		}

		#endregion

		#region Constructors

        public TerrainMaterial(GraphicFactory factory, string terrainModelAssetName)		
		{
            _terrainModel = factory.GetAsset<TerrainModel>(terrainModelAssetName);            
            IsVisible = true;
		}

		#endregion

		#region Methods
         

		#endregion
        GeoclipShader GeoclipShader = new GeoclipShader();
        GraphicInfo ginfo;
        /// <summary>
        /// Initializations the specified Material.
        /// </summary>
        /// <param name="ginfo">The ginfo.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="obj">The obj.</param>
        public void Initialization(GraphicInfo ginfo, GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {
            this.ginfo = ginfo;
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
        public void PreDrawnPhase(GameTime gt, PloobsEngine.SceneControl.IWorld mundo, PloobsEngine.SceneControl.IObject obj, PloobsEngine.Cameras.ICamera cam, System.Collections.Generic.IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render)
        {
            
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
        public void PosDrawnPhase(GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.Cameras.ICamera cam, System.Collections.Generic.IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render)
        {
            
        }

        bool first= true;
        /// <summary>
        /// Normal Drawn Function.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        public void Drawn(GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.Cameras.ICamera cam, System.Collections.Generic.IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render)
        {
            if (first)
            {
                _terrainModel.Initialize(cam, render.device);
                first = false;
            }

            if (_terrainModel.Effect is IEffectMatrices)
            {
                IEffectMatrices effectMatrices = (IEffectMatrices)_terrainModel.Effect;
                effectMatrices.World = obj.WorldMatrix;
                effectMatrices.View = cam.View;
                effectMatrices.Projection = cam.Projection;                
            }

            render.SetSamplerStates(SamplerState.LinearWrap, 6);
            _terrainModel.Draw();
            render.SetSamplerStates(ginfo.SamplerState, 6);

        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="gametime">The gametime.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="world">The world.</param>
        /// ///
        public void Update(GameTime gametime, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.IWorld world)
        {
            _terrainModel.Update(world.CameraManager.ActiveCamera);    
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
                return new GeoclipShader();
            }
            set
            {                
            }
        }

        /// <summary>
        /// Gets or sets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.FORWARD; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this material is [Create shadow on others objects].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [affected by shadow]; otherwise, <c>false</c>.
        /// </value>
        public bool CanCreateShadow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can appear of reflection and refraction.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can appear of reflection/refraction; otherwise, <c>false</c>.
        /// </value>
        public bool CanAppearOfReflectionRefraction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="factory"></param>
        public void CleanUp(GraphicFactory factory)
        {
            _terrainModel.CleanUp();   
        }

#if WINDOWS
        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
#endif

        /// <summary>
        /// Called After the IObject is Added.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public void AfterAdded(SceneControl.IObject obj)
        {
        }
    }

    /// <summary>
    /// Hack shader used for Geoclip technic
    /// </summary>
    public class GeoclipShader : IShader
    {        
        internal GeoclipShader()
        {
            
        }


        /// <summary>
        /// Draws the specified gt.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        protected override void Draw(GameTime gt, SceneControl.IObject obj, SceneControl.RenderHelper render, Cameras.ICamera cam, System.Collections.Generic.IList<Light.ILight> lights)
        {
        }
#if !REACH && !WINDOWS_PHONE
        /// <summary>
        /// Draw the object in a simple way (WITH MINIMUM EFFECTS,....)
        /// USED IN RELECTIONS, REFRACTION .....
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        /// <param name="clippingPlane">The clipping plane.</param>
        /// <param name="useAlphaBlending">if set to <c>true</c> [use alpha blending].</param>
        public override void BasicDraw(GameTime gt, SceneControl.IObject obj, ref Matrix view, ref Matrix projection, System.Collections.Generic.IList<Light.ILight> lights, SceneControl.RenderHelper render, Plane? clippingPlane, bool useAlphaBlending = false)
        {
        }

        /// <summary>
        /// Exctract the depth from an object
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="View">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="render">The render.</param>
        public override void DepthExtractor(GameTime gt, SceneControl.IObject obj, ref Matrix View, ref Matrix projection, SceneControl.RenderHelper render)
        {
        }
#endif        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="ginfo"></param>
        /// <param name="factory"></param>
        /// <param name="obj"></param>
        public override void Initialize(GraphicInfo ginfo, GraphicFactory factory, SceneControl.IObject obj)
        {
            
        }

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public override MaterialType MaterialType
        {
            get { return Material.MaterialType.FORWARD; }
        }
    }
}
