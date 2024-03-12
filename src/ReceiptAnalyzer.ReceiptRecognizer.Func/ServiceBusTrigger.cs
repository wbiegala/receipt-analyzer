using System.Text.Json;
using Azure.Messaging.ServiceBus;
using BS.ReceiptAnalyzer.Contract.Config;
using BS.ReceiptAnalyzer.Contract.Models;
using BS.ReceiptAnalyzer.Contract.Requests;
using BS.ReceiptAnalyzer.Contract.Results;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ReceiptAnalyzer.ReceiptRecognizer.Core;

namespace ReceiptAnalyzer.ReceiptRecognizer.Func
{
    public class ServiceBusTrigger
    {
        private readonly IReceiptRecognizerService _receiptRecognizerService;
        private readonly ILogger<ServiceBusTrigger> _logger;

        public ServiceBusTrigger(IReceiptRecognizerService receiptRecognizerService, ILogger<ServiceBusTrigger> logger)
        {
            _receiptRecognizerService = receiptRecognizerService;
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

            var result = await _receiptRecognizerService.RecognizeReceiptsAsync(request!.TaskId);


            await messageActions.CompleteMessageAsync(message);
            return MapResult(result);
        }

        private static StartAnalysisTaskStepProcessing? DeserializeMessage(BinaryData messageBody)
        {
            var messageJson = messageBody.ToString();
            return JsonSerializer.Deserialize<StartAnalysisTaskStepProcessing>(messageJson);
        }

        private static AnalysisTaskStepProcessingResult MapResult(ReceiptRecognizerServiceContract.Result result) =>
            new AnalysisTaskStepProcessingResult
            {
                TaskId = result.TaskId,
                StartTime = result.StartTime,
                EndTime = result.EndTime,
                IsSuccess = result.IsSuccess,
                FailReason = result.FailReason,
                AnalysisTaskStep = AnalysisTaskSteps.ReceiptsRecognition
            };
    }
}
