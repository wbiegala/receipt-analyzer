using BS.ReceiptAnalyzer.Contract.Config;
using BS.ReceiptAnalyzer.Contract.Results;
using BS.ReceiptAnalyzer.Core.Commands.FailAnalysisTask;
using BS.ReceiptAnalyzer.Core.Commands.NotifyAnalysisTaskProgress;
using BS.ReceiptAnalyzer.Shared.ServiceBus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.ResultsConsumers
{
    internal class AnalysisTaskStepProcessingResultConsumer : ServiceBusConsumer<AnalysisTaskStepProcessingResult>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AnalysisTaskStepProcessingResultConsumer> _logger;

        public override string QueueOrSubscription => MessagingConfiguration.AnalysisTaskSteps.ResultsQueue;


        public AnalysisTaskStepProcessingResultConsumer(IMediator mediator,
            ILogger<AnalysisTaskStepProcessingResultConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task ConsumeAsync(AnalysisTaskStepProcessingResult message,
            CancellationToken cancellationToken)
        {
            try
            {
                await NotifyAnalysisTask(message, cancellationToken);

                if (!message.IsSuccess)
                {
                    await FailAnalysisTask(message, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null);
            }
        }


        private Task NotifyAnalysisTask(AnalysisTaskStepProcessingResult message,
            CancellationToken cancellationToken)
        {
            var command = new NotifyAnalysisTaskProgressCommand(
                    message.TaskId,
                    message.AnalysisTaskStep,
                    message.IsSuccess,
                    message.StartTime,
                    message.EndTime);
            return _mediator.Send(command, cancellationToken);
        }

        private Task FailAnalysisTask(AnalysisTaskStepProcessingResult message,
            CancellationToken cancellationToken)
        {
            var command = new FailAnalysisTaskCommand(message.TaskId, message.FailReason!);
            return _mediator.Send(command, cancellationToken);
        }
    }
}
