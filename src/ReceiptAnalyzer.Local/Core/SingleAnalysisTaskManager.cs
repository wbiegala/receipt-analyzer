using BS.ReceiptAnalyzer.Domain.Model;
using BS.ReceiptAnalyzer.Local.Core.Storage;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;
using Contract = BS.ReceiptAnalyzer.Local.Core.TaskManagerContract;

namespace BS.ReceiptAnalyzer.Local.Core
{
    internal class SingleAnalysisTaskManager : IAnalysisTaskManager
    {
        private readonly IStorageFacade _storage;
        private readonly IReceiptRecognizerService _receiptRecognizerService;
        private AnalysisTask? _current;

        public SingleAnalysisTaskManager(IStorageFacade storage,
            IReceiptRecognizerService receiptRecognizerService)
        {
            _storage = storage;
            _receiptRecognizerService = receiptRecognizerService;
        }

        public async Task<Contract.CreateTask.Result> CreateTask(Contract.CreateTask.Request request)
        {
            var taskId = Guid.NewGuid();
            var saveResult = await _storage.SaveSourceImage(request.FilePath, taskId);
            if (saveResult.Item1 != true)
                return new Contract.CreateTask.Result { Success = false, Error = saveResult.Item2 };

            var task = AnalysisTask.Create("ignore");
            GenerateTaskId(task, taskId);

            _current = task;
            _current.Start();

            return new Contract.CreateTask.Result { Success = true, TaskId = taskId };
        }

        public async Task<Contract.ExecuteNextStep.Result> ExecuteNextStep(Contract.ExecuteNextStep.Request request)
        {
            if (_current == null || _current.Status != AnalysisTaskStatus.OnProcessing)
                return new Contract.ExecuteNextStep.Result
                { 
                    Success = false,
                    Error = "Nie można wykonać kolejnego kroku"
                };

            switch (_current.Progression)
            {
                case AnalysisTaskProgression.Scheduled:
                    return await ExecuteReceiptRecognition();
                case AnalysisTaskProgression.ReceiptsRecognition:
                    return await ExecuteReceiptsPartitioning();
                case AnalysisTaskProgression.ReceiptsPartitioning:
                    return await ExecuteReceiptsConvertion();
                case AnalysisTaskProgression.ReceiptsConvertion:
                    return await ExecuteReceiptsDataMatching();
                default:
                    return new Contract.ExecuteNextStep.Result 
                    { 
                        Success = false,
                        Error = "Nie można wykonać kolejnego kroku"
                    };
            }
        }

        public string GetSourceImagePath(Guid taskId)
        {
            return _storage.GetSourceImagePath(taskId);
        }

        private void GenerateTaskId(AnalysisTask task, Guid id)
        {
            var taskType = task.GetType();
            var property = taskType.GetProperty(nameof(AnalysisTask.Id));
            property!.SetValue(task, id);
        }

        private async Task<Contract.ExecuteNextStep.Result> ExecuteReceiptRecognition()
        {
            var stepStarted = DateTimeOffset.Now;
            var recognitionResult = await _receiptRecognizerService.RecognizeReceiptsAsync(_current!.Id);
            var analysisStep = AnalysisTaskStep.Create(
                AnalysisTaskProgression.ReceiptsRecognition,
                recognitionResult.IsSuccess,
                DateTimeOffset.Now,
                stepStarted,
                recognitionResult.FinishedAt);
            _current.NotifyStepFinished(analysisStep);

            if (!recognitionResult.IsSuccess)
            {
                _current.Fail(recognitionResult.FailReason!);
            }

            return new Contract.ExecuteNextStep.Result
            {
                TaskId = _current!.Id,
                Success = recognitionResult.IsSuccess,
                CanContinue = _current.Status == AnalysisTaskStatus.OnProcessing,
                Error = recognitionResult.FailReason,
                CompletionPercentage = (int)AnalysisTaskProgression.ReceiptsRecognition,
                StepName = StepNames.ReceiptsRecognition,

                //TODO: mapowanie wyniku
            };
        }

        private async Task<Contract.ExecuteNextStep.Result> ExecuteReceiptsPartitioning()
        {
            throw new NotImplementedException();
        }

        private async Task<Contract.ExecuteNextStep.Result> ExecuteReceiptsConvertion()
        {
            throw new NotImplementedException();
        }

        private async Task<Contract.ExecuteNextStep.Result> ExecuteReceiptsDataMatching()
        {
            throw new NotImplementedException();
        }

        private static class StepNames
        {
            public const string ReceiptsRecognition = "Rozpoznawanie paragonów";
            public const string ReceiptsPartitioning = "Analiza elementów paragonów";
            public const string ReceiptsConvertion = "Konwersja obrazu na teskt";
        }
    }
}
