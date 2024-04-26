using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;
using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using BS.ReceiptAnalyzer.Shared.Storage.Images;

namespace BS.ReceiptAnalyzer.Tool.ReceiptRecognition
{
    internal class ReceiptRecognitionProcessor : IImagesProcessor
    {
        private readonly IReceiptRecognizerService _recognizer;
        private readonly string _inputDirectory;
        private readonly ISourceImagePathStrategy _sourceImagePathStrategy;
        private readonly IStorageService _storageService;

        public ReceiptRecognitionProcessor(IReceiptRecognizerService recognizer,
            ISourceImagePathStrategy sourceImagePathStrategy,
            IStorageService storageService, ProgramArgs config)
        {
            _recognizer = recognizer
                ?? throw new ArgumentNullException(nameof(recognizer));
            _sourceImagePathStrategy = sourceImagePathStrategy
                ?? throw new ArgumentNullException(nameof(sourceImagePathStrategy));
            _storageService = storageService
                ?? throw new ArgumentNullException(nameof(storageService));
            _inputDirectory = config.InputDirectory;
        }

        public async Task ProcessAsync()
        {
            var files = GetFiles();

            foreach (var file in files)
            {
                var task = await PrepareTaskAsync(file);
                var result = await _recognizer.RecognizeReceiptsAsync(task);
            }
        }

        private IEnumerable<string> GetFiles()
        {
            var result = new List<string>();
            result.AddRange(Directory.GetFiles(_inputDirectory, "*.png"));
            result.AddRange(Directory.GetFiles(_inputDirectory, "*.jpg"));
            result.AddRange(Directory.GetFiles(_inputDirectory, "*.jpeg"));

            return result;
        }

        private async Task<Guid> PrepareTaskAsync(string file)
        {
            var taskId = Guid.NewGuid();
            using (var fileStream = File.OpenRead(file))
            {
                var bytes = FileHelper.GetBytes(fileStream);
                var pngFile = ImageFormatHelper.ConvertToPng(ImageFormatHelper.GetMimeFileFormant(file), bytes);
                await _storageService.SaveFileAsync(pngFile,
                    _sourceImagePathStrategy.GetSourceImagePath(taskId));
            }
            
            File.Delete(file);
            
            return taskId;
        }
    }
}
