using Azure.Messaging.ServiceBus;
using BS.ReceiptAnalyzer.Contract.Config;
using BS.ReceiptAnalyzer.Contract.Requests;
using BS.ReceiptAnalyzer.Domain.Events;
using MediatR;
using BS.ReceiptAnalyzer.Shared.ServiceBus;

namespace BS.ReceiptAnalyzer.Core.DomainEventHandlers
{
    internal class AnalysisTaskStartedEventHandler : INotificationHandler<AnalysisTaskStarted>
    {
        private readonly ServiceBusClient _busClient;

        public AnalysisTaskStartedEventHandler(ServiceBusClient busClient)
        {
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
        }

        public async Task Handle(AnalysisTaskStarted @event, CancellationToken cancellationToken)
        {
            var request = new StartAnalysisTaskStepProcessing { TaskId = @event.TaskId };
            var sender = _busClient.CreateSender(MessagingConfiguration.AnalysisTaskSteps.StartRequestQueue_ReceiptsRecognition);
            await sender.SendMessageAsync(request, cancellationToken);
        }
    }
}
