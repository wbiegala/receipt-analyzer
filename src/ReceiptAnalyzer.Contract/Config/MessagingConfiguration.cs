namespace BS.ReceiptAnalyzer.Contract.Config
{
    public static class MessagingConfiguration
    {
        public static class AnalysisTaskSteps
        {
            public const string StartRequestQueue_ReceiptsRecognition = "analysis-task.step.receipt-recognition.start";
            public const string StartRequestQueue_ReceiptsPartitioning = "analysis-task.step.receipts-partitioning.start";
            public const string StartRequestQueue_ReceiptsConvertion = "analysis-task.step.receipts-convertion.start";
            public const string StartRequestQueue_ReceiptsDataMatching = "analysis-task.step.receipts-data-matching.start";
            public const string ResultsQueue = "analysis-task.step.result";
        }
    }
}
