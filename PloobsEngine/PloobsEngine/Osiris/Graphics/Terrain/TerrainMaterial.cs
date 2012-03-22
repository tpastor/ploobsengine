using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using Osiris.Graphics.Terrain;

namespace PloobsEngine.Material
{
    public class TerrainMaterial : IMaterial
	{
		#region Variables

		private readonly string _terrainModelAssetName;
		private TerrainModel _terrainModel;

		#endregion

		#region Properties

		public TerrainModel Model
		{
			get { return _terrainModel; }
		}

		#endregion

		#region Constructors

        public TerrainMaterial(GraphicFactory factory, string terrainModelAssetName, string diffuseTexture)		
		{
			_terrainModelAssetName = terrainModelAssetName;

            _terrainModel = factory.GetAsset<TerrainModel>(_terrainModelAssetName);
            
            IsVisible = true;
		}

		#endregion

		#region Methods
         

		#endregion

        public void Initialization(GraphicInfo ginfo, GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {
            
        }

        public void PreDrawnPhase(GameTime gt, PloobsEngine.SceneControl.IWorld mundo, PloobsEngine.SceneControl.IObject obj, PloobsEngine.Cameras.ICamera cam, System.Collections.Generic.IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render)
        {
            
        }

        public void PosDrawnPhase(GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.Cameras.ICamera cam, System.Collections.Generic.IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render)
        {
            
        }

        bool first= true;
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
                        
            _terrainModel.Draw();

        }

        public void Update(GameTime gametime, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.IWorld world)
        {
            _terrainModel.Update(world.CameraManager.ActiveCamera);    
        }

        public IShader Shader
        {
            get
            {
                return null;
            }
            set
            {                
            }
        }

        public MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.FORWARD; }
        }

        public bool CanCreateShadow
        {
            get;
            set;
        }

        public bool CanAppearOfReflectionRefraction
        {
            get;
            set;
        }

        public bool IsVisible
        {
            get;
            set;
        }

        public void CleanUp(GraphicFactory factory)
        {
            _terrainModel.CleanUp();   
        }

#if WINDOWS
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
#endif
        
        public void AfterAdded(SceneControl.IObject obj)
        {
        }
    }
}
