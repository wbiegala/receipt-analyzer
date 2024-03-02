namespace BS.ReceiptAnalyzer.Domain.Model
{
    public enum AnalysisTaskStatus
    {
        Pending,
        OnProcessing,
        Canceled,
        Failed,
        Finished
    }
}
