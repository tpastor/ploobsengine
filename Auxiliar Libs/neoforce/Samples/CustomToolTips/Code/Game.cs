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
  public class CustomToolTips: Game
  {

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////
    private GraphicsDeviceManager graphics;
    
    private Manager manager; // GUI manager
    private Window window; // Main window
    private Button button; // Standard button showing default tooltip   
    private TextBox textbox; // Textbox showing custom tooltip
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////
    public CustomToolTips()
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

      manager.Initialize();      
            
      // Controls setup.
            
      window = new Window(manager);
      window.Init();      
      window.Text = "Custom ToolTips";
      window.Width = 480;
      window.Height = 200;      
      window.Center();
      window.Visible = true;

      button = new Button(manager);
      button.Init();
      button.Text = "Standard";
      button.Width = 72;
      button.Height = 24;
      button.Left = (window.ClientWidth / 2) - (button.Width / 2);
      button.Top = window.ClientHeight - button.Height - 8;
      button.Anchor = Anchors.Bottom;
      button.Parent = window;  
      button.Focused = true; 
      button.ToolTip.Text = "This is standard tooltip.";  
      
      textbox = new TextBox(manager);
      textbox.Init();
      textbox.Parent = window;
      textbox.Left = 32;
      textbox.Top = 32;
      textbox.Width = window.ClientWidth - 64;
      textbox.TextChanged += new EventHandler(TextChanged);                    
      
      // Set the tooltip class we want to constuct for this control 
      textbox.ToolTipType = typeof(CustomToolTip); 
      
      // Now we can set custom properties of the custom tooltip.
      // Tooltip instance is created by the first access to the ToolTip property and disposed with the control
      (textbox.ToolTip as CustomToolTip).Image = manager.Skin.Images["Icon.Information"].Resource;
      
      textbox.Text = ">>> Type your custom tooltip message here. <<<";
      textbox.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;                  
            
      manager.Add(window);
    }
    ////////////////////////////////////////////////////////////////////////////   
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      manager.Update(gameTime);
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Draw(GameTime gameTime)
    {
      manager.BeginDraw(gameTime);
      graphics.GraphicsDevice.Clear(Color.White);
      base.Draw(gameTime);

      manager.EndDraw();
    }
  	////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////   
    void TextChanged(object sender, EventArgs e)
    {
      // We alter text from the textbox
      textbox.ToolTip.Text = textbox.Text + "\n--- Second line added automatically. ---";
    }
    ////////////////////////////////////////////////////////////////////////////   
			
		#endregion  
		
	}
}
