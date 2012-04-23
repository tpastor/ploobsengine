using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using Phyx = StillDesign.PhysX.MathPrimitives;
using PloobsEngine.SceneControl;

namespace EngineTestes
{
 
    /// <summary>
    /// implement the light model you want in the shader and bake it to a texture =P
    /// </summary>
	public class BakerShader : IShader
	{
        public BakerShader()
            : base()
        {
          
        }
        
        public override MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.FORWARD; }
        }
        Effect Effect;
        public override void Initialize(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory, IObject obj)
        {
            Effect = factory.GetEffect("Effects/baker");
            base.Initialize(ginfo, factory, obj);
            Effect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
            rt = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
        }

        RenderTarget2D rt;
        protected override void Draw(GameTime gt, IObject obj, RenderHelper render, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights)
        {
            //render.PushRenderTarget(rt);

            Effect.Parameters["lightMapping"].SetValue(true);
            Effect.Parameters["projection"].SetValue(cam.Projection);
            Effect.Parameters["view"].SetValue(cam.View);

            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {
                    Effect.Parameters["diffuse"].SetValue(obj.Modelo.getTexture(PloobsEngine.Modelo.TextureType.DIFFUSE, i, j));
                    Effect.Parameters["uvcorrection"].SetValue(new Vector2(1,1));
                    Effect.Parameters["world"].SetValue(bi[j].ModelLocalTransformation * obj.WorldMatrix); 
                    render.RenderBatch(bi[j], Effect);
                }
            }

            //render.PopRenderTarget();
            //rt.SaveAsPng(...)
            
        }

        
	}
}
