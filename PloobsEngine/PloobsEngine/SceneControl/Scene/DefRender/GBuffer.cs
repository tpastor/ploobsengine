using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Material;
using PloobsEngine.Engine.Logger;
using System.IO;

namespace PloobsEngine.SceneControl
{
    public class GBuffer : IDeferredGBuffer
    {   
        private RenderTarget2D colorRT;    //this Render Target will hold color and Specular Intensity
        private RenderTarget2D normalRT; //this Render Target will hold normals and Specular Power
        private RenderTarget2D depthRT; //finally, this one will hold the depth
        private RenderTarget2D lightOclusionRT; //finally, this one will hold the depth
        private Effect clearBufferEffect;
        private Color backGroundColor;        
        #region IDeferredGBuffer Members        

        public Color BackGroundColor
        {
            get { return backGroundColor; }
            set { backGroundColor = value; }
        }         

        public Microsoft.Xna.Framework.Graphics.Texture2D this[GBufferTypes type]
        {
            get {

                switch (type)
                {
                    case GBufferTypes.DEPH:
                        return depthRT;
                    case GBufferTypes.COLOR:
                        return colorRT;
                    case GBufferTypes.NORMAL:
                        return normalRT;
                    case GBufferTypes.Extra1:
                        return lightOclusionRT;                                            
                    default:
                        ActiveLogger.LogMessage("Invalid Buffer requested", LogLevel.FatalError);
                        throw new Exception("This GBUFFER dont use this Buffer Type");                        
                }
            }
        }
        
        #endregion

        #region IDeferredGBuffer Members

        public void SetGBuffer(RenderHelper render)
        {
            render.PushRenderTarget(colorRT, normalRT, depthRT, lightOclusionRT);            
        }

        public void ResolveGBuffer(RenderHelper render)
        {
            render.PopRenderTarget();                
        }

        public void ClearGBuffer(RenderHelper render)
        {
            render.PushDepthState(DepthStencilState.None);
            clearBufferEffect.Parameters["BackColor"].SetValue(backGroundColor.ToVector3());
            render.RenderFullScreenQuadVertexPixel(clearBufferEffect);
            render.PushDepthState(DepthStencilState.Default);
        }

        public void PreDrawScene(GameTime gameTime, IWorld world, RenderHelper render)
        {            
            foreach (IObject item in world.Objects)
            {
                item.Material.PreDrawnPhase(gameTime,world, item,world.CameraManager.ActiveCamera, world.Lights, render);
            }
        }

        public void DrawScene(GameTime gameTime, IWorld world, RenderHelper render)
        {            
            render.RenderPreComponents(gameTime, world.CameraManager.ActiveCamera.View, world.CameraManager.ActiveCamera.Projection);
            System.Diagnostics.Debug.Assert(render.PeekBlendState() == BlendState.Opaque);
            System.Diagnostics.Debug.Assert(render.PeekDepthState() == DepthStencilState.Default);
            System.Diagnostics.Debug.Assert(render.PeekRasterizerState() == RasterizerState.CullCounterClockwise);

            foreach (IObject item in world.Culler.GetNotCulledObjectsList(MaterialType.DEFERRED))
            {
                item.Material.Drawn(gameTime,item, world.CameraManager.ActiveCamera, world.Lights, render);
            }                    
        }

        public void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, Color BackGroundColor)
        {
            this.backGroundColor = BackGroundColor;
            const int multisample = 1;
            colorRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, multisample, RenderTargetUsage.DiscardContents);
            normalRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, multisample, RenderTargetUsage.DiscardContents);
            depthRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Single, ginfo.UseMipMap, DepthFormat.None, multisample, RenderTargetUsage.DiscardContents);
            lightOclusionRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, multisample, RenderTargetUsage.DiscardContents);            
            clearBufferEffect = manager.GetAsset<Effect>("ClearGBuffer",true);
        }

        #endregion
    }
}
