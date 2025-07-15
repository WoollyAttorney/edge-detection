using Edge_Detection.Core;
using SkiaSharp;

namespace Edge_Detectionls
{
    public class EdgeDetectorProgram
    {

        public static void Main(string[] args)
        {
            string? input;

            Console.WriteLine("Enter path/to/image: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input provided. Exiting program.");
                return;
            }
            string imagePath = input;

            Console.WriteLine("Enter operator name [Sobel, Prewitt]: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input provided. Exiting program.");
                return;
            }
            string operatorString = input;

            Console.WriteLine("Enter output/path/to/image ");
            input = Console.ReadLine();
            string output;
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input provided. Storing the image as output.png");
                output = "output.png";
            }
            else
                output = input;

            DetectionOperator detectionOperator;
            if (operatorString.Equals("Sobel", StringComparison.OrdinalIgnoreCase))
            {
                detectionOperator = DetectionOperator.Sobel;
            }
            else if (operatorString.Equals("Prewitt", StringComparison.OrdinalIgnoreCase))
            {
                detectionOperator = DetectionOperator.Prewitt;
            }
            else
            {
                Console.WriteLine("Invalid operator name. Exiting program.");
                return;
            }

            using var rawData = File.OpenRead(imagePath);
            using var inputStream = new SKManagedStream(rawData);
            using var image = SKBitmap.Decode(inputStream);
            if (image == null)
            {
                Console.WriteLine("Error opening image file");
                return;
            }
            SKBitmap outputImage = Detector.DetectEdges(image, detectionOperator);
            using (var outputStream = File.OpenWrite(output))
            {
                outputImage.Encode(SKEncodedImageFormat.Png, 100).SaveTo(outputStream);
            }
        }
    }
}
