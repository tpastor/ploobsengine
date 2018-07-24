﻿#region License
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
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Utils;

namespace PloobsEngine.Engine
{   
    /// <summary>
    /// Contains Graphics Informations about the current execution
    /// </summary>
    public class GraphicInfo
    {
        internal GraphicInfo(int BackBufferHeight, int BackBufferWidth, Rectangle FullScreenRectangle, Vector2 halfPixel, GraphicsDevice device, int MultiSample, DepthFormat DepthFormat, bool useMipMap, EngineStuff engine, SamplerState DefaultSamplerState)
        {
            this.BackBufferHeight = BackBufferHeight;
            this.BackBufferWidth = BackBufferWidth;
            this.FullScreenRectangle = FullScreenRectangle;
            this.HalfPixel = halfPixel;            
            this.device = device;
            Viewport = device.Viewport;
            this.MultiSample = MultiSample;
            this.DepthFormat = DepthFormat;
            this.UseMipMap = useMipMap;
            this.EngineStuff = engine;
#if !XBOX && !WINDOWS_PHONE
            this.window = (System.Windows.Forms.Form) System.Windows.Forms.Form.FromHandle(engine.Window.Handle);
#endif
            
            SamplerState = DefaultSamplerState;
#if !MONO        
    #if !WINDOWS_PHONE || SILVER
                GraphicsAdapter = engine.GraphicsDevice.Adapter;
    #else
                GraphicsAdapter = engine.game.GraphicsDevice.Adapter;
    #endif
#endif
        }

        internal void ChangeProps(int BackBufferHeight, int BackBufferWidth, Rectangle FullScreenRectangle, Vector2 halfPixel, GraphicsDevice device, int MultiSample, DepthFormat DepthFormat, bool useMipMap,SamplerState DefaultSamplerState)
        {
            this.BackBufferHeight = BackBufferHeight;
            this.BackBufferWidth = BackBufferWidth;
            this.FullScreenRectangle = FullScreenRectangle;
            this.HalfPixel = halfPixel;            
            this.device = device;
            Viewport = device.Viewport;
            this.MultiSample = MultiSample;
            this.DepthFormat = DepthFormat;
            this.UseMipMap = useMipMap;
            SamplerState = samplerState;
        
        }

        internal EngineStuff EngineStuff;

        private GraphicsAdapter graphicsAdapter;

        /// <summary>
        /// Graphic Adapter
        /// </summary>
        public GraphicsAdapter GraphicsAdapter
        {
            get { return graphicsAdapter; }
            internal set { graphicsAdapter = value; }
        }

        private SamplerState samplerState;

        /// <summary>
        /// Sampler to be used when possible
        /// </summary>
        public SamplerState SamplerState
        {
            get { return samplerState; }
            internal set { samplerState = value; }
        }

        private GraphicsDevice device;        

        private bool useMipMap;

        /// <summary>
        /// Gets a value indicating whether [use mip map].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use mip map]; otherwise, <c>false</c>.
        /// </value>
        public bool UseMipMap
        {
            get { return useMipMap; }
            internal set { useMipMap = value; }
        }
        
        private DepthFormat depthFormat;

        /// <summary>
        /// BackBuffer Depth Format
        /// </summary>
        public DepthFormat DepthFormat
        {
            get { return depthFormat; }
            internal set { depthFormat = value; }
        }
        
        private int multiSample;

        /// <summary>
        /// Back Buffer Multisample
        /// </summary>
        public int MultiSample
        {
            get { return multiSample; }
            internal set { multiSample = value; }
        }



#if WINDOWS
        /// <summary>
        /// Occurs when [on graphic info change].
        /// </summary>
        public event EventHandler OnGraphicInfoChange
        {
            add { _onGraphicInfoChange.Add(value); }
            remove { _onGraphicInfoChange.Remove(value); }
        }

        internal void FireEvent(GraphicInfo gi)
        {
            _onGraphicInfoChange.Raise(this,null);
        }

        SmartWeakEvent<EventHandler> _onGraphicInfoChange
        = new SmartWeakEvent<EventHandler>();

        SmartWeakEvent<EventHandler> _deviceReset
        = new SmartWeakEvent<EventHandler>();

        /// <summary>
        /// Occurs when [device reset].
        /// </summary>
        public event EventHandler DeviceReset
        {
            add { _deviceReset.Add(value); }
            remove { _deviceReset.Remove(value); }
        }


        internal void FireResetEvent(object obj, EventArgs args)
        {            
             _deviceReset.Raise(obj, args);
        }

#else
        internal void ClearOnGraphicInfoChangeEvent()
        {
            OnGraphicInfoChange = null;
            DeviceReset = null;
        }

        /// <summary>
        /// Occurs when [on graphic info change].
        /// </summary>
        public event EventHandler OnGraphicInfoChange;

        internal void FireEvent(GraphicInfo gi)
        {
            if(OnGraphicInfoChange!= null)
                OnGraphicInfoChange(gi,null);
        }

        
        /// <summary>
        /// Occurs when [device reset].
        /// </summary>
        public event EventHandler DeviceReset;

        internal void FireResetEvent(object obj, EventArgs args)
        {            
            if(DeviceReset!=null)
                DeviceReset(obj, args);
        }
#endif


        private int backBufferHeight;

        /// <summary>
        /// BackBufferHeight
        /// </summary>
        public int BackBufferHeight
        {
            get { return backBufferHeight; }
            internal set { backBufferHeight = value; }
        }        
        private int backBufferWidth;

        /// <summary>
        /// BackBufferWidth
        /// </summary>
        public int BackBufferWidth
        {
            get { return backBufferWidth; }
            internal set { backBufferWidth = value; }
        }
        
        private Rectangle fullScreenRectangle;

        /// <summary>
        /// FullScreenRectangle
        /// </summary>
        public Rectangle FullScreenRectangle
        {
            get { return fullScreenRectangle; }
            internal set { fullScreenRectangle = value; }
        }
        
        private Vector2 halfPixel;

        /// <summary>
        /// HalfPixel (used in DX 9 shaders)
        /// </summary>
        public Vector2 HalfPixel
        {
            get { return halfPixel; }
            internal set { halfPixel = value; }
        }
        
        private Viewport viewport;

        /// <summary>
        /// Viewport
        /// </summary>
        public Viewport Viewport
        {
            get { return viewport; }
            internal set { viewport = value; }
        }

#if !XBOX && !WINDOWS_PHONE
        private System.Windows.Forms.Form window = null;

        /// <summary>
        /// Game Window.
        /// </summary>
        public System.Windows.Forms.Form Window
        {
            get { return window; }
            internal set { window = value; }
        }
#endif



        /// <summary>
        /// Checks if render target properties combination is supported.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="depthFormat">The depth format.</param>
        /// <param name="useMipMap">if set to <c>true</c> [use mip map].</param>
        /// <param name="multisample">The multisample.</param>
        /// <param name="GraphicsProfile">The graphics profile.</param>
        /// <returns></returns>
        public bool CheckIfRenderTargetFormatIsSupported(SurfaceFormat format, DepthFormat depthFormat, bool useMipMap, int multisample, GraphicsProfile GraphicsProfile = GraphicsProfile.HiDef)
        {
#if !MONO
            SurfaceFormat outformat;
            DepthFormat outdepthFormat;
            int outmultisample;
            if (GraphicsAdapter.QueryRenderTargetFormat(GraphicsProfile, format, depthFormat, multisample, out outformat, out outdepthFormat, out outmultisample) == false)
            {
                if (outformat != format)
                {
                    ActiveLogger.LogMessage("The Combination is not supported, problem in the format", LogLevel.Warning);
                }

                if (outdepthFormat != depthFormat)
                {
                    ActiveLogger.LogMessage("The Combination is not supported, problem in the dephformat", LogLevel.Warning);
                }

                if (outmultisample != multisample)
                {
                    ActiveLogger.LogMessage("The Combination is not supported, problem in the multisample", LogLevel.Warning);
                }

                return false;
            }
            else //true !!!
            {
                return true;
            }
#else
            return false;
#endif
        }

        /// <summary>
        /// Gets the render target supported format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="depthFormat">The depth format.</param>
        /// <param name="useMipMap">if set to <c>true</c> [use mip map].</param>
        /// <param name="multisample">The multisample.</param>
        /// <param name="GraphicsProfile">The graphics profile.</param>
        public void GetRenderTargetSupportedFormat(ref SurfaceFormat format, ref DepthFormat depthFormat, ref bool useMipMap, ref int multisample, GraphicsProfile GraphicsProfile = GraphicsProfile.HiDef)
        {
#if !MONO
            GraphicsAdapter.QueryRenderTargetFormat(GraphicsProfile, format, depthFormat, multisample, out format, out depthFormat, out multisample);
#endif
        }

        /// <summary>
        /// Gets the back buffer supported format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="depthFormat">The depth format.</param>
        /// <param name="useMipMap">if set to <c>true</c> [use mip map].</param>
        /// <param name="multisample">The multisample.</param>
        /// <param name="GraphicsProfile">The graphics profile.</param>
        public void GetBackBufferSupportedFormat(ref SurfaceFormat format, ref DepthFormat depthFormat, ref bool useMipMap, ref int multisample, GraphicsProfile GraphicsProfile = GraphicsProfile.HiDef)
        {
#if !MONO
            GraphicsAdapter.QueryRenderTargetFormat(GraphicsProfile, format, depthFormat, multisample, out format, out depthFormat, out multisample);
#endif
            }

    }
}
