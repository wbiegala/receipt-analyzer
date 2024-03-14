using BS.ReceiptAnalyzer.Shared.Extensions;

namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem
{
    internal sealed class FileSytemPathPolicy :
        ITaskRootDirectoryPathStrategy,
        ITaskReceiptDirectoryPathStrategy,
        ISourceImagePathStrategy,
        IReceiptRecognitionImagePathStrategy,
        IReceiptRecognitionResultPathStrategy,
        IReceiptPartitioningImagePathStrategy,
        IReceiptPartitioningResultPathStrategy,
        IOcrResultPathStrategy,
        ITaskResultPathStrategy
    {
        public string GetTaskDirectoryPath(Guid taskId)
        {
            return TransformGuid(taskId);
        }
        public string GetReceiptDirectoryPath(Guid taskId, Guid receiptId)
        {
            return $"{TransformGuid(taskId)}/{TransformGuid(receiptId)}";
        }

        public string GetSourceImagePath(Guid taskId, string extension = IMAGE_FILE_FORMAT)
        {
            return $"{GetTaskDirectoryPath(taskId)}/source.{extension}";
        }

        public string GetReceiptImagePath(Guid taskId, Guid receiptId)
        {
            return $"{GetReceiptDirectoryPath(taskId, receiptId)}/receipt-source.{IMAGE_FILE_FORMAT}";
        }

        public string GetReceiptRecognitionResultFilePath(Guid taskId)
        {
            return $"{GetTaskDirectoryPath(taskId)}/recognition-result.json";
        }

        public string GetReceiptPartPath(Guid taskId, Guid receiptId, string partType, int partNumber)
        {
            partType = partType.ToLower().RemoveSpecialCharacters();
            var partId = BuildNumber(partNumber);

            return $"{GetReceiptDirectoryPath(taskId, receiptId)}/{partType}_{partId}.{IMAGE_FILE_FORMAT}";
        }

        public string GetReceiptPartitioningResultPath(Guid taskId, Guid receiptId)
        {
            return $"{GetReceiptDirectoryPath(taskId, receiptId)}/partitioning-result.json";
        }

        public string GetOcrResultPath(Guid taskId, Guid receiptId)
        {
            return $"{GetReceiptDirectoryPath(taskId, receiptId)}/ocr-result.json";
        }

        public string GetTaskResultPath(Guid taskId)
        {
            return $"{GetTaskDirectoryPath(taskId)}/final-result.json";
        }

        private static string BuildNumber(int number)
        {
            if (number >= 1000)
                throw new ArgumentException("To many parts");

            var result = number.ToString();

            return result.Length switch
            {
                1 => result.PadLeft(2, '0'),
                2 => result.PadLeft(1, '0'),
                _ => result
            };
        }
        private static string TransformGuid(Guid guid) => guid.ToString("N");
        private const string IMAGE_FILE_FORMAT = "png";
    }
}
