namespace Edge_Detection.Core
{
    public class PrewittOperator : BaseOperator
    {
        readonly static float[,] kernelX = new float[,]
        {
            { 1, 0, -1 },
            { 1, 0, -1 },
            { 1, 0, -1 }
        };
        readonly static float[,] kernelY = new float[,]
        {
            { 1, 1, 1 },
            { 0, 0, 0 },
            { -1, -1, -1 }
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