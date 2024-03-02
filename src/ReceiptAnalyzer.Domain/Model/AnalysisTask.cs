using BS.ReceiptAnalyzer.Domain.Basic;
using BS.ReceiptAnalyzer.Domain.Events;
using BS.ReceiptAnalyzer.Domain.Model.ReceiptAnalyzed;

namespace BS.ReceiptAnalyzer.Domain.Model
{
    public sealed class AnalysisTask : AggregateRoot
    {
        private AnalysisTask() { }

        public static AnalysisTask Create(Func<DateTimeOffset> getTimestampFunc)
        {
            var task = new AnalysisTask
            {
                CreationTime = getTimestampFunc(),
                Status = AnalysisTaskStatus.Pending,
                Progression = AnalysisTaskProgression.NotStarted,
            };

            task.AddEvent(new AnalysisTaskCreated(task.Id));

            return task;
        }

        #region Properties

        public DateTimeOffset CreationTime { get; private set; }
        public DateTimeOffset? StartTime { get; private set; }
        public DateTimeOffset? EndTime { get; private set; }
        public AnalysisTaskStatus Status { get; private set; }
        public AnalysisTaskProgression Progression { get; private set; }
        public IReadOnlyCollection<AnalysisTaskStep> ProgressionDetails => _progressionDetails.ToList();
        public IReadOnlyCollection<Receipt>? AnalysisResult => _result.Any() ? _result : null;
        public string? FailReason { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Starts processing of task (sets statuses and publish event)
        /// </summary>
        public void Start(Func<DateTimeOffset> getTimestampFunc)
        {
            if (Status != AnalysisTaskStatus.Pending)
                return;

            StartTime = getTimestampFunc();
            Status = AnalysisTaskStatus.OnProcessing;
            Progression = AnalysisTaskProgression.Scheduled;

            AddEvent(new AnalysisTaskStarted(Id));
        }

        /// <summary>
        /// Cancels processing of task. Current step will be finished.
        /// </summary>
        public void Cancel(Func<DateTimeOffset> getTimestampFunc)
        {
            if (Status != AnalysisTaskStatus.OnProcessing || Status != AnalysisTaskStatus.Pending)
                return;

            EndTime = getTimestampFunc();
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
        public void FinishTask(Func<DateTimeOffset> getTimestampFunc, IEnumerable<Receipt> receipts)
        {
            SetResults(receipts);

            Status = AnalysisTaskStatus.Finished;
            Progression = AnalysisTaskProgression.Finished;
            EndTime = getTimestampFunc();

            AddEvent(new AnalysisTaskFinished(Id));
        }

        /// <summary>
        /// Finishes task with failed status and sets reason
        /// </summary>
        public void FailTask(Func<DateTimeOffset> getTimestampFunc, string reason)
        {
            FailReason = reason;
            Status = AnalysisTaskStatus.Failed;
            EndTime = getTimestampFunc();

            AddEvent(new AnalysisTaskFailed(Id));
        }


        private void SetResults(IEnumerable<Receipt> receipts)
        {
            foreach (var receipt in receipts)
            {
                _result.Add(receipt);
            }
        }

        #endregion

        private HashSet<AnalysisTaskStep> _progressionDetails = new HashSet<AnalysisTaskStep>();
        private HashSet<Receipt> _result = new HashSet<Receipt>();
    }
}
