using Edge_Detection.Core;
using SkiaSharp;

namespace Edge_Detection.Tests
{
    public class TestOperations
    {
        [Fact]
        public void TestConvolution()
        {
            Console.WriteLine("Testing Convolution with a simple kernel that should not change the image");
            SKBitmap sampleInput = Create5By5PlusBitmap();
            // Simple kernel which returns the center pixel, so no transformation
            float[,] kernel = new float[,]
            {
                { 0, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 0 },
            };
            SKBitmap result = Convolution.ApplyKernels(sampleInput, kernel, kernel);
            Assert.NotNull(result);
            Assert.Equal(sampleInput.Width, result.Width);
            Assert.Equal(sampleInput.Height, result.Height);
            int error = 0;
            for (int y = 0; y < sampleInput.Height; y++)
            {
                for (int x = 0; x < sampleInput.Width; x++)
                {
                    SKColor expectedColor = sampleInput.GetPixel(x, y);
                    SKColor actualColor = result.GetPixel(x, y);
                    error += Math.Abs(expectedColor.Red - actualColor.Red);
                    error += Math.Abs(expectedColor.Green - actualColor.Green);
                    error += Math.Abs(expectedColor.Blue - actualColor.Blue);
                }
            }
            // Check if edges are detected correctly
            Assert.Equal(0, error);
        }

        private static SKBitmap Create5By5PlusBitmap()
        {
            SKBitmap bitmap = new(5, 5, SKColorType.Gray8, SKAlphaType.Opaque);
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.White);
                canvas.DrawLine(new SKPoint(1, 2), new SKPoint(4, 2), new SKPaint { Color = SKColors.Black });
                canvas.DrawLine(new SKPoint(2, 1), new SKPoint(2, 4), new SKPaint { Color = SKColors.Black });
            }
            return bitmap;
        }
    }
}