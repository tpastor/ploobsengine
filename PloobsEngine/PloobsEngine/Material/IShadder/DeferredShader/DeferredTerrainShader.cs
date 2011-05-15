using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Physic.PhysicObjects.BepuObject;
using BEPUphysics;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Light;

namespace PloobsEngine.Material
{
    /// <summary>
    /// Terrain Type
    /// </summary>
    public enum TerrainType
    {
        /// <summary>
        /// Use multitexture (THE TEXTURES MUST BE SPECIFIED IN THE IMODELO)
        /// </summary>
        MULTITEXTURE,
        /// <summary>
        /// Use only one texture for all the terrain
        /// </summary>
        SINGLETEXTURE
    }

    /// <summary>
    /// Terrain Shader
    /// </summary>
    public class DeferredTerrainShader : IShader
    {
        public DeferredTerrainShader(TerrainType TerrainType)
        {
            this.terrainType = TerrainType;
        }

        private Effect _shader;
        private TerrainType terrainType;

        float nivelBaixoAltura = 0;

        /// <summary>
        /// Default 0 
        /// </summary>        
        public float NivelBaixoAltura
        {
            get { return nivelBaixoAltura; }
            set { nivelBaixoAltura = value; }
        }
        float nivelBaixoEspalhamento = 15;

        /// <summary>
        /// Default 15
        /// </summary>
        public float NivelBaixoEspalhamento
        {
            get { return nivelBaixoEspalhamento; }
            set { nivelBaixoEspalhamento = value; }
        }
        float nivelMedioAltura = 20;

        /// <summary>
        /// Default 20
        /// </summary>
        public float NivelMedioAltura
        {
            get { return nivelMedioAltura; }
            set { nivelMedioAltura = value; }
        }
        float nivelMedioEspalhamento = 20;

        /// <summary>
        /// Default 20
        /// </summary>
        public float NivelMedioEspalhamento
        {
            get { return nivelMedioEspalhamento; }
            set { nivelMedioEspalhamento = value; }
        }
        float nivelAltoAltura = 80;

        /// <summary>
        /// Default 80
        /// </summary>
        public float NivelAltoAltura
        {
            get { return nivelAltoAltura; }
            set { nivelAltoAltura = value; }
        }

        float nivelAltoEspalhamento = 40;

        /// <summary>
        /// Default 40
        /// </summary>
        public float NivelAltoEspalhamento
        {
            get { return nivelAltoEspalhamento; }
            set { nivelAltoEspalhamento = value; }
        }

        float nivelBaseAltura = 40;

        /// <summary>
        /// Default 40
        /// </summary>
        public float NivelBaseAltura
        {
            get { return nivelBaseAltura; }
            set { nivelBaseAltura = value; }
        }
        float nivelBaseEspalhamento = 25;
        /// <summary>
        /// Default 25
        /// </summary>
        public float NivelBaseEspalhamento
        {
            get { return nivelBaseEspalhamento; }
            set { nivelBaseEspalhamento = value; }
        }
        float nearTextureEspalhamento = 0;

        /// <summary>
        /// Default 0
        /// </summary>
        public float NearTextureEspalhamento
        {
            get { return nearTextureEspalhamento; }
            set { nearTextureEspalhamento = value; }
        }

        float farTextureEspalhamento = 100;

        /// <summary>
        /// Default 100
        /// </summary>
        public float FarTextureEspalhamento
        {
            get { return farTextureEspalhamento; }
            set { farTextureEspalhamento = value; }
        }

        float blendDistance = 0.99f;

        /// <summary>
        /// Default 0.99f
        /// </summary>
        public float BlendDistance
        {
            get { return blendDistance; }
            set { blendDistance = value; }
        }
        float blendWidth = 0.009f;

        /// <summary>
        /// Default 0.009f
        /// </summary>
        public float BlendWidth
        {
            get { return blendWidth; }
            set { blendWidth = value; }
        }

        public override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
                System.Diagnostics.Debug.Assert(obj.Modelo is TerrainModel, "This shader expects a TerrainModel");
                this._shader.Parameters["id"].SetValue(shaderId);                
                this._shader.Parameters["BaseTexture"].SetValue(obj.Modelo.getTexture(TextureType.MULTITEX1));
                this._shader.Parameters["blendDistance"].SetValue(blendDistance);
                this._shader.Parameters["blendWidth"].SetValue(blendWidth);
                this._shader.Parameters["nearTextureEspalhamento"].SetValue(nearTextureEspalhamento);
                this._shader.Parameters["farTextureEspalhamento"].SetValue(farTextureEspalhamento);
                Matrix World = obj.WorldMatrix;
                this._shader.Parameters["World"].SetValue(World);

                if (terrainType == TerrainType.MULTITEXTURE)
                {
                    this._shader.Parameters["NivelAlto"].SetValue(obj.Modelo.getTexture(TextureType.MULTITEX3));
                    this._shader.Parameters["NivelMedio"].SetValue(obj.Modelo.getTexture(TextureType.MULTITEX2));
                    this._shader.Parameters["NivelBaixo"].SetValue(obj.Modelo.getTexture(TextureType.MULTITEX1));

                    this._shader.Parameters["nivelBaixoAltura"].SetValue(nivelBaixoAltura);
                    this._shader.Parameters["nivelBaixoEspalhamento"].SetValue(nivelBaixoEspalhamento);
                    this._shader.Parameters["nivelMedioAltura"].SetValue(nivelMedioAltura);
                    this._shader.Parameters["nivelMedioEspalhamento"].SetValue(nivelMedioEspalhamento);
                    this._shader.Parameters["nivelAltoAltura"].SetValue(nivelAltoAltura);
                    this._shader.Parameters["nivelAltoEspalhamento"].SetValue(nivelAltoEspalhamento);
                    this._shader.Parameters["nivelBaseAltura"].SetValue(nivelBaseAltura);
                    this._shader.Parameters["nivelBaseEspalhamento"].SetValue(nivelBaseEspalhamento);
                }
                
                this._shader.Parameters["ViewProjection"].SetValue(cam.ViewProjection);
                BatchInformation batchInfo = obj.Modelo.GetBatchInformation(0)[0];

                //render.PushRasterizerState(RasterizerState.CullNone);
                render.RenderBatch(batchInfo, this._shader);
                //render.PopRasterizerState();
      }       

        public override MaterialType MaterialType
        {
            get { return MaterialType.DEFERRED; }
        }

        public override void Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
        {
            this._shader = factory.GetEffect("Terrain",true,true);            
            if (terrainType == TerrainType.SINGLETEXTURE)
            {
                this._shader.CurrentTechnique = this._shader.Techniques["Technique1"];
            }
            else
            {
                this._shader.CurrentTechnique = this._shader.Techniques["MultiTexture"];
            }
        }

    }
}
