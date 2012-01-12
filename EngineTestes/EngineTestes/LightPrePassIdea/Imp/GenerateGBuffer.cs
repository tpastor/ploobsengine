using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;
using PloobsEngine.Cameras;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.Material;
using PloobsEngine.Light;
using PloobsEngine;

namespace EngineTestes.LightPrePassIdea.Imp
{

    public class GenerateGBuffer
    {

        /// <summary>
        /// Our final corners, the 4 farthest points on the view space frustum
        /// </summary>
        private Vector3[] _currentFrustumCorners = new Vector3[4];
        private Vector3[] _localFrustumCorners = new Vector3[4];
               

        public GenerateGBuffer(GraphicFactory factory, GraphicInfo ginfo ,int width, int height)
        {
            _width = width;
            _height = height;            
            //_quadRenderer = new QuadRenderer();
            //_sphereRenderer = new MeshRenderer(contentManager.Load<Model>("defaultMeshes/sphere"));
            //_spotRenderer = new MeshRenderer(contentManager.Load<Model>("defaultMeshes/cone"));

            _cwDepthState = new DepthStencilState();
            _cwDepthState.DepthBufferWriteEnable = false;
            _cwDepthState.DepthBufferFunction = CompareFunction.LessEqual;

            _ccwDepthState = new DepthStencilState();
            _ccwDepthState.DepthBufferWriteEnable = false;
            _ccwDepthState.DepthBufferFunction = CompareFunction.GreaterEqual;

            _directionalDepthState = new DepthStencilState(); ;
            _directionalDepthState.DepthBufferWriteEnable = false;
            _directionalDepthState.DepthBufferFunction = CompareFunction.Greater;


            _depthStateDrawLights = new DepthStencilState();

            //we draw our volumes with front-face culling, so we have to use GreaterEqual here
            _depthStateDrawLights.DepthBufferFunction = CompareFunction.GreaterEqual;
            //with our z-buffer reconstructed we only need to read it
            _depthStateDrawLights.DepthBufferWriteEnable = false;
            
            _depthStateReconstructZ = new DepthStencilState
            {
                DepthBufferEnable = true,
                DepthBufferWriteEnable = true,
                DepthBufferFunction = CompareFunction.Always
            };

            _lightAddBlendState = new BlendState()
                {
                    AlphaSourceBlend = Blend.One,
                    ColorSourceBlend = Blend.One,
                    AlphaDestinationBlend = Blend.One,
                    ColorDestinationBlend = Blend.One,
                };

            CreateGBuffer(factory);
            LoadShaders(factory);
        }


        /// <summary>
        /// Depth states to render our light volume meshes
        /// </summary>
        private DepthStencilState _ccwDepthState;
        private DepthStencilState _cwDepthState;
        private DepthStencilState _directionalDepthState;
        private DepthStencilState _depthStateReconstructZ;
        private DepthStencilState _depthStateDrawLights;

        private BlendState _lightAddBlendState;

        //store the bindings for avoiding useless reallocations
        private RenderTargetBinding[] _lightAccumBinding = new RenderTargetBinding[2];
        private RenderTargetBinding[] _gBufferBinding = new RenderTargetBinding[2];


        /// <summary>
        /// GBuffer height
        /// </summary>
        private int _height;

        /// <summary>
        /// GBuffer width
        /// </summary>
        private int _width;

        /// <summary>
        /// This render target stores our zbuffer values
        /// </summary>
        private RenderTarget2D _depthBuffer;

        /// <summary>
        /// This render target is a downsampled version of the main depth buffer
        /// </summary>
        private RenderTarget2D _halfDepth;


        /// <summary>
        /// Effect to downsample zbuffer
        /// </summary>
        //private DownsampleDepthEffect _downsampleDepth;

        /// <summary>
        /// Keed track if we already downsampled the depth buffer fot this frame
        /// </summary>
        private bool _depthDownsampledThisFrame = false;

        /// <summary>
        /// This render target stores our normal + specular power values
        /// </summary>
        private RenderTarget2D _normalBuffer;

        /// <summary>
        /// This render target stores the light buffer, the sum of all lights
        /// applied to our scene
        /// </summary>
        private RenderTarget2D _lightBuffer;


        /// <summary>
        /// This render target stores the specular component of the light buffer, the sum of all lights
        /// applied to our scene
        /// </summary>
        private RenderTarget2D _lightSpecularBuffer;

        /// <summary>
        /// This render target stores our final composition
        /// </summary>
        private RenderTarget2D _outputTexture;

        /// <summary>
        /// Scratch buffers that are half the resolution of the main one
        /// </summary>
        private RenderTarget2D _halfBuffer0;
        private RenderTarget2D _halfBuffer1;

        /// <summary>
        /// Effect to reconstruct Z buffer from linear depth buffer
        /// </summary>
        private Effect _reconstructZBuffer;

        /// <summary>
        /// Effect that clears our GBuffer
        /// </summary>
        private Effect _clearGBuffer;

        /// <summary>
        /// Effect that performs the lighting 
        /// </summary>
        private Effect _lighting;

        /// <summary>
        /// Use screen-aligned quads for point lights
        /// </summary>
        private bool _useQuads = true;

        public  void CreateGBuffer(GraphicFactory factory)
        {
            //One of our premises is to do not use the PRESERVE CONTENTS flags, 
            //that is supposed to be more expensive than DISCARD CONTENT.
            //We use a floating point (32bit) buffer for Z values, although our HW use only 24bits.
            //We could use some packing and use a 24bit buffer too, but lets start simpler
            _depthBuffer = factory.CreateRenderTarget( _width, _height, SurfaceFormat.Single,false,
                                              DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            //the downsampled depth buffer must have the same format as the main one
            _halfDepth = factory.CreateRenderTarget(_width / 2, _height / 2,
                                              SurfaceFormat.Single, false, DepthFormat.None, 0,
                                              RenderTargetUsage.DiscardContents);

            //Our normal buffer stores encoded view-space normal into RG (10bit each) and the specular power in B.
            //Some engines encode the specular power with some log or ln functions. We will output 
            //only the normal texture's alpha channel multiplied by a const value (100),
            //so we have specular power in the range [1..100].
            //Currently, A is not used (2bit).
            _normalBuffer = factory.CreateRenderTarget(_width, _height, SurfaceFormat.Color, false,
                                               DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);

            //This buffer stores all the "pure" lighting on the scene, no albedo applied to it. We use an floating
            //point format to allow us "overbright" some areas. Read the blog for more information. We use a depth buffer
            //to optimize light rendering.
            _lightBuffer = factory.CreateRenderTarget(_width, _height, SurfaceFormat.HdrBlendable,false,
                                              DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);

            //we need a separate texture for the specular, since the xbox doesnt allow a RGBA64 buffer
            _lightSpecularBuffer = factory.CreateRenderTarget(_width, _height, 
                                             SurfaceFormat.HdrBlendable, false,DepthFormat.None, 0,
                                             RenderTargetUsage.DiscardContents);

            //We need another depth here because we need to render all objects again, to reconstruct their shading 
            //using our light texture.
            _outputTexture = factory.CreateRenderTarget(_width, _height, SurfaceFormat.Color,false,
                                                DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);

            int halfRes = 2;
            _halfBuffer0 = factory.CreateRenderTarget(_width / halfRes, _height / halfRes, 
                                              SurfaceFormat.Color,false, DepthFormat.None, 0,
                                              RenderTargetUsage.DiscardContents);
            _halfBuffer1 = factory.CreateRenderTarget(_width / halfRes, _height / halfRes,
                                              SurfaceFormat.Color, false, DepthFormat.None, 0,
                                              RenderTargetUsage.DiscardContents);

            _gBufferBinding[0] = new RenderTargetBinding(_normalBuffer);
            _gBufferBinding[1] = new RenderTargetBinding(_depthBuffer);

            _lightAccumBinding[0] = new RenderTargetBinding(_lightBuffer);
            _lightAccumBinding[1] = new RenderTargetBinding(_lightSpecularBuffer);
        }

        private void LoadShaders(GraphicFactory factory)
        {
            _clearGBuffer = factory.GetEffect("PrePass/ClearGBuffer");
            _lighting = factory.GetEffect("PrePass/LightingLpp");
            _reconstructZBuffer = factory.GetEffect("PrePass/ReconstructDepth");
        }

        public RenderTarget2D RenderScene(RenderHelper render, ICamera camera, IWorld world, GameTime gameTime, GraphicInfo ginfo)
        {
            
            ComputeFrustumCorners(camera);
            
            //first of all, we must bind our GBuffer and reset all states
            render.PushRenderTargetBinding(_gBufferBinding);

            render.Clear(Color.Black,ClearOptions.DepthBuffer | ClearOptions.Stencil, 1.0f, 0);           

            render.PushDepthStencilState(DepthStencilState.None);
            render.PushRasterizerState(RasterizerState.CullNone);

            render.RenderFullScreenQuadVertexPixel(_clearGBuffer);

            render.PopDepthStencilState();
            render.PopRasterizerState();

            //now, render them to the G-Buffer

            world.Culler.StartFrame(world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection, world.CameraManager.ActiveCamera.BoundingFrustum);
            List<IObject> AllnotCulledObjectsList = world.Culler.GetNotCulledObjectsList(null);
            List<IObject> DeferrednotCulledObjectsList = world.Culler.GetNotCulledObjectsList(MaterialType.DEFERRED);
            List<IObject> ForwardnotCulledObjectsList = world.Culler.GetNotCulledObjectsList(MaterialType.FORWARD);


            foreach (IObject item in DeferrednotCulledObjectsList)
            {
                if (item.Material.IsVisible)
                    item.Material.Drawn(gameTime, item, world.CameraManager.ActiveCamera, world.Lights, render);
            }

            _depthBuffer = render.PopRenderTarget()[1].RenderTarget as RenderTarget2D;

            render.PushRenderTargetBinding(_lightAccumBinding);
            render.Clear(new Color(0, 0, 0, 0));

            render.PushDepthStencilState(DepthStencilState.None);            

            //draw using additive blending. 
            //At first I was using BlendState.additive, but it seems to use alpha channel for modulation, 
            //and as we use alpha channel as the specular intensity, we have to create our own blend state here
            render.PushBlendState(_lightAddBlendState);           
 
            RenderLights(camera,world,render,ginfo);

            render[PrincipalConstants.lightRt] = render.PopRenderTarget()[0].RenderTarget as Texture2D;
            render.PopDepthStencilState();
            render.PopBlendState();

            //reconstruct each object shading, using the light texture as input (and another specific parameters too)
            
            //render.PushRenderTarget(_outputTexture);

            render.Clear(Color.Black);

            //reconstruct the shading, using the already culled list
            //ReconstructShading(camera);
            foreach (IObject item in AllnotCulledObjectsList)
            {
                if (item.Material.IsVisible)
                    item.Material.PosDrawnPhase(gameTime, item, world.CameraManager.ActiveCamera, world.Lights, render);
            }
            //render objects that doesn't need the lightbuffer information, such as skyboxes, pure reflective meshes, etc
            //DrawOpaqueObjects(camera);
            //draw objects with transparency
            //DrawBlendObjects(camera);

            
            //unbind our final buffer and return it
            //render.PopRenderTarget();            

            return _outputTexture;
        }

        private void RenderLights(ICamera camera, IWorld world, RenderHelper render, GraphicInfo ginfo)
        {
            _lighting.Parameters["halfPixel"].SetValue(ginfo.HalfPixel);
            _lighting.Parameters["DepthBuffer"].SetValue(_depthBuffer);
            _lighting.Parameters["NormalBuffer"].SetValue(_normalBuffer);

            //just comment this line if you dont want to reconstruct the zbuffer
            //ReconstructZBuffer(camera);

            //float tang = (float)Math.Tan(camera.FieldOfView * 0.5f);
            //_lighting.Parameters["TanAspect"].SetValue(new Vector2(tang * camera.AspectRatio, -tang));


            ApplyFrustumCorners(_lighting, -Vector2.One, Vector2.One);

            for (int i = 0; i < world.Lights.Count; i++)
            {
                DirectionalLightPE dl = (DirectionalLightPE)world.Lights[i];
                _lighting.Parameters["LightColor"].SetValue(dl.Color.ToVector4());
                _lighting.Parameters["LightDir"].SetValue(dl.LightDirection);
                render.RenderFullScreenQuadVertexPixel(_lighting);

            }
        }

        public void ApplyFrustumCorners(Effect effect, Vector2 topLeftVertex, Vector2 bottomRightVertex)
        {
            ApplyFrustumCorners(effect.Parameters["FrustumCorners"], topLeftVertex, bottomRightVertex);
        }
        public void ApplyFrustumCorners(EffectParameter frustumCorners, Vector2 topLeftVertex, Vector2 bottomRightVertex)
        {
            float dx = _currentFrustumCorners[1].X - _currentFrustumCorners[0].X;
            float dy = _currentFrustumCorners[0].Y - _currentFrustumCorners[2].Y;

            _localFrustumCorners[0] = _currentFrustumCorners[2];
            _localFrustumCorners[0].X += dx * (topLeftVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[0].Y += dy * (bottomRightVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[1] = _currentFrustumCorners[2];
            _localFrustumCorners[1].X += dx * (bottomRightVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[1].Y += dy * (bottomRightVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[2] = _currentFrustumCorners[2];
            _localFrustumCorners[2].X += dx * (topLeftVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[2].Y += dy * (topLeftVertex.Y * 0.5f + 0.5f);

            _localFrustumCorners[3] = _currentFrustumCorners[2];
            _localFrustumCorners[3].X += dx * (bottomRightVertex.X * 0.5f + 0.5f);
            _localFrustumCorners[3].Y += dy * (topLeftVertex.Y * 0.5f + 0.5f);

            frustumCorners.SetValue(_localFrustumCorners);
        }

        /// <summary>
        /// Our frustum corners in world space
        /// </summary>
        private Vector3[] _cornersWorldSpace = new Vector3[8];

        /// <summary>
        /// Our frustum corners in view space
        /// </summary>
        private Vector3[] _cornersViewSpace = new Vector3[8];

        private void ComputeFrustumCorners(ICamera camera)
        {
            camera.BoundingFrustum.GetCorners(_cornersWorldSpace);
            Matrix matView = camera.View; //this is the inverse of our camera transform
            Vector3.Transform(_cornersWorldSpace, ref matView, _cornersViewSpace); //put the frustum into view space
            for (int i = 0; i < 4; i++) //take only the 4 farthest points
            {
                _currentFrustumCorners[i] = _cornersViewSpace[i + 4];
            }
            Vector3 temp = _currentFrustumCorners[3];
            _currentFrustumCorners[3] = _currentFrustumCorners[2];
            _currentFrustumCorners[2] = temp;
        }       

        
    }
}
