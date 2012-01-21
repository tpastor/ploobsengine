using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Modelo;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.Material
{
    public class ForwardTerrainMaterial : IMaterial
    {
        public ForwardTerrainMaterial(QuadTerrain QuadTerrain,float lod = 3.5f)
        {
            this.QuadTerrain = QuadTerrain;
            CanAppearOfReflectionRefraction = false;
            CanCreateShadow = false;
            IsVisible = true;            
            LOD = lod;

            WorldMatrix = QuadTerrain.TerrainShader.Parameters["World"];
            ViewMatrix = QuadTerrain.TerrainShader.Parameters["View"];
            ProjectionMatrix = QuadTerrain.TerrainShader.Parameters["Projection"];
        }

        EffectParameter WorldMatrix;
        EffectParameter ViewMatrix;
        EffectParameter ProjectionMatrix;

        public QuadTerrain QuadTerrain
        {
            internal set;
            get;
        }

        public float LOD
        {
            set;
            get;
        }

        public void Initialization(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, SceneControl.IObject obj)
        {
            
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
            WorldMatrix.SetValue(obj.PhysicObject.WorldMatrix);
            ViewMatrix.SetValue(cam.View);
            ProjectionMatrix.SetValue(cam.Projection);

            QuadTerrain.DrawTerrain(render.device);
            render.ResyncStates();
        }

        public void Update(Microsoft.Xna.Framework.GameTime gametime, SceneControl.IObject obj, IWorld world)
        {
            ICamera cam =  world.CameraManager.ActiveCamera;
            QuadTerrain.UpdateTerrain(cam.Position, cam.BoundingFrustum, LOD);
        }

        public IShader Shader
        {
            get
            {
                return null;
            }
            set
            {
                throw new Exception("Cant set Shader of Terrain Material");
            }
        }

        public MaterialType MaterialType
        {
            get { return Material.MaterialType.FORWARD; }
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
            QuadTerrain.CleanUp();
        }

#if WINDOWS
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
        }
#endif

    }
}
