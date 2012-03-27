using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.ComponentModel;
namespace ContentLibrary
{

    [ContentProcessor(DisplayName = "Gamma Decode TextureCube Processor")]
    class ContentProcessor2 : ContentProcessor<TextureCubeContent, TextureCubeContent>
    {

        [DisplayName("Encode to SRGB after mipmap")]
        [DefaultValue(false)]
        public bool EncodeAfter
        {
            get;
            set;
        }


        public override TextureCubeContent Process(TextureCubeContent input, ContentProcessorContext context)
        {
//            System.Diagnostics.Debugger.Launch();
            TextureCubeContent tc = new TextureCubeContent();            
            tc.Name = input.Name;
            tc.Identity = input.Identity;
            int i = 0;
            foreach (var item in input.Faces)
            {
                PixelBitmapContent<Color> bmpInput = (PixelBitmapContent<Color>)item[0];
                
                // Create Intermediate Content
                Texture2DContent texMipMap = new Texture2DContent();

                // Add decoded Vector4
                texMipMap.Mipmaps.Add(Decode2(bmpInput));

                // Generate Mip Maps
                texMipMap.GenerateMipmaps(true);
                MipmapChain mc = new MipmapChain();
                // Convert each bitmap to Gamma Encoded SurfaceFormat.Color
                for (int mi = 0; mi < texMipMap.Mipmaps.Count; mi++)
                {
                    // Get Mip Map
                    PixelBitmapContent<Rgba1010102> bmpMipMap = (PixelBitmapContent<Rgba1010102>)texMipMap.Mipmaps[mi];
                    if (EncodeAfter)
                    {
                        PixelBitmapContent<Rgba1010102> bmpColor = Encode2(bmpMipMap);
                        mc.Add(bmpColor);
                    }
                    else
                    {
                        mc.Add(bmpMipMap);
                    }
                }                

                tc.Faces[i++] = mc;
            }

         return tc;
        }

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

        Rgba1010102 GammaEncodeColor2(Vector4 vec)
        {
            Vector4 resp = new Vector4(1);
            resp.X = (float)Math.Pow(vec.X, 1.0 / 2.2);
            resp.Y = (float)Math.Pow(vec.Y, 1.0 / 2.2);
            resp.Z = (float)Math.Pow(vec.Z, 1.0 / 2.2);
            return new Rgba1010102(resp); ;
        }

        static Rgba1010102 GammaDecodeColor2(Vector4 vec)
        {
            Vector4 resp = new Vector4(1);
            resp.X = (float)Math.Pow(vec.X, 2.2);
            resp.Y = (float)Math.Pow(vec.Y, 2.2);
            resp.Z = (float)Math.Pow(vec.Z, 2.2);
            return new Rgba1010102(resp);
        }

       
    }//class
    // - - - - - - - - - - - - - - - - - - - -
}//namespace
// - - - - - - - - - - - - - - - - - - - -
