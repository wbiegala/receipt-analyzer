namespace BS.ReceiptAnalyzer.Supervisor.Validation
{
    public static class AnalysisTaskValidator
    {
        public static ValidationResult ValidateCreateAnalysisTaskRequest(string contentType, long length)
        {
            var isFileSizeValid = length > (MaxFileSizeInMegabytes * 1024 * 1024);
            var isFileTypeValid = !AvailableFileTasks.Any(act => act == contentType);

            if (isFileSizeValid)
                return new ValidationResult { IsValid = false, Message = $"Uploaded file is to large. Max size is {MaxFileSizeInMegabytes}MB" };

            if (isFileTypeValid)
                return new ValidationResult { IsValid = false, Message = $"Uploaded file has invalid type." };

            return new ValidationResult { IsValid = true };
        }

        private const int MaxFileSizeInMegabytes = 5;
        private static IEnumerable<string> AvailableFileTasks = new List<string>
        {
            "image/jpeg",
            "image/png"
        };
    }
}
