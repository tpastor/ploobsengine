using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        internal GraphicInfo(int BackBufferHeight, int BackBufferWidth, Rectangle FullScreenRectangle, Vector2 halfPixel, GraphicsDevice device, int MultiSample, DepthFormat DepthFormat,bool useMipMap)
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
        }

        internal void ChangeProps(int BackBufferHeight, int BackBufferWidth, Rectangle FullScreenRectangle, Vector2 halfPixel, GraphicsDevice device, int MultiSample, DepthFormat DepthFormat, bool useMipMap)
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
        }


        private GraphicsDevice device;

        internal void FireEvent(GraphicInfo gi)
        {
            if(OnGraphicInfoChange!= null)
                OnGraphicInfoChange(gi);
        }

        private bool useMipMap;

        public bool UseMipMap
        {
            get { return useMipMap; }
            internal set { useMipMap = value; }
        }

        /// <summary>
        /// BackBuffer Depth Format
        /// </summary>
        private DepthFormat depthFormat;

        public DepthFormat DepthFormat
        {
            get { return depthFormat; }
            internal set { depthFormat = value; }
        }

        /// <summary>
        /// BAck Buffer Multisample
        /// </summary>
        private int multiSample;

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
        /// BackBufferHeight
        /// </summary>
        private int backBufferHeight;

        public int BackBufferHeight
        {
            get { return backBufferHeight; }
            internal set { backBufferHeight = value; }
        }
        /// <summary>
        /// BackBufferWidth
        /// </summary>
        private int backBufferWidth;

        public int BackBufferWidth
        {
            get { return backBufferWidth; }
            internal set { backBufferWidth = value; }
        }
        /// <summary>
        /// FullScreenRectangle
        /// </summary>
        private Rectangle fullScreenRectangle;

        public Rectangle FullScreenRectangle
        {
            get { return fullScreenRectangle; }
            internal set { fullScreenRectangle = value; }
        }
        /// <summary>
        /// HalfPixel (used in DX 9 shaders)
        /// </summary>
        private Vector2 halfPixel;

        public Vector2 HalfPixel
        {
            get { return halfPixel; }
            internal set { halfPixel = value; }
        }
        /// <summary>
        /// Viewport
        /// </summary>
        private Viewport viewport;

        public Viewport Viewport
        {
            get { return viewport; }
            internal set { viewport = value; }
        }
    }
}
