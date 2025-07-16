using Edge_Detection.Core;
using SkiaSharp;

namespace Edge_Detection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string imagePath = GetInput("Enter path/to/image: ");
            if (string.IsNullOrEmpty(imagePath))
            {
                Console.WriteLine("No input provided. Exiting program.");
                return;
            }
            if (!IsValidImagePath(imagePath))
            {
                Console.WriteLine("Ivalid Image path, the extensions accepted are [.jpeg, .jpg, and .png]");
                return;
            }

            string operatorString = GetInput("Enter operator name [Sobel, Prewitt]: ");
            if (string.IsNullOrEmpty(operatorString))
            {
                Console.WriteLine("No input provided. Exiting program.");
                return;
            }

            string output = GetInput("Enter output/path/to/image ");
            if (string.IsNullOrEmpty(output))
            {
                Console.WriteLine("No input provided. Storing the image as output.png");
                output = "output.png";
            }
            if (!IsValidImagePath(output))
            {
                Console.WriteLine("Invalid output path provided");
                return;
            }

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

            ImageHandler image = new();
            try
            {
                image.LoadImage(imagePath);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid Image path provided");
                return;

            }
            try
            {
                SKBitmap outputImage = Detector.DetectEdges(image.GetValidBitmap(), detectionOperator);
                using var outputStream = File.OpenWrite(output);
                outputImage.Encode(SKEncodedImageFormat.Png, 100).SaveTo(outputStream);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Could not load or convert image");
            }
        }

        private static string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No Input provided...");
                input = "";
            }
            return input;
        }

        private static bool IsValidImagePath(string inputString)
        {
            string[] parts = inputString.Split('.');
            if (parts.Length > 1)
            {
                string lastExtension = parts[^1];
                if (string.Equals(lastExtension, "png") || string.Equals(lastExtension, "jpeg") || string.Equals(lastExtension, "jpg"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
