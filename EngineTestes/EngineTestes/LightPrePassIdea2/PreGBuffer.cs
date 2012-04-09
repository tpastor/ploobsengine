#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
#if !WINDOWS_PHONE && !REACH
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

namespace PloobsEngine.SceneControl
{
    public class PreGBuffer : IDeferredGBuffer
    {           
        private RenderTarget2D normalRT; //this Render Target will hold normals and Specular Power
        private RenderTarget2D depthRT; //finally, this one will hold the depth
        private RenderTarget2D lightOclusionRT; //finally, this one will hold the depth
        private Effect clearBufferEffect;
#region IDeferredGBuffer Members        


        public Microsoft.Xna.Framework.Graphics.Texture2D this[GBufferTypes type]
        {
            get {

                switch (type)
                {
                    case GBufferTypes.DEPH:
                        return depthRT;                    
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
            render.PushRenderTarget(normalRT, depthRT);            
        }

        public void ResolveGBuffer(RenderHelper render)
        {
            render.PopRenderTarget();                
        }

        public void ClearGBuffer(RenderHelper render)
        {
            render.ValidateSamplerStates();      
            render.PushDepthStencilState(DepthStencilState.None);
            render.RenderFullScreenQuadVertexPixel(clearBufferEffect);
            render.PopDepthStencilState();      
            render.ValidateSamplerStates();
        }

        public void PreDrawScene(GameTime gameTime, IWorld world, RenderHelper render, GraphicInfo ginfo, List<IObject> objectsToPreDraw)
        {
            render.ValidateSamplerStates();
            foreach (IObject item in objectsToPreDraw)
            {
                if(item.Material.IsVisible)
                    item.Material.PreDrawnPhase(gameTime,world, item,world.CameraManager.ActiveCamera, world.Lights, render);                
            }            
            render.ValidateSamplerStates();
        }

        public void DrawScene(GameTime gameTime, IWorld world, RenderHelper render, GraphicInfo ginfo, List<IObject> objectsToDraw)
        {
            render.ValidateSamplerStates();

            Matrix view = world.CameraManager.ActiveCamera.View;
            Matrix projection = world.CameraManager.ActiveCamera.Projection;

            if (render.RenderPreComponents(gameTime, ref view, ref projection) > 0)
            {
                render.SetSamplerStates(ginfo.SamplerState);                
            }

            System.Diagnostics.Debug.Assert(render.PeekBlendState() == BlendState.Opaque);
            System.Diagnostics.Debug.Assert(render.PeekDepthState() == DepthStencilState.Default);
            System.Diagnostics.Debug.Assert(render.PeekRasterizerState() == RasterizerState.CullCounterClockwise);

            render.DettachBindedTextures(5);

            foreach (IObject item in objectsToDraw)
            {
                if(item.Material.IsVisible)
                    item.Material.Drawn(gameTime,item, world.CameraManager.ActiveCamera, world.Lights, render);                
            }

            render.ValidateSamplerStates();                 

        }

        bool useFloatBuffer;
        public void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, Color BackGroundColor,bool useFloatBuffer)
        {
            this.useFloatBuffer = useFloatBuffer;                                    
            normalRT = factory.CreateRenderTarget(ginfo.BackBufferWidth , ginfo.BackBufferHeight, SurfaceFormat.Color, false, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);
            depthRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Single, false, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents);
            //lightOclusionRT = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, multisample, RenderTargetUsage.DiscardContents);            
            clearBufferEffect = manager.GetAsset<Effect>("PrePass2/ClearGBuffer");
        }

        #endregion
    }
}
#endif