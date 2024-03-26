using System.Text.Json;
using Azure.Messaging.ServiceBus;
using BS.ReceiptAnalyzer.Contract.Config;
using BS.ReceiptAnalyzer.Contract.Requests;
using BS.ReceiptAnalyzer.Contract.Results;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Func.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;
using BS.ReceiptAnalyzer.Contract.Models.ReceiptsRecognition;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Func.Services;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Func
{
    public class ServiceBusTrigger
    {
        private readonly IReceiptRecognizerService _receiptRecognizerService;
        private readonly IResultUploader _resultUploader;
        private readonly ILogger<ServiceBusTrigger> _logger;

        public ServiceBusTrigger(IReceiptRecognizerService receiptRecognizerService,
            IResultUploader resultUploader,
            ILogger<ServiceBusTrigger> logger)
        {
            _receiptRecognizerService = receiptRecognizerService;
            _resultUploader = resultUploader;
            _logger = logger;
        }

        [Function(nameof(ServiceBusTrigger))]
        [ServiceBusOutput(MessagingConfiguration.AnalysisTaskSteps.ResultsQueue, Connection = "ServiceBus")]
        public async Task<AnalysisTaskStepProcessingResult> Run(
            [ServiceBusTrigger(MessagingConfiguration.AnalysisTaskSteps.StartRequestQueue_ReceiptsRecognition, Connection = "ServiceBus")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            var request = DeserializeMessage(message.Body);
            _logger.LogInformation($"Start processing receipt recognition id={request.TaskId}");

            var resultBuilder = StepResultBuilder.Create(request.TaskId);
            try
            {
                var recognitionResult = await _receiptRecognizerService.RecognizeReceiptsAsync(request!.TaskId);
                var contract = MapToContract(recognitionResult);
                await _resultUploader.UploadResultsAsync(request.TaskId, contract);

                resultBuilder.AddRecognitionResult(recognitionResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                resultBuilder.AddException(ex);
            }
            finally
            {
                await messageActions.CompleteMessageAsync(message);
            }

            return resultBuilder.Build();
        }

        private static StartAnalysisTaskStepProcessing DeserializeMessage(BinaryData messageBody)
        {
            var messageJson = messageBody.ToString();
            var message = JsonSerializer.Deserialize<StartAnalysisTaskStepProcessing>(messageJson);

            if (message == null)
                throw new InvalidOperationException("Unreadable message body");

            return message;
        }

        private static ReceiptsRecognitionModel MapToContract(ReceiptRecognizerServiceContract.Result recognitionResult) =>
            new ReceiptsRecognitionModel
            {
                ReceiptsRecognized = recognitionResult.ReceiptsFound,
                Receipts = recognitionResult.Receipts
            };
    }
}
