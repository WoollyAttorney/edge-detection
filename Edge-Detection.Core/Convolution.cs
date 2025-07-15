namespace Edge_Detection.Core
{
    public class Convolution
    {
        public static SkiaSharp.SKBitmap ApplyKernel(SkiaSharp.SKBitmap image, float[,] kernel)
        {
            int image_width = image.Width;
            int image_height = image.Height;
            if (image.ColorType != SkiaSharp.SKColorType.Gray8)
            {
                throw new System.ArgumentException("Image must be in grayscale format.");
            }

            SkiaSharp.SKBitmap result = new(image_width, image_height, SkiaSharp.SKColorType.Gray8, SkiaSharp.SKAlphaType.Opaque);
            
            for (int y = 1; y < image_height - 1; y++)
            {
                for (int x = 1; x < image_width - 1; x++)
                {
                    float convolutionSum = 0;
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            SkiaSharp.SKColor pixelColor = image.GetPixel(x + kx, y + ky);
                            float grayValue = pixelColor.Red;
                            convolutionSum += grayValue * kernel[ky + 1, kx + 1];
                        }
                    }
                    uint newValue = (uint)Math.Clamp(convolutionSum, 0, 255);
                    result.SetPixel(x, y, new SkiaSharp.SKColor(newValue));
                }
            }
            return result;

        }

        public static SkiaSharp.SKBitmap CombineResults(SkiaSharp.SKBitmap image1, SkiaSharp.SKBitmap image2)
        {
            if (image1.ColorType != SkiaSharp.SKColorType.Gray8 || image2.ColorType != SkiaSharp.SKColorType.Gray8)
            {
                throw new System.ArgumentException("Images must be in grayscale to combine results");
            }

            SkiaSharp.SKBitmap result = new(image1.Width, image1.Height, SkiaSharp.SKColorType.Gray8, SkiaSharp.SKAlphaType.Opaque);

            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    int image1Pixel = image1.GetPixel(x, y).Red;
                    int image2Pixel = image2.GetPixel(x, y).Red;
                    uint pixelResult = (uint)Math.Clamp(Math.Sqrt((image1Pixel * image1Pixel) + (image2Pixel * image2Pixel)), 0, 255);
                    result.SetPixel(x, y, new SkiaSharp.SKColor(pixelResult));
                }
            }
            return result;
        }
    }
}