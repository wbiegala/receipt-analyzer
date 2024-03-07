using BS.ReceiptAnalyzer.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.EventHandlers.Domain
{
    internal class AnalysisTaskFailedEventHandler : INotificationHandler<AnalysisTaskFailed>
    {
        private readonly ILogger<AnalysisTaskFailedEventHandler> _logger;

        public AnalysisTaskFailedEventHandler(ILogger<AnalysisTaskFailedEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AnalysisTaskFailed notification, CancellationToken cancellationToken)
        {
            //TODO
            _logger.LogInformation("task failed");
        }
    }
}
