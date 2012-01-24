////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Samples                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Game.cs                                      //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 11/09/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by Tom Shane                                //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Samples
{
  public class RenderTargets: Game
  {

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////
    private GraphicsDeviceManager graphics;

    // Define manager and few controls we use.
    private Manager manager;
    private Window window;
    private Button button;       
    private SpriteBatch sprite;
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////
    public RenderTargets()
    {
      graphics = new GraphicsDeviceManager(this);     
      
      // Basic setup of the game window.
      IsMouseVisible = true;
      IsFixedTimeStep = false;
      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;
      graphics.SynchronizeWithVerticalRetrace = false;
      
      // Create an instance of manager using Default skin. We set the fourth parameter to false,
      // so the instance of manager is not registered as an XNA game component and methods
      // like Initialize(), Update() and Draw() are called manually in the game loop.
      manager = new Manager(this, graphics, "Default");
            
      // Setting up the shared skins directory
      manager.SkinDirectory = @"..\..\Skins\";     
    }
    ////////////////////////////////////////////////////////////////////////////    

    #endregion 
    
		#region //// Methods ///////////
			
		////////////////////////////////////////////////////////////////////////////
		protected override void Initialize()
    {
      base.Initialize();
  
      // Create sprite batch for ploting texture with rendered UI.
      sprite = new SpriteBatch(GraphicsDevice);                         
                                                
      // Initialize manager.
      manager.Initialize();

      // Create and assign render target for UI rendering.
      manager.RenderTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
      // Maximum of frames we want to render per second (applies only for Neoforce, not Game itself)
      manager.TargetFrames = 60; 
                  
      // Create and setup Window control.
      window = new Window(manager);
      window.Init();      
      window.Text = "Render Targets";
      window.Width = 480;
      window.Height = 200;      
      window.Center();
      window.Visible = true;      
      
      // Create Button control and set the previous window as its parent.
      button = new Button(manager);
      button.Init();
      button.Text = "OK";
      button.Width = 72;
      button.Height = 24;
      button.Left = (window.ClientWidth / 2) - (button.Width / 2);
      button.Top = window.ClientHeight - button.Height - 8;
      button.Anchor = Anchors.Bottom; 
      button.Parent = window;
      
      // Add the window control to the manager processing queue.
      manager.Add(window);
    }
    ////////////////////////////////////////////////////////////////////////////   
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Update(GameTime gameTime)
    {
      base.Update(gameTime);            
      
      // Call manager updates.
      manager.Update(gameTime);
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Draw(GameTime gameTime)
    {
      // We need to draw controls before first draw to back buffer to avoid back buffer purple clear.
      manager.BeginDraw(gameTime);
            
      // Clear back buffer to White color.
      graphics.GraphicsDevice.Clear(Color.White);
            
      base.Draw(gameTime);
      
      
      // -- put your rendering code here -- 
      
      
      // We draw our already rendered UI texture here as a last thing to be drawn.
      manager.EndDraw();
    }
  	////////////////////////////////////////////////////////////////////////////
			
		#endregion  
		
	}
}
