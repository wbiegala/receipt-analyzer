using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.Model;
using Microsoft.ML.Data;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core.ML
{
    internal class ReceiptRecognitionService : IReceiptRecognitionService
    {
        public async Task<IEnumerable<ReceiptRecognized>> FindReceiptsOnImageAsync(Stream source)
        {
            var image = MLImage.CreateFromStream(source);
            var recognitionResult = await PredictAsync(image);

            return MapToResult(recognitionResult, image.Width, image.Height);
        }


        private Task<ReceiptRecognitionML.ModelOutput> PredictAsync(MLImage image)
        {
            return Task.Run(() =>
            {
                var sample = new ReceiptRecognitionML.ModelInput
                {
                    ImageSource = image,
                };
                var result = ReceiptRecognitionML.Predict(sample);

                return result;
            });
        }

        private static List<ReceiptRecognized> MapToResult(
            ReceiptRecognitionML.ModelOutput output,
            int imageWidth,
            int imageHeight)
        {
            var result = new List<ReceiptRecognized>();

            foreach (var boundingBox in output.BoundingBoxes)
            {
                var top = imageHeight * boundingBox.Top / DefaultHeight;
                var bottom = imageHeight * boundingBox.Bottom / DefaultHeight;
                var left = imageWidth * boundingBox.Left / DefaultWidth;
                var right = imageWidth * boundingBox.Right / DefaultWidth;

                var receipt = new ReceiptRecognized((int)left, (int)top, (int)right, (int)bottom, boundingBox.Score);

                result.Add(receipt);
            }

            return result;
        }

        private const int DefaultHeight = 600;
        private const int DefaultWidth = 800;
    }
}
