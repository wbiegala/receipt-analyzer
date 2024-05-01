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
        private readonly string _outputDirectory;
        private readonly ISourceImagePathStrategy _sourceImagePathStrategy;
        private readonly IReceiptRecognitionImagePathStrategy _resultImagePathStrategy;
        private readonly IStorageService _storageService;

        public ReceiptRecognitionProcessor(IReceiptRecognizerService recognizer,
            ISourceImagePathStrategy sourceImagePathStrategy,
            IReceiptRecognitionImagePathStrategy resultImagePathStrategy,
            IStorageService storageService, ProgramArgs config)
        {
            _recognizer = recognizer
                ?? throw new ArgumentNullException(nameof(recognizer));
            _sourceImagePathStrategy = sourceImagePathStrategy
                ?? throw new ArgumentNullException(nameof(sourceImagePathStrategy));
            _resultImagePathStrategy = resultImagePathStrategy
                ?? throw new ArgumentNullException(nameof(resultImagePathStrategy));
            _storageService = storageService
                ?? throw new ArgumentNullException(nameof(storageService));
            _inputDirectory = config.InputDirectory;
            _outputDirectory = config.OutputDirectory;
        }

        public async Task ProcessAsync()
        {
            var files = GetFiles();

            Console.WriteLine($"Start processing of {files.Count()} files");

            foreach (var file in files)
            {
                var task = await PrepareTaskAsync(file);
                var result = await _recognizer.RecognizeReceiptsAsync(task);
                await MoveResultsAsync(task, result.Receipts);
            }

            Console.WriteLine("Finished processing files, deleting temprary data");

            await Task.Delay(1000);

            try
            {
                Directory.Delete($"{_outputDirectory}\\temp", true);
            }
            catch
            {
                Console.WriteLine($"Something went wrong while on temp data delete, you have to do it manually from output directory!");
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
                await _storageService.SaveFileAsync(pngFile, _sourceImagePathStrategy.GetSourceImagePath(taskId));
            }      
            
            File.Delete(file);
            
            return taskId;
        }

        private Task MoveResultsAsync(Guid taskId, IEnumerable<Guid> receipts)
        {
            foreach (var receipt in receipts)
            {
                var sourcePath = _resultImagePathStrategy.GetReceiptImagePath(taskId, receipt);
                var fileFormat = Path.GetExtension(sourcePath);
                var outputPath = $"{_outputDirectory}\\{receipt.ToString("N")}{fileFormat}";
                File.Move($"{_outputDirectory}\\temp\\{sourcePath}", outputPath);
            }

            return Task.CompletedTask;
        }
    }
}
