namespace BS.ReceiptAnalyzer.Shared.Storage
{
    public static class StorageServiceContract
    {
        public sealed record SaveFileResult(bool Success, string? Error = null);
    }
}
