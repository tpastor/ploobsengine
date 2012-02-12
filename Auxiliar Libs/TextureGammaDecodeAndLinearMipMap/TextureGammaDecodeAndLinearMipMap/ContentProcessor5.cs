using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

using TInput = Microsoft.Xna.Framework.Content.Pipeline.Graphics.TextureContent;
using TOutput = Microsoft.Xna.Framework.Content.Pipeline.Graphics.TextureContent;

namespace TextureGammaDecodeAndLinearMipMap
{
    public enum PixelFormat
    {
        Bgr565,
        Bgra4444,
    }

    public enum DitherMethod
    {
        None,
        FloydSteinberg,
    }

    [ContentProcessor(DisplayName = "Texture 16-bit - Konaju")]
    public class Tex16bits : ContentProcessor<TInput, TOutput>
    {
        [DefaultValue(typeof(DitherMethod), "None")]
        [DisplayName("Dither Method")]
        [Description("Set the dithering method used to approximate intermediate colors.")]
        public DitherMethod DitherMethod { get; set; }

        [DefaultValue(typeof(PixelFormat), "Bgr565")]
        [DisplayName("Pixel Format")]
        [Description("Set the pixel format. Use Bgr565 for images with no alpha values.")]
        public PixelFormat PixelFormat { get; set; }

        bool premultiplyAlpha = true;
        [DefaultValue(true)]
        [DisplayName("Premultiply Alpha")]
        [Description("If enabled, the texture is converted to premultipled alpha format.")]
        public bool PremultiplyAlpha { get { return premultiplyAlpha; } set { premultiplyAlpha = value; } }

        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            foreach (MipmapChain chain in input.Faces)
            {
                for (int i = 0; i < chain.Count; ++i)
                {
                    BitmapContent bitmap = chain[i];
                    if (bitmap is PixelBitmapContent<Color>)
                    {
                        switch (PixelFormat)
                        {
                            case PixelFormat.Bgr565:
                                chain[i] = Convert<Bgr565>((PixelBitmapContent<Color>)bitmap);
                                break;

                            case PixelFormat.Bgra4444:
                                chain[i] = Convert<Bgra4444>((PixelBitmapContent<Color>)bitmap);
                                break;
                        }
                    }
                }
            }
            return input;
        }

        BitmapContent Convert<T>(PixelBitmapContent<Color> bitmap) where T: struct, IPackedVector, IPackedVector<ushort>, IEquatable<T>
        {
            int w = bitmap.Width;
            int h = bitmap.Height;
            byte[] srcData = bitmap.GetPixelData();
            // BlockCopy only works on simple datatypes, not structs
            uint[] color = new uint[w * h];
            Buffer.BlockCopy(srcData, 0, color, 0, srcData.Length);
            int index = 0;
            ushort[] dest = new ushort[w * h];
            switch (DitherMethod)
            {
                case DitherMethod.None:
                    foreach (uint i in color)
                    {
                        // Convert the uint to a Color
                        Color c = new Color();
                        c.PackedValue = i;
                        Vector4 oldPixel = c.ToVector4();
                        if (PremultiplyAlpha)
                        {
                            // Set dest to the pre-multiplied value
                            oldPixel.X = oldPixel.X * oldPixel.W;
                            oldPixel.Y = oldPixel.Y * oldPixel.W;
                            oldPixel.Z = oldPixel.Z * oldPixel.W;
                        }
                        T d = new T();
                        // PackFromVector4 will convert to the destination format
                        d.PackFromVector4(oldPixel);
                        dest[index++] = d.PackedValue;
                    }
                    break;

                case DitherMethod.FloydSteinberg:
                    // Create a copy of the entire bitmap in Vector4 format
                    Vector4[] src = new Vector4[w * h];
                    foreach (uint i in color)
                    {
                        Color c = new Color();
                        c.PackedValue = i;
                        src[index++] = c.ToVector4();
                    }
                    color = null;

                    index = 0;
                    for (int y = 0; y < h; ++y)
                    {
                        for (int x = 0; x < w; ++x)
                        {
                            Vector4 oldPixel = src[index];

                            // Clamp to make sure the distributed errors don't overflow the valid range for colours
                            oldPixel.X = MathHelper.Clamp(oldPixel.X, 0, 1);
                            oldPixel.Y = MathHelper.Clamp(oldPixel.Y, 0, 1);
                            oldPixel.Z = MathHelper.Clamp(oldPixel.Z, 0, 1);
                            oldPixel.W = MathHelper.Clamp(oldPixel.W, 0, 1);

                            T d = new T();
                            d.PackFromVector4(oldPixel);
                            Vector4 newPixel = d.ToVector4();

                            // Distribute the error ahead and below the current pixel
                            //
                            // +------+------+------+
                            // |      |pixel | 7/16 |
                            // +------+------+------+
                            // | 3/16 | 5/16 | 1/16 |
                            // +------+------+------+

                            Vector4 quantError = oldPixel - newPixel;
                            if (x < (w - 1))
                            {
                                src[index + 1] = src[index + 1] + (quantError * (7.0f / 16.0f));
                                if (y < (h - 1))
                                {
                                    src[index + w + 1] = src[index + w + 1] + (quantError * (1.0f / 16.0f));
                                }
                            }
                            if (y < (h - 1))
                            {
                                if (x > 0)
                                {
                                    src[index + w - 1] = src[index + w - 1] + (quantError * (3.0f / 16.0f));
                                }
                                src[index + w] = src[index + w] + (quantError * (5.0f / 16.0f));
                            }

                            if (PremultiplyAlpha)
                            {
                                // Set dest to the pre-multiplied value
                                newPixel.X = newPixel.X * newPixel.W;
                                newPixel.Y = newPixel.Y * newPixel.W;
                                newPixel.Z = newPixel.Z * newPixel.W;
                            }

                            // PackFromVector4 will convert to the destination format
                            d.PackFromVector4(newPixel);
                            dest[index] = d.PackedValue;

                            ++index;
                        }
                    }
                    break;
            }
            PixelBitmapContent<T> result = new PixelBitmapContent<T>(w, h);
            byte[] destData = new byte[w * h * 2];
            Buffer.BlockCopy(dest, 0, destData, 0, destData.Length);
            result.SetPixelData(destData);
            return result;
        }
    }
}