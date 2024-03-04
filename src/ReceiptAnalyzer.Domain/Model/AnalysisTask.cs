using BS.ReceiptAnalyzer.Domain.Basic;
using BS.ReceiptAnalyzer.Domain.Events;
using BS.ReceiptAnalyzer.Domain.Exceptions;
using BS.ReceiptAnalyzer.Domain.Model.ReceiptAnalyzed;

namespace BS.ReceiptAnalyzer.Domain.Model
{
    public sealed class AnalysisTask : AggregateRoot
    {
        private AnalysisTask() { }

        public static AnalysisTask Create()
        {
            var task = new AnalysisTask
            {
                CreationTime = DateTimeOffset.Now,
                Status = AnalysisTaskStatus.Pending,
                Progression = AnalysisTaskProgression.NotStarted,
            };

            task.AddEvent(new AnalysisTaskCreated(task.Id));

            return task;
        }

        #region Properties

        public string ImageHash { get; private set; }
        public DateTimeOffset CreationTime { get; private set; }
        public DateTimeOffset? StartTime { get; private set; }
        public DateTimeOffset? EndTime { get; private set; }
        public AnalysisTaskStatus Status { get; private set; }
        public AnalysisTaskProgression Progression { get; private set; }
        public IReadOnlyCollection<AnalysisTaskStep> ProgressionDetails => _progressionDetails.ToList();
        public AnalysisTaskResult? Results { get; private set; }
        public string? FailReason { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Starts processing of task (sets statuses and publish event)
        /// </summary>
        public void Start(string imageHash)
        {
            if (Status != AnalysisTaskStatus.Pending)
                throw new InvalidStateException("Can't start task that is started already.");

            if (string.IsNullOrEmpty(imageHash))
                throw new ArgumentException($"'{nameof(imageHash)}' can't be null or empty");

            ImageHash = imageHash;
            StartTime = DateTimeOffset.Now;
            Status = AnalysisTaskStatus.OnProcessing;
            Progression = AnalysisTaskProgression.Scheduled;

            AddEvent(new AnalysisTaskStarted(Id));
        }

        /// <summary>
        /// Cancels processing of task. Current step will be finished.
        /// </summary>
        public void Cancel()
        {
            if (Status != AnalysisTaskStatus.OnProcessing && Status != AnalysisTaskStatus.Pending)
                throw new InvalidStateException("Can't cancel task that is canceled, failed or finished.");

            EndTime = DateTimeOffset.Now;
            Status = AnalysisTaskStatus.Canceled;

            AddEvent(new AnalysisTaskCanceled(Id));
        }

        /// <summary>
        /// Notifies progression
        /// </summary>
        public void NotifyStepFinished(AnalysisTaskStep step)
        {
            _progressionDetails.Add(step);
            Progression = step.StepType;

            AddEvent(new AnalysisTaskProgressionNotified(Id));
        }

        /// <summary>
        /// Finishes task with success status and sets results
        /// </summary>
        public void Success(IEnumerable<Receipt> receipts)
        {
            if (Status != AnalysisTaskStatus.OnProcessing)
                throw new InvalidStateException("Can't finish task that is already finished or not started yet.");

            if (receipts == null || !receipts.Any())
                throw new ArgumentException($"'{nameof(receipts)}' can't be null or empty");

            Results = new AnalysisTaskResult { Receipts = receipts };

            Status = AnalysisTaskStatus.Finished;
            Progression = AnalysisTaskProgression.Finished;
            EndTime = DateTimeOffset.Now;

            AddEvent(new AnalysisTaskFinished(Id));
        }

        /// <summary>
        /// Finishes task with failed status and sets reason
        /// </summary>
        public void Fail(string reason)
        {
            if (Status != AnalysisTaskStatus.OnProcessing)
                throw new InvalidStateException("Can't finish task that is already finished or not started yet.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException($"'{nameof(reason)}' can't be null or empty");

            FailReason = reason;
            Status = AnalysisTaskStatus.Failed;
            EndTime = DateTimeOffset.Now;

            AddEvent(new AnalysisTaskFailed(Id));
        }

        #endregion

        private HashSet<AnalysisTaskStep> _progressionDetails = new();
    }
}
