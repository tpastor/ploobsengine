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
        Effect TerrainShader;

        public ForwardTerrainMaterial(QuadTerrain QuadTerrain,float lod = 3.5f)
        {
            TerrainShader = QuadTerrain.factory.GetEffect("terrainShader", false, true);
            this.QuadTerrain = QuadTerrain;
            CanAppearOfReflectionRefraction = false;
            CanCreateShadow = false;
            IsVisible = true;            
            LOD = lod;

            WorldMatrix = TerrainShader.Parameters["World"];
            ViewMatrix = TerrainShader.Parameters["View"];
            ProjectionMatrix = TerrainShader.Parameters["Projection"];
            sunDir = TerrainShader.Parameters["sunlightVector"];
            sunCol = TerrainShader.Parameters["lightColour"];
            ambCol = TerrainShader.Parameters["ambientColour"];
        }

        ////////////////////////////////////////////////////////////////////
        //These shader parameters are more dynamic, so they function through a reference.
        //This means they can be set every frame without noticable slowdown.
        //Sun direction needs to be converted to work correctly in the shader, because the normal maps use Blue as the Up vector.
        private EffectParameter sunDir;
        private Vector3 sunlightRealVector;
        private float tempZ;
        public Vector3 sunlightVector
        {

            set
            {
                sunlightRealVector = value;
                tempZ = sunlightRealVector.Z;
                sunlightRealVector.Z = sunlightRealVector.Y;
                sunlightRealVector.Y = tempZ;
                sunDir.SetValue(sunlightRealVector);
            }
        }
        private EffectParameter sunCol;
        public Vector3 sunlightColour
        {
            set { sunCol.SetValue(value); }
        }
        private EffectParameter ambCol;
        public Vector3 ambientColour
        {
            set { ambCol.SetValue(value); }
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

        //Various Shader parameters.
        public Texture2D textureDetail
        {
            set { TerrainShader.Parameters["detailTexture"].SetValue(value); }
        }
        public Texture2D textureBlend
        {
            set { TerrainShader.Parameters["BlendTexture"].SetValue(value); }
        }
        public Texture2D textureRed
        {
            set { TerrainShader.Parameters["RTexture"].SetValue(value); }
        }
        public Texture2D textureGreen
        {
            set { TerrainShader.Parameters["GTexture"].SetValue(value); }
        }
        public Texture2D textureBlue
        {
            set { TerrainShader.Parameters["BTexture"].SetValue(value); }
        }
        public Texture2D textureBlack
        {
            set { TerrainShader.Parameters["BaseTexture"].SetValue(value); }
        }

        public float diffuseScale
        {
            set { TerrainShader.Parameters["diffuseScale"].SetValue(value); }
        }
        public float detailScale
        {
            set { TerrainShader.Parameters["detailScale"].SetValue(value); }
        }
        public float detailMapStrength
        {
            set { TerrainShader.Parameters["detailMapStrength"].SetValue(value); }
        }

        public void Initialization(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, SceneControl.IObject obj)
        {
            //Add the normal texture to the shader
            TerrainShader.Parameters["NormalTexture"].SetValue(QuadTerrain.globalNormalTexture);
            //Set the Global Scale (used for the normal and blending textures) value in the shader.
            TerrainShader.Parameters["globalScale"].SetValue(QuadTerrain.flatScale * QuadTerrain.TerrainHeight); 
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

            TerrainShader.CurrentTechnique = TerrainShader.Techniques["Technique1"];
            TerrainShader.CurrentTechnique.Passes[0].Apply();

            QuadTerrain.DrawTerrain(TerrainShader, render.device);

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
