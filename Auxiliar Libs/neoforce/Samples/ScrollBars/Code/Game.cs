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
  public class ScrollBars: Game
  {

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////
    private GraphicsDeviceManager graphics;    
    private Manager manager; // GUI manager
    private Window window;  // Main window   
    private ScrollBar vert; // Vertical scrollbar  
    private ScrollBar horz; // Horizontal scrollbar
    private Texture2D image; // Image we wanna display in the window    
    private ImageBox box; // Imagebox showing the image
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////
    public ScrollBars()
    {
      // Basic setup.
      
      graphics = new GraphicsDeviceManager(this);     
      
      IsMouseVisible = true;
      IsFixedTimeStep = false;
      graphics.PreferredBackBufferWidth = 800;
      graphics.PreferredBackBufferHeight = 600;
      graphics.SynchronizeWithVerticalRetrace = false;
      
      // Creates manager and sets the path to skins 
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
      
      // Load image      
      image = Content.Load<Texture2D>("Content\\Image");
                                                
      // Creates and initializes window
      window = new Window(manager);
      window.Init();      
      window.Text = "ScrollBars";
      window.Width = 360;
      window.Height = 360;      
      window.Center();
      window.Visible = true;      

      // Setup of the vertical scrollbar      
      vert = new ScrollBar(manager, Orientation.Vertical);
      vert.Init();
      vert.Parent = window;
      vert.Top = 0;
      vert.Left = window.ClientWidth - vert.Width ;
      vert.Height = window.ClientHeight - vert.Width;
      vert.Value = 0;
      vert.Anchor = Anchors.Top | Anchors.Right | Anchors.Bottom;

      // Setup of the horizontal scrollbar
      horz = new ScrollBar(manager, Orientation.Horizontal);
      horz.Init();
      horz.Parent = window;
      horz.Left = 0;
      horz.Top = window.ClientHeight - horz.Height;
      horz.Width = window.ClientWidth - vert.Width;
      horz.Value = 0;
      horz.Anchor = Anchors.Left | Anchors.Right | Anchors.Bottom;
      
      // Creates imagebox in the client area
      box = new ImageBox(manager);
      box.Init();
      box.Parent = window;
      box.Left = 0;
      box.Top = 0;
      box.Width = window.ClientWidth - vert.Width;
      box.Height = window.ClientHeight - horz.Height;
      box.Image = image;      
      box.Anchor = Anchors.All;
            
      // Add window to manager processing
      manager.Add(window);

      // Bind events
      window.Resize += new ResizeEventHandler(Recalc);
      vert.ValueChanged += new EventHandler(ValueChanged);
      horz.ValueChanged += new EventHandler(ValueChanged);
      Recalc(this, null); // Calculates initial properties of the scrollbars
    }
    ////////////////////////////////////////////////////////////////////////////   
        
    ////////////////////////////////////////////////////////////////////////////   
    void Recalc(object sender, ResizeEventArgs e)
    {
      // Disable scrollbars when there is nothing to scroll
      horz.Enabled = box.Width < image.Width;
      vert.Enabled = box.Height < image.Height; 
            
      vert.Range = image.Height;  // Set range to width of the image, one step equals to one pixel of the image
      vert.PageSize = box.Height; // Size of the slider according to displayed portion of the image
      
      horz.Range = image.Width; // Same like above, just for horizontal scrollbar
      horz.PageSize = box.Width;
            
      // Portion of the image we are actually displaying in the imagebox
      box.SourceRect = new Rectangle(horz.Value, vert.Value, box.Width, box.Height);
      
      // Make the imagebox redraw
      box.Invalidate(); 
    }
    ////////////////////////////////////////////////////////////////////////////   

    ////////////////////////////////////////////////////////////////////////////   
    void ValueChanged(object sender, EventArgs e)
    {
      // For every scrollbar value change we recalt properties
      Recalc(sender, null);
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
