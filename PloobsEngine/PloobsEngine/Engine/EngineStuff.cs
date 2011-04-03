using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Components;

namespace PloobsEngine.Engine
{

    /// <summary>
    /// Delegate Called when a unhandle exception is found in the engine
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
    public delegate void UnhandledException(object sender, UnhandledExceptionEventArgs e);

    /// <summary>
    /// Delegate Called when the engine exits
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    public delegate void OnExit(object sender, EventArgs e);

    public delegate void LoadScreen(ScreenManager ScreenManager);

    /// <summary>
    /// InitialEngineDescription
    /// </summary>
    public struct InitialEngineDescription
    {
        public InitialEngineDescription(int BackBufferWidth = 800, int BackBufferHeight = 600, bool isFullScreen = false, GraphicsProfile graphicsProfile = GraphicsProfile.HiDef, bool useVerticalSyncronization = false, bool isMultiSampling = false,bool isFixedGameTime = false, ILogger logger = null)
        {
            this.UseVerticalSyncronization = useVerticalSyncronization;
            this.BackBufferHeight = BackBufferHeight;
            this.BackBufferWidth = BackBufferWidth;
            this.Logger = logger;
            this.isMultiSampling = isMultiSampling;
            this.GraphicsProfile = graphicsProfile;
            this.isFullScreen = isFullScreen;
            UnhandledException_Handler = null;
            this.isFixedGameTime = isFixedGameTime;
            UnhandledExceptionEventHandler = null;
            OnExit = null;
            onExitHandler = null;
        }

        /// <summary>
        /// use V-Sync
        /// </summary>
        public bool UseVerticalSyncronization ;
        /// <summary>
        /// BackBufferHeight 
        /// </summary>
        public int BackBufferHeight ;
        /// <summary>
        /// BackBufferWidth
        /// </summary>
        public int BackBufferWidth;
        /// <summary>
        /// Logger implementation, can be null for no logging
        /// </summary>
        public ILogger Logger;

        /// <summary>
        /// Use Multisampling ?
        /// </summary>
        public bool isMultiSampling;

        /// <summary>        
        //     Identifies the set of supported devices for the game based on device capabilities.
        //
        // Parameters:
        //   HiDef:
        //     Use the largest available set of graphic features and capabilities to target
        //     devices, such as an Xbox 360 console and a Windows-based computer, that have
        //     more enhanced graphic capabilities.
        //
        //   Reach:
        //     Use a limited set of graphic features and capabilities, allowing the game
        //     to support the widest variety of devices, including all Windows-based computers
        //     and Windows Phone.
        /// </summary>
        public GraphicsProfile GraphicsProfile;

        /// <summary>
        /// FullScreen Mode ?
        /// </summary>
        public bool isFullScreen;

        /// <summary>
        /// If the engine should force 60 fps
        /// </summary>
        public bool isFixedGameTime;

        /// <summary>
        /// Handler for unexpected error
        /// </summary>
        public UnhandledException UnhandledException_Handler;

        /// <summary>
        /// handler Called when engine exit
        /// </summary>
        public OnExit OnExit;

        internal UnhandledExceptionEventHandler UnhandledExceptionEventHandler;
        internal EventHandler<EventArgs> onExitHandler;        
 
    }


    /// <summary>
    /// Engine Primary Interface
    /// </summary>
    public class EngineStuff : Game
    {        
        InitialEngineDescription initialDescription;
        GraphicsDeviceManager graphics;
        ScreenManager ScreenManager;
        ComponentManager ComponentManager;
        GraphicInfo GraphicInfo;
        GraphicFactory GraphicFactory;
        LoadScreen LoadScreen;
        IContentManager contentManager;
        RenderHelper render;



        /// <summary>
        /// Initializes a new instance of the <see cref="EngineStuff"/> class.
        /// </summary>
        /// <param name="initialDescription">The initial description.</param>
        /// <param name="LoadScreen">The load screen function.</param>
        public EngineStuff(ref InitialEngineDescription initialDescription, LoadScreen LoadScreen)
        {
            System.Diagnostics.Debug.Assert(LoadScreen != null);
            this.LoadScreen = LoadScreen;
            this.initialDescription = initialDescription;
            this.IsFixedTimeStep = initialDescription.isFixedGameTime;
            ActiveLogger.logger = initialDescription.Logger;            
                
            if (this.initialDescription.OnExit != null)
            {
                EventHandler <EventArgs> evhandler = new EventHandler<EventArgs>(initialDescription.OnExit);
                this.initialDescription.onExitHandler = evhandler;
                this.Exiting += evhandler;
            }

            if (this.initialDescription.UnhandledException_Handler != null)
            {
                this.initialDescription.UnhandledExceptionEventHandler = new UnhandledExceptionEventHandler(initialDescription.UnhandledException_Handler);
                AppDomain.CurrentDomain.UnhandledException += this.initialDescription.UnhandledExceptionEventHandler;
            }
                    
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = initialDescription.GraphicsProfile;
            graphics.IsFullScreen = initialDescription.isFullScreen;
            graphics.SynchronizeWithVerticalRetrace = initialDescription.UseVerticalSyncronization;
            graphics.PreferMultiSampling = initialDescription.isMultiSampling;
            graphics.PreferredBackBufferHeight = initialDescription.BackBufferHeight;
            graphics.PreferredBackBufferWidth = initialDescription.BackBufferWidth;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;            
            
        }        
        

        /// <summary>
        /// Gets the engine description.
        /// </summary>
        /// <returns></returns>
        public InitialEngineDescription GetEngineDescription()
        {
            return initialDescription;
        }

        /// <summary>
        /// Applies the engine description.
        /// </summary>
        /// <param name="initialDescription">The initial description.</param>
        public void ApplyEngineDescription(ref InitialEngineDescription initialDescription)
        {
            if (this.initialDescription.UnhandledException_Handler != null)
            {
                AppDomain.CurrentDomain.UnhandledException -= this.initialDescription.UnhandledExceptionEventHandler;                    
            }

            if (this.initialDescription.onExitHandler != null)
            {
                this.Exiting -= this.initialDescription.onExitHandler;
            }

            this.initialDescription = initialDescription;
            this.IsFixedTimeStep = initialDescription.isFixedGameTime;                        

            ActiveLogger.logger = initialDescription.Logger;            
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = initialDescription.GraphicsProfile;
            graphics.IsFullScreen = initialDescription.isFullScreen;
            graphics.SynchronizeWithVerticalRetrace = initialDescription.UseVerticalSyncronization;
            graphics.PreferMultiSampling = initialDescription.isMultiSampling;
            graphics.PreferredBackBufferHeight = initialDescription.BackBufferHeight;
            graphics.PreferredBackBufferWidth = initialDescription.BackBufferWidth;            

            if (this.initialDescription.OnExit != null)
            {
                EventHandler<EventArgs> evhandler = new EventHandler<EventArgs>(initialDescription.OnExit);
                this.initialDescription.onExitHandler = evhandler;
                this.Exiting += evhandler;
            }

            if (this.initialDescription.UnhandledException_Handler != null)
            {
                this.initialDescription.UnhandledExceptionEventHandler = new UnhandledExceptionEventHandler(initialDescription.UnhandledException_Handler);
                AppDomain.CurrentDomain.UnhandledException += this.initialDescription.UnhandledExceptionEventHandler;
            }

            Rectangle fs = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Vector2 halfPixel;
            halfPixel.X = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
            halfPixel.Y = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferHeight;

            GraphicInfo ginfo = new GraphicInfo(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel,GraphicsDevice);
            GraphicInfo.FireEvent(ginfo);
            GraphicInfo = ginfo;

        }


        protected override void LoadContent()
        {
            base.LoadContent();

            Rectangle fs = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Vector2 halfPixel;
            halfPixel.X = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
            halfPixel.Y = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferHeight;

            contentManager = new EngineContentManager(this);
            GraphicInfo = new GraphicInfo(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel,GraphicsDevice);
            GraphicFactory = new Engine.GraphicFactory(GraphicInfo, GraphicsDevice, contentManager);
            render = new RenderHelper(GraphicsDevice);

            ComponentManager = new ComponentManager(ref GraphicInfo);            
            ComponentManager.LoadContent(ref GraphicInfo);            
            ScreenManager = new ScreenManager(ref GraphicInfo, GraphicFactory, contentManager,render);            

            LoadScreen(ScreenManager);
            if (ScreenManager.GetScreens().Count() == 0)
            {
                ActiveLogger.LogMessage("LoadScreen should create at least one screen", LogLevel.FatalError);
                System.Diagnostics.Debug.Assert(ScreenManager.GetScreens().Count() != 0);
                Exit();
            }
            
            
        }

        protected override bool ShowMissingRequirementMessage(Exception exception)
        {
            ActiveLogger.LogMessage(exception.Message, LogLevel.FatalError);
            return base.ShowMissingRequirementMessage(exception);
        }        

        protected override void Update(GameTime gameTime)
        {            
            ComponentManager.Update(gameTime);
            ScreenManager.Update(gameTime);            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {            
            ScreenManager.Draw(gameTime);
            base.Draw(gameTime);
        }        
        
    }

    
}
