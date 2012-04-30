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
    /// WIP 
    /// implementing 
    /// http://developer.download.nvidia.com/presentations/2010/gdc/Direct3D_Effects.pdf
    /// </summary>
	public class FluidShader : IShader
	{
        public FluidShader()
            : base()
        {
            RasterizerState = new RasterizerState();
            RasterizerState.CullMode = CullMode.None;
            //RasterizerState.FillMode = FillMode.WireFrame;
        }
        RenderTarget2D r1;
        RenderTarget2D r2;
        RenderTarget2D r3;
        RenderTarget2D gaussian;

        public override MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.DEFERRED; }
        }

        public override void Update(GameTime gt, PloobsEngine.SceneControl.IObject ent, IList<PloobsEngine.Light.ILight> lights)
        {
            base.Update(gt, ent, lights);

            PhysxFluidObject PhysxFluidObject = ent.PhysicObject as PhysxFluidObject;
            FluidMOdel FluidMOdel = ent.Modelo as FluidMOdel;

            if (!PhysxFluidObject.Fluid.ParticleWriteData.NumberOfParticles.HasValue)
                return;

            Phyx.Vector3[] pos = PhysxFluidObject.Fluid.ParticleWriteData.PositionBuffer.GetData<Phyx.Vector3>();

            int j = 0;
            for (int i = 0; i < PhysxFluidObject.Fluid.ParticleWriteData.NumberOfParticles * 6; i+=6)
            {
                FluidMOdel.billboardVertices[i ].Position = pos[j].AsXNA();
                FluidMOdel.billboardVertices[i + 1].Position = pos[j].AsXNA();
                FluidMOdel.billboardVertices[i + 2].Position = pos[j].AsXNA();

                FluidMOdel.billboardVertices[i + 3].Position = pos[j].AsXNA();
                FluidMOdel.billboardVertices[i + 4].Position = pos[j].AsXNA();
                FluidMOdel.billboardVertices[i + 5].Position = pos[j].AsXNA();
                j++;
            }

            FluidMOdel.UpdateVertices(pos.Count());
        }        

        RasterizerState RasterizerState;


        public override void PreDrawPhase(GameTime gt, PloobsEngine.SceneControl.IWorld world, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.RenderHelper render, PloobsEngine.Cameras.ICamera cam)
        {
            Effect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(cam.Projection));
            Effect.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);

            Effect.Parameters["forward"].SetValue(Vector3.Normalize(cam.Target - cam.Position));
            Effect.Parameters["upVector"].SetValue(cam.Up);
            Effect.Parameters["xView"].SetValue(cam.View);
            Effect.Parameters["xProjection"].SetValue(cam.Projection);
            Effect.Parameters["scaleX"].SetValue(3.5f);
            Effect.Parameters["scaleY"].SetValue(3.5f);
            Effect.Parameters["sphereRadius"].SetValue(3f);
            Effect.CurrentTechnique = Effect.Techniques["FLUID0"];
            render.PushRenderTarget(r1);
            render.Clear(Color.Black);
            render.PushRasterizerState(RasterizerState);
            render.PushDepthStencilState(DepthStencilState.Default);
            render.RenderBatch(obj.Modelo.GetBatchInformation(0)[0], Effect);
            render.PopRasterizerState();
            render.PopDepthStencilState();
            render.PopRenderTarget();
            

            //Effect.CurrentTechnique = Effect.Techniques["FLUID2"];
            //render.PushRenderTarget(r2);
            //render.Clear(Color.Black);
            //Effect.Parameters["depth"].SetValue(r1);
            //Effect.Parameters["blurDepthFalloff"].SetValue(0);
            //Effect.Parameters["blurScale"].SetValue(1.5f);
            //Effect.Parameters["blurDir"].SetValue(new Vector2( 2 * ginfo.HalfPixel.X,0));
            //render.RenderFullScreenQuadVertexPixel(Effect);
            //render.PopRenderTarget();

            //Effect.CurrentTechnique = Effect.Techniques["FLUID2"];
            //render.PushRenderTarget(r1);
            //render.Clear(Color.Black);
            //Effect.Parameters["depth"].SetValue(r2);
            //Effect.Parameters["blurDepthFalloff"].SetValue(0);
            //Effect.Parameters["blurScale"].SetValue(1.5f);
            //Effect.Parameters["blurDir"].SetValue(new Vector2(0, 2 * ginfo.HalfPixel.Y));
            //render.RenderFullScreenQuadVertexPixel(Effect);
            //render.PopRenderTarget();

            Effect.Parameters["blurDepthFalloff"].SetValue(20);
            render.PushRenderTarget(r2);
            render.Clear(Color.Black);
            PerformGaussianBLur(render, r1);
            render.PopRenderTarget();

            render.PushRenderTarget(r1);
            render.Clear(Color.Black);
            PerformGaussianBLur(render, r2);
            render.PopRenderTarget();
            
            render.PushRenderTarget(r3);
            render.Clear(Color.Black);
            Effect.CurrentTechnique = Effect.Techniques["FLUID1"];
            Effect.Parameters["depth"].SetValue(r1);
            render.RenderFullScreenQuadVertexPixel(Effect);
            render.PopRenderTarget();           

            //render.PushRenderTarget(r3);
            //render.Clear(Color.Black);
            //GaussianBlurPostEffect.Draw(r2, render, gt, ginfo, world, false);
            //render.PopRenderTarget();

            //render.PushRenderTarget(r2);
            //render.Clear(Color.Black);
            //GaussianBlurPostEffect.Draw(r3, render, gt, ginfo, world, false);
            //render.PopRenderTarget();                        

        }

        protected override void Draw(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.RenderHelper render, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights)
        {
            Effect.CurrentTechnique = Effect.Techniques["FLUID3"];
            Effect.Parameters["depth"].SetValue(r1);
            Effect.Parameters["normal"].SetValue(r3);
            render.RenderFullScreenQuadVertexPixel(Effect);
            //render.RenderTextureComplete(r3);

            render.SetSamplerStates(ginfo.SamplerState);
        }

        Effect Effect;
        PloobsEngine.Engine.GraphicInfo ginfo;
        public override void Initialize(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {
            this.ginfo = ginfo;
            Effect = factory.GetEffect("Effects/fluid");
            r1 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight,SurfaceFormat.Single);
            r2 = factory.CreateRenderTarget(ginfo.BackBufferWidth , ginfo.BackBufferHeight , SurfaceFormat.Single);
            r3 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color);
            gaussian = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Single);
            ComputeKernel(BLUR_RADIUS, BLUR_AMOUNT);
            ComputeOffsets(ginfo.BackBufferWidth, ginfo.BackBufferHeight);
        }

        private const int BLUR_RADIUS = 15;
        private const float BLUR_AMOUNT = 2f;
        private int radius;
        private float amount;
        private float sigma;
        private float[] kernel;
        private Vector2[] offsetsHoriz;
        private Vector2[] offsetsVert;
        
        public void ComputeKernel(int blurRadius, float blurAmount)
        {
            radius = blurRadius;
            amount = blurAmount;

            kernel = null;
            kernel = new float[radius * 2 + 1];
            sigma = radius / amount;

            float twoSigmaSquare = 2.0f * sigma * sigma;
            float sigmaRoot = (float)Math.Sqrt(twoSigmaSquare * Math.PI);
            float total = 0.0f;
            float distance = 0.0f;
            int index = 0;

            for (int i = -radius; i <= radius; ++i)
            {
                distance = i * i;
                index = i + radius;
                kernel[index] = (float)Math.Exp(-distance / twoSigmaSquare) / sigmaRoot;
                total += kernel[index];
            }

            for (int i = 0; i < kernel.Length; ++i)
                kernel[i] /= total;
        }

        
        public void ComputeOffsets(float textureWidth, float textureHeight)
        {
            offsetsHoriz = null;
            offsetsHoriz = new Vector2[radius * 2 + 1];

            offsetsVert = null;
            offsetsVert = new Vector2[radius * 2 + 1];

            int index = 0;
            float xOffset = 1.0f / textureWidth;
            float yOffset = 1.0f / textureHeight;

            for (int i = -radius; i <= radius; ++i)
            {
                index = i + radius;
                offsetsHoriz[index] = new Vector2(i * xOffset, 0.0f);
                offsetsVert[index] = new Vector2(0.0f, i * yOffset);
            }
        }
        public void PerformGaussianBLur(RenderHelper render,Texture2D source)
        {
            render.PushRenderTarget(gaussian);
            render.Clear(Color.Black);
            Effect.CurrentTechnique = Effect.Techniques["GAUSS"];
            Effect.Parameters["weights"].SetValue(kernel);
            Effect.Parameters["depth"].SetValue(source);
            Effect.Parameters["offsets"].SetValue(offsetsHoriz);
            render.RenderFullScreenQuadVertexPixel(Effect);
            render.PopRenderTarget();

            // Perform vertical Gaussian blur.            
            Effect.Parameters["depth"].SetValue(gaussian);
            Effect.Parameters["offsets"].SetValue(offsetsVert);

            render.RenderFullScreenQuadVertexPixel(Effect);            
        }

	}
}
