namespace Edge_Detection.Core
{
    public class Detector
    {
        public static SkiaSharp.SKBitmap DetectEdges(SkiaSharp.SKBitmap image, DetectionOperator option)
        {
            if (image.ColorType != SkiaSharp.SKColorType.Gray8)
            {
                throw new System.ArgumentException("Invalid Color Type of image, currently only Grayscale is supported");
            }
            SkiaSharp.SKBitmap result;
            switch (option)
            {
                case DetectionOperator.Sobel:
                    result = new SobelOperator().DetectEdge(image);
                    break;

                case DetectionOperator.Prewitt:
                    result = new PrewittOperator().DetectEdge(image);
                    break;

                default:
                    Console.WriteLine("Invalid Operator Selected");
                    result = image;
                    break;
            }
            return result;
        }
    }
}