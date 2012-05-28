using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;
using PloobsEngine;

namespace EngineTestes.LightPrePassIdea.obj
{
    class PrePassShader : IShader
    {
        public override MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.DEFERRED; }
        }

        Effect _shader2;
        Effect _shader;
        public override void Initialize(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {

            _shader2 = factory.GetEffect("PrePass2/ShaderPass");
            _shader = factory.GetEffect("PrePass2/renderobj");
            base.Initialize(ginfo, factory, obj);
        }

        public override void PosDrawPhase(GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.RenderHelper render, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights)
        {
            Matrix wld = obj.WorldMatrix;
            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {                                       
                    this._shader2.Parameters["DiffuseMap"].SetValue(obj.Modelo.GetTextureInformation(i)[j].getTexture(TextureType.DIFFUSE));
                    Matrix w1 = Matrix.Multiply(bi[j].ModelLocalTransformation, wld);
                    this._shader2.Parameters["worldViewProjection"].SetValue(w1 * cam.ViewProjection);
                    this._shader2.Parameters["lightSamplerBuffer"].SetValue(render[PrincipalConstants.lightRt]);
                    this._shader2.Parameters["LightBufferPixelSize"].SetValue(new Vector2(0.5f / render[PrincipalConstants.lightRt].Width, 0.5f / render[PrincipalConstants.lightRt].Height));
                    render.RenderBatch(bi[j], _shader2);
                }
            }       
        }

        protected override void Draw(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.RenderHelper render, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights)
        {
            this._shader.Parameters["FarClip"].SetValue(cam.FarPlane);
            this._shader.Parameters["View"].SetValue(cam.View);
            this._shader.Parameters["Projection"].SetValue(cam.Projection);            

            Matrix wld = obj.WorldMatrix;
            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {
                    
                    Matrix w1 = Matrix.Multiply(bi[j].ModelLocalTransformation, wld);
                    this._shader.Parameters["World"].SetValue(w1);
                    this._shader.Parameters["VPIT"].SetValue(Matrix.Transpose(Matrix.Invert(w1 * cam.View)));
                    render.RenderBatch(bi[j], _shader);
                }
            }
        }

    }
}
