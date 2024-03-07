using BS.ReceiptAnalyzer.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.EventHandlers.Domain
{
    internal class AnalysisTaskStartedEventHandler : INotificationHandler<AnalysisTaskStarted>
    {
        private readonly ILogger<AnalysisTaskStartedEventHandler> _logger;

        public AnalysisTaskStartedEventHandler(ILogger<AnalysisTaskStartedEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AnalysisTaskStarted notification, CancellationToken cancellationToken)
        {
            //TODO
            _logger.LogInformation("task started");
        }
    }
}
