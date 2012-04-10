using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.ComponentModel;
namespace ContentLibrary
{

    [ContentProcessor(DisplayName = "Blur CubeMap")]
    class ContentProcessor4 : ContentProcessor<TextureCubeContent, TextureCubeContent>
    {


        public override TextureCubeContent Process(TextureCubeContent input, ContentProcessorContext context)
        {
//          System.Diagnostics.Debugger.Launch();
            TextureCubeContent tc = new TextureCubeContent();            
            tc.Name = input.Name;
            tc.Identity = input.Identity;
            int i = 0;
            foreach (var item in input.Faces)
            {                
                PixelBitmapContent<Color> bmpInput = (PixelBitmapContent<Color>)item[0];
                BitmapContent BitmapContent = BlurCubemapFace(bmpInput,bmpInput.Height,bmpInput.Width);
                tc.Faces[i++] = BitmapContent;
            }
            return tc;
        }
        
        /// <summary>
        /// The top and bottom cubemap faces will have a nasty discontinuity
        /// in the middle where the four source image flaps meet. We can cover
        /// this up by applying a blur filter to the problematic area.
        /// </summary>
        static BitmapContent BlurCubemapFace(PixelBitmapContent<Color> source, int height, int width)
        {
            // Create two temporary bitmaps.
            PixelBitmapContent<Vector4> temp1, temp2;

            temp1 = new PixelBitmapContent<Vector4>(height, width);
            temp2 = new PixelBitmapContent<Vector4>(height, width);

            // Antialias by shrinking the larger generated image to the final size.
            BitmapContent.Copy(source, temp1);

            // Apply the blur in two passes, first horizontally, then vertically.
            ApplyBlurPass(temp1, temp2, 1, 0,width,height);
            ApplyBlurPass(temp2, temp1, 0, 1,width,height);

            // Convert the result back to Color format.
            PixelBitmapContent<Color> result;

            result = new PixelBitmapContent<Color>(height, width);

            BitmapContent.Copy(temp1, result);

            return result;
        }


        /// <summary>
        /// Applies a single pass of a separable box filter, blurring either
        /// along the x or y axis. This could give much higher quality results
        /// if we used a gaussian filter kernel rather than this simplistic box,
        /// but this is good enough to get the job done.
        /// </summary>
        static void ApplyBlurPass(PixelBitmapContent<Vector4> source,
                                  PixelBitmapContent<Vector4> destination,
                                  int dx, int dy, int width, int height)
        {
            int m = (height + width) / 2;
            int cubemapCenter = m / 2;

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    // How far is this texel from the center of the image?
                    int xDist = cubemapCenter - x;
                    int yDist = cubemapCenter - y;

                    int distance = (int)Math.Sqrt(xDist * xDist + yDist * yDist);

                    // Blur more in the center, less near the edges.
                    int blurAmount = Math.Max(cubemapCenter - distance, 0) / 8;

                    // Accumulate source texel values.
                    Vector4 blurredValue = Vector4.Zero;

                    for (int i = -blurAmount; i <= blurAmount; i++)
                    {
                        blurredValue += source.GetPixel(x + dx * i, y + dy * i);
                    }

                    // Average them to calculate a blurred result.
                    blurredValue /= blurAmount * 2 + 1;

                    destination.SetPixel(x, y, blurredValue);
                }
            }
        }
              
    }//class
    // - - - - - - - - - - - - - - - - - - - -
}//namespace
// - - - - - - - - - - - - - - - - - - - -
