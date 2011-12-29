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
  public class GettingStarted: Application
  {

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////    
    // Define controls we use.
    private Window window;
    private Button button;   
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////
    // We use Default skin and we don't want desktop window to be created
    public GettingStarted(): base("Default", false)
    {
      Graphics.PreferredBackBufferWidth = 800;
      Graphics.PreferredBackBufferHeight = 600;                  
      
      // Setting up the shared skins directory
      Manager.SkinDirectory = @"Content\Skins\";       
      
      ClearBackground = true;
      BackgroundColor = Color.White;    
      ExitConfirmation = false;
      
      Manager.AutoUnfocus = false;  
    }
    ////////////////////////////////////////////////////////////////////////////    

    #endregion 
    
		#region //// Methods ///////////
			
		////////////////////////////////////////////////////////////////////////////
		protected override void Initialize()
    {
      base.Initialize();                                                                      
      
      // Create and setup Window control.
      window = new Window(Manager);
      window.Init();      
      window.Text = "Getting Started";
      window.Width = 480;
      window.Height = 200;      
      window.Center();
      window.Visible = true;      
      
      // Create Button control and set the previous window as its parent.
      button = new Button(Manager);
      button.Init();
      button.Text = "OK";
      button.Width = 72;
      button.Height = 24;
      button.Left = (window.ClientWidth / 2) - (button.Width / 2);
      button.Top = window.ClientHeight - button.Height - 8;
      button.Anchor = Anchors.Bottom; 
      button.Parent = window;
      
      // Add the window control to the manager processing queue.
      Manager.Add(window);
    }
    ////////////////////////////////////////////////////////////////////////////   
      
		#endregion  
		
	}
}
