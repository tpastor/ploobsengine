using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.ComponentModel;
namespace ContentLibrary
{

    [ContentProcessor(DisplayName = "Gamma Decode Texture Processor")]
    class GameTextureProcessor : ContentProcessor<Texture2DContent, Texture2DContent>
    {

        [DisplayName("Encode to SRGB after mipmap")]
        [DefaultValue(false)]        
        public bool EncodeAfter
        {
            get;
            set;
        }

        [DisplayName("Use RGBA10")]
        [DefaultValue(false)]
        public bool UseRGBA10
        {
            get;
            set;
        }

        // - - - - - - - - - - - - - - - - - - - -
        public override Texture2DContent Process(Texture2DContent texInput, ContentProcessorContext context)
        {

            //System.Diagnostics.Debugger.Launch();

            // Input Bitmap
            PixelBitmapContent<Color> bmpInput = (PixelBitmapContent<Color>)texInput.Mipmaps[0];
            if (!UseRGBA10)
            {
                // Gamma Decode            
                PixelBitmapContent<Color> bmpDecoded = Decode(bmpInput);

                // Create Intermediate Content
                Texture2DContent texMipMap = new Texture2DContent();

                // Add decoded Vector4
                texMipMap.Mipmaps.Add(bmpDecoded);

                // Generate Mip Maps
                texMipMap.GenerateMipmaps(true);

                // Create Output Content
                Texture2DContent texOutput = new Texture2DContent();


                // Passthrough Properties
                texOutput.Name = texInput.Name;
                texOutput.Identity = texInput.Identity;

                // Convert each bitmap to Gamma Encoded SurfaceFormat.Color
                for (int mi = 0; mi < texMipMap.Mipmaps.Count; mi++)
                {

                    // Get Mip Map
                    PixelBitmapContent<Color> bmpMipMap = (PixelBitmapContent<Color>)texMipMap.Mipmaps[mi];

                    // Create Color Bitmap of Equal Size
                    if (EncodeAfter)
                    {
                        PixelBitmapContent<Color> bmpColor = Encode(bmpMipMap);
                        texOutput.Mipmaps.Add(bmpColor);
                    }
                    else
                    {
                        texOutput.Mipmaps.Add(bmpMipMap);
                    }


                }//for

                // Return Gamma Encoded Texture with Linear Filtered Mip Maps
                return texOutput;
            }
            else
            {
                // Gamma Decode            
                PixelBitmapContent<Rgba1010102> bmpDecoded = Decode2(bmpInput);

                // Create Intermediate Content
                Texture2DContent texMipMap = new Texture2DContent();

                // Add decoded Vector4
                texMipMap.Mipmaps.Add(bmpDecoded);

                // Generate Mip Maps
                texMipMap.GenerateMipmaps(true);

                // Create Output Content
                Texture2DContent texOutput = new Texture2DContent();


                // Passthrough Properties
                texOutput.Name = texInput.Name;
                texOutput.Identity = texInput.Identity;

                // Convert each bitmap to Gamma Encoded SurfaceFormat.Color
                for (int mi = 0; mi < texMipMap.Mipmaps.Count; mi++)
                {

                    // Get Mip Map
                    PixelBitmapContent<Rgba1010102> bmpMipMap = (PixelBitmapContent<Rgba1010102>)texMipMap.Mipmaps[mi];

                    // Create Color Bitmap of Equal Size
                    if (EncodeAfter)
                    {
                        PixelBitmapContent<Rgba1010102> bmpColor = Encode2(bmpMipMap);
                        texOutput.Mipmaps.Add(bmpColor);
                    }
                    else
                    {
                        texOutput.Mipmaps.Add(bmpMipMap);
                    }


                }//for

                // Return Gamma Encoded Texture with Linear Filtered Mip Maps
                return texOutput;
            }

        }//method
        // - - - - - - - - - - - - - - - - - - - -

        public PixelBitmapContent<Rgba1010102> Decode2(PixelBitmapContent<Color> bmpInput)
        {
            // Decoded Bitmap
            PixelBitmapContent<Rgba1010102> bmpDecoded = new PixelBitmapContent<Rgba1010102>(bmpInput.Width, bmpInput.Height);

            // Convert each pixel to gamma decoded float
            for (int y = 0; y < bmpInput.Height; y++)
            {

                for (int x = 0; x < bmpInput.Width; x++)
                {

                    // Get Input Pixel
                    Color Cinput = bmpInput.GetPixel(x, y);

                    // Set Output Pixel
                    bmpDecoded.SetPixel(x, y, GammaDecodeColor2(Cinput.ToVector4()));

                }//for (x)

            }//for (y)

            return bmpDecoded;

        }
        PixelBitmapContent<Color> Decode(PixelBitmapContent<Color> bmpInput)
        {

            // Decoded Bitmap
            PixelBitmapContent<Color> bmpDecoded = new PixelBitmapContent<Color>(bmpInput.Width, bmpInput.Height);            

            // Convert each pixel to gamma decoded float
            for (int y = 0; y < bmpInput.Height; y++)
            {

                for (int x = 0; x < bmpInput.Width; x++)
                {

                    // Get Input Pixel
                    Color Cinput = bmpInput.GetPixel(x, y);
                    
                    // Set Output Pixel
                    bmpDecoded.SetPixel(x, y, GammaDecodeColor(Cinput.ToVector4()));

                }//for (x)

            }//for (y)

            return bmpDecoded;

        }//method
        // - - - - - - - - - - - - - - - - - - - -
        PixelBitmapContent<Color> Encode(PixelBitmapContent<Color> bmpMipMap)
        {
            // Create Color Bitmap of Equal Size
            PixelBitmapContent<Color> bmpColor = new PixelBitmapContent<Color>(bmpMipMap.Width, bmpMipMap.Height);

            // Convert each pixel to gamma encoded color
            for (int y = 0; y < bmpMipMap.Height; y++)
            {

                for (int x = 0; x < bmpMipMap.Width; x++)
                {

                    // Get Input Pixel
                    Color CmipMap = bmpMipMap.GetPixel(x, y);                    

                    // Set Output Pixel
                    bmpColor.SetPixel(x, y, GammaEncodeColor(CmipMap.ToVector4()));

                }//for (x)

            }//for (y)

            return bmpColor;

        }//method

        PixelBitmapContent<Rgba1010102> Encode2(PixelBitmapContent<Rgba1010102> bmpMipMap)
        {
            // Create Color Bitmap of Equal Size
            PixelBitmapContent<Rgba1010102> bmpColor = new PixelBitmapContent<Rgba1010102>(bmpMipMap.Width, bmpMipMap.Height);

            // Convert each pixel to gamma encoded color
            for (int y = 0; y < bmpMipMap.Height; y++)
            {

                for (int x = 0; x < bmpMipMap.Width; x++)
                {

                    // Get Input Pixel
                    Rgba1010102 CmipMap = bmpMipMap.GetPixel(x, y);

                    // Set Output Pixel
                    bmpColor.SetPixel(x, y, GammaEncodeColor2(CmipMap.ToVector4()));

                }//for (x)

            }//for (y)

            return bmpColor;

        }//method

        // - - - - - - - - - - - - - - - - - - - -
        byte ConvertFloat(float val)
        {
            return (byte)(val * 255.0f);
        }//method
        // - - - - - - - - - - - - - - - - - - - -
        byte GammaEncodeFloat(float val)
        {
            double encoded = Math.Pow(val, 1.0 / 2.2);
            double expanded = encoded * 255.0;
            return (byte)expanded;
        }//method
        // - - - - - - - - - - - - - - - - - - - -
        float NormalizeByte(byte val)
        {
            float unit = (float)val / 255.0f;
            return unit;
        }//method
        // - - - - - - - - - - - - - - - - - - - -
        float GammaDecodeByte(byte val)
        {
            double unit = (double)val / 255.0;
            double decode = Math.Pow(unit, 2.2);
            return (float)decode;
        }//method

        Color GammaEncodeColor(Vector4 vec)
        {
            Vector4 resp = new Vector4(1);
            resp.X = (float)Math.Pow(vec.X, 1.0 / 2.2);
            resp.Y = (float)Math.Pow(vec.Y, 1.0 / 2.2);
            resp.Z = (float)Math.Pow(vec.Z, 1.0 / 2.2);
            return Color.FromNonPremultiplied(resp); ;
        }

        Rgba1010102 GammaEncodeColor2(Vector4 vec)
        {
            Vector4 resp = new Vector4(1);
            resp.X = (float)Math.Pow(vec.X, 1.0 / 2.2);
            resp.Y = (float)Math.Pow(vec.Y, 1.0 / 2.2);
            resp.Z = (float)Math.Pow(vec.Z, 1.0 / 2.2);
            return new Rgba1010102(resp); ;
        }

        Color GammaDecodeColor(Vector4 vec)
        {
            Vector4 resp = new Vector4(1);
            resp.X = (float)Math.Pow(vec.X, 2.2);
            resp.Y = (float)Math.Pow(vec.Y, 2.2);
            resp.Z = (float)Math.Pow(vec.Z, 2.2);
            return Color.FromNonPremultiplied(resp); ;
        }

        static Rgba1010102 GammaDecodeColor2(Vector4 vec)
        {
            Vector4 resp = new Vector4(1);
            resp.X = (float)Math.Pow(vec.X, 2.2);
            resp.Y = (float)Math.Pow(vec.Y, 2.2);
            resp.Z = (float)Math.Pow(vec.Z, 2.2);
            return new Rgba1010102(resp); 
        }

        // - - - - - - - - - - - - - - - - - - - -
    }//class
    // - - - - - - - - - - - - - - - - - - - -
}//namespace
// - - - - - - - - - - - - - - - - - - - -
