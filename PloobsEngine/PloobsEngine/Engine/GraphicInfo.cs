using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Engine
{
    /// <summary>
    /// When GraphicInfo changes
    /// </summary>
    /// <param name="newGraphicInfo">The new graphic info.</param>
    public delegate void OnGraphicInfoChange(GraphicInfo newGraphicInfo);
    
    /// <summary>
    /// Contains Graphics Informations about the current execution
    /// </summary>
    public class GraphicInfo
    {
        internal GraphicInfo(int BackBufferHeight, int BackBufferWidth, Rectangle FullScreenRectangle, Vector2 halfPixel, GraphicsDevice device, int MultiSample, DepthFormat DepthFormat, bool useMipMap, EngineStuff engine, bool useAnisotropicFiltering = true)
        {
            this.BackBufferHeight = BackBufferHeight;
            this.BackBufferWidth = BackBufferWidth;
            this.FullScreenRectangle = FullScreenRectangle;
            this.HalfPixel = halfPixel;
            OnGraphicInfoChange = null;
            this.device = device;
            Viewport = device.Viewport;
            this.MultiSample = MultiSample;
            this.DepthFormat = DepthFormat;
            this.UseMipMap = useMipMap;
#if !XBOX
            this.window = (System.Windows.Forms.Form) System.Windows.Forms.Form.FromHandle(engine.Window.Handle);
#endif

            if (useAnisotropicFiltering)
                SamplerState = SamplerState.AnisotropicClamp;
            else
                SamplerState = SamplerState.LinearClamp;

            UseAnisotropicFiltering = useAnisotropicFiltering;

            GraphicsAdapter = engine.GraphicsDevice.Adapter;
        }

        internal void ChangeProps(int BackBufferHeight, int BackBufferWidth, Rectangle FullScreenRectangle, Vector2 halfPixel, GraphicsDevice device, int MultiSample, DepthFormat DepthFormat, bool useMipMap,bool useAnisotropicFiltering = true)
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
            if (useAnisotropicFiltering)
                SamplerState = SamplerState.AnisotropicClamp;
            else
                SamplerState = SamplerState.LinearClamp;

            UseAnisotropicFiltering = useAnisotropicFiltering;
        }

        internal void FireResetEvent(object obj, EventArgs args)
        {
            if(DeviceReset!=null)
                DeviceReset(obj, args);
        }

        private GraphicsAdapter graphicsAdapter;

                /// <summary>
        /// Graphic Adapter
        /// </summary>
        public GraphicsAdapter GraphicsAdapter
        {
            get { return graphicsAdapter; }
            internal set { graphicsAdapter = value; }
        }

        private bool useAnisotropicFiltering;

        /// <summary>
        /// Gets a value indicating whether [use anisotropic filtering].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [use anisotropic filtering]; otherwise, <c>false</c>.
        /// </value>
        public bool UseAnisotropicFiltering
        {
            get { return useAnisotropicFiltering; }
            internal set { useAnisotropicFiltering = value; }
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

        internal void FireEvent(GraphicInfo gi)
        {
            if(OnGraphicInfoChange!= null)
                OnGraphicInfoChange(gi);
        }

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

        /// <summary>
        /// Occurs when [on graphic info change].
        /// </summary>
        public event OnGraphicInfoChange OnGraphicInfoChange;

        /// <summary>
        /// Occurs when [device reset].
        /// </summary>
        public event EventHandler<EventArgs> DeviceReset = null;

        
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

#if !XBOX
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
        /// <returns></returns>
        public bool CheckIfRenderTargetFormatIsSupported(SurfaceFormat format, DepthFormat depthFormat, bool useMipMap, int multisample, GraphicsProfile GraphicsProfile = GraphicsProfile.HiDef)
        {
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
            else ///true !!!
            {
                return true;
            }
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
            GraphicsAdapter.QueryRenderTargetFormat(GraphicsProfile, format, depthFormat, multisample, out format, out depthFormat, out multisample);
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
            GraphicsAdapter.QueryRenderTargetFormat(GraphicsProfile, format, depthFormat, multisample, out format, out depthFormat, out multisample);
        }

    }
}
