using BS.ReceiptAnalyzer.Contract.Models;
using BS.ReceiptAnalyzer.Contract.Results;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Func.Extensions
{
    internal class StepResultBuilder
    {
        public static StepResultBuilder Create(Guid taskId)
        {
            return new StepResultBuilder(taskId, DateTimeOffset.Now);
        }

        public void AddRecognitionResult(ReceiptRecognizerServiceContract.Result result)
        {
            _endTime = result.FinishedAt;
            _isSuccess = result.IsSuccess;
            _errorMessage = result.FailReason;
        }

        public void AddException(Exception exception)
        {
            _endTime = DateTimeOffset.Now;
            _isSuccess = false;
            _errorMessage = exception.Message;
        }

        public AnalysisTaskStepProcessingResult Build()
        {
            return new AnalysisTaskStepProcessingResult
            {
                TaskId = _taskId,
                AnalysisTaskStep = STEP_ID,
                StartTime = _startTime,
                EndTime = _endTime ?? DateTimeOffset.Now,
                IsSuccess = _isSuccess ?? false,
                FailReason = _errorMessage
            };
        }

        private const int STEP_ID = AnalysisTaskSteps.ReceiptsRecognition;
        private Guid _taskId;
        private DateTimeOffset _startTime;
        private DateTimeOffset? _endTime;
        private bool? _isSuccess;
        private string? _errorMessage;

        private StepResultBuilder(Guid taskId, DateTimeOffset startTime)
        {
            _taskId = taskId;
            _startTime = startTime;
        }
    }
}
