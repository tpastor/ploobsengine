////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Samples                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: CustomToolTip.cs                             //
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

  #region //// Classes ///////////
    
  ////////////////////////////////////////////////////////////////////////////  
  public class CustomToolTip: ToolTip
  {

    #region //// Consts ////////////

    ////////////////////////////////////////////////////////////////////////////
    public int IconSize = 32; // Size of the icon we will be displaying
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////
    private Texture2D image = null; // Image we show in tooltip   
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Properties ////////

    ////////////////////////////////////////////////////////////////////////////
    // Visible property should be always overriden in this manner.
    // You can set width and height of the tooltip according to it's content.    
    public override bool Visible
    {
      set
      {
        base.Visible = value;
                
        Vector2 size = Skin.Layers[0].Text.Font.Resource.MeasureString(Text);
        Width = (int)size.X + Skin.Layers[0].ContentMargins.Horizontal + IconSize + 16;
        Height = (int)size.Y + Skin.Layers[0].ContentMargins.Vertical;

        if (Height < IconSize + Skin.Layers[0].ContentMargins.Vertical + 8) Height = IconSize + Skin.Layers[0].ContentMargins.Vertical + 8;
      }
    } 
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    public Texture2D Image
    {
      get { return image; }
      set { image = value; }
    }
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Construstors //////

    ////////////////////////////////////////////////////////////////////////////       
    // Standard constructor
    public CustomToolTip(Manager manager): base(manager)
    {
    }
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Methods ///////////

    ////////////////////////////////////////////////////////////////////////////
    public override void Init()
    {
      base.Init();
    }
    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    protected override void InitSkin()
    {
      base.InitSkin();
      
      // We specify what skin this control uses. We use standard tooltip background here.
      Skin = new SkinControl(Manager.Skin.Controls["ToolTip"]);
    }
    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
    {      
      SkinLayer l = Skin.Layers[0];

      // We render background of the tooltip
      renderer.DrawLayer(this, l, rect);           
            
      Rectangle rc1 = Rectangle.Empty;
      if (image != null)
      {        
        // Now we draw image in the left top corner of the tooltip
        rc1 = new Rectangle(l.ContentMargins.Left, l.ContentMargins.Top + 4, IconSize, IconSize);
        renderer.Draw(image, rc1, Color.White);
      }
      
      // Text is rendered next to the image
      rect = new Rectangle(rc1.Right, rect.Top + 4, rect.Width, rect.Height);           
      
      // We alter text alignment from the default skin
      l.Text.Alignment = Alignment.TopLeft;
      renderer.DrawString(this, l, Text, rect, true);
    }
    ////////////////////////////////////////////////////////////////////////////         

    #endregion

  }

  #endregion

}
