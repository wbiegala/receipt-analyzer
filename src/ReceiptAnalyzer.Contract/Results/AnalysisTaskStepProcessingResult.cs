namespace BS.ReceiptAnalyzer.Contract.Results
{
    public class AnalysisTaskStepProcessingResult
    {
        public Guid TaskId { get; set; }
        public int AnalysisTaskStep { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool IsSuccess { get; set; }
        public string? FailReason { get; set; }
    }
}
