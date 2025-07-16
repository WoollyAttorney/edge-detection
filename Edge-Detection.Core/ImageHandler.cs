using SkiaSharp;

namespace Edge_Detection.Core
{
    public class ImageHandler
    {
        private SKBitmap? bitmap;
        private static readonly double[] colorWeights = [1, 1, 1];

        public void LoadImage(string imagePath)
        {
            using var rawData = File.OpenRead(imagePath);
            using var inputStream = new SKManagedStream(rawData);
            bitmap = SKBitmap.Decode(inputStream);
            if (bitmap == null)
            {
                throw new ArgumentException("Error opening image file");
            }
        }
        public SKBitmap GetValidBitmap()
        {
            if (bitmap == null)
            {
                throw new InvalidOperationException("No image loaded.");
            }
            if (bitmap.ColorType != SKColorType.Gray8)
                return GetGrayscaleBitmap(bitmap);
            else
                return bitmap;
        }

        protected static SKBitmap GetGrayscaleBitmap(SKBitmap bitmap)
        {
            SKBitmap grayscale = new(bitmap.Width, bitmap.Height, SKColorType.Gray8, SKAlphaType.Opaque);
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    SKColor rgbPixel = bitmap.GetPixel(x, y);
                    uint gray = (uint)Math.Clamp(((int)rgbPixel.Red * colorWeights[0] + (int)rgbPixel.Green * colorWeights[1] + (int)rgbPixel.Blue * colorWeights[2]) / (colorWeights[0] + colorWeights[1] + colorWeights[2]), 0, 255);
                    SKColor grayPixel = new(gray);
                    grayscale.SetPixel(x, y, grayPixel);
                }
            }
            return grayscale;
        }
    }
}