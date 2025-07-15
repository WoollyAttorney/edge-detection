namespace Edge_Detection.Core
{
    public class SobelOperator : BaseOperator
    {
        readonly static float[,] kernelX = new float[,]
        {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };
        readonly static float[,] kernelY = new float[,]
        {
            { -1, -2, -1 },
            { 0, 0, 0 },
            { 1, 2, 1 }
        };
        public override SkiaSharp.SKBitmap DetectEdge(SkiaSharp.SKBitmap image)
        {
            SkiaSharp.SKBitmap resultX = Convolution.ApplyKernel(image, kernelX);
            SkiaSharp.SKBitmap resultY = Convolution.ApplyKernel(image, kernelY);
            SkiaSharp.SKBitmap result = Convolution.CombineResults(resultX, resultY);
            return result;
        }
    }
}