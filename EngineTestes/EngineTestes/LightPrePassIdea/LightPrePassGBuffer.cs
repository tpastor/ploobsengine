using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Material;
using PloobsEngine.Engine.Logger;
using System.IO;
using PloobsEngine.Engine;
using PloobsEngine.Modelo;

/////////////////////////////////////////////////
/////////////////////////////////////////////////
/////////////////////////////////////////////////
//PROTOTYPE PHASE, NOT WORKING YET, INVOLVING CHANGING LOTS OF STUFFS, 
//AND I DONT KNOW if THIS IS GOING TO WORK
/////////////////////////////////////////////////
/////////////////////////////////////////////////
/////////////////////////////////////////////////

namespace PloobsEngine.SceneControl
{
    public class LightPrePassGBuffer : IDeferredGBuffer
    {   
        private RenderTarget2D colorRT;    //this Render Target will hold color and Specular Intensity
        private RenderTarget2D normalRT; //this Render Target will hold normals and Specular Power
        private RenderTarget2D depthRT; //finally, this one will hold the depth
        private RenderTarget2D lightOclusionRT; //finally, this one will hold the depth
        private RenderTarget2D colorRT2;
        private RenderTarget2D colorRTFINAL;
        private Effect clearBufferEffect;
        private Color backGroundColor;
        Effect effect;
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
                        return colorRTFINAL;
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
            
        }

        public void ResolveGBuffer(RenderHelper render)
        {            
        }

        public void ClearGBuffer(RenderHelper render)
        {
            render.PushDepthStencilState(DepthStencilState.None);
            clearBufferEffect.Parameters["BackColor"].SetValue(backGroundColor.ToVector3());
            render.RenderFullScreenQuadVertexPixel(clearBufferEffect);
            render.PopDepthStencilState();
        }

        public void PreDrawScene(GameTime gameTime, IWorld world, RenderHelper render, GraphicInfo ginfo,List<IObject> objs)
        {
            foreach (IObject item in objs)
            {
                item.Material.PreDrawnPhase(gameTime,world, item,world.CameraManager.ActiveCamera, world.Lights, render);
            }
        }

        public void DrawScene(GameTime gameTime, IWorld world, RenderHelper render, GraphicInfo ginfo, List<IObject> objs)
        {
            Matrix v = world.CameraManager.ActiveCamera.View;
            Matrix p = world.CameraManager.ActiveCamera.Projection;
                    
            render.PushRenderTarget(colorRT, normalRT, depthRT, lightOclusionRT);
            
            render.RenderPreComponents(gameTime, ref v, ref p);
            System.Diagnostics.Debug.Assert(render.PeekBlendState() == BlendState.Opaque);
            System.Diagnostics.Debug.Assert(render.PeekDepthState() == DepthStencilState.Default);
            System.Diagnostics.Debug.Assert(render.PeekRasterizerState() == RasterizerState.CullCounterClockwise);
                        
            render.SetSamplerState(ginfo.SamplerState, 0);

            foreach (IObject item in objs)
            {
                item.Material.Drawn(gameTime,item, world.CameraManager.ActiveCamera, world.Lights, render);
            }

            render.PopRenderTarget();                

            colorRTFINAL = colorRT2;
            render.PushRenderTarget(colorRTFINAL);
            render.Clear(Color.CornflowerBlue);
            effect.Parameters["View"].SetValue(world.CameraManager.ActiveCamera.View);
            effect.Parameters["Projection"].SetValue(world.CameraManager.ActiveCamera.Projection);
            {

                foreach (IObject item in world.Culler.GetNotCulledObjectsList(MaterialType.DEFERRED))
                {                    
                    for (int i = 0; i < item.Modelo.MeshNumber; i++)
                    {
                        BatchInformation[] bi = item.Modelo.GetBatchInformation(i);
                        for (int j = 0; j < bi.Count(); j++)
                        {
                            effect.Parameters["Texture"].SetValue(item.Modelo.getTexture(TextureType.DIFFUSE,i,j));                    
                            effect.Parameters["World"].SetValue(bi[j].ModelLocalTransformation * item.WorldMatrix);
                            render.RenderBatch(bi[j], effect);
                        }
                    }
                }

            }
            render.PopRenderTarget();            

        }

        public void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, Color BackGroundColor)
        {
            this.backGroundColor = BackGroundColor;
            colorRT2 = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, 8, RenderTargetUsage.DiscardContents);
            colorRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents);
            normalRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            depthRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Single, ginfo.UseMipMap, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            lightOclusionRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);            
            clearBufferEffect = manager.GetAsset<Effect>("ClearGBuffer",true);
            effect = factory.GetEffect("Effects//hibe");
            
        }

        #endregion
    }
}
