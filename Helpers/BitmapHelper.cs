using System;
using System.Diagnostics;
using System.IO;
using Avalonia.Media.Imaging;
using SkiaSharp;

namespace Graven.Hearts.MakeGLTF.Helpers
{
    public static class BitmapHelpers
    {
        public static WriteableBitmap SolidColourBitmap(int red, int green, int blue)
        {
            var bitmap = new SKBitmap(new SKImageInfo(1, 1, SKColorType.Rgba8888, SKAlphaType.Opaque));
            bitmap.SetPixel(0, 0, new SKColor((byte)red, (byte)green, (byte)blue, 255));
            return bitmap.FromSKBitmap();
        }

        public static WriteableBitmap CombineChannels(WriteableBitmap red,
            WriteableBitmap green,
            WriteableBitmap blue,
            WriteableBitmap alpha,
            int width,
            int height)
        {
            if (red is null || green is null || blue is null)
                return default!;

            var merged = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

            var skRed = red.FromWriteableBitmap();
            var skGreen = green.FromWriteableBitmap();
            var skBlue = blue.FromWriteableBitmap();
            var skAlpha = alpha?.FromWriteableBitmap();

            for (int posY = 0; posY < height; ++posY)
            {
                for (int posX = 0; posX < width; ++posX)
                {
                    SetMergedPixel(merged, red, green, blue, alpha!, skRed, skGreen, skBlue, skAlpha!, posX, posY);
                }
            }

            return merged.FromSKBitmap();
        }

        private static void SetMergedPixel(SKBitmap merged,
            WriteableBitmap red, WriteableBitmap green,
            WriteableBitmap blue, WriteableBitmap alpha,
            SKBitmap skRed, SKBitmap skGreen,
            SKBitmap skBlue, SKBitmap skAlpha,
            int posX, int posY)
        {
            var redIntensity = skRed.GetPixel((int)(posX % red.Size.Width), (int)(posY % red.Size.Height)).Red;
            var greenIntensity = skGreen.GetPixel((int)(posX % green.Size.Width), (int)(posY % green.Size.Height)).Green;
            var blueIntensity = skBlue.GetPixel((int)(posX % blue.Size.Width), (int)(posY % blue.Size.Height)).Blue;
            var alphaIntensity = alpha is not null ? skAlpha.GetPixel((int)(posX % alpha.Size.Width), (int)(posY % alpha.Size.Height)).Red : default;

            merged.SetPixel(posX, posY, new SKColor(redIntensity, greenIntensity, blueIntensity, alpha is not null ? alphaIntensity : (byte)255));
        }

        public static SKBitmap FromWriteableBitmap(this WriteableBitmap bitmap)
        {
            using var stream = new MemoryStream();
            bitmap.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return SKBitmap.Decode(stream, new SKImageInfo((int)bitmap.Size.Width, (int)bitmap.Size.Height, SKColorType.Rgba8888));
        }

        public static WriteableBitmap FromSKBitmap(this SKBitmap bitmap)
        {
            return WriteableBitmap.Decode(bitmap.Encode(SKEncodedImageFormat.Png, 100).AsStream());
        }
    }
}
