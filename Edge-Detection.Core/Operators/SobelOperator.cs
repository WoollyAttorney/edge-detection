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
            SkiaSharp.SKBitmap result = Convolution.ApplyKernels(image, kernelX, kernelY);
            return result;
        }
    }
}