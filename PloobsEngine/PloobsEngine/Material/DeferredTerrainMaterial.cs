#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Modelo;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Material
{
    public class DeferredTerrainMaterial : IMaterial
    {
        public DeferredTerrainMaterial(GraphicFactory factory, IShader shader, ICamera camera, QuadTerrainModel QuadTerrainModel, Vector3 position)
        {
            QuadTree = new Tutorial1.Terrain.QuadTree(position, QuadTerrainModel.vertexList, QuadTerrainModel.GetTextureInformation(0)[0].getTexture(TextureType.HEIGHTMAP), camera.View, camera.Projection, factory.device);
            QuadTerrainModel.vertexList.Clear();
            QuadTerrainModel.vertexList = null;
            CanAppearOfReflectionRefraction = true;
            CanCreateShadow = true;
            IsVisible = true;
            this.Shader = shader;
        }

        public Tutorial1.Terrain.QuadTree QuadTree
        {
            set;
            get;
        }

        public void Initialization(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, SceneControl.IObject obj)
        {
            Shader.Initialize(ginfo, factory, obj);
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
            Shader.PreDrawPhase(gt, mundo, obj, render, cam);
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
            Shader.PosDrawPhase(gt, obj, render, cam, lights);
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
            Shader.Draw(gt, obj, render, cam, lights);
        }                
        
        public void Update(Microsoft.Xna.Framework.GameTime gametime, SceneControl.IObject obj, IWorld world)
        {
            QuadTree.Update(gametime, world.CameraManager.ActiveCamera);
            QuadTree.UpdateBatchInformation(obj.Modelo.GetBatchInformation(0)[0]);
        }

        public IShader Shader
        {
            get;
            set;
        }

        public MaterialType MaterialType
        {
            get { return Material.MaterialType.DEFERRED; }
        }

        public bool CanCreateShadow
        {
            get;
            set;
        }

        public bool CanAppearOfReflectionRefraction
        {
            set;
            get;
        }

        public bool IsVisible
        {
            get;
            set;
        }

        public void CleanUp(Engine.GraphicFactory factory)
        {
            Shader.Cleanup(factory);
        }

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
        }

    }
}
#endif