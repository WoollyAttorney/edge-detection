using Edge_Detection.Core;
using SkiaSharp;

namespace Edge_Detection.Tests
{
    public class TestSelections
    {
        [Fact]
        public void TestOperatorSelection()
        {
            // The testing idea here is to create a blank white image with a single black dot in the center,
            // and then test if selection of Sobel and Prewitt operators works correctly.
            // For the N-E-S-W neighbours, the pixel values for Sobel should be twice that of Prewitt. 
            Console.WriteLine("Testing Sobel and Prewitt Operators selection and execution");
            SKBitmap image = Create5By5DotBitmap();

            DetectionOperator option = DetectionOperator.Sobel;
            SKBitmap sobelResult = Detector.DetectEdges(image, option);
            Assert.NotNull(sobelResult);
            Assert.Equal(image.Width, sobelResult.Width);
            Assert.Equal(image.Height, sobelResult.Height);
            Assert.Equal(sobelResult.GetPixel(1, 2).Red, sobelResult.GetPixel(3, 2).Red);
            Assert.Equal(sobelResult.GetPixel(2, 1).Red, sobelResult.GetPixel(2, 3).Red);
            Assert.Equal(sobelResult.GetPixel(1, 2).Red, sobelResult.GetPixel(2, 1).Red);
            Assert.Equal(120, (int)sobelResult.GetPixel(2, 1).Red);
            Assert.Equal(0, (int)sobelResult.GetPixel(2, 2).Red);

            // Test Prewitt Operator
            option = DetectionOperator.Prewitt;
            SKBitmap prewittResult = Detector.DetectEdges(image, option);
            Assert.NotNull(prewittResult);
            Assert.Equal(image.Width, prewittResult.Width);
            Assert.Equal(image.Height, prewittResult.Height);
            Assert.Equal(prewittResult.GetPixel(1, 2).Red, prewittResult.GetPixel(3, 2).Red);
            Assert.Equal(prewittResult.GetPixel(2, 1).Red, prewittResult.GetPixel(2, 3).Red);
            Assert.Equal(prewittResult.GetPixel(1, 2).Red, prewittResult.GetPixel(2, 1).Red);
            Assert.Equal(85, (int)prewittResult.GetPixel(2, 1).Red);
            Assert.Equal(0, (int)prewittResult.GetPixel(2, 2).Red);
        }

        private static SKBitmap Create5By5DotBitmap()
        {
            SKBitmap bitmap = new(5, 5, SKColorType.Gray8, SKAlphaType.Opaque);
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.White);
                canvas.DrawPoint(new SKPoint(2, 2), new SKPaint { Color = SKColors.Black });
            }
            return bitmap;
        }
    }
}