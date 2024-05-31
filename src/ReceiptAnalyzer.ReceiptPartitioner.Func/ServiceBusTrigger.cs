using System.Text.Json;
using Azure.Messaging.ServiceBus;
using BS.ReceiptAnalyzer.Contract.Config;
using BS.ReceiptAnalyzer.Contract.Requests;
using BS.ReceiptAnalyzer.Contract.Results;
using BS.ReceiptAnalyzer.ReceiptPartitioner.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.ReceiptPartitioner.Func
{
    public class ServiceBusTrigger
    {
        private readonly IReceiptPartitionerService _receiptPartitionerService;
        private readonly ILogger<ServiceBusTrigger> _logger;

        public ServiceBusTrigger(IReceiptPartitionerService receiptPartitionerService, ILogger<ServiceBusTrigger> logger)
        {
            _receiptPartitionerService = receiptPartitionerService;
            _logger = logger;
        }

        [Function(nameof(ServiceBusTrigger))]
        [ServiceBusOutput(MessagingConfiguration.AnalysisTaskSteps.ResultsQueue, Connection = "ServiceBus")]
        public async Task<AnalysisTaskStepProcessingResult> Run(
            [ServiceBusTrigger(MessagingConfiguration.AnalysisTaskSteps.StartRequestQueue_ReceiptsPartitioning, Connection = "ServiceBus")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            var request = DeserializeMessage(message.Body);
            _logger.LogInformation($"Start processing receipt partitioning id={request.TaskId}");

            try
            {
                var result = await _receiptPartitionerService.PartitionReceiptsAsync(request.TaskId!);
                //TODO: apend to result
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //TODO: apend to result
            }
            finally
            {
                await messageActions.CompleteMessageAsync(message);
            }

            return new AnalysisTaskStepProcessingResult();
        }

        private static StartAnalysisTaskStepProcessing DeserializeMessage(BinaryData messageBody)
        {
            var messageJson = messageBody.ToString();
            var message = JsonSerializer.Deserialize<StartAnalysisTaskStepProcessing>(messageJson);

            if (message == null)
                throw new InvalidOperationException("Unreadable message body");

            return message;
        }
    }
}
