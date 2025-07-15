namespace Edge_Detection.Core
{
    public abstract class BaseOperator
    {
        public abstract SkiaSharp.SKBitmap DetectEdge(SkiaSharp.SKBitmap image);
    }
}
