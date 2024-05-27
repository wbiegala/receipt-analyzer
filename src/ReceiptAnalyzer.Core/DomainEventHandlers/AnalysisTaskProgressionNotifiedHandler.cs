using Azure.Messaging.ServiceBus;
using BS.ReceiptAnalyzer.Contract.Config;
using BS.ReceiptAnalyzer.Contract.Requests;
using BS.ReceiptAnalyzer.Domain.Events;
using BS.ReceiptAnalyzer.Domain.Model;
using BS.ReceiptAnalyzer.Shared.ServiceBus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.DomainEventHandlers
{
    internal class AnalysisTaskProgressionNotifiedHandler : INotificationHandler<AnalysisTaskProgressionNotified>
    {
        private readonly ILogger<AnalysisTaskProgressionNotifiedHandler> _logger;
        private readonly ServiceBusClient _busClient;

        public AnalysisTaskProgressionNotifiedHandler(ServiceBusClient busClient,
            ILogger<AnalysisTaskProgressionNotifiedHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
        }

        public async Task Handle(AnalysisTaskProgressionNotified @event, CancellationToken cancellationToken)
        {
            if (!@event.Success)
                return;

            var queueName = GetQueueName(@event.StepType);

            if (string.IsNullOrWhiteSpace(queueName))
                return;

            var sender = _busClient.CreateSender(queueName);
            var request = new StartAnalysisTaskStepProcessing { TaskId = @event.TaskId };
            await sender.SendMessageAsync(request, cancellationToken);
        }

        private static string? GetQueueName(AnalysisTaskProgression stepType) =>
            stepType switch {
                AnalysisTaskProgression.ReceiptsRecognition => MessagingConfiguration.AnalysisTaskSteps.StartRequestQueue_ReceiptsPartitioning,
                AnalysisTaskProgression.ReceiptsPartitioning => MessagingConfiguration.AnalysisTaskSteps.StartRequestQueue_ReceiptsConvertion,
                AnalysisTaskProgression.ReceiptsConvertion => MessagingConfiguration.AnalysisTaskSteps.StartRequestQueue_ReceiptsDataMatching,
                _ => null
            };
    }
}
