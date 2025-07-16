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
            SkiaSharp.SKBitmap result = Convolution.ApplyKernels(image, kernelX, kernelY);
            return result;

        }
    }
}