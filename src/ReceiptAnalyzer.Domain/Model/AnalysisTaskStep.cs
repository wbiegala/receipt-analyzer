using BS.ReceiptAnalyzer.Domain.Basic;

namespace BS.ReceiptAnalyzer.Domain.Model
{
    public sealed class AnalysisTaskStep : Entity
    {
        public AnalysisTaskProgression StepType { get; private set; }
        public bool Success { get; private set; }
        public DateTimeOffset NotificationTime { get; private set; }
        public DateTimeOffset StartTime { get; private set; }
        public DateTimeOffset EndTime { get; private set; }

        private AnalysisTaskStep() { }

        internal static AnalysisTaskStep Create(AnalysisTaskProgression type,
            bool success,
            DateTimeOffset notificationTime,
            DateTimeOffset startTime,
            DateTimeOffset endTime)
        {
            return new AnalysisTaskStep
            {
                StepType = type,
                Success = success,
                NotificationTime = notificationTime,
                StartTime = startTime,
                EndTime = endTime,
            };
        }
    }
}