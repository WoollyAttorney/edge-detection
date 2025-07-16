namespace Edge_Detection.Core
{
    public class Convolution
    {
        public static SkiaSharp.SKBitmap ApplyKernels(SkiaSharp.SKBitmap image, float[,] kernelX, float[,] kernelY)
        {
            int image_width = image.Width;
            int image_height = image.Height;
            if (image.ColorType != SkiaSharp.SKColorType.Gray8)
            {
                throw new ArgumentException("Image must be in grayscale format.");
            }
            if (kernelX.GetLength(0) != kernelY.GetLength(0) || kernelX.GetLength(1) != kernelY.GetLength(1))
            {
                throw new ArgumentException("Both kernels must be same size.");
            }

            SkiaSharp.SKBitmap result = new(image_width, image_height, SkiaSharp.SKColorType.Gray8, SkiaSharp.SKAlphaType.Opaque);
            int[] tempImage = new int[image_width * image_height];
            int maxValue = 0;
            int minValue = 255;

            for (int y = 1; y < image_height - 1; y++)
            {
                for (int x = 1; x < image_width - 1; x++)
                {
                    float convolutionSumX = 0;
                    float convolutionSumY = 0;
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            SkiaSharp.SKColor pixelColor = image.GetPixel(x + kx, y + ky);
                            float grayValue = pixelColor.Red;
                            convolutionSumX += grayValue * kernelX[ky + 1, kx + 1];
                            convolutionSumY += grayValue * kernelY[ky + 1, kx + 1];
                        }
                    }
                    int newValue = (int)Math.Sqrt((convolutionSumX * convolutionSumX) + (convolutionSumY * convolutionSumY));
                    tempImage[y * image_width + x] = newValue;
                    maxValue = Math.Max(maxValue, newValue);
                    minValue = Math.Min(minValue, newValue);
                }
            }

            for (int i = 0; i < tempImage.Length; i++)
            {
                byte normalizedValue = (byte)Math.Clamp(((tempImage[i] - minValue) / (float)(maxValue - minValue)) * 255, 0, 255);
                result.SetPixel(i % image_width, i / image_width, new SkiaSharp.SKColor(normalizedValue, normalizedValue, normalizedValue));
            }

            return result;
        }
    }
}