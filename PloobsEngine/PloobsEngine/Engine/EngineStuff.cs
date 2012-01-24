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
#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Components;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.Input;
using PloobsEngine.Features;

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

    /// <summary>
    /// Delegate resposible to load the first screen
    /// </summary>
    /// <param name="ScreenManager">The screen manager.</param>
    public delegate void LoadScreen(ScreenManager ScreenManager);

    /// <summary>
    /// InitialEngineDescription
    /// </summary>
    public struct InitialEngineDescription
    {
        
        /// <summary>
        /// Defaults this instance.
        /// </summary>
        /// <returns></returns>
        public static InitialEngineDescription Default()
        {
#if !REACH
            return new InitialEngineDescription("PloobsEngine",800,600,false,GraphicsProfile.HiDef,false,false,true,null,false,false);
#else
            return new InitialEngineDescription("PloobsEngine",800,600,false,GraphicsProfile.Reach,false,false,true,null,false,false);
#endif
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InitialEngineDescription"/> struct.
        /// </summary>
        /// <param name="ScreenName">Name of the screen.</param>
        /// <param name="BackBufferWidth">Width of the back buffer.</param>
        /// <param name="BackBufferHeight">Height of the back buffer.</param>
        /// <param name="isFullScreen">if set to <c>true</c> [is full screen].</param>
        /// <param name="graphicsProfile">The graphics profile.</param>
        /// <param name="useVerticalSyncronization">if set to <c>true</c> [use vertical syncronization].</param>
        /// <param name="isMultiSampling">if set to <c>true</c> [is multi sampling].</param>
        /// <param name="isFixedGameTime">if set to <c>true</c> [is fixed game time].</param>
        /// <param name="logger">The logger.</param>
        /// <param name="useMipMapWhenPossible">if set to <c>true</c> [use mip map when possible].</param>
        /// <param name="UseAnisotropicFiltering">if set to <c>true</c> [use anisotropic filtering].</param>
        /// <param name="supportedOrientation">The supported orientation.</param>
        #if !REACH
        internal InitialEngineDescription(String ScreenName = "PloobsEngine", int BackBufferWidth = 800, int BackBufferHeight = 600, bool isFullScreen = false, GraphicsProfile graphicsProfile = GraphicsProfile.HiDef, bool useVerticalSyncronization = false, bool isMultiSampling = false, bool isFixedGameTime = false, ILogger logger = null, bool useMipMapWhenPossible = false, bool UseAnisotropicFiltering = false, DisplayOrientation supportedOrientation = DisplayOrientation.Portrait | DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight)
#else
        internal InitialEngineDescription(String ScreenName = "PloobsEngine", int BackBufferWidth = 800, int BackBufferHeight = 600, bool isFullScreen = false, GraphicsProfile graphicsProfile = GraphicsProfile.Reach, bool useVerticalSyncronization = false, bool isMultiSampling = false, bool isFixedGameTime = false, ILogger logger = null, bool useMipMapWhenPossible = false, bool UseAnisotropicFiltering = false, DisplayOrientation supportedOrientation = DisplayOrientation.Default | DisplayOrientation.Portrait | DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight)
#endif
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
            this.ScreenName = ScreenName;
            this.useMipMapWhenPossible = useMipMapWhenPossible;
            this.UseAnisotropicFiltering = UseAnisotropicFiltering;
            this.SupportedOrientations = supportedOrientation;
            
        }

        /// <summary>
        /// Use Anisotropic Filtering when possible
        /// </summary>
        public bool UseAnisotropicFiltering;
        
        /// <summary>
        /// Screen Name
        /// </summary>
        public String ScreenName;

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
        /// Use MipMap When creating the Render Targets
        /// </summary>
        public bool useMipMapWhenPossible;

        /// <summary>        
        ///     Identifies the set of supported devices for the game based on device capabilities.
        ///
        /// Parameters:
        ///   HiDef:
        ///     Use the largest available set of graphic features and capabilities to target
        ///     devices, such as an Xbox 360 console and a Windows-based computer, that have
        ///     more enhanced graphic capabilities.
        ///
        ///   Reach:
        ///     Use a limited set of graphic features and capabilities, allowing the game
        ///     to support the widest variety of devices, including all Windows-based computers
        ///     and Windows Phone.
        /// </summary>
        internal GraphicsProfile GraphicsProfile;

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
        public DisplayOrientation SupportedOrientations;        
 
    }

    /// <summary>
    /// Sound Options Desciption
    /// </summary>
    public struct SoundMasterOptionDescription
    {
        public SoundMasterOptionDescription(float DistanceScale, float DoplerScale,float MasterVolume)
        {
            this.DistanceScale = DistanceScale;
            this.DoplerScale = DoplerScale;
            this.MasterVolume = MasterVolume;            
        }

        public static SoundMasterOptionDescription Default()
        {            
            return new SoundMasterOptionDescription(1,1,1);
        }

        public float DistanceScale;
        public float DoplerScale;
        public float MasterVolume;        
    }

    /// <summary>
    /// Engine Entry point
    /// </summary>
    public class EngineStuff : Game
    {
        SoundMasterOptionDescription soundMasterOptionDescription = SoundMasterOptionDescription.Default();
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
        /// Gets or sets the content manager.
        /// </summary>
        /// <value>
        /// The content manager.
        /// </value>
        public IContentManager ContentManager
        {
            get { return contentManager; }
            set { contentManager = value; }
        }

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
                    
            
            this.Window.Title = initialDescription.ScreenName;

            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = initialDescription.GraphicsProfile;
            graphics.IsFullScreen = initialDescription.isFullScreen;
            graphics.SynchronizeWithVerticalRetrace = initialDescription.UseVerticalSyncronization;
            graphics.PreferMultiSampling = initialDescription.isMultiSampling;
            graphics.PreferredBackBufferHeight = initialDescription.BackBufferHeight;
            graphics.PreferredBackBufferWidth = initialDescription.BackBufferWidth;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            graphics.SupportedOrientations = initialDescription.SupportedOrientations;
            graphics.ApplyChanges();            
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
            this.Window.Title = initialDescription.ScreenName;
            
            ActiveLogger.logger = initialDescription.Logger;                        
            graphics.GraphicsProfile = initialDescription.GraphicsProfile;
            graphics.IsFullScreen = initialDescription.isFullScreen;
            graphics.SynchronizeWithVerticalRetrace = initialDescription.UseVerticalSyncronization;
            graphics.PreferMultiSampling = initialDescription.isMultiSampling;
            graphics.PreferredBackBufferHeight = initialDescription.BackBufferHeight;
            graphics.PreferredBackBufferWidth = initialDescription.BackBufferWidth;
            graphics.SupportedOrientations = initialDescription.SupportedOrientations;
            graphics.ApplyChanges();
            
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
            Vector2 halfPixel = new Vector2();
            halfPixel.X = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
            halfPixel.Y = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferHeight;

            GraphicInfo.ChangeProps(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel, GraphicsDevice, GraphicsDevice.PresentationParameters.MultiSampleCount, GraphicsDevice.PresentationParameters.DepthStencilFormat, initialDescription.useMipMapWhenPossible,initialDescription.UseAnisotropicFiltering);
            GraphicInfo.FireEvent(GraphicInfo);
        }

        /// <summary>
        /// Gets the sound master option description.
        /// </summary>
        /// <returns></returns>
        public SoundMasterOptionDescription GetSoundMasterOptionDescription()
        {
            return soundMasterOptionDescription;
        }

        /// <summary>
        /// Sets the sound master option description.
        /// </summary>
        /// <param name="soundMasterOptionDescription">The sound master option description.</param>
        public void SetSoundMasterOptionDescription(ref SoundMasterOptionDescription soundMasterOptionDescription)
        {
            this.soundMasterOptionDescription = soundMasterOptionDescription;
            SoundEffect.DistanceScale = soundMasterOptionDescription.DistanceScale;
            SoundEffect.DopplerScale = soundMasterOptionDescription.DoplerScale;
            SoundEffect.MasterVolume = soundMasterOptionDescription.MasterVolume;
        }



        /// <summary>
        /// Load the content
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();


            Rectangle fs = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Vector2 halfPixel = new Vector2()
            {
                X = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferWidth,
                Y = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferHeight
            };

            this.Content = new ContentTracker(this.Services,"Content");            
            contentManager = new IContentManager(this);
            GraphicInfo = new GraphicInfo(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel, GraphicsDevice, GraphicsDevice.PresentationParameters.MultiSampleCount, GraphicsDevice.PresentationParameters.DepthStencilFormat,initialDescription.useMipMapWhenPossible,this,initialDescription.UseAnisotropicFiltering);
            this.GraphicsDevice.DeviceReset += new EventHandler<EventArgs>(GraphicsDevice_DeviceReset);            
            GraphicFactory = new Engine.GraphicFactory(GraphicInfo, GraphicsDevice, contentManager);
            ComponentManager = new ComponentManager(GraphicInfo, GraphicFactory);
            render = new RenderHelper(GraphicsDevice, ComponentManager, contentManager);
            GraphicFactory.render = render;
            ComponentManager.LoadContent(ref GraphicInfo);            
            render.PushBlendState(BlendState.Opaque);
            render.PushDepthStencilState(DepthStencilState.Default);
            render.PushRasterizerState(RasterizerState.CullCounterClockwise);
            render.PushRenderTarget(null);
            
            ScreenManager = new ScreenManager(ref GraphicInfo, GraphicFactory, contentManager,render,this);            

            LoadScreen(ScreenManager);
            if (ScreenManager.GetScreens().Count() == 0)
            {
                ActiveLogger.LogMessage("LoadScreen should create at least one screen", LogLevel.FatalError);
                System.Diagnostics.Debug.Assert(ScreenManager.GetScreens().Count() != 0);
                Exit();
            }

            //THE ONLY COMPONENTS ADDED BY DEFAULT
           
            ComponentManager.AddComponent(new InputAdvanced());
          
            ComponentManager.AddComponent(new TaskProcessor());
        }

        void GraphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            GraphicInfo.FireResetEvent(sender, e);
        }
        
        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void AddComponent(IComponent component)
        {
            if (component == null)
                ActiveLogger.LogMessage("Cant add null Component", LogLevel.RecoverableError);
            else
            {
                bool resp = ComponentManager.AddComponent(component);
                if (!resp)
                    ActiveLogger.LogMessage("Component already added ", LogLevel.Warning);
            }
        }

        /// <summary>
        /// Removes the component by name
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        public void RemoveComponent(String componentName)
        {
            if (String.IsNullOrEmpty(componentName))
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
            else
            {
                bool resp = ComponentManager.RemoveComponent(componentName);
                if (!resp)
                    ActiveLogger.LogMessage("Component already Removed", LogLevel.Warning);
            }
        }

        /// <summary>
        /// Gets the component by name.
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="componentName">Name of the component.</param>
        /// <returns></returns>
        public T GetComponent<T>(String componentName)  where T : IComponent
        {
            if (String.IsNullOrEmpty(componentName))
            {
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
                return null;
            }
            else
            {
                IComponent comp = ComponentManager.GetComponent(componentName);
                if (comp == null)
                    ActiveLogger.LogMessage("Component not found " + componentName, LogLevel.RecoverableError);
                return (T)comp;
            }
        }

        /// <summary>
        /// Determines whether the specified component name exist.
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        /// <returns>
        ///   <c>true</c> if the specified component name has component; otherwise, <c>false</c>.
        /// </returns>
        public bool HasComponent(String componentName)
        {
            if (String.IsNullOrEmpty(componentName))
            {
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
                return false;
            }
            else
            {
                return ComponentManager.HasComponent(componentName);
            }
        }


        /// <summary>
        /// This is used to display an error message if there is no suitable graphics device or sound card.
        /// </summary>
        /// <param name="exception">The exception to display.</param>
        /// <returns></returns>
        protected override bool ShowMissingRequirementMessage(Exception exception)
        {
            ActiveLogger.LogMessage(exception.Message, LogLevel.FatalError);
            return base.ShowMissingRequirementMessage(exception);
        }

        /// <summary>
        /// Updates the engine, called by XNA
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        protected override void Update(GameTime gameTime)
        {
            if ( (initialDescription.isFullScreen == true && this.IsActive) || initialDescription.isFullScreen == false)
            {
                ComponentManager.Update(gameTime);
                ScreenManager.Update(gameTime);
                CommandProcessor.getCommandProcessor().ProcessCommands();
            }
            base.Update(gameTime);
        }

        
        /// <summary>
        /// Reference page contains code sample.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        protected override void Draw(GameTime gameTime)
        {
            if ((initialDescription.isFullScreen == true && this.IsActive) || initialDescription.isFullScreen == false)
            {
                ScreenManager.Draw(gameTime);             
            }
            base.Draw(gameTime);
        }        
        
    }

    
}
#elif SILVER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Components;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.Input;
using PloobsEngine.Features;
using Microsoft.Xna.Framework.Content;
using Microsoft.Phone.Controls;

namespace PloobsEngine.Engine
{

    /// <summary>
    /// Delegate Called when a unhandle exception is found in the engine
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
    public delegate void UnhandledException(object sender, UnhandledExceptionEventArgs e);
        
    /// <summary>
    /// InitialEngineDescription
    /// </summary>
    public struct InitialEngineDescription
    {

        /// <summary>
        /// Defaults this instance.
        /// </summary>
        /// <returns></returns>
        public static InitialEngineDescription Default()
        {
            return new InitialEngineDescription("PloobsEngine", 800, 480, false, GraphicsProfile.Reach, false, false, true, null, false, false);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="InitialEngineDescription"/> struct.
        /// </summary>
        /// <param name="ScreenName">Name of the screen.</param>
        /// <param name="BackBufferWidth">Width of the back buffer.</param>
        /// <param name="BackBufferHeight">Height of the back buffer.</param>
        /// <param name="isFullScreen">if set to <c>true</c> [is full screen].</param>
        /// <param name="graphicsProfile">The graphics profile.</param>
        /// <param name="useVerticalSyncronization">if set to <c>true</c> [use vertical syncronization].</param>
        /// <param name="isMultiSampling">if set to <c>true</c> [is multi sampling].</param>
        /// <param name="isFixedGameTime">if set to <c>true</c> [is fixed game time].</param>
        /// <param name="logger">The logger.</param>
        /// <param name="useMipMapWhenPossible">if set to <c>true</c> [use mip map when possible].</param>
        /// <param name="UseAnisotropicFiltering">if set to <c>true</c> [use anisotropic filtering].</param>
        internal InitialEngineDescription(String ScreenName = "PloobsEngine", int BackBufferWidth = 800, int BackBufferHeight = 600, bool isFullScreen = false, GraphicsProfile graphicsProfile = GraphicsProfile.HiDef, bool useVerticalSyncronization = false, bool isMultiSampling = false, bool isFixedGameTime = false, ILogger logger = null, bool useMipMapWhenPossible = false, bool UseAnisotropicFiltering = false, DisplayOrientation supportedOrientation = DisplayOrientation.Default | DisplayOrientation.Portrait | DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight)
        {
            this.UseVerticalSyncronization = useVerticalSyncronization;
            this.BackBufferHeight = BackBufferHeight;
            this.BackBufferWidth = BackBufferWidth;
            this.Logger = logger;
            this.GraphicsProfile = graphicsProfile;
            this.ScreenName = ScreenName;
            this.useMipMapWhenPossible = useMipMapWhenPossible;
            this.UseAnisotropicFiltering = UseAnisotropicFiltering;
            UpdateInterval = TimeSpan.FromTicks(333333);
        }

        /// <summary>
        /// Update per second
        /// Default TimeSpan.FromTicks(333333);
        /// </summary>
        public TimeSpan UpdateInterval ;

        /// <summary>
        /// Use Anisotropic Filtering when possible
        /// </summary>
        public bool UseAnisotropicFiltering;

        /// <summary>
        /// Screen Name
        /// </summary>
        public String ScreenName;

        /// <summary>
        /// use V-Sync
        /// </summary>
        public bool UseVerticalSyncronization;
        /// <summary>
        /// BackBufferHeight 
        /// </summary>
        public int BackBufferHeight;
        /// <summary>
        /// BackBufferWidth
        /// </summary>
        public int BackBufferWidth;
        /// <summary>
        /// Logger implementation, can be null for no logging
        /// </summary>
        public ILogger Logger;

        /// <summary>/
        /// Use MipMap When creating the Render Targets
        /// </summary>
        public bool useMipMapWhenPossible;

        
        /// <summary>        
        ///     Identifies the set of supported devices for the game based on device capabilities.
        ///
        /// Parameters:
        ///   HiDef:
        ///     Use the largest available set of graphic features and capabilities to target
        ///     devices, such as an Xbox 360 console and a Windows-based computer, that have
        ///     more enhanced graphic capabilities.
        ///
        ///   Reach:
        ///     Use a limited set of graphic features and capabilities, allowing the game
        ///     to support the widest variety of devices, including all Windows-based computers
        ///     and Windows Phone.
        /// </summary>
        internal GraphicsProfile GraphicsProfile;


        

    }

    /// <summary>
    /// Sound Options Desciption
    /// </summary>
    public struct SoundMasterOptionDescription
    {
        public SoundMasterOptionDescription(float DistanceScale, float DoplerScale, float MasterVolume)
        {
            this.DistanceScale = DistanceScale;
            this.DoplerScale = DoplerScale;
            this.MasterVolume = MasterVolume;
        }

        public static SoundMasterOptionDescription Default()
        {
            return new SoundMasterOptionDescription(1, 1, 1);
        }

        public float DistanceScale;
        public float DoplerScale;
        public float MasterVolume;
    }

    /// <summary>
    /// Engine Entry point
    /// </summary>
    public class EngineStuff
    {
        SoundMasterOptionDescription soundMasterOptionDescription = SoundMasterOptionDescription.Default();
        InitialEngineDescription initialDescription;
        SharedGraphicsDeviceManager graphics;
        ScreenManager ScreenManager;
        ComponentManager ComponentManager;
        ContentManager SilverLightContentManager;
        GraphicInfo GraphicInfo;
        GraphicFactory GraphicFactory;
        IContentManager contentManager;        
        RenderHelper render;
        /// <summary>
        /// Graphics device
        /// </summary>
        public readonly GraphicsDevice GraphicsDevice;
        GameTimer timer;
        PhoneApplicationPage PhoneApplicationPage;
        UIElementRenderer elementRenderer;
        EventHandler EventHandler = null;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineStuff"/> class.
        /// </summary>        
        private EngineStuff(SharedGraphicsDeviceManager SharedGraphicsDeviceManager, ContentManager ContentManager, ref InitialEngineDescription initialDescription)
        {            
            this.SilverLightContentManager = ContentManager;
            this.initialDescription = initialDescription;
            ActiveLogger.logger = initialDescription.Logger;

            this.graphics = SharedGraphicsDeviceManager;

            GraphicsDevice = SharedGraphicsDeviceManager.Current.GraphicsDevice;
            graphics.GraphicsProfile = initialDescription.GraphicsProfile;            
            graphics.SynchronizeWithVerticalRetrace = initialDescription.UseVerticalSyncronization;            
            graphics.PreferredBackBufferHeight = initialDescription.BackBufferHeight;
            graphics.PreferredBackBufferWidth = initialDescription.BackBufferWidth;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;            
            LoadContent();
        }

        private EngineStuff(SharedGraphicsDeviceManager SharedGraphicsDeviceManager, ContentManager ContentManager,bool UseAnisotropicFiltering = false,bool useMipMapWhenPossible = false)
        {
            this.SilverLightContentManager = ContentManager;
            this.graphics = SharedGraphicsDeviceManager;
            GraphicsDevice = SharedGraphicsDeviceManager.Current.GraphicsDevice;
            InitialEngineDescription initialDescription = InitialEngineDescription.Default();
            initialDescription.UseAnisotropicFiltering = UseAnisotropicFiltering;
            initialDescription.useMipMapWhenPossible = useMipMapWhenPossible;
            initialDescription.UseVerticalSyncronization = SharedGraphicsDeviceManager.SynchronizeWithVerticalRetrace;
            initialDescription.BackBufferHeight = SharedGraphicsDeviceManager.DefaultBackBufferHeight;
            initialDescription.BackBufferWidth = SharedGraphicsDeviceManager.DefaultBackBufferWidth;
            this.initialDescription = initialDescription;
            LoadContent();
        }

        /// <summary>
        /// Gets the current instance of ploobsengine.
        /// </summary>
        public static EngineStuff Current
        {
            internal set;
            get;
        }

        /// <summary>
        /// Initializes the ploobs engine.
        /// </summary>
        /// <param name="SharedGraphicsDeviceManager">The shared graphics device manager.</param>
        /// <param name="ContentManager">The content manager.</param>
        /// <param name="initialDescription">The initial description.</param>
        /// <param name="forceRecreating">if set to <c>true</c> [force recreating].</param>
        /// <param name="removeComponents">if set to <c>true</c> [remove components].</param>
        /// <param name="ClearAllEventsSubscribers">if set to <c>true</c> [clear all events subscribers].</param>
        public static void InitializePloobsEngine(SharedGraphicsDeviceManager SharedGraphicsDeviceManager, ContentManager ContentManager, ref InitialEngineDescription initialDescription, bool forceRecreating = false, bool removeComponents = false, bool ClearAllEventsSubscribers = false)
        {
            if (Current == null || forceRecreating == true)
            {
                Current = new EngineStuff(SharedGraphicsDeviceManager, ContentManager, ref initialDescription);
            }
            else
            {                
                if (removeComponents)
                {
                    foreach (var item in Current.ComponentManager.GetComponentsNames().ToArray())
                    {
                        Current.ComponentManager.RemoveComponent(item);
                    }
                }
                if (ClearAllEventsSubscribers)
                {
                    Current.GraphicInfo.ClearOnGraphicInfoChangeEvent();
                }
            }
        }

        /// <summary>
        /// Initializes the ploobs engine.
        /// </summary>
        /// <param name="SharedGraphicsDeviceManager">The shared graphics device manager.</param>
        /// <param name="ContentManager">The content manager.</param>
        /// <param name="forceRecreating">if set to <c>true</c> [force recreating].</param>
        /// <param name="UseAnisotropicFiltering">if set to <c>true</c> [use anisotropic filtering].</param>
        /// <param name="useMipMapWhenPossible">if set to <c>true</c> [use mip map when possible].</param>
        /// <param name="removeComponents">if set to <c>true</c> [remove components].</param>
        /// <param name="ClearAllEventsSubscribers">if set to <c>true</c> [clear all events subscribers].</param>
        public static void InitializePloobsEngine(SharedGraphicsDeviceManager SharedGraphicsDeviceManager, ContentManager ContentManager, bool forceRecreating = false, bool UseAnisotropicFiltering = false, bool useMipMapWhenPossible = false, bool removeComponents = false, bool ClearAllEventsSubscribers = false)
        {
            if (Current == null || forceRecreating == true)
            {
                Current = new EngineStuff(SharedGraphicsDeviceManager, ContentManager, UseAnisotropicFiltering, useMipMapWhenPossible);
            }
            else
            {                
                if (removeComponents)
                {
                    foreach (var item in Current.ComponentManager.GetComponentsNames().ToArray())
                    {
                        Current.ComponentManager.RemoveComponent(item);
                    }
                }
                if (ClearAllEventsSubscribers)
                {
                    Current.GraphicInfo.ClearOnGraphicInfoChangeEvent();
                }
            }
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
        /// NOT RECOMENDED IN WP7 + XNA
        /// </summary>
        /// <param name="initialDescription">The initial description.</param>
        public void ApplyEngineDescription(ref InitialEngineDescription initialDescription)
        {
            this.initialDescription = initialDescription;
        
            ActiveLogger.logger = initialDescription.Logger;
            graphics.GraphicsProfile = initialDescription.GraphicsProfile;
        
            graphics.SynchronizeWithVerticalRetrace = initialDescription.UseVerticalSyncronization;        
            graphics.PreferredBackBufferHeight = initialDescription.BackBufferHeight;
            graphics.PreferredBackBufferWidth = initialDescription.BackBufferWidth;            
            graphics.ApplyChanges();

            
            Rectangle fs = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Vector2 halfPixel = new Vector2();
            halfPixel.X = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
            halfPixel.Y = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferHeight;

            GraphicInfo.ChangeProps(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel, GraphicsDevice, GraphicsDevice.PresentationParameters.MultiSampleCount, GraphicsDevice.PresentationParameters.DepthStencilFormat, initialDescription.useMipMapWhenPossible, initialDescription.UseAnisotropicFiltering);
            GraphicInfo.FireEvent(GraphicInfo);
        }

        /// <summary>
        /// Gets the sound master option description.
        /// </summary>
        /// <returns></returns>
        public SoundMasterOptionDescription GetSoundMasterOptionDescription()
        {
            return soundMasterOptionDescription;
        }

        /// <summary>
        /// Sets the sound master option description.
        /// </summary>
        /// <param name="soundMasterOptionDescription">The sound master option description.</param>
        public void SetSoundMasterOptionDescription(ref SoundMasterOptionDescription soundMasterOptionDescription)
        {
            this.soundMasterOptionDescription = soundMasterOptionDescription;
            SoundEffect.DistanceScale = soundMasterOptionDescription.DistanceScale;
            SoundEffect.DopplerScale = soundMasterOptionDescription.DoplerScale;
            SoundEffect.MasterVolume = soundMasterOptionDescription.MasterVolume;
        }



        /// <summary>
        /// Load the content
        /// </summary>
        private void LoadContent()
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true); 
            Rectangle fs = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Vector2 halfPixel = new Vector2()
            {
                X = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferWidth,
                Y = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferHeight
            };

            contentManager = new IContentManager(SilverLightContentManager);
            GraphicInfo = new GraphicInfo(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel, GraphicsDevice, GraphicsDevice.PresentationParameters.MultiSampleCount, GraphicsDevice.PresentationParameters.DepthStencilFormat, initialDescription.useMipMapWhenPossible, this, initialDescription.UseAnisotropicFiltering);
            GraphicsDevice.DeviceReset += new EventHandler<EventArgs>(GraphicsDevice_DeviceReset);
            GraphicFactory = new Engine.GraphicFactory(GraphicInfo, GraphicsDevice, contentManager);
            ComponentManager = new ComponentManager(GraphicInfo, GraphicFactory);
            render = new RenderHelper(this.GraphicsDevice, ComponentManager, contentManager);
            GraphicFactory.render = render;
            ComponentManager.LoadContent(ref GraphicInfo);
            render.PushBlendState(BlendState.Opaque);
            render.PushDepthStencilState(DepthStencilState.Default);
            render.PushRasterizerState(RasterizerState.CullCounterClockwise);
            render.PushRenderTarget(null);

            ScreenManager = new ScreenManager(ref GraphicInfo, GraphicFactory, contentManager, render, this);
            
            //THE ONLY COMPONENTS ADDED BY DEFAULT                        
            ComponentManager.AddComponent(new InputAdvanced());
            //ComponentManager.AddComponent(new TaskProcessor());
        }

        void GraphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            GraphicInfo.FireResetEvent(sender, e);
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void AddComponent(IComponent component)
        {
            if (component == null)
                ActiveLogger.LogMessage("Cant add null Component", LogLevel.RecoverableError);
            else
            {
                bool resp = ComponentManager.AddComponent(component);
                if (!resp)
                    ActiveLogger.LogMessage("Component already added ", LogLevel.Warning);
            }
        }

        /// <summary>
        /// Removes the component by name
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        public void RemoveComponent(String componentName)
        {
            if (String.IsNullOrEmpty(componentName))
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
            else
            {
                bool resp = ComponentManager.RemoveComponent(componentName);
                if (!resp)
                    ActiveLogger.LogMessage("Component already Removed", LogLevel.Warning);
            }
        }

        /// <summary>
        /// Gets the component by name.
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="componentName">Name of the component.</param>
        /// <returns></returns>
        public T GetComponent<T>(String componentName) where T : IComponent
        {
            if (String.IsNullOrEmpty(componentName))
            {
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
                return null;
            }
            else
            {
                IComponent comp = ComponentManager.GetComponent(componentName);
                if (comp == null)
                    ActiveLogger.LogMessage("Component not found " + componentName, LogLevel.RecoverableError);
                return (T)comp;
            }
        }

        /// <summary>
        /// Determines whether the specified component name exist.
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        /// <returns>
        ///   <c>true</c> if the specified component name has component; otherwise, <c>false</c>.
        /// </returns>
        public bool HasComponent(String componentName)
        {
            if (String.IsNullOrEmpty(componentName))
            {
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
                return false;
            }
            else
            {
                return ComponentManager.HasComponent(componentName);
            }
        }

        /// <summary>
        /// This is used to display an error message if there is no suitable graphics device or sound card.
        /// </summary>
        /// <param name="exception">The exception to display.</param>
        /// <returns></returns>
        internal void ShowMissingRequirementMessage(Exception exception)
        {
            ActiveLogger.LogMessage(exception.Message, LogLevel.FatalError);
        }


        /// <summary>
        /// Remove All Screens and stop the timer
        /// </summary>
        /// <param name="removeComponents">if set to <c>true</c> [remove components].</param>
        /// <param name="removeAllEventSubscribers">if set to <c>true</c> [remove all event subscribers].</param>
        public void LeaveScene(bool removeComponents = false, bool removeAllEventSubscribers = false)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            foreach (var item in ScreenManager.GetScreens())
            {
                ScreenManager.RemoveScreen(item);
            }

            if (removeComponents)
            {
                foreach (var item in this.ComponentManager.GetComponentsNames())
                {
                    this.RemoveComponent(item);
                }
            }

            if (removeAllEventSubscribers)
            {
                GraphicInfo.ClearOnGraphicInfoChangeEvent();
            }

        }

        PageOrientation PageOrientation;
        IScreen ScreenAddedAfter = null;
        /// <summary>
        /// Add a new scene to the Engine
        /// REMOVE ALL PREVIOUS ONES
        /// </summary>
        /// <param name="Screen">The screen.</param>
        /// <param name="PhoneApplicationPage">The phone application page.</param>
        public void StartScene(IScreen Screen, PhoneApplicationPage PhoneApplicationPage)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true); 
            this.PhoneApplicationPage = PhoneApplicationPage;

            foreach (var item in ScreenManager.GetScreens())
            {
                ScreenManager.RemoveScreen(item);
            }
            
            if (PageOrientation != Microsoft.Phone.Controls.PageOrientation.None && PageOrientation != PhoneApplicationPage.Orientation)
            {
                graphics.PreferredBackBufferHeight = initialDescription.BackBufferWidth;
                graphics.PreferredBackBufferWidth = initialDescription.BackBufferHeight;
                graphics.ApplyChanges();

                Rectangle fs = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
                Vector2 halfPixel = new Vector2();
                halfPixel.X = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
                halfPixel.Y = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferHeight;

                GraphicInfo.ChangeProps(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel, GraphicsDevice, GraphicsDevice.PresentationParameters.MultiSampleCount, GraphicsDevice.PresentationParameters.DepthStencilFormat, initialDescription.useMipMapWhenPossible, initialDescription.UseAnisotropicFiltering);
                GraphicInfo.FireEvent(GraphicInfo);

                initialDescription.BackBufferWidth = graphics.PreferredBackBufferWidth;
                initialDescription.BackBufferHeight = graphics.PreferredBackBufferHeight;

                //hack, cant initialize the scene now cause the device is lost now
                //and xna only reconstruct it in the next internal "update" method.
                ScreenAddedAfter = Screen;
            }
            else
            {
                ScreenManager.AddScreen(Screen);           
            }

            PageOrientation = PhoneApplicationPage.Orientation;

            //restart everything
            if (elementRenderer != null)
                elementRenderer.Dispose();
            
            elementRenderer = new UIElementRenderer(PhoneApplicationPage, GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight);
            
            if(EventHandler != null)
                PhoneApplicationPage.LayoutUpdated -= EventHandler;

            EventHandler = new EventHandler(PhoneApplicationPage_LayoutUpdated);
            PhoneApplicationPage.LayoutUpdated += EventHandler;

            this.PhoneApplicationPage.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(PhoneApplicationPage_OrientationChanged);
                        
            timer = new GameTimer();
            timer.UpdateInterval = initialDescription.UpdateInterval;
            timer.Update +=new EventHandler<GameTimerEventArgs>(timer_Update);
            timer.Draw += new EventHandler<GameTimerEventArgs>(timer_Draw);             
            timer.Start();

        }

        void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            //can i change the parameters ??            
            if (PhoneApplicationPage.SupportedOrientations == SupportedPageOrientation.PortraitOrLandscape
                ||
                (PhoneApplicationPage.SupportedOrientations == SupportedPageOrientation.Landscape && ( (e.Orientation & PageOrientation.Landscape ) == PageOrientation.Landscape))
                ||
                (PhoneApplicationPage.SupportedOrientations == SupportedPageOrientation.Portrait && ((e.Orientation & PageOrientation.Portrait) == PageOrientation.Portrait))
                )
            {
                
                if (PageOrientation != PhoneApplicationPage.Orientation)
                {
                    ///the manual way ....
                    graphics.PreferredBackBufferHeight = initialDescription.BackBufferWidth;
                    graphics.PreferredBackBufferWidth = initialDescription.BackBufferHeight;
                    graphics.ApplyChanges();

                    Rectangle fs = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
                    Vector2 halfPixel = new Vector2();
                    halfPixel.X = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
                    halfPixel.Y = 0.5f / (float)GraphicsDevice.PresentationParameters.BackBufferHeight;

                    GraphicInfo.ChangeProps(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel, GraphicsDevice, GraphicsDevice.PresentationParameters.MultiSampleCount, GraphicsDevice.PresentationParameters.DepthStencilFormat, initialDescription.useMipMapWhenPossible, initialDescription.UseAnisotropicFiltering);
                    GraphicInfo.FireEvent(GraphicInfo);

                    PageOrientation = PhoneApplicationPage.Orientation;
                    
                    initialDescription.BackBufferWidth = graphics.PreferredBackBufferWidth;
                    initialDescription.BackBufferHeight = graphics.PreferredBackBufferHeight;

                    //restart 
                    if (elementRenderer != null)
                        elementRenderer.Dispose();

                    elementRenderer = new UIElementRenderer(PhoneApplicationPage, GraphicInfo.BackBufferWidth, GraphicInfo.BackBufferHeight);

                    if (EventHandler != null)
                        PhoneApplicationPage.LayoutUpdated -= EventHandler;

                    EventHandler = new EventHandler(PhoneApplicationPage_LayoutUpdated);
                    PhoneApplicationPage.LayoutUpdated += EventHandler;
                }
            }
        }

        void PhoneApplicationPage_LayoutUpdated(object sender, EventArgs e)
        {                        
            // Create the UIElementRenderer to draw the Silverlight page to a texture.

            // Verify the page has a valid size
            if (PhoneApplicationPage.ActualWidth <= 0 && PhoneApplicationPage.ActualHeight <= 0)
            {
                return;
            }

            int width = (int)PhoneApplicationPage.ActualWidth;
            int height = (int)PhoneApplicationPage.ActualHeight;

            // See if the UIElementRenderer is alre ady the page's size
            if ((elementRenderer != null) &&
                (elementRenderer.Texture != null) &&
                (elementRenderer.Texture.Width == width) &&
                (elementRenderer.Texture.Height == height))
            {
                return;
            }

            // Dispose the UIElementRenderer before creating a new one
            if (elementRenderer != null)
            {
                elementRenderer.Dispose();
            }

           elementRenderer = new UIElementRenderer(PhoneApplicationPage, width, height);        
            
        }

        void timer_Draw(object sender, GameTimerEventArgs e)
        {
            //needed hack
            if (ScreenAddedAfter != null)
            {
                ScreenManager.AddScreen(ScreenAddedAfter);
                ScreenAddedAfter = null;
            }

            if(elementRenderer != null)
                elementRenderer.Render();
            
            GameTime GameTime = new GameTime(e.TotalTime, e.ElapsedTime);
            Draw(GameTime);


            if (elementRenderer != null)
            {
                render.RenderTextureComplete(elementRenderer.Texture);
            }
        }

        void timer_Update(object sender, GameTimerEventArgs e)
        {
            GameTime GameTime = new GameTime(e.TotalTime, e.ElapsedTime);
            Update(GameTime);
        }

        /// <summary>
        /// Updates the engine, called by XNA
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        private void Update(GameTime gameTime)
        {
                ComponentManager.Update(gameTime);
                ScreenManager.Update(gameTime);
                CommandProcessor.getCommandProcessor().ProcessCommands();
        }

        /// <summary>
        /// Gets or sets the content manager.
        /// </summary>
        /// <value>
        /// The content manager.
        /// </value>
        public IContentManager ContentManager
        {
            get { return contentManager; }
            set { contentManager = value; }
        }

        /// <summary>
        /// Reference page contains code sample.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        private void Draw(GameTime gameTime)
        {
                ScreenManager.Draw(gameTime);
        }
    }
}

#else
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Components;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.Input;
using PloobsEngine.Features;

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

    /// <summary>
    /// Delegate resposible to load the first screen
    /// </summary>
    /// <param name="ScreenManager">The screen manager.</param>
    public delegate void LoadScreen(ScreenManager ScreenManager);

    /// <summary>
    /// InitialEngineDescription
    /// </summary>
    public struct InitialEngineDescription
    {
        
        /// <summary>
        /// Defaults this instance.
        /// </summary>
        /// <returns></returns>
        public static InitialEngineDescription Default()
        {
            return new InitialEngineDescription("PloobsEngine",800,480,false,GraphicsProfile.Reach,false,false,true,null,false,false);
        }

        
        /// <summary>
        /// Initializes a new instance of the <see cref="InitialEngineDescription"/> struct.
        /// </summary>
        /// <param name="ScreenName">Name of the screen.</param>
        /// <param name="BackBufferWidth">Width of the back buffer.</param>
        /// <param name="BackBufferHeight">Height of the back buffer.</param>
        /// <param name="isFullScreen">if set to <c>true</c> [is full screen].</param>
        /// <param name="graphicsProfile">The graphics profile.</param>
        /// <param name="useVerticalSyncronization">if set to <c>true</c> [use vertical syncronization].</param>
        /// <param name="isMultiSampling">if set to <c>true</c> [is multi sampling].</param>
        /// <param name="isFixedGameTime">if set to <c>true</c> [is fixed game time].</param>
        /// <param name="logger">The logger.</param>
        /// <param name="useMipMapWhenPossible">if set to <c>true</c> [use mip map when possible].</param>
        /// <param name="UseAnisotropicFiltering">if set to <c>true</c> [use anisotropic filtering].</param>
        internal  InitialEngineDescription(String ScreenName = "PloobsEngine", int BackBufferWidth = 800, int BackBufferHeight = 600, bool isFullScreen = false, GraphicsProfile graphicsProfile = GraphicsProfile.HiDef, bool useVerticalSyncronization = false, bool isMultiSampling = false, bool isFixedGameTime = false, ILogger logger = null, bool useMipMapWhenPossible = false, bool UseAnisotropicFiltering = false,DisplayOrientation supportedOrientation = DisplayOrientation.Default | DisplayOrientation.Portrait | DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight)
        {            
            this.UseVerticalSyncronization = useVerticalSyncronization;
            this.BackBufferHeight = BackBufferHeight;
            this.BackBufferWidth = BackBufferWidth;
            this.Logger = logger;
            this.isMultiSampling = isMultiSampling;
            this.GraphicsProfile = graphicsProfile;
            this.isFullScreen = isFullScreen;
            this.isFixedGameTime = isFixedGameTime;
            onExitHandler = null;            
            OnExit = null;            
            this.ScreenName = ScreenName;
            this.useMipMapWhenPossible = useMipMapWhenPossible;
            this.UseAnisotropicFiltering = UseAnisotropicFiltering;
            this.SupportedOrientations = supportedOrientation;            
        }

        /// <summary>
        /// Use Anisotropic Filtering when possible
        /// </summary>
        public bool UseAnisotropicFiltering;
        
        /// <summary>
        /// Screen Name
        /// </summary>
        public String ScreenName;

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
        /// Use MipMap When creating the Render Targets
        /// </summary>
        public bool useMipMapWhenPossible;

        /// <summary>
        /// Supported DisplayOrientation 
        /// </summary>
        public DisplayOrientation SupportedOrientations;        

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
        internal GraphicsProfile GraphicsProfile;

        /// <summary>
        /// FullScreen Mode ?
        /// </summary>
        public bool isFullScreen;

        /// <summary>
        /// If the engine should force 60 fps
        /// </summary>
        public bool isFixedGameTime;

        /// <summary>
        /// handler Called when engine exit
        /// </summary>
        public OnExit OnExit;
        
        internal EventHandler<EventArgs> onExitHandler;        
 
    }

    /// <summary>
    /// Sound Options Desciption
    /// </summary>
    public struct SoundMasterOptionDescription
    {
        public SoundMasterOptionDescription(float DistanceScale, float DoplerScale,float MasterVolume)
        {
            this.DistanceScale = DistanceScale;
            this.DoplerScale = DoplerScale;
            this.MasterVolume = MasterVolume;            
        }

        public static SoundMasterOptionDescription Default()
        {            
            return new SoundMasterOptionDescription(1,1,1);
        }

        public float DistanceScale;
        public float DoplerScale;
        public float MasterVolume;        
    }

    /// <summary>
    /// Engine Entry point
    /// </summary>
    public class EngineStuff
    {
        SoundMasterOptionDescription soundMasterOptionDescription = SoundMasterOptionDescription.Default();
        InitialEngineDescription initialDescription;
        GraphicsDeviceManager graphics;
        ScreenManager ScreenManager;
        ComponentManager ComponentManager;
        GraphicInfo GraphicInfo;
        GraphicFactory GraphicFactory;
        LoadScreen LoadScreen;
        EngineContentManager contentManager;
        RenderHelper render;
        internal Game game;
        public event Action<Object, EventArgs> OrientationChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineStuff"/> class.
        /// </summary>
        /// <param name="initialDescription">The initial description.</param>
        /// <param name="LoadScreen">The load screen function.</param>
        public EngineStuff(Game game, ref InitialEngineDescription initialDescription, LoadScreen LoadScreen)
        {            
            System.Diagnostics.Debug.Assert(LoadScreen != null);
            this.LoadScreen = LoadScreen;
            this.initialDescription = initialDescription;
            this.game = game;
            game.IsFixedTimeStep = initialDescription.isFixedGameTime;            
            ActiveLogger.logger = initialDescription.Logger;                        
                
            if (this.initialDescription.OnExit != null)
            {
                EventHandler <EventArgs> evhandler = new EventHandler<EventArgs>(initialDescription.OnExit);
                this.initialDescription.onExitHandler = evhandler;
                this.game.Exiting += evhandler;
            }
                    
            game.Content.RootDirectory = "Content";
            this.game.Window.Title = initialDescription.ScreenName;

            graphics = new GraphicsDeviceManager(game);
            graphics.GraphicsProfile = initialDescription.GraphicsProfile;
            graphics.IsFullScreen = initialDescription.isFullScreen;
            graphics.SynchronizeWithVerticalRetrace = initialDescription.UseVerticalSyncronization;
            graphics.PreferMultiSampling = initialDescription.isMultiSampling;
            graphics.PreferredBackBufferHeight = initialDescription.BackBufferHeight;
            graphics.PreferredBackBufferWidth = initialDescription.BackBufferWidth;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;                                    
            graphics.SupportedOrientations = initialDescription.SupportedOrientations;
            this.game.Window.OrientationChanged += new EventHandler<EventArgs>(Window_OrientationChanged);
                 
        }


        void Window_OrientationChanged(object sender, EventArgs e)
        {
            if (OrientationChanged != null)
                OrientationChanged(sender, e);
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
            if (this.initialDescription.onExitHandler != null)
            {
                this.game.Exiting -= this.initialDescription.onExitHandler;
            }

            this.initialDescription = initialDescription;
            this.game.IsFixedTimeStep = initialDescription.isFixedGameTime;
            this.game.Window.Title = initialDescription.ScreenName;
            
            ActiveLogger.logger = initialDescription.Logger;                        
            graphics.GraphicsProfile = initialDescription.GraphicsProfile;
            graphics.IsFullScreen = initialDescription.isFullScreen;
            graphics.SynchronizeWithVerticalRetrace = initialDescription.UseVerticalSyncronization;
            graphics.PreferMultiSampling = initialDescription.isMultiSampling;
            graphics.PreferredBackBufferHeight = initialDescription.BackBufferHeight;
            graphics.PreferredBackBufferWidth = initialDescription.BackBufferWidth;
            graphics.SupportedOrientations = initialDescription.SupportedOrientations;
            graphics.ApplyChanges();
            
            if (this.initialDescription.OnExit != null)
            {
                EventHandler<EventArgs> evhandler = new EventHandler<EventArgs>(initialDescription.OnExit);
                this.initialDescription.onExitHandler = evhandler;
                this.game.Exiting += evhandler;
            }
            
            Rectangle fs = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Vector2 halfPixel = new Vector2();
            halfPixel.X = 0.5f / (float)game.GraphicsDevice.PresentationParameters.BackBufferWidth;
            halfPixel.Y = 0.5f / (float)game.GraphicsDevice.PresentationParameters.BackBufferHeight;

            GraphicInfo.ChangeProps(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel, game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.MultiSampleCount, game.GraphicsDevice.PresentationParameters.DepthStencilFormat, initialDescription.useMipMapWhenPossible, initialDescription.UseAnisotropicFiltering);
            GraphicInfo.FireEvent(GraphicInfo);
        }

        /// <summary>
        /// Gets the sound master option description.
        /// </summary>
        /// <returns></returns>
        public SoundMasterOptionDescription GetSoundMasterOptionDescription()
        {
            return soundMasterOptionDescription;
        }

        /// <summary>
        /// Sets the sound master option description.
        /// </summary>
        /// <param name="soundMasterOptionDescription">The sound master option description.</param>
        public void SetSoundMasterOptionDescription(ref SoundMasterOptionDescription soundMasterOptionDescription)
        {
            this.soundMasterOptionDescription = soundMasterOptionDescription;
            SoundEffect.DistanceScale = soundMasterOptionDescription.DistanceScale;
            SoundEffect.DopplerScale = soundMasterOptionDescription.DoplerScale;
            SoundEffect.MasterVolume = soundMasterOptionDescription.MasterVolume;
        }



        /// <summary>
        /// Load the content
        /// </summary>
        public void LoadContent()
        {
            Rectangle fs = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Vector2 halfPixel = new Vector2()
            {
                X = 0.5f / (float)game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Y = 0.5f / (float)game.GraphicsDevice.PresentationParameters.BackBufferHeight
            };
            
            contentManager = new EngineContentManager(this.game);
            GraphicInfo = new GraphicInfo(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, fs, halfPixel, this.game.GraphicsDevice, this.game.GraphicsDevice.PresentationParameters.MultiSampleCount, this.game.GraphicsDevice.PresentationParameters.DepthStencilFormat, initialDescription.useMipMapWhenPossible, this, initialDescription.UseAnisotropicFiltering);
            this.game.GraphicsDevice.DeviceReset += new EventHandler<EventArgs>(GraphicsDevice_DeviceReset);
            GraphicFactory = new Engine.GraphicFactory(GraphicInfo, this.game.GraphicsDevice, contentManager);
            ComponentManager = new ComponentManager(GraphicInfo, GraphicFactory);
            render = new RenderHelper(this.game.GraphicsDevice, ComponentManager, contentManager);
            GraphicFactory.render = render;
            ComponentManager.LoadContent(ref GraphicInfo);            
            render.PushBlendState(BlendState.Opaque);
            render.PushDepthStencilState(DepthStencilState.Default);
            render.PushRasterizerState(RasterizerState.CullCounterClockwise);
            render.PushRenderTarget(null);
            
            ScreenManager = new ScreenManager(ref GraphicInfo, GraphicFactory, contentManager,render,this);            

            LoadScreen(ScreenManager);
            if (ScreenManager.GetScreens().Count() == 0)
            {
                ActiveLogger.LogMessage("LoadScreen should create at least one screen", LogLevel.FatalError);
                System.Diagnostics.Debug.Assert(ScreenManager.GetScreens().Count() != 0);
                game.Exit();
            }

            ///THE ONLY COMPONENTS ADDED BY DEFAULT                        
               ComponentManager.AddComponent(new InputAdvanced());
          
            ComponentManager.AddComponent(new TaskProcessor());
        }

        void GraphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            GraphicInfo.FireResetEvent(sender, e);
        }

        /// <summary>
        /// Removes the subscribers for the event Device Lost of GraphicInfo obj.
        /// This is usefull cause it releases references and let GC works
        /// Call this (if needed) in screen transitions (if you dont need old screens anymore)
        /// </summary>
        public void RemoveSubscribersForGraphicEvents()
        {
            GraphicInfo.RemoveSubscribers();
        }
        
        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="component">The component.</param>
        public void AddComponent(IComponent component)
        {
            if (component == null)
                ActiveLogger.LogMessage("Cant add null Component", LogLevel.RecoverableError);
            else
            {
                bool resp = ComponentManager.AddComponent(component);
                if (!resp)
                    ActiveLogger.LogMessage("Component already added ", LogLevel.Warning);
            }
        }

        /// <summary>
        /// Removes the component by name
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        public void RemoveComponent(String componentName)
        {
            if (String.IsNullOrEmpty(componentName))
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
            else
            {
                bool resp = ComponentManager.RemoveComponent(componentName);
                if (!resp)
                    ActiveLogger.LogMessage("Component already Removed", LogLevel.Warning);
            }
        }

        /// <summary>
        /// Gets the component by name.
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="componentName">Name of the component.</param>
        /// <returns></returns>
        public T GetComponent<T>(String componentName)  where T : IComponent
        {
            if (String.IsNullOrEmpty(componentName))
            {
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
                return null;
            }
            else
            {
                IComponent comp = ComponentManager.GetComponent(componentName);
                if (comp == null)
                    ActiveLogger.LogMessage("Component not found " + componentName, LogLevel.RecoverableError);
                return (T)comp;
            }
        }

        /// <summary>
        /// Determines whether the specified component name exist.
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        /// <returns>
        ///   <c>true</c> if the specified component name has component; otherwise, <c>false</c>.
        /// </returns>
        public bool HasComponent(String componentName)
        {
            if (String.IsNullOrEmpty(componentName))
            {
                ActiveLogger.LogMessage("Bad Component name", LogLevel.RecoverableError);
                return false;
            }
            else
            {
                return ComponentManager.HasComponent(componentName);
            }
        }

        /// <summary>
        /// This is used to display an error message if there is no suitable graphics device or sound card.
        /// </summary>
        /// <param name="exception">The exception to display.</param>
        /// <returns></returns>
        public void ShowMissingRequirementMessage(Exception exception)
        {
            ActiveLogger.LogMessage(exception.Message, LogLevel.FatalError);            
        }

        /// <summary>
        /// Updates the engine, called by XNA
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        public void Update(GameTime gameTime)
        {
            if ((initialDescription.isFullScreen == true && this.game.IsActive) || initialDescription.isFullScreen == false)
            {
                ComponentManager.Update(gameTime);
                ScreenManager.Update(gameTime);
                CommandProcessor.getCommandProcessor().ProcessCommands();
            }            
        }

        
        /// <summary>
        /// Reference page contains code sample.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        public void Draw(GameTime gameTime)
        {
            if ((initialDescription.isFullScreen == true && this.game.IsActive) || initialDescription.isFullScreen == false)
            {
                ScreenManager.Draw(gameTime);             
            }            
        }        
        
    }

    
}
#endif