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
  public class AddonControls: Game
  {

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////
    private GraphicsDeviceManager graphics;
    
    private Manager manager; // GUI manager
    private Window window; // Main window
    private Button button; // Standard button
    private CustomButton custom; // Our custom button (see CustomButton.cs)    
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////
    public AddonControls()
    {
      // Basic setup of the game window and GUI manager. 
      graphics = new GraphicsDeviceManager(this);     
            
      IsMouseVisible = true;
      IsFixedTimeStep = false;
      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;
      graphics.SynchronizeWithVerticalRetrace = false;
      
      manager = new Manager(this, graphics, "Default");

      manager.SkinDirectory = @"Content\Skins\";
    }
    ////////////////////////////////////////////////////////////////////////////    

    #endregion 
    
		#region //// Methods ///////////
			
		////////////////////////////////////////////////////////////////////////////
		protected override void Initialize()
    {
      base.Initialize();                                               
            
      // Controls setup.
            
      window = new Window(manager);
      window.Init();      
      window.Text = "Addon Controls";
      window.Width = 480;
      window.Height = 200;      
      window.Center();
      window.Visible = true;

      button = new Button(manager);
      button.Init();
      button.Text = "DEFAULT";
      button.Width = 72;
      button.Height = 24;
      button.Left = (window.ClientWidth / 2) - 4 - button.Width;
      button.Top = window.ClientHeight - button.Height - 32;
      button.Anchor = Anchors.Bottom;
      button.Parent = window;  
      button.Focused = true;
            
      // Here we construct our custom button.
      custom = new CustomButton(manager);
      custom.Init();
      custom.Text = "CUSTOM";
      custom.Width = 72;
      custom.Height = 24;
      custom.Left = (window.ClientWidth / 2) + 4;
      custom.Top = window.ClientHeight - button.Height - 32;
      custom.Anchor = Anchors.Bottom;
      custom.Parent = window;
      custom.Cursor = manager.Skin.Cursors["Custom.Cursor"].Resource;
            
      manager.Add(window);
    }
    ////////////////////////////////////////////////////////////////////////////   
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Draw(GameTime gameTime)
    {
      graphics.GraphicsDevice.Clear(Color.White);
      base.Draw(gameTime);
    }
  	////////////////////////////////////////////////////////////////////////////
			
		#endregion  
		
	}
}
