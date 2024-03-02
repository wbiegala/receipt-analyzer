namespace BS.ReceiptAnalyzer.Domain.Model
{
    public enum AnalysisTaskProgression
    {
        NotStarted = -1,
        Scheduled = 0,
        ReceiptsRecognition = 20,
        ReceiptsPartitioning = 40,
        ReceiptsConvertion = 60,
        ReceiptsDataMatching = 80,
        Finished = 100
    }
}
